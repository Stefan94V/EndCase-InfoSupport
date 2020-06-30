using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie.Data.ViewModels.CursusInstantie
{
    public class CursusInstantieToDetailsDto
    {
        public int Id { get; set; }
        public DateTime StartDatum { get; set; }
        public string Titel { get; set; }
        public string Code { get; set; }
        public int Duur { get; set; }
        public int AantalCursisten { get; set; }
    }
}
