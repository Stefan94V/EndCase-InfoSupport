﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CursusAdministratie.Data.Models
{
    public class Cursus
    {
        [Key]
        public int Id { get; set; }
        public string Titel { get; set; }
        public int Duur { get; set; }
        public string Code { get; set; }
        public ICollection<CursusInstantie> CursusInstanties { get; set; }
    }
}
