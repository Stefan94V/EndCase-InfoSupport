using AutoMapper;
using CursusAdministratie.Api.Cors;
using CursusAdministratie.Data;
using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.Services.Implementations;
using CursusAdministratie.Data.Services.Interfaces;
using CursusAdministratie.Data.ViewModels.Cursist;
using CursusAdministratie.Data.ViewModels.Cursus;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using HttpDeleteAttribute = System.Web.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace CursusAdministratie.Api.Controllers
{
    [AllowCrossSite]
    public class CursistenController : ApiController
    {
        private readonly ICursistService _cursistSerivce;
        private readonly ICursusInstantieService _cursusInstantieService;

        public CursistenController(ICursistService cursistService, ICursusInstantieService cursusInstantieService)
        {
            _cursistSerivce = cursistService;
            _cursusInstantieService = cursusInstantieService;
        }

        readonly ICursistService cursistService = new CursistService(new ApplicationDbContext());
        readonly ICursusInstantieService cursusInstantieService = new CursusInstantieService(new ApplicationDbContext());

        public CursistenController()
        {
            _cursistSerivce = cursistService;
            _cursusInstantieService = cursusInstantieService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var cursussen = await _cursistSerivce.GetAllAsync();

            if (cursussen == null)
            {
                return BadRequest("Geen cursisten gevonden");
            }

            var dto = Mapper.Map<List<CursistToDetailsDto>>(cursussen);

            return Ok(dto);
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetByIdAsync(int id)
        {
            var cursus = await _cursistSerivce.GetAsync(id);

            if (cursus == null)
            {
                return BadRequest("Geen cursist gevonden");
            }

            var dto = Mapper.Map<CursistToDetailsDto>(cursus);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateAsync([FromBody] CursistToCreateDto dto)
        {
            var cursusFromDto = Mapper.Map<Cursist>(dto);
            
            var cursusInstantie = await _cursusInstantieService.GetAsync(dto.CursusInstantieId);

            if (cursusInstantieService == null)
            {
                return BadRequest("CursusInstantie is onbekend");
            }

            var cursist = await _cursistSerivce.CreateAsync(cursusFromDto);

            if (cursist == null)
            {
                return BadRequest("Cursist aanmaken is mislukt");
            }

            var addedDB = await _cursusInstantieService.AddCursist(dto.CursusInstantieId, cursist);

            if (addedDB == null)
            {
                return BadRequest("Cursist niet aan instantie kunnen toevoegen");
            }

            var resultDto = Mapper.Map<CursistToDetailsDto>(cursist);

            return Created(resultDto.Id.ToString(), resultDto);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            var isRemoved = await _cursistSerivce.RemoveAsync(id);

            if (!isRemoved)
            {
                return BadRequest("Cursist verwijderd");
            }

            return Ok();
        }
    }
}