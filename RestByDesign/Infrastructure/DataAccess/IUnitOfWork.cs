using System;
using PersonalBanking.Domain.Model;

namespace RestByDesign.Infrastructure.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Client, string> ClientRepository { get; }
        IGenericRepository<Account, string> AccountRepository { get; }
        void SaveChanges();
    }
}