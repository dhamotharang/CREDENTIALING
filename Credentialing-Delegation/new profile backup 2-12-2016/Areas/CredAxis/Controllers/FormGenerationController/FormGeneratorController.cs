using Newtonsoft.Json;
using PortalTemplate.Areas.CredAxis.Models.ProviderviewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers.PackageGeneratorController
{
    public class FormGeneratorController : Controller
    {
        //
        // GET: /CredAxis/PackegeGenerator/

        // Added By Manideep Innamuri
        // Controller for Form Generator

        //Main method that calls the main View of Form Generation
        public ActionResult Index()
        {
            List<ProviderSearchResultViewModel> SearchResultData;
            {
                string file = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/ProviderData/Providerdata.json");
                string json = System.IO.File.ReadAllText(file);

                SearchResultData = JsonConvert.DeserializeObject<List<ProviderSearchResultViewModel>>(json);

            }

            return PartialView("~/Areas/CredAxis/Views/FormGeneration/_FormGenerationIndex.cshtml", SearchResultData);
        }     

        //Method to delete the Wrongly selected Providers

        //public ActionResult RemoveUnwantedProvider(ProviderSearchResultViewModel ProviderSearchViewModel)
        //{

        //}
   

	}
}