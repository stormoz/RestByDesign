using System.Net;
using System.Web.Http;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.Extensions;
using RestByDesign.Infrastructure.Mappers;
using RestByDesign.Models;
using RestByDesign.Models.Helpers;


namespace RestByDesign.Controllers
{
    public class ClientsController : BaseApiController
    {
        public ClientsController(IUnitOfWork uow) : base(uow)
        {  }

        //GET /clients/123
        public object GetById(string id, string fields = null)
        {
            var client = UnitOfWork.ClientRepository.GetById(id);
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

        [Queryable(AllowedQueryOptions = AllowedQueryOptions.Select)]
        public IQueryable<ClientModel> Get()
        {
            var clients = UnitOfWork.ClientRepository.Get();
            var clientModel = ModelMapper.Map<Client, ClientModel>(clients);

            return ODataHelper.List(clientModel);
        }
        */
    }
}