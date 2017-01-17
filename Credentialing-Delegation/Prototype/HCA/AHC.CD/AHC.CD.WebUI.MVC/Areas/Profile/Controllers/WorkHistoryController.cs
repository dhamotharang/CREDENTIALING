using AHC.CD.Business;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.WorkHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class WorkHistoryController : Controller
    {
        IProfileManager ProfileManager;

        // GET: Profile/WorkHistory
        public ActionResult Index()
        {
            return View();
        }

        public WorkHistoryController(IProfileManager ProfileManager)
        {

            this.ProfileManager=ProfileManager;
        }

        [HttpPost]
        public async Task<ActionResult> SaveWorkExp(ProfessionalWorkExperienceViewModel WorkExp)
        {
            Object response = "";

            if (ModelState.IsValid)
            {
                // save the file here 

                string docURL = "HardCoded File URL";

                // Has to be mapped using automapper

                //WorkGap.LastModifiedDate = new DateTime();
                //int WorkGapID = await ProfileManager.AddWorkGapAsync(1, WorkGap);


                //response = new { WorkGapID=WorkGapID,Message="Work Gap Added Successfully"};
            }
            else
            {
                response = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveWorkGapInfo(WorkGapViewModel WorkGap)
        {
            Object response = "";

            if (ModelState.IsValid)
            {
                // save the file here 

                string docURL = "HardCoded File URL";

                // Has to be mapped using automapper

                //WorkGap.LastModifiedDate = new DateTime();
                //int WorkGapID = await ProfileManager.AddWorkGapAsync(1, WorkGap);


                //response = new { WorkGapID=WorkGapID,Message="Work Gap Added Successfully"};
            }
            else
            {
                response = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // hard coded data for dev , please remove while release
        public JsonResult GetAllWorkExp()
        {

            //WorkGap WorkGap1 = new WorkGap();
            //WorkGap1.WorkGapID = 1;
            //WorkGap1.StartDate = new DateTime();
            //WorkGap1.EndDate = new DateTime();

            //WorkGap WorkGap2 = new WorkGap();
            //WorkGap2.WorkGapID = 1;
            //WorkGap2.StartDate = new DateTime();
            //WorkGap2.EndDate = new DateTime();

            //WorkGap WorkGap3 = new WorkGap();
            //WorkGap3.WorkGapID = 1;
            //WorkGap3.StartDate = new DateTime();
            //WorkGap3.EndDate = new DateTime();

            //List<WorkGap> WorkGaps = new List<WorkGap>();
            //WorkGaps.Add(WorkGap1);
            //WorkGaps.Add(WorkGap2);
            //WorkGaps.Add(WorkGap3);

            List<ProfessionalWorkExperienceViewModel> ProfessionalWorkExperiences = new List<ProfessionalWorkExperienceViewModel>();

            ProfessionalWorkExperienceViewModel ProfessionalWorkExperienceViewModel1 = new ProfessionalWorkExperienceViewModel();

            ProfessionalWorkExperienceViewModel1.EmployerName = "ashok";
            ProfessionalWorkExperienceViewModel1.DepartureReason = "temp reason";

            ProfessionalWorkExperiences.Add(ProfessionalWorkExperienceViewModel1);



            return Json(ProfessionalWorkExperiences, JsonRequestBehavior.AllowGet);
        }

    


    }
}