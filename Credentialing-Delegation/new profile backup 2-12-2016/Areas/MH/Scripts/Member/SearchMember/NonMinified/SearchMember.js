$(function () {

    

    $("[title=Filter]").addClass('MemberSearchfilterBtn');

    //$('#SearchResultGrid').prtGrid({
    //    url: "/MH/SearchMember/GetMembersListByIndex",
    //    dataLength: 30,
    //    height: 300,
    //    columns: [{ type: 'text', name: 'SubscriberID', text: 'Subscriber ID', widthPercentage: 13, sortable: { isSort: true, defaultSort: null } },
    //              { type: 'text', name: 'LastName', text: 'Last Name', widthPercentage: 13, sortable: { isSort: true, defaultSort: null } },
    //              { type: 'text', name: 'FirstName', text: 'First Name', widthPercentage: 13, sortable: { isSort: true, defaultSort: null } },
    //              { type: 'text', name: 'DOB', text: 'DOB', widthPercentage: 13, sortable: { isSort: true, defaultSort: null } },
    //              { type: 'text', name: 'Gender', text: 'Gender', widthPercentage: 13, sortable: { isSort: true, defaultSort: null } },
    //              { type: 'text', name: 'PCP', text: 'PCP', widthPercentage: 12, sortable: { isSort: true, defaultSort: null } },
    //              { type: 'text', name: 'Status', text: 'Status', widthPercentage: 12, sortable: { isSort: true, defaultSort: null } },
    //              { type: 'none', name: '', text: '', widthPercentage: 10, sortable: { isSort: false, defaultSort: null } }
    //    ]
    //});
    /*
    * @description Show and Hide of Advanced Search.
    */
    $(".advancedSearchOptions").off('click', '#advancedsearchMembersButton').on('click', '#advancedsearchMembersButton', function () {
        $(this).addClass('hidden');
        $(this).siblings().removeClass('hidden');
        if ($("#advancedInputFieldsTable").hasClass('hidden')) $("#advancedInputFieldsTable").removeClass('hidden');
    }).off('click', '#closeAdvancedsearchMembersButton').on('click', '#closeAdvancedsearchMembersButton', function () {
        $(this).addClass('hidden');
        $(this).siblings().removeClass('hidden');
        if ($("#advancedInputFieldsTable").hasClass('hidden')) $("#advancedInputFieldsTable").removeClass('hidden')
        else $("#advancedInputFieldsTable").addClass('hidden')
    })
    EnableButtons = function () {
        alert();
    }
    /// <summary> Get All Search Inputs using Id's.</summary>
    var $AllInputFields = $('#memberIdInput, #lastNameInput, #firstNameInput, #phoneInput , #medicaidInput');
    var $AllSelectFields = $('#dobInput, #genderInput, #pcpInput, #pcpNPI, #memberInsuranceCo , #memberPlanName , #memberEffectiveDate');
    $('#fullBodyContainer').css('overflow', 'hidden');

    /*
    * @description Enabling and Disabling of Search and Clear Buttions.
    */
    $AllInputFields.on('keyup', function () {
        var empty = checkIfAllInputFieldsAreEmpty($AllInputFields);
        if (empty == true) {
            $('#searchMembersButton').prop("disabled", true);
            $('#clearSearchButton').prop("disabled", true);
            $("#resultTableHeader .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white');
        }
        else {
            $('#searchMembersButton').prop("disabled", false);
            $('#clearSearchButton').prop("disabled", false);
        }
    });
    $AllSelectFields.on('change', function () {
        var empty = checkIfAllInputFieldsAreEmpty($AllSelectFields);
        if (empty == true) {
            $('#searchMembersButton').prop("disabled", true);
            $('#clearSearchButton').prop("disabled", true);
            $("#resultTableHeader .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white');
        }
        else {
            $('#searchMembersButton').prop("disabled", false);
            $('#clearSearchButton').prop("disabled", false);
        }
    })
    /*
    * @description Search Member(ONCLICK of Search).
    */
    //$('#searchMembersButton').on('click', function () {
    //    initSearch();
    //});

    $('#inputFieldsTable ,#advancedInputFieldsTable').keypress(function (e) {
        if (e.which == 13) {
            if (checkIfAllInputFieldsAreEmpty($AllInputFields) == false) {
                initSearch();
            }
        }
    });

    //$("button#clearSearchButton").hover(function () {
    //    $(this).css({"color":"white","textdecoration":"none"});
    //})
    /*
    * @description Clear Data Entered to Search a Member.
    */
    $('#clearSearchButton').on('click', function () {
        //$('#tableWrapper').hide();
        $('#resultCountSection').hide();
        $.each($AllInputFields, function (index, element) {
            if (element.value != "" || element.value != null) {
                element.value = "";
            }
        });
        $.each($AllSelectFields, function (index, element) {
            if (element.value != "" || element.value != null) {
                element.value = "";
            }
        });
        var dropdowns = $(".select2-selection");
        var placeholders = ["MBR PCP", "PCP NPI", "Insurance Company", "Plan Name"];
        for (var i = 0; i < dropdowns.length; i++) {
            var ele = dropdowns[i];
            var placeholder = placeholders[i];
            var html = "<span class='select2-selection__placeholder'>" + placeholder + "</span>";
            if (ele.firstChild.hasAttribute('title')) {
                ele.firstChild.removeAttribute('title');
                ele.firstChild.innerText = "";
                ele.firstChild.innerHTML = html;
            }
        }
        $('#searchMembersButton').prop("disabled", true);
        $('#clearSearchButton').prop("disabled", true);
        $("#resultTableHeader .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white');

        $.ajax({
            url: '/MH/SearchMember/SearchMember',
            type: 'GET',
            data: { param : null, searchdata: null },
            error: function () {
                $('#info').html('<p>An error has occurred</p>');
            },
            dataType: 'jsonp',
            success: function (data) {
                var $title = $('<h1>').text(data.talks[0].talk_title);
                var $description = $('<p>').text(data.talks[0].talk_description);
                $('#info')
                   .append($title)
                   .append($description);
            },
            
        });
    });

    /*
    * @description Manually Trigger All the Filters for SearchResult Table.
    */
    $('.MemberSearchfilterBtn').on('click', function () {
        ShowSearchFilters('MemberSearchfilterBtn', 'resultTableHeader');
    })

  
});

$('#QueueName').hide();
var AllMembers = [];


/*
@Description Function called on Success of Ajax.FormBegin
*/
var getMembersOnSearch = function() {
    // TabManager.loadOrReloadScriptsUsingId('tableWrapper');
    
    $(".gridFeatures").removeClass('hidden');
    var resultSpan = $("#initailCountSection")[0];
    resultSpan.innerHTML = "";
    $('#SearchResultGrid').prtGrid({
        url: "/MH/SearchMember/GetMembersListByIndex",
        dataLength: 30,
        height: 300,
        columns: [{ type: 'text', name: 'SubscriberID', text: 'Subscriber ID', widthPercentage: 13, sortable: { isSort: true, defaultSort: null } },
                  { type: 'text', name: 'LastName', text: 'Last Name', widthPercentage: 13, sortable: { isSort: true, defaultSort: null } },
                  { type: 'text', name: 'FirstName', text: 'First Name', widthPercentage: 13, sortable: { isSort: true, defaultSort: null } },
                  { type: 'text', name: 'DOB', text: 'DOB', widthPercentage: 13, sortable: { isSort: true, defaultSort: null } },
                  { type: 'text', name: 'Gender', text: 'Gender', widthPercentage: 13, sortable: { isSort: true, defaultSort: null } },
                  { type: 'text', name: 'PCP', text: 'PCP', widthPercentage: 12, sortable: { isSort: true, defaultSort: null } },
                  { type: 'text', name: 'Status', text: 'Status', widthPercentage: 12, sortable: { isSort: true, defaultSort: null } },
                  { type: 'none', name: '', text: '', widthPercentage: 10, sortable: { isSort: false, defaultSort: null } }
        ]
    });
    var gridBody = $(".pt-table-tbody")[0];
    var count = gridBody.childElementCount;
    resultSpan.innerHTML = "Search Member Result - (" + count + ") Results";
}
/*
 *@Description Checks if All the Search Fields are Empty.
 *@Param {array} Array of All Search Field Elements.
 *@return {boolean}
*/
function checkIfAllInputFieldsAreEmpty(AllInputFields) {
    $('#QueueName').hide();
    var empty = true;
    $.each(AllInputFields, function (index, element) {
        if (element.value != "") {
            empty = false;
        }
    });
    return empty;
}
/*
 *@Description Initializes the Search of Member.
*/
function initSearch() {
    $("#resultTableHeader .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white');
    var memberIdInput = $('#memberIdInput').val();
    var lastNameInput = $('#lastNameInput').val();
    var firstNameInput = $('#firstNameInput').val();
    var dobInput = $('#dobInput').val();
    var genderInput = $('#genderInput').val();
    var phoneInput = $('#phoneInput').val();
    //var hicnInput = $('#hicnInput').val();
    var pcpInput = $('#pcpInput').val();
    var pcpNPIInput = $('#pcpNPI').val();
    var medicaidInput = $('#medicaidInput').val();
    var insuranceCoInput = $('#memberInsuranceCo').val();
    var planNameInput = $('#memberPlanName').val();
    var effectiveDateInput = $('#memberEffectiveDate').val();
    
    SearchMembersByFactors(memberIdInput, lastNameInput, firstNameInput, dobInput, genderInput, phoneInput, pcpInput, pcpNPIInput, medicaidInput, insuranceCoInput, planNameInput, effectiveDateInput);
}

$('#dobInput').datetimepicker({ format: 'MM/DD/YYYY' });
$('#memberEffectiveDate').datetimepicker({ format: 'MM/DD/YYYY' });
$('#dobInput', '#memberEffectiveDate').on('click', function () {
    var p = $('.bootstrap-datetimepicker-widget');
    //var d = $(p[0]);
    //console.log(d.html());
})



/*
 *@Description Search Member By Considering the search Factors.
*/
/// <signature>
///   <param name='mbrid' type='string' /><param name='lastname' type='string' /><param name='firstname' type='string' />
///   <param name='dob' type='string' /><param name='gender' type='string' /><param name='pcp' type='string' />
/// </signature>
function SearchMembersByFactors(mbrid, lastname, firstname, dob , gender, pcp, pcpNPI, medicareid, insurancecompany, planname, effectivedate) {
    var filteredResult;
    if (mbrid != "") {
        filteredResult = filterByParticularField(mbrid, "SubscriberID");
    }
    if (lastname != "") {
        filteredResult = filterByParticularField(lastname, "LastName");
    }
    if (firstname != "") {
        filteredResult = filterByParticularField(firstname, "FirstName");
    }
    if (dob != "") {
        filteredResult = filterByParticularField(hicn, "");
    }
    if (gender != "") {
        filteredResult = filterByParticularField(medicaid, "");
    }
    if (pcp != "") {
        filteredResult = filterByParticularField(phone, "ContactNumber");
    }
}

/*
 *@Description Filters the Members Data Based on Search Factors.
*/
/// <signature>
///   <param name='input' type='string' />
///   <param name='field' type='string' />
/// </signature>
function filterByParticularField(input, field) {
    $('#tableWrapper').show();
    //TabManager.showLoadingSymbol('tableBodyWrapper');
    if (AllMembers.length == 0) {
        /// <summary>Making an Ajax Call to Get the Members.</summary>
        //$.ajax({
        //    url: '/Areas/UM/Resources/ServiceData/MemberServiceData.js',
        //    success: function (response) {
        //        AllMembers = JSON.parse(response);
        //        for (var mem in AllMembers) {
        //            if (AllMembers[mem].Provider && AllMembers[mem].Provider.ProviderPracticeLocationAddress) {
        //                if (AllMembers[mem].Provider.ProviderPracticeLocationAddress.MobileNumber) {
        //                    AllMembers[mem].Provider.ProviderPracticeLocationAddress.MobileNumber = AllMembers[mem].Provider.ProviderPracticeLocationAddress.MobileNumber.replace(/\D/g, '');
        //                    AllMembers[mem].Provider.PhoneNumber = AllMembers[mem].Provider.ProviderPracticeLocationAddress.MobileNumber.slice(-10);
        //                }
        //            }
        //        }
        //        processResults(input, field, AllMembers);
        //    }
        //});
    }
    else {
        processResults(input, field, AllMembers);
    }
}

/*
 *@Description Processing the Results Based on Search Factor.
*/
/// <signature>
///   <param name='input' type='string' />
///   <param name='field' type='string' />
///   <param name='list'  type='array' />
/// </signature>
function processResults(input, field, list) {
    var filteredResult = [];
    if (field == "LastName" || field == "FirstName" || field == "DOB") {
        $.each(list, function (key, value) {
            var pi = value.Member.PersonalInformation;
            $.each(pi, function (k, v) {
                if (k == field && v != null) {
                    if (v.toUpperCase().indexOf(input.toUpperCase()) > -1) {
                        filteredResult.push(value);
                    }
                }
            });
        });
    }
    if (field == "ContactNumber") {
        $.each(list, function (key, value) {
            var pi = value.Member.ContactInformation[0].PhoneInformation[0];
            $.each(pi, function (k, v) {
                if (k == field && v != null) {
                    if (v.toUpperCase().indexOf(input.toUpperCase()) > -1) {
                        filteredResult.push(value);
                    }
                }
            });
        });
    }
    if (field == "SubscriberID") {
        $.each(list, function (key, value) {
            var pi = value.Member.MemberMemberships[0].Membership;
            $.each(pi, function (k, v) {
                if (k == field && v != null) {
                    if (v.toUpperCase().indexOf(input.toUpperCase()) > -1) {
                        filteredResult.push(value);
                    }
                }
            });
        });
    }
    exportResults(filteredResult);
}


/*
 *@Description Exporting the Processed Data.
 *@Param {array} array of Members
*/
function exportResults(filteredResult) {
    $('#searchResultGrid').empty();
    if (filteredResult.length != 0) {
        buildRows(filteredResult);
        $('#resultCountSection').show();
        $('#initailCountSection').hide();
    }
    else {
        $('#tableWrapper').hide();
        $('#noResultFound').show();
        $('#resultCountSection').hide();
    }
    //TabManager.hideLoadingSymbol();
}


/*
 *@Description Show the Data on UI(Building Rows Dynamically).
 *@Param {array} array of Members
*/
function buildRows(filteredResult) {
    $('#resultCount').empty().append(getCountOfResult(filteredResult));
    var html = '';
    $.each(filteredResult, function (index, value) {
        html = html +
       '<tr>' +
       '<td class="sm-mbrid">' + value.Member.MemberMemberships[0].Membership.SubscriberID + '</td>' +
       '<td class="sm-lastname">' + value.Member.PersonalInformation.LastName + '</td>' +
       '<td class="sm-firstname">' + value.Member.PersonalInformation.FirstName + '</td>' +
       '<td class="sm-middleInitial">' + 'M' + '</td>' +
       '<td class="sm-dob">' + value.Member.PersonalInformation.DOB.formatDate() + '</td>' +
       '<td class="sm-gender">' + value.Member.PersonalInformation.Gender + '</td>' +
       'html before' + (value.Provider.ContactName == null ? '<td class="sm-pcp">-</td>' : '<td class="sm-pcp">' + value.Provider.ContactName + '</td>') + 'more html' +
       //'html before' + (value.Provider.PhoneNumber == null ? '<td class="sm-pcpphone">-</td>' :
       //'<td class="sm-pcpphone">' + value.Provider.PhoneNumber.formatTelephone() + '</td>') + 'more html' +
       '<td class="sm-actions">' + '<button class="btn btn-primary btn-xs tab-navigation"  data-tab-action="Member Profile" data-tab-title="' + value.Member.PersonalInformation.FirstName + ' ' + value.Member.PersonalInformation.LastName + '" data-tab-floatmenu="false" data-tab-issubtab="false" data-tab-defaultparenttab="Member" data-tab-autoflush="true" data-tab-usertype="Member" data-tab-userid="' + value.Member.MemberMemberships[0].Membership.SubscriberID + '" data-tab-path="~/Areas/MH/Views/NewMember/View/_ViewNewMember.cshtml" data-tab-container="fullBodyContainer" data-tab-parentcontainer="fullBodyContainer" data-tab-parenttabtitle="' + value.Member.PersonalInformation.FirstName + ' ' + value.Member.PersonalInformation.LastName + '" data-tab-hassubtabs="false" data-tab-parenttabpath="~/Areas/SharedView/Views/MemberHouse/_MemberProfileHeader.cshtml" data-tab-parenttabrenderingarea="headerArea"  data-tab-float-menu-path="~/Areas/MH/Views/Shared/_MHFloatMenu.cshtml" data-tab-memberdata=\'' + JSON.stringify(value) + '\'>View</button>' +
                                   '<button class="btn btn-primary btn-xs tab-navigation"  data-tab-action="Member Profile" data-tab-title="' + value.Member.PersonalInformation.FirstName + ' ' + value.Member.PersonalInformation.LastName + '" data-tab-floatmenu="false" data-tab-issubtab="false" data-tab-defaultparenttab="Member" data-tab-autoflush="true" data-tab-usertype="Member" data-tab-userid="' + value.Member.MemberMemberships[0].Membership.SubscriberID + '" data-tab-path="~/Areas/MH/Views/NewMember/Edit/_EditNewMember.cshtml" data-tab-container="fullBodyContainer" data-tab-parentcontainer="fullBodyContainer" data-tab-parenttabtitle="' + value.Member.PersonalInformation.FirstName + ' ' + value.Member.PersonalInformation.LastName + '" data-tab-hassubtabs="false" data-tab-parenttabpath="~/Areas/SharedView/Views/MemberHouse/_MemberProfileHeader.cshtml" data-tab-parenttabrenderingarea="headerArea"  data-tab-float-menu-path="~/Areas/MH/Views/Shared/_MHFloatMenu.cshtml" data-tab-memberdata=\'' + JSON.stringify(value) + '\'>Edit</button>' +
       '</td>' +
       //'<td class="sm-effdate">' + value.Member.MemberMemberships[0].Membership.MemberEffectiveDate.formatDate() + '</td>' +
       //'<td class="sm-address">' + value.Member.ContactInformation[0].AddressInformation[0].AddressLine1 + '</td>' +
       //'<td class="sm-city">' + value.Member.ContactInformation[0].AddressInformation[0].City + '</td>' +
       //'<td class="sm-state">' + value.Member.ContactInformation[0].AddressInformation[0].State + '</td>' +
       //'<td class="sm-zip">' + value.Member.ContactInformation[0].AddressInformation[0].ZipCode + '</td>' +
       '</tr>';
    });
    $('#searchResultGrid').append(html);
}

/*
 *@Description Sorting the Table Data.
/// <signature>
///   <param name='array' type='array' />
///   <param name='key' type='string' />
/// </signature>
*/
function sortByKey(array, key) {
    return array.sort(function (a, b) {
        var x = a[key]; var y = b[key];
        return ((x < y) ? -1 : ((x > y) ? 1 : 0));
    });
}

/*
 *@Description Get the Count of Search Result.
 *@Param {array} array of Processed Members
*/
function getCountOfResult(filteredResult) {
    var count = 0;
    $.each(filteredResult, function (index, value) {
        count++;
    });
    return count;
}

/*
 *@Description Get Count of Members on Filtering.
 *@Param {array} array of Members
*/
//function getCountOfResultOnFilter(elementArray) {
//    var count = 0;
//    $.each(elementArray, function (i, v) {
//        count++;
//    });
//    return count;
//}




