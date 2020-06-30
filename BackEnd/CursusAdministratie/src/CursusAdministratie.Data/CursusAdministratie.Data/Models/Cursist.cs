using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CursusAdministratie.Data.Models
{
    public class Cursist
    {
        [Key]
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Achternaam { get; set; }
        public virtual ICollection<CursusInstantie> Cursussen { get; set; }

    }
}
