using System;
using System.Web.Http.Cors;
using PersonalBanking.Domain.Model;
using RestByDesign.Controllers.Base;
using RestByDesign.Infrastructure.Core.Extensions;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.Mapping;
using RestByDesign.Models;
using RestByDesign.Models.Helpers;
using System.Web.Http;

namespace RestByDesign.Controllers
{
    //[EnableCors("*", "*", "*")] //Example of CORS support per controller (http://msdn.microsoft.com/en-us/magazine/dn532203.aspx)
    public class ClientsController : BaseApiController
    {
        public ClientsController(IUnitOfWork uow) : base(uow)
        {  }

        public IHttpActionResult GetById(string id, string fields = null)
        {
            var client = UnitOfWork.ClientRepository.GetSingle(x=>x.Id == id);

            if (client == null)
                return NotFound();

            var clientModel = ModelMapper.Map<Client, ClientModel>(client);

            return Ok(clientModel.SelectFields(fields));
        }

        public IHttpActionResult Get([FromUri]PagingInfo pagingInfo, string fields = null)
        {
            var clients = UnitOfWork.ClientRepository.Get(pagingInfo:pagingInfo);
            var clientsCount = UnitOfWork.ClientRepository.Count();
            var clientsModel = ModelMapper.Map<Client, ClientModel>(clients);

            return OkCollection(clientsModel.SelectFields(fields), clientsCount);
        }

        //OData example
        /*
        [Queryable]
        public SingleResult<ClientModel> GetById(string id)
        {
            var client = UnitOfWork.ClientRepository.GetSingle(x=>x.Id == id);
            var clientModel = ModelMapper.Map<Client, ClientModel>(client);

            return ODataHelper.Single(clientModel);
        }
        */
    }
}