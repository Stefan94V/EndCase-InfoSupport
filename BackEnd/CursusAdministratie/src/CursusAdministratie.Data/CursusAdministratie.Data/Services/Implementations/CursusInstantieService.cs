using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.Services.Interfaces;
using CursusAdministratie.Data.ViewModels.CursusInstantie;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CursusAdministratie.Data.Services.Implementations
{
    public class CursusInstantieService : ICursusInstantieService
    {
        private readonly ApplicationDbContext _context;

        public CursusInstantieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CursusInstantieUploadResultSet> CreateRangeAsync(List<CursusInstantie> cursusInstanties)
        {
            var duplicates = new List<CursusInstantie>();
            var toReturn = new List<CursusInstantie>();
            foreach (var ci in cursusInstanties)
            {
                var isDupAsWhole = await _context.CursusInstanties.AnyAsync(x => x.StartDatum == ci.StartDatum && x.Cursus.Code == ci.Cursus.Code);
                if (isDupAsWhole)
                {
                    duplicates.Add(ci);
                }
                else
                {
                    var isDup = await _context.Cursussen.AnyAsync(x => x.Code == ci.Cursus.Code);
                    if (isDup && toReturn.Any(c => c.Cursus.Code == ci.Cursus.Code))
                    {
                        var existingCursus = await _context.Cursussen
                            .Include(x => x.CursusInstanties)
                            .FirstOrDefaultAsync(x => x.Code == ci.Cursus.Code);
                        var instantie = new CursusInstantie
                        {
                            Cursus = existingCursus,
                            CursusId = existingCursus.Id,
                            StartDatum = ci.StartDatum,
                        };

                        existingCursus.CursusInstanties.Add(instantie);
                        toReturn.Add(instantie);
                    }
                    else
                    {
                        var dubCursus = toReturn.FirstOrDefault(x => x.Cursus.Code == ci.Cursus.Code) != null
                                ? toReturn.FirstOrDefault(x => x.Cursus.Code == ci.Cursus.Code).Cursus
                                : ci.Cursus;
                        ci.Cursus = dubCursus;
                        _context.CursusInstanties.Add(ci);
                        toReturn.Add(ci);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return new CursusInstantieUploadResultSet
            {
                Uploaded = toReturn,
                Duplicate = duplicates
            };
        }

        public async Task<List<CursusInstantie>> GetAllAsync()
        {
            return await _context.CursusInstanties
                .OrderBy(x => x.StartDatum)
                .Include(x => x.Cursisten)
                .ToListAsync();
        }

        public async Task<CursusInstantie> GetAsync(int id)
        {
            return await _context.CursusInstanties
               .Include(x => x.Cursisten)
               .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
