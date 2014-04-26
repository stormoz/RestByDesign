using System.Linq;
using System.Net;
using System.Web.Http;
using PersonalBanking.Domain.Model;
using RestByDesign.Controllers.Base;
using RestByDesign.Infrastructure.Core.Extensions;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.Mapping;
using RestByDesign.Models;
using RestByDesign.Services;

namespace RestByDesign.Controllers
{
    public class TransfersController : BaseApiController
    {
        private readonly ITransferService _transferService;

        public TransfersController(IUnitOfWork uow, ITransferService transferService) : base(uow)
        {
            _transferService = transferService;
        }

        [Route("api/clients/{clientId}/transfers")]
        public IHttpActionResult Post(string clientId, [FromBody]TransferModel transferModel)
        {
            if(!ModelState.IsValid)
                return Fail("Model state is invalid", data: new { errors = ModelState.Errors() });

            var transfer = ModelMapper.Map<TransferModel, Transfer>(transferModel);

            var transferResult = _transferService.MakeTransfer(clientId, transfer);

            if (!transferResult.Errors.Any())
            {
                return StatusCode(HttpStatusCode.Created);
            }

            return Fail("Could not make a transfer", data: transferResult.Errors);
        }
    }
}