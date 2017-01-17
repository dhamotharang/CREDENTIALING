using Newtonsoft.Json;
using PortalTemplate.Models.Claims_Dashboard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Controllers
{
    public class ClaimsDashboardController : Controller
    {
        List<FilterHistory> filterHistories = new List<FilterHistory>();



        public ClaimsDashboardController(){
         List<FilterElement> filterElements = new List<FilterElement>();

            FilterElement filterEle = new FilterElement();

            List<string> on;

            on = new List<string>();
            filterEle.By = "Payer";
            on.Add("Access2");
            on.Add("Access 2 Tempa");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Billing Provider";
            on.Add("Dina Amundsen");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Rendering Provider";
            on.Add("Aasma Riaz");
            on.Add("Rajyalakshmi Kolli");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Claim Type";
            on.Add("CAP");
            on.Add("FFS");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "DOS From";
            on.Add("02-25-2016");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "DOS To";
            on.Add("02-25-2016");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Billing Team";
            on.Add("Access2");
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);


            filterHistories.Add(new FilterHistory {ID=1, DivId = "filter_query_1", FilterName = "My Favorite Filter 1", Elements = filterElements });


            filterElements = new List<FilterElement>();

            filterEle = new FilterElement();

            on = new List<string>();
            filterEle.By = "Payer";
            on.Add("Access2");
            on.Add("Access 2 Tampa");
            on.Add("All American physician");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Billing Provider";
            on.Add("Dina Amundsen");
            on.Add("Sheila Trissel");
            on.Add("Tony Martin");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Rendering Provider";
            on.Add("Aasma Riaz");
            on.Add("Rajyalakshmi Kolli");
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Claim Type";
            on.Add("CAP");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "DOS From";
            on.Add("04-25-2016");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "DOS To";
            on.Add("08-25-2016");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Billing Team";
            on.Add("Access2");
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Biller";
            on.Add("Access2");
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);

            filterHistories.Add(new FilterHistory { ID = 2, DivId = "filter_query_2", FilterName = "My Favorite Filter 2", Elements = filterElements });


            filterElements = new List<FilterElement>();

            filterEle = new FilterElement();

            on = new List<string>();
            filterEle.By = "Payer";
            on.Add("All American physician");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Billing Provider";
            on.Add("Dina Amundsen");
            on.Add("Sheila Trissel");
            on.Add("Tony Martin");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Rendering Provider";
            on.Add("Aasma Riaz");
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Claim Type";
            on.Add("CAP");
            on.Add("FFS");
            on.Add("UB04");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "DOS From";
            on.Add("10-25-2016");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "DOS To";
            on.Add("10-25-2016");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Billing Team";
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Biller";
            on.Add("Access2");
            filterEle.On = on;
            filterElements.Add(filterEle);


            filterHistories.Add(new FilterHistory { ID = 3, DivId = "filter_query_3", FilterName = "My Favorite Filter 3", Elements = filterElements });

            filterElements = new List<FilterElement>();

            filterEle = new FilterElement();

            on = new List<string>();
            filterEle.By = "Payer";
            on.Add("All American physician");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Billing Provider";
            on.Add("Dina Amundsen");
            on.Add("Sheila Trissel");
            on.Add("Tony Martin");
            filterEle.On = on;
            filterElements.Add(filterEle);


            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Claim Type";
            on.Add("CAP");
            on.Add("FFS");
            on.Add("UB04");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "DOS From";
            on.Add("10-25-2016");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "DOS To";
            on.Add("10-25-2016");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Billing Team";
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);


            filterHistories.Add(new FilterHistory { ID = 4, DivId = "filter_query_4", FilterName = "My Favorite Filter 4", Elements = filterElements });

            filterElements = new List<FilterElement>();

            filterEle = new FilterElement();

            on = new List<string>();
            filterEle.By = "Payer";
            on.Add("Access2");
            on.Add("Access 2 Tampa");
            on.Add("All American physician");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Billing Provider";
            on.Add("Dina Amundsen");
            on.Add("Tony Martin");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Rendering Provider";
            on.Add("Rajyalakshmi Kolli");
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "DOS From";
            on.Add("04-25-2016");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "DOS To";
            on.Add("08-25-2016");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Billing Team";
            on.Add("Access2");
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Biller";
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);

            filterHistories.Add(new FilterHistory { ID = 5, DivId = "filter_query_5", FilterName = "My Favorite Filter 5", Elements = filterElements });

            filterElements = new List<FilterElement>();

            filterEle = new FilterElement();

            on = new List<string>();
            filterEle.By = "Payer";
            on.Add("Access2");
            on.Add("Access 2 Tampa");
            on.Add("All American physician");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Billing Provider";
            on.Add("Dina Amundsen");
            on.Add("Tony Martin");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Rendering Provider";
            on.Add("Aasma Riaz");
            on.Add("Rajyalakshmi Kolli");
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Claim Type";
            on.Add("CAP");
            on.Add("FFS");
            filterEle.On = on;
            filterElements.Add(filterEle);


            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Billing Team";
            on.Add("Access2");
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Biller";
            on.Add("Access2");
            filterEle.On = on;
            filterElements.Add(filterEle);


            filterHistories.Add(new FilterHistory { ID = 6, DivId = "filter_query_6", FilterName = "My Favorite Filter 6", Elements = filterElements });

            filterElements = new List<FilterElement>();

            filterEle = new FilterElement();

            on = new List<string>();
            filterEle.By = "Payer";
            on.Add("Access2");
            on.Add("Access 2 Tampa");
            on.Add("All American physician");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Billing Provider";
            on.Add("Dina Amundsen");
            on.Add("Sheila Trissel");
            on.Add("Tony Martin");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Rendering Provider";
            on.Add("Aasma Riaz");
            on.Add("Rajyalakshmi Kolli");
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Claim Type";
            on.Add("CAP");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "DOS From";
            on.Add("04-25-2016");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "DOS To";
            on.Add("08-25-2016");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Billing Team";
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);

            on = new List<string>();
            filterEle = new FilterElement();
            filterEle.By = "Biller";
            on.Add("Access2");
            on.Add("Gary Merlino");
            filterEle.On = on;
            filterElements.Add(filterEle);

            filterHistories.Add(new FilterHistory { ID = 7, DivId = "filter_query_7", FilterName = "My Favorite Filter 7", Elements = filterElements });
        }


   
      
        //
        // GET: /ClaimsDashboard/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FlatIndex()
        {
            return View();
        }


        public ActionResult GetProcedureClaimsReport(int type)
        {
            List<CPT> cpts = new List<CPT>();
            if (type == 1)
            {
                cpts.Add(new CPT { Id = 1, Code = "97814", Count = "9535", Cost = "40000", Description = "Acupunct w/s timul addl 15 m" });
                cpts.Add(new CPT { Id = 2, Code = "95504", Count = "8000", Cost = "35214", Description = "Percute allergy skin test" });
                cpts.Add(new CPT { Id = 3, Code = "95010", Count = "8001", Cost = "43545", Description = "Percute allergy titrate test" });
                cpts.Add(new CPT { Id = 4, Code = "95028", Count = "7856", Cost = "25344", Description = "Id allergy test delayed type" });
                cpts.Add(new CPT { Id = 5, Code = "95044", Count = "7589", Cost = "353", Description = "Allergy patch test" });
                cpts.Add(new CPT { Id = 6, Code = "95056", Count = "7495", Cost = "353", Description = "Photo Sensitivity Test" });
                cpts.Add(new CPT { Id = 7, Code = "95078", Count = "7487", Cost = "4356", Description = "NO Longer Valid-07 Provocative Test" });
                cpts.Add(new CPT { Id = 8, Code = "95024", Count = "7123", Cost = "34534", Description = "Id Allergy Test" });
                cpts.Add(new CPT { Id = 9, Code = "97811", Count = "7025", Cost = "353", Description = "Acupunct w/s timul addl 15 m" });
                cpts.Add(new CPT { Id = 10, Code = "95074", Count = "6985", Cost = "4534", Description = "Ingestion Challenge test" });

            }
            else {
                cpts.Add(new CPT { Id = 11, Code = "95028", Count = "7856", Cost = "25344", Description = "Id allergy test delayed type" });
                cpts.Add(new CPT { Id = 12, Code = "95044", Count = "7589", Cost = "353", Description = "Allergy patch test" });
                cpts.Add(new CPT { Id = 13, Code = "95056", Count = "7495", Cost = "353", Description = "Photo Sensitivity Test" });
                cpts.Add(new CPT { Id = 14, Code = "95078", Count = "7487", Cost = "4356", Description = "NO Longer Valid-07 Provocative Test" });
                cpts.Add(new CPT { Id = 15, Code = "95024", Count = "7123", Cost = "34534", Description = "Id Allergy Test" });
                cpts.Add(new CPT { Id = 16, Code = "97811", Count = "7025", Cost = "353", Description = "Acupunct w/s timul addl 15 m" });
                cpts.Add(new CPT { Id = 17, Code = "95074", Count = "6985", Cost = "4534", Description = "Ingestion Challenge test" });
                cpts.Add(new CPT { Id = 18, Code = "97814", Count = "9535", Cost = "40000", Description = "Acupunct w/s timul addl 15 m" });
                cpts.Add(new CPT { Id = 19, Code = "95504", Count = "8000", Cost = "35214", Description = "Percute allergy skin test" });
                cpts.Add(new CPT { Id = 20, Code = "95010", Count = "8001", Cost = "43545", Description = "Percute allergy titrate test" });
               
            
            }
          
            return PartialView("_ProcedureClaimedReport", cpts);
        }



        public ActionResult GetAdjustmentCodeReport(int type)
        {
            List<Adjustment> adjustment = new List<Adjustment>();
            if (type == 1)
            {
                adjustment.Add(new Adjustment { Id = 1, Code = "2079", Count = "9535", Cost = "40000", Description = "Inc/exc/destr in ear NEC" });
                adjustment.Add(new Adjustment { Id = 2, Code = "5592", Count = "8000", Cost = "35214", Description = "Percutan renal aspirat" });
                adjustment.Add(new Adjustment { Id = 3, Code = "9787", Count = "8001", Cost = "43545", Description = "Remov trunk device NEC" });
                adjustment.Add(new Adjustment { Id = 4, Code = "3555", Count = "7856", Cost = "25344", Description = "Pros rep ventrc def-clos" });
                adjustment.Add(new Adjustment { Id = 5, Code = "0398", Count = "7589", Cost = "353", Description = "Remove spine theca shunt" });
                adjustment.Add(new Adjustment { Id = 6, Code = "8956", Count = "7495", Cost = "353", Description = "Carotid pulse tracing" });
                adjustment.Add(new Adjustment { Id = 7, Code = "1286", Count = "7487", Cost = "4356", Description = "Rep scler staphyloma NEC" });
                adjustment.Add(new Adjustment { Id = 8, Code = "6595", Count = "7123", Cost = "34534", Description = "Ovarian torsion release" });
                adjustment.Add(new Adjustment { Id = 9, Code = "1426", Count = "7025", Cost = "353", Description = "Chorioret les radiother" });
                adjustment.Add(new Adjustment { Id = 10, Code = "9356", Count = "6985", Cost = "4534", Description = "Pressure dressing applic" });

            }
            else
            {
                adjustment.Add(new Adjustment { Id = 5, Code = "0398", Count = "7589", Cost = "353", Description = "Remove spine theca shunt" });
                adjustment.Add(new Adjustment { Id = 6, Code = "8956", Count = "7495", Cost = "353", Description = "Carotid pulse tracing" });
                adjustment.Add(new Adjustment { Id = 1, Code = "2079", Count = "9535", Cost = "40000", Description = "Inc/exc/destr in ear NEC" });
                adjustment.Add(new Adjustment { Id = 2, Code = "5592", Count = "8000", Cost = "35214", Description = "Percutan renal aspirat" });
                adjustment.Add(new Adjustment { Id = 3, Code = "9787", Count = "8001", Cost = "43545", Description = "Remov trunk device NEC" });
                adjustment.Add(new Adjustment { Id = 7, Code = "1286", Count = "7487", Cost = "4356", Description = "Rep scler staphyloma NEC" });
                adjustment.Add(new Adjustment { Id = 8, Code = "6595", Count = "7123", Cost = "34534", Description = "Ovarian torsion release" });
                adjustment.Add(new Adjustment { Id = 9, Code = "1426", Count = "7025", Cost = "353", Description = "Chorioret les radiother" });
                adjustment.Add(new Adjustment { Id = 10, Code = "9356", Count = "6985", Cost = "4534", Description = "Pressure dressing applic" });
                adjustment.Add(new Adjustment { Id = 4, Code = "3555", Count = "7856", Cost = "25344", Description = "Pros rep ventrc def-clos" });
                

            }

            return PartialView("_PenaltyAdjustmentReport", adjustment);
        }



        public ActionResult GetPartialView(string url)
        {
            return PartialView(url);
        }


        //This Method Gets the JSON Data from JSON Files for the Authorization Summary Charts and Tables
        //Receives Arguments for Pending/Submitted/Completed Authorizations and returns required
        public object GetData(string input)
        {
            try
            {
                String path;
                if (input == "OMT1")
                {
                    path = HttpContext.Server.MapPath("~/Resources/Data/ClaimsDashboard/OverallMT1.json");
                }
                else if (input == "OMT2")
                {
                    path = HttpContext.Server.MapPath("~/Resources/Data/ClaimsDashboard/OverallMT2.json");
                }
                else if (input == "HCC1")
                {
                    path = HttpContext.Server.MapPath("~/Resources/Data/ClaimsDashboard/HighCostClaim1.json");
                }
                else if (input == "HCC2")
                {
                    path = HttpContext.Server.MapPath("~/Resources/Data/ClaimsDashboard/HighCostClaim2.json");
                }
                else if (input == "PC1")
                {
                    path = HttpContext.Server.MapPath("~/Resources/Data/ClaimsDashboard/ProceduresClaimedDetailedReport1.json");
                }
                else if (input == "PC2")
                {
                    path = HttpContext.Server.MapPath("~/Resources/Data/ClaimsDashboard/ProceduresClaimedDetailedReport2.json");
                }
                else if (input == "RDR1")
                {
                    path = HttpContext.Server.MapPath("~/Resources/Data/ClaimsDashboard/RejectionsDR1.json");
                }
                else if (input == "RDR2")
                {
                    path = HttpContext.Server.MapPath("~/Resources/Data/ClaimsDashboard/RejectionsDR2.json");
                }
                else if (input == "IMD1")
                {
                    path = HttpContext.Server.MapPath("~/Resources/Data/ClaimsDashboard/InternalMeanDR1.json");
                }
                else if (input == "IMD2")
                {
                    path = HttpContext.Server.MapPath("~/Resources/Data/ClaimsDashboard/InternalMeanDR2.json");
                }
                else if (input == "MCD1")
                {
                    path = HttpContext.Server.MapPath("~/Resources/Data/ClaimsDashboard/MostCommonDiseasesDR1.json");
                }
                else if (input == "MCD2")
                {
                    path = HttpContext.Server.MapPath("~/Resources/Data/ClaimsDashboard/MostCommonDiseasesDR2.json");
                }
                else
                {
                    path = HttpContext.Server.MapPath("~/Resources/JSONData/ChartsDataPending.json");
                }

                using (TextReader reader = System.IO.File.OpenText(path))
                {
                    string text = reader.ReadToEnd();
                    var data = JsonConvert.DeserializeObject(text);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public ActionResult GetFilterHistory()
        {
           
            return PartialView("~/Views/ClaimsDashboard/_FilterHistory.cshtml", filterHistories);
        }



        public ActionResult GetFilterHistoryForDetailedReport()
        {

            return PartialView("~/Views/ClaimsDashboard/_FilterHistoryForDetailedReport.cshtml", filterHistories);
        }



        public ActionResult GetSelectedFavFilter(int id)
        {
            FilterHistory filterHistory = new FilterHistory();
            foreach (var item in filterHistories)
            {
                if (item.ID == id) {
                    filterHistory = item;
                    break;
                }   
            }


            return PartialView("~/Views/ClaimsDashboard/_SelectedFilterHistory.cshtml",filterHistory);
        }

    }
}