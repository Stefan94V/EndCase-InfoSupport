using CursusAdministratie.Api.Controllers;
using CursusAdministratie.Data.Services.Interfaces;
using CursusAdministratie.Data.ViewModels.CursusInstantie;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace CursusAdministratie.UnitTests
{
    [TestClass]
    public class FileUploadUnitTests
    {
        private Mock<ICursusInstantieService> _cursusInstantieService;
        private FileUploadController _fileUploadController;
        private static int _mapperInitializerCount = 0;


        [TestInitialize]
        public void Initialize()
        {
            _cursusInstantieService = new Mock<ICursusInstantieService>();
            _fileUploadController = new FileUploadController(_cursusInstantieService.Object);
        }

        
        // TODO: HttpFormFileUpload mocken in de Controller Request
        //[TestMethod]
        //public async Task UploadWithCorrectFileFormatShouldReturnListOfCursussen()
        //{
        //    // ARRANGE
        //    var file = File.Open("../../TestFiles/goedvoorbeeld.txt");
        //    var lines = _fileUploadController.FileToLines(file.st)
        //    _fileUploadController.ControllerContext = ctrContext;

        //    // Act
        //    var result = await _fileUploadController._linesToCursussen();
        //    var objectResult = result as OkNegotiatedContentResult<CursusInstantieUploadedResultSetDto>;

        //    // Assert
        //    Assert.IsNotNull(objectResult);
        //}





    }

}
