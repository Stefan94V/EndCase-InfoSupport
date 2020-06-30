using AutoMapper;
using CursusAdministratie.Api.Cors;
using CursusAdministratie.Data;
using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.Services.Implementations;
using CursusAdministratie.Data.Services.Interfaces;
using CursusAdministratie.Data.ViewModels.Cursus;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using ActionNameAttribute = System.Web.Http.ActionNameAttribute;
using HttpDeleteAttribute = System.Web.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using HttpPutAttribute = System.Web.Mvc.HttpPutAttribute;
using RouteAttribute = System.Web.Mvc.RouteAttribute;

namespace CursusAdministratie.Api.Controllers
{
    [AllowCrossSite]
    public class CursussenController : ApiController
    {
        private readonly ICursusService _cursusService;
        private Mapper _mapper;

        public CursussenController(ICursusService _cursusService)
        {
            this._cursusService = _cursusService;
        }

        readonly ICursusService cursusService = new CursusService(new ApplicationDbContext());

        public CursussenController()
        {
            _cursusService = cursusService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var cursussen = await _cursusService.GetAllAsync();

            if(cursussen == null)
            {
                return BadRequest("Geen cursussen gevonden");
            }

            var dto = Mapper.Map<List<CursusToDetailsDto>>(cursussen);
            
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

            var dto = Mapper.Map<CursusToDetailsDto>(cursus);

            return Ok(dto);
        }

       
        public async Task<IHttpActionResult> CreateAsync([FromBody] CursusToCreateDto dto)
        {
            var cursusFromDto = Mapper.Map<Cursus>(dto);

            var cursus = await _cursusService.CreateAsync(cursusFromDto);

            if (cursus == null)
            {
                return BadRequest("Updaten is mislukt");
            }

            var resultDto = Mapper.Map<CursusToDetailsDto>(cursus);

            return Created(resultDto.Id.ToString(), resultDto);
        }

        
       


        [HttpPut]
        public async Task<IHttpActionResult> UpdateAsync([FromBody] CursusToUpdateDto dto)
        {
            var cursusFromDto = Mapper.Map<Cursus>(dto);

            var cursus = await _cursusService.UpdateAsync(cursusFromDto);

            if (cursus == null)
            {
                return BadRequest("Geen cursus gevonden");
            }


            var resultDto = Mapper.Map<CursusToDetailsDto>(cursus);

            return Ok(resultDto);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            var isRemoved = await _cursusService.RemoveAsync(id);

            if (!isRemoved)
            {
                return BadRequest("Cursus verwijderd");
            }

            return Ok();
        }
    }
}
