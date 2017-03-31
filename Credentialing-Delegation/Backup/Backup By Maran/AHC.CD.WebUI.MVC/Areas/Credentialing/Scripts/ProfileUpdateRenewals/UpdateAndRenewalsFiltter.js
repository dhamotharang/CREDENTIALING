UpdateAndRenewalsApp.filter("MasterSettingFiltter", ["$rootScope", "UpdateAndRenewalsFactory", "MasterSettings", function ($rootScope, UpdateAndRenewalsFactory, MasterSettings) {
    return function (Operation) {
        return MasterSettings.filter(function (Data) { return UpdateAndRenewalsFactory.LowerCaseTrimedData(Data.ActionType) == UpdateAndRenewalsFactory.LowerCaseTrimedData(Operation) })[0];
    }
}])





