using OrganicMarket.Core.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicMarket.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUserAsync();

        Task<User> GetUserIdAsync(int id);
    }
}
