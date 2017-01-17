using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class CMSDataController : Controller
    {
        //
        // GET: /CMS/CMSData/

        public PartialViewResult CMSIndex()
        {
            ViewBag.TableData = null;
            ViewBag.Code = null;
            ViewBag.Title = "CMS " + CMS_Resources.Content.List;

            using (StreamReader r = new StreamReader(Server.MapPath("~/Areas/CMS/Resources/JSON Data/CMSAllList.js")))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject(json);
                ViewBag.TableData = items;
            }

            //foreach (var code in  ViewBag.TableData.data)
            //{
            //      if (code.Category=="Code")
            //        {
                        
            //            ViewBag.Code = code;
            //        }
            //}
           

            //XmlDocument doc = new XmlDocument();
            //doc.Load(Server.MapPath("~/Areas/CMS/AppResource/Route.config.xml"));

            //ICMSEntityServices cmsList = new CMSEntityServices();

            //ViewBag.TableData = cmsList.GetAllCMSEntity();


            //foreach (var link in ViewBag.TableData)
            //{
            //    var clickLink = "";

            //    //Split sentences if a Capital letter appears in string
            //    if (string.IsNullOrWhiteSpace(link.EntityName))
            //        return null;
            //    StringBuilder newText = new StringBuilder(link.EntityName.Length * 2);
            //    newText.Append(link.EntityName[0]);
            //    for (int i = 1; i < link.EntityName.Length; i++)
            //    {
            //        if (char.IsUpper(link.EntityName[i]) && link.EntityName[i - 1] != ' ')
            //            newText.Append(' ');
            //        newText.Append(link.EntityName[i]);
            //    }
            //    link.Name= newText.ToString();

            //    try
            //    {
            //        clickLink = doc.SelectSingleNode("root/cmsitem[@module='" + link.EntityName.ToLower() + "']").InnerText;
            //        link.ClickLink = clickLink;
            //    }
            //    catch (Exception e)
            //    {
            //        continue;
            //    }

            //}

            CMSList model = new CMSList();

            return PartialView("~/Areas/CMS/Views/CMSData/Index.cshtml", model);
        }
    }
}