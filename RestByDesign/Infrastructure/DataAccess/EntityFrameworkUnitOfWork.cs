using System;
using PersonalBanking.Domain.Model;
using PersonalBanking.Domain.Model.Core;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly RestByDesignContext _context;

        public IGenericRepository<Client> ClientRepository { get; private set; }
        public IGenericRepository<Account> AccountRepository { get; private set; }
        public IGenericRepository<SmartTag> SmartTagRepository { get; private set; }
        public IGenericRepository<Transaction> TransactionRepository { get; private set; }

        public EntityFrameworkUnitOfWork(
            RestByDesignContext context, 
            IGenericRepository<Client> clientRepository,
            IGenericRepository<Account> accountRepository,  
            IGenericRepository<SmartTag> smartTagRepository,
            IGenericRepository<Transaction> transactionRepository
            )
        {
            _context = context;
            ClientRepository = clientRepository;
            AccountRepository = accountRepository;
            SmartTagRepository = smartTagRepository;
            TransactionRepository = transactionRepository;
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {
            if (typeof(TEntity) == typeof(Client))
                return ClientRepository as IGenericRepository<TEntity>;

            if (typeof(TEntity) == typeof(Account))
                return AccountRepository as IGenericRepository<TEntity>;

            if (typeof(TEntity) == typeof(SmartTag))
                return SmartTagRepository as IGenericRepository<TEntity>;

            if (typeof(TEntity) == typeof(Transaction))
                return TransactionRepository as IGenericRepository<TEntity>;

            throw new ArgumentOutOfRangeException(typeof(TEntity).ToString(), "No repo found for this type");
        }
    }
}