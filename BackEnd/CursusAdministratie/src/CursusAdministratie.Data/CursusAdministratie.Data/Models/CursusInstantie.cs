using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursusAdministratie.Data.Models
{
    public class CursusInstantie
    {
        [Key, Column(Order = 0)]
        public int CursusId { get; set; }
        [Key, Column(Order = 1)]
        public int CursistId { get; set; }

        public virtual Cursus Cursus { get; set; }
        public virtual Cursist Cursist { get; set; }

        public DateTime StartDatum { get; set; }


    }
}
