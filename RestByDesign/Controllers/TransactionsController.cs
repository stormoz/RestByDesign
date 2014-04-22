using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using PersonalBanking.Domain.Model;
using RestByDesign.Controllers.Base;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.Extensions;
using RestByDesign.Infrastructure.Mappers;
using RestByDesign.Models;
using RestByDesign.Models.Enums;
using RestByDesign.Models.Helpers;

namespace RestByDesign.Controllers
{
    public class TransactionsController : BaseApiController
    {
        public TransactionsController(IUnitOfWork uow) : base(uow)
        {
        }

        //GET /accounts/123/transactions
        //GET /accounts/123/transactions?fields=name,number,balance
        //GET /accounts/123/transactions?skip=10&take=10
        //GET /accounts/123/transactions?dateFrom=31122013&dateTo=31032014&amountFrom=5000
        [Route("api/accounts/{accountId}/transactions")]
        public IHttpActionResult Get(string accountId,
            string fields = null,
            [FromUri]PagingInfo pagingInfo = null,
            [FromUri]TransactionFilter filter = null)
        {
            var transactions = UnitOfWork.TransactionRepository.Get(
                TransactionSearchExpression(accountId,filter)
                , pagingInfo: pagingInfo).ToList();

            var transactionsModel = ModelMapper.Map<Transaction, TransactionModel>(transactions);

            return Ok(transactionsModel.SelectFields(fields));
        }

        private static Expression<Func<Transaction, bool>> TransactionSearchExpression(string accountId, TransactionFilter filter)
        {
            Expression<Func<Transaction, bool>> findByAccountId = tr => tr.AccountId.Equals(accountId);
            return findByAccountId.And(filter.GetFilterExpression());
        }
    }
}
