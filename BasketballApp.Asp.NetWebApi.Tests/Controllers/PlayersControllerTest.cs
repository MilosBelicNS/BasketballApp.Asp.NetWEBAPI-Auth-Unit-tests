using BasketballApp.Asp.NetWebApi.Controllers;
using BasketballApp.Asp.NetWebApi.Interfaces;
using BasketballApp.Asp.NetWebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace BasketballApp.Asp.NetWebApi.Tests.Controllers
{
    [TestClass]
    public class PlayersControllerTest
    {
        [TestMethod]
        public void GetReturnsProductWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<IPlayerRepository>();
            mockRepository.Setup(x => x.GetById(42)).Returns(new Player { Id = 42 });

            var controller = new PlayersController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetById(42);
            var contentResult = actionResult as OkNegotiatedContentResult<Player>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);
        }

        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IPlayerRepository>();
            var controller = new PlayersController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetById(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IPlayerRepository>();
            var controller = new PlayersController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(10, new Player { Id = 9, Name = "Player2" });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }


        [TestMethod]
        public void GetReturnsMultipleObjects()
        {
            // Arrange
            List<Player> players = new List<Player>();
            players.Add(new Player { Id = 1, Name = "Player1" });
            players.Add(new Player { Id = 2, Name = "Player2" });

            var mockRepository = new Mock<IPlayerRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(players.AsEnumerable());
            var controller = new PlayersController(mockRepository.Object);

            // Act
            IEnumerable<Player> result = controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(players.Count, result.ToList().Count);
            Assert.AreEqual(players.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(players.ElementAt(1), result.ElementAt(1));
        }
    }
}
