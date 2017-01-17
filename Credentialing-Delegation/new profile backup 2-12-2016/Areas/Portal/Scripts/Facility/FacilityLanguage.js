$(document).ready(function () {
    //Dropdownlist Selectedchange event

    $("#selectFacilityLanguage").change(function () {
       // $("#State").empty();

        $.ajax({
            url: "/ProviderType/Reload",
            success: function (data) {
                // your data could be a View or Json or what ever you returned in your action method 
                // parse your data here
                $("#ProviderTypeList").replaceWith("<div class='row' id='ProviderTypeList'>" + data + "</div>")

            }
        });
        //$.ajax({
        //    type: 'POST',
        //    url: "/City/GetStates", // we are calling json method
        //    dataType: 'json',
        //    data: { id: $("#Country").val() },
        //    // here we get value of selected country and passing same value
        //    // as input to json method GetStates.

        //    success: function (States) {
        //        // states contains the JSON formatted list of states passed from the controller
        //        $.each(States, function (i, States) {
        //            $("#State").append('<option value="' + States.Value + '">' +
        //                 States.Text + '</option>');
        //            // here we are adding option for States
        //        });
        //    },
        //    error: function (ex) {
        //        alert('Failed to retrieve states.' + ex);
        //    }
        //});
        return false;
    })
});
