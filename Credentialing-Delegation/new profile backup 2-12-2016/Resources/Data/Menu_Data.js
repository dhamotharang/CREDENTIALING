/// <reference path="../../Views/PBAS/CreateEncounter/_EditEncounter.cshtml" />
/// <reference path="../../Views/PBAS/EncounterList/_EncounterList.cshtml" />
/// <reference path="../../Views/PBAS/EncounterList/_EncounterList.cshtml" />
/// <reference path="../../Views/PBAS/EncounterList/_EncounterList.cshtml" />
var menudata = [{
    "Role": "SuperAdmin",
    "RoleMenu":
	[
       
  {
      "IconClass": "fa fa-money",
      "Heading": "Billing",
      "childMenu": [
            {
                "Action": "Claims",
                "Title": "Home",
                "TabTitle": "Home",
                "Path": "~/Areas/Billing/Views/ClaimsTracking/Index.cshtml",
                "Container": "fullBodyContainer",
                "AutoFlush": true,
                "FloatMenu": false,
                "IsSubTab": false,
                "DefaultSubTab": "",
                "HasSubTab": false
            },
            {
                "Action": "Files",
                "Title": "Files",
                "TabTitle": "Files",
                "Path": "/Billing/FileManagement/Index",
                "Container": "fullBodyContainer",
                "AutoFlush": false,
                "FloatMenu": false,
                "IsSubTab": false,
                "DefaultSubTab": "",
                "HasSubTab": false
            },
             //{
             //    "Action": "Reports",
             //    "Title": "Reports",
             //    "TabTitle":"EOB",
             //    "Path": "~/Views/EOBReport/Index.cshtml",
             //    "Container": "fullBodyContainer",
             //    "AutoFlush": false,
             //    "FloatMenu": true,
             //    "FloatMenuPath": "~/Views/Shared/ClaimsFloatMenu/_ReportFloatMenu.cshtml",
             //    "IsSubTab": false,
             //    "DefaultSubTab": "",
             //    "HasSubTab": false
             //},
             {
                 "Action": "Reports",
                 "Title": "Reports",
                 "TabTitle": "Reports",
                 "Path": "~/Areas/Billing/Views/EOBReport/_IndexReports1.cshtml",
                 "Container": "fullBodyContainer",
                 "AutoFlush": false,
                 "FloatMenu": false,
                 "IsSubTab": false,
                 "DefaultSubTab": "",
                 "HasSubTab": false
             },  
            //{
            //    "Action": "Settings",
            //    "Title": "Settings",
            //    "TabTitle":"Clearing House",
            //    "Path": "~/Views/ClearingHouse/Index.cshtml",
            //    "Container": "fullBodyContainer",
            //    "AutoFlush": false,
            //    "FloatMenu": false,
            //    "FloatMenu": true,
            //     "FloatMenuPath": "~/Views/Shared/ClaimsFloatMenu/_settingsFloatMenu.cshtml",
            //    "IsSubTab": false,
            //    "DefaultSubTab": "",
            //    "HasSubTab": false
            //},
             {
                 "Action": "Settings",
                 "Title": "Settings",
                 "TabTitle": "Settings",
                 "Path": "/Billing/ClearingHouse/GetClearingHouseList",
                 "Container": "fullBodyContainer",
                 "AutoFlush": false,
                 "FloatMenu": false,
                 "IsSubTab": false,
                 "DefaultSubTab": "",
                 "HasSubTab": false
             }
      ],
      "collapsedLabel": "Billing"
  }]
    }

];


