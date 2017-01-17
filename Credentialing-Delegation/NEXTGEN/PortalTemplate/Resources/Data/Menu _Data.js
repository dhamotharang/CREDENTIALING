var menudata = [{
    "Role": "SuperAdmin",
    "RoleMenu":
	[
        {
            //"IconClass": "fa fa-home", "Heading": "Home", "childMenu": [{ "title": "Intake DashBoard", "href": "/DashBoards/IntakeDashboard" }, { "title": "Intake Lead Dashboard", "href": "/DashBoards/IntakeLeadDashboard" }, { "title": "UM Admin Dashboard", "href": "/DashBoards/UmDashboard" }, { "title": "Superuser DashBoard", "href": "/DashBoards/ClaimsDashboard" }, ],
            "IconClass": "fa fa-home",
            "Heading": "Home",
            "childMenu": [
            //{
            //    "action": "Intake Dashboard",
            //    "title": "Intake Dashboard",
            //    "href": "/DashBoards/IntakeDashboard"
            //},
            //{
            //    "action": "Superuser Dashboard",
            //    "title": "Superuser Dashboard",
            //    "href": "/DashBoards/ClaimsDashboard"
            //},
             {
                 "action": "PACLeadDashboard",
                 "title": "PAC Lead Dashboard",
                 "href": "/PACLeadDashboard/Index"
             },
            {
                "action": "IntakeLead Dashboard",
                "title": "Claims Dashboard",
                "href": "/ClaimsDashboard/Index"
            },
            {
                "action": "IntakeLead Dashboard",
                "title": "Flat Claims Dashboard",
                "href": "/ClaimsDashboard/FlatIndex"
            },
            {
                "title": "Claims Tracking",
                "href": "/EDITracking/Index"
            }],

            "collapsedLabel": "Home"
        },
  {
      "IconClass": "fa fa-user", "Heading": "Members", "childMenu": [
      { "title": "Home", "href": "#" },
      { "title": "Enroll Member", "href": "#" },
      { "title": "Profiles", "href": "#" },
      { "title": "Rosters", "href": "#" }
      ],
      "collapsedLabel": "Members"
  },
  {
      "IconClass": "fa fa-user-md",
      "Heading": "Providers",
      "childMenu": [{
          "title": "Home",
          "href": "#"
      },
      {
          "title": "Add Provider",
          "href": "/AddProvider/Index"
      },
      {
          "title": "Search",
          "href": "#"
      },
      {
          "title": "Profile Management & Credentialing",
          "href": "#"
      }],
      "collapsedLabel": "Providers"
  },
  {
      "IconClass": "fa fa-h-square",
      "Heading": "Utilization Management",
      "childMenu": [{
          "title": "Search",
          "href": "#"
      },
      {
          "title": "Authorizations",
          "href": "#"
      },
      {
          "title": "Queue",
          "href": "/Queue/Index"
      },
      {
          "title": "Reports",
          "href": "#"
      }],
      "collapsedLabel": "Utilization Management"
  },
  {
      "IconClass": "fa fa-heartbeat",
      "Heading": "Case Management",
      "childMenu": [{
          "title": "Search",
          "href": "#"
      },
      {
          "title": "Episodes",
          "href": "#"
      },
      {
          "title": "Queue",
          "href": "#"
      },
      {
          "title": "Reports",
          "href": "#"
      }],
      "collapsedLabel": "Case Management"
  },
  {
      "IconClass": "fa fa-stethoscope",
      "Heading": "Disease Management",
      "childMenu": [{
          "title": "Search",
          "href": "#"
      },
      {
          "title": "Queue",
          "href": "#"
      },
      {
          "title": "Reports",
          "href": "#"
      }],
      "collapsedLabel": "Disease Management"
  },
  {
      "IconClass": "fa fa-hospital-o",
      "Heading": "Encounters",
      "childMenu": [],
      "collapsedLabel": "Encounters"
  },
  {
      "IconClass": "fa fa-code",
      "Heading": "Coding",
      "childMenu": [],
      "collapsedLabel": "Coding"
  },
  {
      "IconClass": "fa fa-dollar",
      "Heading": "Prebilling Auditing",
      "childMenu": [],
      "collapsedLabel": "Prebilling Auditing"
  },
  {
      "IconClass": "fa fa-money",
      "Heading": "Billing",
      "childMenu": [],
      "collapsedLabel": "Billing"
  },
  {
      "IconClass": "fa fa-thumbs-o-up",
      "Heading": "Quality",
      "childMenu": [{
          "title": "HEDIS",
          "href": "#"
      },
      {
          "title": "ACO",
          "href": "#"
      }],
      "collapsedLabel": "Quality"
  },
  {
      "IconClass": "fa fa-ambulance",
      "Heading": "Admissions",
      "childMenu": [],
      "collapsedLabel": "Admissions"
  },
  {
      "IconClass": "fa fa-thumbs-o-down",
      "Heading": "A & G",
      "childMenu": [],
      "collapsedLabel": "A & G"
  },
  {
      "IconClass": "fa fa-bar-chart-o",
      "Heading": "Reports",
      "childMenu": [],
      "collapsedLabel": "Reports"
  }]
},

    {
        "Role": "IntakeCoOrdinator",
        "RoleMenu": [{
            "IconClass": "fa fa-home", "Heading": "Home", "childMenu": [],
            "collapsedLabel": "Home"
        },
      {
          "IconClass": "fa  fa-h-square", "Heading": "Authorizations", "childMenu": [],
          "collapsedLabel": "Authorizations"
      },
      {
          "IconClass": "fa fa-list-ul", "Heading": "Queue", "childMenu": [],
          "collapsedLabel": "Queue"
      },
      {
          "IconClass": "fa fa-bar-chart-o",
          "Heading": "Reports",
          "childMenu": [],
          "collapsedLabel": "Reports"
      }]
    }

];


