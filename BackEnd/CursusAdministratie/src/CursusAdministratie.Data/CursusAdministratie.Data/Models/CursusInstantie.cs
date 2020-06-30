using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursusAdministratie.Data.Models
{
    public class CursusInstantie
    {
        [Key]
        public int Id { get; set; }
        public int CursusId { get; set; }
        public virtual Cursus Cursus { get; set; }
        public virtual ICollection<Cursist> Cursisten { get; set; }

        public DateTime StartDatum { get; set; }


    }
}
