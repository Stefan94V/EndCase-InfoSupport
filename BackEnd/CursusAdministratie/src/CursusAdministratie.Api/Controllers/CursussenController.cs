using AutoMapper;
using CursusAdministratie.Data;
using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.Services.Implementations;
using CursusAdministratie.Data.Services.Interfaces;
using CursusAdministratie.Data.ViewModels.Cursus;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace CursusAdministratie.Api.Controllers
{
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
        public async Task<IHttpActionResult> GetAll()
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
        public async Task<IHttpActionResult> GetById(int id)
        {
            var cursus = await _cursusService.GetAsync(id);

            if (cursus == null)
            {
                return BadRequest("Geen cursus gevonden");
            }

            var dto = Mapper.Map<CursusToDetailsDto>(cursus);

            return Ok(dto);
        }
    }
}
