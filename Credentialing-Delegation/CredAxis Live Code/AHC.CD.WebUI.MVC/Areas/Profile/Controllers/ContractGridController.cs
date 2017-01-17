using AHC.CD.Business.Profiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ContractGridController : Controller
    {
        //
        // GET: /Profile/ContractGrid/
        private IContractManager contractmanager = null;
        public ContractGridController(IContractManager contractmanager)
        {
            this.contractmanager = contractmanager;
        }

        public string GetAllActiveContractGridinfoes(int profileid)
        {
            var result = contractmanager.GetAllActiveContractGridInfoByID(profileid);
            return JsonConvert.SerializeObject(result);
        }
        public string GetAllInActiveContractGridinfoes(int ProfileID)
        {
            var result = contractmanager.GetAllInactiveContractGridByID(ProfileID);
            return JsonConvert.SerializeObject(result);
        }
	}
}