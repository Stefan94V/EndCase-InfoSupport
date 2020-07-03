using AutoMapper;
using CursusAdministratie.Api.Cors;
using CursusAdministratie.Data;
using CursusAdministratie.Data.Services.Implementations;
using CursusAdministratie.Data.Services.Interfaces;
using CursusAdministratie.Data.ViewModels.CursusInstantie;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace CursusAdministratie.Api.Controllers
{

    [AllowCrossSite]
    public class CursusInstantiesController : ApiController
    {
        private readonly ICursusInstantieService _cursusInstantieService;

        public CursusInstantiesController(ICursusInstantieService cursusInstantieService)
        {
            _cursusInstantieService = cursusInstantieService;
        }

        readonly ICursusInstantieService cursusService = new CursusInstantieService(new ApplicationDbContext());

        public CursusInstantiesController()
        {
            _cursusInstantieService = cursusService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var cursussen = await _cursusInstantieService.GetAllAsync();

            if (cursussen == null)
                return BadRequest("Geen cursussen gevonden");

            var dto = Mapper.Map<List<CursusInstantieToDetailsDto>>(cursussen);

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllByWeekAsync(int year, int week)
        {
            var cursussen = await _cursusInstantieService.GetAllByWeekAndYearAsync(year, week);

            if (cursussen == null)
                return BadRequest("Geen cursussen gevonden");

            var dto = Mapper.Map<List<CursusInstantieToDetailsDto>>(cursussen);

            return Ok(dto);
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetByIdAsync(int id)
        {
            var cursus = await _cursusInstantieService.GetAsync(id);

            if (cursus == null)
                return BadRequest("Geen cursus gevonden");

            var dto = Mapper.Map<CursusInstantieToDetailsDto>(cursus);

            return Ok(dto);
        }

    }

}