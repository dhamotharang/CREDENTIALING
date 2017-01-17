///*===================================================================================*/
//var AllPotentialMembers = [];
//function generatePotentialmembersGrid(dataArray) {
//    var html = '';
//    //'<tr class="tab-navigation"  data-tab-action="Member Profile" data-tab-title="Profile" data-tab-floatmenu="false" data-tab-issubtab="true" data-tab-defaultparenttab="Member" data-tab-autoflush="true" data-tab-usertype="Member" data-tab-userid="' + value.Member.MemberMemberships[0].Membership.SubscriberID + '" data-tab-path="~/Areas/MH/Views/NewMember/View/_ViewNewMember.cshtml" data-tab-container="fullBodyContainer" data-tab-parentcontainer="fullBodyContainer" data-tab-parenttabtitle="' + value.Member.PersonalInformation.FirstName + ' ' + value.Member.PersonalInformation.LastName + '" data-tab-hassubtabs="false" data-tab-parenttabpath="~/Areas/SharedView/Views/MemberHouse/_MemberProfileHeader.cshtml" data-tab-parenttabrenderingarea="headerArea"  data-tab-float-menu-path="~/Areas/MH/Views/Shared/_MHFloatMenu.cshtml" data-tab-memberdata=\'' + JSON.stringify(value) + '\'>'
//    $.each(dataArray, function (index, value) {
//        html = html +
//            '<tr>' +
//            '<td class="sm-mbrid">' + value.Member.MemberMemberships[0].Membership.SubscriberID + '</td>' +
//            '<td class="sm-lastname">' + value.Member.PersonalInformation.LastName + '</td>' +
//            '<td class="sm-firstname">' + value.Member.PersonalInformation.FirstName + '</td>' +
//            '<td class="sm-middleInitial">' + 'M' + '</td>' +
//            '<td class="sm-dob">' + value.Member.PersonalInformation.DOB.formatDate() + '</td>' +
//            '<td class="sm-gender">' + value.Member.PersonalInformation.Gender + '</td>' +
//            'html before' + (value.Provider.ContactName == null ? '<td class="sm-pcp">-</td>' : '<td class="sm-pcp">' + value.Provider.ContactName + '</td>') + 'more html' +
//            //'html before' + (value.Provider.PhoneNumber == null ? '<td class="sm-pcpphone">-</td>' :
//            //'<td class="sm-pcpphone">' + value.Provider.PhoneNumber.formatTelephone() + '</td>') + 'more html' +
//            '<td class="sm-actions">' + '<button class="btn btn-primary btn-xs tab-navigation"  data-tab-action="Member Profile" data-tab-title="' + value.Member.PersonalInformation.FirstName + ' ' + value.Member.PersonalInformation.LastName + '" data-tab-floatmenu="false" data-tab-issubtab="false" data-tab-defaultparenttab="Member" data-tab-autoflush="true" data-tab-usertype="Member" data-tab-userid="' + value.Member.MemberMemberships[0].Membership.SubscriberID + '" data-tab-path="~/Areas/MH/Views/NewMember/View/_ViewNewMember.cshtml" data-tab-container="fullBodyContainer" data-tab-parentcontainer="fullBodyContainer" data-tab-parenttabtitle="' + value.Member.PersonalInformation.FirstName + ' ' + value.Member.PersonalInformation.LastName + '" data-tab-hassubtabs="false" data-tab-parenttabpath="~/Areas/SharedView/Views/MemberHouse/_MemberProfileHeader.cshtml" data-tab-parenttabrenderingarea="headerArea"  data-tab-float-menu-path="~/Areas/MH/Views/Shared/_MHFloatMenu.cshtml" data-tab-memberdata=\'' + JSON.stringify(value) + '\'>View</button>' +
//                                        '<button class="btn btn-primary btn-xs tab-navigation"  data-tab-action="Member Profile" data-tab-title="' + value.Member.PersonalInformation.FirstName + ' ' + value.Member.PersonalInformation.LastName + '" data-tab-floatmenu="false" data-tab-issubtab="false" data-tab-defaultparenttab="Member" data-tab-autoflush="true" data-tab-usertype="Member" data-tab-userid="' + value.Member.MemberMemberships[0].Membership.SubscriberID + '" data-tab-path="~/Areas/MH/Views/NewMember/Edit/_EditNewMember.cshtml" data-tab-container="fullBodyContainer" data-tab-parentcontainer="fullBodyContainer" data-tab-parenttabtitle="' + value.Member.PersonalInformation.FirstName + ' ' + value.Member.PersonalInformation.LastName + '" data-tab-hassubtabs="false" data-tab-parenttabpath="~/Areas/SharedView/Views/MemberHouse/_MemberProfileHeader.cshtml" data-tab-parenttabrenderingarea="headerArea"  data-tab-float-menu-path="~/Areas/MH/Views/Shared/_MHFloatMenu.cshtml" data-tab-memberdata=\'' + JSON.stringify(value) + '\'>Edit</button>' +
//            '</td>' +
//            //'<td class="sm-effdate">' + value.Member.MemberMemberships[0].Membership.MemberEffectiveDate.formatDate() + '</td>' +
//            //'<td class="sm-address">' + value.Member.ContactInformation[0].AddressInformation[0].AddressLine1 + '</td>' +
//            //'<td class="sm-city">' + value.Member.ContactInformation[0].AddressInformation[0].City + '</td>' +
//            //'<td class="sm-state">' + value.Member.ContactInformation[0].AddressInformation[0].State + '</td>' +
//            //'<td class="sm-zip">' + value.Member.ContactInformation[0].AddressInformation[0].ZipCode + '</td>' +
//            '</tr>';
//    });
//    $('#searchResultGrid').append(html);
//}
//function getPotentialMembers() {
//    $.ajax({
//        url: '/Areas/MH/Resources/JSONData/Potentialmembers.js',
//        success: function (response) {
//            AllPotentialMembers = JSON.parse(response);
//        }
//    });
//    generatePotentialmembersGrid(AllPotentialMembers);
//}
//setTimeout(function () {
//    getPotentialMembers();
//}, 3000);
///*===================================================================================*/


$(function () {
   

    //$('#tableBodyWrapper').css('height', $(window).height() - 327);
    $("#resultTableHeader .filer-search").keyup(function () {
        var n = $(this).parent().parent().prevAll().length;
        var data = this.value.toUpperCase().split(" ");
        var allRows = $("#searchResultGrid").find("tr");
        if (this.value == "") {
            allRows.show();
            $('#resultCount').empty().append(getCountOfResultOnFilter(allRows));
            return;
        }
        allRows.hide();
        var filteredRows = allRows.filter(function (i, v) {
            for (var d = 0; d < data.length; ++d) {
                if ($(this).children('td').eq(n).text().toUpperCase().indexOf(data[d]) > -1) {
                    return true;
                }
            }
            return false;
        });
        filteredRows.show();
        $('#resultCount').empty().append(getCountOfResultOnFilter(filteredRows));
    });
    var f_all = 1;
    var n = 0;
    var prev_n = 0;
    $("#resultTableHeader .fa").click(function () {
        f_all *= -1;
        n = $(this).parent().parent().prevAll().length; // CURRENT COLUMN POSITION
        if (prev_n != n) {
            f_all = -1;
            $("#resultTableHeader .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white'); //SORT ICONS
        }
        sortTable(f_all, n);
        if (n == prev_n) {
            if ($(this).hasClass('fa-sort')) {
                $(this).removeClass('fa-sort').addClass('fa-sort-asc').css('color', 'black');
            }
            else if ($(this).hasClass('fa-sort-asc')) {
                $(this).removeClass('fa-sort-asc').addClass('fa-sort-desc').css('color', 'black');
            }
            else {
                $(this).removeClass('fa-sort-desc').addClass('fa-sort-asc').css('color', 'black');
            }
        }
    });

    function sortTable(f, n) {
        var rows = $('#searchResultGrid tr').get();
        rows.sort(function (a, b) {
            var A = getVal(a);
            var B = getVal(b);
            if (A > B) {
                return -1 * f;
            }
            if (A < B) {
                return 1 * f;
            }
            return 0;
        });
        function getVal(elm) {
            var v = $(elm).children('td').eq(n).text().toUpperCase();
            if ($.isNumeric(v)) {
                v = parseInt(v, 10);
            }
            return v;
        }
        $.each(rows, function (index, row) {
            $('#searchResultGrid').append(row);
        });
        prev_n = n;
    }
});

function getCountOfResultOnFilter(elementArray) {
    var count = 0;
    $.each(elementArray, function (i, v) {
        count++;
    });
    return count;
}

