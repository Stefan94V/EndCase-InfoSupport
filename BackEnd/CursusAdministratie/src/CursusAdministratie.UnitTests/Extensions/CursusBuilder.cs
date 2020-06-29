using CursusAdministratie.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie.UnitTests.Extensions
{
    public static class CursusBuilder
    {
        public static Cursus GetCursus(int id, string code, string titel)
        {
            return new Cursus
            {
                Id = id,
                Code = code,
                Titel = titel,
                Cursisten = new List<CursusInstantie>(),
            };
        }
    }
}
