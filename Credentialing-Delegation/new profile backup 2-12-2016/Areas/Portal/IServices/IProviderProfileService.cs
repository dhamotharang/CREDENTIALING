using PortalTemplate.Areas.Portal.Models.ProviderProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Portal.IServices
{
    interface IProviderProfileService
    {
        ProviderProfileViewModel GetProfile(int ProfileId);
        ProviderProfileViewModel EditProfile(ProviderProfileViewModel ProviderProfile);
    }
}
