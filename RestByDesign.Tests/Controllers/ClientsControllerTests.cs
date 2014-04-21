using System.Collections.Generic;
using System.Web.Http.Results;
using NSubstitute;
using NUnit.Framework;
using PersonalBanking.Domain.Model;
using RestByDesign.Controllers;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Models;
using Shouldly;
using System.Net.Http;
using System.Web.Http;

namespace RestByDesign.Tests.Controllers
{
    public class ClientsControllerTests
    {
        private readonly ClientsController clientController;
        private readonly List<Client> clients;

        public ClientsControllerTests()
        {
            MappingRegistration.RegisterMappings();

            clients = DummyDataHelper.GetClients();
            var clientRepo = new DummyGenericRepository<Client, string>(clients);

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
