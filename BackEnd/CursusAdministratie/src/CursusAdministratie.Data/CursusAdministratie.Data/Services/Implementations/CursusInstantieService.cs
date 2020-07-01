using CursusAdministratie.Data.Models;
using CursusAdministratie.Data.Services.Interfaces;
using CursusAdministratie.Data.ViewModels.CursusInstantie;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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
                .OrderByDescending(x => x.StartDatum)
                .Include(x => x.Cursisten)
                .ToListAsync();
        }

        public async Task<List<CursusInstantie>> GetAllByWeekAndYearAsync(int year, int week)
        {
            // Get week of the requested date
            var weekOfYearAsDate = _firstDateOfWeekISO8601(year, week);

            // Get data
            var cursussen = await _context.CursusInstanties
                .ToListAsync();
            
            // Filter if both sundays match (equals same week)
            return cursussen
                .Where(x => StartOfWeek(x.StartDatum, DayOfWeek.Sunday) == StartOfWeek(weekOfYearAsDate, DayOfWeek.Sunday))
                .ToList();
        }

        public async Task<CursusInstantie> GetAsync(int id)
        {
            return await _context.CursusInstanties
               .Include(x => x.Cursisten)
               .FirstOrDefaultAsync(x => x.Id == id);
        }

      
        private DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }


        private DateTime _firstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }
    }
}
