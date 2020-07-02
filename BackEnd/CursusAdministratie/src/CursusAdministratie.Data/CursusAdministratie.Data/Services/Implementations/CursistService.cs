using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie.Data.Services.Implementations
{
    public class CursistService : ICursistService
    {
        private readonly ApplicationDbContext _context;

        public CursistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cursist> CreateAsync(Cursist cursist)
        {

            _context.Cursisten.Add(cursist);

            await _context.SaveChangesAsync();

            return cursist;
        }

        public async Task<List<Cursist>> GetAllAsync()
        {
            return await _context.Cursisten
                .ToListAsync();
        }

        public async Task<List<Cursist>> GetAllByCursusInstantie(int id)
        {
             var ci = await _context.CursusInstanties
                .Include(x => x.Cursisten)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (ci == null)
                return null;

            return ci.Cursisten.ToList();
        }

        public async Task<Cursist> GetAsync(int id)
        {
            return await _context.Cursisten
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var cursistDB = await _context.Cursisten
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cursistDB == null)
            {
                return false;
            }

            _context.Cursisten.Remove(cursistDB);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Cursist> UpdateAsync(Cursist cursist)
        {
            var cursistDB = await _context.Cursisten
                .FirstOrDefaultAsync(x => x.Id == cursist.Id);

            if (cursistDB == null)
            {
                return null;
            }

            cursistDB.Achternaam = cursist.Achternaam;

            await _context.SaveChangesAsync();

            return cursistDB;
        }
    }
}
