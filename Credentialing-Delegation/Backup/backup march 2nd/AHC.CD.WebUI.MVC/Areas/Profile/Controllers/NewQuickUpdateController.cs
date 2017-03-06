﻿using AHC.CD.Business;
using AHC.CD.Business.Users;
using AHC.CD.ErrorLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.CustomHelpers;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.Entities.MasterData.Account;
using Newtonsoft.Json;
using AHC.CD.Business.Profiles;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfileReviewSection;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.Entities.MasterProfile.ProfileReviewSection;


namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class NewQuickUpdateController : Controller
    {
        private IProfileManager profileManager = null;
        private IUserManager userManager = null;
        private IErrorLogger errorLogger = null;
        private IQuickUpdateManager quickUpdateManager = null;

        public NewQuickUpdateController(IProfileManager profileManager, IErrorLogger errorLogger, IUserManager userManager, IQuickUpdateManager quickUpdateManager)
        {
            this.profileManager = profileManager;
            this.userManager = userManager;
            this.errorLogger = errorLogger;
            this.quickUpdateManager = quickUpdateManager;
        }

        //
        // GET: /Profile/NewQuickUpdate/
        public ActionResult Index()
        {
            return View();
        }

        [CompressFilter]
        public async Task<ActionResult> ProviderProfile()
        {
            try
            {
                var profileId = this.userManager.GetProfileId(User.Identity.GetUserId());
                if (profileId == null)
                    return View("ProfileDoesNotExist");

                ViewBag.Roles = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());

                ViewBag.ProfileId = profileId;

                ViewBag.Demographics = JsonConvert.SerializeObject(await profileManager.GetDemographicsProfileDataAsync(Convert.ToInt32(profileId)), new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });

                //ViewBag.StateLicenses = JsonConvert.SerializeObject(await profileManager.GetIdentificationAndLicensesProfileDataAsync(Convert.ToInt32(profileId)));

                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [CompressFilter]
        public async Task<ActionResult> QuickProviderProfile(int id)
        {
            try
            {
                ViewBag.ProfileId = id;

                ViewBag.Roles = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());

                ViewBag.Demographics = JsonConvert.SerializeObject(await profileManager.GetDemographicsProfileDataAsync(id), new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });

                //ViewBag.StateLicenses = JsonConvert.SerializeObject(await profileManager.GetIdentificationAndLicensesProfileDataAsync(id));

               // ViewBag.ProfileData = JsonConvert.SerializeObject(await quickUpdateManager.GetProfileReviewDataAsync(id));

                return View("ProviderProfile");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [CompressFilter]
        public async Task<ActionResult> TestActionMethod(int profileId)
        {
            try
            {
                var data = await quickUpdateManager.GetProfileReviewDataAsync(profileId);
                //return View("ProviderProfile");
                return Json(new { profilereviewdata = data }, JsonRequestBehavior.AllowGet);    
            }
            catch (Exception)
            {

                throw;
            }
        }

        [CompressFilter]
        public JsonResult DisplayProfileSectionNotApplicable(ProfileReviewSectionViewModel profileReview) 
        {
            string status = "true";
            ProfileReviewSection dataModelProfileReviewSection = null;  
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProfileReviewSection = AutoMapper.Mapper.Map<ProfileReviewSectionViewModel, ProfileReviewSection>(profileReview);

                    quickUpdateManager.DisplayProfileSectionNotApplicableAsync(dataModelProfileReviewSection); 

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.SECTION_NOTAPPLICABLE;
            }

            return Json(new { status = status}, JsonRequestBehavior.AllowGet);

        }

    }
}