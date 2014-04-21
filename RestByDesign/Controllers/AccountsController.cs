using System;
using System.Linq;
using System.Web.Http;
using PersonalBanking.Domain.Model;
using RestByDesign.Controllers.Base;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.Extensions;
using RestByDesign.Infrastructure.Mappers;
using RestByDesign.Models;

namespace RestByDesign.Controllers
{
    public class AccountsController : BaseApiController
    {
        public AccountsController(IUnitOfWork uow) : base(uow)
        {  }

        //GET /clients/123/accounts
        [Route("api/clients/{clientId}/accounts")]
        public IHttpActionResult GetByClientId(string clientId, string fields = null)
        {
            var accounts = UnitOfWork.AccountRepository.Get(acc => acc.ClientId.Equals(clientId));

            if (accounts == null || !accounts.Any())
                return NotFound();

            var clientModel = ModelMapper.Map<Account, AccountModel>(accounts);

            return Ok(clientModel.SelectFields(fields));
        }

        //GET /clients/123/accounts/2
        [Route("api/clients/{clientId}/accounts/{accountNum}")]
        public IHttpActionResult Get(string clientId, int accountNum, string fields = null)
        {
            var account = UnitOfWork.AccountRepository.Get(acc => acc.ClientId.Equals(clientId),
                accounts => accounts.OrderBy(acc => acc.Id)).Skip(accountNum - 1).Take(1).FirstOrDefault();

            if (account == null)
                return NotFound();

            var clientModel = ModelMapper.Map<Account, AccountModel>(account);

            return Ok(clientModel.SelectFields(fields));
        }

        //GET /accounts/123456
        [Route("api/accounts/{accountId}")]
        public IHttpActionResult GetById(string accountId, string fields = null)
        {
            var account = UnitOfWork.AccountRepository.Get(acc => acc.Id.Equals(accountId)).FirstOrDefault();

            if (account == null)
                return NotFound();

            var clientModel = ModelMapper.Map<Account, AccountModel>(account);

            return Ok(clientModel.SelectFields(fields));
        }
    }
}
