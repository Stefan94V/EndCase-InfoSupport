using CursusAdministratie.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursusAdministratie.Data.Services.Interfaces
{
    public interface ICursistService
    {
        Task<Cursist> GetAsync(int id);
        Task<List<Cursist>> GetAllAsync();
        Task<List<Cursist>> GetAllByCursusInstantie(int id);
        Task<Cursist> CreateAsync(Cursist cursist);
        Task<Cursist> UpdateAsync(Cursist cursist);
        Task<bool> RemoveAsync(int id);
    }
}
