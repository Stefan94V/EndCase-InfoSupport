using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using AutoMapper;
using CursusAdministratie.Api;
using CursusAdministratie.Api.Controllers;
using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.Services.Interfaces;
using CursusAdministratie.Data.ViewModels.Cursus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CursusAdministratie.UnitTests
{
    [TestClass]
    public class CursussenControllerTest
    {
        private Mock<ICursusService> _cursusService;
        private CursussenController _cursussenController;
        private Mapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _cursusService = new Mock<ICursusService>();
            _cursussenController = new CursussenController(_cursusService.Object);
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfiles>());

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
            var result = await _cursussenController.GetAll();
            var objectResult = result as OkNegotiatedContentResult<List<CursusToDetailsDto>>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.IsTrue(objectResult.Content.Count == 3);
            Assert.IsNotNull(objectResult.Content.FirstOrDefault(x => x.Id == 3));
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