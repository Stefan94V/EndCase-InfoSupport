using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.ViewModels.CursusInstantie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie.Data.Services.Interfaces
{
    public interface ICursusInstantieService
    {
        Task<CursusInstantie> GetAsync(int id);
        Task<List<CursusInstantie>> GetAllAsync();
        Task<CursusInstantieUploadResultSet> CreateRangeAsync(List<CursusInstantie> cursusInstanties);
    }
}
