using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie.Data.ViewModels.CursusInstantie
{
    public class CursusInstantieUploadedResultSetDto
    {
        public List<CursusInstantieToDetailsDto> Uploaded { get; set; }
        public List<CursusInstantieToDetailsDto> Duplicates { get; set; }
        public string Message { get; set; }
    }
}
