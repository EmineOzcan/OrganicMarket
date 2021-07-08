
using Microsoft.IdentityModel.Tokens;
using OrganicMarket.Core;
using OrganicMarket.Core.Models;
using OrganicMarket.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace MusicMarket.Services
{
    public class UserService : IUserService
    {


        private readonly IUnitOfWork _unitOfWork;
        public readonly JwtSettings _jwtSettings;
        public UserService(IUnitOfWork unitOfWork, JwtSettings jwtSettings)
        {
            this._unitOfWork = unitOfWork;
            this._jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResult> Post(User newUser)
        {
            if (_unitOfWork.User.GetAll().Any((item) => item.NickName == newUser.NickName || item.Email == newUser.Email))
            {
                return new AuthenticationResult
                {
                    ResultDescription = "Nickname/E-mail is used",
                    Result = false
                };
            }

            try
            {
                await _unitOfWork.User.AddAsync(newUser);
                await _unitOfWork.CommitAsync();

                return new AuthenticationResult
                {
                    ResultDescription = "Success",
                    Result = true,
                };
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new AuthenticationResult
                {
                    ResultDescription = ex.InnerException.Message,
                    Result = false,
                };


            }
        }

        public async Task Delete(User user)
        {
            _unitOfWork.User.Remove(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<User>> Get()
        {
            return await _unitOfWork.User.GetAllAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _unitOfWork.User.GetByIdAsync(id);
        }

        public async Task Update(User artistToBeUpdated, User users)
        {
            artistToBeUpdated.NickName = users.NickName;

            await _unitOfWork.CommitAsync();
        }

        public async Task<AuthenticationResult> Authenticate(string userName, string password)
        {
            var user = await _unitOfWork.User.SingleOrDefaultAsync(x => x.NickName == userName && x.Password == password);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    ResultDescription = "User/Password is wrong",
                    Result = false
                };
            }

            // Authentication(Yetkilendirme) başarılı ise JWT token üretilir.
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.NickName.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //user.Result.  = tokenHandler.WriteToken(token);

            return new AuthenticationResult
            {
                ResultDescription = "Success",
                Result = true,
                Token = tokenHandler.WriteToken(token)
,
            };
        }
    }

}