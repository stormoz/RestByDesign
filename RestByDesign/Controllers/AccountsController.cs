using System;
using System.Web.Http;

namespace RestByDesign.Controllers
{
    public class AccountsController : ApiController
    {
        //GET /clients/123/accounts
        public IHttpActionResult Get(string clientId)
        {
            throw new NotImplementedException();
        }

        //GET /clients/123/accounts/2
        public IHttpActionResult Get(string clientId, string id)
        {
            throw new NotImplementedException();
        }

        //GET /accounts/123456
        public IHttpActionResult GetById(string accountId)
        {
            throw new NotImplementedException();
        }
    }
}
