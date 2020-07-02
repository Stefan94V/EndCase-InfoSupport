using AutoMapper;
using CursusAdministratie.Api;
using CursusAdministratie.Api.Controllers;
using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.Services.Interfaces;
using CursusAdministratie.Data.ViewModels.Cursist;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace CursusAdministratie.UnitTests
{
    [TestClass]
    public class CursistenControllerTest
    {
        private Mock<ICursistService> _cursistService;
        private Mock<ICursusInstantieService> _cursistInstantieService;
        private CursistenController _cursistenController;
        private static int _mapperInitializerCount = 0;

        [TestInitialize]
        public void Initialize()
        {
            _cursistService = new Mock<ICursistService>();
            _cursistInstantieService = new Mock<ICursusInstantieService>();
            _cursistenController = new CursistenController(_cursistService.Object, _cursistInstantieService.Object);
            _shouldInitialize();
        }

        [TestMethod]
        public async Task GetAllWillReturnListOfCurssusen()
        {
            // ARRANGE
            _cursistService.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(new List<Cursist> {
                    new Cursist{Id = 1, Naam ="Test1", Achternaam = "Test1"},
                    new Cursist{Id = 2, Naam ="Test2", Achternaam = "Test2"},
                    new Cursist{Id = 3, Naam ="Test3", Achternaam = "Test3"},

                }));

            // Act
            var result = await _cursistenController.GetAllAsync();
            var objectResult = result as OkNegotiatedContentResult<List<CursistToDetailsDto>>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.IsTrue(objectResult.Content.Count == 3);
            Assert.IsNotNull(objectResult.Content.FirstOrDefault(x => x.Id == 3));
        }

        [TestMethod]
        public async Task GetAllWillReturnBadRequestWhenNull()
        {
            // ARRANGE
            _cursistService.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(It.IsAny<List<Cursist>>()));

            // Act
            var result = await _cursistenController.GetAllAsync();
            var objectResult = result as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(objectResult);
        }


        [TestMethod]
        public async Task GetByIdWillResultInOkResult()
        {
            // ARRANGE
            _cursistService.Setup(x => x.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new Cursist { Id = 1, Naam = "Test1", Achternaam = "Test1" }));

            // Act
            var result = await _cursistenController.GetByIdAsync(It.IsAny<int>());
            var objectResult = result as OkNegotiatedContentResult<CursistToDetailsDto>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.IsTrue(objectResult.Content.Id == 1);
        }

        [TestMethod]
        public async Task GetByIdWillReturnBadRequestWhenNull()
        {
            // ARRANGE
            _cursistService.Setup(x => x.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(It.IsAny<Cursist>()));

            // Act
            var result = await _cursistenController.GetByIdAsync(It.IsAny<int>());
            var objectResult = result as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(objectResult);
        }


        [TestMethod]
        public async Task CreateWithValidReturnsCreatedResult()
        {
            // ARRANGE
            var cInstantie = new CursusInstantie { Id = 1, Cursus = new Cursus { }, CursusId = 1, StartDatum = DateTime.Now };
            var cursist = new Cursist { Id = 1, Naam = "Test1", Achternaam = "Test1", Cursussen = new List<CursusInstantie>() { cInstantie } };
            _cursistService.Setup(x => x.CreateAsync(It.IsAny<Cursist>()))
                .Returns(Task.FromResult(cursist));
            _cursistInstantieService.Setup(x => x.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(cInstantie));
            _cursistInstantieService.Setup(x => x.AddCursist(It.IsAny<int>(), It.IsAny<Cursist>()))
                .Returns(Task.FromResult(cInstantie));

            var inputDto = new CursistToCreateDto { CursusInstantieId = 1, Achternaam = "Test1", Naam = "Test1" };
            // Act
            var result = await _cursistenController.CreateAsync(inputDto);
            var objectResult = result as CreatedNegotiatedContentResult<CursistToDetailsDto>;

            // Assert
            Assert.IsNotNull(objectResult);
        }

        [TestMethod]
        public async Task CreateWithInValidCursistInstantieIdReturnsBadRequestResult()
        {
            // ARRANGE
            var cursusToCreate = new Cursist { Naam = "Test1", Achternaam = "Test1", Cursussen = { It.IsAny<CursusInstantie>() } };
            _cursistService.Setup(x => x.CreateAsync(It.IsAny<Cursist>()))
                .Returns(Task.FromResult(cursusToCreate));
            _cursistInstantieService.Setup(x => x.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(It.IsAny<CursusInstantie>()));

            var inputDto = new CursistToCreateDto { CursusInstantieId = 1, Achternaam = "Test1", Naam = "Test1" };
            // Act
            var result = await _cursistenController.CreateAsync(It.IsAny<CursistToCreateDto>());
            var objectResult = result as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(objectResult);
        }



        [TestMethod]
        public async Task DeleteWithValidReturnsCreatedResult()
        {
            // ARRANGE
            _cursistService.Setup(x => x.RemoveAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(true));

            // Act
            var result = await _cursistenController.DeleteAsync(It.IsAny<int>());
            var objectResult = result as OkResult;

            // Assert
            Assert.IsNotNull(objectResult);

        }

        [TestMethod]
        public async Task DeleteWithInValidReturnsBadRequestResult()
        {
            // ARRANGE
            _cursistService.Setup(x => x.RemoveAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(false));

            // Act
            var result = await _cursistenController.DeleteAsync(It.IsAny<int>());
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
