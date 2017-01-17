 authSummaryTable = function () {
    $("#LevelRate").change(
       function () {
           var htmlTag = "/ -"
           switch ($("#LevelRate").val().toUpperCase()) {
               case null:
               case "":

                   htmlTag = "/ -";
                   break;

               case "Level 1".toUpperCase():
                   htmlTag = "/ 150";
                   break;
               case "Level 2".toUpperCase():
                   htmlTag = "/ 250";
                   break;
               case "Level 3".toUpperCase():
                   htmlTag = "/ 350";
                   break;
               case "Level 4".toUpperCase():
                   htmlTag = "/ 450";
                   break;
               case "Level 5".toUpperCase():
                   htmlTag = "/ 550";
                   break;
               default:
                   htmlTag = "";
                   break;

           }
           $("#LevelRateCost").val(htmlTag)

       }
       );
    $(".AuthSummaryDataClass").html('');
    var totalUnits = 0;
    for (var index = 0; index < $("#CPTArea").children().length; index++) {

        if ($("#PlaceOfService").val() == "12- PATIENT HOME" || $("#PlaceOfService").val() == "62- CORF") {
            try{
                totalUnits += (parseInt($("#CPTCodes_" + index + "__TotalUnits").val())?parseInt($("#CPTCodes_" + index + "__TotalUnits").val()):0);
            }catch(ex){
              
            }
            var summaryTemplate = '<tr>' +
                'html before' +

                        (($("#FromDateAuthTextBox").val() != null && $("#FromDateAuthTextBox").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#FromDateAuthTextBox").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +

                            (($("#ToDateAuthTextBox").val() != null && $("#ToDateAuthTextBox").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#ToDateAuthTextBox").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                             (($("#ServicingProvider_FullName").val() != null && $("#ServicingProvider_FullName").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="SVC PROVIDER" data-content="' + $("#ServicingProvider_FullName").val() + '">' + $("#ServicingProvider_FullName").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                            (($("#Facility_FullName").val() != null && $("#Facility_FullName").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="FACILITY NAME" data-content="' + $("#Facility_FullName").val() + '">' + $("#Facility_FullName").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                             (($("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() != null && $("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                             (($("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() != null && $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="PRIMARY DESC" data-content="' + $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() + '">' + $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                            (($("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() != null && $("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                              (($("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() != null && $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover"data-placement="left" data-trigger="hover" data-title="PROC DESC" data-content="' + $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() + '">' + $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')

                            +

                            (($("#CPTCodes_" + index + "__TotalUnits").val() != null && $("#CPTCodes_" + index + "__TotalUnits").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#CPTCodes_" + index + "__TotalUnits").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')

                          +
                             (($("#UMServiceGroup option:selected").text() != null && $("#UMServiceGroup option:selected").text() != '' && $("#UMServiceGroup option:selected").text().toUpperCase() != 'SELECT') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#UMServiceGroup option:selected").text() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                            'more html'
                            +
                            '</tr>';
        } else if ($("#PlaceOfService").val() == "22- OP HOSPITAL" && ($("#OutPatientType").val() == null || $("#OutPatientType").val() == "" || ($("#OutPatientType").val().toUpperCase() == "OP PROCEDURE" || $("#OutPatientType").val().toUpperCase() == "OP DIAGNOSTIC"))) {
            var summaryTemplate = '<tr>' +
                'html before' +
                              (($("#FromDateAuthTextBox").val() != null && $("#FromDateAuthTextBox").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#FromDateAuthTextBox").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +

                            (($("#ToDateAuthTextBox").val() != null && $("#ToDateAuthTextBox").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#ToDateAuthTextBox").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                            (($("#OutPatientType").val() != null && $("#OutPatientType").val() != '') ?
                             '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="OP TYPE" data-content="' + $("#OutPatientType").val() + '">' + $("#OutPatientType").val() + '</div></td>' :

                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +


                             (($("#AttendingProvider_FullName").val() != null && $("#AttendingProvider_FullName").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="ATT PROVIDER" data-content="' + $("#AttendingProvider_FullName").val() + '">' + $("#AttendingProvider_FullName").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                            (($("#Facility_FullName").val() != null && $("#Facility_FullName").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="FACILITY NAME" data-content="' + $("#Facility_FullName").val() + '">' + $("#Facility_FullName").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                             +
                            (($("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() != null && $("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data">' + $("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +
                            (($("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() != null && $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="PRIMARY DESC" data-content="' + $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() + '">' + $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +
                           (($("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() != null && $("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data">' + $("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +
                             (($("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() != null && $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover"data-placement="left" data-trigger="hover" data-title="PROC DESC" data-content="' + $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() + '">' + $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')


                            +

                            (($("#CPTCodes_" + index + "__RequestedUnits").val() != null && $("#CPTCodes_" + index + "__RequestedUnits").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#CPTCodes_" + index + "__RequestedUnits").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +


                          (($("#UMServiceGroup option:selected").text() != null && $("#UMServiceGroup option:selected").text() != '' && $("#UMServiceGroup option:selected").text().toUpperCase() != 'SELECT') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#UMServiceGroup option:selected").text() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                            'more html'
                            +
                              '</tr>';
        }
        else if ($("#PlaceOfService").val() == "22- OP HOSPITAL") {
            var summaryTemplate = '<tr>' +
                'html before' +
                              (($("#RequestDateOfService").val() != null && $("#RequestDateOfService").val() != '') ?
                         '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="SVC DATE" data-content="' + $("#RequestDateOfService").val() + '">' + $("#RequestDateOfService").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                              (($("#ExpDischrgeDate").val() != null && $("#ExpDischrgeDate").val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="SVC TO DATE" data-content="' + $("#ExpDischrgeDate").val() + '">' + $("#ExpDischrgeDate").val() + '</div></td>' :
                         '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                             (($("#AdmsnFromDate").val() != null && $("#AdmsnFromDate").val() != '') ?
                             '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="FROM DATE" data-content="' + $("#AdmsnFromDate").val() + '">' + $("#AdmsnFromDate").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                              (($("#DischrgeToDate").val() != null && $("#DischrgeToDate").val() != '') ?
                                '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="TO DATE" data-content="' + $("#DischrgeToDate").val() + '">' + $("#DischrgeToDate").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                            (($("#OutPatientType").val() != null && $("#OutPatientType").val() != '') ?
                             '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="OP TYPE" data-content="' + $("#OutPatientType").val() + '">' + $("#OutPatientType").val() + '</div></td>' :

                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +


                             (($("#AttendingProvider_FullName").val() != null && $("#AttendingProvider_FullName").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="ATT PROVIDER" data-content="' + $("#AttendingProvider_FullName").val() + '">' + $("#AttendingProvider_FullName").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                            (($("#Facility_FullName").val() != null && $("#Facility_FullName").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="FACILITY NAME" data-content="' + $("#Facility_FullName").val() + '">' + $("#Facility_FullName").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                             +
                            (($("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() != null && $("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data">' + $("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +
                            (($("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() != null && $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="PRIMARY DESC" data-content="' + $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() + '">' + $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +
                           (($("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() != null && $("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data">' + $("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +
                             (($("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() != null && $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover"data-placement="left" data-trigger="hover" data-title="PROC DESC" data-content="' + $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() + '">' + $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')


                            +
                               (($("#AdmsnApprovedDays").val() != null && $("#AdmsnApprovedDays").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#AdmsnApprovedDays").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')

                            +

  (($("#UMServiceGroup option:selected").text() != null && $("#UMServiceGroup option:selected").text() != '' && $("#UMServiceGroup option:selected").text().toUpperCase() != 'SELECT') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#UMServiceGroup option:selected").text() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                              '</tr>';
        }
        else if ($("#PlaceOfService").val() == "21- IP HOSPITAL") {
            var summaryTemplate = '<tr>' +
                'html before' +

                        (($("#ExpectedDateOfService").val() != null && $("#ExpectedDateOfService").val() != '') ?
                         '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="EXP DOS/DOA" data-content="' + $("#ExpectedDateOfService").val() + '">' + $("#ExpectedDateOfService").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                              (($("#ExpDischrgeDate").val() != null && $("#ExpDischrgeDate").val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="EXP DC DT" data-content="' + $("#ExpDischrgeDate").val() + '">' + $("#ExpDischrgeDate").val() + '</div></td>' :
                         '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                             (($("#AdmsnFromDate").val() != null && $("#AdmsnFromDate").val() != '') ?
                             '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="FROM DATE" data-content="' + $("#AdmsnFromDate").val() + '">' + $("#AdmsnFromDate").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                              (($("#DischrgeToDate").val() != null && $("#DischrgeToDate").val() != '') ?
                                '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="TO DATE" data-content="' + $("#DischrgeToDate").val() + '">' + $("#DischrgeToDate").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +

                             (($("#AttendingProvider_FullName").val() != null && $("#AttendingProvider_FullName").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="ATT PROVIDER" data-content="' + $("#AttendingProvider_FullName").val() + '">' + $("#AttendingProvider_FullName").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                            (($("#Facility_FullName").val() != null && $("#Facility_FullName").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="FACILITY NAME" data-content="' + $("#Facility_FullName").val() + '">' + $("#Facility_FullName").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                             (($("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() != null && $("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                             (($("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() != null && $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="PRIMARY DESC" data-content="' + $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() + '">' + $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                            (($("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() != null && $("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                              (($("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() != null && $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover"data-placement="left" data-trigger="hover" data-title="PROC DESC" data-content="' + $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() + '">' + $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')

                            +
                               (($("#AdmsnApprovedDays").val() != null && $("#AdmsnApprovedDays").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#AdmsnApprovedDays").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
            +
            'more html'
             +

                              '</tr>';
        }
        else if ($("#PlaceOfService").val() == "31- SNF") {
            var summaryTemplate = '<tr>' +
               'html before' +

                       (($("#ExpectedDateOfService").val() != null && $("#ExpectedDateOfService").val() != '') ?
                        '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="EXP DOS/DOA" data-content="' + $("#ExpectedDateOfService").val() + '">' + $("#ExpectedDateOfService").val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +
                             (($("#ExpDischrgeDate").val() != null && $("#ExpDischrgeDate").val() != '') ?
                          '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="EXP DC DT" data-content="' + $("#ExpDischrgeDate").val() + '">' + $("#ExpDischrgeDate").val() + '</div></td>' :
                        '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +
                            (($("#AdmsnFromDate").val() != null && $("#AdmsnFromDate").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="FROM DATE" data-content="' + $("#AdmsnFromDate").val() + '">' + $("#AdmsnFromDate").val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +
                             (($("#DischrgeToDate").val() != null && $("#DischrgeToDate").val() != '') ?
                               '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="TO DATE" data-content="' + $("#DischrgeToDate").val() + '">' + $("#DischrgeToDate").val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +

                            (($("#AttendingProvider_FullName").val() != null && $("#AttendingProvider_FullName").val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="ATT PROVIDER" data-content="' + $("#AttendingProvider_FullName").val() + '">' + $("#AttendingProvider_FullName").val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +
                           (($("#Facility_FullName").val() != null && $("#Facility_FullName").val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="FACILITY NAME" data-content="' + $("#Facility_FullName").val() + '">' + $("#Facility_FullName").val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +
                            (($("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() != null && $("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data">' + $("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +
                            (($("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() != null && $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="PRIMARY DESC" data-content="' + $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() + '">' + $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +
                           (($("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() != null && $("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data">' + $("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
                           +
                             (($("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() != null && $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover"data-placement="left" data-trigger="hover" data-title="PROC DESC" data-content="' + $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() + '">' + $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')

                           +
                              (($("#AdmsnApprovedDays").val() != null && $("#AdmsnApprovedDays").val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data">' + $("#AdmsnApprovedDays").val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')
            +
            (($("#LevelRate").val() != null && $("#LevelRate").val() != '') ?
                           '<td class="maxWidthtdXL theme_label_data">' + $("#LevelRate").val() + $("#LevelRateCost").val() + '</div></td>' :
                           '<td class="maxWidthtdXL theme_label_data">-</td>')

           +
           'more html'
            +

                             '</tr>';

        }
        else {
            var summaryTemplate = '<tr>' +
                'html before' +

                        (($("#FromDateAuthTextBox").val() != null && $("#FromDateAuthTextBox").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#FromDateAuthTextBox").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +

                            (($("#ToDateAuthTextBox").val() != null && $("#ToDateAuthTextBox").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#ToDateAuthTextBox").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                             (($("#ServicingProvider_FullName").val() != null && $("#ServicingProvider_FullName").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="SVC PROVIDER" data-content="' + $("#ServicingProvider_FullName").val() + '">' + $("#ServicingProvider_FullName").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                            (($("#Facility_FullName").val() != null && $("#Facility_FullName").val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="FACILITY NAME" data-content="' + $("#Facility_FullName").val() + '">' + $("#Facility_FullName").val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                             (($("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() != null && $("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#" + $("#ICDArea").children()[0].children[0].children[0].id).val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                             (($("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() != null && $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover" data-trigger="hover" data-title="PRIMARY DESC" data-content="' + $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() + '">' + $("#" + $("#ICDArea").children()[0].children[1].children[0].id).val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                            (($("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() != null && $("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#" + $("#CPTArea").children()[index].children[0].children[0].id).val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                              (($("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() != null && $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() != '') ?
                            '<td class="maxWidthtdXL theme_label_data"><div class="td-ellipsis theme_label_data" data-toggle="popover"data-placement="left" data-trigger="hover" data-title="PROC DESC" data-content="' + $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() + '">' + $("#" + $("#CPTArea").children()[index].children[2].children[0].id).val() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +

                             ( ( $("#UMServiceGroup option:selected").text() != null && $("#UMServiceGroup option:selected").text() != '' && $("#UMServiceGroup option:selected").text().toUpperCase() != 'SELECT') ?
                            '<td class="maxWidthtdXL theme_label_data">' + $("#UMServiceGroup option:selected").text() + '</div></td>' :
                            '<td class="maxWidthtdXL theme_label_data">-</td>')
                            +
                            'more html'
                            +
                            '</tr>';
        }
        $(".AuthSummaryDataClass").append(summaryTemplate);
    }
    if ($("#PlaceOfService").val() == "12- PATIENT HOME" || $("#PlaceOfService").val() == "62- CORF") {
        var AggregrateHeader = '<tr>' +
            '<td></td>' +
            '<td></td>' +
            '<td></td>' +
            '<td></td>' +
            '<td></td>' +
            '<td></td>' +
            '<td></td>' +
            '<td class="theme_label_data">TOTAL UNITS :</td>' +
            '<td class="theme_label_data">' + totalUnits + '</td>' +
            '<td></td>' +
             '</tr>';
        $(".AuthSummaryDataClass").append(AggregrateHeader);
    }

    $('[data-toggle="popover"]').popover();
}

$.fn.watch = function (property, callback) {
    return $(this).each(function () {
        var self = this;
        var old_property_val = this[property];
        var timer;

        function watch() {
            if ($(self).data(property + '-watch-abort') == true) {
                timer = clearInterval(timer);
                $(self).data(property + '-watch-abort', null);
                return;
            }

            if (self[property] != old_property_val) {
                old_property_val = self[property];
                callback.call(self);
            }
        }
        timer = setInterval(watch, 700);
    });
};

$.fn.unwatch = function (property) {
    return $(this).each(function () {
        $(this).data(property + '-watch-abort', true);
    });
};
$('#UM_auth_form').off('change', '.summaryDataChange').on('change', '.summaryDataChange', function () {
    setTimeout(function () { authSummaryTable() }, 600);
    setTimeout(function () {
        $('.summaryDataChange').watch('value', function () {
            $(this).change();
        });
    },500);
    
   
});
$('#UM_auth_form').off('blur', '.summaryDateChange').on('blur', '.summaryDateChange', function () {
    setTimeout(function () { authSummaryTable() }, 500);  
});
$('document').ready(function () {
    setTimeout(function () { authSummaryTable() }, 600);
    setTimeout(function () {
        $('.summaryDataChange').watch('value', function () {
            $(this).change();
        });
    }, 500);
});