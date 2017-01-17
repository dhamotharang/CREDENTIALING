using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Enums
{
    public enum EducationGraduateType
    {
        [Display(Name = ("Graduate"))]
        Graduate = 1,
        [Display(Name=("Fifth Pathway Graduate"))]
        FifthPathwayGraduate
    }
}
