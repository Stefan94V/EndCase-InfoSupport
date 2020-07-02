using AutoMapper;
using CursusAdministratie.Api.Cors;
using CursusAdministratie.Data;
using CursusAdministratie.Data.Services.Implementations;
using CursusAdministratie.Data.Services.Interfaces;
using CursusAdministratie.Data.ViewModels.CursusInstantie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CursusAdministratie.Api.Controllers
{

    [AllowCrossSite]
    public class CursusInstantiesController : ApiController
    {
        private readonly ICursusInstantieService _cursusService;

        public CursusInstantiesController(ICursusInstantieService _cursusService)
        {
            this._cursusService = _cursusService;
        }

        readonly ICursusInstantieService cursusService = new CursusInstantieService(new ApplicationDbContext());

        public CursusInstantiesController()
        {
            _cursusService = cursusService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var cursussen = await _cursusService.GetAllAsync();

            if (cursussen == null)
            {
                return BadRequest("Geen cursussen gevonden");
            }

            var dto = Mapper.Map<List<CursusInstantieToDetailsDto>>(cursussen);

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllByWeekAsync(int year, int week)
        {
            var cursussen = await _cursusService.GetAllByWeekAndYearAsync(year, week);

            if (cursussen == null)
            {
                return BadRequest("Geen cursussen gevonden");
            }

            var dto = Mapper.Map<List<CursusInstantieToDetailsDto>>(cursussen);

            return Ok(dto);
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetByIdAsync(int id)
        {
            var cursus = await _cursusService.GetAsync(id);

            if (cursus == null)
            {
                return BadRequest("Geen cursus gevonden");
            }

            var dto = Mapper.Map<CursusInstantieToDetailsDto>(cursus);

            return Ok(dto);
        }

    }

}