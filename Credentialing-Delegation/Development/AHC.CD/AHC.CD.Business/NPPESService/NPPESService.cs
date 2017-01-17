using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.NPPESService
{
    public class NPPESService
    {
        //param(case sensitive)     actual meaning
        //number                --  NPI number
        //enumeration_type      --  NPI1 or NPI2
        //taxonomy_description  --  EX :- "Family Medicine Sleep Medicine"
        //first_name            --  First Name
        //last_name             --  Last Name
        //organization_name     --  Organization Name
        //address_purpose       --  Location
        //city                  --  City
        //state                 --  State
        //postal_code           --  Postal Code
        //country_code          --  Country Code
        //limit                 --  Results Limit
        //skip                  --  Skip First N Results
        //pretty                --  Response will be formatted with indented lines

        // use the above parameters to get data from NPI registry.Parameters are case sensitive.
        public string GetProviderDataFromNPI(string key, string value)
        {
            try
            {
                string url = "https://npiregistry.cms.hhs.gov/api?";
                string FinalUrl = String.Concat(url, key + "=" + value + "&pretty=true");
                string responseStr = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(FinalUrl);
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    responseStr = new StreamReader(responseStream).ReadToEnd();
                }
                return responseStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
