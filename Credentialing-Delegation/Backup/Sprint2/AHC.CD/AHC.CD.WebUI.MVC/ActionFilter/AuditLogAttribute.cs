using AHC.ActivityLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.ActionFilter
{
    public class AuditLogAttribute : ActionFilterAttribute
    {
        
        
        
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            AuditMessage auditMessage = new AuditMessage();
            auditMessage.Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            auditMessage.Action = filterContext.ActionDescriptor.ActionName;
            auditMessage.DateTime = filterContext.HttpContext.Timestamp;
            auditMessage.User = filterContext.HttpContext.User.Identity.Name;
            auditMessage.IP = filterContext.HttpContext.Request.UserHostAddress;
            //auditMessage.Values = new List<ParameterValue>();


            //var parameters = filterContext.ActionDescriptor.GetParameters();
            //var values = parameters.Select(s => new ParameterValue
            //{
            //    Title = s.ParameterName,
            //    Value = filterContext.HttpContext.Request[s.ParameterName]
            //});

            //auditMessage.Values = values.ToList<ParameterValue>();
            IActivityLogger logger = ActivityLogFactory.Instance.GetActivityLogger();
            
            Task.Factory.StartNew(() =>
            {
                logger.Log(auditMessage);
            });
            base.OnActionExecuted(filterContext);
        }
   }
}