using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie.Data.Models
{
    public class Cursus
    {
        [Key]
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Code { get; set; }
        public virtual ICollection<CursusInstantie> Cursisten { get; set; }
    }
}
