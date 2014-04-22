using System;
using PersonalBanking.Domain.Model;
using PersonalBanking.Domain.Model.Core;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly RestByDesignContext _context;
        public IGenericRepository<Client, string> ClientRepository { get; private set; }
        public IGenericRepository<Account, string> AccountRepository { get; private set; }
        public IGenericRepository<SmartTag, string> SmartTagRepository { get; private set; }


        public EntityFrameworkUnitOfWork(RestByDesignContext context, 
            IGenericRepository<Client, string> clientRepository,
            IGenericRepository<Account, string> accountRepository,
            IGenericRepository<SmartTag, string> smartTagRepository)
        {
            _context = context;
            ClientRepository = clientRepository;
            AccountRepository = accountRepository;
            SmartTagRepository = smartTagRepository;
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

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class, IEntity<TKey>
        {
            throw new NotImplementedException();
        }
    }
}