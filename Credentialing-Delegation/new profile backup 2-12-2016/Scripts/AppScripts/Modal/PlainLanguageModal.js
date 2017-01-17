//**************************//
// AUTHOR: RAHUL TEJA       //
// CREATED DATE: 11-09-2016 //
//**************************//
$(function () {
    /*GENERATING PLAIN LANGUAGES*/
    //function GeneratePlainLanguages() {
    //    var selectedcptli = '';
    //    try{
    //        if (CPTCodesDataObj.length > 0) {
    //            for (var i = 0; i < CPTCodesDataObj.length; i++) {
    //                selectedcptli = selectedcptli + '<li class="list-group-item"><a>' + CPTCodesDataObj[i].CPTDescription + '</a><input type="submit" value="EDIT" style="width:45px" class="copy-button pull-right editPlainLangBtn"></li>';
    //            }
    //            return selectedcptli;
    //        } else {
    //            throw "No CPT Codes Selected";
    //        }
    //    }catch(err){
    //        console.log(err);
    //    }
    //};
    /*---------------------------------------------------------------------------------------------*/
    function returnEditButton() {
        return '<button style="float:right;" class="copy-button"><i class="fa fa-pencil"></i>&nbsp;&nbsp;Edit</button>';
    }

    //var cptPreviewLetterBtn = function () {
    //    TabManager.openSideModal('~/Areas/UM/Views/Common/Letter/_ApprovalLetter.cshtml', 'Approval Letter', 'both', 'print', '', '');
    //    return;
    //};
    function returnSaveButton() {
        return '<button style="float:right;" class="view-button SaveBtn">Save</button>';
    }
    /* Save Plain Language */
    $('.SaveBtn').on('click', function () {
        // remove the dummy hidden div first
        $('#PlainLang_DummyDiv').remove();
        var PlainLang = $('#CPTCodes')[0].children[0].children[0].children;
        var PlainLang_Html = "";
        for (var index = 0; index < PlainLang.length; index++) {
            var info = PlainLang[index].children[0].innerText;
            // add <div> tag if index is 0
            if (index == 0) {
                PlainLang_Html = '<div id="PlainLang_DummyDiv" class="hidden">';
            }
            // append the input text tag
            PlainLang_Html = PlainLang_Html + '<input type="text" name="CPTCodes[' + index + '].PlainLanguageDesc" value="' + info + '" id="PlainLang_Hidden[' + index + ']"/>' + '<br/>';
            // add </div> tag if index is length -1 
            if (index == PlainLang.length - 1) {
                PlainLang_Html = PlainLang_Html + '</div>';
            }
            //$('#'+'PlainLang_Hidden'+'['+index+']').val = info;
        }
        $('#UM_auth_form').append(PlainLang_Html);
        //$('.close_modal_btn ').trigger("click");
        CloseModalManually("slide_modal", "modal_background");
    })
    /*-------------*/

    /*SET THE SELECTED PLAIN LANGUAGE TO EDIT*/
    function setSelectedPlainLanguage(plainlang) {
        $('.editplainlangselected').children('li').each(function (i) {
            if ($(this).find('textarea').hasClass('plangtextarea')) {
                var liIndex = $(this).index();
                var selectedli = '<div class="plainLang wrap-words" value="' + plainlang + '">' + plainlang + '</div>' +
                                  '<div class="pull-right">' +
                                  '<a class="btn btn-primary btn-xs pull-right editPlainLangBtn"><i class="fa fa-pancil-square-o"></i>Edit</a>' +
                                  '</div>'
                $(this).html(selectedli);
                try {
                    if (CPTCodesDataObj.length > 0) {
                        for (var i = 0; i < CPTCodesDataObj.length; i++) {
                            if (i === liIndex) {
                                CPTCodesDataObj[i].CPTDescription = plainlang;
                            }
                        }
                        return;
                    } else throw "No CPT Codes Selected";
                } catch (err) {
                    console.log(err);
                }
            }
        });
    }
    /*---------------------------------------------------------------------------------------------*/
    /*GENERATING SEARCHCUMDROPDOWN FOR PLAIN LANGUAGES-AND-LIST OF PLAIN LANGUAGES*/
    function GenerateOptionsandList() {
        var optn = '';
        var li = '';
        try {
            if (MasterPlainLanguages.length > 0) {
                for (var i = 0; i < MasterPlainLanguages.length; i++) {
                    optn = optn + '<option value="' + MasterPlainLanguages[i].PlainLanguageName + '">' + MasterPlainLanguages[i].PlainLanguageName + '</option>';
                    li = li + '<li class="list-group-item"><a>' + MasterPlainLanguages[i].PlainLanguageName + '</a></li>';
                }
            } else {
                throw "Plain Languages Not Available";
            }
        } catch (err) {
            console.log(err);
        }
        $("#selectplainlang").append(optn);
        $('.plainlanglist').append(li);
        $("#selectplainlang").customselect();
        return;
    }
    /*---------------------------------------------------------------------------------------------*/

    //$('.editplainlangselected').append(GeneratePlainLanguages());
    /*---------------------------------------------------------------------------------------------*/
    GenerateOptionsandList();
    /*EDIT PLAIN LANGUAGES*/
    $(".edit-selected-plain-languages").off("click", ".editPlainLangBtn").on("click", ".editPlainLangBtn", function () {
        $(this).addClass('hidden');
        var $row = $(this).parents('.list-group-item');
        var $rowIndex = $row.index();
        var $Description = $row.children('.plainLang').text().trim();
        //$('.editplainlangselected').children('li').each(function (i) {
        //    if ($(this).find('textarea').hasClass('plangtextarea') && (i !== liIndex)) {
        //        var litext = $(this).find('textarea').val();
        //        var selectedli = '<a>' + litext + '</a><input type="submit" value="EDIT" style="width:45px" class="copy-button pull-right editPlainLangBtn">';
        //        $(this).html(selectedli);
        //    }
        //})
        var editselectedli = '<textarea class="form-control plangtextarea" rows="5">' + $Description + '</textarea>' +
                             '<input type="submit" value="SAVE" style="width: 45px;margin-top: -43px;" class="btn btn-success btn-xs pull-right savePlainLangBtn">';
        $row.children('.plainLang').html(editselectedli);
        $('.list-of-plain-languages').show();
    });
    /*---------------------------------------------------------------------------------------------*/
    /*SAVE PLAIN LANGUAGES*/
    $(".edit-selected-plain-languages").off("click", ".savePlainLangBtn").on("click", ".savePlainLangBtn", function () {
        var $row = $(this).parents('.list-group-item');
        var $Desc = $row.find('.plangtextarea').val();
        var $rowindex = $row.index();
        var selectedli = '<div class="plainLang wrap-words">' + $Desc + '</div>' +
                            '<div class="pull-right">' +
                                 '<a class="btn btn-primary btn-xs pull-right editPlainLangBtn"><i class="fa fa-pancil-square-o"></i>Edit</a>' +
                             '</div>'
        $row.html(selectedli);
        $('.list-of-plain-languages').hide()
        //try {
        //    if (CPTCodesDataObj.length > 0) {
        //        for (var i = 0; i < CPTCodesDataObj.length; i++) {
        //            if (i === liIndex) { CPTCodesDataObj[i].CPTDescription = plainlang }
        //        }
        //    } else throw "No CPT Codes Selected";
        //} catch (err) { console.log(err) } finally { $('.list-of-plain-languages').hide() }
    });
    /*---------------------------------------------------------------------------------------------*/
    /*SELECTING PLAIN LANGUAGES FROM LIST*/
    $(".plainlanglist").off("click", ".list-group-item").on("click", ".list-group-item", function (event) {
        var pltext = $(this).find('a').text();
        setSelectedPlainLanguage(pltext);
    });
    /*---------------------------------------------------------------------------------------------*/
    /*SELECTING PLAIN LANGUAGES FROM FILTER*/
    $(".list-of-plain-languages").off('change', '.plainlangdropdown').on('change', '.plainlangdropdown', function () {
        var plainlang = $(this).val();
        var selectid = $(this).attr('id');
        setSelectedPlainLanguage(plainlang);
    });
    /*---------------------------------------------------------------------------------------------*/
    /*CLOSE PLAIN LANGUAGES MODAL*/
    $(".plainlangModalContainer").off("click", ".closePlainLangBtn").on("click", ".closePlainLangBtn", function () {
        CloseModalManually("slide_modal", "modal_background");
    });
    /*---------------------------------------------------------------------------------------------*/

});