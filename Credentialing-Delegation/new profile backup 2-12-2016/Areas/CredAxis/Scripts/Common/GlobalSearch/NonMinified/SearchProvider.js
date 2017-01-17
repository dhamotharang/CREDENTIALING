/// <summary> Get All Search Inputs using Id's.</summary>
var $AllInputFields = $('#providernpiInput, #lastNameInput, #firstNameInput, .typeMutiple, #providerlevelInput,#RelationshipInput, .groupMutiple ,.planMutiple');
$('#fullBodyContainer').css('overflow', 'hidden');
$(".planMutiple").select2({
    tags: true,
    placeholder: 'PLAN',
    allowClear: true
})
$(".typeMutiple").select2({
    tags: true,
    placeholder: 'SPECIALTY',
    allowClear: true
})
$(".groupMutiple").select2({
    tags: true,
    placeholder: 'GROUP/IPA',
    allowClear: true
})
/*
* @description Enabling and Disabling of Search and Clear Buttions.
*/
$AllInputFields.on('keyup', function () {
    var empty = checkIfAllInputFieldsAreEmpty($AllInputFields);
    if (empty == true) {
        $('#searchProviderButton').prop("disabled", true);
        $('#clearSearchButton').prop("disabled", true);
        $("#resultTableHeader .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white');
    }
    else {
        $('#searchProviderButton').prop("disabled", false);
        $('#clearSearchButton').prop("disabled", false);
    }
});
$AllInputFields.on('change', function () {
    var empty = checkIfAllInputFieldsAreEmpty($AllInputFields);
    if (empty == true) {
        $('#tableWrapper').hide();
        $('#resultCountSection').hide();
        $('#searchProviderButton').prop("disabled", true);
        $('#clearSearchButton').prop("disabled", true);
        $("#resultTableHeader .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white');
    }
    else {
        $('#searchProviderButton').prop("disabled", false);
        $('#clearSearchButton').prop("disabled", false);
    }
});

$('#inputFieldsTable ,#advancedInputFieldsTable').keypress(function (e) {
    if (e.which == 13) {
        if (checkIfAllInputFieldsAreEmpty($AllInputFields) == false) {
            initSearch();
        }
    }
});


function checkIfAllInputFieldsAreEmpty(AllInputFields) {
    //$('#QueueName').hide();
    var empty = true;
    $.each(AllInputFields, function (index, element) {
        if (element.value != "") {
            empty = false;
        }
    });
    return empty;
}

/*
  * @description Manually Trigger All the Filters for SearchResult Table.
  */
$('#exportOptionsForAdvanceUMSearch').off('click').on('click', 'a[title="Filter"]', function () {
    ShowSearchFilters('MemberSearchfilterBtn', 'resultTableHeader');
})

/*
* @description Clear Data Entered to Search a Member.
*/
$('#clearSearchButton').on('click', function () {
    $('#tableWrapper').hide();
    $('#resultCountSection').hide();
    $(".typeMutiple").val('').trigger('change');
    $(".planMutiple").val('').trigger('change');
    $(".groupMutiple").val('').trigger('change');
    $AllInputFields.val("");
    $('#searchProviderButton').prop("disabled", true);
    $('#clearSearchButton').prop("disabled", true);
    $("#resultTableHeader .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white');
});

$('#searchProviderButton').on('click', function () {
    initSearch();
});

function initSearch() {
    $("#resultTableHeader .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white');
    var providernpiInput = $('#providernpiInput').val();
    var lastNameInput = $('#lastNameInput').val();
    var firstNameInput = $('#firstNameInput').val();
    var specialtyInput = $('#specialtyInput').val();
    var providerlevelInput = $('#providerlevelInput').val();
    var RelationshipInput = $('#RelationshipInput').val();
    var groupipaInput = $('#groupipaInput').val();
    var planInput = $('#planInput').val();
    SearchProviderByFactors(providernpiInput, lastNameInput, firstNameInput, specialtyInput, providerlevelInput, RelationshipInput, groupipaInput, planInput);
}



function SearchProviderByFactors(Pronpi, lastname, firstname, specialty, prolevel, relation, goupipa, plan) {
    var filteredResult;
    filterResult = {
        "ProviderNPI": Pronpi,
        "LastName": lastname,
        "FirstName": firstname,
        "Specialty": specialty,
        "ProviderLevel": prolevel,
        "Relationship": relation,
        "GroupIPA": goupipa,
        "plan": plan
    };

    filterByParticularField(filterResult);
}



function filterByParticularField(fields) {
    $('#tableWrapper').show();
    $.ajax({
        type: "post",
        url: '/CredAxis/ServiceProvider/SearchProvider',
        data: fields,
        success: function (response) {
            $("#searchResultGrid").empty();
            $("#searchResultGrid").html(response);
            $("#resultCountSection").show();
            var allRows = $("#searchResultGrid").find("tr");
            $('#resultCount').empty().append(getCountOfResultOnFilter(allRows));
            $('#ActualresultCount').empty().append(getCountOfResultOnFilter(allRows));
            //Popover initialization
            $('[data-toggle="popover"]').popover();

            //$('#resultTableHeader').prtGrid({
            //    url: "/CredAxis/ProviderService/GetFilteredSearchProvider",
            //    dataLength: 20,
            //    columns: [{ type: 'text', name: 'ProviderNPI', text: 'NPI', widthPercentage: 10, sortable: true },
            //    { type: 'text', name: 'LastName', text: 'L NAME', widthPercentage: 10, sortable: true },
            //    { type: 'text', name: 'FirstName', text: 'F NAME', widthPercentage: 10, sortable: true },
            //    { type: 'text', name: 'Network', text: 'NP', widthPercentage: 5, sortable: true },
            //     { type: 'text', name: 'ProviderLevel', text: 'LEVEL', widthPercentage: 10, sortable: true },
            //    { type: 'text', name: 'Relationship', text: 'REL', widthPercentage: 10, sortable: true },
            //    { type: 'text', name: 'Specialty', text: 'SPECIALTY', widthPercentage: 10, sortable: true },
            //        { type: 'text', name: 'GroupIPA', text: 'GROUP/IPA', widthPercentage: 10, sortable: true },
            //      { type: 'text', name: 'Plan', text: 'PLAN', widthPercentage: 10, sortable: true },
            //     { type: 'text', name: 'Status', text: 'STATUS', widthPercentage: 5, sortable: true },
            //    { type: '', name: '', text: '', widthPercentage: 5, sortable: true },
                
            //    ]
            //});

        }
    });

}




