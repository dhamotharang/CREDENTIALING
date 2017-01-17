using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface ILOSServices
    {
        LengthOfStayViewModel AddLOS(LengthOfStayViewModel dto);
        LengthOfStayViewModel EditLOS(LengthOfStayViewModel dto);
    }
}