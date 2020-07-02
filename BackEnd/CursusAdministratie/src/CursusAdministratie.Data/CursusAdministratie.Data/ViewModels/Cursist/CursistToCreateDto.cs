using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie.Data.ViewModels.Cursist
{
    public class CursistToCreateDto
    {
        public string Naam { get; set; }
        public string Achternaam { get; set; }
        public int CursusInstantieId { get; set; }
    }
}
