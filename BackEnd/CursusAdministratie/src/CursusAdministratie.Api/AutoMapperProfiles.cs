using AutoMapper;
using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.ViewModels.Cursus;
using CursusAdministratie.Data.ViewModels.CursusInstantie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace CursusAdministratie.Api
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CursusToDetailsDto, Cursus>();
                //.ForMember(res => res.CursusInstanties.Select(x => x.StartDatum), mem => mem.MapFrom(x => x.StartDatums));
            CreateMap<CursusToCreateDto, Cursus>();
            CreateMap<CursusToUpdateDto, Cursus>();

            //CreateMap<CursusInstantieToDetailsDto, CursusInstantie>()
            //    .ForPath(x => x.Cursisten.Count, dest => dest.MapFrom(y => y.AantalCursisten))
            //    .ForMember(x => x.Cursisten, dest => dest.Ignore())
            //    .ForMember(x => x.Cursus, dest => dest.Ignore())
            //    .ForPath(x => x.Cursus.Code, dest => dest.MapFrom(y => y.Code))
            //    .ForPath(x => x.Cursus.Titel, dest => dest.MapFrom(y => y.Titel))
            //    .ForPath(x => x.Cursus.Duur, dest => dest.MapFrom(y => y.Duur));

            CreateMap<CursusInstantie, CursusInstantieToDetailsDto>()
                .ForPath(x => x.AantalCursisten, dest => dest.MapFrom(y => y.Cursisten.Count()))
                .ForPath(x => x.Code, dest => dest.MapFrom(y => y.Cursus.Code))
                .ForPath(x => x.Titel, dest => dest.MapFrom(y => y.Cursus.Titel))
                .ForPath(x => x.Duur, dest => dest.MapFrom(y => y.Cursus.Duur));
        }
    }
}