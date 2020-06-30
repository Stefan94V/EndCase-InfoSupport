using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie.Data.ViewModels.CursusInstantie
{
    public class CursusInstantieUploadResultSet
    {
        public List<Data.Models.CursusInstantie> Uploaded { get; set; }
        public List<Data.Models.CursusInstantie> Duplicate { get; set; }
    }
}
