using AHC.CD.Business.Profiles;
using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.Credentialing.Loading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AHC.CD.WebApi.Controllers
{
    public class ProviderServiceADOController : ApiController
    {
        private readonly IProfileManagerServiceADO iProfileManagerServiceADO = null;
        public ProviderServiceADOController(IProfileManagerServiceADO iProfileManagerServiceADO)
        {
            this.iProfileManagerServiceADO = iProfileManagerServiceADO;
        }
        //    
        // GET: /ProviderServiceADO/
        [HttpGet]
        public IEnumerable<ProviderDTO> Index()
        {
            IEnumerable<ProviderDTO> providersData = iProfileManagerServiceADO.GetAllProviders();
            return providersData;
        }
        [HttpGet]
        public IEnumerable<ProfileAndPlanDTO> Index1()
        {
            IEnumerable<ProfileAndPlanDTO> ProviderPlans = iProfileManagerServiceADO.GetAllCredentialingPlansForProviders();
            return ProviderPlans;
        }
    }
}
