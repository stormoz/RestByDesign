using System;
using PersonalBanking.Domain.Model;
using PersonalBanking.Domain.Model.Core;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class DummyUnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Client> ClientRepository { get; private set; }
        public IGenericRepository<Account> AccountRepository { get; private set; }
        public IGenericRepository<SmartTag> SmartTagRepository { get; private set; }
        public IGenericRepository<Transaction> TransactionRepository { get; private set; }

        public DummyUnitOfWork(
            IGenericRepository<Client> clientRepository,
            IGenericRepository<Account> accountRepository,
            IGenericRepository<SmartTag> smartTagRepository,
            IGenericRepository<Transaction> transactionRepository)
        {
            ClientRepository = clientRepository;
            AccountRepository = accountRepository;
            SmartTagRepository = smartTagRepository;
            TransactionRepository = transactionRepository;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {
            if (typeof (TEntity) == typeof (Client))
                return ClientRepository as IGenericRepository<TEntity>;

            if (typeof(TEntity) == typeof(Account))
                return AccountRepository as IGenericRepository<TEntity>;

            if (typeof(TEntity) == typeof(SmartTag))
                return SmartTagRepository as IGenericRepository<TEntity>;

            if (typeof(TEntity) == typeof(Transaction))
                return TransactionRepository as IGenericRepository<TEntity>;

            throw new ArgumentOutOfRangeException(typeof (TEntity).ToString(), "No repo found for this type");
        }

        public void Dispose()
        { }

        public void SaveChanges()
        { }
    }
}