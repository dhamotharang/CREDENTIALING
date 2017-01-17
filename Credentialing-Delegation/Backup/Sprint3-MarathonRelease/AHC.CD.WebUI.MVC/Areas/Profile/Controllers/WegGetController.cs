using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class WegGetController : ApiController
    {
        // GET: api/WegGet
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/WegGet/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/WegGet
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/WegGet/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WegGet/5
        public void Delete(int id)
        {
        }
    }
}
