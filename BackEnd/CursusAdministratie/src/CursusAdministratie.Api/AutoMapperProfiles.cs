using AutoMapper;
using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.ViewModels.Cursus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CursusAdministratie.Api
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CursusToDetailsDto, Cursus>();
        }
    }
}