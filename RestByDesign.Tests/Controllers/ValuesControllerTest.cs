using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestByDesign;
using RestByDesign.Controllers;
using TestStack.Seleno.Extensions;

namespace RestByDesign.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            //// Arrange
            //ValuesController controller = new ValuesController();

            //// Act
            //IEnumerable<string> result = controller.Get();

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(2, result.Count());
            //Assert.AreEqual("value1", result.ElementAt(0));
            //Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void  GetById()
        {
            //// Arrange
            //ValuesController controller = new ValuesController();

            //// Act
            //var result = controller.Get(5);

            //// Assert
            //HttpRequestMessage msg = new HttpRequestMessage();
            //msg.CreateResponse(HttpStatusCode.OK, "value");
            //Assert.AreEqual(new OkResult(msg), result);
        }

        [TestMethod]
        public void Post()
        {
            //// Arrange
            //ValuesController controller = new ValuesController();

            //// Act
            //controller.Post("value");

            //// Assert
        }

        [TestMethod]
        public void Put()
        {
            //// Arrange
            //ValuesController controller = new ValuesController();

            //// Act
            //controller.Put(5, "value");

            //// Assert
        }

        [TestMethod]
        public void Delete()
        {
            //// Arrange
            //ValuesController controller = new ValuesController();

            //// Act
            //controller.Delete(5);

            //// Assert
        }
    }
}
