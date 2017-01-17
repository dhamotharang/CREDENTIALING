using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Configuration;
using Newtonsoft.Json;


namespace PortalTemplate.Areas.MH.Common
{
    public class CommonMethods
    {
        /// <summary>This Method is Used to Get the JSON Data.</summary>
        /// <param name="SourceFile">Used to indicate SourceFile of JSON Data.</param>
        /// <returns>Returns JSON Data as string.</returns>
        public string GetJSONData(string SourceFile)
        {
            string file = HostingEnvironment.MapPath(GetResourceLink(SourceFile));
            string json = System.IO.File.ReadAllText(file);
            return json;
        }

        public string GetResourceLink(string SourceFileName)
        {
            return "~/Areas/MH/Resources/ServiceData/Member/" + SourceFileName;
        }

        /// <summary>This Method is Used to Get the Filtered JSON Data for SearchWithDropdown.</summary>
        // Multiple parameters.
        /// <param name="searchTerm">User Search Term</param>
        /// <param name="dataUrl">Used to specify the JSON Data Url.</param>
        /// <param name="property">Used to specify the Property of Value Searched.</param>
        /// <returns>Returns Json Data.</returns>
        public dynamic GetMasterDataFromJson<T>(string searchTerm, string dataUrl, string property)
        {
            string file = HttpContext.Current.Server.MapPath(dataUrl);
            string json = System.IO.File.ReadAllText(file);
            var PlansList = JsonConvert.DeserializeObject<List<T>>(json);
            var filteredPersonList = Filter(PlansList, searchTerm, property); //Generic Method for Data Filteration
            var filteredPersonListInJson = JsonConvert.SerializeObject(filteredPersonList);
            return filteredPersonListInJson;
        }

        /// <summary>This Method Filters the Data based on Search Term and Property.</summary>
        // Multiple parameters.
        /// <param name="planList">List of any View Model Type</param>
        /// <param name="searchTerm">User Search Term.</param>
        /// <param name="property">Used to specify the Property of Value Searched.</param>
        /// <returns>Returns List of Matched Data.</returns>
        [NonAction]
        public List<T> Filter<T>(List<T> data, string searchTerm, string property)
        {
            List<T> fliteredList = new List<T>();
            PropertyInfo propertyInfo = typeof(T).GetProperty(property);
            try
            {
                fliteredList = data.FindAll(x => x.GetType().GetProperty(property).GetValue(x).ToString().ToUpper().Contains(searchTerm.ToUpper()));
            }
            catch (Exception)
            {
                throw;
            }
            return fliteredList;
        } 
    }
}