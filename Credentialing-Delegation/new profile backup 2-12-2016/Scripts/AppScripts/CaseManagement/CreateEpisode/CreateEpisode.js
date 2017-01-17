//*******************************
// AUTHOR: SAI JASWANTH PALUKURI
// DATE: SEPTEMBER 3, 2016
//*******************************


$(document).ready(function () {

    $('.PotentialProblems .RemoveBtn').click(function () {
        $(this).closest('li').remove();
    });

    $('.PotentialDiseases .RemoveBtn').click(function () {
        $(this).closest('li').remove();
    });

});

//SELECT DROPDOWN
$("#referredbyname").change(function () {
    GoToSIP();
});
// NAVIGATION TO SYSTEM IDENTIFIED PROBLEMS PARTIAL VIEW
$('#GoToSIPbtn').on('click', GoToSIP);

function GoToSIP() {

    $('#referredBy').hide();
    $('#rx').hide();
    $('#HRA_History').hide();
    $('#claims').hide();
    $('#um').hide();
    $('#notes').hide();
    $('#contacts').hide();
    $('#docs').hide();
    $('#letters').hide();
    $('#summary').hide();
    $('#decision').hide();
    $('#system_identified_problem').show();

}
//NAVIGATE BACK TO REFFERED BY PARTIAL VIEW
$('#BackToReferrredBy').on('click', GoToReferredBy);

function GoToReferredBy() {

    $('#system_identified_problem').hide();
    $('#referredBy').show();

}
// NAVIGATION TO RX PARTIAL VIEW
$('#GoToRXbtn').on('click', GoToRX);

function GoToRX() {

    $('#system_identified_problem').hide();
    // Getting RX_Data
    $.ajax({
        url: '/Resources/CM_JSON/CreateEpisode/RXData.txt',
        success: function (data) {
            var RX_Data = JSON.parse(data);
            var trHTML = '';
            for (var i = 0; i < RX_Data.length; i++) {
                trHTML += '<tr><td>' + RX_Data[i].DrugName + '</td> <td>' + RX_Data[i].DrugDosage + '</td> <td>' + RX_Data[i].DrugFrequency + '</td> <td>' + RX_Data[i].DrugRoute + '</td> <td>' + RX_Data[i].DC + '</td> <td><center><span style="color:green">' + RX_Data[i].SelfReported + '</span></center></td> <td><center>' + RX_Data[i].ClaimsReported + '</center></td> <td><center><span style="color:green">' + RX_Data[i].MemberReported + '</span></center></td> <td>' + RX_Data[i].LoadDate + '</td> <td>' + RX_Data[i].ModifiedDate + '</td></tr>';
            }
            $('#RX_Data').html(trHTML);
        }
    });

    $('#rx').show();
}
//NAVIGATE BACK TO SYSTEM IDENTIFIED PROBLEMS PARTIAL VIEW
$('#GoBackToSIP').on('click', GoToSIP2);

function GoToSIP2() {

    $('#rx').hide();
    $('#system_identified_problem').show();
}

//NAVIGATION TO HRA PARTIAL VIEW
$('#GoToHRAbtn').on('click', GoToHRA);

function GoToHRA() {
    $('#rx').hide();
    $('#HRA_History').show();
    var currentDate = getActualFullDate();
    // GETTING PRESENT DAY DATE (HRA_HISTORY)
    function addZero(i) {
        if (i < 10) {
            i = "0" + i;
        }
        return i;
    }
    function getActualFullDate() {
        var d = new Date();
        var day = addZero(d.getDate());
        var month = addZero(d.getMonth() + 1);
        var year = addZero(d.getFullYear());
        return month + "/ " + day + "/ " + year;
    }

    $.ajax({
        url: '/Resources/CM_JSON/CreateEpisode/HRAData.txt',
        success: function (data) {
            var HRA_Data = JSON.parse(data);
            var trHTML = '';
            for (var i = 0; i < HRA_Data.length; i++) {
                trHTML += '<tr  onclick="HRAFunction(' + HRA_Data[i].EventNumber + ')"> <td>' + HRA_Data[i].EventNumber + '</td> <td>' + HRA_Data[i].Type + '</td> <td>' + HRA_Data[i].StartDate + '</td> <td>' + HRA_Data[i].CaseNumber + '</td> <td>' + HRA_Data[i].CloseDate + '</td> <td>' + HRA_Data[i].Manager + '</td> <td>' + HRA_Data[i].HRA_CP + '</td><td><a class="fa fa-file-text" href="#" style="font-size:20px;"></a></td> </tr>';
            }
            $('#HRA_Data').html(trHTML);
            var HRA = {};
            HRAFunction = function (num) {
                $('#HRADoc').show();
                //$('#HRADoc').empty();
                for (var i = 0; i < HRA_Data.length; i++) {
                    if (HRA_Data[i].EventNumber == num) {
                        HRA = HRA_Data[i];
                        break;
                    }
                }
                var trHTML1 = '';
                console.log(HRA)
                var sample = '';
                var InitiatedBy;
                for (var i = 0; i < HRA.HealthRiskAssesment.length; i++) {
                    trHTML1 += '<tr> <td>' + HRA.EventNumber + '</td> <td contenteditable="false">' + HRA.HealthRiskAssesment[i].Closed + '</td> <td>' + HRA.Type + '</td> <td>' + HRA.CaseNumber + '</td> <td contenteditable="false">' + HRA.HealthRiskAssesment[i].InitiatedDate + '</td> <td contenteditable="false">' + HRA.HealthRiskAssesment[i].InitiatedBy + '</td> <td contenteditable="false">' + HRA.HealthRiskAssesment[i].Completed + '</td> <td contenteditable="false">' + HRA.HealthRiskAssesment[i].CompletedBy + '</td> </tr>';
                }
                sample = '<div class="summary_sectionsDiv"> <fieldset class="fsStyle"> <legend class="legendStyle"> <span class="text-uppercase"> Health Risk Assessment : : ' + HRA.EventNumber + '</span> </legend> <div class="panel-body"> <a class="btn btn-xs btn-info pull-right"> <i class="fa fa-print">&nbsp; Print</i> </a> <a class="btn btn-xs btn-success pull-right" onclick="ReAssessment()"> <i class="fa fa-plus">&nbsp; Re-Assesment</i> </a> <div class="col-lg-12"> <br /> </div> <div class="col-lg-12"> <table class="table table-striped custom-thead-back custom-thead-font custom-tbody custom-table-striped" id="myHRATable"> <thead class="theme_thead"> <tr> <td>EVENT #</td> <td>CLOSED</td> <td>TYPE</td> <td>CASE #</td> <td>INITIATED</td> <td>INITIATED BY</td> <td>COMPLETED</td> <td>COMPLETED BY</td> </tr> </thead> <tbody>' + trHTML1 + '</tbody> </table> </div> </div> </fieldset> </div>';
                $('#HRADoc').html(sample);
            }
            ReAssessment = function () {
                $('#myHRATable tr:last').after('<tr> <td>' + HRA.EventNumber + '</td> <td contenteditable="true"></td> <td>' + HRA.Type + '</td> <td>' + HRA.CaseNumber + '</td> <td contenteditable="true">' + currentDate + '</td> <td contenteditable="true"></td> <td contenteditable="true"></td> <td contenteditable="true"></td> </tr>');
            }
        }
    });

}

// NAVIGATE BACK TO RX PARTIAL VIEW
$('#GoBackToRX').on('click', GoBackToRX);

function GoBackToRX() {

    $('#HRA_History').hide();
    $('#rx').show();

}

//NAVIGATION TO CLAIMS PARTIAL VIEW
$('#GotoClaims').on('click', GoToClaims);

function GoToClaims() {


    $('#HRA_History').hide();
    $('#claims').show();
    var claimsnum;
    $.ajax({
        url: '/Resources/CM_JSON/CreateEpisode/ClaimsData.txt',
        success: function (data, textStatus, XMLHttpRequest) {
            Claims_Data = JSON.parse(data);
            var trHTML = '';
            for (var i = 0; i < Claims_Data.length; i++) {
                var svDesc = '';
                for (var j = 0; j < Claims_Data[i].svDesc.length; j++) {
                    svDesc += "<tr class='text-uppercase wrap-words'><td>" + Claims_Data[i].svDesc[j].code + "</td><td>" + Claims_Data[i].svDesc[j].desc + "</td></tr>";
                }
                var svDescription = '';
                svDescription = "<div class='table_popover'><table class='table table-border table-striped text-uppercase'><tr class='textalign'><th>SERVICE CODE</th><th>SERVICE DESC</th></tr>" + svDesc + "</table></div>";
                var icdDesc = '';
                for (var k = 0; k < Claims_Data[i].icdDesc.length; k++) {
                    icdDesc += "<tr class='text-uppercase wrap-words'><td>" + Claims_Data[i].icdDesc[k].code + "</td><td>" + Claims_Data[i].icdDesc[k].desc + "</td></tr>";
                }
                var icdDescription = "<div class='table_popover'><table class='table table-border table-striped text-uppercase'><tr class='textalign'><th>SERVICE CODE</th><th>SERVICE DESC</th></tr>" + icdDesc + "</table></div>";
                trHTML += '<tr onclick="ClaimDetails(' + Claims_Data[i].claimsNo + ')"> <td>' + Claims_Data[i].MemberName + '</td> <td>' + Claims_Data[i].claimsNo + '</td> <td>' + Claims_Data[i].BillType + '</td> <td>' + Claims_Data[i].dosFrom + '</td> <td>' + Claims_Data[i].dosTo + '</td> <td class="facility">' + Claims_Data[i].Facility + '<a data-toggle="popover" data-trigger="hover" data-placement="top" data-html="true" data-content="' + Claims_Data[i].Facility + '" title="" data-original-title="FACILITY">...</a></td> <td>' + Claims_Data[i].POS + '</td> <td class="icdDesc">' + Claims_Data[0].icdDesc[0].code + ' : ' + Claims_Data[0].icdDesc[0].desc + '<a data-toggle="popover" data-trigger="hover" data-placement="top" data-html="true" data-content="' + icdDescription + '">...</a></td> <td class="svDesc">' + Claims_Data[0].svDesc[0].code + ' : ' + Claims_Data[0].svDesc[0].desc + '<a data-toggle="popover" data-trigger="hover" data-placement="top" data-html="true" data-content="' + svDescription + '">...</a></td> <td>$' + Claims_Data[i].Charge + '</td> <td>' + Claims_Data[i].PaidDate + '</td> <td>$' + Claims_Data[i].netPaid + '</td> </tr>';
                //console.log(trHTML);
            }
            $('#allpartATable').html(trHTML);
            $('#hospTable').html(trHTML);
            $('#snfTable').html(trHTML);
            $('#hhcTable').html(trHTML);
            $('#allpartBTable').html(trHTML);
            $('#dmeTable').html(trHTML);
            $('#specTable').html(trHTML);
            $('#diagnosticsTable').html(trHTML);
            $('#partBdrugsTable').html(trHTML);
            $('#partDTable').html(trHTML);

            $('[data-toggle="popover"]').popover();


            var CLAIMS = '';
            ClaimDetails = function (claimsnum) {
                ShowModal('~/Views/CaseManagement/CreateEpisode/CreateEpisodeModals/_ClaimsModal.cshtml', 'View Claims');
                setTimeout(function () {
                    for (var i = 0; i < Claims_Data.length; i++) {
                        if (Claims_Data[i].claimsNo == claimsnum) {
                            CLAIMS = Claims_Data[i];
                            break;
                        }
                    }
                    var claimsno = CLAIMS.claimsNo;
                    var dosFrom = CLAIMS.dosFrom;
                    var dosThru = "";
                    var los = CLAIMS.los;
                    var typeOfAdmission = CLAIMS.toa;
                    var Cfacility = CLAIMS.Facility;
                    var drugDesc = CLAIMS.DrugDesc;
                    var netPaid = CLAIMS.netPaid;

                    $("#claimsno").val(claimsno);
                    $("#dosFrom").val(dosFrom);
                    $("#dosThru").val("");
                    $("#los").val(los);
                    $("#typeOfAdmission").val(typeOfAdmission);
                    $("#Cfacility").val(Cfacility);
                    $("#drugDesc").val(drugDesc);
                    $("#netPaid").val(netPaid);

                    var svD = '';
                    for (var i = 0; i < CLAIMS.svDesc.length; i++) {
                        svD += '<div class="col-lg-12 col-md-12"> <input type="text" value="' + CLAIMS.svDesc[i].code + ' : ' + CLAIMS.svDesc[i].desc + '" class="form-control inp" disabled=""> </div>';
                    }
                    $('#svData').html(svD);
                    var icD = '';
                    for (var i = 0; i < CLAIMS.icdDesc.length; i++) {
                        icD += '<div class="col-lg-12 col-md-12"> <input type="text" value="' + CLAIMS.icdDesc[i].code + ' : ' + CLAIMS.icdDesc[i].desc + '" class="form-control inp" disabled=""> </div>';
                    }
                    $('#icdData').html(icD);
                    var cpt = '';
                    for (var i = 0; i < CLAIMS.cptDesc.length; i++) {
                        cpt += '<div class="col-lg-12 col-md-12"> <input type="text" value="' + CLAIMS.cptDesc[i].code + ' : ' + CLAIMS.cptDesc[i].desc + '" class="form-control inp" disabled=""> </div>';
                    }
                    $('#cptData').html(cpt);
                    var prov = '';
                    for (var i = 0; i < CLAIMS.provider.length; i++) {
                        prov += '<span class="col-lg-4"> <input type="text" class="form-control inp" value="' + CLAIMS.provider[i].name + ' : ' + CLAIMS.provider[i].type + '" disabled=""> </span>';
                    }
                    $('#providerData').html(prov);
                }, 1000)
            }
        }
    });

    // Graph Under Total Tile (Claims Tab)
    HighCostClaimsChart = c3.generate({
        bindto: '#HighCostClaimsReport',
        data: {
            x: 'x',
            columns: [
                 ['x', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
               ['Cost', 150, 300, 600, 150, 300, 300, 900, 300, 150, 600, 300, 150],
                //['TrendLine', 150, 300, 600, 150, 300, 300, 900, 300, 150, 600, 300, 150],
                ['Benchmark', 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, ]
            ],
            //labels: true,
            type: 'bar',
            types: {

                //TrendLine: 'line',
                Benchmark: 'line'

            },
        },
        point: {
            show: false
        },
        legend: {
            show: true
        },
        bar: {
            width: {
                ratio: 0.5 // this makes bar width 50% of length between ticks
            }
            // or
            //width: 100 // this makes bar width 100px
        },
        axis: {
            x: {
                label: '',
                type: 'category', // this needed to load string x value
                tick: {
                    rotate: 0,
                    multiline: true
                },
                height: 50
            },
            y: {
                label: 'Amount($)'
            }
        }
    });

    // Graph Under PART A Tile (Claims Tab)
    HighCostClaimsChart1 = c3.generate({
        bindto: '#HighCostClaimsReport1',
        data: {
            x: 'x',
            columns: [
                 ['x', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                 ['Cost', 50, 100, 200, 50, 100, 100, 300, 100, 50, 200, 100, 50],
                //['TrendLine', 50, 100, 200, 50, 100, 500, 300, 100, 50, 200, 100, 50],
                ['Benchmark', 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, ]
            ],
            //labels: true,
            type: 'bar',
            types: {

                //TrendLine: 'line',
                Benchmark: 'line'

            },
        },
        point: {
            show: false
        },
        legend: {
            show: true
        },
        bar: {
            width: {
                ratio: 0.5 // this makes bar width 50% of length between ticks
            }
            // or
            //width: 100 // this makes bar width 100px
        },
        axis: {
            x: {
                label: '',
                type: 'category', // this needed to load string x value
                tick: {
                    rotate: 0,
                    multiline: true
                },
                height: 50
            },
            y: {
                label: 'Amount($)'
            }
        }
    });

    // Graph Under PART B Tile (Claims Tab)
    HighCostClaimsChart2 = c3.generate({
        bindto: '#HighCostClaimsReport2',
        data: {
            x: 'x',
            columns: [
                ['x', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                ['Cost', 50, 100, 200, 50, 100, 100, 300, 100, 50, 200, 100, 50],
                //['TrendLine', 50, 100, 200, 50, 100, 500, 300, 100, 50, 200, 100, 50],
                ['Benchmark', 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, ]
            ],
            //labels: true,
            type: 'bar',
            types: {

                //TrendLine: 'line',
                Benchmark: 'line'

            },
        },
        point: {
            show: false
        },
        legend: {
            show: true
        },
        bar: {
            width: {
                ratio: 0.5 // this makes bar width 50% of length between ticks
            }
            // or
            //width: 100 // this makes bar width 100px
        },
        axis: {
            x: {
                label: '',
                type: 'category', // this needed to load string x value
                tick: {
                    rotate: 0,
                    multiline: true
                },
                height: 50,

            },
            y: {
                label: 'Amount($)'
            }
        }
    });


    // Graph Under PART D Tile (Claims Tab)
    HighCostClaimsChart3 = c3.generate({
        bindto: '#HighCostClaimsReport3',
        data: {
            x: 'x',
            columns: [
                 ['x', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                 ['Cost', 50, 100, 200, 50, 100, 100, 300, 100, 50, 200, 100, 50],
                //['TrendLine', 50, 100, 200, 50, 100, 500, 300, 100, 50, 200, 100, 50],
                ['Benchmark', 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, ]
            ],
            //labels: true,
            type: 'bar',
            types: {

                //TrendLine: 'line',
                Benchmark: 'line'

            },
        },
        point: {
            show: false
        },
        legend: {
            show: true
        },
        bar: {
            width: {
                ratio: 0.5 // this makes bar width 50% of length between ticks
            }
            // or
            //width: 100 // this makes bar width 100px
        },
        axis: {
            x: {
                label: '',
                type: 'category', // this needed to load string x value
                tick: {
                    rotate: 0,
                    multiline: true
                },
                height: 50,

            },
            y: {
                label: 'Amount($)',

            }
        }
    });

}
//NAVIGATE BACK TO HRA PARTIAL VIEW
$('#GoBackToHRA').on('click', GoBackToHRA);

function GoBackToHRA() {

    $('#claims').hide();
    $('#HRA_History').show();
}
//NAVIGATION TO UM PARTIAL VIEW
$('#GoToUMbtn').on('click', GoToUM);

function GoToUM() {

    $('#claims').hide();

    $.ajax({
        url: '/Resources/CM_JSON/CreateEpisode/ClaimsData.txt',
        success: function (data, textStatus, XMLHttpRequest) {
            Claims_Data = JSON.parse(data);
            var trHTML1 = '';
            for (var i = 0; i < 1; i++) {
                var svDesc1 = '';
                for (var j = 0; j < Claims_Data[i].svDesc.length; j++) {
                    svDesc1 += "<tr class='text-uppercase wrap-words'><td>" + Claims_Data[i].svDesc[j].code + "</td><td>" + Claims_Data[i].svDesc[j].desc + "</td></tr>";
                }
                var svDescription1 = '';
                svDescription1 = "<div class='table_popover'><table class='table table-border table-striped text-uppercase'><tr class='textalign'><th>SERVICE CODE</th><th>SERVICE DESC</th></tr>" + svDesc1 + "</table></div>";
                
                trHTML1 += '<tr style="font-weight: bold; text-transform: uppercase"> <td> <button class="btn btn-success btn-xs" id="Tableshow"><i class="fa fa-plus"></i> </button> <button class="btn btn-success btn-xs" id="Tablehide"><i class="fa fa-minus"></i> </button> </td> <td><span class="label label-primary">OFC</span></td> <td>1607120029</td> <td>07/12/2016</td> <td>10/10/2016</td> <td>VODKZ NW RCSDKC</td> <td><span class="label label-success">STANDARD</span></td> <td>TAMPA GENERAL HOSPITAL</td> <td>PS</td> <td>07/12/2016</td> <td>1</td> <td>PEND-MD - PS</td> <td>11</td> <td>M13.111</td> <td><button class="btn btn-primary btn-xs">View</button></td> </tr> <tr id="ExpandTR1" hidden> <th></th> <th style="font-weight:bold">EXP DOS</th> <th style="font-weight:bold">FROM DATE</th> <th style="font-weight:bold">TO DATE</th> <th style="font-weight:bold">SVC PROVIDER</th> <th style="font-weight:bold">PRIMARY DX</th> <th style="font-weight:bold">DX DESC</th> <th style="font-weight:bold">POS</th> <th style="font-weight:bold">PROC CODE</th> <th style="font-weight:bold">PROC DESC</th> <th style="font-weight:bold">REQ UNITS</th> <th style="font-weight:bold">AUTH UNITS</th> <th style="font-weight:bold"></th> <th style="font-weight:bold"></th> <th style="font-weight:bold"></th> </tr> <tr id="ExpandTR2" hidden style="font-weight: bold; text-transform: uppercase"> <td></td> <td>07/12/2016</td> <td>07/12/2016</td> <td>10/10/2016</td> <td>CNVYXIOB VOKRMSW</td> <td>' + Claims_Data[0].icdDesc[1].code + '</td> <td>' + Claims_Data[0].icdDesc[1].desc + '</td> <td>11(A)</td> <td>' + Claims_Data[0].svDesc[0].code + '</td> <td>' + Claims_Data[0].svDesc[0].desc + '<a data-toggle="popover" data-trigger="hover" data-placement="bottom" data-html="true" data-content="' + svDescription1 + '">...</a></td> <td>2</td> <td>0</td> <td></td> <td></td> <td></td> </tr>';
                //console.log(trHTML);
            }

            $('#UMDataRows').html(trHTML1);
            $('[data-toggle="popover"]').popover();
        }
    });

    $('#um').show();

    

    function clickHandler() {
        $('#Tablehide').toggle();
        $('#Tableshow').toggle();
        $('#ExpandTR1').toggle();
        $('#ExpandTR2').toggle();
    }

    $(document).ready(function () {
        $('#Tablehide').hide();

        $('#Tablehide, #Tableshow').on('click', clickHandler);
    });

    

}
// NAVIGATE BACK TO CLAIMS PARTIAL VIEW
$('#GoBackToClaims').on('click', GoBackToClaims);

function GoBackToClaims() {

    $('#um').hide();
    $('#claims').show();
}
//NAVIGATION TO NOTES PARTIAL VIEW
$('#GotoNotes').on('click', GoToNotes);

function GoToNotes() {

    $('#um').hide();
    $('#notes').show();

    $.ajax({
        url: '/Resources/CM_JSON/CreateEpisode/CMNotes.txt',
        success: function (data) {
            var Notes_Data = JSON.parse(data);
            var trHTML = '';
            for (var i = 0; i < Notes_Data.length; i++) {
                trHTML += '<tr> <td>' + Notes_Data[i].Module + '</td> <td> 1506206 </td> <td>' + Notes_Data[i].NoteType + '</td> <td>' + Notes_Data[i].DateTime + '</td> <td>' + Notes_Data[i].UserName + '</td> <td>' + Notes_Data[i].Subject + '</td> <td>' + Notes_Data[i].Note + '<a data-toggle="popover" data-trigger="hover" data-placement="top" data-html="true" data-content="' + Notes_Data[i].Note + '" title="" data-original-title="NOTE">...</a></td> <td> <button class="copy-button" onclick="NoteDetails(' + Notes_Data[i].EventId + ')">View</button></td></tr>';
            }
            $('#Notes_Data').html(trHTML);
            $('[data-toggle="popover"]').popover();
            var NOTES = '';
            NoteDetails = function (Eventnum) {
                ShowModal('~/Views/CaseManagement/CreateEpisode/CreateEpisodeModals/_NotesModal.cshtml', 'View Note Details');
                setTimeout(function () {
                    for (var i = 0; i < Notes_Data.length; i++) {
                        if (Notes_Data[i].EventId == Eventnum) {
                            NOTES = Notes_Data[i];
                            break;
                        }
                    }
                    var NoteType = NOTES.NoteType;
                    var DateTime = NOTES.DateTime;
                    var Note = NOTES.Note;
                    $("#noteType").val(NoteType);
                    $("#dateTime").val(DateTime);
                    $("#fullnote").val(Note);
                }, 1000)
            }
        }
    });

    //select all checkboxes
    $("#checkAll").change(function () {  //"select all" change 
        $(".checkbox-radio").prop('checked', $(this).prop("checked")); //change all ".checkbox" checked status
    });

    //".checkbox" change 
    $('.checkbox-radio').change(function () {
        //uncheck "select all", if one of the listed checkbox item is unchecked
        if (false == $(this).prop("checked")) { //if this item is unchecked
            $("#checkAll").prop('checked', false); //change "select all" checked status to false


        }
        //check "select all" if all checkbox items are checked       
        if ($('.checkbox-radio:checked').length == ($('.checkbox-radio').length) - 1) {
            $("#checkAll").prop('checked', true);
        }
    });

}

//NAVIGATE BACK TO UM PARTIAL VIEW
$('#GoBackToUM').on('click', GoBackToUM);

function GoBackToUM() {

    $('#notes').hide();
    $('#um').show();
}

//NAVIGATION TO CONTACTS PARTIAL VIEW
$('#GotoContacts').on('click', GoToContacts);

function GoToContacts() {
    $('#notes').hide();
    $('#contacts').show();

    $.ajax({
        url: '/Resources/CM_JSON/CreateEpisode/ContactData.txt',
        success: function (data) {
            var Contact_Data = JSON.parse(data);
            var trHTML = '';
            for (var i = 0; i < Contact_Data.length; i++) {
                popover = '<div>' + Contact_Data[i].Notes + '<br> <b> REASON : </b><span>' + Contact_Data[i].Reason + '</span> <br><b>OUTCOME : </b><span>' + Contact_Data[i].Outcome + '</span></div>';
                trHTML += '<tr><td>' + Contact_Data[i].Module + '</td> <td>' + Contact_Data[i].EpisodeID + '</td> <td>' + Contact_Data[i].Entity + '</td> <td>' + Contact_Data[i].ContactName + '</td> <td>' + Contact_Data[i].Type + '</td> <td>' + Contact_Data[i].ContactNumber + '</td> <td> ' + Contact_Data[i].Direction + '</td> <td>' + Contact_Data[i].OutcomeType + ' </td>  <td>' + Contact_Data[i].Notes + '<a data-toggle="popover" data-trigger="hover" data-placement="top" data-html="true" data-content="' + popover + '" title="" data-original-title="NOTE">...</a> </td>  <td>' + Contact_Data[i].DateTime + ' </td>  <td> ' + Contact_Data[i].CreatedBy + '</td>  <td><button class="copy-button" onclick="ContactDetails(' + Contact_Data[i].EpisodeID + ')">View</button> </td></tr>';
            }
            $('#Contact_Data').html(trHTML);
            $('[data-toggle="popover"]').popover();
            var CONTACTDETAILS = '';
            ContactDetails = function (EpisodeID) {
                ShowModal('~/Views/CaseManagement/CreateEpisode/CreateEpisodeModals/_ContactModal.cshtml', 'View Note Details');
                setTimeout(function () {
                    for (var i = 0; i < Contact_Data.length; i++) {
                        if (Contact_Data[i].EpisodeID == EpisodeID) {
                            CONTACTDETAILS = Contact_Data[i];
                            break;
                        }
                    }
                    var Type = CONTACTDETAILS.Type;
                    var Entity = CONTACTDETAILS.Entity;
                    var ContactName = CONTACTDETAILS.ContactName;
                    var ContactNumber = CONTACTDETAILS.ContactNumber;
                    var Direction = CONTACTDETAILS.Direction;
                    var DateAndTime = CONTACTDETAILS.DateTime;
                    var ACN = CONTACTDETAILS.AltContactNUmber;
                    var Reason = CONTACTDETAILS.Reason;
                    var OutcomeType = CONTACTDETAILS.OutcomeType;
                    var Outcome = CONTACTDETAILS.Outcome;
                    var Note = CONTACTDETAILS.Notes;

                    $("#type").val(Type);
                    $("#entity").val(Entity);
                    $("#contactname").val(ContactName);
                    $("#CN").val(ContactNumber);
                    $("#Direction").val(Direction);
                    $("#DateAndTime").val(DateAndTime);
                    $("#ACN").val(ACN);
                    $("#Reason").val(Reason);
                    $("#OType").val(OutcomeType);
                    $("#Outcome").val(Outcome);
                    $("#fullcontactnote").val(Note);
                }, 1000)
            }
        }
    });


    //select all checkboxes
    $("#selectAll").change(function () {  //"select all" change 
        $(".checkbox-radio").prop('checked', $(this).prop("checked")); //change all ".checkbox" checked status
    });

}

$('#GoBackToNotes').on('click', GoBackToNotes);

function GoBackToNotes() {

    $('#contacts').hide();
    $('#notes').show();
}

//NAVIGATION TO DOCUMENTS PARTIAL VIEW
$('#GotoDocs').on('click', GotoDocs);

function GotoDocs() {


    $('#contacts').hide();
    $('#docs').show();

    $.ajax({
        url: '/Resources/CM_JSON/CreateEpisode/DocumentsData.txt',
        success: function (data) {
            var Doc_Data = JSON.parse(data);
            var trHTML = '';
            for (var i = 0; i < Doc_Data.length; i++) {
                trHTML += '<tr onclick="DocDetails(' + Doc_Data[i].EpisodeRefID + ')"> <td>CM</td> <td>' + Doc_Data[i].DocumentDateTime + '</td> <td>' + Doc_Data[i].DocumentID + '</td> <td>' + Doc_Data[i].EpisodeRefID + '</td> <td>' + Doc_Data[i].DocumentName + '</td> <td>' + Doc_Data[i].DocumentType + '</td> <td>' + Doc_Data[i].CreatedBy + '</td> <td><a class="fa fa-file-text" href="#" style="font-size:20px;"></a></td> </tr>';
            }
            $('#Document_Data').html(trHTML);
            var DocumentDetails = '';
            DocDetails = function (EpisodeID) {
                ShowModal('~/Views/CaseManagement/CreateEpisode/CreateEpisodeModals/_DocumentsModal.cshtml', 'View Document Details');
                setTimeout(function () {
                    for (var i = 0; i < Doc_Data.length; i++) {
                        if (Doc_Data[i].EpisodeRefID == EpisodeID) {
                            DocumentDetails = Doc_Data[i];
                            break;
                        }
                    }
                    var DocCategory = DocumentDetails.DocumentCategory;
                    var DTime = DocumentDetails.DocumentDateTime;
                    var DocID = DocumentDetails.DocumentID;
                    var RefID = DocumentDetails.EpisodeRefID;
                    var DocName = DocumentDetails.DocumentName;
                    var DocType = DocumentDetails.DocumentType;
                    var CreatedBy = DocumentDetails.CreatedBy;

                    $("#DocCategory").val(DocCategory);
                    $("#DTime").val(DTime);
                    $("#DocID").val(DocID);
                    $("#RefID").val(RefID);
                    $("#DocName").val(DocName);
                    $("#DocType").val(DocType);
                    $("#CreatedBy").val(CreatedBy);

                }, 1000)
            }
        }
    });

    //select all checkboxes
    $("#showAll").change(function () {  //"select all" change 
        $(".checkbox-radio").prop('checked', $(this).prop("checked")); //change all ".checkbox" checked status
    });

    //".checkbox" change 
    $('.checkbox-radio').change(function () {
        //uncheck "select all", if one of the listed checkbox item is unchecked
        if (false == $(this).prop("checked")) { //if this item is unchecked
            $("#showAll").prop('checked', false); //change "select all" checked status to false
        }
        //check "select all" if all checkbox items are checked       
        if ($('.checkbox-radio:checked').length == ($('.checkbox-radio').length) - 1) {
            $("#showAll").prop('checked', true);
        }
    });

}


//NAVIGATE BACK TO CONTACTS
$('#GoBackToContacts').on('click', GoBackToContacts);

function GoBackToContacts() {

    $('#docs').hide();
    $('#contacts').show();
}

//NAVIGATION TO LETTERS PARTIAL VIEW
$('#GotoLetters').on('click', GotoLetters);

function GotoLetters() {


    $('#docs').hide();
    $('#letters').show();

    $.ajax({
        url: '/Resources/CM_JSON/CreateEpisode/LettersData.txt',
        success: function (data) {
            var Letters_Data = JSON.parse(data);
            var trHTML = '';
            for (var i = 0; i < Letters_Data.length; i++) {
                trHTML += '<tr> <td>' + Letters_Data[i].LetterEntity + '</td> <td>' + Letters_Data[i].LetterTemplateName + '</td> <td>' + Letters_Data[i].MailDate + '</td> <td>' + Letters_Data[i].SentBy + '</td> <td>' + Letters_Data[i].BatchNumber + '</td> <td><a class="fa fa-file pointer" href="http://www.nursecredentialing.org/NCM-SampleChapter" style="font-size:20px;"></a></td> </tr>';
            }
            $('#Letters_Data').html(trHTML);
        }
    });
}
// NAVIGATE BACK TO DOCUMENTS PARTIAL VIEW
$('#GoBackToDocs').on('click', GoBackToDocs);

function GoBackToDocs() {

    $('#letters').hide();
    $('#docs').show();
}

//NAVIGATION TO SUMMARY PARTIAL VIEW
$('#GotoSummary').on('click', GotoSummary);

function GotoSummary() {
    $('#letters').hide();

    $('#summary').show();

    $('#AddProblems').on('click', AddProblems);

    function AddProblems() {

        ShowModal('~/Views/CaseManagement/CreateEpisode/CreateEpisodeModals/_AddProblems(Summary).cshtml', 'Add Potential Problems');
    }

    $('#AddDiseases').on('click', AddDiseases);

    function AddDiseases() {

        ShowModal('~/Views/CaseManagement/CreateEpisode/CreateEpisodeModals/_AddDiseases(Summary).cshtml', 'Add Potential Diseases');
    }

}
//NAVIGATE BACK TO LETTERS PARTIAL VIEW
$('#GoBackToLetters').on('click', GoBackToLetters);

function GoBackToLetters() {

    $('#summary').hide();
    $('#letters').show();
}


$('#GotoDecision').on('click', GotoDecision);

function GotoDecision() {

    $('#summary').hide();
    $('#decision').show();
}

$('#GoBackToSummary').on('click', GoBackToSummary);

function GoBackToSummary() {

    $('#decision').hide();
    $('#summary').show();
}




//OUTER TABS IN CLAIMS
$(".tabs-menu a").click(function (event) {
    event.preventDefault();
    $(this).parent().addClass("current");
    $(this).parent().siblings().removeClass("current");
    var tab = $(this).attr("href");
    $(this).parents('.viewAuthTabsContainer').siblings().children(".customtab-content").not(tab).css("display", "none");
    $(this).parents('.viewAuthTabsContainer').siblings().children(tab).css("display", "block");
    // $(".customtab-content").not(tab).css("display", "none");
    $(this).parents('.viewAuthTabsContainer').siblings().children(tab).fadeIn();
});

//INNER TABS IN CLAIMS
$(".innertabs-menu a").click(function (event) {
    event.preventDefault();
    $(this).parent().addClass("current");
    $(this).parent().siblings().removeClass("current");
    var tab = $(this).attr("href");
    $(this).parents('.innerTabsContainer').siblings().children(".innercustomtab-content").not(tab).css("display", "none");
    $(this).parents('.innerTabsContainer').siblings().children(tab).css("display", "block");
    // $(".customtab-content").not(tab).css("display", "none");
    $(this).parents('.innerTabsContainer').siblings().children(tab).fadeIn();
});






