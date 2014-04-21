using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.Extensions;
using RestByDesign.Infrastructure.Mappers;
using RestByDesign.Models;
using RestByDesign.Models.Helpers;
using System.Net;
using System.Web.Http;

namespace RestByDesign.Controllers
{
    public class ClientsController : BaseApiController
    {
        public ClientsController(IUnitOfWork uow) : base(uow)
        {  }

        //GET /clients/123
        public IHttpActionResult GetById(string id, string fields = null)
        {
            var client = UnitOfWork.ClientRepository.GetById(id);

            if (client == null)
                return NotFound();

            var clientModel = ModelMapper.Map<Client, ClientModel>(client);

            return Ok(clientModel.SelectFields(fields));
        }

        //GET /clients
        public IHttpActionResult Get([FromUri]PagingInfo pagingInfo, string fields = null)
        {
            var clients = UnitOfWork.ClientRepository.Get(pagingInfo:pagingInfo);
            var clientsModel = ModelMapper.Map<Client, ClientModel>(clients);

            var data = new CollectionWrapper(clientsModel.SelectFields(fields), pagingInfo);
            return Ok(data);
        }

        [HttpPost]
        [HttpPut]
        [HttpPatch]
        [HttpDelete]
        public IHttpActionResult NotAllowed(string id = null)
        {
            throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);  
        }

        //OData example
        /*
        [Queryable]
        public SingleResult<ClientModel> GetById(string id)
        {
            var client = UnitOfWork.ClientRepository.GetById(id);
            var clientModel = ModelMapper.Map<Client, ClientModel>(client);

            return ODataHelper.Single(clientModel);
        }
        */
    }
}