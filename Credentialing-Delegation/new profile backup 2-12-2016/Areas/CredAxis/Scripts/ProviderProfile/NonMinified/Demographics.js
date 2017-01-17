/*********method to append data to the Encounters Table::::(Schedules)*************/
$('#schedulelist').empty();

/**********Sample List of Data for active encounters*************/

$.each(scheduleEncounters, function (index, value) {
    $('a[data-toggle="tooltip"]').tooltip();
    $('#schedulelist').append(
                       '<tr>' +
                       '<td>' + value.ENCTRID + '</label>' + '</td>' +
                       '<td>' + value.MBRID + '</td>' +
                       '<td>' + value.MBRLASTNAME + '</td>' +
                       '<td>' + value.MBRFIRSTNAME + '</td>' +
                       '<td>' + value.NPI + '</td>' +
                       '<td>' + value.PRVDRLASTNAME + '</div></td>' +
                       '<td>' + value.PRVDRFIRSTNAME + '</td>' +
                       '<td>' + value.FACILITY + '</td>' +
                       '<td>' + value.DOS + '</td>' +
                       '<td>' + value.DOC + '</td>' +
                       '<td>' + value.CREATEDBY + '</td>' +
                       '<td>' + value.ENCOUNTERTYPE + '</td>' +
                       '<td>' + value.STATUS + '</td>' +
                       '<td class="displaytd">' +
                        '<a data-toggle="tooltip" data-placement="top" title="View Encounter" class="p_view_btn btn btn-xs tab-navigation member-actions" data-tab-val="ViewEncounter"  data-tab-action="View Encounter" data-tab-title="View #' + value.ENCTRID + '" data-tab-container="fullBodyContainer" data-tab-autoflush="false" data-tab-path="~/Views/PBAS/CreateEncounter/_ViewEncounter.cshtml"><i class="fa fa-eye"></i></a>' +
                       '<a data-toggle="tooltip" title="Edit Encounter" class="p_edit_btn btn btn-xs tab-navigation member-actions" data-tab-val="EditEncounter"  data-tab-action="Edit Encounter"  data-tab-title="Edit #' + value.ENCTRID + '" data-tab-container="fullBodyContainer" data-tab-autoflush="false"  data-tab-path="~/Views/PBAS/CreateEncounter/_EditEncounter.cshtml"><i class="fa fa-edit"></i></a>' +
                       '<a data-toggle="tooltip" title="Cancel Encounter" class="p_delete_btn btn btn-xs" onclick="showCancelModal(' + value.ENCTRID + ')"><i class="fa fa-close"></i></a>' +
                       //'<a data-toggle="tooltip" title="Delete Encounter" class="p_delete_btn btn btn-xs" onclick="showDeleteModal(' + value.ENCTRID + ')"><i class="fa fa-trash"></i></a>'+
                       //'<a data-toggle="tooltip" title="No Show Encounter" class="p_delete_btn btn btn-xs" onclick="showNoshowModal(' + value.ENCTRID + ')"><i class="fa fa-ban"></i></a>' +
                       '<a data-toggle="tooltip" title="ReSchedule Encounter" class="p_cancel_btn btn btn-xs" onclick="showRescheduleModal(' + value.ENCTRID + ')"><i class="fa fa-history"></i></a>' +
                       '</td>' +
                       //'<input type="button" data-toggle="tooltip" title="View Encounter"  class="view-button tab-navigation member-actions btn-sm fa fa-view" data-tab-val="ViewEncounter"  data-tab-action="View Encounter" data-tab-title="View #'+value.ENCTRID+'" data-tab-container="fullBodyContainer" data-tab-autoflush="false" data-tab-path="~/Views/PBAS/CreateEncounter/_ViewEncounter.cshtml"/>' +
                       //'<input type="button" data-toggle="tooltip" title="Edit Encounter" class="edit-button tab-navigation member-actions btn-sm fa fa-edit" data-tab-val="EditEncounter"  data-tab-action="Edit Encounter"  data-tab-title="Edit #' + value.ENCTRID + '" data-tab-container="fullBodyContainer" data-tab-autoflush="false"  data-tab-path="~/Views/PBAS/CreateEncounter/_EditEncounter.cshtml"/>' +
                       //'<input type="button" data-toggle="tooltip" title="Delete Encounter" class="red-button btn-sm fa fa-trash" onclick="deleteEncounter(' + value.ENCTRID + ')"/>' + '</td>' +
                       '</tr>'
              );
});