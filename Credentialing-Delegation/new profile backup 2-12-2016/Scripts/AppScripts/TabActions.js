///**********************************************************************/
///*|                   File: TabActions.js                            |*/
///*|                   Author: Adarsh Nanwani                         |*/
///*|                   Creation Date: August 5th, 2016                |*/
///**********************************************************************/

//var sampleAction = {
//    'ActionName': {
//        'Title': 'Home',                        // The Tab title for that action
//        "Path": "~/Views/Home/_Index.cshtml",   // Absolute path of the partial
//        "Container": "fullBodyContainer",       // 'id' of the <div> inside which partial is to be rendered
//        "AutoFlush": true,                      // To decide if backend call should be made for data or local data should be use. Set to "true" for backend call.
//        "FloatMenu": false,                     // To show or hide Sidebar Float Menu. Set to "true" to show.
//        "IsSubTab": false,                      // To determine whether this tab is a subtab.Set to "true" if Yes.
//        "DefaultSubTab": "",                    // Name of the Action which will be a subTab
//        "DefaultParentTab": ""                  // Name of the Action which will be a ParentTab
//    }
//};

//var Actions = {
//    "Intake Dashboard": {
//        "Title": "Intake Dashboard",
//        "Path": "~/Views/DashBoards/_IntakeDashboard.cshtml",
//        "Container": "fullBodyContainer",
//        "AutoFlush": true,
//        "FloatMenu": false,
//        "IsSubTab": false,
//        "DefaultSubTab": ""
//    },
//    "Superuser Dashboard": {
//        "Title": "Superuser Dashboard",
//        "Path": "~/Views/DashBoards/_ClaimsDashBoard.cshtml",
//        "Container": "fullBodyContainer",
//        "AutoFlush": true,
//        "FloatMenu": false,
//        "IsSubTab": false,
//        "DefaultSubTab": ""
//    },
//    "IntakeLead Dashboard": {
//        "Title": "IntakeLead Dashboard",
//        "Path": "~/Views/DashBoards/_IntakeLeadDashboard.cshtml",
//        "Container": "fullBodyContainer",
//        "AutoFlush": true,
//        "FloatMenu": true,
//        "IsSubTab": false,
//        "DefaultSubTab": ""
//    },
//    "PACLead Dashboard": {
//        "Title": "PACLead Dashboard",
//        "Path": "~/Views/PACLeadDashboard/Index.cshtml",
//        "Container": "fullBodyContainer",
//        "AutoFlush": true,
//        "FloatMenu": false,
//        "IsSubTab": false,
//        "DefaultSubTab": ""
//    },
//    "Claims Dashboard": {
//        "Title": "Claims Dashboard",
//        "Path": "~/Views/ClaimsDashboard/Index.cshtml",
//        "Container": "fullBodyContainer",
//        "AutoFlush": true,
//        "FloatMenu": false,
//        "IsSubTab": false,
//        "DefaultSubTab": ""
//    },
//    "Claims Tracking": {
//        "Title": "Claims Tracking",
//        "Path": "~/Views/EDITracking/Index.cshtml",
//        "Container": "fullBodyContainer",
//        "AutoFlush": true,
//        "FloatMenu": false,
//        "IsSubTab": false,
//        "DefaultSubTab": ""
//    },
//    "Member": {
//        "Title": "Member",
//        "Path": "",
//        "Container": "partialBodyContainer",
//        "AutoFlush": false,
//        "FloatMenu": true,
//        "IsSubTab": false,
//        "DefaultSubTab": "Profile"
//    },
//    "Profile": {
//        "Title": "Profile",
//        "Path": "~/Views/MemberProfile/_MemberProfile.cshtml",
//        "Container": "innerTabContainer",
//        "AutoFlush": true,
//        "FloatMenu": true,
//        "IsSubTab": true,
//        "DefaultSubTab": "",
//        "DefaultParentTab": "Member"
//    },
//    "Create New Auth": {
//        "Title": "Create New Auth",
//        "Path": "~/Views/Home/_AuthForm.cshtml",
//        "Container": "innerTabContainer",
//        "AutoFlush": false,
//        "FloatMenu": true,
//        "IsSubTab": true,
//        "DefaultSubTab": "",
//        "DefaultParentTab": "Member"
//    },
//    "Auth History": {
//        "Title": "Auth History",
//        "Path": "~/Areas/UM/Views/History/UMHistory/HistoryList/_UMAuthHistory.cshtml",
//        "Container": "innerTabContainer",
//        "AutoFlush": true,
//        "FloatMenu": true,
//        "IsSubTab": true,
//        "DefaultSubTab": "",
//        "DefaultParentTab": "Member"
//    },
//    "Facility Queue": {
//        "Title": "Facility Queue",
//        "Path": "~/Views/Queue/_Queue.cshtml",
//        "Container": "fullBodyContainer",
//        "AutoFlush": true,
//        "FloatMenu": false,
//        "IsSubTab": false,
//        "DefaultSubTab": ""
//    }
//};