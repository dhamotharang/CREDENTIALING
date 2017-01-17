using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Enums
{
    public enum ResidencyInternshipProgramType
    {
        [Display(Name = ("Internship"))]
        Internship = 1,
        [Display(Name = ("Fellowship"))]
        Fellowship,
        [Display(Name = ("Residency"))]
        Resident,
        [Display(Name = ("Other"))]
        Other
    }
}
