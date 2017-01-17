using AHC.CD.Business;
using AHC.CD.Entities.MasterProfile.ProfessionalAffiliation;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalAffiliation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ProfessionalAffiliationController : Controller
    {
        private IProfileManager profileManager = null;

        public ProfessionalAffiliationController(IProfileManager profileManager)
	    {
            this.profileManager = profileManager;
	    }

        [HttpPost]
        public async Task<ActionResult> AddProfessionalAffiliation(int profileId, ProfessionalAffiliationDetailViewModel professionalAffiliation)
        {
            string returnMessage = "";

            try
            {
                if (ModelState.IsValid)
                {
                    var data = new ProfessionalAffiliationInfo() 
                    {
                        EndDate = professionalAffiliation.EndDate,
                        OrganizationName = professionalAffiliation.OrganizationName,
                        StartDate = professionalAffiliation.StartDate,
                        PositionOfficeHeld = professionalAffiliation.PositionOfficeHeld,
                        Member = professionalAffiliation.Member
                    };
                    
                    var result = await profileManager.AddProfessionalAffiliationAsync(profileId, data);
                    returnMessage = result.ToString();
                }
                else
                {
                    returnMessage = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }

            return Json(returnMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProfessionalAffiliation(int profileId, ProfessionalAffiliationDetailViewModel professionalAffiliation)
        {
            string returnMessage = "";

            if (ModelState.IsValid)
            {
                var data = new ProfessionalAffiliationInfo()
                {
                    ProfessionalAffiliationInfoID = professionalAffiliation.ProfessionalAffiliationInfoID,
                    EndDate = professionalAffiliation.EndDate,
                    OrganizationName = professionalAffiliation.OrganizationName,
                    StartDate = professionalAffiliation.StartDate,
                    PositionOfficeHeld = professionalAffiliation.PositionOfficeHeld,
                    Member = professionalAffiliation.Member
                };

                await profileManager.UpdateProfessionalAffiliationAsync(profileId, data);
                returnMessage = "true";
            }
            else
            {
                returnMessage = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }

            return Json(returnMessage, JsonRequestBehavior.AllowGet);
        }
    }
}