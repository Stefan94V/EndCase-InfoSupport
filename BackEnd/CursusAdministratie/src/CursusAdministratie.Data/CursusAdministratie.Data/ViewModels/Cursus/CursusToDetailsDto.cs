using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie.Data.ViewModels.Cursus
{
    public class CursusToDetailsDto
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Code { get; set; }
        public int Duur { get; set; }
        //public List<DateTime> StartDatums { get; set; }
    }
}
