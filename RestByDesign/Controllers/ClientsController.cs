﻿using PersonalBanking.Domain.Model;
using RestByDesign.Controllers.Base;
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

        public IHttpActionResult GetById(string id, string fields = null)
        {
            var client = UnitOfWork.ClientRepository.GetById(x=>x.Id == id);

            if (client == null)
                return NotFound();

            var clientModel = ModelMapper.Map<Client, ClientModel>(client);

            return Ok(clientModel.SelectFields(fields));
        }

        public IHttpActionResult Get([FromUri]PagingInfo pagingInfo, string fields = null)
        {
            var clients = UnitOfWork.ClientRepository.Get(pagingInfo:pagingInfo);
            var clientsModel = ModelMapper.Map<Client, ClientModel>(clients);

            return Ok(clientsModel.SelectFields(fields));
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