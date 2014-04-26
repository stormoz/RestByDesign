using System;
using System.Linq;
using System.Linq.Expressions;
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
    public class TransactionsController : BaseApiController
    {
        public TransactionsController(IUnitOfWork uow) : base(uow)
        { }

        [Route("api/accounts/{accountId}/transactions")]
        public IHttpActionResult Get(string accountId,
            string fields = null,
            [FromUri]PagingInfo pagingInfo = null,
            [FromUri]TransactionFilter filter = null)
        {
            if(!ModelState.IsValid)
                return Fail("Model state is invalid", data: new { errors = ModelState.Errors() });

            var transactions = UnitOfWork.TransactionRepository.Get(
                TransactionSearchExpression(accountId,filter)
                , pagingInfo: pagingInfo).ToList();

            var transactionsModel = ModelMapper.Map<Transaction, TransactionModel>(transactions);

            return Ok(transactionsModel.SelectFields(fields));
        }

        private static Expression<Func<Transaction, bool>> TransactionSearchExpression(string accountId, TransactionFilter filter)
        {
            Expression<Func<Transaction, bool>> findByAccountId = tr => tr.AccountId.Equals(accountId);
            return filter == null ? findByAccountId : findByAccountId.And(filter.GetFilterExpression());
        }
    }
}
