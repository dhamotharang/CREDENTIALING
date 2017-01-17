$(function(){
    var wizardScript = document.createElement('script');
    wizardScript.src = "/Areas/Encounters/Scripts/AppJs/EncounterWizard/Unminified/WizardController.js";
    document.body.appendChild(wizardScript);

    $('#fullBodyContainer').on('WizardLoaded',function () {
        var createScheduleScript = document.createElement('script');
        createScheduleScript.src = "/Areas/Encounters/Scripts/AppJs/Schedule/Unminified/CreateSchedule.js";
        document.body.appendChild(createScheduleScript);
    });  
})