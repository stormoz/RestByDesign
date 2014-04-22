using System;
using System.Web.Http;
using RestByDesign.Models;

namespace RestByDesign.Controllers
{
    public class TransfersController : ApiController
    {
        //POST /clients/123/transfers
        [Route("api/clients/{clientId}/transfers")]
        public IHttpActionResult Post(string clientId, [FromBody]TransferModel transfer)
        {
            if(!ModelState.IsValid)
                throw new Exception("");

           throw new NotImplementedException();
        }
    }
}