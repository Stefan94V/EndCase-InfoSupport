using CursusAdministratie.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie.Data.Services.Interfaces
{
    public interface ICursistService
    {
        Task<Cursist> GetAsync(int id);
        Task<List<Cursist>> GetAllAsync();
        Task<Cursist> CreateAsync(Cursist cursist);
        Task<Cursist> UpdateAsync(Cursist cursist);
        Task<bool> RemoveAsync(int id);
    }
}
