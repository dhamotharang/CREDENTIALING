using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Users;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models;
using AHC.CD.WebUI.MVC.Models;
using AHC.MailService;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ContractInformation;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.Areas.Initiation.Models;
using System.Text;
using AHC.CD.Business.Email;

namespace AHC.CD.WebUI.MVC.Areas.Initiation.Controllers
{
    public class ProviderController : Controller
    {
        private IUserManager UserManager { get; set; }
        private IErrorLogger ErrorLogger { get; set; }
        private IEmailSender EmailSender { get; set; }
        private IEmailServiceManager emailServiceManager = null;
        private ApplicationUserManager _authUserManager;
        public ApplicationUserManager AuthUserManager
        {
            get
            {
                return _authUserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _authUserManager = value;
            }
        }

        private ApplicationRoleManager _authRoleManager;
        public ApplicationRoleManager AuthRoleManager
        {
            get
            {
                return _authRoleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _authRoleManager = value;
            }
        }

        public ProviderController(IUserManager userManager, IErrorLogger errorLogger, IEmailSender mailService, IEmailServiceManager emailServiceManager)
        {
            this.UserManager = userManager;
            this.ErrorLogger = errorLogger;
            this.EmailSender = mailService;
            this.emailServiceManager = emailServiceManager;
        }


        [Authorize(Roles = "CCO,HR,CRA")]
        // GET: Initiation/Provider
        public ActionResult Index()
        {
            //var authUserId = await CreateAuthUser("sachingarg@pratian.com", "Password@123456");
            //SendEmail(authUserId, "sachingarg@pratian.com");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "CCO,CRA")]
        public async Task<ActionResult> Index(ProfileViewModel profile)
        {
            string ErrorMessage = "";
            int profileId = 0;
            bool providerInitiationStatus = true;
            bool mailStatus = true;

            if (string.IsNullOrEmpty(profile.ContactDetail.PhoneDetails[0].Number))
            {
                profile.ContactDetail.PhoneDetails = null;
            }

            try
            {
                if (ModelState.IsValid)
                {
                    //Create authentication user with the email address and the password as NPI
                    var authUserId = await CreateAuthUser(profile.ContactDetail.EmailIDs[0].EmailAddress, profile.OtherIdentificationNumber.NPINumber, profile.PersonalDetail.FirstName + profile.PersonalDetail.MiddleName + profile.PersonalDetail.LastName);

                    try
                    {
                        //Create CD user with the given information
                        DocumentDTO contractDocument = null;
                        DocumentDTO cvDocument = null;

                        if (profile.ContractInfoes[0].ContractFile != null)
                        {
                            contractDocument = CreateDocument(profile.ContractInfoes[0].ContractFile);
                        }

                        if (profile.CVInformation.CVFile != null)
                        {
                            cvDocument = CreateDocument(profile.CVInformation.CVFile);
                        }

                        var profileData = AutoMapper.Mapper.Map<ProfileViewModel, AHC.CD.Entities.MasterProfile.Profile>(profile);




                        profileData.StatusType = StatusType.Active;

                        profileId = await UserManager.InitiateProviderAsync(authUserId, profileData, cvDocument, contractDocument);

                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                        ErrorMessage = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
                        providerInitiationStatus = false;
                    }

                    try
                    {
                        //end Activation Link with username and password
                        //SendEmail(authUserId, profile.ContactDetail.EmailIDs[0].EmailAddress, profileId);

                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                        ErrorMessage = ex.Message;
                        mailStatus = false;
                    }
                }
                else
                {
                    providerInitiationStatus = false;
                    mailStatus = false;
                    ErrorMessage = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                ErrorMessage = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                ErrorMessage = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { providerInitiationStatus = providerInitiationStatus, mailStatus = mailStatus, errorMessage = ErrorMessage, profileId = profileId }, JsonRequestBehavior.AllowGet);
        }

        #region Private Methods

        private async Task<string> CreateAuthUser(string userName, string password, string fullName)
        {
            try
            {
                //Create Role Admin if it does not exist
                var role = await AuthRoleManager.FindByNameAsync("PRO");
                if (role == null)
                {
                    role = new IdentityRole("PRO");
                    var roleresult = await AuthRoleManager.CreateAsync(role);
                }

                var appUser = new ApplicationUser() { UserName = userName, Email = userName, LockoutEnabled = true, FullName = fullName };
                var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
                if (user == null)
                {
                    var result = await AuthUserManager.CreateAsync(appUser, password);
                    await AddUserToRole(appUser, "PRO");
                    return appUser.Id;
                }

                return user.Id;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private async Task AddUserToRole(ApplicationUser user, string roleName)
        {
            // Add user admin to Role Admin if not already added
            var rolesForUser = await AuthUserManager.GetRolesAsync(user.Id);
            if (!rolesForUser.Contains(roleName))
            {
                var result = await AuthUserManager.AddToRoleAsync(user.Id, roleName);
            }
        }

        private DocumentDTO CreateDocument(HttpPostedFileBase file, bool isRemoved = false)
        {
            DocumentDTO document = new DocumentDTO() { IsRemoved = isRemoved };

            if (file != null)
            {
                document.FileName = file.FileName;
                document.InputStream = file.InputStream;
            }

            return document;
        }
        public string GetBaseUrl()
        {
            var request = HttpContext.ApplicationInstance.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;


            if (!string.IsNullOrWhiteSpace(appUrl)) appUrl += "/";


            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
            return baseUrl;
        }
        //private void SendEmail(string authUserId, string emailAddress, int profileId)
        //{
        //    var hosturl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/ConfirmAccount?token=" + authUserId;
        //    var rootPath = GetBaseUrl();
        //    //var confirmationLink = string.Format("<a href=\"{0}\">Clink to confirm your registration</a>\n\nLogin with username as {1} and password as your NPI number", hosturl, emailAddress);
        //    AHC.CD.Entities.MasterProfile.Profile profile = new AHC.CD.Entities.MasterProfile.Profile();
        //    profile = UserManager.getProfileById(profileId);
        //    var confirmationLink = "";
        //    if (profile.PersonalDetail.ProviderLevel.Name.Equals("Doctor"))
        //    {
        //        //confirmationLink = string.Format("<h4>Dear Provider,</h4>" +
        //        //    "Welcome to the Access Family! <br/><br/>" +
        //        //    "We will be bringing you online with our systems. <br/><br/>" +
        //        //    "We have auto registered your e-mail id and now you have access to your Profile. <br/>" +
        //        //    "<dl><dt><span>Please take note of your credentials below:</span></dt><dd>Username: {1} (Your email ID)</dd>" +
        //        //    "<dd>Password: The Password is your NPI Number (National Provider Identification Number)</dd></dl>" +
        //        //    "<a href=\"{0}\" style=\"text-decoration: none;display: inline-block;margin-bottom: 0;font-weight: normal;text-align: center;vertical-align: middle;-ms-touch-action: manipulation;touch-action: manipulation;cursor: pointer;background-image: none;border: 1px solid transparent;" +
        //        //    "white-space: nowrap;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;border-radius: 4px;-webkit-user-select: none;-moz-user-select: none;-ms-user-select: none;user-select: none;color: #fff;background-color: #337ab7;border-color: #2e6da4;padding: 5px 10px;font-size: 12px;line-height: 1.5;border-radius: 3px;text-shadow: 0 -1px 0 rgba(0,0,0,0.2);-webkit-box-shadow: inset 0 1px 0 rgba(255,255,255,0.15),0 1px 1px rgba(0,0,0,0.075);" +
        //        //    "box-shadow: inset 0 1px 0 rgba(255,255,255,0.15),0 1px 1px rgba(0,0,0,0.075);background-image: -webkit-linear-gradient(top, #337ab7 0, #265a88 100%);background-image: -o-linear-gradient(top, #337ab7 0, #265a88 100%);background-image: -webkit-gradient(linear, left top, left bottom, color-stop(0, #337ab7), to(#265a88));" +
        //        //    "background-image: linear-gradient(to bottom, #337ab7 0, #265a88 100%);filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#ff337ab7', endColorstr='#ff265a88', GradientType=0);filter: progid:DXImageTransform.Microsoft.gradient(enabled = false);background-repeat: repeat-x;border-color: #245580;\">Login</a>" +
        //        //    "<br/><br/><span>*** Please change your password upon logging in, for better security.</span> <br/><br/><h4 style=\"color: rgb(30, 30, 237);\"><u>Please have the following documents ready for Credentialing Profile:</u></h4>" +
        //        //    "<ul><li>Application or CAQH Application w/Attestation</li><li>Medical License</li><li>Curriculum Vitae (written explanation of gaps greater than 6 months)</li>" +
        //        //    "<li>Board Certification (if applicable)</li><li>ECFMG (if applicable)</li><li>Current DEA Certificate</li><li>Current Malpractice Liability Insurance Certificate</li><li>Copy of Hospital Privilege Letters (if applicable)</li><li>Attestation of Total Patient Load (PCP’s only)</li>" +
        //        //    "<li>Attestation of Site Visit</li><li>Medical School Diploma, Residency and Fellowship Certificate</li><li>Special Certifications (ex. ACLS, PALS, BLS, CPR)</li><li>Copy of Driver’s License, SSC, and passport</li><li>CME Certificates within the past two years</li></ul>" +
        //        //    "<br/><br/>Any missing or expired documentation will delay the credentialing process. If you have any questions, please don’t hesitate to contact me. Thank you for your cooperation.<br/><br/>" +
        //        //    "Please contact us if you have any questions. <br/><br/><b>Regards,</b> <br/><br/>" +
        //        //    "<table><tr><th>Leslie Hedick</th><th></th><th>Jeanine Martin</th></tr><tr><td>HR Director</td><td></td><td>Credentialing Coordinator</td></tr></table><br/><br/>" +
        //        //    "<b><i>Jeanine Martin </i></b> <br/><i>Credentialing Coordinator, Access 2 Healthcare Physicians LLC <br/>P. 352-277-5307 ext 7801, F. 352-277-5309 or 352-277-5288<br/>" +
        //        //    "<a href=\"mailto:abcdd1536@gmail.com?subject=New%20Provider%20Information\">abcdd1536@gmail.com</a></i>", hosturl, emailAddress);
        //        confirmationLink = string.Format("Dear" + profile.PersonalDetail.FirstName + " " + profile.PersonalDetail.LastName+",<br/><br/>" +

        //            "Welcome" + System.Configuration.ConfigurationManager.AppSettings["Welcome"] +"<br/><br/>" +

        //            "We will be bringing you online with our systems.<br/><br/>" +

        //            "Please supply the following documentation  for your Credentialing Profile:<br/><br/>" +

        //            "<ul><li>CAQH user name and password</li>" +
        //            "<li>NPI user name and password</li>" +
        //            "<li>Medical License</li>" +
        //            "<li>Curriculum Vitae (written explanation of gaps greater than 6 months)</li>" +
        //            "<li>Board Certification (if applicable)</li>" +
        //            "<li>ECFMG (if applicable)</li>" +
        //            "<li>Current DEA Certificate</li>" +
        //            "<li>Current Malpractice Liability Insurance Certificate</li>" +
        //            "<li>Copy of Hospital Privilege Letters (if applicable)</li>" +
        //            "<li>Attestation of Total Patient Load (PCP’s only)</li>" +
        //            "<li>Attestation of Site Visit</li>" +
        //            "<li>Medical School Diploma, Residency and Fellowship Certificate</li>" +
        //            "<li>Special Certifications (ex. ACLS, PALS, BLS, CPR)</li>" +
        //            "<li>Copy of Driver’s License, SSC, and passport</li>" +
        //            "<li>CME Certificates within the past two years</li></ul><br/><br/>" +

        //            "Any missing or expired documentation will delay the credentialing process.</br></br>" +

        //            "If you have any questions, please don’t hesitate to contact us.<br/><br/>" +

        //            "Thank you for your cooperation.<br/><br/>" +

        //            "Regards,<br/><br/>");
        //        if (System.Configuration.ConfigurationManager.AppSettings["isPrimeCare"] == "true")
        //        {
        //            confirmationLink += ("<i style='font-weight:bold;color: #337ab7;'>" +
        //                System.Configuration.ConfigurationManager.AppSettings["CredentialingManager"] + "&nbsp;&nbsp;&nbsp;" + System.Configuration.ConfigurationManager.AppSettings["ProjectManager"] + "</i><br/>" +
        //                "<i style='font-weight:bold;color: #337ab7;'>Credentialing Manager&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&thinsp;&thinsp;Project Manager</i> <br/> <br/>" +
        //                "<img style='width:100px;height:30px' src='" + rootPath + "/Content/Images/logo2.png' /><br/>" +
        //                //"HR Director<br/>" +
        //                //"lhedick@accesshealthcarellc.net<br/><br/>" +
        //                "Attention:Credentialing Dept.<br/>" +
        //                "1214 Mariner Blvd<br/> " +
        //                "Spring Hill, FL 34609-5657<br/><br/><br/>"
        //                + "<b>");                                      
        //        }
                  
        //        else
        //        {
        //            confirmationLink += ("Leslie Hedick<br/>"+                     
        //            "HR Director<br/>"+
        //            "lhedick@accesshealthcarellc.net<br/><br/>"+ 

        //            "Credentialing Dept.<br/>"+ 
        //            "(352) 799-0046<br/>"+
        //            "credentialing@accesshealthcarellc.net<br/>"+
        //            "<br/><br/><b>This is a System Generated mail, Please don't respond to this..</b>");
        //        }
        //        confirmationLink += (System.Configuration.ConfigurationManager.AppSettings["DisclaimerMessage"] + "</b></br>");
        //    }
        //    else
        //    {
        //        confirmationLink = string.Format("Dear" + profile.PersonalDetail.FirstName + " " + profile.PersonalDetail.LastName +",<br/><br/>" +
        //            "Welcome" + System.Configuration.ConfigurationManager.AppSettings["Welcome"] + "<br/><br/>" +
        //            "We will be bringing you online with our systems.<br/><br/>" +
        //            "Please have the following documents ready for your Credentialing Profile:<br/><br/>" +
        //            "<ul><li>CAQH user name and password</li>" +
        //            "<li>NPI user name and password</li>" +
        //            "<li>Medical License</li>" +
        //            "<li>Curriculum Vitae (written explanation of gaps greater than 6 months)</li>" +
        //            "<li>Board Certification (if applicable)</li>" +
        //            "<li>Current Malpractice Liability Insurance Certificate</li>" +
        //            "<li>Copy of Hospital Privilege Letters (if applicable)</li>" +
        //            "<li>Copies of all Diplomas</li>" +
        //            "<li>Special Certifications (ex. ACLS, PALS, BLS, CPR)</li>" +
        //            "<li>ARNP Protocol (ARNP’s only)</li>" +
        //            "<li>Supervision Data Form (PA’s only)</li>" +
        //            "<li>Copy of Driver’s License, SSC, and passport</li>" +
        //            "<li>CME Certificates within the past two years</li></ul>" +
        //            "Any missing or expired documentation will delay the credentialing process.<br/><br/>" +
        //            "If you have any questions, please don’t hesitate to contact us.<br/><br/>" +
        //            "Thank you for your cooperation.<br/><br/>" +
        //            "Regards,<br/><br/>");
        //        if (System.Configuration.ConfigurationManager.AppSettings["isPrimeCare"] == "true")
        //        {
        //            confirmationLink += ("<i style='font-weight:bold;color: #337ab7;'>" +
        //                System.Configuration.ConfigurationManager.AppSettings["CredentialingManager"] + "&nbsp;&nbsp;&nbsp;" + System.Configuration.ConfigurationManager.AppSettings["ProjectManager"] + "</i><br/>" +
        //                "<i style='font-weight:bold;color: #337ab7;'>Credentialing Manager&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&thinsp;&thinsp;Project Manager</i> <br/> <br/>" +
        //                "<img style='width:100px;height:30px' src='" + rootPath + "/Content/Images/logo2.png' /><br/>" +
        //                //"HR Director<br/>" +
        //                //"lhedick@accesshealthcarellc.net<br/><br/>" +
        //                "Attention:Credentialing Dept.<br/>" +
        //                "1214 Mariner Blvd<br/> " +
        //                "Spring Hill, FL 34609-5657<br/><br/><br/>"
        //                + "<b>");                    
        //        }
        //        else
        //        {
        //            confirmationLink += ("Leslie Hedick<br/>" +
        //            "HR Director<br/>" +
        //            "lhedick@accesshealthcarellc.net<br/><br/>" +

        //            "Credentialing Dept.<br/>" +
        //            "(352) 799-0046<br/>" +
        //            "credentialing@accesshealthcarellc.net<br/>" +
        //            "<br/><br/><b>This is a System Generated mail, Please don't respond to this..</b>");
        //        }
        //        confirmationLink += (System.Configuration.ConfigurationManager.AppSettings["DisclaimerMessage"] + "</b></br>");
        //    }
        //    // Send Confirmation Email.
        //    EMailModel confirmEmailData = new EMailModel();
        //    confirmEmailData.Subject = "Welcome Email";
        //    confirmEmailData.To = emailAddress;
        //    //confirmEmailData.From = "venkat@pratian.com";
        //    confirmEmailData.Body = confirmationLink;
        //    EmailSender.SendMail(confirmEmailData);

        //    string emailBody = string.Format("Dear All,<br/><br/>" +
        //        "Welcome " + profile.PersonalDetail.FirstName + " " + profile.PersonalDetail.LastName + System.Configuration.ConfigurationManager.AppSettings["Welcome"] + "<br/><br/>" +
        //        "We will be bringing him online with our systems.<br/><br/>" +

        //        "If you have any questions, please don’t hesitate to contact us.<br/><br/>" +
        //        "Thank you for your cooperation.<br/><br/>" +
        //        "Regards,<br/><br/>");
        //    if (System.Configuration.ConfigurationManager.AppSettings["isPrimeCare"] == "true")
        //    {
        //        emailBody += ("<i style='font-weight:bold;color: #337ab7;'>" +
        //            System.Configuration.ConfigurationManager.AppSettings["CredentialingManager"] + "&nbsp;&nbsp;&nbsp;" + System.Configuration.ConfigurationManager.AppSettings["ProjectManager"] + "</i><br/>" +
        //            "<i style='font-weight:bold;color: #337ab7;'>Credentialing Manager&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&thinsp;&thinsp;Project Manager</i> <br/> <br/>" +
        //            "<img style='width:100px;height:30px' src='" + rootPath + "/Content/Images/logo2.png' /><br/>" +
        //            //"HR Director<br/>" +
        //            //"lhedick@accesshealthcarellc.net<br/><br/>" +
        //            "Attention:Credentialing Dept.<br/>" +
        //            "1214 Mariner Blvd<br/> " +
        //            "Spring Hill, FL 34609-5657"
        //            + "<b>");
        //            //"(352) 799-0046<br/>" +
        //            //"credentialing@accesshealthcarellc.net
                    
        //    }
        //    else
        //    {
        //        confirmationLink += ("Leslie Hedick<br/>" +
        //        "HR Director<br/>" +
        //        "lhedick@accesshealthcarellc.net<br/><br/>" +

        //        "Credentialing Dept.<br/>" +
        //        "(352) 799-0046<br/>" +
        //        "credentialing@accesshealthcarellc.net<br/>" +
        //        "<br/><br/><b>This is a System Generated mail, Please don't respond to this..</b>");
        //    }
        //    confirmationLink += (System.Configuration.ConfigurationManager.AppSettings["DisclaimerMessage"] + "</b></br>");
        //    EMailModel newEmailModel = new EMailModel();
        //    newEmailModel.Subject = "New Provider Initiated";
        //    string GroupMailID = System.Configuration.ConfigurationManager.AppSettings["StakeHoldersGroupEmailID"];
        //    if (GroupMailID != null)
        //    {
        //        var id = Int32.Parse(GroupMailID);
        //        List<string> emailList = emailServiceManager.GetAllEmailsForaGroup(id);
        //        newEmailModel.To = "";
        //        foreach (var email in emailList)
        //        {
        //            newEmailModel.To += email;
        //            if (emailList.LastOrDefault() != email)
        //            {
        //                newEmailModel.To += ",";
        //            }
        //        }
        //        StringBuilder mailBody = new StringBuilder();
        //        mailBody.Append("<b>Dear All ");
        //        mailBody.Append("</b></br></br>");
        //        mailBody.Append("<p>This is to inform you that a new Provider is initiated in CredAxis. Please refer below for the details,");
        //        mailBody.Append("<br/><br/></p><pre>");
        //        mailBody.Append("    Provider Name            : " + profile.PersonalDetail.FirstName + " " + profile.PersonalDetail.LastName + "<br/>");
        //        mailBody.Append("    NPI Number               : " + profile.OtherIdentificationNumber.NPINumber + "<br/>");
        //        mailBody.Append("    Provider Level           : " + profile.PersonalDetail.ProviderLevel.Name + "<br/>");
        //        var ProviderTitle = profile.PersonalDetail.ProviderTitles.ToList().FirstOrDefault();
        //        mailBody.Append("    Title                    : " + ProviderTitle.ProviderType.Title + "<br/>");
        //        mailBody.Append("    Email ID                 : " + profile.ContactDetail.EmailIDs.First().EmailAddress + "<br/>");
        //        if (profile.OtherIdentificationNumber.CAQHNumber != null)
        //            mailBody.Append("    CAQH Number              : " + profile.OtherIdentificationNumber.CAQHNumber + "<br/>");
        //        else
        //            mailBody.Append("    CAQH Number              : Not Available <br/>");

        //        var ContractInfo = profile.ContractInfoes.ToList().FirstOrDefault();
        //        mailBody.Append("    Provider Relationship    : " + ContractInfo.ProviderRelationship + "<br/>");
        //        mailBody.Append("    Join Date                : " + ContractInfo.JoiningDate.Value.ToString("MM/dd/yyyy") + "<br/>");
        //        var ContractGroupInfos = ContractInfo.ContractGroupInfoes.ToList();

        //        if (ContractGroupInfos.Count > 0)
        //        {
        //            mailBody.Append("    IPA / Group              : ");
        //            foreach (var ContractGroupInfo in ContractGroupInfos)
        //            {

        //                mailBody.Append(ContractGroupInfo.PracticingGroup.Group.Name);
        //                if (ContractGroupInfos.LastOrDefault() != ContractGroupInfo)
        //                    mailBody.Append(", ");

        //            }
        //        }
        //        else
        //            mailBody.Append("    IPA / Group              : Not Available <br/>");

        //        mailBody.Append("<br/><br/>");
        //        mailBody.Append("</pre><br/><br/>");
        //        mailBody.Append("Thank You,<br/>");
        //        mailBody.Append("Credentialing Dept.<br/>");
        //        mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["CompanyName"] + "<br/>");
        //        mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["CompanyContactNumber"] + "<br/>");
        //        mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["ProductEmailID"]);
        //        mailBody.Append("<br/><br/><br/><b>");
        //        //mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["DisclaimerMessage"]);
        //        // mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["DisclaimerEmailID"]);
        //        mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["DisclaimerMessage"]);
        //        mailBody.Append("</b>");
        //        newEmailModel.Body = mailBody.ToString();
        //        EmailSender.SendMail(newEmailModel);
        //    }
        //}

        #endregion
    }
}