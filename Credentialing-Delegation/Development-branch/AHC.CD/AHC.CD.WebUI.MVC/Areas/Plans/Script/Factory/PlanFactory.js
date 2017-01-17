PlanApp.factory('PlanFactory', ['$q', '$rootScope', '$filter', '$timeout', '$window', '$log', function ($q, $rootScope, $filter, $timeout, $window, $log) {
    function getPage(start, number, params,ActiveStatus) {
        var deferred = $q.defer();
        if (ActiveStatus) {
            $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.ActivePlanData, params.search.predicateObject) : $rootScope.ActivePlanData;
        }
        else {
            $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.InactivePlanData, params.search.predicateObject) : $rootScope.InactivePlanData;
        }
        if (params.sort.predicate) {
            $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
        }
        var result = $rootScope.filtered.slice(start, start + number);
        deferred.resolve({
            data: result,
            numberOfPages: Math.ceil($rootScope.filtered.length / number)
        });
        
        return deferred.promise;
    }
    function getActiveStatus(value) {
        return !value;
    }
    function resetTableState(tableState) {
        tableState.sort = {};
        tableState.pagination.start = 0;
        tableState.search.predicateObject = {};
        return tableState;
    }
    function planRemove(ID) {
        $window.alert("Plan Removed Successfully")
    }
    function resetFormValidation($formdata) {
        $formdata.removeData('validator');
        $formdata.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse($formdata);
    }
    function modalDismiss(){
        angular.element('#PlanModalForRemove').modal('hide');
        angular.element('#PlanModalForRactive').modal('hide');
        angular.element('#PlanModalForAdd').modal('hide');
        angular.element('#PlanModalForAddedContracts').modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    }
    function LowerCaseTrimedData(value) {
        return value.trim().toLowerCase();
    }
    function FormatContractData(PlanName,Plancontracts) {
        var BE = UnqueSortedData(Plancontracts, 'BE')
        var LOB = UnqueSortedData(Plancontracts, 'LOB')
        var LOBBEMapping = [];
        for (k in LOB) {
            var temporary=[];
            for (j in BE) {
                temporary.push(trueValueCheck(LOB[k].ID,BE[j].ID,Plancontracts))
            }
            LOBBEMapping.push({ LOBName: LOB[k].Name, LOBBEMapping: temporary })
        }
        return { PlanName: PlanName, BELIST: BE, LOBBEMAPPING: LOBBEMapping }
    }
    function trueValueCheck(LOBID, BEID,Plancontracts) {
        for (i in Plancontracts) {
            if (Plancontracts[i].BusinessEntity.OrganizationGroupID == BEID && Plancontracts[i].PlanLOB.LOB.LOBID == LOBID) {
                return true;
            }
        }
        return false
    }
    function UnqueSortedData(Data,Type) {
        var data=[];
        if(Type=='BE'){
            for(i in Data){
                if(i==0)
                    data.push({ID:Data[i].BusinessEntity.OrganizationGroupID,Name:Data[i].BusinessEntity.GroupName})
                else{
                    var flag=0;
                    for(j in data){
                        if(Data[i].BusinessEntity.OrganizationGroupID==data[j].ID){
                            flag=1;
                            break;
                        }
                    }
                    if(flag==0){
                        data.push({ID:Data[i].BusinessEntity.OrganizationGroupID,Name:Data[i].BusinessEntity.GroupName})
                    }
                }
            }
        return data;
        }
        else{
            for (i in Data) {
                if (i == 0)
                    data.push({ ID: Data[i].PlanLOB.LOB.LOBID, Name: Data[i].PlanLOB.LOB.LOBName })
                else {
                    var flag = 0;
                    for (j in data) {
                        if (Data[i].PlanLOB.LOB.LOBID == data[j].ID) {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0) {
                        data.push({ ID: Data[i].PlanLOB.LOB.LOBID, Name: Data[i].PlanLOB.LOB.LOBName })
                    }
                }
            }
            return data;
        }

        
    }
    function FormatPlanData(PlanData) {
        for (i in PlanData.PlanLOBs) {
            PlanData.PlanLOBs[i].tableStatus = false;
        }
        if (PlanData.ContactDetails.length > 0) {
            for (j in PlanData.ContactDetails) {
                if (PlanData.ContactDetails[j].ContactDetail == null) {
                    PlanData.ContactDetails[j].ContactDetail = { PhoneDetails: [], EmailIDs: [], PreferredWrittenContacts: [], PreferredContacts: [] };
                }
                else {
                    if (PlanData.ContactDetails[j].ContactDetail.PreferredContacts.length>0) {
                        PlanData.ContactDetails[j].ContactDetail.PreferredContacts = angular.copy(QuickSort(PlanData.ContactDetails[j].ContactDetail.PreferredContacts));
                    }
                }
            }
        }
        else if (PlanData.ContactDetails.length == 0) {
            PlanData.ContactDetails.push({ ContactDetail: { PhoneDetails: [], EmailIDs: [], PreferredWrittenContacts: [], PreferredContacts: [] }, PlanContactDetailID: 0, ContactPersonName: "", StatusType: 1 })
        }
        return PlanData;
    }
    function PlanLogoReset() {
        var $el = angular.element('.file_preview_upload');
        $el.find('img').remove();
        $el.wrap('<form>').closest('form').get(0).reset();
        $el.unwrap();
    }
    function formatContractData(BE, LOB) {
        var BE_LOB_Maps = [];
        for (var i = 0; i < LOB.length; i++) {
            var bes = [];
            for (var j = 0; j < BE.length; j++) {
                bes.push({ BE: BE[j], IsChecked: true });
            }
            BE_LOB_Maps.push({ PlanLOB: LOB[i], BEs: bes, IsChecked: true });
        }
        return BE_LOB_Maps;
    };
    function FormatContractDataForEdit(BE, LOB, Contracts) {
        var BE_LOB_Maps = [];
        $log.log(BE);
        $log.log(LOB);
        $log.log(Contracts);
        for (var i = 0; i < LOB.length; i++) {
            var bes = [];
            for (var j = 0; j < BE.length; j++) {
                checkValue = trueValueCheck(LOB[i].LOBID, BE[j].OrganizationGroupID, Contracts)
                bes.push({ BE: BE[j], IsChecked: checkValue });
            }
            BE_LOB_Maps.push({ PlanLOB: LOB[i], BEs: bes, IsChecked: true });
        }
        return BE_LOB_Maps;
    }
    function FormatPlanContractDataTOBESaved(BE_LOB_Maps) {
        var PlanContractDetails = [];
        var LObs = [];
        for (var i in BE_LOB_Maps) {
            var BEs = [];
            for (var j in BE_LOB_Maps[i].BEs) {
                if (BE_LOB_Maps[i].BEs[j].IsChecked) {
                    BEs.push(BE_LOB_Maps[i].BEs[j].BE.OrganizationGroupID);
                }
            }
            LObs.push(BEs);
        }

        for (var i in BE_LOB_Maps) {
            for (var j in LObs[i]) {
                PlanContractDetails.push({
                    PlanContractDetailID: 0,
                    PlanLOBID: BE_LOB_Maps[i].PlanLOB.PlanLOBID,
                    OrganizationGroupID: LObs[i][j],
                    StatusType: "Active"
                });
            }
        }
        return PlanContractDetails;
    }
    function TablesHideOnLoad() {
        angular.element(".countryDailCodeContainer").hide();
        angular.element(".ProviderTypeSelectAutoList").hide();
    }
    function QuickSort(data) {
        if (data.length > 0) {
            var len = data.length;
            for (var i = len - 1; i >= 0; i--) {
                for (var j = 1; j <= i; j++) {
                    if (data[j - 1].PreferredIndex > data[j].PreferredIndex) {
                        var temp = data[j - 1];
                        data[j - 1] = data[j];
                        data[j] = temp;
                    }
                }
            }
        }
        return data;
    }





    return {
        getPage: getPage,
        getActiveStatus: getActiveStatus,
        resetTableState: resetTableState,
        planRemove: planRemove,
        resetFormValidation: resetFormValidation,
        modalDismiss: modalDismiss,
        LowerCaseTrimedData: LowerCaseTrimedData,
        FormatContractData: FormatContractData,
        FormatPlanData: FormatPlanData,
        PlanLogoReset: PlanLogoReset,
        formatContractData: formatContractData,
        FormatPlanContractDataTOBESaved: FormatPlanContractDataTOBESaved,
        TablesHideOnLoad: TablesHideOnLoad,
        FormatContractDataForEdit: FormatContractDataForEdit,
        QuickSort: QuickSort
    };

}]);
