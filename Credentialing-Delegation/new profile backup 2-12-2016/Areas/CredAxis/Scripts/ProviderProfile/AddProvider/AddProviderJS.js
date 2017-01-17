$(document).ready(function () {
    //input element required hiding
    $("input").keyup(function () {
        $("input:required:valid ").parent().parent().find("span").hide();
    })
   

    $('#preview').on('click', function () {
        //$('body').removeClass('modal-open');
        $('.M_overlay').css({ 'display': 'block' });
        $('#authPreviewModal').modal();
        $('.modal-backdrop').css('zIndex', 0);
        //$('.modal-backdrop').remove();
        window.scrollTo(0, 0);
    });
    $('#previewCancel').click(function(){
        $('#modal').modal('hide');
    });
    
    $('#cancel').click(function () {
        $('#PreViewProvider').hide();
        $('#AddProvider').show();
    });
    $("#Confirm").click(function () {
        $('.M_overlay').css({ 'display': 'block' });
        $('.M_notification').css({ 'display': 'block' });
        $('.M_overlayFloatRight').css({ 'display': 'block' });
    });
    $('.M_overlay').click(function () {
        $('.M_overlay').css({ 'display': 'none' });
        $('.M_notification').css({ 'display': 'none' });
        $('.M_overlayFloatRight').css({ 'display': 'none' });
    });
    $('#viewMore').on('click', function () {               
        $('#addCitizenModal').modal();
        $('.modal-backdrop').css('zIndex', 0);        
        window.scrollTo(0, 0);
    });
    
    $('#ipaInputs').selectize({
        plugins: ['remove_button'],
        delimiter: ',',
        persist: false,
        create: function (input) {
            return {
                value: input,
                text: input
            }
        }
    });
    
    $('#groupInputs').selectize({
        plugins: ['remove_button'],
        delimiter: ',',
        persist: false,
        create: function (input) {
            return {
                value: input,
                text: input
            }
        }
    });
  
    $('.input-tags3').selectize({
        plugins: ['remove_button'],
        delimiter: ',',
        persist: false,
        create: function (input) {
            return {
                value: input,
                text: input
            }
        }
    });
    $('#Employee').change(function () {
        $('#Employee').prop("checked") ? $('#EmployeeDetails').show() : $('#EmployeeDetails').hide();
    });
    $('#Affiliate').change(function () {
        $('#Affiliate').prop("checked") ? $('#AffiliateDetails').show() : $('#AffiliateDetails').hide();
    });
    $('#IPA').change(function () {
        $('#IPA').prop("checked") ? $('#IPADetails').show() : $('#IPADetails').hide(); $("#IPAFullDetails").hide();
    });
   
    $('#Group').change(function () {
        $('#Group').prop("checked") ? $('#GroupDetails').show() : $('#GroupDetails').hide(); $("#GroupFullDetails").hide();
    });
    $('#ipaInputs').on('change', function () {
        $('#ipaRealtionship').html(" ");

        $("#ipaInputs option").each(function (index) {
            $('#ipaRealtionship').append("<tr><td>" + $(this).text() + "</td><td><input type='checkbox' name='Employee' class='normal-checkbox' ><label for='Employee'><span></span>Employee</label><input type='checkbox' name='Employee' class='normal-checkbox' ><label for='Employee'><span></span>Affiliate</label></td><td><input class='form-xs' type='date'/></td></tr>")
           
        });
        ($("#ipaInputs option").length == 0) ? $("#IPAFullDetails").hide() : $("#IPAFullDetails").show();
    });
    $('#groupInputs').on('change', function () {
        $('#groupRealtionship').html(" ");

        $("#groupInputs option").each(function (index) {
            $('#groupRealtionship').append("<tr><td>" + $(this).text() + "</td><td><input type='checkbox' name='Employee' class='normal-checkbox' ><label for='Employee'><span></span>Employee</label><input type='checkbox' name='Employee' class='normal-checkbox' ><label for='Employee'><span></span>Affiliate</label></td><td><input class='form-xs' type='date'/></td></tr>")
           
        });
        ($("#groupInputs option").length==0)?$("#GroupFullDetails").hide():$("#GroupFullDetails").show();

    });


    //provider data filling automatically
    $('[name="Provider.NPI"]').keyup(function () {
       
        if ($('[name="Provider.NPI"]').val().length >= 10) {
            $('[name="Provider.Personal.Salutation"]').val("Mr");
            $('[name="Provider.Personal.FirstName"]').val("EDWARD");
            $('[name="Provider.Personal.LastName"]').val("SMITH");
            $('[name="Provider.Personal.MiddleName"]').val("S");
            $('[name="Provider.Personal.Suffix"]').val("MD");
            $('[name="Provider.Personal.Gender"]').val("1");
            $('[name="Provider.Contact.Street"]').val("DWANT STREET");
            $('[name="Provider.Contact.Appartment"]').val("17045");
            $('[name="Provider.Contact.City"]').val("SPRING HILL");
            $('[name="Provider.Contact.State"]').val("FLORIDA");
            $('[name="Provider.Contact.ZipCode"]').val("170044");
            $('[name="Provider.Contact.Country"]').val("UNTIED STATES");
            $('[name="Provider.Contact.Email"]').val("SMITHED154@GMAIL.COM");
            $('[name="provider.contact.phone"]').val("999758744");
          //  $("input:valid").parent().parent().find("span").hide();

        } else {
            $('[name="Provider.Personal.Salutation"]').val("");
            $('[name="Provider.Personal.FirstName"]').val("");
            $('[name="Provider.Personal.LastName"]').val("");
            $('[name="Provider.Personal.MiddleName"]').val("");
            $('[name="Provider.Personal.Suffix"]').val("");
            $('[name="Provider.Personal.Gender"]').val("");
            $('[name="Provider.Contact.Street"]').val("");
            $('[name="Provider.Contact.Appartment"]').val("");
            $('[name="Provider.Contact.City"]').val("");
            $('[name="Provider.Contact.State"]').val("");
            $('[name="Provider.Contact.ZipCode"]').val("");
            $('[name="Provider.Contact.Country"]').val("");
            $('[name="Provider.Contact.Email"]').val("");
            $('[name="provider.contact.phone"]').val("");
           // $("input:valid ").parent().parent().find("span").show();
        }
    });

});
