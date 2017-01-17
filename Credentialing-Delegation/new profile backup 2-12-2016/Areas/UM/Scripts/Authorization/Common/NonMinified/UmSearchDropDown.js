$(document).ready(function () {
    //----------Search-cum-dropDown-------------------//

    ResultData = {};
    facilityData={}
    //----------------Invokes this function---------------------//
    $('#dynamicSection').off('keyup', '.ProviderSearchDropdown').on('keyup', '.ProviderSearchDropdown',
        _.debounce(function (event) {
            if ((event.keyCode >= 65 && event.keyCode <= 90)||event.keyCode==8)
            {
                getSearchValues($(this).val(), this)
            }
        }, 600));
    $('#dynamicSection').off('keyup', '.FacilitySearchDropdown').on('keyup', '.FacilitySearchDropdown',
        _.debounce(function (event) {
            if ((event.keyCode >= 65 && event.keyCode <= 90) || event.keyCode == 8)
            {
                getFacilitySearchValues($(this).val(), this)
            }
        }, 600));
    //----------------Invokes this function---------------------//
    //$('#dynamicSection').off('blur', '.ProviderSearchDropdown').on('blur', '.ProviderSearchDropdown', function () {
    //    hideSearchCumDropDown(this);
    //});
    //----------------------END-------------------------------//

    function hideSearchCumDropDown(ob) {
        if ($(ob)) {
            if ($(ob).siblings('.UMDropDownList')) {
                $(ob).siblings('.UMDropDownList').hide();
                RemoveExistingHiddenInput(ob.id);
            }
        }
    };

    function getSearchValues(val, ob) {
        $(ob).attr('autocomplete', 'off');
        if ((val != '') && (typeof val != 'undefined')) {
            hideAllSearchCumDropDown();
            var providerSearchCumDropDownWorker = new Worker('/Areas/UM/WebWorkers/AjaxWebWorker.js');
            providerSearchCumDropDownWorker.postMessage({ url: '/UM/ProviderFacilityService/GetProviderData',singleParam:true, searchTerm: val });
            providerSearchCumDropDownWorker.addEventListener('message', function (e) {
                if (e.data) {
                    if (e.data.length > 0) {
                        ResultData = JSON.parse(e.data);
                        if (ResultData != null && ResultData.length > 0) {
                            var liData = "";
                            emptyExistElement($(ob).parent().find('ul'));//for removing the already generated list
                            RemoveAddNewProviderButton($(ob)[0].id);//For Removing Add new Provider  Button 
                            generateList(ResultData, $(ob)[0].id);//For Generating List 
                        }
                        else {
                            hideSearchCumDropDown(ob);
                            showAddNewProviderButton($(ob)[0].id);//If now result found Adding new provider button
                        }
                    }
                    else {
                        hideSearchCumDropDown(ob);
                    }
                }
                providerSearchCumDropDownWorker.terminate();
            }, false);
           
        }
        else {
            RemoveAddNewProviderButton($(ob)[0].id);//For Removing Add new Provider  Button 
            hideSearchCumDropDown(ob);
        }
    };

    function emptyExistElement(ele) {
        $(ele).each(function (e) {

            $(this).remove();
        })
    }
    var $ListItems = '';
    function generateList(eleData, InputId) {
        var liData = "";
        for (i in eleData) {
            liData = liData + "<li class='DropDownListLi' eleindex=" + i + "><span>" + eleData[i].FirstName + " " + eleData[i].LastName + "</span>-<span class='NPIcss'>" + eleData[i].NPI + "</span><span class='label right PcpLabel label-success'>PCP</span></li>";
        }
        var element = "<ul class='UMDropDownList dropdown-menu' id='" + InputId + "'>" + liData + "</ul>";
        var $ListItems = $('#' + InputId).parent().find('li');
        $('#' + InputId).parent().last().after().append(element);
        SetCssForDropDown(InputId);//setting Css For UmdropDownList 
        $('#display').css("display", "block");
    }
    function SetCssForDropDown(CssID) {
        if(CssID=='coMagProvider')
        {
          var top = ($('#' + CssID).position().top + 7 + $('#' + CssID)[0].offsetHeight) + "px";
          var left =-156+ "px";
          $('.UMDropDownList').css({ 'top': top, 'left': left });
        }
        else
        {
            var top = ($('#' + CssID).position().top + 7 + $('#' + CssID)[0].offsetHeight) + "px";
            var left = $('#' + CssID).position().left + "px";
            $('.UMDropDownList').css({ 'top': top, 'left': left });
        }
        
    }
    $(document).not(".UMDropDownList").click(function () {
        emptyExistElement($(this).parent().find('ul'));
    })
    $(document).on('click', ".DropDownListLi", function () {
        if ($(this).parent()[0]) {
            SetValue(ResultData[$(this).attr('eleindex')], $(this).parent()[0].id)
            $(this).parent().remove();
        }
        //emptyExistElement($(this).parent().find('ul'));
    });

    SetValue = function(SelectedData, TextId) {

        if (TextId == 'coMagProvider')
        {
            $('#' + TextId).val(SelectedData.FirstName + " " + SelectedData.LastName);
            SetCssForINOON(TextId, true);
        }
        else
        {
            $('#' + TextId).val(SelectedData.FirstName + " " + SelectedData.LastName);
            var ObjName = TextId.split('_');
            RemoveExistingHiddenInput(TextId);
            var elementData = getHiddenInput(SelectedData, ObjName[0]);
            $('#' + TextId).parent().append(elementData);
            SetCssForINOON(TextId, true);
            if (TextId == 'AttendingProvider_FullName')
            {
                CopyToAdmProvider(SelectedData);
            }
        }
       
    }
    function getHiddenInput(SelectedData, TextId) {
        var Template = "";
        for (var property in SelectedData) {
            (SelectedData.hasOwnProperty(property))
            {
                if(SelectedData[property])
                {
                Template = "<input type='hidden' name='" + TextId + "." + property + "' value='" + SelectedData[property] + "'/>" + Template;
                }
                else
                {
                 Template = "<input type='hidden' name='" + TextId + "." + property + "'>" + Template;
                }
                
            }
        }
        Template = Template + "<input type='hidden' name='" + TextId + ".ProviderMode" + "' value='" + TextId + "'/>";
        return Template;
    }
    
    RemoveExistingHiddenInput=function(ParentId) {
        $('#' + ParentId).parent().find('input[type="hidden"]').remove();
        $('#' + ParentId).parent().find('.INOONLabel').remove();
        if (ParentId == 'AttendingProvider_FullName') {
            RemoveExistingHiddenInput('AdmittingProvider_FullName');
            $('#AdmittingProvider_FullName').val('');
        }
    }

    //-----------END---------------------------------//

    //-----------TO copy same thing To Admitting Provider---------------//
    function CopyToAdmProvider(DataToSet)//-----For Copying the data to admitting provider-----//
    {
        SetValue(DataToSet, 'AdmittingProvider_FullName')
    }

    function SetCssForINOON(ID)//-----For Seting the In/OON of a provider-----//
    {
        var label = "<label style='margin-left:" +( $('#' + ID)[0].offsetWidth - 21 + 'px') +"' class='label label-success INProvider INOONLabel'>IN</label>";
        $('#'+ID).parent().append(label);
    }
    function showAddNewProviderButton(ID)//-----For Adding the Add new Provider Button------//
    {
        if (ID != 'coMagProvider')
        {
            RemoveAddNewProviderButton(ID);
            var addButton = "&nbsp;&nbsp;&nbsp;<i class='label label-success plusbtnSign Add_New_Provider_Btn bold_text loser_field'><span class='fa fa-plus'></span>ADD NEW</i>";
            $('#' + ID).before(addButton)
        }
        
    }

    RemoveAddNewProviderButton=function(ID)//-----For Removing the Add new Provider Button------//
    {
        $('#' + ID).parent().find('.Add_New_Provider_Btn').remove()
    }
    //------------------END---------------------------------------------//


    //-----------------------------------------------Facility Service ---------------------------------------------//
      function getFacilitySearchValues(val, ob) {
        $(ob).attr('autocomplete', 'off');
        if ((val != '') && (typeof val != 'undefined')) {
            hideAllSearchCumDropDown();
            var facilitySearchCumDropDownWorker = new Worker('/Areas/UM/WebWorkers/AjaxWebWorker.js');
            facilitySearchCumDropDownWorker.postMessage({ url: '/UM/ProviderFacilityService/GetFacilityData',singleParam:true, searchTerm: val });
            facilitySearchCumDropDownWorker.addEventListener('message', function (e) {
                if (e.data) {
                    if (e.data.length > 0) {
                        facilityData = JSON.parse(e.data);
                        if (facilityData != null && facilityData.length > 0) {
                            var liData = "";
                            emptyExistElement($(ob).parent().find('ul'));
                            generateFacilityList(facilityData, $(ob)[0].id);
                        }
                        else {
                            hideSearchCumDropDown(ob);
                        }
                    }
                    else {
                        hideSearchCumDropDown(ob);
                    }
                }
                facilitySearchCumDropDownWorker.terminate();
            });
        }
        else {
            hideSearchCumDropDown(ob);
        }
      };


      function generateFacilityList(eleData, InputId) {
          var liData = "";
          for (i in eleData) {
              liData = liData + "<li class='DropDownListFacilityLi' eleindex=" + i + "><span>" + eleData[i].FullName + "</span>-<span class='NPIcss'>" + eleData[i].TaxID + "</span></li>";
          }
          var element = "<ul class='UMDropDownList dropdown-menu' id='" + InputId + "'>" + liData + "</ul>";
          $('#' + InputId).parent().last().after().append(element);
          SetCssForDropDown(InputId);//setting Css For UmdropDownList 
          $('#display').css("display", "block");
      }
      $(document).on('click', ".DropDownListFacilityLi", function () {

          SetFacilityValue(facilityData[$(this).attr('eleindex')], $(this).parent()[0].id)
          $(this).parent().remove();
          //emptyExistElement($(this).parent().find('ul'));

      });
      SetFacilityValue = function (SelectedData, TextId) {
          $('#' + TextId).val(SelectedData.FullName);
          var ObjName = TextId.split('_');
          RemoveExistingHiddenInput(TextId);
          var elementData = getHiddenInput(SelectedData, ObjName[0]);
          $('#' + TextId).parent().append(elementData);
          SetCssForINOONForFacility(TextId, true);
      }
      function SetCssForINOONForFacility(ID)
    {
        var label = "<label class='label label-success INFacility INOONLabel'>IN</label>";
        $('#'+ID).parent().append(label);
    }
    //----------------------------------------------End Of Facility Service Data-----------------------------------//

    //--------------------Iterating through generated through list when user uses key up and down------------------//
      $('#UM_auth_form').off('keydown', '.ProviderSearchDropdown , .FacilitySearchDropdown').on('keydown', '.ProviderSearchDropdown , .FacilitySearchDropdown', function (event) {
          addCssOnKeyUpAndDown(event);
      })

      addCssOnKeyUpAndDown=function(event)//-----------Adding Css on Key Up And Down In List-------------//
      {
          var $ListItems = $('#' + event.currentTarget.id).parent().find('li');
          if ($ListItems.length > 0) {
              var key = event.keyCode,
              $selected = $ListItems.filter('.DropDownListSelected'),
              $current;
              if (key == 13) {
                  GetSelectedValueOnEnter($selected);
              }
              else if (key == 9)
              {
                  $(event.currentTarget.parentElement).find('ul').remove();//To remove the List When User clicks On Tab
              }
              if (key != 40 && key != 38) return;

              $ListItems.removeClass('DropDownListSelected');

              if (key == 40) // Down key
              {
                  if (!$selected.length || $selected.is(':last-child')) {
                      $current = $ListItems.eq(0);
                  }
                  else {
                      $current = $selected.next();
                  }
              }
              else if (key == 38) // Up key
              {
                  if (!$selected.length || $selected.is(':first-child')) {
                      $current = $ListItems.last();
                  }
                  else {
                      $current = $selected.prev();
                  }
              }
              $current.addClass('DropDownListSelected');
          }
      }

      function GetSelectedValueOnEnter(value)
      {
          if ($(value).parent()[0].id == 'Facility_FullName')
          {
              SetFacilityValue(facilityData[$(value).attr('eleindex')], $(value).parent()[0].id)
          }
          else
          {
              SetValue(ResultData[$(value).attr('eleindex')], $(value).parent()[0].id)
          }
          $(value).parent().remove();
      }
    
    //--------------------------------------------------------END--------------------------------------------------//
      $('#partialBodyContainer').off('click', '#innerTabContainer').on('click', '#innerTabContainer', function (e) {
          if (!jQuery.contains('.UMDropDownList', e.target)) {
              $('.UMDropDownList').remove();
          }
      });

})