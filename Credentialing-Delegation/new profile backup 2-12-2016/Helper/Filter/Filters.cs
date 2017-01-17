using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using System.Xml.Linq;

namespace PortalTemplate.Helper.Filter
{
    public class IncompatibleBrowser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool IncomatibleBrowser = false;
            List<String> CompatibleBrowser = new List<string>();

            XmlDocument xml = new XmlDocument();
            xml.Load(System.Web.HttpContext.Current.Server.MapPath("~/App_Config/ClientsDetail.xml"));
            XmlNodeList resources = xml.SelectNodes("data/client");
            Dictionary<string, double> Clients = new Dictionary<string, double>();
            foreach (XmlNode node in resources)
            {
                CompatibleBrowser.Add(node.Attributes["clientname"].Value);
                Clients.Add(node.Attributes["clientname"].Value, Double.Parse(node.InnerText));
            }

            //Check the Browser
            var filterBrowser = filterContext.RequestContext.HttpContext.Request.Browser;

            if (filterContext.ActionDescriptor.ActionName != "IncompatibleBrowser" &&  !CompatibleBrowser.Contains(filterBrowser.Browser.Trim()))
            {
                IncomatibleBrowser = true;
            }

            else if (filterContext.ActionDescriptor.ActionName != "IncompatibleBrowser" && Convert.ToDouble(filterBrowser.Version) < Clients[filterBrowser.Browser.Trim().ToString()])
            {
                IncomatibleBrowser = true;
            }

            if (IncomatibleBrowser)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary 
                    {
                        { "controller", "ErrorPage" },
                        { "action", "IncompatibleBrowser"} 
                    });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}