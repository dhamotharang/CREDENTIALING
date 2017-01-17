$('#ScheduleEncounterContainer #MemberResult tbody tr').on('click', function () {
    var selectedMemberId = parseInt($(this).attr('data-container'));
    currentProgressBarData[3].parameters[0].value = selectedMemberId;
    MakeItActive(4, currentProgressBarData);
})


$('#ScheduleEncounterContainer #MemberResult tbody tr').on('click', function () {
    var selectedMemberId = parseInt($(this).attr('data-container'));
    currentProgressBarData[2].parameters[0].value = selectedMemberId;
    MakeItActive(3, currentProgressBarData);
})