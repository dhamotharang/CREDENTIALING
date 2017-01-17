var chart = null;

$(window).on('beforeunload', function () {
    return 'Are you sure you want to leave?';
});
if (appConfig.toUpperCase() != 'UM') {
    $(document).ajaxError(function () {
        PopNotify("Error", "An Error Occured. Please Try Again Later.", "error");
    });
}


$(document).ready(function () {
    var tab
    if (appConfig.toUpperCase() == 'CLAIMS') {
        $(".inner_menu_item").first().click();
    }
    if (appConfig.toUpperCase() == 'CREDAXIS') {
        tab = {
            "tabAction": "Search Provider",
            "tabTitle": "SEARCH PROVIDER",
            "tabPath": "~/Areas/CredAxis/Views/SearchProvider/_SearchProvider.cshtml",
            "tabContainer": "fullBodyContainer"
        }
        TabManager.navigateToTab(tab);
    }
    if (appConfig.toUpperCase() == 'MH') {
        tab = {
            "tabAction": "Search Member",
            "tabTitle": "Search Member",
            "tabPath": "/MH/SearchMember/Index",
            "tabContainer": "fullBodyContainer"
        }
        TabManager.navigateToTab(tab);
    }
    if (appConfig.toUpperCase() == 'UM') {
        tab = {
            "tabAction": "Search Member",
            "tabTitle": "Search Member",
            "tabPath": "~/Areas/UM/Views/SearchMember/_SearchMember.cshtml",
            "tabContainer": "fullBodyContainer"
        }
        TabManager.navigateToTab(tab);
    }
    if (appConfig.toUpperCase() == 'ETL') {
        var tab = {
            "tabAction": "EDW Logger",
            "tabTitle": "EDW Logger",
            "tabPath": "/ETL/EdwLogger/DisplayEdwLogger",
            "tabContainer": "fullBodyContainer"
        }
        TabManager.navigateToTab(tab);
    }
});
/** ******  left menu  *********************** **/
var getPercen = function () {
    var p;
    if (appConfig.toUpperCase() == 'UM') {
        p = (($("span:contains('Search Member')").width() / $(".main_container").width()) * 100) + 1;
    }
    else if (appConfig.toUpperCase() == 'ETL') {
        var p = (($("span:contains('Monthly Claims Plan Vs Schema')").width() / $(".main_container").width()) * 100);
    }
    else if (appConfig.toUpperCase() == 'CREDAXIS') {
        p = (($("span:contains('Task Tracker')").width() / $(".main_container").width()) * 100) + 1;
    }
    else {
        p = (($("span:contains('Utilization Management')").width() / $(".main_container").width()) * 100) + 1;
    }
    return p;
}

/*------------------------------------------------*/
//Table Search Filters
var filters = false;
var ShowSearchFilters = function (btnclass, tableheaderid) {
    if (filters === false) {
        $("." + btnclass).find('i').removeClass('fa fa-caret-down').addClass('fa fa-caret-up');
        $('#' + tableheaderid).find('tr').children('th').each(function () {
            var spanelem = $(this).children('span');
            spanelem.find('input').css('outline', 0);
            spanelem.find('label').addClass('searchfilterlabel');
            spanelem.find('span').addClass('searchfilterspan');
        });
        filters = true;
    } else {
        $("." + btnclass).find('i').removeClass('fa fa-caret-up').addClass('fa fa-caret-down');
        $('#' + tableheaderid).find('tr').children('th').each(function () {
            var spanelem = $(this).children('span');
            spanelem.find('input').css('outline', 'none');
            spanelem.find('label').removeClass('searchfilterlabel');
            spanelem.find('span').removeClass('searchfilterspan');
        });
        filters = false;
    }
    return;
}
/*------------------------------------------------*/
/*App and Work Menu Toggle*/
$(function () {
    $('#appWorkToggle').change(function () {
        if ($(this).prop('checked')) {
            $('.work_menu').hide("slow");
            $('.app_menu').show("slow");
        }
        else {
            $('.work_menu').show("slow");
            $('.app_menu').hide("slow");
        }
    });
});
var HideAppMenu = function () {
    $("#menuBar").addClass("sideBarCollapse hidden");
    $("#menuBar").removeClass("sideBarExpand");
    $("#mainBody").addClass("mainBodyExpand");
    $("#mainBody").removeClass("mainBodyCollapse");
    $("#mainBody").css("width", "100%");
    $("#mainContent").css("width", "100%");

};

var ShowAppMenu = function () {
    $("#menuBar").addClass("sideBarExpand");
    $("#menuBar").removeClass("sideBarCollapse hidden");
    $("#mainBody").removeClass("mainBodyExpand");
    $("#mainBody").addClass("mainBodyCollapse");
    $(".mainBodyCollapse").css("width", $(".main_container").width() - $("#sidebar-menu").width());
    var percen = getPercen();
    var sideBarVal;
    if (appConfig.toUpperCase() == 'CREDAXIS') {
        sideBarVal = (percen * $(".main_container").width() * 0.013) + 60;
    }
    else if (appConfig.toUpperCase() == 'ETL') {
        var sideBarVal = (percen * $(".main_container").width() * 0.013) - 15;
    }
    else {
        sideBarVal = (percen * $(".main_container").width() * 0.013) + 40;
    }
    var mainBodyVal = $(".main_container").width() - sideBarVal;
    $(".sideBarExpand").css("width", sideBarVal);
    $(".mainBodyCollapse").css("width", mainBodyVal);

};

/* /App and Work Menu Toggle*/
$(function () {
    $('#menu_toggle').click(function () {
        if ($("#menuBar").hasClass("sideBarCollapse")) {
            ShowAppMenu();
        }
        else {
            HideAppMenu();
        }
        setAllFieldsWidth();
        setRunningFooterWidth();
    });

});

/* Sidebar Menu active class */
$(function () {

    $("ul.memberinnermenu li a").click(function () {
        if ($(this).parents().hasClass('sideBarExpand')) {
            $(this).parents("ul.memberinnermenu").children("li").children("a").removeClass("active");
            $(this).addClass("active");
        }
    });
    $("ul.memberinnermenu li").click(function () {
        $(".anchorselected").removeClass("anchorselected");
        $(this).addClass("anchorselected");
        if ($(this).parents().hasClass('sideBarCollapse')) {
            $(this).parents("ul.memberinnermenu").children("li").children("a").removeClass("active");
            $(this).children("a").addClass("active");
        }
    });
});

/** ******  /left menu  *********************** **/
/** ******  right_col height flexible + menu flexible  *********************** **/
var resetscreen = function () {
    $('body').css('overflow', 'hidden')
    $(".right_col").css("min-height", $(window).height());

    $("#sidebar-menu").animate({ width: $("#menuBar").outerWidth() }, 'fast');
    $("#menu-container").css("height", $(window).height() - 132);
    $(".runningbar").css("width", $("#mainBody").outerWidth());
    if ($('#menuBar').hasClass('sideBarCollapse')) {
        if ($("#sidebar-menu").outerWidth() < 100) {
            $("#menu-container").css("padding-right", 0);
            $(".app_menu").css({ "width": $("#menuBar").css("width"), "display": "inline-block" });
        }
        else {
            $(".work_menu").css("padding-right", 0);
            $(".app_menu").css({ "width": $("#menuBar").css("width"), "display": "inline-block" });
        }
    }
    else {
        if ($("#sidebar-menu").outerWidth() < 100) {
            $("#menu-container").css("height", $(window).height());
        }
        else {
            $("#menu-container").css("width", parseInt($("#menuBar").css("width").split("px")[0]) + 5);
            $(".app_menu").css({ "width": $("#menuBar").css("width"), "display": "inline-block" });
            $(".work_menu").css("padding-right", 0);
        }
    }



    if (!$("#menuBar").hasClass("sideBarCollapse")) {
        var percen = getPercen();
        var sideBarVal;
        var scrollBarWidth = 6;
        if (appConfig.toUpperCase() == 'CREDAXIS') {
            sideBarVal = (percen * $(".main_container").width() * 0.013) + 60;
        }
        else if (appConfig.toUpperCase() == 'ETL') {
            var sideBarVal = (percen * $(".main_container").width() * 0.013) - 15;
        }
        else {
            sideBarVal = (percen * $(".main_container").width() * 0.013) + 40;
        }

        var mainBodyVal = $(".main_container").width() - sideBarVal;

        var styScroll = '<style id="scrollVisibility">body::-webkit-scrollbar{display:none;}</style>'

        $(".sideBarExpand").css("width", sideBarVal);
        $(".mainBodyCollapse").css("width", mainBodyVal);
        $('#mainContent').css("width", mainBodyVal - scrollBarWidth);
        if ($('#breadVal').text() == "FACILITY QUEUE") {
            $('head').append(styScroll);
        } else {
            $("#scrollVisibility").remove();
        }

        setTimeout(function () {
            $("#menuBar").css("width", sideBarVal);
            $("#sidebar-menu").css("width", sideBarVal);
            $("#menu-container").css("width", parseInt($("#menuBar").css("width").split("px")[0]) + 1);
            $(".app_menu").css({ "width": $("#menuBar").css("width"), "display": "inline-block" });
            $(".sideBarCollapse li .label_on_collapsed").css("width", $("#menuBar").css("width").split("px")[0] - 15);
            $(".runningbar").css("width", $("#mainBody").width() + 20);
        }, 300);

    }


    if ($(window).width() > 2700) {
        $(".left-scroll-button-tab").hide();
        $(".right-scroll-button-tab").hide();
    }

    setTimeout(function () {
        setBodyHeight();
        $('#menu_section').css("height", $('#mainContent').height())
    }, 500);
    function setBodyHeight() {
        var containerHeight, bodyHeight, windowHeight, finalHeight;
        bodyHeight = $("#mainBody").height() - 100;
        windowHeight = $(window).height();
        if ($("#fullBodyContainer").css("display") == "none") {
            containerHeight = $("#partialBodyContainer").height();
            $("#partialBodyContainer").css("min-height", windowHeight);
        }
        else if ($("#partialBodyContainer").css("display") == "none") {
            containerHeight = $("#fullBodyContainer").height();
            $("#fullBodyContainer").css("min-height", windowHeight);
        }

        //(containerHeight >= bodyHeight && containerHeight >= windowHeight) ? (finalHeight = containerHeight) : (bodyHeight >= containerHeight && bodyHeight >= windowHeight) ? (finalHeight = bodyHeight) : (finalHeight = windowHeight);
        if (windowHeight > containerHeight) {
            $("#mainBody").css("min-height", windowHeight);
        }
        else {
            $("#mainBody").css("min-height", "auto");
        }
        if ($("#fullBodyContainer").css("display") == "none") {
            $("#innerTabContainer").css("min-height", windowHeight - 250);
        }
        else if ($("#partialBodyContainer").css("display") == "none") {
            $("#fullBodyContainer").css("min-height", windowHeight - 110);
        }
        $(".modal_body").css("height", 750);
        $(".modal_body").css("width", 815);

    };
    setAllFieldsWidth();
    setRunningFooterWidth();
    TabManager.setCenterModalHeight();
}


var setRunningFooterWidth = function () {
    if ($('#UMRunningFooter')) {
        $('#UMRunningFooter').css("width", $('#mainContent').css("width"))
    }
}

/*Color pallet default selected*/
$(function () {
    $(".theme_picker").removeClass('active');
    $(".theme_picker[data-theme-filepath='" + $('#CurrentTheme').attr('href') + "']").addClass('active');
})
/*Color pallet default selected*/
$(function () {

    resetscreen();

    $(window).resize(function () {
        resetscreen();
        if ($(window).width() > 2700) {
            $(".left-scroll-button-tab").hide();
            $(".right-scroll-button-tab").hide();
        }
        else {
            $(".left-scroll-button-tab").show();
            $(".right-scroll-button-tab").show();
        }
    });

    function showFloatMenu() {
        $(".work_menu").removeAttr("style");
        $(".work_menu_overlay").show();
        $('.float_menu_toggle').hide();
        $('#backToMenu').show();
        $('#backToMemberMenu').hide();
    }

    function hideFloatMenu() {
        $(".work_menu").css("right", -($("#sidebar-menu").outerWidth() + 80)).css("background", "rgba(0,0,0,0.5)");
        $(".work_menu_overlay").hide();
        $('#backToMenu').hide();
        $('#backToMemberMenu').show();
        $('.float_menu_toggle').show();
    }

    $('#backToMemberMenu').hide();
    $('#backToMenu').hide();
    $("#sidebarColors").mouseover(function () {
        hideFloatMenu();
    });

    $('body').on('click', "#backToMemberMenu", function () {
        showFloatMenu();
    });


    $('body').on('click', "#backToMenu", function () {
        var tab;
        if (appConfig.toUpperCase() == 'UM') {
            tab = {
                "tabAction": "Own Queue",
                "tabTitle": "Barbara Joy Queue",
                "tabPath": "/UM/Queue/GetQueue?QueueType=OWN&QueueTab=work",
                "tabContainer": "fullBodyContainer",
                "tabDataMoveRight": "true"
            }
            TabManager.navigateToTab(tab);
        }
        else if (appConfig.toUpperCase() == 'MH' || appConfig.toUpperCase() == 'CLAIMS') {
            hideFloatMenu();
        }
        else if (appConfig.toUpperCase() == 'CREDAXIS') {
            tab = {
                "tabAction": "Search Provider",
                "tabTitle": "SEARCH PROVIDER",
                "tabPath": "~/Areas/CredAxis/Views/SearchProvider/_SearchProvider.cshtml",
                "tabContainer": "fullBodyContainer"
            }
            TabManager.navigateToTab(tab);
            hideFloatMenu();
        }
    });
    $(".float_menu_toggle").click(function () {
        showFloatMenu();
    })
    $('#QueueName').hide();
});
/** ******  /right_col height flexible + menu flexible  *********************** **/



/** ******  tooltip  *********************** **/
$(function () {
    $('[data-role="tooltip"]').tooltip();
})
/** ******  /tooltip  *********************** **/

/** ******  collapse panel  *********************** **/
// Close ibox function
$('.close-link').click(function () {
    var content = $(this).closest('div.x_panel');
    content.remove();
});

// Collapse ibox function

/** ******  /collapse panel  *********************** **/
/** ******  iswitch  *********************** **/
if ($("input.flat")[0]) {
    $(document).ready(function () {
        $('input.flat').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green'
        });
    });
}
/** ******  /iswitch  *********************** **/

/**Gradient Color for SideBar**/
$(function () {
    $(".sidebarColorPallet").click(function () {
        $('#sidebar-menu').attr({ 'style': '' });
        $('#menu_section').attr('style', '');
        $('#sidebar-menu,#sidebar-menu span,#sidebar-menu .fa,.inner_menu_item').attr({ 'style': '' });
        var hr = $(this).attr("href");
        $("#SidebarBackground").attr("href", hr);
        //Giving same gradient to the collapsed menu
        if ($("#menuBar").hasClass("sideBarCollapse")) {
            var className = (hr.split("/")[hr.split("/").length - 1]).split(".")[0];
            $(".child_menu").removeClass().addClass("nav child_menu " + className);
        }
        event.preventDefault();
    });

    $(".theme_picker").click(function () {
        var filepath = $(this).data("themeFilepath");
        $('#CurrentTheme').prop("href", filepath);
        $(".theme_picker").removeClass('active');
        $(".theme_picker[data-theme-filepath='" + $('#CurrentTheme').attr('href') + "']").addClass('active');
        event.preventDefault();
    });

    $(".floatMemberColorPallet").click(function (ev) {
        $('.memberinnermenu').removeClass($('.memberinnermenu').attr('class').split(" ")[3]);
        $('.runningbar').removeClass($('.runningbar').attr('class').split('runningbar ')[1]).addClass(' ' + ev.target.classList[1]);
        $('.memberinnermenu').addClass(ev.target.classList[1]);
        $('#topTabs').removeClass().addClass(ev.target.classList[1]);
        event.preventDefault();
    });


    $(".topNavBarColorPallet").click(function (ev) {
        $('.nav_menu').attr({ 'style': '' });
        $('.nav_menu .fa').attr({ 'style': '' });
        $('.nav_menu .nav.navbar-nav > li > a').attr({ 'style': '' });
        $('#searchBox').attr({ 'style': '' });
        $('.nav_menu').removeClass().addClass('nav_menu');
        $('.nav_menu').addClass(ev.target.classList[1]);
        if ($('#menuBar').attr('class').split(' ').length == 2) {
            $('#menuBar').addClass(ev.target.classList[1] + 'Active');
        }
        else if ($('#menuBar').attr('class').split(' ').length > 2) {
            for (var i in $('#menuBar').attr('class').split(' ')) {
                if ($('#menuBar').attr('class').split(' ')[i].search("Active") > 0) {
                    var indexClass = i;
                }
            }
            $('#menuBar').removeClass($('#menuBar').attr('class').split(' ')[indexClass]).addClass(ev.target.classList[1] + 'Active');
        }
        event.preventDefault();
    });


    $('.color_dropdown').on('click', function (event) {
        $(this).parent().toggleClass('open');
        event.stopImmediatePropagation();
    });
    $('body').on('click', function (e) {
        if (!$('.color_dropdown').is(e.target)
            && $('.color_dropdown').has(e.target).length === 0
            && $('.open').has(e.target).length === 0
        ) {
            $('.color_dropdown').removeClass('open');
        }
        if ($(e.target).parents().hasClass('color_list')) {
            event.stopImmediatePropagation();
        }
    });
});
/** /Gradient Color for SideBar**/
/* /Utility Functions*/
var setBreadCrum = function (id) {

    if (event && !$(event.currentTarget).hasClass("chrome-tab-current")) {
        $('#refnum').hide();
        $('#reftitle').hide();
    }
    if (id) {
        var idz = id;
        if (typeof id == 'object') {
            idz = $(id).children()[1].innerHTML.toLowerCase();
            if (Object.keys(id).length === 0) {
                return;
            }
        }

        $('#breadcrumbArea').html("");
        var floatClass = $('#FloatMenu')[0].classList[$('#FloatMenu')[0].classList.length - 1];
        if (floatClass != "memberinnermenu") {
            var finalClass = floatClass;
        }
        var sideCss = $('.main_menu_side').css("background");
        var create = '<a href="#" class="btn btn-default"><i class="fa fa-user fa-1x"></i>&nbsp;Barbara Joy</a><a href="#" class="btn btn-default">UM</a> <a href="#" class="btn btn-default">INTAKE</a><a href="#" class="btn btn-success" id="breadVal">CREATE AUTHORIZATION - EVELINE HEWETT</a>';
        var queue = '<a href="#" class="btn btn-default"><i class="fa fa-user fa-1x"></i>&nbsp;Barbara Joy</a><a href="#" class="btn btn-default">UM</a><a href="#" class="btn btn-success" id="breadVal">FACILITY QUEUE</a>';
        var history = '<a href="#" class="btn btn-default"><i class="fa fa-user fa-1x"></i>&nbsp;Barbara Joy</a><a href="#" class="btn btn-default">UM</a><a href="#" class="btn btn-default">EVELINE HEWETT</a><a href="#" class="btn btn-success" id="breadVal">AUTH HISTORY</a>'
        var viewhistory = '<a href="#" class="btn btn-default"><i class="fa fa-user fa-1x"></i>&nbsp;Barbara Joy</a><a href="#" class="btn btn-default">UM</a><a href="#" class="btn btn-default">EVELINE HEWETT</a><a href="#" class="btn btn-success" id="breadVal">VIEW HISTORY</a>'
        var claims = '<a href="#" class="btn btn-default"><i class="fa fa-user fa-1x"></i>&nbsp;Barbara Joy</a><a href="#" class="btn btn-default">CLAIMS</a><a href="#" class="btn btn-default">EVELINE HEWETT</a><a href="#" class="btn btn-success" id="breadVal">CLAIMS HISTORY</a>';
        var viewauth = '<a href="#" class="btn btn-default"><i class="fa fa-user fa-1x"></i>&nbsp;Barbara Joy</a><a href="#" class="btn btn-default">UM</a><a href="#" class="btn btn-default">EVELINE HEWETT</a><a href="#" class="btn btn-success" id="breadVal">REF# 1607050015</a>';
        var member = '<a href="#" class="btn btn-default"><i class="fa fa-user fa-1x"></i>&nbsp;Barbara Joy</a><a href="#" class="btn btn-default">UM</a><a href="#" class="btn btn-success" id="breadVal">PROFILE - EVELINE HEWETT</a>';
        switch (idz) {
            case 'create new auth':
                $('#breadcrumbArea').html(create);
                if (finalClass) {
                    $('#topTabs').removeClass().addClass(finalClass);
                }
                break;
            case 'queue':
                $('#breadcrumbArea').html(queue);
                $('#topTabs').removeClass().css("background", sideCss);
                $('.bottomfixed').hide();
                break;
            case 'auth history':
                $('#breadcrumbArea').html(history);
                if (finalClass) {
                    $('#topTabs').removeClass().addClass(finalClass);
                }
                $('.bottomfixed').show();
                break;
            case 'view history':
                $('#breadcrumbArea').html(viewhistory);
                $('#refnum').show();
                $('#reftitle').show();
                $('.bottomfixed').show();
                if (finalClass) {
                    $('#topTabs').removeClass().addClass(finalClass);
                }
                break;
            case 'claims history':
                $('#breadcrumbArea').html(claims);
                if (finalClass) {
                    $('#topTabs').removeClass().addClass(finalClass);
                }
                $('.bottomfixed').show();
                break;
            case 'member':
                $('.chrome-tab-current').click();
                $('.bottomfixed').show();
                if (finalClass) {
                    $('#topTabs').removeClass().addClass(finalClass);
                }
                break;
            case 'ref# 1607050015':
                $('#breadcrumbArea').html(viewauth);
                $('#refnum').show();
                $('#reftitle').show();
                $('.bottomfixed').show();
                if (finalClass) {
                    $('#topTabs').removeClass().addClass(finalClass);
                }
                break;
            case 'profile':
                $('#breadcrumbArea').html(member);
                $('#refnum').hide();
                $('#reftitle').hide();
                if (finalClass) {
                    $('#topTabs').removeClass().addClass(finalClass);
                }
                break;

            default: break;
        }
    }

    resetscreen();

};
/** Page Container Animation**/


var innerTabs = ['DataContainer'];

var openContainer = function (e, pagename, innerTabTitle) {

    $("#Realestatelabel").empty().text($(e).attr('data-val-realestate'));

    setBreadCrum(innerTabTitle.toLowerCase());
    var isPresent = false;
    document.title = innerTabTitle + " | AHC";
    for (var i = 0; i < innerTabs.length; i++) {
        if (innerTabs[i] == pagename) {
            isPresent = true;
        }
    }
    if (!isPresent) {
        $('#DataContainer').scrollTop();

        $(".contenttab").removeAttr("style");
        chromeTabs.addNewTab($chromeTabsExampleShell, {
            title: innerTabTitle,
            data: {
                'id': pagename
            }
        });

        innerTabs.push(pagename);


        $("div[data-id='" + pagename + "']").effect("bounce");


        $("#DataContainer .active.in").hide().show("scale", { percent: 80 }, 500);

    }
    else {
        $("div[data-id='" + pagename + "']").trigger("click");
        if (pagename == 'claimListContainer') {
            $('#Realestatelabel').replaceWith("<strong>CLAIMS</strong>");
        }
        else if (pagename == 'authHistory') {
            $('#Realestatelabel').replaceWith("<strong>INTAKE</strong>");
        }
    }
};

var setPageTitle = function (pageTitle) {
    document.title = pageTitle + " | AHC";

    if (pageTitle.toLowerCase() == 'create auth' || pageTitle.toLowerCase() == 'auth history' || pageTitle.toLowerCase() == 'view history') {
        $('#Realestatelabel').replaceWith("<strong>INTAKE</strong>");
    }
};
/** /Page Container Animation**/

/** USE PCP USE MBR buttons**/
$('#mainBody').on('click', '.options_label', function (ev) {
    if (ev.target.localName == "label") {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            $(this).parent().find('.options_label').removeClass('selected');
            $(this).addClass('selected');
        }
    }
});
/** /USE PCP USE MBR buttons**/

/** Show modal method**/
function showModal(id, type) {
    if (type == 'draggable') {
        $("#" + id).modal({
            backdrop: "static",
            show: true
        }).draggable({
            handle: ".modal-header"
        });
    }
    else {
        $("#" + id).modal({
            backdrop: "static",
            show: true
        });
    }
}
/** /Show modal method**/

/* View on Request */
var requestView = function (e) {
    $.ajax({
        type: "POST",
        data: $(e).attr("data-url"),
        url: "demo_test.txt",
        success: function (result) {
            $("#" + $(e).attr("data-loading")).html(result);
        }
    });
}
/* /View on Request */



/*Utility Functions*/
function nodeScriptReplace(node) {
    if (nodeScriptIs(node) === true) {
        node.parentNode.replaceChild(nodeScriptClone(node), node);
    }
    else {
        var i = 0;
        var children = node.childNodes;
        while (i < children.length) {
            nodeScriptReplace(children[i++]);
        }
    }

    return node;
}
function nodeScriptIs(node) {
    return node.tagName === 'SCRIPT';
}
function nodeScriptClone(node) {
    var script = document.createElement("script");
    script.text = node.innerHTML;
    for (var i = node.attributes.length - 1; i >= 0; i--) {
        script.setAttribute(node.attributes[i].name, node.attributes[i].value);
    }
    return script;
}
function InitICheckFinal() {
    $('input.flat').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green'
    });
}

/*Methods for Notification Color Management*/
var mouseOutMap = function () {
    $('#previewArea').css('background-color', "#FFFFFF")
};

var mouseOverColor = function (colorcode) {
    $('#previewArea').css('background-color', colorcode);
};

var updateNotificationColor = function (colorcode) {

    switch ($('#colorArea').val()) {
        case "topbarcolor":
            $('.nav_menu').css({ 'background-color': colorcode, 'background-image': "none" });
            break;
        case "topbariconcolor":
            $('#customsearch').remove();
            $('.nav_menu .fa').css({ 'color': colorcode });
            $('.nav_menu .nav.navbar-nav > li > a').css({ 'color': colorcode });
            $('#searchBox').css({ 'color': colorcode });
            $('.user-profile').attr("style", "color:" + colorcode + " !important");
            break;
        case "sidebarcolor":
            $('#sidebar-menu').attr('style', 'background-color:' + colorcode + ' !important;');
            $('.memberinnermenu').attr('style', 'background-color:' + colorcode + ' !important;');
            $('#menu_section').attr('style', 'background-color:' + colorcode + ' !important;');
            $('#SidebarBackground').attr('href', '');
            break;
        case "sidebariconcolor":
            $('#sidebar-menu,#sidebar-menu span,.inner_menu_item').attr({ 'style': 'color:' + colorcode + ' !important;' });
            $('#sidebar-menu .fa').attr('style', 'color:' + colorcode);
            break;
        case "tableheader":
            if ($('#customTheadBackClass')) {
                $('#customTheadBackClass').remove();
            }
            var theadcolor = '<style id="customTheadBackClass">.custom-thead-back > thead{background-color: ' + colorcode + ' !important;}</style>';
            $('head').append(theadcolor);
            break;
        case "tableheaderfont":
            if ($('#customTheadFontClass')) {
                $('#customTheadFontClass').remove();
            }
            var theadfont = '<style id="customTheadFontClass">.custom-thead-font > thead > tr> th {color: ' + colorcode + ' !important;}.custom-thead-font .styled-input > label {color: ' + colorcode + ' !important;}</style>';
            $('head').append(theadfont);
            break;
        case "tablestripe":
            if ($('#theadClass')) {
                $('#theadClass').remove();
            }
            var spp = '<style id="theadClass">.custom-table-striped > tbody > tr:nth-child(2n-1) { background-color: ' + colorcode + '; transition: all .125s ease-in-out;  }</style>';
            $('head').append(spp);
            break;
        case "tablebodyfont":
            if ($('#customTbodyFontClass')) {
                $('#customTbodyFontClass').remove();
            }
            var tbodyfont = '<style id="customTbodyFontClass">.custom-tbody > tbody td {color: ' + colorcode + ' !important;}.custom-tbody > tbody td label {color: ' + colorcode + ' !important;}</style>';
            $('head').append(tbodyfont);
            break;
        default: break;
    }
    $('#html5colorpicker').val(colorcode);
};
/*Methods for Notification Color Management ENDS*/

/* Font-Size Management*/

$('.font-resizer > .inc_font').click(function () {
    var currentFontSize = parseInt($("body").css('font-size').split("px")[0]);
    if (currentFontSize < 18) {
        $("body").css('font-size', currentFontSize + 1);
        var currentSize = $("body").css('font-size');
        $(".tooltip-inner").text("Font Size : " + currentSize);
    }
    else {
        $(".tooltip-inner").text("Max. Font Size : 18px");
        $(".tooltip-inner").attr("style", "color:red !important;");
    }
    $(".options_label.mbr").css("right", $(".options_label.mbr").outerWidth() + 6);
    setAllFieldsWidth();
});
$('.font-resizer > .fa-font').click(function () {
    $("body").css('font-size', 12);
    var currentSize = $("body").css('font-size');
    $(".tooltip-inner").text("Font Size : " + currentSize);
    $(".options_label.mbr").css("right", $(".options_label.mbr").outerWidth() + 6);
    setAllFieldsWidth();
});
$('.font-resizer > .dec_font').click(function () {
    var currentFontSize = parseInt($("body").css('font-size').split("px")[0]);
    if (currentFontSize > 10) {
        $("body").css('font-size', currentFontSize - 1);
        var currentSize = $("body").css('font-size');
        $(".tooltip-inner").text("Font Size : " + currentSize);
    }
    else {
        $(".tooltip-inner").text("Min. Font Size : 10px");
        $(".tooltip-inner").attr("style", "color:red !important;");
    }
    $(".options_label.mbr").css("right", $(".options_label.mbr").outerWidth() + 6);
    setAllFieldsWidth();
});
$('.font-resizer,.font-resizer > .dec_font,.font-resizer > .fa-font,.font-resizer > .inc_font,.resize').hover(function () {
    $(".tooltip-inner").attr("style", "color:#333 !important;");
    var currentFontSize = $("body").css('font-size');
    $(".tooltip-inner").text("Font Size : " + currentFontSize);
    setAllFieldsWidth();
});
/* /Font-Size Management*/


/* Custom jQuery plugins*/
String.prototype.trimToLength = function (m) {
    return (this.length > m)
      ? jQuery.trim(this).substring(0, m - 3) + "..."
      : this;
};

String.prototype.formatTelephone = function () {
    var tel = "";

    for (var i = 0; i < this.length; i++) {
        tel = tel + this[i];
    }

    if (!tel) {
        return '';
    }

    if (tel.toString().length == 13) {
        tel = tel.toString().trim();
        if (tel[0] == '+' && tel[1] == '1' && tel[2] == '-')
            tel = tel.substring(3, 13);
    }

    var value = tel.toString().trim().replace(/^\+/, '');

    if (value.match(/[^0-9]/)) {
        return tel;
    }

    var city, number;

    switch (value.length) {
        case 1:
        case 2:
        case 3:
            city = value;
            break;

        default:
            city = value.slice(0, 3);
            number = value.slice(3);
    }

    if (number) {
        if (number.length > 3) {
            number = number.slice(0, 3) + '-' + number.slice(3, 7);
        }
        return ("(" + city + ") " + number).trim();
    }
    else {
        return "(" + city;
    }

}
/* /Custom jQuery plugins*/

/*  Custom Date Formate  */

String.prototype.formatDate = function (type) {
    if (this) {
        var date = "";
        var dformat;

        for (var i = 0; i < this.length; i++) {
            date = date + this[i];
        }

        Number.prototype.padLeft = function (base, chr) {
            var len = (String(base || 10).length - String(this).length) + 1;
            return len > 0 ? new Array(len).join(chr || '0') + this : this;
        }
        var d = new Date(this);
        if (typeof type != 'undefined') {
            if (type.toLowerCase() == 'datetime') {
                dformat = [(d.getMonth() + 1).padLeft(),
                     d.getDate().padLeft(),
                     d.getFullYear()].join('/') +
                ' ' +
                [d.getHours().padLeft(),
                  d.getMinutes().padLeft(),
                  d.getSeconds().padLeft()].join(':');
            }
        }
        else {
            dformat = [(d.getMonth() + 1).padLeft(),
                      d.getDate().padLeft(),
                      d.getFullYear()].join('/');
        }
        return dformat;
    }
};

String.prototype.getAge = function () {
    if (this) {
        var dateString = "";

        for (var i = 0; i < this.length; i++) {
            dateString = dateString + this[i];
        }

        var today = new Date();
        var birthDate = new Date(dateString);
        var age = today.getFullYear() - birthDate.getFullYear();
        var m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age--;
        }
        return age;
    }
};


$('.body').off('click', '#queueBucketsBtn').on('click', '#queueBucketsBtn', function () {
    data = {
        MemberId: $("#MemberID")[0].innerText,
        MemberName:$("#MemberName")[0].innerText,
        AuthType: $("#RequestType").val(),
        ActionPerformed: $(this)[0].dataset.action,
        CurrenUserRole: $(this)[0].dataset.currentuserrole,
        POS: $("#PlaceOfService").val(),
        OutPatientType: $("#OutPatientType").val()
    };
    TabManager.openSideModal('/AuthorizationAction/GetUserRoles', 'SMART ASSIGN', 'cancel', '', '', "", data);
});

//$('.body').off('click', '#moveWorkBtn').on('click', '#moveWorkBtn', function () {
//    data = {
//        TotalCount: $("#total-count")[0].innerText,
//        ExpeditedCount: $("#total-expedited")[0].innerText,
//        StandardCount: $("#total-standard")[0].innerText,
//        ActionPerformed: $(this)[0].dataset.action,
//        CurrenUserRole: $(this)[0].dataset.currentuserrole
//    };
//    TabManager.openSideModal('/AuthorizationAction/GetUserRoles', 'SMART ASSIGN', 'cancel', '', '', "", data);
//});

var toggleQueueList = function () {
    

};
var moveWorkQueueList = function (heading) {
    ShowModal("~/Views/Home/_moveWorkBuckets.cshtml", heading);
    setTimeout(function () {
        $(".bucketButtons").hide();
        $("#" + userQueueid).show();
    }, 1000);
}
var successBox = function () {
    $(".theCardContainer").hide();
    $(".theAnimationContainer").show("slide", { direction: "left" }, 300);
    $(".btn-group>.mbrBreadcrumbWidth:first-child").css({ "color": "#33333f", "background-color": "white" });
    $(".btn-group>.mbrBreadcrumbWidth:nth-child(2)").css({ "color": "#33333f", "background-color": "white" });
    $(".btn-group>.mbrBreadcrumbWidth:nth-child(3)").css({ "background-color": "#0ebf9d", "color": "white" });
    $(".overlay nav").css({ "display": "none" });

    setTimeout(function () {
        $(".progress").hide(200);
        $(".doneSuccess").show();
        setTimeout(function () {
            $(".close_btn").trigger("click");
        }, 700);

        setTimeout(function () {
            TabManager.closeCurrentlyActiveSubTab();
            setTimeout(function () {
                TabManager.navigateToTab({ "tabAction": "Facility Queue", "tabTitle": "Facility Queue", "tabPath": "~/Views/Queue/_Queue.cshtml", "tabContainer": "fullBodyContainer" });
            }, 500);

        }, 1500);
    }, 2000);

};

var successMoveWork = function () {
    $(".theCardContainer").hide();
    $(".theAnimationContainer").show("slide", { direction: "left" }, 300);
    $(".btn-group>.mbrBreadcrumbWidth:first-child").css({ "color": "#33333f", "background-color": "white" });
    $(".btn-group>.mbrBreadcrumbWidth:nth-child(2)").css({ "color": "#33333f", "background-color": "white" });
    $(".btn-group>.mbrBreadcrumbWidth:nth-child(3)").css({ "background-color": "#0ebf9d", "color": "white" });
    $(".overlay nav").css({ "display": "none" });

    setTimeout(function () {
        $(".progress").hide(200);
        $(".doneSuccess").show();
        setTimeout(function () {
            $(".close_btn").trigger("click");
        }, 700);


    }, 2000);

};
$('.btn-group.mbrBreadcrumbWidth1 a:first-child').click(function (e) {
    e.preventDefault();
    toggleUserList(this, id);
    return false;
});


var toggleUserList = function (types) {
    $("#" + types.id).css({ "color": "#0ebf9d", "border-color": "#0ebf9d" });
    $(".theCardContainer").show(500);
    $(".overlay nav").css({ "margin-top": "4%" });
    $(".btn.btn-app").css({ "height": "14%", "padding": "0.5% 1%" })
    $(".theMember").hide();
    $(".topLine").hide();
    $(".secondLine").hide();
    $(".theAnimationContainer").hide(100);
    $(".btn-group>.mbrBreadcrumbWidth:first-child").css({ "color": "#33333f", "background-color": "white" });
    $(".btn-group>.mbrBreadcrumbWidth:nth-child(2)").css({ "background-color": "#0ebf9d", "color": "white" });
}


/* Keeping these here temporarily. will not be needed later*/
var openViewAuth = function () {
    $('#home-tab1').click();
    openContainer(this, 'viewAuth', 'REF# 1607050015');
    $.ajax({
        url: '/Home/GetView',
        data: {},
        cache: false,
        type: "POST",
        dataType: "html",
        success: function (data) {
            SetViewAuthData(data);
        }
    });
};

function SetViewAuthData(data) {
    $("#viewAuth").html(data);
    setTimeout(function () {
        nodeScriptReplace(document.getElementById("viewAuth"));
    }, 1000);
    $(".tabs-menu").children().find("a").first().trigger("click");


    if ($("input.flat")[0]) {
        $(document).ready(function () {
            $('input.flat').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green'
            });
        });
    }
}

function SetData(data) {
    $("#viewAuthHistory").html(data);
    setTimeout(function () {
        nodeScriptReplace(document.getElementById("viewAuthHistory"));
    }, 1000);
    $(".tabs-menu").children().find("a").first().trigger("click");
    if ($("input.flat")[0]) {
        $(document).ready(function () {
            $('input.flat').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green'
            });
        });
    }
}
/* /Keeping these here temporarily. will not be needed later*/

//==================================================================================================
(function () {
    var triggerBttn = document.getElementById('trigger-overlay');
    var overlay = document.querySelector('div.overlay');
    if (overlay) {
        var closeBttn = overlay.querySelector('button.overlay-close');
    }
    var transEndEventNames = {
        'WebkitTransition': 'webkitTransitionEnd',
        'MozTransition': 'transitionend',
        'OTransition': 'oTransitionEnd',
        'msTransition': 'MSTransitionEnd',
        'transition': 'transitionend'
    },
    transEndEventName = transEndEventNames[Modernizr.prefixed('transition')],
    support = { transitions: Modernizr.csstransitions };

    function toggleOverlay() {
        if (classie.has(overlay, 'open')) {
            classie.remove(overlay, 'open');
            classie.add(overlay, 'close');
            var onEndTransitionFn = function (ev) {
                if (support.transitions) {
                    if (ev.propertyName !== 'visibility') {
                        return
                    };
                    this.removeEventListener(transEndEventName, onEndTransitionFn);
                }
                classie.remove(overlay, 'close');
            };
            if (support.transitions) {
                overlay.addEventListener(transEndEventName, onEndTransitionFn);
            }
            else {
                onEndTransitionFn();
            }
        }
        else if (!classie.has(overlay, 'close')) {
            classie.add(overlay, 'open');
        }
    }

    if (triggerBttn) {
        triggerBttn.addEventListener('click', toggleOverlay);
    }
    if (closeBttn) {
        closeBttn.addEventListener('click', toggleOverlay);
    }
})();



/*!
 * classie - class helper functions
 * from bonzo https://github.com/ded/bonzo
 * 
 * classie.has( elem, 'my-class' ) -> true/false
 * classie.add( elem, 'my-new-class' )
 * classie.remove( elem, 'my-unwanted-class' )
 * classie.toggle( elem, 'my-class' )
 */

/*jshint browser: true, strict: true, undef: true */
/*global define: false */

(function (window) {

    'use strict';

    // class helper functions from bonzo https://github.com/ded/bonzo

    function classReg(className) {
        return new RegExp("(^|\\s+)" + className + "(\\s+|$)");
    }

    // classList support for class management
    // altho to be fair, the api sucks because it won't accept multiple classes at once
    var hasClass, addClass, removeClass;

    if ('classList' in document.documentElement) {
        hasClass = function (elem, c) {
            return elem.classList.contains(c);
        };
        addClass = function (elem, c) {
            elem.classList.add(c);
        };
        removeClass = function (elem, c) {
            elem.classList.remove(c);
        };
    }
    else {
        hasClass = function (elem, c) {
            return classReg(c).test(elem.className);
        };
        addClass = function (elem, c) {
            if (!hasClass(elem, c)) {
                elem.className = elem.className + ' ' + c;
            }
        };
        removeClass = function (elem, c) {
            elem.className = elem.className.replace(classReg(c), ' ');
        };
    }

    function toggleClass(elem, c) {
        var fn = hasClass(elem, c) ? removeClass : addClass;
        fn(elem, c);
    }

    var classie = {
        // full names
        hasClass: hasClass,
        addClass: addClass,
        removeClass: removeClass,
        toggleClass: toggleClass,
        // short names
        has: hasClass,
        add: addClass,
        remove: removeClass,
        toggle: toggleClass
    };

    // transport
    if (typeof define === 'function' && define.amd) {
        // AMD
        define(classie);
    } else {
        // browser global
        window.classie = classie;
    }

})(window);


//--------------------sliding Modal--------------------------------------------------
function ShowModal(targetUrl, Header, callback, arg) {

    function CloseModal() {
        $slide_modal.html('');
        $slide_modal.animate({ width: '0px' }, 400, 'swing', function () {
            $modal_background.remove();
        });
    }

    var $template = '<div class="modal_background"><div class="slide_modal"><div class="modal_header"><h3 class="col-md-5">' + Header + '</h3><div class="col-lg-6 pull-right"><span class="close_btn"><button class="red-button close_modal_btn pull-right" >Cancel</button></span><span class="save_btn" ><button class="view-button pull-right SaveBtn" id="modalSaveBtn" data-index="">Save</button></div><div class="modal_body"></div></div></div>';
    $('body').append($template);
    var $window = $(window),
    $window_height = $window.height(),
    $window_width = $window.width(),
    $modal_background = $('.modal_background'),
    $slide_modal = $modal_background.find('.slide_modal');
    var $modal_header = $slide_modal.find('.modal_header');
    var $p = $modal_header.find('h3');
    var $btn = $modal_header.find('div');
    var $button = $btn.find('button');

    $slide_modal.css({ height: $window_height + 'px', width: '0px' });
    $modal_background.css({ height: $window_height + 'px', width: $window_width + 'px' });
    $modal_header.css({ height: 50 + 'px', backgroundColor: '#4a88c7', 'margin-top': '-15px', 'margin-left': '-5px', 'margin-right': '-5px' });
    $p.css({ color: 'white', 'margin-top': '15px' });
    $btn.css({ 'margin-top': '10px' });
    $button.css({ 'width': '60px', height: '30px', 'font-size': 'medium' });
    $slide_modal.animate({ width: $window_width * 0.8 + 'px' }, 400, 'swing', function () {

        //--------------------------click event for closing-------------------------------
        $modal_background.on('click', function () {
            CloseModal();

        });

        $slide_modal.on('click', function (e) {
            e.stopPropagation();
        });

        $slide_modal.find('.close_btn').on('click', function () {
            CloseModal();
        });
        //--------------------get partial view----------------------------------------        
        $.ajax({
            type: "POST",
            url: "/Home/GetPartialOnRequest?partialURL=" + targetUrl,
            success: function (result) {
                $slide_modal.find('.modal_body').html(result);
                TabManager.loadOrReloadScriptsUsingHtml(result, callback, arg);
            }
        });
    });
}

//---------------------Loading Symbol-------------------------------


var showLoaderSymbol = function (id) {
    var $ob = $('#loadingSample');
    var $clon = $ob.clone().addClass("loadingArea").removeClass("hidden");
    $('#' + id).prepend($clon);
};

var removeLoaderSymbol = function () {
    $('.loadingArea').fadeOut(function () {
        $(this).remove();
    });
};

//------------------------------------------------------------------



var setWidthForSelects = function () {
    var allSelects = $('.variable_width_select');
    for (var sel = 0; sel < allSelects.length; sel++) {
        var currentSel = allSelects[sel];
        var optionsList = $(currentSel).children();
        var maxWidth = 0;
        for (var opt = 0; opt < optionsList.length; opt++) {
            $('#ruler').text($(optionsList[opt]).text());
            var optWidth = $('#ruler').outerWidth() + 50;
            if (optWidth >= maxWidth) {
                maxWidth = optWidth;
            }
        }
        $(currentSel).css("width", maxWidth);
    }
    $('#ruler').text("");
};

var setWidthforDateFields = function () {
    var allDates = $('.variable_width_date');
    $('#ruler').text("MM/DD/YYYY");
    var dateWidth = $('#ruler').outerWidth() + 30;
    for (var date = 0; date < allDates.length; date++) {
        var currentDate = allDates[date];
        $(currentDate).css("width", dateWidth);
    }
    $('#ruler').text("");
};

var setWidthforDateTimeFields = function () {
    var allDates = $('.variable_width_datetime');
    $('#ruler').text("MM/DD/YYYY HH:MM:SS");
    var dateWidth = $('#ruler').outerWidth() + 30;
    for (var date = 0; date < allDates.length; date++) {
        var currentDate = allDates[date];
        $(currentDate).css("width", dateWidth);
    }
    $('#ruler').text("");
};

var setWidthforProviderFields = function () {
    var allProviderFields = $('.variable_width_providername');
    $('#ruler').text("MARIA SCHUZANIOAAN SINGHSINGH");
    var fieldWidth = $('#ruler').outerWidth() + 5;
    for (var prov = 0; prov < allProviderFields.length; prov++) {
        var currentProv = allProviderFields[prov];
        $(currentProv).css("width", fieldWidth);
    }
    $('#ruler').text("");
};

var setWidthforFacilityFields = function () {
    var allFacilityFields = $('.variable_width_facilityname');
    $('#ruler').text("SUGAR MILL DIAGNOSTIC IMAGING IMAGING, LLC");
    var fieldWidth = $('#ruler').outerWidth() + 5;
    for (var fac = 0; fac < allFacilityFields.length; fac++) {
        var currentFac = allFacilityFields[fac];
        $(currentFac).css("width", fieldWidth);
    }
    $('#ruler').text("");
};

var setWidthforExpFields = function () {
    if ($('.expected_charges_field').length > 0) {
        var exp = $('.expected_charges_field')[0];
        $('#ruler').text("EXPECTED CHARGES");
        var fieldWidth = $('#ruler').outerWidth() + 15;
        $(exp).css("width", fieldWidth);
        $('#ruler').text("");
    }
};



$('#mainBody').off('click', '.dependent-checkbox').on('click', '.dependent-checkbox', function () {
    var id;
    if ($(this).prop("checked")) {
        id = $(this).data('dependentId');
        $('#' + id).css('display', 'block');
    }
    else {
        id = $(this).data('dependentId');
        $('#' + id).css('display', 'none');
    }
});

var setAllFieldsWidth = function () {
    setWidthForSelects();
    setWidthforDateFields();
    setWidthforDateTimeFields();
    setWidthforProviderFields();
    setWidthforFacilityFields();
    setWidthforExpFields();
}