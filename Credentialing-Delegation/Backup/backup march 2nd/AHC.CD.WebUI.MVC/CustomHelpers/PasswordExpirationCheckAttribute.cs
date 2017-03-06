using AHC.CD.Business.Users;
using AHC.CD.Data.ADO.AspnetUser;
using AHC.CD.WebUI.MVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AHC.CD.WebUI.MVC.CustomHelpers
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class PasswordExpirationCheckAttribute : AuthorizeAttribute
    {
        private int _maxPasswordAgeInDay = int.Parse(ConfigurationManager.AppSettings["MaximumPasswordAge"]);
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.ActionDescriptor.IsDefined(typeof(SkipPasswordExpirationCheckAttribute), inherit: true)
                && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipPasswordExpirationCheckAttribute), inherit: true))
            {
                if (_maxPasswordAgeInDay != int.MinValue)
                {
                    IPrincipal user = filterContext.HttpContext.User;
                    ApplicationUserManager userManager = filterContext.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    if (user != null && user.Identity.IsAuthenticated)
                    {
                        var appUser = userManager.FindByName(user.Identity.Name);
                        var daysLeft = PasswordHistoryLastUpdated(appUser);
                        if (daysLeft==null)
                        {
                            return;
                        }
                        if (daysLeft >= _maxPasswordAgeInDay)
                        {
                            HttpContext httpContext = HttpContext.Current;
                            HttpContextBase httpContextBase = new HttpContextWrapper(httpContext);
                            RouteData routeData = new RouteData();
                            RequestContext requestContext = new RequestContext(httpContextBase, routeData);
                            UrlHelper urlHelper = new UrlHelper(requestContext);

                            filterContext.HttpContext.Response.Redirect(urlHelper.Action("ChangePassword", "Manage"));
                        }
                    }
                }
            }
            
            base.OnAuthorization(filterContext);
        }

        private int? PasswordHistoryLastUpdated(ApplicationUser appUser)
        {
            var temp = appUser.UserUsedPassword.LastOrDefault();
            if (temp.CreatedDate!=null)
            {
                return (DateTime.Today - temp.CreatedDate.Date).Days;
            }
            return null;
        }
    }
}