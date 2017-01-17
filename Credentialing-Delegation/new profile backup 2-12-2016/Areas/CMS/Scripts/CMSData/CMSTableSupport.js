$(document).ready(function () {

    /*--------------------Mouseover on Category---------------------*/

    $('.dropdown-submenu a.test').on("mouseover", function (e) {
        $(this).next('ul').toggle();
        //e.stopPropagation();
        //e.preventDefault();
    }).on("mouseleave", function (e) {
        $(this).next('ul').toggle();
    });

    $("input:checkbox.switchCss").bootstrapSwitch();

    /*-------------------Tooltip for the data------------------------*/
    $('[data-toggle="tooltip"]').tooltip();
    $('#QueueName').hide().text('');

    var resetCMSScreen = function () {
        $("#cmstablecontainer").css("height", $("#SharedModalBody").height() - 200);
    }

    $(window).resize(function () {
        resetCMSScreen();
    })

    resetCMSScreen();


    //============================ Search Filter ==========================================
    $('#cmsCategory').off('click', '.cmsdropdownlist').on('click', '.cmsdropdownlist', function () {
        $("#SelectedCategory").empty();
        $("#NoDataAvailable").empty();
        $(".alphabet").removeClass("btn-success").addClass("btn-default");
        $(".cmsalldata").removeClass("btn-success").addClass("btn-default");
        var countIt = 0;
        var search_filter = $(this)[0].title.toUpperCase();
        $("#SelectedCategory").append("<label>" + "<b>" + "<div class='btn btn-success btn-xs'>" + search_filter + "</div>" + "</b>" + "&nbsp;" + "Category is Selected" + "</label>");


        $("#cmslistpanel .cmslist").each(function () {
            if ($(this)[0].children[0].innerText.toUpperCase().search(new RegExp('\\b(' + search_filter + ')\\b')) < 0) {
                $(this).fadeOut();
            }
            else {
                $(this).show();
                countIt++;
            }
        });
        if (countIt == 0) {
            $("#NoDataAvailable").append("<div class='btn btn-default btn-xs'>" + 'No Data is Available' + "</div>")
            $('.totalcount').html(countIt);
        }
        else {
            $("#NoDataAvailable").empty();
            $('.totalcount').html(countIt);
        }
    });
    //============================ /Search Filter ==========================================



    /*---------- filter CMS List by Alphabet-------------*/

    var _alphabets = $('.alphabet');
    var _contentRows = $('.cmslist');
    var data = this;
    $('#cmslistpanel').off('click', '.alphabet').on('click', '.alphabet', function () {

        $("#SelectedCategory").empty();
        $("#NoDataAvailable").empty();

        $(".alphabet").removeClass("btn-success").addClass("btn-default");
        $(".cmsalldata").removeClass("btn-success").addClass("btn-default");
        ///<summary>filter CMS List by Alphabet</summary>

        var _letter = $(this), _text = $(this).text(), _count = 0;

        $("#alphabaticfilter" + _letter[0].innerHTML).removeClass("btn-default").addClass("btn-success");

        _contentRows.hide();
        _contentRows.each(function (i) {
            var _cellText = $(this).children('div.mdm_title').eq(0).text();
            if (RegExp('^' + _text).test(_cellText)) {
                _count += 1;
                $(this).fadeIn(400);
            }
        });
        $('.totalcount').empty().append(_count);
    });

    $('#cmslistpanel').off('click', '.cmsalldata').on('click', '.cmsalldata', function () {
        _contentRows.show();
        $("#SelectedCategory").empty();
        $("#NoDataAvailable").empty();
        $(".cmsalldata").removeClass("btn-default").addClass("btn-success");
        $(".alphabet").removeClass("btn-success").addClass("btn-default");
        $('.totalcount').empty().append(_contentRows.length);

    });
    /*----------------------Filter Method End-----------------------------*/



    //$('#cmsCategory').off('click', 'ul li a.cmsCode').on('click', 'ul li a.cmsCode', function () {

    //    var _letter = $(this), _text = $(this).text(), _count = 0;
    //    _contentRows.hide();
    //    _contentRows.each(function (i) {
    //        var _cellText = $(this).children('div.mdm_title').eq(0).text();
    //        if (RegExp('^' + _text).test(_cellText)) {
    //            _count += 1;
    //            $(this).fadeIn(400);
    //        }
    //    });
    //    $('.totalcount').empty().append(_count);
    //});


    /*--------------- Search Entity in CMS Data List------------------------*/
    $("#Search").on('keyup', function () {

        ///<summary> Search Entity in CMS List</summary>

        var val = $("#Search").val();
        var count = 0;
        _contentRows.hide();

        _contentRows.each(function (i) {
            var _cellText = $(this).children('div.mdm_title').eq(0).text();
            if (_cellText.toUpperCase().indexOf(val.toUpperCase()) > -1) {
                count += 1;
                $(this).fadeIn(400);
            }
        })
        $('.totalcount').empty().append(count);
    });
    /*----------------------------Search Method Ends---------------------*/
});

/* Validate Form during Add or Edit*/
var validateForm = function () {
    var form = $('#AddEditDiv').find('form');
    form.removeData("validator").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
    form.valid();
};






//$("tbody tr td button i.add-row").click(function () {

//    var title = $("#title" + index).text();
//    var code = $("#code" + index).text();
//    var desc = $("#desc" + index).text();
//    var status = $("#status" + index).text();
//    var icon = $("#status" + index).attr("class")
//    var markup = "<tr id='row"+index+"'><td class='maxWidthXLem' id='code"+ index +"'>" + title +
//        "</td><td class='maxWidthXLem'  id='code" + index + ">" + code +
//        "</td><td class='maxWidthXLem' id=desc" + index + ">" + desc +
//        "</td><td><i id=status" + index + " class='icon'></i></td><th><div class='btn-group' role='group'><button type='button' name='edit' id="+index+" class='btn btn-xs btn-primary editclass'><i class='fa fa-edit'></i></button></div></th></tr>";


//    $("table tbody").append(markup);
//});

/* Converting String to object*/
//var obj = $.parseJSON(jsonObj);
/* Loop through Array */
//$.each(obj, function (index, value) {

//    $(".addeditinputclass" + index).val(value);
//});