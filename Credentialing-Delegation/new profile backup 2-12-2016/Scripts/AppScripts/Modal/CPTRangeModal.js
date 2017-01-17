//**************************//
// AUTHOR: RAHUL TEJA       //
// CREATED DATE: 11-09-2016 //
//**************************//
$(function () {
     var AllCPTCodes =  [
    {
      "Code": "J1725",
      "ShortDescription": "INJECTION HPC 1 MG",
      "status": "Active"
    },
    {
      "Code": "J1730",
      "ShortDescription": "INJECTION DIAZOXIDE UP TO 300 MG",
      "status": "Active"
    },
    {
      "Code": "J1740",
      "ShortDescription": "INJECTION IBANDRONATE SODIUM 1 MG",
      "status": "Active"
    },
    {
      "Code": "J1741",
      "ShortDescription": "INJECTION IBUPROFEN 100 MG",
      "status": "Active"
    },
    {
      "Code": "J1742",
      "ShortDescription": "INJ IBUTILIDE FUMARATE 1 MG",
      "status": "Active"
    },
    {
      "Code": "J1743",
      "ShortDescription": "INJECTION  IDURSULFASE  1 MG",
      "status": "Active"
    },
    {
      "Code": "J1744",
      "ShortDescription": "INJECTION ICATIBANT 1 MG",
      "status": "Active"
    },
    {
      "Code": "J1745",
      "ShortDescription": "INJECTION INFLIXIMAB 10 MG",
      "status": "Active"
    },
    {
      "Code": "J1750",
      "ShortDescription": "INJECTION IRON DEXTRAN 50 MG",
      "status": "Active"
    },
    {
      "Code": "J1756",
      "ShortDescription": "INJECTION IRON SUCROSE 1 MG",
      "status": "Active"
    },
    {
      "Code": "J1786",
      "ShortDescription": "INJECTION IMIGLUCERASE 10 UNITS",
      "status": "Active"
    },
    {
      "Code": "J1790",
      "ShortDescription": "INJECTION DROPERIDOL UP TO 5 MG",
      "status": "Active"
    },
    {
      "Code": "J1810",
      "ShortDescription": "INJ DROPRIDL&FENTNYL CITRAT TO 2ML",
      "status": "Active"
    },
    {
      "Code": "J1815",
      "ShortDescription": "INJECTION INSULIN PER 5 UNITS",
      "status": "Active"
    },
    {
      "Code": "J1817",
      "ShortDescription": "INSULIN ADMIN THRU DME PER 50 UNITS",
      "status": "Active"
    },
    {
      "Code": "J1826",
      "ShortDescription": "INJECTION INTERFERON BETA-1A 30 MCG",
      "status": "Active"
    }]

      //if (AllCPTCodes.length > 0) {
      //    for (var i = 0; i < AllCPTCodes.length; i++) {
      //        optn = optn + '<option value="' + AllCPTCodes[i].Code + '-' + AllCPTCodes[i].ShortDescription + '">' + AllCPTCodes[i].Code + '-' + AllCPTCodes[i].ShortDescription + '</option>';
      //             }
      //              return optn;
      //          } else throw "No CPT Codes Available";
      //      }
    /*GENERATE TABLE FOR CPT CODES RANGE*/
    function GenerateCPTCodesRangeTable(SelectedRangeCPTCodes) {
        var tr = "";
        try{
            if (SelectedRangeCPTCodes.length > 0) {
                for (var i = 0; i < SelectedRangeCPTCodes.length; i++) {
                    tr = tr + '<tr>' +
                          '<td><input type="checkbox" id="chkCptRange"' + i + '" class="form-control input-xs cptRangeTableCheckBox normal-checkbox" style="position: absolute; opacity: 0;"><label for="chkCptRange"' + i + '"><span></span></label></td>' +
                          '<td>' + SelectedRangeCPTCodes[i].Code + '</td>' +
                          '<td>' + SelectedRangeCPTCodes[i].ShortDescription + '</td>' +
                          '</tr>';
                }
                return tr;
            } else throw "CPT Codes Range Not Selected";
        }catch(err){
            console.log(err);
        }
    }
    /*-------------------------------------------------------------------------------------------------------*/
    /*GENERATE OPTIONS FOR CPT DROPDOWN*/
    //function GenerateOptionsforDropdown() {
    //    var optn = '';
    //    try{
    //        if (AllCPTCodes.length > 0) {
    //            for (var i = 0; i < AllCPTCodes.length; i++) {
    //                optn = optn + '<option value="' + AllCPTCodes[i].Code + '-' + AllCPTCodes[i].ShortDescription + '">' + AllCPTCodes[i].Code + '-' + AllCPTCodes[i].ShortDescription + '</option>';
    //            }
    //            return optn;
    //        } else throw "No CPT Codes Available";
    //    }catch(err){
    //        console.log(err);
    //    }
    //}
    /*-------------------------------------------------------------------------------------------------------*/
    /*GENERATING CPT CODES DROPDOWN FOR RANGE SELECTION*/
    //function AppendGeneratedOptions(fromid, toid) {
    //    var otpn = GenerateOptionsforDropdown();
    //    $("#" + fromid).append(otpn);
    //    $("#" + fromid).customselect();
    //    $("#" + toid).append(otpn);
    //    $("#" + toid).customselect();
    //}

    //AppendGeneratedOptions("cptrangefrom", "cptrangeto");

    /*-------------------------------------------------------------------------------------------------------*/
    /*CPT CODES SELECT-FROM*/
    $(".addCptRangeMainContainer").off('change', '#cptrangefrom').on('change', '#cptrangefrom', function () {
        var codeArr = $(this).val().split('-');
        var code = codeArr[0];
        var description = codeArr[1];
        var selectid = $(this).attr('id');
        $("#cptcodefromdesc1").val(description);
    });
    /*-------------------------------------------------------------------------------------------------------*/
    /*CPT CODES SELECT-TO*/
    $(".addCptRangeMainContainer").off('change', '#cptrangeto').on('change', '#cptrangeto', function () {
        var codeArr = $(this).val().split('-');
        var code = codeArr[0];
        var description = codeArr[1];
        var selectid = $(this).attr('id');
        $("#cptrangetodesc2").val(description);
        $(".searchCptRangeBtn").prop("disabled", false);
    });

    //InitICheckFinal();
    RangeOfCPTCodesSelected = [];
    /*SEARCH RANGE OF CPT CODES*/
    $(".addCptRangeMainContainer").off("click", ".searchCptRangeBtn").on("click", ".searchCptRangeBtn", function () {
        var temp; var startIndex; var endIndex;
        var start = $("#cptrangefrom").val().split('-')[0];
        var end = $("#cptrangeto").val().split('-')[0];
        if (start > end) 
        { temp = start; 
          start = end; 
          end = temp 
        }
        var SelectedRangeCPTCodes = [];
        if (AllCPTCodes.length > 0) {
                    for (var i = 0; i < AllCPTCodes.length; i++) {
                        if (AllCPTCodes[i].Code === start) { startIndex = i }
                        if (AllCPTCodes[i].Code === end) { endIndex = i }
                    }
        }
        for (var i = startIndex; i <= endIndex; i++) {
                   SelectedRangeCPTCodes.push(AllCPTCodes[i]);
        }
        showLoaderSymbol('cptRangeTableDiv');
        $(".cptrangetablebody").html("");
        $(".cptrangetablebody").append(GenerateCPTCodesRangeTable(SelectedRangeCPTCodes));
        RangeOfCPTCodesSelected = SelectedRangeCPTCodes;
        InitICheckFinal();
        setTimeout(function () {
            removeLoaderSymbol();
            $(".cptRangeTableDisplayDiv").removeClass('plnotvisible').addClass('plvisible');
        }, 2000);
    });
    /*-------------------------------------------------------------------------------------------------------*/
    /*CHECK OR UNCHECK RANGE*/
    $(".cptRangeTableDisplayDiv").off("click", ".checkAllcptsBtn").on("click", ".checkAllcptsBtn", function () {
        if (!$(".cptRangeTableCheckBox").is(':checked')) {
            $(".cptRangeTableCheckBox").iCheck('check');
        }
    });
    $(".cptRangeTableDisplayDiv").off("click", ".unCheckAllcptsBtn").on("click", ".unCheckAllcptsBtn", function () {
        if ($(".cptRangeTableCheckBox").is(':checked')) {
            $(".cptRangeTableCheckBox").iCheck('uncheck');
        }
    });
    /*-------------------------------------------------------------------------------------------------------*/
    $(".cptRangeTableDisplayDiv").off("click", ".selectCheckCPTCodes").on("click", ".selectCheckCPTCodes", function () {
        $("#CPTArea").html("");
        var Btns = '';
        var plusbtn='';
        var CPTRow = '';
        try{
            if (RangeOfCPTCodesSelected.length > 0) {
                for (var i = 0; i < RangeOfCPTCodesSelected.length; i++) {
                if(i === RangeOfCPTCodesSelected.length-1){
                    //$(".plain_language_btn").removeClass('hidden');
                    //$(".smartIntelligenceBtn").removeClass('hidden');
                        Btns='<div class="col-lg-2 col-md-2 col-sm-2 col-xs-1 text-center cptInc_pullleft zero-padding-left-right pull-left">' +
                                                        '<button class="btn btn-primary btn-xs plain_language_btn  bold_text">Plain Language</button>' +
                                                     '</div>' +
                                                '<div class="col-lg-1 col-md-1 col-sm-2 col-xs-1 text-center  zero-padding-left-right text-center">' +
                                                        '<button class="btn btn-success btn-xs smartIntelligenceBtn bold_text loser_field">Plain Language</button><button class="btn btn-danger btn-xs smartIntelligenceBtn bold_text calypso_ai_btn"> CALYPSO AI</button>' +
                                                '</div>';
                        plusbtn= '<button class="btn btn-success btn-xs plusButton"><i class="fa fa-plus"></i></button>';
                                               }
                        CPTRow = CPTRow + '<div class="CPTRow">'+
                                                '<div class="col-lg-1 col-md-1 col-sm-8 col-xs-4  theme_label_data zero-padding-left-right">'+
                                                        '<input class="form-control input-xs" name="CPTCodes['+i+'].CPTCode" placeholder="CPT Code" type="text" value="'+RangeOfCPTCodesSelected[i].Code+'">' +
                                                '</div>'+
                                                '<div class="col-lg-1 col-md-1 col-sm-8 col-xs-4  theme_label_data zero-padding-left-right">'+
                                                        '<input class="form-control input-xs"  name="CPTCodes['+i+'].Modifier" placeholder="MODIFIER" type="text" value="">'+
                                                '</div>'+
                                                '<div class="col-lg-3 col-md-3 col-sm-6 col-xs-3 zero-padding-left-right">' +
                                                        '<input class="form-control input-xs" name="CPTCodes['+i+'].CPTDesc" placeholder="DESCRIPTION" type="text" value="'+RangeOfCPTCodesSelected[i].ShortDescription+'">'+
                                                        '<input class="form-control input-xs" name="CPTCodes['+i+'].Discipline" placeholder="PLAIN LANGUAGE" type="hidden" value="">'+
                                                '</div>'+
                                                '<div class="col-lg-1 col-md-2 col-sm-8 col-xs-4 cptrequnits_rightmargin zero-padding-left-right">'+
                                                        '<input class="form-control input-xs" name="CPTCodes['+i+'].RequestedUnits" placeholder="REQ UNITS" type="text" value="">'+
                                                '</div>'+
                                                '<div class="col-lg-1 col-md-1 col-sm-2 col-xs-1 pull-left button-styles-xs row-action zero-padding-left-right">'+
                                                        '<button class="btn btn-danger btn-xs minusButton"><i class="fa fa-minus"></i></button>'+
                                                      plusbtn +
                                                '</div>'+
                                                '<div class="col-lg-1 col-md-1 col-sm-2 col-xs-1 text-center cptInc_pullleft zero-padding-left-right">'+
                                                        '<input id="IncludeLetter0" type="checkbox" name="CPTCodes['+i+'].IncludeLetter" class="checkbox-radio">'+
                                                        '<label for="IncLetter"><span></span></label>'+
                                                '</div>' +Btns+
                                                '<div class="clearfix"></div>'+
                                            '</div>'
                    $("#CPTArea").html(CPTRow);
                    InitICheckFinal();
                    CloseModalManually("slide_modal", "modal_background");
                }
                
            } else throw "CPT Codes Range Not Selected";
        }catch(err){
            console.log(err);
        }
    });
    /*-------------------------------------------------------------------------------------------------------*/
    /*GET THE PLAIN LANGUAGE PARTIAL VIEW*/
    $("#plRows").off("click", ".plain_language_btn").on("click", ".plain_language_btn", function () {
        ShowModal('~/Views/Home/_GetPlainLanguageModal.cshtml', 'Plain Language')
    });
    /*------------------------------------------------------------------------------------*/

   
});


