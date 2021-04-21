using System;
using System.Net;
using System.Threading.Tasks;
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
        public async Task GetClaims()
        {
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
            var result = await controller.Get(Guid.Parse("5570d15a-b679-4f20-83f8-f96b2b3b9bc8"));


            var contentResult = result as OkObjectResult;
            var returnValue = contentResult.Value as Claim;

            // Assert
            Assert.AreEqual(contentResult.StatusCode, 200);
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(Guid.Parse("5570d15a-b679-4f20-83f8-f96b2b3b9bc8"), returnValue.ClaimId);

        }

        [TestMethod]
        public async Task CreateClaim()
        {
            // Arrange
            Claim testClaim = new Claim
            {
                Description = "My test claim",
                Payout = 999.99
            };

            // Act
            var result = await claimController.Post(testClaim);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            var returnValue = createdAtActionResult.Value as Claim;

            Assert.AreEqual(createdAtActionResult.StatusCode, 201);
            Assert.AreEqual(returnValue.Description, testClaim.Description);
            Assert.AreEqual(returnValue.Payout, testClaim.Payout);
        }
    }
}
