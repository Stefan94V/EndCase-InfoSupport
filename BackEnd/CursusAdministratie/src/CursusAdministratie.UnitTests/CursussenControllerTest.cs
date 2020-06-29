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
using CursusAdministratie.UnitTests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CursusAdministratie.UnitTests
{
    [TestClass]
    public class CursussenControllerTest
    {
        private Mock<ICursusService> _cursusService;
        private CursussenController _cursussenController;
        private static int _mapperInitializerCount = 0;


        [TestInitialize]
        public void Initialize()
        {
            _cursusService = new Mock<ICursusService>();
            _cursussenController = new CursussenController(_cursusService.Object);
            _shouldInitialize();
        }

        

        [TestMethod]
        public async Task GetAllWillReturnListOfCurssusen()
        {
            // ARRANGE
            _cursusService.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(new List<Cursus> {
                    new Cursus(){ Id= 1, Code = "AAN", Titel = "TEST"},
                    new Cursus(){ Id= 2, Code = "AAB", Titel = "TRST"},
                    new Cursus(){ Id= 3, Code = "CAN", Titel = "TECT"},

                }));

            // Act
            var result = await _cursussenController.GetAllAsync();
            var objectResult = result as OkNegotiatedContentResult<List<CursusToDetailsDto>>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.IsTrue(objectResult.Content.Count == 3);
            Assert.IsNotNull(objectResult.Content.FirstOrDefault(x => x.Id == 3));
        }

        [TestMethod]
        public async Task GetAllWillReturnBadRequestWhenNull()
        {
            //Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfiles>());
            // ARRANGE
            _cursusService.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(It.IsAny<List<Cursus>>()));

            // Act
            var result = await _cursussenController.GetAllAsync();
            var objectResult = result as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(objectResult);
        }


        [TestMethod]
        public async Task GetByIdWillResultInOkResult()
        {
            // ARRANGE
            _cursusService.Setup(x => x.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(CursusBuilder.GetCursus(1, "TTT", "Test")));

            // Act
            var result = await _cursussenController.GetByIdAsync(It.IsAny<int>());
            var objectResult = result as OkNegotiatedContentResult<CursusToDetailsDto>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.IsTrue(objectResult.Content.Id == 1);
        }

        [TestMethod]
        public async Task GetByIdWillReturnBadRequestWhenNull()
        {
            // ARRANGE
            _cursusService.Setup(x => x.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(It.IsAny<Cursus>()));

            // Act
            var result = await _cursussenController.GetByIdAsync(It.IsAny<int>());
            var objectResult = result as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(objectResult);
        }


        [TestMethod]
        public async Task CreateWithValidReturnsCreatedResult()
        {
            // ARRANGE
            var cursusToCreate = CursusBuilder.GetCursus(1, "test", "testtitel");
            _cursusService.Setup(x => x.CreateAsync(It.IsAny<Cursus>()))
                .Returns(Task.FromResult(cursusToCreate));
            var inputDto = new CursusToCreateDto { Titel = cursusToCreate.Titel, Code = cursusToCreate.Code };
            // Act
            var result = await _cursussenController.CreateAsync(inputDto);
            var objectResult = result as CreatedNegotiatedContentResult<CursusToDetailsDto>;

            // Assert
            Assert.IsNotNull(objectResult);
        }

        [TestMethod]
        public async Task CreateWithInValidReturnsBadRequestResult()
        {
            // ARRANGE
            _cursusService.Setup(x => x.CreateAsync(It.IsAny<Cursus>()))
                .Returns(Task.FromResult(It.IsAny<Cursus>()));
            // Act
            var result = await _cursussenController.CreateAsync(It.IsAny<CursusToCreateDto>());
            var objectResult = result as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(objectResult);
        }

        [TestMethod]
        public async Task UpdateWithValidReturnsCreatedResult()
        {
            // ARRANGE
            var newTitle = "new title";
            var cursusToUpdate = CursusBuilder.GetCursus(1, "test", newTitle);
            var inputDto = new CursusToUpdateDto { Id = 1, Titel = newTitle, Code = cursusToUpdate.Code };

            _cursusService.Setup(x => x.UpdateAsync(It.IsAny<Cursus>()))
                .Returns(Task.FromResult(cursusToUpdate));

            // Act
            var result = await _cursussenController.UpdateAsync(inputDto);
            var objectResult = result as OkNegotiatedContentResult<CursusToDetailsDto>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.IsTrue(objectResult.Content.Titel.Equals(newTitle));

        }

        [TestMethod]
        public async Task UpdateWithInValidReturnsBadRequestResult()
        {
            // ARRANGE
            _cursusService.Setup(x => x.UpdateAsync(It.IsAny<Cursus>()))
                .Returns(Task.FromResult(It.IsAny<Cursus>()));
            // Act
            var result = await _cursussenController.UpdateAsync(It.IsAny<CursusToUpdateDto>());
            var objectResult = result as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(objectResult);
        }

        [TestMethod]
        public async Task DeleteWithValidReturnsCreatedResult()
        {
            // ARRANGE
            _cursusService.Setup(x => x.RemoveAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(true));

            // Act
            var result = await _cursussenController.DeleteAsync(It.IsAny<int>());
            var objectResult = result as OkResult;

            // Assert
            Assert.IsNotNull(objectResult);

        }

        [TestMethod]
        public async Task DeleteWithInValidReturnsBadRequestResult()
        {
            // ARRANGE
            _cursusService.Setup(x => x.RemoveAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(false));

            // Act
            var result = await _cursussenController.DeleteAsync(It.IsAny<int>());
            var objectResult = result as BadRequestErrorMessageResult;


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