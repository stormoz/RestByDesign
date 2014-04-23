using System.Linq;
using System.Net;
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

        [Route("api/clients/{clientId}/accounts")]
        public IHttpActionResult GetByClientId(string clientId, string fields = null)
        {
            var accounts = UnitOfWork.AccountRepository.Get(acc => acc.ClientId.Equals(clientId)).ToList();

            var accountModel = ModelMapper.Map<Account, AccountModel>(accounts);

            return Ok(accountModel.SelectFields(fields));
        }

        [Route("api/clients/{clientId}/accounts/{accountNum}")]
        public IHttpActionResult Get(string clientId, int accountNum, string fields = null)
        {
            var account = UnitOfWork.AccountRepository.Get(acc => acc.ClientId.Equals(clientId),
                accounts => accounts.OrderBy(acc => acc.Id)).Skip(accountNum - 1).Take(1).SingleOrDefault();

            if (account == null)
                return NotFound();

            var accountModel = ModelMapper.Map<Account, AccountModel>(account);

            return Ok(accountModel.SelectFields(fields));
        }

        [Route("api/accounts/{accountId}")]
        public IHttpActionResult GetById(string accountId, string fields = null)
        {
            var account = UnitOfWork.AccountRepository.Get(acc => acc.Id.Equals(accountId)).SingleOrDefault();

            if (account == null)
                return NotFound();

            var accountModel = ModelMapper.Map<Account, AccountModel>(account);

            return Ok(accountModel.SelectFields(fields));
        }

        [HttpPost]
        [HttpPut]
        [HttpPatch]
        [HttpDelete]
        public IHttpActionResult NotAllowed(string id = null)
        {
            throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
        }

        public IHttpActionResult GetAll()
        {
            throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
        }
    }
}
