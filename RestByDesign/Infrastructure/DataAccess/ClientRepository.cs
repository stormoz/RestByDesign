using System;
using PersonalBanking.Domain.Model;
using RestByDesign.Models;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class ClientRepository : GenericRepository<Client, string>
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