using AHC.CD.Business.Users;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.MailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Initiation.Controllers
{
    public class TeamLeadController : InitiateUserController
    {
        public TeamLeadController(IUserManager userManager, IErrorLogger errorLogger, IEmailSender mailService)
            : base(userManager, errorLogger, mailService)
        {

        }
        
        // GET: Initiation/TeamLead
        public async Task<ActionResult> Index()
        {
            string status = "true";
            int profileId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    //Create authentication user with the email address and the password
                    var authUserId = await CreateAuthUser("", "", "TL");

                    //Map the view model to business model
                    //var profileData = AutoMapper.Mapper.Map<ProfileViewModel, AHC.CD.Entities.MasterProfile.Profile>(profile);

                    //await UserManager.InitiateTeamLeadAsync(authUserId);

                    //end Activation Link with username and password
                    SendEmail(authUserId, "", "");
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, profileId = profileId }, JsonRequestBehavior.AllowGet);
        }


    }
}