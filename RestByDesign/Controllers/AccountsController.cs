using System.Linq;
using System.Web.Http;
using PersonalBanking.Domain.Model;
using RestByDesign.Controllers.Base;
using RestByDesign.Infrastructure.Core.Extensions;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.Mapping;
using RestByDesign.Models;
using RestByDesign.Models.Helpers;

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
            var accountsCount = UnitOfWork.AccountRepository.Count(acc => acc.ClientId.Equals(clientId));

            var accountModel = ModelMapper.Map<Account, AccountModel>(accounts);

            return OkCollection(accountModel.SelectFields(fields), accountsCount);
        }

        [Route("api/clients/{clientId}/accounts/{accountNum}")]
        public IHttpActionResult Get(string clientId, int accountNum, string fields = null)
        {
            var account = UnitOfWork.AccountRepository.Get(acc => acc.ClientId.Equals(clientId),
                accounts => accounts.OrderBy(acc => acc.Id), new PagingInfo(accountNum - 1, 1)).SingleOrDefault();

            if (account == null)
                return NotFound();

            var accountModel = ModelMapper.Map<Account, AccountModel>(account);

            return Ok(accountModel.SelectFields(fields));
        }

        [Route("api/accounts/{accountId}")]
        public IHttpActionResult GetById(string accountId, string fields = null)
        {
            var account = UnitOfWork.AccountRepository.GetSingle(acc => acc.Id.Equals(accountId));

            if (account == null)
                return NotFound();

            var accountModel = ModelMapper.Map<Account, AccountModel>(account);

            return Ok(accountModel.SelectFields(fields));
        }
    }
}
