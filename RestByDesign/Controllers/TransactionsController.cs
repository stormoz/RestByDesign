using System;
using System.Web.Http;
using RestByDesign.Models;

namespace RestByDesign.Controllers
{
    public class TransactionsController : ApiController
    {
        //GET /accounts/123/transactions
        //GET /accounts/123/transactions?fields=name,number,balance
        //GET /accounts/123/transactions?skip=10&take=10
        //GET /accounts/123/transactions?dateFrom=31122013&dateTo=31032014&amountFrom=5000
        public IHttpActionResult Get(string accountId, string fields, PagingInfo pagingInfo, TransactionFilter transactionFilter)
        {
            throw new NotImplementedException();
        }
    }
}
