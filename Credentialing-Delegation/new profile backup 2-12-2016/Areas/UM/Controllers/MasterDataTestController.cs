using PortalTemplate.Areas.UM.Models.MasterDataEntities;
using PortalTemplate.Areas.UM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.UM.Controllers
{
    public class MasterDataTestController : Controller
    {
        //
        // GET: /UM/MasterDataTest/
        public List<ContactTypeViewModel> GetTestData()
        {
            //Test Action to see Master Data's
            return new MasterDataService().GetContactTypes();
        }
	}
}