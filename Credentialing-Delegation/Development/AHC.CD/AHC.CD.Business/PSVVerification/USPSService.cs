using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AHC.CD.Business.PSVVerification
{
    public class USPSService
    {
        public string ValidateLocationDetails()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"E:\New folder (3)\Credentialing-Delegation\Development\AHC.CD\AHC.CD.WebUI.MVC\ApplicationDocument\USPSServices");
            string rawXml = doc.OuterXml;
            var result = GetLocationDataFromUSPS("http://testing.shippingapis.com/ShippingAPITest.dll?API=Verify&XML=", rawXml);
            return result;
        }

        /// <summary>
        /// Getting Response from USPS API 
        /// </summary>
        /// <param name="destinationUrl"></param>
        /// <param name="requestXml"></param>
        /// <returns></returns>
        private string GetLocationDataFromUSPS(string destinationUrl, string requestXml)
        {
            string url = String.Concat(destinationUrl, requestXml);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "GET";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    return responseStr;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
