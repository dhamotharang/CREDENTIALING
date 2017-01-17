using AHC.UtilityService;
using NReco.PhantomJS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace AHC.CD.Business.PSVVerification
{
    public class DEAVerification
    {
        public string VerifyFederalDEA(string DEANumber, string LastName, string SSN, string TaxID)
        {
            string NewFileName = "";
            var json = new JavaScriptSerializer();
            var phantomJS = new PhantomJS();
            var inputData = json.Serialize(new[] {
				new FederalDeaDTOForPsvVerification() { DEA_Number = DEANumber, Last_Name = LastName, SSN = SSN ,TAX_ID=TaxID},
			});
            var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputData + "\n"));
            try
            {
                string script = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Scripts/Custom/PSVVerification/FederalDEA.js"));
                NewFileName = "FederalDEA-" + UniqueKeyGenerator.GetUniqueKey() + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Second.ToString() + ".png";
                var res = HttpContext.Current.Server.MapPath("~/ApplicationDocuments/PSVDocuments/" + NewFileName).Replace("\\", "/");
                script = script.Replace("[DestinationFolder]", res);
                phantomJS.RunScript(script, null, null, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }   
            finally
            {
                phantomJS.Abort(); // ensure that phantomjs.exe is stopped
            }
            return "/ApplicationDocuments/PSVDocuments/" + NewFileName;
        }
    }

    public class FederalDeaDTOForPsvVerification
    {
        public string DEA_Number { get; set; }
        public string Last_Name { get; set; }
        public string SSN { get; set; }
        public string TAX_ID { get; set; }
    }
}
