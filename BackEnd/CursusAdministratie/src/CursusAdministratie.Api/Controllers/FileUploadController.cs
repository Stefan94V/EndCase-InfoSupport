using AutoMapper;
using CursusAdministratie.Api.Cors;
using CursusAdministratie.Data;
using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.Services.Implementations;
using CursusAdministratie.Data.Services.Interfaces;
using CursusAdministratie.Data.ViewModels.CursusInstantie;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            // Check for files, get only one
            var file = HttpContext.Current.Request.Files.Count > 0 ?
                   HttpContext.Current.Request.Files[0] : null;

            if (file == null)
                return Ok(new CursusInstantieUploadedResultSetDto
                {
                    Uploaded = new List<CursusInstantieToDetailsDto>(),
                    Duplicates = new List<CursusInstantieToDetailsDto>(),
                    Message = "|Geen file geselecteerd|",
                });
            // Extensions allowed to upload
            var extensies = new List<string>
            {
                ".txt"
            };

            if (!extensies.Contains(Path.GetExtension(file.FileName)))
                return Ok(new CursusInstantieUploadedResultSetDto
                {
                    Uploaded = new List<CursusInstantieToDetailsDto>(),
                    Duplicates = new List<CursusInstantieToDetailsDto>(),
                    Message = $"|{Path.GetExtension(file.FileName)} is geen geldige extensie.|",
                });

            // Filter on date
            var content = HttpContext.Current.Request;
            var start = new DateTime();
            var end = new DateTime();
            var hasDateFilter = false;

            if (content.Form.Count > 0)
            {
                var startString = content.Form.GetValues("startDatum").FirstOrDefault();
                var endString = content.Form.GetValues("eindDatum").FirstOrDefault();

                start = DateTime.Parse(startString);
                end = DateTime.Parse(endString);
                hasDateFilter = true;
            }

            if (file != null && file.ContentLength > 0)
            {
                String line;
                List<String> lines = new List<String>();
                using (var reader = new StreamReader(file.InputStream))
                {
                    while ((line = reader.ReadLine()) != null)
                        lines.Add(line);
                }

                var lineToCursusModel = _linesToCursussen(lines.ToArray());

                if (hasDateFilter)
                {
                    lineToCursusModel.Cursussen = lineToCursusModel.Cursussen
                        .Where(x => x.StartDatum.Date >= start.Date)
                        .Where(x => x.StartDatum.Date <= end.Date)
                        .ToList();
                }

                if (lineToCursusModel.Cursussen.Any())
                {
                    var result = await _cursusInstantieService.CreateRangeAsync(lineToCursusModel.Cursussen);

                    var uploadedDtos = Mapper.Map<List<CursusInstantieToDetailsDto>>(result.Uploaded);
                    var duplicateDtos = Mapper.Map<List<CursusInstantieToDetailsDto>>(result.Duplicate);

                    return Ok(new CursusInstantieUploadedResultSetDto
                    {
                        Uploaded = uploadedDtos,
                        Duplicates = duplicateDtos,
                        Message = lineToCursusModel.Message,
                    });
                }
                else
                {
                    return Ok(new CursusInstantieUploadedResultSetDto
                    {
                        Uploaded = new List<CursusInstantieToDetailsDto>(),
                        Duplicates = new List<CursusInstantieToDetailsDto>(),
                        Message = lineToCursusModel.Message,
                    });
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// 
        /// LineCount:
        /// 0:Titel
        /// 1:CursusCode
        /// 2:Duur
        /// 3:StartDatum
        /// 5:Leeg
        /// 
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private static LineToCursusModel _linesToCursussen(string[] lines)
        {
            var cursussen = new List<CursusInstantie>(); // Bevat alle gevonden cursusinstanties
            var currentCursus = new CursusInstantie(); // Nieuwe cursus instantie dat aangevuld wordt en vernieuwd na elke iteratie

            var newCursus = true; // Check of een nieuwe cursusinstantie nodig is
            var errorsMessages = ""; // String met error messages

            // Counters
            var stop = false; // Check voor en foutmelding, indien aanwezig stopt het met verder zoeken
            var lineCount = 0; // Check in welke volgorde van het format het momenteel is
            var rijnummer = 1; // Check in welke rij de counter is van het document zelf

            foreach (var line in lines)
            {

                var words = line.Split(':');
                // Words moeten deelbaar zijn, geen foutmelding bevatten en lineCount mag niet 4 zijn (dat is een lege rij)
                if (words.Length == 2 && stop == false && lineCount != 4)
                {
                    // Check voor de Titel
                    if (words[0].Equals("Titel"))
                    {
                        if (lineCount == 0)
                        {
                            currentCursus = new CursusInstantie();
                            currentCursus.Cursus = new Cursus
                            {
                                Titel = words[1]
                            };
                            newCursus = false;
                            lineCount++;
                        }
                        else
                        {
                            stop = true;
                            errorsMessages = $"|Titel staat op de veerkerde volgorde op rij: {rijnummer}|";
                        }

                    }
                    // Check voor de Cursuscode
                    else if (words[0].Equals("Cursuscode") && newCursus == false)
                    {
                        if (lineCount == 1)
                        {
                            currentCursus.Cursus.Code = words[1];
                            lineCount++;
                        }
                        else
                        {
                            stop = true;
                            errorsMessages = $"|CursusCode staat op de veerkerde volgorde op rij: {rijnummer}|";
                        }

                    }
                    // Check voor de Duur
                    else if (words[0].Equals("Duur") && newCursus == false)
                    {
                        // Waarde moet in het juiste formaat zijn {nummer: Dagen}
                        if (words[1].Contains("dagen"))
                        {
                            var duurSplit = words[1].Split(' ');
                            int duur;
                            if (duurSplit.Length > 0)
                            {
                                int.TryParse(duurSplit[1], out duur);
                                if (duur != 0)
                                {
                                    if (lineCount == 2)
                                    {
                                        currentCursus.Cursus.Duur = duur;
                                        lineCount++;
                                    }
                                    else
                                    {
                                        stop = true;
                                        errorsMessages = $"|Duur staat op de verkeerde volgorde op rij: {rijnummer}|";
                                    }

                                }
                            }
                        }
                        else
                        {
                            stop = true;
                            errorsMessages = $"|Duur bevat geen dagen op rij waarde op rij: {rijnummer}|";
                        }
                    }
                    // Check voor de startdatum
                    else if (words[0].Equals("Startdatum") && newCursus == false)
                    {
                        newCursus = true;
                        if (!string.IsNullOrWhiteSpace(currentCursus.Cursus.Code) && !string.IsNullOrWhiteSpace(currentCursus.Cursus.Titel) && !string.IsNullOrWhiteSpace(currentCursus.Cursus.Titel))
                        {
                            //var selDate = Convert.ToDateTime(words[1]);

                            var dateSplit = words[1].Split('/');
                            int month;
                            int day;
                            int year;

                            // Check of het in de juiste volgorde staan dd/MM/yyyy
                            if (dateSplit.Length != 1)
                            {
                                int.TryParse(dateSplit[0], out day);
                                int.TryParse(dateSplit[1], out month);
                                int.TryParse(dateSplit[2], out year);

                                // Check of de dagen op juiste volgorde staan en/of de datums correct zijn
                                if ((month != 0 && month < 13) && (day != 0 && day < 32) && year != 0)
                                {
                                    if (lineCount == 3)
                                    {
                                        currentCursus.StartDatum = new DateTime(year: year, month: month, day: day);
                                        cursussen.Add(currentCursus);
                                        lineCount++;
                                    }
                                    else
                                    {
                                        stop = true;
                                        errorsMessages = $"|Strartdatum staat op de verkeerde volgorde op rij: {rijnummer}|";
                                    }

                                }
                                else
                                {
                                    errorsMessages += $"|Incorrecte datum input formaat| op rij: {rijnummer}|";
                                    stop = true;
                                }
                            }
                            else
                            {
                                errorsMessages += $"|Startdatum is niet in het dd/MM/YYYY formaat| op rij: {rijnummer}|";
                                stop = true;
                            }
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(currentCursus.Cursus.Code))
                            {
                                errorsMessages += $" |Cursus Code ontbreekt| op rij: {rijnummer}|";
                                stop = true;
                            }
                            else if (string.IsNullOrWhiteSpace(currentCursus.Cursus.Titel))
                            {
                                errorsMessages += $" |Cursus Titel ontbreekt| op rij: {rijnummer}|";
                                stop = true;
                            }
                            else if (string.IsNullOrWhiteSpace(currentCursus.Cursus.Duur.ToString()))
                            {
                                errorsMessages += $" |Cursus Duur ontbreekt| op rij: {rijnummer}|";
                                stop = true;
                            }

                            stop = true;
                        }
                    }
                }
                // Check voor de white space
                else if (lineCount == 4 && stop == false)
                {
                    if (words.Length == 1) // Lege veld is aanwezig
                    {
                        lineCount = 0;
                    }
                    else // Geen lege veld aanwezig
                    {
                        stop = true;
                        errorsMessages = $"|Lege rij ontbreekt op rij: {rijnummer}|";
                    }
                }
                else
                {
                    // Gaat niet meer verder met data ophalen
                    if (stop == true)
                        return new LineToCursusModel
                        {
                            Message = errorsMessages,
                            Cursussen = new List<CursusInstantie>()
                        };
                }
                rijnummer++;
            }
            return new LineToCursusModel
            {
                Cursussen = cursussen,
                Message = errorsMessages
            };

        }

        private class LineToCursusModel
        {
            public string Message { get; set; }
            public List<CursusInstantie> Cursussen { get; set; }
        }



    }
}