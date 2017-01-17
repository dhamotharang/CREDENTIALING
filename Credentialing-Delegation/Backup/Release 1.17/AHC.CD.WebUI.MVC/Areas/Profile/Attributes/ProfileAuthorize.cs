using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Attributes
{
    public class ProfileAuthorize : System.Web.Mvc.AuthorizeAttribute
    {
        public ProfileActionType Action { get; set; }
        public bool ContainsHashed { get; set; }

        public ProfileAuthorize(ProfileActionType action, bool containsHashed)
        {
            this.Action = action;
            this.ContainsHashed = containsHashed;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            var component = new SiteActionsComponent();
            var roles = component.GetRolesForSiteActions(Action, ContainsHashed);
            if (!roles.Any(user.IsInRole))
            {
                return false;
            }

            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                var urlHelper = new UrlHelper(context.RequestContext);
                context.HttpContext.Response.StatusCode = 403;
                context.Result = new JsonResult
                {
                    Data = new
                    {
                        Error = "NotAuthorized",
                        LogOnUrl = "/Account/Login"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                base.HandleUnauthorizedRequest(context);
            }
        }
    }

    public class SiteActionsComponent
    {
        internal List<string> GetRolesForSiteActions(ProfileActionType currentAction, bool containsHashed)
        {
            Dictionary<ProfileActionType, List<string>> actionRoles = new Dictionary<ProfileActionType, List<string>>();
            actionRoles.Add(ProfileActionType.Add, new List<string> { "ADM", "CRA", "CCO", "PRO", "TL" });
            actionRoles.Add(ProfileActionType.Edit, new List<string> { "ADM", "CRA", "CCO", "PRO", "TL" });
            actionRoles.Add(ProfileActionType.View, new List<string> { "ADM", "CRA", "CCO", "CCM", "TL", "PRO", "MGT", "HR" });
            actionRoles.Add(ProfileActionType.Preview, new List<string> { "ADM", "CRA", "CCO", "CCM", "TL", "PRO", "MGT", "HR" });

            if (containsHashed && currentAction != ProfileActionType.View)
                actionRoles[currentAction].Remove("TL");
            else if (containsHashed && currentAction == ProfileActionType.Preview)
            {
                actionRoles[ProfileActionType.Add].Remove("TL");
                return actionRoles[ProfileActionType.Add];
            }                

            return actionRoles[currentAction];
        }
    }
}