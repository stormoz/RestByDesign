using PersonalBanking.Domain.Model;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class DummyUnitOfWork : IUnitOfWork
    {
        private readonly IGenericRepository<Client, string> _clientRepository;

        public DummyUnitOfWork(IGenericRepository<Client, string> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public IGenericRepository<Client, string> ClientRepository
        {
            get { return _clientRepository; }
        } 

        public void Dispose()
        { }

        public void SaveChanges()
        { }
    }
}