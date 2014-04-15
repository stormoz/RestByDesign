using System;
using System.Web.Http;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.Mappers;
using RestByDesign.Models;


namespace RestByDesign.Controllers
{
    public class ClientsController : BaseApiController
    {
        public ClientsController(IUnitOfWork uow) : base(uow)
        {
            
        }

        //GET /clients/
        public IHttpActionResult Get(string id)
        {
            var client = UnitOfWork.ClientRepository.GetById(id);
            var clientModel = ModelMapper.Map<Client, ClientModel>(client);
            return Ok(clientModel);
        }

        //GET /clients/123/
        //public IHttpActionResult Get(string id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}