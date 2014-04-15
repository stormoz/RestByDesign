using System;
using System.Web.Http;
using RestByDesign.Models;

namespace RestByDesign.Controllers
{
    public class TransfersController : ApiController
    {
        //POST /clients/123/transfers
        public IHttpActionResult Post(string clientId, TransferModel transfer)
        {
            if(!ModelState.IsValid)
                throw new Exception("");

           throw new NotImplementedException();
        }
    }
}