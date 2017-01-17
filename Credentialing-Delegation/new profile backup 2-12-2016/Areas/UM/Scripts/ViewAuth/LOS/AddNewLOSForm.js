$(function () {
    $('#total_app').text('0');
    $('#total_denied').text('0');
    var currentdate = new Date();
    var datetime = (currentdate.getMonth() + 1) + "/" +
                    currentdate.getDate() + "/"
                  + currentdate.getFullYear() + " "
                  + currentdate.getHours() + ":"
                  + currentdate.getMinutes() + ":"
                  + currentdate.getSeconds();
    $('#recDate').val(datetime);
    $.each(MasterAuthorizationTypes, function (index, value) {
        $("#authtype").append($("<option />").val(value.AuthorizationTypeID).text(value.Name.toUpperCase()));
    });
    $('#authtype option[value="2"]').attr("selected", true);
    $.each(MasterPlacesOfService[1].POSRoomTypes, function (index, value) {
        $("#roomType").append($("<option />").val(value.TypeOfRoom.TypeOfRoomID).text(value.TypeOfRoom.RoomTypeName.toUpperCase()));
    });
});

$('#appLOS').on('keyup', function () {
    if ($('#appLOS').val() == '') {
        $('#total_app').text('0');
        $('#total_denied').text('0');
        $('#totalApp').val('');
        $('#totalDenied').val('');
        $('#denied').val('');
        $('#rowsForDenialDateAndReason').empty();
        return;
    }
    else if (parseInt($('#appLOS').val()) > parseInt($('#reqDate').val())) {
        $('#appLOS').val(parseInt($('#reqDate').val()));
    }
    var appLos = parseInt($('#appLOS').val());
    var reqLos = parseInt($('#reqDate').val());
    var deniedLos = reqLos - appLos;
    $('#total_app').text(appLos);
    $('#totalApp').val(appLos);
    $('#total_denied').text(deniedLos);
    $('#denied').val(deniedLos);
    $('#totalDenied').val(deniedLos);
    $('#rowsForDenialDateAndReason').empty();
    for (var i = 0; i < deniedLos; i++) {
        $('#rowsForDenialDateAndReason').append(
                '<div class="row">' +
                    '<div class="col-lg-4">' +
                        '<span class="theme_label">DENIAL DATE</span>' +
                        '<span class="summary_data">' +
                            '<span class="text-uppercase posexplanation theme_label_data">' +
                                '<input type="text" class="form-control inp" id="denial_date_' + i + '" placeholder="MM/DD/YYYY" />' +
                            '</span>' +
                        '</span>' +
                    '</div>' +
                    '<div class="col-lg-4">' +
                        '<span class="theme_label">REASON</span>' +
                        '<span class="summary_data">' +
                            '<span class="text-uppercase posexplanation theme_label_data">' +
                                '<select class="form-control inp" id="denial_reason_' + i + '"/>' +
                            '</span>' +
                        '</span>' +
                    '</div>' +
                '</div>'
        );
    }
    $("#rowsForDenialDateAndReason select").append($("<option />").val('select').text('SELECT'));
    $.each(MasterDeniedReasons, function (index, value) {
        $("#rowsForDenialDateAndReason select").append($("<option />").val(value.DenialLOSReasonID).text(value.ReasonDescription.toUpperCase()));
    });
});

$('#reqDate').on('keyup', function () {
    if ($('#reqDate').val() == '') {
        $('#total_app').text('0');
        $('#total_denied').text('0');
        $('#appLOS').val('');
        $('#totalApp').val('');
        $('#totalDenied').val('');
        $('#denied').val('');
        $('#appLOS').attr('disabled', true).addClass('read_only_field');
        $('#rowsForDenialDateAndReason').empty();
        return;
    }
    $('#appLOS').attr('disabled', false).removeClass('read_only_field');
    if (parseInt($('#appLOS').val()) > parseInt($('#reqDate').val())) {
        $('#appLOS').val(parseInt($('#reqDate').val()));
    }
    if ($('#reqDate').val() != '' && $('#appLOS').val() != '' && parseInt($('#appLOS').val()) <= parseInt($('#reqDate').val())) {
        var appLos = parseInt($('#appLOS').val());
        var reqLos = parseInt($('#reqDate').val());
        var deniedLos = reqLos - appLos;
        $('#total_app').text(appLos);
        $('#totalApp').val(appLos);
        $('#total_denied').text(deniedLos);
        $('#denied').val(deniedLos);
        $('#totalDenied').val(deniedLos);
        $('#rowsForDenialDateAndReason').empty();
        for (var i = 0; i < deniedLos; i++) {
            $('#rowsForDenialDateAndReason').append(
                    '<div class="row">' +
                        '<div class="col-lg-4">' +
                            '<span class="theme_label">DENIAL DATE</span>' +
                            '<span class="summary_data">' +
                                '<span class="text-uppercase posexplanation theme_label_data">' +
                                    '<input type="text" class="form-control inp" id="denial_date_' + i + '" placeholder="MM/DD/YYYY" />' +
                                '</span>' +
                            '</span>' +
                        '</div>' +
                        '<div class="col-lg-4">' +
                            '<span class="theme_label">REASON</span>' +
                            '<span class="summary_data">' +
                                '<span class="text-uppercase posexplanation theme_label_data">' +
                                    '<select class="form-control inp" id="denial_reason_' + i + '"/>' +
                                '</span>' +
                            '</span>' +
                        '</div>' +
                    '</div>'
            );
        }
        $("#rowsForDenialDateAndReason select").append($("<option />").val('select').text('SELECT'));
        $.each(MasterDeniedReasons, function (index, value) {
            $("#rowsForDenialDateAndReason select").append($("<option />").val(value.DenialLOSReasonID).text(value.ReasonDescription.toUpperCase()));
        });
    }
});