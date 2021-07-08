using Microsoft.EntityFrameworkCore;
using OrganicMarket.Core.Models;
using OrganicMarket.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganicMarket.Data.Repositories
{

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(OrganicMarketContext context) : base(context)
        {
        }

        async Task<IEnumerable<User>> IUserRepository.GetUserAsync()
        {
            return await OrganicMarketContext.Users
                          .ToListAsync();
        }

        public Task<User> GetUserIdAsync(int id)
        {
            return OrganicMarketContext.Users
                         .SingleOrDefaultAsync(a => a.Id == id);
        }

        private OrganicMarketContext OrganicMarketContext
        {
            get { return Context as OrganicMarketContext; }
        }

    }
}