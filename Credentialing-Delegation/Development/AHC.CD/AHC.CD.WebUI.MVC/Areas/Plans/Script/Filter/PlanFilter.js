PlanApp.filter("PlanExist", ["$rootScope", "PlanFactory", function ($rootScope, PlanFactory) {
    return function (Plan) {
        var tempPlan = PlanFactory.LowerCaseTrimedData(Plan);
        if ($rootScope.ActivePlanData.filter(function (ActivePlanDatas) { return PlanFactory.LowerCaseTrimedData(ActivePlanDatas.PlanName) == tempPlan }).length == 0 && $rootScope.InactivePlanData.filter(function (InactivePlanDatas) { return PlanFactory.LowerCaseTrimedData(InactivePlanDatas.PlanName) == tempPlan }).length == 0) {
            return false;
        }
        else{
            return true;
        }
    }
}])

PlanApp.filter("MasterDataResetforPage", ["PlanFactory", "MasterDataForCrudOperation", function (PlanFactory, MasterDataForCrudOperation) {
    return function (Operation) {
        return MasterDataForCrudOperation.filter(function (Data) { return PlanFactory.LowerCaseTrimedData(Data.Type) == PlanFactory.LowerCaseTrimedData(Operation) })[0];
    }
}])

//PlanApp.filter("PreferredContactsSortedFilter", ["PlanFactory", function (PlanFactory) {
//    return function (PreferredContacts) {
//        return PlanFactory.QuickSort(PreferredContacts);
//    }
//}])