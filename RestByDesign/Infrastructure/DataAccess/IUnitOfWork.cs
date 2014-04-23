using System;
using PersonalBanking.Domain.Model;
using PersonalBanking.Domain.Model.Core;

namespace RestByDesign.Infrastructure.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Client> ClientRepository { get; }
        IGenericRepository<Account> AccountRepository { get; }
        IGenericRepository<SmartTag> SmartTagRepository { get; }
        IGenericRepository<Transaction> TransactionRepository { get; }

        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
        void SaveChanges();
    }
}