using System;
using PersonalBanking.Domain.Model;
using PersonalBanking.Domain.Model.Core;

namespace RestByDesign.Infrastructure.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Client, string> ClientRepository { get; }
        IGenericRepository<Account, string> AccountRepository { get; }
        IGenericRepository<SmartTag, string> SmartTagRepository { get; }
        IGenericRepository<Transaction, string> TransactionRepository { get; }

        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class, IEntity<TKey>;
        void SaveChanges();
    }
}