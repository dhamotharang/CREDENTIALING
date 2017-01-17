using AHC.CD.Business.Profiles;
using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AHC.CD.WebApi.Controllers
{
    public class ProviderServiceController : ApiController
    {
        private IProfileServiceManager profiles;

        public ProviderServiceController(IProfileServiceManager profiles)
        {
            this.profiles = profiles;
        }
        [HttpGet]
        public List<ProviderDTO> Index()
        {
            List<ProviderDTO> providers = profiles.GetAllProviders();
            return providers;

        }
    }
}
