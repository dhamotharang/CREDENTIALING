$(document).ready(function () {
    //----------Search-cum-dropDown-------------------//
    var ResultData = {};
    //----------------Invokes this function---------------------//
    $('#dynamicSection').off('keyup', '.ProviderSearchDropdown').on('keyup', '.ProviderSearchDropdown', function () {
        $(this).attr('autocomplete', 'off');
        if (event.keyCode === 27) {
            if ($(this)) {
                if ($(this).next()) {
                    $(this).next().hide();
                }
            }
        }
        else {
                getSearchValues($(this).val());//getting result for provider
               
        }
    });
    //----------------------END-------------------------------//

function getSearchValues(val) {
    setTimeout(function () {
        $.ajax({
            url: '/UM/ProviderService/GetProviderData',
            data: { searchTerm: val },
            async: false,
            success: function (data) {
                if (data) {
                    ResultData = JSON.parse(data);
                    
                    if (ResultData != null && ResultData.length > 0) {
                        var liData = "";
                        emptyExistElement($(this).parent().find('ul'));
                        generateList(ResultData, $(this)[0].id);
                    }
                    else {
                        emptyExistElement($(this).parent().find('ul'));
                    }
                }
            }
        });
    }, 500);
    }

    function emptyExistElement(ele) {
        $(ele).each(function (e) {
            e.preventDefault();
            $(this).remove();
        })
    }
    function generateList(eleData, InputId) {
        var liData = "";
        for (i in eleData) {
            liData = liData + "<li class='DropDownListLi' eleindex=" + i + "><span>" + eleData[i].FirstName + "" + eleData[i].LastName + "</span>-<span class='NPIcss'>" + eleData[i].NPI + "</span><span class='label right PcpLabel label-success'>PCP</span></li>";
        }
        var element = "<ul class='UMDropDownList dropdown-menu' id='" + InputId + "'><li>" + liData + "</li></ul>";
        $('#' + InputId).parent().last().after().append(element);
        SetCssForDropDown(InputId);//setting Css For UmdropDownList 
        $('#display').css("display", "block");
    }
    function SetCssForDropDown(CssID) {
        var top = ($('#' + CssID).position().top + 7 + $('#' + CssID)[0].offsetHeight) + "px";
        var left = $('#' + CssID).position().left + "px";
        $('.UMDropDownList').css({ 'top': top, 'left': left });

    }
    //$('.ProviderSearchDropdown').focusout(function () {
    //    emptyExistElement($(this).parent().find('ul'));

    //})

    $(document).on('click', ".DropDownListLi", function () {

        SetValue(ResultData[$(this).attr('eleindex')], $(this).parent()[0].id)
        emptyExistElement($(this).parent().find('ul'));

    });
    function SetValue(SelectedData, TextId) {
        $('#' + TextId).val(SelectedData.FirstName + " " + SelectedData.LastName);
        var elementData = getHiddenPartialPage(SelectedData, TextId);
    }
    function getHiddenPartialPage(SelectedData, TextId) {
        var Template = "<input type='hidden' name="
    }
    //-----------END---------------------------------//



})