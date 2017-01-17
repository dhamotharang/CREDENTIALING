function setMemberHeaderData(userData) {
    if (typeof userData != 'undefined') {
        $("#MemberID").text(userData.Member.MemberMemberships[0].Membership.SubscriberID);
        $("#MemberName").text(userData.Member.PersonalInformation.FirstName + " " + userData.Member.PersonalInformation.LastName);
        (userData.Member.PersonalInformation.Gender.toLowerCase() == 'female') ? $("#MemberGender").text("F") : $("#MemberGender").text("M")
        $("#MemberDob").text(userData.Member.PersonalInformation.DOB.formatDate());
        if (userData.Member.PersonalInformation.DOB) $("#MemberAge").text(userData.Member.PersonalInformation.DOB.getAge());
        $("#MemberSubGrp").text("-");
        $("#MemberEffDate").text(userData.Member.MemberMemberships[0].Membership.MemberEffectiveDate.formatDate());
        $("#MemberEligibility").text("12/31/2018");
        $("#MemberPCP").text(userData.Member.MemberMemberships[0].Membership.MembershipProviderInformation[0].Provider.PersonalInformation.FirstName + " " + userData.Member.MemberMemberships[0].Membership.MembershipProviderInformation[0].Provider.PersonalInformation.LastName);
        $("#MemberPCPPhone").text(userData.Provider.PhoneNumber.formatTelephone());
    }
}

function commonAjax(partialurl, targetId, dataObject, formId) {
    if(typeof(dataObject) === 'object'){
        $.ajax({
            method: "POST",
            url: "/MH/MemberProfile/GetPartial",
            data: { partialURL: partialurl, generalInfo: dataObject },
            success: function (data) {
                var container = document.getElementById(targetId);
                container.innerHTML = data + '<div class="clearfix"></div>';
               
        }
    })
        
    } else {
        var dataobj = $("#" + formId).serialize();
        $.ajax({
            method: "POST",
            url: "/MH/MemberProfile/GetPartial",
            data: { partialURL: partialurl, generalInfo: dataobj },
            success: function (data) {
                var container = document.getElementById(targetId);
                container.innerHTML = data + '<div class="clearfix"></div>';
               
            }
        })
}
}

/*
 *@Description Gets Edit Form With View Data Pre-filled.
*/
/// <signature>
///   <param name='partialurl'  type='string' />*@Description PartialView of Required Edit.
///   <param name='id'          type='string' />*@Description Target Div Id Where Required View has to be appended.
///   <param name='dataObject'  type='object' />*@Description ViewModel Data Object Binded to Current View.
/// </signature>
function getEditFormWithData(partialurl, targetId, dataObject) {
    TabManager.showLoadingSymbol(targetId);
    commonAjax(partialurl, targetId, dataObject);
    return;
}
/*
 *@Description Gets View from Controller With Data Closing Edit Form.
*/
/// <signature>
///   <param name='partialurl'        type='string' />*@Description PartialView of Required Edit.
///   <param name='targetId'          type='string' />*@Description Target Div Id Where Required View has to be appended.
///   <param name='controllerMethod'  type='string' />*@Description Controller Method that Need to be Invoked.
///   <param name='memberId'          type='string' />*@Description Unique Member Id.
/// </signature>
function closeEditFormAndGetView(partialurl, targetId, controllerMethod, memberId, formdata, formId) {
    commonAjax(partialurl, targetId, formdata, formId);
    return;
}

/*
 *@Description Gets ViewModel Data Binded to View.
 *@Param {string} Div Id of the View.
 *@return {object}
*/
function getViewModelData(divId) {
    var $spanElements = $("#" + divId).find("span[property]");
    var $count = $spanElements.length;
    var obj = {};
    for (var i = 0; i < $count; i++) {
        var prop = $spanElements[i].getAttribute('property');
        var val = $spanElements[i].innerText;
        obj[prop] = val;
    }
    return obj;
}