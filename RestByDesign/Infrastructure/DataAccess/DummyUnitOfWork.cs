using PersonalBanking.Domain.Model;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class DummyUnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Client, string> ClientRepository { get; private set; }
        public IGenericRepository<Account, string> AccountRepository { get; private set; }

        public DummyUnitOfWork(
            IGenericRepository<Client, string> clientRepository,
            IGenericRepository<Account, string> accountRepository)
        {
            ClientRepository = clientRepository;
            AccountRepository = accountRepository;
        }

        public void Dispose()
        { }

        public void SaveChanges()
        { }
    }
}