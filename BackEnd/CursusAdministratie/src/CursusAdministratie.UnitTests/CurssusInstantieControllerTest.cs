using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using AutoMapper;
using CursusAdministratie.Api;
using CursusAdministratie.Api.Controllers;
using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.Services.Interfaces;
using CursusAdministratie.Data.ViewModels.Cursus;
using CursusAdministratie.Data.ViewModels.CursusInstantie;
using CursusAdministratie.UnitTests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CursusAdministratie.UnitTests
{
    [TestClass]
    public class CursusInstantieControllerTest
    {
        private Mock<ICursusInstantieService> _cursusInstantieService;
        private CursusInstantiesController _cursusInstantieController;
        private static int _mapperInitializerCount = 0;


        [TestInitialize]
        public void Initialize()
        {
            _cursusInstantieService = new Mock<ICursusInstantieService>();
            _cursusInstantieController = new CursusInstantiesController(_cursusInstantieService.Object);
            _shouldInitialize();
        }

        

        [TestMethod]
        public async Task GetAllWillReturnListOfCursusInstanties()
        {
            // ARRANGE
            _cursusInstantieService.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(new List<CursusInstantie> {
                    new CursusInstantie() {  Id = 1, Cursus = CursusBuilder.GetCursus(1, "ABC", "Test1")},
                    new CursusInstantie() {  Id = 2, Cursus = CursusBuilder.GetCursus(2, "DEF", "Test2")},
                    new CursusInstantie() {  Id = 3, Cursus = CursusBuilder.GetCursus(3, "GHI", "Test3")},

                }));

            // Act
            var result = await _cursusInstantieController.GetAllAsync();
            var objectResult = result as OkNegotiatedContentResult<List<CursusInstantieToDetailsDto>>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.IsTrue(objectResult.Content.Count == 3);
            Assert.IsNotNull(objectResult.Content.FirstOrDefault(x => x.Id == 3));
        }

        [TestMethod]
        public async Task GetAllWillReturnBadRequestWhenNull()
        {
            // ARRANGE
            _cursusInstantieService.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(It.IsAny<List<CursusInstantie>>()));

            // Act
            var result = await _cursusInstantieController.GetAllAsync();
            var objectResult = result as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(objectResult);
        }


        [TestMethod]
        public async Task GetByIdWillResultInOkResult()
        {
            // ARRANGE
            _cursusInstantieService.Setup(x => x.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new CursusInstantie { Id = 1, Cursus = CursusBuilder.GetCursus(1, "ABC", "Test1"), StartDatum = DateTime.Today, CursusId = 1}));

            // Act
            var result = await _cursusInstantieController.GetByIdAsync(It.IsAny<int>());
            var objectResult = result as OkNegotiatedContentResult<CursusInstantieToDetailsDto>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.IsTrue(objectResult.Content.Id == 1);
        }

        [TestMethod]
        public async Task GetByIdWillReturnBadRequestWhenNull()
        {
            // ARRANGE
            _cursusInstantieService.Setup(x => x.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(It.IsAny<CursusInstantie>()));

            // Act
            var result = await _cursusInstantieController.GetByIdAsync(It.IsAny<int>());
            var objectResult = result as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(objectResult);
        }

        [TestMethod]
        public async Task GetAllBCorrectyWeekShouldReturnOkResult()
        {
            // ARRANGe
            _cursusInstantieService.Setup(x => x.GetAllByWeekAndYearAsync(2020, 24));
            // ACT
            var result = await _cursusInstantieController.GetAllByWeekAsync(2020, 20);
            var objectResult = result as OkNegotiatedContentResult<List<CursusInstantieToDetailsDto>>;

            // Assert
            Assert.IsNotNull(objectResult);
        }

        [TestMethod]
        public async Task GetAllByWeekWithIncorrectWeekShouldReturnBadRequest()
        {
            // ARRANGe
            _cursusInstantieService.Setup(x => x.GetAllByWeekAndYearAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(It.IsAny<List<CursusInstantie>>()));
            // ACT
            var result = await _cursusInstantieController.GetAllByWeekAsync(2020, 20);
            var objectResult = result as OkNegotiatedContentResult<List<CursusInstantieToDetailsDto>>;

            // Assert
            Assert.IsNotNull(objectResult);

        }


        // Fix voor meerdere keren initiliazing als er meerder tests gemaakt worden
        private void _shouldInitialize()
        {
            if (_mapperInitializerCount == 0)
            {
                Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfiles>());
                _mapperInitializerCount = 1;
            }
        }



    }
}


//// ARRANGE
//_businessController.SetIdentity(_owner);

//            _businessService.Setup(x => x.GetBusinessById(_business.Id))
//                .Returns(Task.FromResult(It.IsAny<Data.Models.Business>()));

//            // Act
//            var result = await _businessController.GetMyBusiness();
//var objectResult = result as BadRequestObjectResult;

//// Assert
//Assert.IsNotNull(objectResult);
//            Assert.AreEqual(400, objectResult.StatusCode);
//            Assert.AreEqual("Business not found", objectResult.Value);