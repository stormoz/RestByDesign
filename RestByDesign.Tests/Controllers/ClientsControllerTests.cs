using System.Collections.Generic;
using System.Web.Http.Results;
using NSubstitute;
using NUnit.Framework;
using PersonalBanking.Domain.Model;
using RestByDesign.Controllers;
using RestByDesign.Infrastructure.Core.Helpers;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.Mapping;
using Shouldly;
using System.Net.Http;
using System.Web.Http;

namespace RestByDesign.Tests.Controllers
{
    //Example of controller unit test
    public class ClientsControllerTests
    {
        private ClientsController clientController;
        private IList<Client> clients;

        [SetUp]
        public void Init()
        {
            MappingRegistration.RegisterMappings();

            clients = DummyDataHelper.GetList<Client>();
            var clientRepo = new DummyGenericRepository<Client>(DummyDataHelper.GetClients());

            var uow = Substitute.For<IUnitOfWork>();
            uow.ClientRepository.Returns(clientRepo);

            clientController = new ClientsController(uow);
            clientController.Request = Substitute.For<HttpRequestMessage>();
            clientController.Configuration = Substitute.For<HttpConfiguration>();
        }

        [Test]
        public void GetById_WhenItemWithIdExists_ShouldReturnOk()
        {
            var result = clientController.GetById(clients[0].Id);

            result.ShouldBeOfType<OkNegotiatedContentResult<object>>();
        }

        [Test]
        public void GetById_WhenItemWithIdDoesNotExist_ShouldReturnNotFound()
        {
            var nonExistingId = "0";
            var result = clientController.GetById(nonExistingId);

            result.ShouldBeOfType<NotFoundResult>();
        }
    }
}
