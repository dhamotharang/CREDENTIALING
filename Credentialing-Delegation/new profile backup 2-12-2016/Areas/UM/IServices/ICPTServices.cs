using PortalTemplate.Areas.UM.Models.ViewModels.CPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface ICPTServices
    {
        CPTViewModel AddCPT(CPTViewModel dto);
        CPTViewModel EditCPT(CPTViewModel dto);
        void DeleteCPT(CPTViewModel dto);
    }
}