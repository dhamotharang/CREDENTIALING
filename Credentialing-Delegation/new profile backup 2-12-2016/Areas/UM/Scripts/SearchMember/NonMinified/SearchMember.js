$(function () {
    $("[title=Filter]").addClass('MemberSearchfilterBtn');
    var $AllInputFields = $('#memberIdInput, #lastNameInput, #firstNameInput, #hicnInput, #medicaidInput, #phoneInput, #dobInput');
    $('#fullBodyContainer').css('overflow', 'hidden');
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
    $('#searchMembersButton').on('click', function () {
        initSearch();
    });
    $('#inputFieldsTable').keypress(function (e) {
        if (e.which == 13) {
            if (checkIfAllInputFieldsAreEmpty($AllInputFields) == false) {
                initSearch();
            }
        }
    });
    $('#clearSearchButton').on('click', function () {
        $('#tableWrapper').hide();
        $('#resultCountSection').hide();
        $AllInputFields.val("");
        $('#searchMembersButton').prop("disabled", true);
        $('#clearSearchButton').prop("disabled", true);
        $("#resultTableHeader .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white');
    });
    $('.MemberSearchfilterBtn').on('click', function () {
        ShowSearchFilters('MemberSearchfilterBtn', 'resultTableHeader');
    })
});
$('#QueueName').hide();
var searchParam = [];
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

function initSearch() {
    $("#resultTableHeader .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white');
    var memberIdInput = $('#memberIdInput').val();
    var lastNameInput = $('#lastNameInput').val();
    var firstNameInput = $('#firstNameInput').val();
    var hicnInput = $('#hicnInput').val();
    var medicaidInput = $('#medicaidInput').val();
    var phoneInput = $('#phoneInput').val();
    var dobInput = $('#dobInput').val();
    SearchMembersByFactors(memberIdInput, lastNameInput, firstNameInput, hicnInput, medicaidInput, phoneInput, dobInput);
}

$('#dobInput').datetimepicker({ format: 'MM/DD/YYYY' });
$('#dobInput').on('click', function () {
    var p = $('.bootstrap-datetimepicker-widget');
    var d = $(p[0]);
    console.log(d.html());
})
function SearchMembersByFactors(mbrid, lastname, firstname, hicn, medicaid, phone, dob) {
    var filteredResult;
    filterResult = { "SubscriberID": mbrid,
         "LastName": lastname,
         "FirstName": firstname,
         "HICN": hicn,
         "MEDICAID": medicaid,
         "Phone": phone,
         "DateOfBirth": dob
    };

    filterByParticularField(filterResult);
}

function filterByParticularField(fields) {
    $('#tableWrapper').show();
    $.ajax({
        type:"post",
        url: '/Member/SearchMembers',
        data: fields,
        success: function (response) {
            //AllMembers = JSON.parse(response);
            //for (var mem in AllMembers) {
            //    if (AllMembers[mem].Provider && AllMembers[mem].Provider.ProviderPracticeLocationAddress) {
            //        if (AllMembers[mem].Provider.ProviderPracticeLocationAddress.MobileNumber) {
            //            AllMembers[mem].Provider.ProviderPracticeLocationAddress.MobileNumber = AllMembers[mem].Provider.ProviderPracticeLocationAddress.MobileNumber.replace(/\D/g, '');
            //            AllMembers[mem].Provider.PhoneNumber = AllMembers[mem].Provider.ProviderPracticeLocationAddress.MobileNumber.slice(-10);
            //        }
            //    }
            //}
            $("#searchResultGrid").empty();
            $("#searchResultGrid").html(response);
            //processResults(input, field, AllMembers);
        }
    });
    //  }
    //else {
    //    processResults(input, field, AllMembers);
    //}
}

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

function exportResults(filteredResult) {
    $('#searchResultGrid').empty();
    if (filteredResult.length != 0) {
        buildRows(filteredResult);
        $('#resultCountSection').show();
    }
    else {
        $('#tableWrapper').hide();
        $('#noResultFound').show();
        $('#resultCountSection').hide();
    }
    //TabManager.hideLoadingSymbol();
}

function buildRows(filteredResult) {
    $('#resultCount').empty().append(getCountOfResult(filteredResult));
    var html = '';
    if (appConfig.toUpperCase() == 'MH') {
        $.each(filteredResult, function (index, value) {
            html = html +
                '<tr class="tab-navigation"  data-tab-action="Member Profile" data-tab-title="Profile" data-tab-floatmenu="true" data-tab-issubtab="true" data-tab-defaultparenttab="Member" data-tab-autoflush="true" data-tab-usertype="Member" data-tab-userid="' + value.Member.MemberMemberships[0].Membership.SubscriberID + '" data-tab-path="~/Areas/MH/Views/MemberProfile/_MemberDetails.cshtml" data-tab-container="innerTabContainer" data-tab-parentcontainer="partialBodyContainer" data-tab-parenttabtitle="' + value.Member.PersonalInformation.FirstName + ' ' + value.Member.PersonalInformation.LastName + '" data-tab-hassubtabs="false" data-tab-parenttabpath="~/Areas/SharedView/Views/MemberHouse/_MemberProfileHeader.cshtml" data-tab-parenttabrenderingarea="headerArea"  data-tab-float-menu-path="~/Areas/UM/Views/Shared/_UMFloatMenu.cshtml" data-tab-memberdata=\'' + JSON.stringify(value) + '\'>' +
                '<td class="sm-mbrid">' + value.Member.MemberMemberships[0].Membership.SubscriberID + '</td>' +
                '<td class="sm-lastname">' + value.Member.PersonalInformation.LastName + '</td>' +
                '<td class="sm-firstname">' + value.Member.PersonalInformation.FirstName + '</td>' +
                '<td class="sm-dob">' + value.Member.PersonalInformation.DOB.formatDate() + '</td>' +
                '<td class="sm-gender">' + value.Member.PersonalInformation.Gender + '</td>' +
                'html before' + (value.Provider.ContactName == null ? '<td class="sm-pcp">-</td>' : '<td class="sm-pcp">' + value.Provider.ContactName + '</td>') + 'more html' +
                'html before' + (value.Provider.PhoneNumber == null ? '<td class="sm-pcpphone">-</td>' :
                '<td class="sm-pcpphone">' + value.Provider.PhoneNumber.formatTelephone() + '</td>') + 'more html' +
                '<td class="sm-effdate">' + value.Member.MemberMemberships[0].Membership.MemberEffectiveDate.formatDate() + '</td>' +
                '<td class="sm-address">' + value.Member.ContactInformation[0].AddressInformation[0].AddressLine1 + '</td>' +
                '<td class="sm-city">' + value.Member.ContactInformation[0].AddressInformation[0].City + '</td>' +
                '<td class="sm-state">' + value.Member.ContactInformation[0].AddressInformation[0].State + '</td>' +
                '<td class="sm-zip">' + value.Member.ContactInformation[0].AddressInformation[0].ZipCode + '</td>' +
                '</tr>';
        });
    }
    else {
        $.each(filteredResult, function (index, value) {
            html = html +
                '<tr class="tab-navigation"  data-tab-action="Member Profile" data-tab-title="Profile" data-tab-floatmenu="true" data-tab-issubtab="true" data-tab-defaultparenttab="Member" data-tab-autoflush="true" data-tab-usertype="Member" data-tab-userid="' + value.Member.MemberMemberships[0].Membership.SubscriberID + '" data-tab-path="~/Areas/UM/Views/Member/_MemberDetails.cshtml" data-tab-container="innerTabContainer" data-tab-parentcontainer="partialBodyContainer" data-tab-parenttabtitle="' + value.Member.PersonalInformation.FirstName + ' ' + value.Member.PersonalInformation.LastName + '" data-tab-hassubtabs="false" data-tab-parenttabpath="~/Areas/Portal/Views/Member/_MemberHeader.cshtml" data-tab-parenttabrenderingarea="headerArea"  data-tab-float-menu-path="~/Areas/UM/Views/Shared/_UMFloatMenu.cshtml" data-tab-memberdata=\'' + JSON.stringify(value) + '\'>' +
                '<td class="sm-mbrid">' + value.Member.MemberMemberships[0].Membership.SubscriberID + '</td>' +
                '<td class="sm-lastname">' + value.Member.PersonalInformation.LastName + '</td>' +
                '<td class="sm-firstname">' + value.Member.PersonalInformation.FirstName + '</td>' +
                '<td class="sm-dob">' + value.Member.PersonalInformation.DOB.formatDate() + '</td>' +
                '<td class="sm-gender">' + value.Member.PersonalInformation.Gender + '</td>' +
                'html before' + (value.Provider.ContactName == null ? '<td class="sm-pcp">-</td>' : '<td class="sm-pcp">' + value.Provider.ContactName + '</td>') + 'more html' +
                'html before' + (value.Provider.PhoneNumber == null ? '<td class="sm-pcpphone">-</td>' :
                '<td class="sm-pcpphone">' + value.Provider.PhoneNumber.formatTelephone() + '</td>') + 'more html' +
                '<td class="sm-effdate">' + value.Member.MemberMemberships[0].Membership.MemberEffectiveDate.formatDate() + '</td>' +
                '<td class="sm-address">' + value.Member.ContactInformation[0].AddressInformation[0].AddressLine1 + '</td>' +
                '<td class="sm-city">' + value.Member.ContactInformation[0].AddressInformation[0].City + '</td>' +
                '<td class="sm-state">' + value.Member.ContactInformation[0].AddressInformation[0].State + '</td>' +
                '<td class="sm-zip">' + value.Member.ContactInformation[0].AddressInformation[0].ZipCode + '</td>' +
                '</tr>';
        });
    }

    $('#searchResultGrid').append(html);
}

function sortByKey(array, key) {
    return array.sort(function (a, b) {
        var x = a[key]; var y = b[key];
        return ((x < y) ? -1 : ((x > y) ? 1 : 0));
    });
}

function getCountOfResult(filteredResult) {
    var count = 0;
    $.each(filteredResult, function (index, value) {
        count++;
    });
    return count;
}