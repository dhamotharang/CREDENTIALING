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
    public class OIGVerification
    {
        public string VerifyOIG(string FirstName,string MiddleName,string LastName,string DOB,string NPI,string UPIN)
        {
            
            string NewFileName = "";
            var phantomJS = new PhantomJS();
            var json = new JavaScriptSerializer();
            var inputData = json.Serialize(new[] {
                new OIGDTOForPsvVerification() { FirstName = FirstName, LastName = LastName,MiddleName =MiddleName,DOB=DOB}
            });
            phantomJS.ErrorReceived += (sender, e) =>
            {
                Console.WriteLine("PhantomJS error: {0}", e.Data);
            };
            var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputData + "\n"));
            try
            {
                var script = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Scripts/Custom/PSVVerification/OIGVerification.js"));
                NewFileName = "OIG-" + UniqueKeyGenerator.GetUniqueKey() + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Second.ToString() + ".png";
                var res = HttpContext.Current.Server.MapPath("~/ApplicationDocuments/PSVDocuments/" + NewFileName).Replace("\\", "/");
                script = script.Replace("[DestinationFolder]", res);
                phantomJS.RunScript(script, null, inputStream, null);
            }
            catch (Exception)
            {
                throw;
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

    public class OIGDTOForPsvVerification
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string NPI { get; set; }
        public string UPIN { get; set; }
    }
}
