using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.Services.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CursusAdministratie.Data.Services.Implementations
{
    public class CursusService : ICursusService
    {
        private readonly ApplicationDbContext _context;

        public CursusService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Cursus> CreateAsync(Cursus cursus)
        {
            _context.Cursussen.Add(cursus);

            await _context.SaveChangesAsync();

            return cursus;
        }

        public async Task<List<Cursus>> GetAllAsync()
        {
            return await _context.Cursussen
                .ToListAsync();
        }

        public async Task<Cursus> GetAsync(int id)
        {
            return await _context.Cursussen
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var cursusDB = await _context.Cursussen
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cursusDB == null)
            {
                return false;
            }

            _context.Cursussen.Remove(cursusDB);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Cursus> UpdateAsync(Cursus cursus)
        {
            var cursusDB = await _context.Cursussen
                .FirstOrDefaultAsync(x => x.Id == cursus.Id);

            if (cursusDB == null)
            {
                return null;
            }

            cursusDB.Code = cursus.Code;
            cursusDB.Titel = cursus.Titel;

            await _context.SaveChangesAsync();

            return cursusDB;
        }
    }
}
