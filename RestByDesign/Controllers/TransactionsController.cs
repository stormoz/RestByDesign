using System;
using System.Web.Http;
using RestByDesign.Controllers.Base;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Models;
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
            [FromUri]TransactionFilter transactionFilter = null)
        {
            throw new NotImplementedException();
        }
    }
}
