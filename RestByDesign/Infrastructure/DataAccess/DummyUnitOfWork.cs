using System;
using PersonalBanking.Domain.Model;
using PersonalBanking.Domain.Model.Core;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class DummyUnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Client, string> ClientRepository { get; private set; }
        public IGenericRepository<Account, string> AccountRepository { get; private set; }
        public IGenericRepository<SmartTag, string> SmartTagRepository { get; private set; }
        public IGenericRepository<Transaction, string> TransactionRepository { get; private set; }

        public DummyUnitOfWork(
            IGenericRepository<Client, string> clientRepository,
            IGenericRepository<Account, string> accountRepository,
            IGenericRepository<SmartTag, string> smartTagRepository,
            IGenericRepository<Transaction, string> transactionRepository)
        {
            ClientRepository = clientRepository;
            AccountRepository = accountRepository;
            SmartTagRepository = smartTagRepository;
            TransactionRepository = transactionRepository;
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class, IEntity<TKey>
        {
            if (typeof (TEntity) == typeof (Client))
                return ClientRepository as IGenericRepository<TEntity, TKey>;

            if (typeof(TEntity) == typeof(Account))
                return AccountRepository as IGenericRepository<TEntity, TKey>;

            if (typeof(TEntity) == typeof(SmartTag))
                return SmartTagRepository as IGenericRepository<TEntity, TKey>;

            if (typeof(TEntity) == typeof(Transaction))
                return TransactionRepository as IGenericRepository<TEntity, TKey>;

            throw new ArgumentOutOfRangeException(typeof (TEntity).ToString(), "No repo found for this type");
        }

        public void Dispose()
        { }

        public void SaveChanges()
        { }
    }
}