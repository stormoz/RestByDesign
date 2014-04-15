using System;
using System.Data.Entity;
using PersonalBanking.Domain.Model;
using RestByDesign.Models;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly RestByDesignContext _context;
        private readonly IGenericRepository<Client, string> _clientRepository;

        public EntityFrameworkUnitOfWork(RestByDesignContext context, IGenericRepository<Client, string> clientRepository)
        {
            _context = context;
            _clientRepository = clientRepository;
        }

        public IGenericRepository<Client, string> ClientRepository
        {
            get { return _clientRepository; }
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