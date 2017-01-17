$(function(){
    var ValidateScheduleDetails = function () {
       
    }
    var validateForm = function (id) {
        var form = $(id);
        form.removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(form);
        if (form.valid()) {
            return true;
        }
        return false;

    };
    $("#TempProfile").off('click', '.list-item').on('click', '.list-item', function (e) {
        e.preventDefault();
        var MethodName = 'callAfterLoad';

        var url = $(this).data().tabPath;
        var theID = $(this).data().tabContainer;
        var theDataVal = $(this).parent().data().val;
        theTempId = theID;
        TabManager.getDynamicContent(url, theID, MethodName);
    });
  callAfterLoad = function () {
       $(".FacilityMultipleplan").select2({
           tags: true,
           placeholder: 'PLAN NAME',
           allowClear: true
       });

       $(".groupContact").select2({
           tags: true,
           placeholder: 'GROUP CONTACT NAME',
           allowClear: true
       });
       $(".FacilityMultiple").select2({
           tags: true,
           placeholder: 'FACILITY',
           allowClear: true
       });
       $(".SpecialtyMultiple").select2({
           tags: true,
           placeholder: 'SPECIALTY',
           allowClear: true
       });
    }

      

    });