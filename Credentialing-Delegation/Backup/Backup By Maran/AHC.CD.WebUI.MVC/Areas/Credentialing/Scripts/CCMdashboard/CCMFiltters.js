CCMDashboard.filter("CCMDashboardFilterByPlan", ["$rootScope", "CCMDashboardFactory", function ($rootScope, CCMDashboardFactory) {
    return function (PlanName) {
        return CCMDashboardFactory.getFilteredAppointmentdataByPlan(PlanName);
    }
}])
CCMDashboard.filter("CCMDashboardFilterByAppointmentDate", ["$rootScope", "CCMDashboardFactory", function ($rootScope, CCMDashboardFactory) {
    return function (AppointmentDate) {
        return CCMDashboardFactory.getFilteredAppointmentdataByAppointmentDate(AppointmentDate);
    }
}])