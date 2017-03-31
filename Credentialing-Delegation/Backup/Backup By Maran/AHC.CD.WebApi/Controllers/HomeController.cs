using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AHC.CD.WebApi.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public String Index()
        {
            return "All good";
        }
    }
}
