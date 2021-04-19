using System;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestingTests.Controllers;
using TestingTests.Models;

namespace ActualTesting
{
    [TestClass]
    public class ClaimsControllerUnitTests
    {
        MockRepositoryClaims repository;
        ClaimsController claimController;

        [TestInitialize]
        public void InitializeForTests()
        {
            repository = new MockRepositoryClaims();
            claimController = new ClaimsController(repository);
        }

        [TestMethod]
        public async void GetClaims()
        {
            //// act
            //var result = await claimController.Get();
            //var okResult = result as OkObjectResult;

            //// assert
            //Assert.IsNotNull(okResult);
            //Assert.AreEqual(200, okResult.StatusCode);

            // Arrange
            var mockRepository = new Mock<IRepositoryClaims>();
            mockRepository.Setup(c => c.Get(Guid.Parse("5570d15a-b679-4f20-83f8-f96b2b3b9bc8")))
                .ReturnsAsync(new Claim
                {
                    ClaimId = Guid.Parse("5570d15a-b679-4f20-83f8-f96b2b3b9bc8"),
                    Description = "Mocked claim number 1",
                    Payout = 2000
                });

            var controller = new ClaimsController(mockRepository.Object);

            // Act
            IActionResult actionResult = await controller.Get(Guid.Parse("5570d15a-b679-4f20-83f8-f96b2b3b9bc8"));
            var contentResult = actionResult as OkNegotiatedContentResult<Claim>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(Guid.Parse("5570d15a-b679-4f20-83f8-f96b2b3b9bc8"), contentResult.Content.ClaimId);

        }

        [TestMethod]
        public async void CreateClaim()
        {
            Claim testClaim = new Claim
            {
                Description = "My test claim",
                Payout = 999.99
            };

            // act
            var result = await claimController.Post(testClaim);
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
