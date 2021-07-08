using OrganicMarket.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganicMarket.Core.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> Get();
        Task<User> GetById(int id);
        Task<AuthenticationResult> Post(User newUser);
        Task Update(User userToBeUpdated, User user);
        Task Delete(User user);

        Task<AuthenticationResult> Authenticate(string userName, string password);
    }
}