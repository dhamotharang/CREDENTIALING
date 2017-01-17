using PortalTemplate.Areas.CredAxis.Models.SpecialtyViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CredAxis.Services.IServices
{
   public  interface ISpecialityService
    {
       //SpecialityMainViewModel GetAllSpeciality(int ProfileId,int SpecialtyId);
       ////SpecialityMainViewModel AddEditSpeciality(SpecialityMainViewModel specialityMainViewModel);  
       //SpecialtyDetailViewModel AddEditSpeciality(SpecialityMainViewModel specialtyMainViewModel, int ProfileId,int SpecialtyId);  
        List<SpecialtyDetailViewModel> GetAllSpeciality();
        SpecialityMainViewModel AddEditSpeciality(SpecialityMainViewModel specialityMainViewModel);  
    }
}
