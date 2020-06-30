using AutoMapper;
using CursusAdministratie.Api.Cors;
using CursusAdministratie.Data;
using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.Services.Implementations;
using CursusAdministratie.Data.Services.Interfaces;
using CursusAdministratie.Data.ViewModels.Cursus;
using CursusAdministratie.Data.ViewModels.CursusInstantie;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CursusAdministratie.Api.Controllers
{
    [AllowCrossSite]
    public class FileUploadController : ApiController
    {
        private readonly ICursusInstantieService _cursusInstantieService;

        public FileUploadController(ICursusInstantieService cursusInstantieService)
        {
            _cursusInstantieService = cursusInstantieService;
        }

        readonly ICursusInstantieService cursusInstantieService = new CursusInstantieService(new ApplicationDbContext());

        public FileUploadController()
        {
            _cursusInstantieService = cursusInstantieService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> UploadFile()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ?
                   HttpContext.Current.Request.Files[0] : null;

            if (file != null && file.ContentLength > 0)
            {
                String line;
                List<String> lines = new List<String>();
                using (var reader = new StreamReader(file.InputStream))
                {
                    while ((line = reader.ReadLine()) != null)
                        lines.Add(line);
                }

                var cursussen = _linesToCursussen(lines.ToArray());

                if (cursussen.Any())
                {
                    var result = await _cursusInstantieService.CreateRangeAsync(cursussen);

                    var uploadedDtos = Mapper.Map<List<CursusInstantieToDetailsDto>>(result.Uploaded);
                    var duplicateDtos = Mapper.Map<List<CursusInstantieToDetailsDto>>(result.Duplicate);

                    return Ok(new CursusInstantieUploadedResultSetDto
                    {
                        Uploaded = uploadedDtos,
                        Duplicates = duplicateDtos
                    });
                }
            }
            return BadRequest();
        }

        private static List<CursusInstantie> _linesToCursussen(string[] lines)
        {
            var cursussen = new List<CursusInstantie>();
            var currentCursus = new CursusInstantie();
            var newCursus = true;


            foreach (var line in lines)
            {
                var words = line.Split(':');
                if(words.Length == 2)
                {
                    if (words[0].Equals("Titel"))
                    {
                        currentCursus = new CursusInstantie();
                        currentCursus.Cursus = new Cursus
                        {
                            Titel = words[1]
                        };
                        newCursus = false;
                    }else if(words[0].Equals("Cursuscode") && newCursus == false)
                    {
                        currentCursus.Cursus.Code = words[1];
                    }
                    else if (words[0].Equals("Duur") && newCursus == false)
                    {
                        var duurSplit = words[1].Split(' ');
                        int duur;
                        if(duurSplit.Length > 0)
                        {
                            int.TryParse(duurSplit[1], out duur);
                            if(duur != 0)
                            {
                                currentCursus.Cursus.Duur = duur;
                            }
                        }
                        
                    }
                    else if (words[0].Equals("Startdatum") && newCursus == false)
                    {
                        newCursus = true;
                        if(!string.IsNullOrWhiteSpace(currentCursus.Cursus.Code) && !string.IsNullOrWhiteSpace(currentCursus.Cursus.Titel))
                        {
                            //var selDate = Convert.ToDateTime(words[1]);

                            var dateSplit = words[1].Split('/');
                            int month;
                            int day;
                            int year;

                            int.TryParse(dateSplit[0], out day);
                            int.TryParse(dateSplit[1], out month);
                            int.TryParse(dateSplit[2], out year);


                            if ((month != 0 && month < 13) && (day !=0 && day < 32) && year != 0)
                            {
                                currentCursus.StartDatum = new DateTime(year: year, month: month, day: day);
                                cursussen.Add(currentCursus);
                            }
                        }
                    }
                }
            }
            return cursussen;
           
        }



    }
}