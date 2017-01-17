using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers.ProviderProfileController
{
    public class HospitalPrivilegeController : Controller
    {
        //
        // GET: /CredAxis/HospitalPrivilege/
        public ActionResult Index()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Hospital Privilege/_ViewHospitalPrivilege.cshtml");
        }
        public ActionResult GetHospitalPrivilages(string val)
        {
            if (val == "Edit")
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Hospital Privilege/_AddEditHospitalPrivilege.cshtml");

            }
            else
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Hospital Privilege/_AddHospitalPrivilege.cshtml");

            }
        }

    }
}