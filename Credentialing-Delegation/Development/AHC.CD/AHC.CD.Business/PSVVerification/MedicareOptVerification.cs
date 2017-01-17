using AHC.UtilityService;
using NReco.PhantomJS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AHC.CD.Business.PSVVerification
{
    public class MedicareOptVerification
    {
        public string VerifyMedicareOpt(string NPINumber)
        {
            string NewFileName = "";
            var phantomJS = new PhantomJS();
            try
            {
                string script = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Scripts/Custom/PSVVerification/MedicareOpt.js"));
                script = script.Replace("[ProviderNPINumber]", NPINumber);
                NewFileName = "MedicareOpt-" + UniqueKeyGenerator.GetUniqueKey() + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Second.ToString() + ".png";
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

        private bool IsBase64String(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            try
            {
                Convert.FromBase64String(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}
