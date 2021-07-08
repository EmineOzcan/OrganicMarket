using CommonServiceLocator;
using OrganicMarket.Core;
using OrganicMarket.Core.Models;
using OrganicMarket.Core.Repositories;
using OrganicMarket.Data;
using OrganicMarket.Data.Repositories;
using System;
using System.Threading.Tasks;
namespace MusicMarket.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrganicMarketContext _context;

        private UserRepository _userRepository;

        public UnitOfWork(OrganicMarketContext context)
        {
            this._context = context;
        }

        public IUserRepository User => _userRepository = _userRepository ?? new UserRepository(_context);


        public async Task CommitAsync()
        {
             _context.BeginTransaction();
              await _context.Commit();
             
        }

        public  void Rollback()
        {
            _context.Rollback();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}