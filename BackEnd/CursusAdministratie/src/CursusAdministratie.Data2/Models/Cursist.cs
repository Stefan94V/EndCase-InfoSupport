using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie.Data.Models
{
    public class Cursist
    {
        [Key]
        public int Id { get; set; }
        public string Achternaam { get; set; }
        public virtual ICollection<CursusInstantie> Cursussen { get; set; }

    }
}
