using AHC.CD.WebUI.MVC.Models.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.Utility.Graph
{

    public class GraphDataTransformer
    {

       public static List<ProviderTypeGraphModel> TransformProviderTypes(Dictionary<string, int> dbProviderData){

            List<ProviderTypeGraphModel> providerTypes=new List<ProviderTypeGraphModel>();

            foreach (KeyValuePair<string, int> entry in dbProviderData)
            {

                providerTypes.Add(new ProviderTypeGraphModel { label = entry.Key, value = entry.Value });
            }


            return providerTypes;

        }


    }
}