
$("#MemberGeneralInfo").off('click', '.editGeneralInfo').on('click', '.editGeneralInfo', function () {
    var obj = getViewModelData("MemberGeneralInfo");
    getEditFormWithData("~/Areas/MH/Views/MemberProfile/ProfileTabs/Demographics/Edit/_EditGeneralInformation.cshtml", "MemberGeneralInfo", obj);
});

$("#MemberGeneralInfo").off('click', '.cancelEditGeneralInfo').on('click', '.cancelEditGeneralInfo', function () {
    var partialurl = "~/Areas/MH/Views/MemberProfile/ProfileTabs/Demographics/View/_ViewGeneralInformation.cshtml";
    var controllerMethod = "GetDemographicsDetails";
    var targetId = "MemberGeneralInfo";
    var umId = "";
    var formdata = $("#GeneralInformationForm").serialize();
    closeEditFormAndGetView(partialurl, targetId, controllerMethod, umId, formdata, "GeneralInformationForm");
})
