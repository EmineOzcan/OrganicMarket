using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganicMarket.Api.DTO;
using OrganicMarket.Core.Models;
using OrganicMarket.Core.Services;
using System.Linq;

namespace OrganicMarket.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService _userService, IMapper mapper)
        {
            this._mapper = mapper;
            this._userService = _userService;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<ActionResult<AuthenticationResult>> Authenticate([FromBody] UserDTO userParam)
        {
            var result = await _userService.Authenticate(userParam.NickName, userParam.Password);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<AuthenticationResult>> Register([FromBody] UserDTO userParam)
        {

            User k = new User
            {
                Status = 1,
                NickName = userParam.NickName,
                Password = userParam.Password,
                Autority = userParam.Autority,
                Email = userParam.Email,
                FirstName = userParam.FirstName,
                LastName = userParam.LastName,
            };

            var result = await _userService.Post(k);
            return Ok(result);
        }


        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.Get();
            var musicResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
            return Ok(musicResources);
        }


    }
}
