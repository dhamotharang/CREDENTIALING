using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.UM.IServices.Authorization
{
    public interface IAuthorizationService
    {
        Task SaveAuthorization(AuthorizationViewModel Authorization);
       int? CalculatePlainLanguageValues(AuthorizationViewModel auth, int index);
       int GetPOSID(string POSName);
    }
}
