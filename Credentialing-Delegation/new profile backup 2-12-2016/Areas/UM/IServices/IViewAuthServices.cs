using PortalTemplate.Areas.UM.Models.ViewModels.ViewAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface IViewAuthServices
    {
        ViewAuthorizationViewModel ConvertAuth(ViewAuthorizationViewModel auth);
        DateTime UpdateDischargeDate(int AuthID, DateTime DischargeDate);
        ViewAuthorizationViewModel GetAuthByID(int AuthID);
        ViewAuthorizationViewModel GetTabData(string TabId, int AuthID);
    }
}