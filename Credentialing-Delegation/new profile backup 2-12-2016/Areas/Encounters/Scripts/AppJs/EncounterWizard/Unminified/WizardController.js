//Wizard Controller Class - Main
function EncounterWizardController(divSelectorWizard, divSelectorWizardContent, stepList) {
    //List Of Properties
    var progress = 0;
    //Internal Methods
    var UpdateWizard = function () {
        $(''+divSelectorWizardContent).find('.wizard_div').hide();
        var listItems = '';
        $('' + divSelectorWizard).empty().append('<ul class="wizard_steps anchor EncounterWizardAnchor"></ul>');
        for (var i = 0; i < stepList.length; i++) {
            if (i < progress) {
                listItems += '<li><a href="create_claim_by" data-step="' + i + '" class="disabled done"><span class="step_no">' + stepList[i].StepNumber + '</span><span class="step_descr">' + stepList[i].StepDescription + '</span></a></li>';
            }
            if (i == progress) {
                listItems += '<li><a href="create_claim_by" data-step="' + i + '" class="selected"><span class="step_no">' + (i + 1) + '</span><span class="step_descr">' + stepList[i].StepDescription + '</span></a></li>';
            }
            if (i > progress) {
                listItems += '<li><a href="create_claim_by" data-step="' + i + '" class="disabled"><span class="step_no">' + (i + 1) + '</span><span class="step_descr">' + stepList[i].StepDescription + '</span></a></li>';
            }
        }
        $('' + divSelectorWizard).find('.EncounterWizardAnchor').append(listItems);
    }

    var GeneratePlaceholders = function () {
        var element = $('' + divSelectorWizardContent).empty();
        var divList = '';
        for (var i = 0; i < stepList.length; i++) {
            divList += ' <div id="WizardItem'+ stepList[i].StepNumber +'" class="wizard_div" style="display: block;"></div>'
        }
        element.append(divList);
    }
    //Exposed Methods
    this.ReinitializeWizard = function () {
        progress = 0;
        UpdateWizard();
        GeneratePlaceholders();
    }

    this.Proceed = function (steps) {
        if (progress + steps <= stepList.length) {
            progress += steps;
            UpdateWizard();
        }
    }

    this.Back = function (steps) {
        if (progress - steps >= 0) {
            progress -= steps;
            UpdateWizard();
        }
    }
}

$('#fullBodyContainer').trigger('WizardLoaded');