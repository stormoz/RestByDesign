using System;
using PersonalBanking.Domain.Model;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly RestByDesignContext _context;
        public IGenericRepository<Client, string> ClientRepository { get; private set; }
        public IGenericRepository<Account, string> AccountRepository { get; private set; }

        public EntityFrameworkUnitOfWork(RestByDesignContext context, 
            IGenericRepository<Client, string> clientRepository,
            IGenericRepository<Account, string> accountRepository)
        {
            _context = context;
            ClientRepository = clientRepository;
            AccountRepository = accountRepository;
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
    }
}