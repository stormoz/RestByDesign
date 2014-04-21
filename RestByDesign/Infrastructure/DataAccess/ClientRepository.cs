using System;
using PersonalBanking.Domain.Model;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class ClientRepository : EfGenericRepository<Client, string>
    {
        public ClientRepository(RestByDesignContext context) : base(context)
        {
        }

        public override void Delete(Client entityToDelete)
        {
            throw new NotSupportedException();
        }

        public override void Delete(string id)
        {
            throw new NotSupportedException();
        }

        public override void Insert(Client entity)
        {
            throw new NotSupportedException();
        }

        public override void Update(Client entityToUpdate)
        {
            throw new NotSupportedException();
        }
    }
}