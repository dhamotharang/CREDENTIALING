$(document).ready(function () {
    CarePlan = {};
    /***********  set Probelm, Intervention, Goal and Outcome Library data from Master Care Plan  **************/
    CarePlan.PlanLibrary = { ProblemLibrary: [], InterventionLibrary: [], GoalLibrary: [], OutcomeLibrary: [] };
    CarePlan.currentOffset = { ProblemOffset: 0, InterventionOffset: 0, GoalOffset: 0, OutcomeOffset: 0 };
    setCarePlanLibraryData = function (data) {
        CarePlan.PlanLibrary.ProblemLibrary = data;
        for(var i in data) {
            $.merge(CarePlan.PlanLibrary.InterventionLibrary,data[i].AvailableInterventions);
           
            for (var j in data[i].AvailableInterventions) {
                $.merge(CarePlan.PlanLibrary.GoalLibrary, data[i].AvailableInterventions[j].AvailableGoals);
               
                for (var k in data[i].AvailableInterventions[j].AvailableGoals) {
                    $.merge(CarePlan.PlanLibrary.OutcomeLibrary, data[i].AvailableInterventions[j].AvailableGoals[k].AvailableOutcomes);
                   
                }
            }
        }
    }
    /********** Get Data for Search Template : ~/Resources/CM_JSON/CarePlanList.json ***********/
    $.ajax({
        type: "GET",
        url: "/ManageCarePlan/GetAllCarePlans",
        //data: ,
        dataType: "json",
        async: false,
        success: function (data) {
            console.log(data);
            CarePlan.CarePlanResult = data;
        }
    });
    /********** Get Master Care Plan Template : ~/Resources/CM_JSON/MasterCarePlan.json ***********/
    $.ajax({
        type: "GET",
        url: "/ManageCarePlan/GetMasterCarePlan",
        data: null,
        dataType: "json",
        async: false,
        success: function (data) {
            console.log(data);
            CarePlan.MasterCarePlan = data;
            setCarePlanLibraryData(data);
        }
    });
    $.ajax({
        type: "GET",
        url: "/ManageCarePlan/GetMasterData",
        data: null,
        dataType: "json",
        async: false,
        success: function (data) {
            console.log(data);
            CarePlan.MasterData = data;
        }
    });
    /***************** define ID's for Care Plan *********************/
    var problemTableId = '#carePlanProblemTable';
    var interventionTableId = '#carePlanInterventionTable';
    var goalTableId = '#carePlanGoalTable';
    var outcomeTableId = '#carePlanOutcomeTable';
    var interventionPanelId = '#InterventionPanel';
    var goalPanelId = '#GoalPanel';
    var outcomePanelId = '#OutcomePanel';
    /**********  set Data in Care Plan Search Result Tables  *************/
    CarePlan.CarePlanResultSet = { ResultSet1: [], ResultSet2: [], ResultSet3: [],ResultSet4:[] };
    var resultSet = 0;
    $("#SearchCarePlanResultSet1").html("");
    $("#SearchCarePlanResultSet2").html("");
    $("#SearchCarePlanResultSet3").html("");
    for(var k in CarePlan.CarePlanResult)
    {
        var id;
        if (resultSet == 0) {
            CarePlan.CarePlanResultSet.ResultSet1.push(CarePlan.CarePlanResult[k]);
            resultSet = 1;
            id = 'SearchCarePlanResultSet1';
        }
        else if (resultSet == 1) {
            CarePlan.CarePlanResultSet.ResultSet2.push(CarePlan.CarePlanResult[k]);
            resultSet = 2;
            id = 'SearchCarePlanResultSet2';
        }
        else if (resultSet == 2) {
            CarePlan.CarePlanResultSet.ResultSet3.push(CarePlan.CarePlanResult[k]);
            resultSet = 3;
            id = 'SearchCarePlanResultSet3';
        }
        else if (resultSet == 3) {
            CarePlan.CarePlanResultSet.ResultSet4.push(CarePlan.CarePlanResult[k]);
            resultSet = 0;
            id = 'SearchCarePlanResultSet4';
        }
        $('#' + id).append("<tr><th>" + CarePlan.CarePlanResult[k].TemplateTitle + "</th><th>" + CarePlan.CarePlanResult[k].CategoryName + "</th></tr>");
    }
   
    CarePlan.CarePlanSelectedIndex = {};
    $('#viewTemplateFieldSet').hide();

    /*****************  set Outcomes for a Goal selected (Outcome Table Id, Selected Goal Object, Goal Index) ********************/
    setSelectedGoalData = function (id, data, index) {
        // $('#' + id).html("");
        $(outcomeTableId).html("");
        //CarePlan.CarePlanSelectedIndex = { ProblemIndex: CarePlan.CarePlanSelectedIndex.ProblemIndex, InterventionIndex: CarePlan.CarePlanSelectedIndex.InterventionIndex, GoalIndex: index };
        if (data.AvailableOutcomes.length > 0) {
            for (var k in data.AvailableOutcomes) { $(id).append("<tr><td><div class='careplan-item' style='overflow:hidden;'><div class='careplan-content'><div class='careplan-body text-uppercase'>" + data.AvailableOutcomes[k].Description + "</div><div class='careplan-footer'><div class='pull-left'></div><div class='pull-right'><button class='copy-button selected-from-library' onclick='event.stopPropagation();'><i class='fa fa-arrow-down'></i></button>&nbsp; <input type='button' value='VIEW' class='view-button' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='EDIT' class='edit-button modifyCarePlanElements' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='REMOVE' class='red-button modifyCarePlanElements' onclick='event.stopPropagation();'></div></div></div></div></td></tr>"); }
        }
       
    }
    /*****************  set Goals, GoalIndex for a Intervention selected  (Goal Table Id, Selected Intervention Object, Intervention Index) ********************/
    setSelectedInterventionData = function (id, data, index) {
        // $('#' + id).html("");
        $(goalTableId).html("");
        $(outcomeTableId).html("");
        //CarePlan.CarePlanSelectedIndex = { ProblemIndex: CarePlan.CarePlanSelectedIndex.ProblemIndex, InterventionIndex: index, GoalIndex: -1 };
        if (data.AvailableGoals.length > 0) {
            for (var k in data.AvailableGoals) { $(id).append("<tr onclick='setClickedItemInCarePlan(\"Goal\",\"" + goalTableId + "\",this);'><td><div class='careplan-item' style='overflow:hidden;'><div class='careplan-content'><div class='careplan-body text-uppercase'>" + data.AvailableGoals[k].Description + "</div><div class='careplan-footer'><div class='pull-left'><label class='label label-success'>STG</label></div><div class='pull-right'><button class='copy-button selected-from-library' onclick='event.stopPropagation();'><i class='fa fa-arrow-down'></i></button>&nbsp; <input type='button' value='VIEW' class='view-button' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='EDIT' class='edit-button modifyCarePlanElements' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='REMOVE' class='red-button modifyCarePlanElements' onclick='event.stopPropagation();'></div></div></div></div></td></tr>"); }
            CarePlan.CarePlanSelectedIndex.GoalIndex = 0;
            $(goalTableId + ' tr:eq(' + CarePlan.CarePlanSelectedIndex.GoalIndex + ') .careplan-content').addClass('selected-carePlan-element');
            setSelectedGoalData(outcomeTableId, data.AvailableGoals[CarePlan.CarePlanSelectedIndex.GoalIndex], CarePlan.CarePlanSelectedIndex.GoalIndex);
        }
      
    }
    /*****************  set Interventions, InterventionIndex for a Problem selected  (Intervention Table Id, Selected Problem Object, Problem Index)   ********************/
    setSelectedProblemData = function (id, data, index) {
        // $('#' + id).html("");
        $(interventionTableId).html("");
        $(goalTableId).html("");
        $(outcomeTableId).html("");
       // CarePlan.CarePlanSelectedIndex = { ProblemIndex: index, InterventionIndex: -1, GoalIndex: -1 };
        if (data.AvailableInterventions.length > 0) {
            for (var k in data.AvailableInterventions) { $(id).append("<tr onclick='setClickedItemInCarePlan(\"Intervention\",\"" + interventionTableId + "\",this);'><td><div class='careplan-item' style='overflow:hidden;'><div class='careplan-content'><div class='careplan-body text-uppercase'>" + data.AvailableInterventions[k].Description + "</div><div class='careplan-footer'><div class='pull-left'></div><div class='pull-right'><button class='copy-button selected-from-library' onclick='event.stopPropagation();'><i class='fa fa-arrow-down'></i></button>&nbsp; <input type='button' value='VIEW' class='view-button' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='EDIT' class='edit-button modifyCarePlanElements' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='REMOVE' class='red-button modifyCarePlanElements' onclick='event.stopPropagation();'></div></div></div></div></td></tr>"); }
            CarePlan.CarePlanSelectedIndex.InterventionIndex = 0;
            $(interventionTableId + ' tr:eq(' + CarePlan.CarePlanSelectedIndex.InterventionIndex + ') .careplan-content').addClass('selected-carePlan-element');
            setSelectedInterventionData(goalTableId, data.AvailableInterventions[CarePlan.CarePlanSelectedIndex.InterventionIndex], CarePlan.CarePlanSelectedIndex.InterventionIndex);
        }
        
    }
    $('#createCarePlanTemplate').hide();
    /**************** Css,Selected Index and other steps corresponding to selected for Problem, Intervention, Goal in Care Plan Template **************/
    
    setClickedItemInCarePlan = function (item,id,currentindex) {
        var index = $(id + ' tr').index(currentindex.closest('tr'));
        var offset = $($(id + ' tr')[index]).position();
       
        $(id + ' tr .careplan-content').removeClass('selected-carePlan-element');
        $(id + ' tr:eq(' + index + ') .careplan-content').addClass('selected-carePlan-element');
       
        switch (item) {
            case 'Problem':
                CarePlan.CarePlanSelectedIndex = { ProblemIndex: index, InterventionIndex: -1, GoalIndex: -1 };
                offset.top = offset.top - CarePlan.currentOffset.ProblemOffset;
                CarePlan.currentOffset = { ProblemOffset: CarePlan.currentOffset.ProblemOffset, InterventionOffset: offset.top, GoalOffset: offset.top, OutcomeOffset: offset.top };
                
                $(interventionPanelId).css('margin-top', CarePlan.currentOffset.InterventionOffset);
                $(goalPanelId).css('margin-top', CarePlan.currentOffset.GoalOffset);
                $(outcomePanelId).css('margin-top', CarePlan.currentOffset.OutcomeOffset);
                setSelectedProblemData(interventionTableId, CarePlan.CarePlanSelected[CarePlan.CarePlanSelectedIndex.ProblemIndex], CarePlan.CarePlanSelectedIndex.ProblemIndex);
                break;
            case 'Intervention':
                CarePlan.CarePlanSelectedIndex = { ProblemIndex: CarePlan.CarePlanSelectedIndex.ProblemIndex, InterventionIndex: index, GoalIndex: -1 };
                offset.top = CarePlan.currentOffset.InterventionOffset + offset.top - CarePlan.currentOffset.ProblemOffset;
                CarePlan.currentOffset = { ProblemOffset: CarePlan.currentOffset.ProblemOffset, InterventionOffset: CarePlan.currentOffset.InterventionOffset, GoalOffset: offset.top, OutcomeOffset: offset.top };
                $(goalPanelId).css('margin-top', CarePlan.currentOffset.GoalOffset);
                $(outcomePanelId).css('margin-top', CarePlan.currentOffset.OutcomeOffset);
                setSelectedInterventionData(goalTableId, CarePlan.CarePlanSelected[CarePlan.CarePlanSelectedIndex.ProblemIndex].AvailableInterventions[CarePlan.CarePlanSelectedIndex.InterventionIndex], CarePlan.CarePlanSelectedIndex.InterventionIndex);
                break;
            case 'Goal':
                CarePlan.CarePlanSelectedIndex = { ProblemIndex: CarePlan.CarePlanSelectedIndex.ProblemIndex, InterventionIndex: CarePlan.CarePlanSelectedIndex.InterventionIndex, GoalIndex: index };
                offset.top = CarePlan.currentOffset.GoalOffset + offset.top - CarePlan.currentOffset.ProblemOffset;
                CarePlan.currentOffset = { ProblemOffset: CarePlan.currentOffset.ProblemOffset, InterventionOffset: CarePlan.currentOffset.InterventionOffset, GoalOffset: CarePlan.currentOffset.GoalOffset, OutcomeOffset: offset.top };

                $(outcomePanelId).css('margin-top', CarePlan.currentOffset.OutcomeOffset);
                setSelectedGoalData(outcomeTableId, CarePlan.CarePlanSelected[CarePlan.CarePlanSelectedIndex.ProblemIndex].AvailableInterventions[CarePlan.CarePlanSelectedIndex.InterventionIndex].AvailableGoals[CarePlan.CarePlanSelectedIndex.GoalIndex], CarePlan.CarePlanSelectedIndex.GoalIndex);
                break;
        }
        if (modifyCarePlanVar) { $('.modifyCarePlanElements').css('display', 'inline-block'); }
        else { $('.modifyCarePlanElements').css('display', 'none'); }
        $('.selected-from-library').css('display', 'none');
    }

    /***************** Display the Template selected and collapse the search Result ******************/
    /************** Setting data in Problem panel for Care Plan *************/
    setProblemData = function () {

        //$("#ProblemPanel .CarePlanPanels").append("<div class='tabs btn-group btn-breadcrumb'><a class='btn btn-default'><i class='fa fa-user fa-1x'></i>&nbsp;Barbara Joy</a><a class='btn btn-success text-uppercase' id='activeBreadcrumb'>Claims Dashboard</a></div>");
        
        

        $(problemTableId).html("");
        $(interventionTableId).html("");
        $(goalTableId).html("");
        $(outcomeTableId).html("");
        CarePlan.CarePlanSelected.sort(function (a, b) {
            var nameA = a.ProblemCategory.Name.toUpperCase(); // ignore upper and lowercase
            var nameB = b.ProblemCategory.Name.toUpperCase(); // ignore upper and lowercase
            if (nameA < nameB) {
                return -1;
            }
            if (nameA > nameB) {
                return 1;
            }
            // names must be equal
            return 0;
        });
        var uniqueNames = [];
        for (i = 0; i < CarePlan.CarePlanSelected.length; i++) {
            if (uniqueNames.indexOf(CarePlan.CarePlanSelected[i].ProblemCategory.Name) === -1) {
                
                uniqueNames.push(CarePlan.CarePlanSelected[i].ProblemCategory.Name);
            }
        }
        uniqueNames.sort();
        
        var currentUniqueCategory = uniqueNames[0];
        var prevUniqueCategory = null;
        CarePlan.CarePlanSelectedIndex = { ProblemIndex: -1, InterventionIndex: -1, GoalIndex: -1 };
        if (CarePlan.CarePlanSelected.length > 0) {
            for (var k in CarePlan.CarePlanSelected) {
                currentUniqueCategory = CarePlan.CarePlanSelected[k].ProblemCategory.Name;
                if (currentUniqueCategory != prevUniqueCategory)
                {
                    prevUniqueCategory = currentUniqueCategory;
                    $(problemTableId).append("<tr onclick='setClickedItemInCarePlan(\"Problem\",\"" + problemTableId + "\",this);'><td><div onclick='event.stopPropagation();' class='tabs btn-group btn-breadcrumb'><a style='background-color:#32408f' class='btn btn-success text-uppercase' id='activeBreadcrumb'>" + CarePlan.CarePlanSelected[k].ProblemCategory.Name + "</a></div> <div class='careplan-item' style='overflow:hidden;'><div class='careplan-content'><div class='careplan-body text-uppercase'>" + CarePlan.CarePlanSelected[k].Description + "</div><div class='careplan-footer'><div class='pull-left'><label class='label label-primary'> " + CarePlan.CarePlanSelected[k].ProblemCategory.Name + "</label></div><div class='pull-right'><button class='copy-button selected-from-library'></button>&nbsp; <input type='button' value='VIEW' class='view-button' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='EDIT' class='edit-button modifyCarePlanElements' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='REMOVE' class='red-button modifyCarePlanElements' onclick='event.stopPropagation();'></div></div></div></div></td></tr>");
                }
                else {
                   
                    $(problemTableId).append("<tr onclick='setClickedItemInCarePlan(\"Problem\",\"" + problemTableId + "\",this);'><td><div class='careplan-item' style='overflow:hidden;'><div class='careplan-content'><div class='careplan-body text-uppercase'>" + CarePlan.CarePlanSelected[k].Description + "</div><div class='careplan-footer'><div class='pull-left'><label class='label label-primary'> " + CarePlan.CarePlanSelected[k].ProblemCategory.Name + "</label></div><div class='pull-right'><button class='copy-button selected-from-library'></button>&nbsp; <input type='button' value='VIEW' class='view-button' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='EDIT' class='edit-button modifyCarePlanElements' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='REMOVE' class='red-button modifyCarePlanElements' onclick='event.stopPropagation();'></div></div></div></div></td></tr>");

                }

               

               

            }
            CarePlan.currentOffset.ProblemOffset= $($(problemTableId + ' tr')[0]).position().top;
            CarePlan.CarePlanSelectedIndex.ProblemIndex = 0;
            $(problemTableId + ' tr:eq(' + CarePlan.CarePlanSelectedIndex.ProblemIndex + ') .careplan-content').addClass('selected-carePlan-element');
            setSelectedProblemData(interventionTableId, CarePlan.CarePlanSelected[CarePlan.CarePlanSelectedIndex.ProblemIndex], CarePlan.CarePlanSelectedIndex.ProblemIndex);
          
        }
    }

    $('#SearchCarePlanResultSet1, #SearchCarePlanResultSet2, #SearchCarePlanResultSet3, #SearchCarePlanResultSet4').find('tr').click(function () {
        CarePlan.CarePlanSelected = CarePlan.MasterCarePlan;
        $('.searchResultView').hide();
        $('.viewCarePlan').show();
        $("#flip-body").slideDown('1000');
        $('#flip-head').css('cursor', 'pointer');
        $('#flip-head').html("<span class='h5'><i class='fa fa-chevron-down'></i> Care Management Category : " + CarePlan.CarePlanResult[$(this).index() * 3].CategoryName + "    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Program Name : " + CarePlan.CarePlanResult[$(this).index() * 3].TemplateTitle + " </span>");
        $('#flip-head').append("<span class='pull-right'>&nbsp; <input type='button' value='PREVIEW' class='view-button' onclick='event.stopPropagation();'> &nbsp;<input id='editCarePlan' type='button' value='EDIT' class='edit-button' onclick='event.stopPropagation();EditCarePlanTemplate();'>  &nbsp;<input type='button'value='COPY' class='copy-button' onclick='event.stopPropagation();copyCarePlan();'> &nbsp;<input type='button' value='CLOSE' class='red-button' > </span>")
        $('.modifyCarePlanElements').css('display','none');
        viewSearchResultVar = true;
        setProblemData();
        $('.modifyCarePlanElements').css('display', 'none');
        $('.selected-from-library').css('display','none')

        //modifyCarePlanVar = false;
        $('.masterCarePlanTemplateElements').css('display', 'block');
        $('.modifyCarePlanElements').css('display', 'inline-block');
    });
    var viewSearchResultVar = false;
    $('#flip-head').html("<span class='h5'> Search Template Result</span>");
    $('.viewCarePlan').hide();
    $('#flip-head').click(function () {
        if (viewSearchResultVar) {
            $('#flip-head').css('cursor', 'auto');
            $('#flip-head').html("<span class='h5'> Search Template Result</span>");
            $('.searchResultView').show('1000');
            $('.viewCarePlan').hide('1000');
            $("#flip-body").slideDown();
            viewSearchResultVar = false;
         
            $('.masterCarePlanTemplateElements').css('display', 'none');
        }
    });
   
    
    //var modifyCarePlanVar = false;
   

    /********************** Create Care Plan ***********************/
    $('.createtemplate-tabcontent .createtemplate-careplan').hide();
    CreateCarePlan = function () {
        CarePlan.CarePlanSelected = [];
        $('#searchCarePlanTemplate').hide();
        $('#createCarePlanTemplate').show();
        var optionsAsString = "";
        /************ Set options in Care Management Category dropdown for Create Template ***********/
        for (var i = 0; i < CarePlan.MasterData.CareMngmtCat.length; i++) {
            optionsAsString += "<option value='" + CarePlan.MasterData.CareMngmtCat[i].Name + "'>" + CarePlan.MasterData.CareMngmtCat[i].Name + "</option>";
        }
        $('select[name="careManagementCategory"]').append(optionsAsString);
        //modifyCarePlanVar = false;
        /*********** set Selected Care Plan Table ID's for create (same Partial page used for Create and View) ***********/
        problemTableId = '.createtemplate-careplan #carePlanProblemTable';
        interventionTableId = '.createtemplate-careplan #carePlanInterventionTable';
        goalTableId = '.createtemplate-careplan #carePlanGoalTable';
        outcomeTableId = '.createtemplate-careplan #carePlanOutcomeTable';
        interventionPanelId = '.createtemplate-careplan #InterventionPanel';
        goalPanelId = '.createtemplate-careplan #GoalPanel';
        outcomePanelId = '.createtemplate-careplan #OutcomePanel';
        $(problemTableId).html("");
       
        /*********** reSet Care Plan Library Tables ***********/
        $('#carePlanProblemLibraryTable').html('');
        $('#carePlanInterventionLibraryTable').html('');
        $('#carePlanGoalLibraryTable').html('');
        $('#carePlanOutcomeLibraryTable').html('');

        /************  reSet Index for Care Plan to be Created *************/
        CarePlan.CarePlanSelectedIndex = { ProblemIndex: -1, InterventionIndex: -1, GoalIndex: -1 };
        CarePlan.currentOffset = { ProblemOffset: 0, InterventionOffset: 0, GoalOffset: 0, OutcomeOffset: 0 };
        $('.masterCarePlanTemplateElements').css('display', 'none');
    }
    changeCareManagementCategory = function () {
        if ($('select[name="careManagementCategory"]').val() != "") {
            $("#careManagementProgram").attr("readonly", false);
            $('#caremanagementcategory-tab').attr("isdone", 1)
            $('#caremanagementcategory-tab').removeClass();
            $('#caremanagementcategory-tab').addClass('done');
            $('#program-tab').removeClass();
            $('#program-tab').addClass('selected');
        }
        else {
            $("#careManagementProgram").attr("readonly", true);
            $("#careManagementProgram").val("");
            $('#caremanagementcategory-tab').attr("isdone", 0)
            $('#caremanagementcategory-tab').removeClass();
            $('#caremanagementcategory-tab').addClass('selected');
            $('#program-tab').removeClass();
            $('#program-tab').addClass('disabled');
        }
    }
    selectProgram = function (event) {
        if (event.which == 13) {
            $('#program-tab').attr("isdone", 1)
            $('#program-tab').removeClass();
            $('#program-tab').addClass('done');
            $('#problems-tab').removeClass();
            $('#problems-tab').addClass('selected');
            $('.createtemplate-tabcontent .createtemplate-conditions').hide(1000);
            $('.createtemplate-tabcontent .createtemplate-careplan').show(1000);
           
            setCarePlanLibraryPanels(CarePlan.PlanLibrary.ProblemLibrary, CarePlan.PlanLibrary.InterventionLibrary, CarePlan.PlanLibrary.GoalLibrary, CarePlan.PlanLibrary.OutcomeLibrary);

        }
        
    }
    setCarePlanLibraryPanels = function (problem_Lib, intervention_Lib, goal_Lib, outcome_Lib) {
        CarePlan.CarePlanSelectedIndex = { ProblemIndex: -1, InterventionIndex: -1, GoalIndex: -1 };
        for (var k in problem_Lib) { $("#carePlanProblemLibraryTable").append("<tr><td><div class='careplan-item' style='overflow:hidden;' onclick='setClickedProblemInLibrary(this)'><div class='careplan-content'><div class='careplan-body text-uppercase'>" + problem_Lib[k].Description + "</div><div class='careplan-footer'><div class='pull-left'><label class='label label-primary'>" + problem_Lib[k].ProblemCategory.Name + "</label></div><div class='pull-right'><button  class='copy-button selected-from-library' onclick='pushProblemFromLibraryToSelected(\"" + problemTableId + "\",this)'><i class='fa fa-arrow-up'></i></button>&nbsp; <input type='button' value='VIEW' class='view-button' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='EDIT' class='edit-button modifyLibraryCarePlanElements' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='REMOVE' class='red-button modifyLibraryCarePlanElements' onclick='event.stopPropagation();'></div></div></div></div></td></tr>"); }
        for (var k in intervention_Lib) { $('#carePlanInterventionLibraryTable').append("<tr><td><div  onclick='setClickedInterventionInLibrary(this)' class='careplan-item' style='overflow:hidden;'><div class='careplan-content'><div class='careplan-body text-uppercase'>" + intervention_Lib[k].Description + "</div><div class='careplan-footer'><div class='pull-left'></div><div class='pull-right'><button  class='copy-button selected-from-library' onclick='pushInterventionFromLibraryToSelected(\"" + interventionTableId + "\",this)'><i class='fa fa-arrow-up'></i></button>&nbsp; <input type='button' value='VIEW' class='view-button' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='EDIT' class='edit-button modifyLibraryCarePlanElements' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='REMOVE' class='red-button modifyLibraryCarePlanElements' onclick='event.stopPropagation();'></div></div></div></div></td></tr>"); }
        for (var k in goal_Lib) { $('#carePlanGoalLibraryTable').append("<tr onclick='setClickedGoalInLibrary(this)'><td><div class='careplan-item' style='overflow:hidden;'><div class='careplan-content'><div class='careplan-body text-uppercase'>" + goal_Lib[k].Description + "</div><div class='careplan-footer'><div class='pull-left'><label class='label label-success'>STG</label></div><div class='pull-right'><button  class='copy-button selected-from-library' onclick='pushGoalFromLibraryToSelected(\"" + goalTableId + "\",this)'><i class='fa fa-arrow-up'></i></button>&nbsp; <input type='button' value='VIEW' class='view-button' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='EDIT' class='edit-button modifyLibraryCarePlanElements' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='REMOVE' class='red-button modifyLibraryCarePlanElements' onclick='event.stopPropagation();'></div></div></div></div></td></tr>"); }
        for (var k in outcome_Lib) { $('#carePlanOutcomeLibraryTable').append("<tr onclick='setClickedOutcomeInLibrary(this)'><td><div class='careplan-item' style='overflow:hidden;'><div class='careplan-content'><div class='careplan-body text-uppercase'>" + outcome_Lib[k].Description + "</div><div class='careplan-footer'><div class='pull-left'></div><div class='pull-right'><button  class='copy-button selected-from-library' onclick='pushOutcomeFromLibraryToSelected(\"" + outcomeTableId + "\",this)'><i class='fa fa-arrow-up'></i></button>&nbsp; <input type='button' value='VIEW' class='view-button' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='EDIT' class='edit-button modifyLibraryCarePlanElements' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='REMOVE' class='red-button modifyLibraryCarePlanElements' onclick='event.stopPropagation();'></div></div></div></div></td></tr>"); }
    }
  
    pushProblemFromLibraryToSelected = function (id, currentIndex) {
        var index = $('#carePlanProblemLibraryTable tr').index(currentIndex.closest('tr'));
        var injection = $.extend(true, {}, CarePlan.PlanLibrary.ProblemLibrary[index]);
        CarePlan.CarePlanSelected.push(injection);
        $(id).append("<tr onclick='setClickedItemInCarePlan(\"Problem\",\"" + problemTableId + "\",this);'><td><div class='careplan-item' style='overflow:hidden;'><div class='careplan-content'><div class='careplan-body text-uppercase'>" + injection.Description + "</div><div class='careplan-footer'><div class='pull-left'><label class='label label-primary'>" + injection.ProblemCategory.Name + "</label></div><div class='pull-right'><button class='copy-button selected-from-library' onclick='event.stopPropagation();'><i class='fa fa-arrow-down'></i></button>&nbsp; <input type='button' value='VIEW' class='view-button' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='EDIT' class='edit-button modifyCarePlanElements' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='REMOVE' class='red-button modifyCarePlanElements' onclick='event.stopPropagation();'></div></div></div></div></td></tr>");
        if (CarePlan.CarePlanSelected.length == 1) {
            CarePlan.CarePlanSelectedIndex = { ProblemIndex: 0, InterventionIndex: -1, GoalIndex: -1 };
            $(id + ' tr .careplan-content').removeClass('selected-carePlan-element');
            $(id + ' tr:nth-child(' + CarePlan.CarePlanSelected.length + ') .careplan-content').addClass('selected-carePlan-element');
            setSelectedProblemData(interventionTableId, injection, index);

            $('#problems-tab').attr("isdone", 1)
            $('#problems-tab').removeClass();
            $('#problems-tab').addClass('done');
            $('#interventions-tab').attr("isdone", 1)
            $('#interventions-tab').removeClass();
            $('#interventions-tab').addClass('done');
            $('#goals-tab').attr("isdone", 1)
            $('#goals-tab').removeClass();
            $('#goals-tab').addClass('done');
            $('#outcomes-tab').attr("isdone", 1)
            $('#outcomes-tab').removeClass();
            $('#outcomes-tab').addClass('done');

        }
       
        if (modifyCarePlanVar) { $('.modifyCarePlanElements').css('display', 'inline-block'); }
        else { $('.modifyCarePlanElements').css('display', 'none'); }


    }
     
    pushInterventionFromLibraryToSelected = function (id, currentIndex) {
        var index = $('#carePlanInterventionLibraryTable tr').index(currentIndex.closest('tr'));
        var problemindex = CarePlan.CarePlanSelectedIndex.ProblemIndex;
        if (problemindex != -1) {
            var injection = $.extend(true, {}, CarePlan.PlanLibrary.InterventionLibrary[index])
            CarePlan.CarePlanSelected[problemindex].AvailableInterventions.push(injection);
            $(id).append("<tr onclick='setClickedItemInCarePlan(\"Intervention\",\"" + interventionTableId + "\",this);'><td><div class='careplan-item' style='overflow:hidden;'><div class='careplan-content'><div class='careplan-body text-uppercase'>" + injection.Description + "</div><div class='careplan-footer'><div class='pull-left'></div><div class='pull-right'><button class='copy-button selected-from-library' onclick='event.stopPropagation();'><i class='fa fa-arrow-down'></i></button>&nbsp; <input type='button' value='VIEW' class='view-button' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='EDIT' class='edit-button modifyCarePlanElements' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='REMOVE' class='red-button modifyCarePlanElements' onclick='event.stopPropagation();'></div></div></div></div></td></tr>");
            if (CarePlan.CarePlanSelected[problemindex].AvailableInterventions.length == 1) {
                CarePlan.CarePlanSelectedIndex = { ProblemIndex: CarePlan.CarePlanSelectedIndex.ProblemIndex, InterventionIndex: 0, GoalIndex: -1 };
                $(id + ' tr .careplan-content').removeClass('selected-carePlan-element');
                $(id + ' tr:nth-child(' + CarePlan.CarePlanSelected[problemindex].AvailableInterventions.length + ')  .careplan-content').addClass('selected-carePlan-element');
                setSelectedInterventionData(goalTableId, injection, index);

            }
            if (modifyCarePlanVar) { $('.modifyCarePlanElements').css('display', 'inline-block'); }
            else { $('.modifyCarePlanElements').css('display', 'none'); }
        }
    }

    pushGoalFromLibraryToSelected = function (id, currentIndex) {
        var index = $('#carePlanGoalLibraryTable tr').index(currentIndex.closest('tr'));
        var interventionindex = CarePlan.CarePlanSelectedIndex.InterventionIndex;
        if (interventionindex != -1) {
            var injection = $.extend(true, {}, CarePlan.PlanLibrary.GoalLibrary[index]);
            CarePlan.CarePlanSelected[CarePlan.CarePlanSelectedIndex.ProblemIndex].AvailableInterventions[interventionindex].AvailableGoals.push(injection);
            //CarePlan.PlanLibrary.ProblemLibrary[CarePlan.CarePlanSelectedIndex.ProblemIndex].AvailableInterventions[interventionindex].AvailableGoals.splice(CarePlan.PlanLibrary.ProblemLibrary[CarePlan.CarePlanSelectedIndex.ProblemIndex].AvailableInterventions[interventionindex].AvailableGoals.length, 1);
            $(id).append("<tr onclick='setClickedItemInCarePlan(\"Goal\",\"" + goalTableId + "\",this);'><td><div class='careplan-item' style='overflow:hidden;'><div class='careplan-content'><div class='careplan-body text-uppercase'>" + injection.Description + "</div><div class='careplan-footer'><div class='pull-left'><label class='label label-success'>STG</label></div><div class='pull-right'><button class='copy-button selected-from-library' onclick='event.stopPropagation();'><i class='fa fa-arrow-down'></i></button>&nbsp; <input type='button' value='VIEW' class='view-button' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='EDIT' class='edit-button modifyCarePlanElements' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='REMOVE' class='red-button modifyCarePlanElements' onclick='event.stopPropagation();'></div></div></div></div></td></tr>");
            if (CarePlan.CarePlanSelected[CarePlan.CarePlanSelectedIndex.ProblemIndex].AvailableInterventions[interventionindex].AvailableGoals.length == 1) {
                CarePlan.CarePlanSelectedIndex = { ProblemIndex: CarePlan.CarePlanSelectedIndex.ProblemIndex, InterventionIndex: CarePlan.CarePlanSelectedIndex.InterventionIndex, GoalIndex: -1 };
                $(id + ' tr  .careplan-content').removeClass('selected-carePlan-element');
                $(id + ' tr:nth-child(' + CarePlan.CarePlanSelected[CarePlan.CarePlanSelectedIndex.ProblemIndex].AvailableInterventions[interventionindex].AvailableGoals.length + ')  .careplan-content').addClass('selected-carePlan-element');
                setSelectedGoalData(outcomeTableId, injection, index);
            }
            if (modifyCarePlanVar) { $('.modifyCarePlanElements').css('display', 'inline-block'); }
            else { $('.modifyCarePlanElements').css('display', 'none'); }
        }
    }

    pushOutcomeFromLibraryToSelected = function (id, currentIndex) {
        var index = $('#carePlanOutcomeLibraryTable tr').index(currentIndex.closest('tr'));
        var goalindex = CarePlan.CarePlanSelectedIndex.GoalIndex;
        if (goalindex != -1) {
            var injection = $.extend(true, {}, CarePlan.PlanLibrary.OutcomeLibrary[index]);
            CarePlan.CarePlanSelected[CarePlan.CarePlanSelectedIndex.ProblemIndex].AvailableInterventions[CarePlan.CarePlanSelectedIndex.InterventionIndex].AvailableGoals[goalindex].AvailableOutcomes.push(injection);
            $(id).append("<tr><td><div class='careplan-item' style='overflow:hidden;'><div class='careplan-content'><div class='careplan-body text-uppercase'>" + injection.Description + "</div><div class='careplan-footer'><div class='pull-left'></div><div class='pull-right'><button class='copy-button selected-from-library' onclick='event.stopPropagation();'><i class='fa fa-arrow-down'></i></button>&nbsp; <input type='button' value='VIEW' class='view-button' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='EDIT' class='edit-button modifyCarePlanElements' onclick='event.stopPropagation();'>&nbsp; <input type='button' value='REMOVE' class='red-button modifyCarePlanElements' onclick='event.stopPropagation();'></div></div></div></div></td></tr>");
            if (modifyCarePlanVar) { $('.modifyCarePlanElements').css('display', 'inline-block'); }
            else { $('.modifyCarePlanElements').css('display', 'none'); }
        }
    }
    
    CarePlan.PlanPreview = { Level1: [] };
    previewCarePlan = function (trackindex) {
        var libindex = $('#carePlanProblemTable tr').index(trackindex.closest('tr'));
        var previewData = CarePlan.CarePlanSelected[libindex];
        ShowModal("~/Views/ManageCarePlan/ManageCare/_PreviewCarePlan.cshtml", 'Preview Care Plan');
        for (var i in previewData.Level1) {
            previewData.Level1[i].Visibility = true;
            previewData.Level1[i].Level2 = previewData.Level1[i].AvailableInterventions;
            for (var j in previewData.Level1[i].Level2) {
                previewData.Level1[i].Level2[j].Visibility = true;
                previewData.Level1[i].Level2[j].Level3 = previewData.Level1[i].Level2[j].AvailableGoals;
                for (var k in previewData.Level1[i].Level2[j].Level3) {
                    previewData.Level1[i].Level2[j].Level3[k].Visibility = true;
                    previewData.Level1[i].Level2[j].Level3[k].Level4 = previewData.Level1[i].Level2[j].Level3[k].AvailableOutcomes;
                }
            }
        }
    }
 
    copyCarePlan = function () {
        CarePlan.CarePlanSelected = CarePlan.MasterCarePlan;
        $('#searchCarePlanTemplate').hide();
        $('#createCarePlanTemplate').show();
        var optionsAsString = "";
        /************ Set options in Care Management Category dropdown for Create Template ***********/
        for (var i = 0; i < CarePlan.MasterData.CareMngmtCat.length; i++) {
            optionsAsString += "<option value='" + CarePlan.MasterData.CareMngmtCat[i].Name + "'>" + CarePlan.MasterData.CareMngmtCat[i].Name + "</option>";
        }
        $('select[name="careManagementCategory"]').append(optionsAsString);
        //modifyCarePlanVar = false;
        /*********** set Selected Care Plan Table ID's for create (same Partial page used for Create and View) ***********/
        problemTableId = '.createtemplate-careplan #carePlanProblemTable';
        interventionTableId = '.createtemplate-careplan #carePlanInterventionTable';
        goalTableId = '.createtemplate-careplan #carePlanGoalTable';
        outcomeTableId = '.createtemplate-careplan #carePlanOutcomeTable';
        $(problemTableId).html("");

        /*********** reSet Care Plan Library Tables ***********/
        $('#carePlanProblemLibraryTable').html('');
        $('#carePlanInterventionLibraryTable').html('');
        $('#carePlanGoalLibraryTable').html('');
        $('#carePlanOutcomeLibraryTable').html('');

        /************  reSet Index for Care Plan to be Created *************/
        CarePlan.CarePlanSelectedIndex = { ProblemIndex: -1, InterventionIndex: -1, GoalIndex: -1 };
        $('.masterCarePlanTemplateElements').css('display', 'none');
        setProblemData();

    }

//    setClickedProblemInLibrary = function (currentIndex) {
//        var index = $('#carePlanProblemLibraryTable tr').index(currentIndex.closest('tr'));
//        var injection = $.extend(true, {}, CarePlan.PlanLibrary.ProblemLibrary[index]);
//        for (var k in CarePlan.PlanLibrary.InterventionLibrary) {
//            if (CarePlan.PlanLibrary.InterventionLibrary[k].AvailableProblemID == injection.AvailableProblemID) {
//                $($('#carePlanInterventionLibraryTable tr')[index]).insertBefore($($('#carePlanInterventionLibraryTable tr')[0]));
//                CarePlan.PlanLibrary.InterventionLibrary
//            }
//        }

//    }
//    setClickedInterventionInLibrary = function (currentIndex) {

//    }
//    setClickedGoalInLibrary = function (currentIndex) {

//    }
//    setClickedOutcomeInLibrary = function (currentIndex) {
            
//    }

});

