using OrganicMarket.Core.Models;
using OrganicMarket.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace OrganicMarket.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        Task CommitAsync();

        void Rollback();

    }
}
