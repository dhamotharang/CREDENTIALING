(function () {
    $('.dateTimePicker').datetimepicker({
        format:"MM/DD/YYYY hh:mm:ss",
        widgetPositioning: {
            vertical: 'bottom'
        }
    });

    function generateOdag() {
        var templateYesNO = "", templateDescriptive = "", templateOnlyDateTime = "", templateObjectiveWithDateTime = "";
        $("#odagQuestionContent").html("");
        $.each(MasterODAGQuestions, function (index, value) {
          
            var str = "";
            if (value.QuestionType == null) {
                templateYesNO = '<div class="col-lg-3">'
                                 + '<b class="font-size-odag-100 theme_label">' + value.Description + '</b><br>'
                                 + '<input type="hidden" name="ODAGs[' + index + '].QuestionID" value="' + (index + 1) + '">';
                $.each(value.Options, function (innerIndex, innerValue) {
                    if (innerIndex == 0) {
                        var odagStyleClass = "ClassSuccess";
                    }
                    if (innerIndex == 1) {
                        var odagStyleClass = "ClassDanger";
                    }
                    if (innerIndex  == 2) {
                        var odagStyleClass = "ClassPrimary";
                    }
                    var radioClass = "radio-inline radioStyle  font-size-odag-100";
                    if (innerIndex == 0) {
                        radioClass = "font-size-odag-100";
                    }

                    templateYesNO += '<label class="' + radioClass + ' theme_label_data">';
                    templateYesNO += '<div><input id="radio' + index + '' + innerIndex + '" type="radio" name="ODAGs[' + index + '].OptionAnswer" class="checkbox-radio '+ odagStyleClass +'" value="' + innerValue.Value + '"><label for="radio' + index + '' + innerIndex + '""><span>                 </span>' + innerValue.Value + '</label></div>'
                                      + '</label>';
                });
                //templateYesNO += '<span class="btn pull-right"  style="margin-top:-6px"><i class="fa fa-close"></i> </span>';
                templateYesNO += "</div>";
                str = templateYesNO;
            }
            else if (value.QuestionType == "ObjectiveWithDateTime") {
                templateObjectiveWithDateTime = '<div class="col-lg-3">'
                                 + '<b class="font-size-odag-100 theme_label">' + value.Description + '</b><br>'
                                 + '<input type="hidden" name="ODAGs[' + index + '].QuestionID" value="' + (index + 1) + '">';
                $.each(value.Options, function (innerIndex, innerValue) {
                    if (innerIndex == 0) {
                        var odagStyleClass = "ClassSuccess";
                    }
                    if (innerIndex  == 1) {
                        var odagStyleClass = "ClassDanger";
                    }
                    if (innerIndex == 2) {
                        var odagStyleClass = "ClassPrimary";
                    }
                    var radioClass = "radio-inline  font-size-odag-100";
                    if (innerIndex == 0) {
                        radioClass = "font-size-odag-100";
                    }

                    templateObjectiveWithDateTime += '<label class="' + radioClass + ' theme_label_data">';
                    templateObjectiveWithDateTime += '<div><input id="radio' + index + '' + innerIndex + '" type="radio" name="ODAGs[' + index + '].OptionAnswer" class="checkbox-radio ' + odagStyleClass + '" value="' + innerValue.Value + '"><label for="radio' + index + '' + innerIndex + '""><span>                 </span>' + innerValue.Value + '</label></div>'
                                                      + '</label>';
                });
                templateObjectiveWithDateTime += '<label class="radio-inline font-size-100 theme_label_data"><input  name="ODAGs[' + index + '].OptionDate" id="ODAGs[' + index + '].OptionAnswer" type="text" class="form-control input-xs text-uppercase displayNone dateTimePicker" autocomplete="off"  placeholder="MM/DD/YYYY HH:MM:SS"></label>';

                //templateObjectiveWithDateTime += '<span class="btn pull-right"  style="margin-top:-6px"><i class="fa fa-close"></i> </span>';
                templateObjectiveWithDateTime += "</div>";
                str = templateObjectiveWithDateTime;
            }
            else if (value.QuestionType == "OnlyDateTime") {
                templateOnlyDateTime = '<div class="col-lg-3">'
                               + '<b class="font-size-odag-100 theme_label">' + value.Description + '</b><br>'
                               + '<input type="hidden" name="ODAGs[' + index + '].QuestionID" value="' + (index + 1) + '">'
                               + '<label class="font-size-100"><input  name="ODAGs[' + index + '].OptionDate" id="ODAGs[' + index + '].OptionDate" type="text" class="form-control input-xs text-uppercase dateTimePicker" autocomplete="off" placeholder="MM/DD/YYYY HH:MM:SS"></label>';
                templateOnlyDateTime += '<span class="btn theme_label_data" ><i class="fa fa-close"></i> </span>';
                templateOnlyDateTime += "</div>";
                str = templateOnlyDateTime;
            }
            else if (value.QuestionType == "Descriptive") {
                templateDescriptive = '<div class="col-lg-3">'
                                + '<b class="font-size-odag-100 theme_label">' + value.Description + '</b><br>'
                                + '<input type="hidden" name="ODAGs[' + index + '].QuestionID" value="' + (index + 1) + '">'
                                + '<input  name="ODAGs[' + index + '].OptionAnswer" id="ODAGs[' + index + '].OptionAnswer" type="text" class="form-control input-xs text-uppercase read_only_field" autocomplete="off" value="Ultimate" readonly="readonly">'
                                + '</div>';
                str = templateDescriptive;
            }

            if (str) {
                $("#odagQuestionContent").append(str);
            }
            if ((index + 1) % 4 == 0) {
                var clearfix = "<div class='clearfix'></div>";
                $("#odagQuestionContent").append(clearfix);
            }
        })
    }
    (function initCreateAuth() {
        //Generate ODAG
        generateOdag();
    })();
    (function listenerStubs() {
        var oneSecond = 1000;
        //$('#ODAGArea').off('click', '#collapse-link-odag').on('click', '#collapse-link-odag', function () {
        //    $("#ODAGContainerDiv").toggleClass("displayNone");
        //});
        //$('#providerPanel').off('click', '.pcpLabelReqProvider').on('click', '.pcpLabelReqProvider', function () {
        //    $("#RequestingProvider_FullName").val(memberData.Provider.ContactName);
        //});
        //$('#providerPanel').off('click', '.mbrLabelReqProvider').on('click', '.mbrLabelReqProvider', function () {
        //    $("#RequestingProvider_FullName").val(memberData.Member.PersonalInformation.FirstName + " " + memberData.Member.PersonalInformation.LastName);
        //});
        //$('#providerPanel').off('click', '.pcpLabelSVCProvider').on('click', '.pcpLabelSVCProvider', function () {
        //    $("#ServiceProvider_FullName").val(memberData.Provider.ContactName);
        //});
        setTimeout(function () {
            $('#odagQuestionContent input[type="radio"]').change(function () {
                if (this.value == "Y") {
                    $("input[name='" + this.name.split(".")[0] + ".OptionDate']").removeClass("displayNone");
                } else {
                    $("input[name='" + this.name.split(".")[0] + ".OptionDate']").addClass("displayNone");
                }
            })
        }, oneSecond);

    })()

})();