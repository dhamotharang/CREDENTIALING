using PortalTemplate.Areas.UM.Models.ViewModels.ICD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface IICDServices
    {
        ICDViewModel AddICD(ICDViewModel dto);
        ICDViewModel EditICD(ICDViewModel dto);
        void DeleteICD(int id);
    }
}