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

namespace AHC.CD.WebUI.MVC.CustomHelpers
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class PasswordExpirationCheckAttribute : AuthorizeAttribute
    {
        private int _maxPasswordAgeInDay = int.Parse(ConfigurationManager.AppSettings["MaximumPasswordAge"]);
        IPasswordHistory iPasswordHistory = null;
        public PasswordExpirationCheckAttribute()
        {
            this.iPasswordHistory = new PasswordHistory(new UserDetails());
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if ((filterContext.ActionDescriptor.ActionName == "ChangePassword" && filterContext.HttpContext.Request.RequestType == "POST") || filterContext.ActionDescriptor.ActionName == "LogOff" && filterContext.HttpContext.Request.RequestType == "POST")
            {                
                return;
            }
            if (!filterContext.ActionDescriptor.IsDefined(typeof(SkipPasswordExpirationCheckAttribute), inherit: true)
                && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipPasswordExpirationCheckAttribute), inherit: true))
            {
                if (_maxPasswordAgeInDay != int.MinValue)
                {
                    IPrincipal user = filterContext.HttpContext.User;


                    if (user != null && user.Identity.IsAuthenticated)
                    {
                        var daysLeft = iPasswordHistory.PasswordHistoryLastUpdated(user.Identity.Name);
                        if (daysLeft == null)
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
    }
}