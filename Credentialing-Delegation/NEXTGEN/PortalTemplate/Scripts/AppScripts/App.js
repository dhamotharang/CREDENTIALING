var chart = null;

/** ******  left menu  *********************** **/
$(function () {
    function slideMenu(ele) {
        if (ele.is('.active')) {
            ele.removeClass('active active-sm');
            $('ul:first', ele).slideUp();
        } else {
            // prevent closing menu if we are on child menu
            if (!ele.parent().is('.child_menu')) {
                $('#sidebar-menu').find('li').removeClass('active active-sm');
                $('#sidebar-menu').find('li ul').slideUp();
            }
            else {
                if (ele.parent().children().hasClass('active')) {
                    ele.parent().find('li').removeClass('active active-sm');
                    ele.parent().find('li ul').slideUp();
                }
            }
            ele.addClass('active');
            $('ul:first', ele).slideDown();
        }
    };

    $('#sidebar-menu').on('click', '.menu_click_expanded', function (ev) {
        var $li = $(this).parent();
        slideMenu($li);
    });

    $('#sidebar-menu').on('click', '.menu_click_collapsed', function (ev) {
        var $li = $(this);
        slideMenu($li);
    });

    $('#menu_toggle').click(function () {
        //To close open menus Item while collapsing SideMenu
        var percen = (($("span:contains('Utilization Management')").width() / $(".pace-done").width()) * 100) + 1;
        var val = percen * $(".pace-done").width() * .01;
        if ($('.side-menu').find('li.active').length != 0) {
            $li = $('.side-menu').find('li.active')[0];
            //$li.className = 0;
            $('ul:first', $li).slideUp(500);
        }
        if ($("#menuBar").hasClass("sideBarCollapse")) {
            $("#menuBar").css("width", val + 90);
            $("#mainBody").css("width", $(".pace-done").width() - val - 92);
            $("#menuBar").removeClass("sideBarCollapse");
            $("#menuBar").addClass("sideBarExpand");
            $("#mainBody").removeClass("mainBodyExpand");
            $("#mainBody").addClass("mainBodyCollapse");
            $(".child_menu").removeClass().addClass("nav child_menu");
            $(".expand_click").removeClass().addClass("expand_click menu_click_expanded");
            $(".collapsed_click").removeClass().addClass("collapsed_click");
            $('.profile').show("slow");
        }
        else {
            $("#menuBar").removeAttr("style");
            $("#mainBody").removeAttr("style");
            var className = ($("#SidebarBackground")[0].href.split("/")[$("#SidebarBackground")[0].href.split("/").length - 1]).split(".")[0];
            $(".child_menu").addClass(className);
            $("#menuBar").addClass("sideBarCollapse");
            $("#menuBar").removeClass("sideBarExpand");
            $("#mainBody").addClass("mainBodyExpand");
            $("#mainBody").removeClass("mainBodyCollapse");
            $(".expand_click").removeClass().addClass("expand_click");
            $(".collapsed_click").removeClass().addClass("collapsed_click menu_click_collapsed");
            $('.profile').hide("slow");
        }
        setTimeout(function () {
            $(".runningbar").css("width", $("#mainBody").outerWidth());
            $("#sidebar-menu").css("width", $("#menuBar").outerWidth());
            if ($("#sidebar-menu").outerWidth() < 100) {
                $("#menu-container").css("height", $(window).height() - 45);
                //$(".work_menu").css("padding-right", 19);
            }
            else {
                $("#menu-container").css("height", $(window).height() - 132);
            }
            resetscreen();
        }, 500)

    });
});

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
/* /App and Work Menu Toggle*/

/** Outer Tabs**/
$(function () {
    function hideFloat() {
        $('.float_menu_toggle').css('display', 'none');
        $('.work_menu_overlay').hide();
        $('.work_menu').hide();
        $('.runningheadercontent').hide();
        $('body').css('overflow', 'hidden')
    };
    function showFloat() {
        $('.work_menu_overlay').show();
        $('.work_menu').show();
        $('.runningheadercontent').show();
        $('body').css('overflow', 'visible')
        $('.float_menu_toggle').click();
    };

    $('.outerTabsLinks').click(function (ev) {
        if ($(this).hasClass('floatMenu')) {
            showFloat();
        }
        else {
            hideFloat();
        }
        $('.outerTabs').removeClass('active');
        var tr = $(ev)[0].currentTarget.href.split("/");
        $('#' + tr[tr.length - 1]).addClass('active');
    });

    $('.outertabclose').click(function (ev) {
        var toRemove = $(this).parent()[0].childNodes[1].href.split("/")[$(this).parent()[0].childNodes[1].href.split("/").length - 1];
        $(this).parent().hide();
        $('.outerTabs').removeClass('active');
        var tabCon = $(this).parent().prev().children().closest('a')[0].href.split("/")[$(this).parent().prev().children().closest('a')[0].href.split("/").length - 1];
        if (!$(this).parent().next()) {
            if ($(this).parent().hasClass('active')) {
                $(this).parent().removeClass('active');
                $(this).parent().prev().addClass('active');
            }
            if ($(this).parent().prev()) {
                if ($(this).parent().prev().children()[0].className.search('floatMenu') > -1) {
                    showFloat();
                }
                else {
                    hideFloat();
                }
            }
        }
        $('#' + tabCon).addClass('active');
    });


    //$('.outerTabsLinks').click(function (ev) {
    //    $('.outerTabs').removeClass('active');
    //    var tr = $(ev)[0].currentTarget.href.split("/");
    //    $('#' + tr[tr.length - 1]).addClass('active');
    //});

    //$('.outertabclose').click(function (ev) {
    //    var toRemove = $(this).parent()[0].childNodes[1].href.split("/")[$(this).parent()[0].childNodes[1].href.split("/").length - 1];
    //    $(this).parent().hide();
    //    $('.outerTabs').removeClass('active');
    //    var tabCon = $(this).parent().prev().children().closest('a')[0].href.split("/")[$(this).parent().prev().children().closest('a')[0].href.split("/").length - 1];
    //    if (!$(this).parent().next()) {
    //        if ($(this).parent().hasClass('active')) {
    //            $(this).parent().removeClass('active');
    //            $(this).parent().prev().addClass('active');
    //        }
    //        if ($(this).parent().prev()) {
    //            if ($(this).parent().prev().children()[0].className.search('floatMenu') > -1) {
    //                showFloat();
    //            }
    //            else {
    //                hideFloat();
    //            }
    //        }
    //    }
    //    $('#' + tabCon).addClass('active');
    //});
});
/** /Outer Tabs**/

/* Sidebar Menu active class */
$(function () {
    var url = window.location;
    $('#sidebar-menu a[href="' + url + '"]').parent('li').addClass('current-page');
    $('#sidebar-menu a').filter(function () {
        return this.href == url;
    }).parent('li').addClass('current-page').parent('ul').slideDown().parent().addClass('active');

    $("ul.memberinnermenu li a").click(function (e) {
        if ($(this).parents().hasClass('sideBarExpand')) {
            $(this).parents("ul.memberinnermenu").children("li").children("a").removeClass("active");
            $(this).addClass("active");
        }
    });
    $("ul.memberinnermenu li").click(function (e) {
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
    setSideMenu();
    setTimeout(function () {
        $(".right_col").css("min-height", $(window).height());
        //$(".fullBodyContainer").css("min-height", $(window).height() - 150);

        $("#sidebar-menu").css("width", $("#menuBar").outerWidth());
        $("#menu-container").css("height", $(window).height() - 132);
        $(".runningbar").css("width", $("#mainBody").outerWidth());
        if ($('#menuBar').hasClass('sideBarCollapse')) {
            if ($("#sidebar-menu").outerWidth() < 100) {
                $("#menu-container").css("height", $(window).height());
                $("#menu-container").css("width", $("#sidebar-menu").outerWidth() + $("#sidebar-menu").outerWidth() * 2.32);
                $("#menu-container").css("padding-right", 0);
                $('.sideBarCollapse li a i').css("padding-left", $('.expand_click').width() * 0.2);
            }
            else {
                $("#menu-container").css("width", $("#sidebar-menu").outerWidth() + $("#sidebar-menu").outerWidth() * 2.1);
                $("#menu-container").css("padding-right", $("#menu-container").innerHeight() * 0.32);
                $(".work_menu").css("padding-right", 0);
            }
        }
        else {
            if ($("#sidebar-menu").outerWidth() < 100) {
                $("#menu-container").css("height", $(window).height());
                //$(".work_menu").css("padding-right", 19);
            }
            else {
                $("#menu-container").css("width", $("#sidebar-menu").outerWidth());
                $("#menu-container").css("padding-right", 0);
                $(".work_menu").css("padding-right", 0);
            }
        }


        if ($("#mainBody").height() <= $('#sidebar-menu').height()) {
            $("#mainBody").css("height", $('#sidebar-menu').height());
            //$("#mainBody").css("height", $('#sidebar-menu').height()+100);
        }
        else {
            $("#mainBody").css("height", 'auto');
        }

        var percen = (($("span:contains('Utilization Management')").width() / $(".main_container").width()) * 100) + 1;
        var val = percen * $(".main_container").width() * 0.013;
        if (!$("#menuBar").hasClass("sideBarCollapse")) {
            $("#menuBar").css("width", val + 60);
            $("#mainBody").css("width", $(".main_container").width() - val - 63);
  
            setTimeout(function () {
                $("#sidebar-menu").css("width", $(window).width() - $("#mainBody").outerWidth());
                $("#menu-container").css("width", $(window).width() - $("#mainBody").outerWidth());
                $(".runningbar").css("width", $("#mainBody").width() + 20);
            }, 500);
        }
        else {
            $("#menuBar").removeAttr("style");
            $("#mainBody").removeAttr("style");
        }

    }, 500)


}

$(function () {

    resetscreen();

    $(window).resize(function () {
        resetscreen();
    });

    function showFloatMenu() {
        $(".work_menu").removeAttr("style");
        $(".work_menu_overlay").show();
        $('.float_menu_toggle').hide();
    }

    function hideFloatMenu() {
        $(".work_menu").css("right", -($("#sidebar-menu").outerWidth() + 20)).css("background", "rgba(0,0,0,0.5)");
        $(".work_menu_overlay").hide();
        $('.float_menu_toggle').show();
    }

    $("#sidebarColors").mouseover(function () {
        hideFloatMenu();
    });
    $("#sidebarColors").mouseleave(function () {
        showFloatMenu();
    });
    $(".work_menu_overlay").click(function () {
        hideFloatMenu();
    })
    $(".float_menu_toggle").click(function () {
        showFloatMenu();
    })


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
$('.collapse-link').click(function () {
    var x_panel = $(this).closest('div.x_panel');
    var button = $(this).find('i');
    var content = x_panel.find('div.x_content');
    content.slideToggle(200);
    (x_panel.hasClass('fixed_height_390') ? x_panel.toggleClass('').toggleClass('fixed_height_390') : '');
    (x_panel.hasClass('fixed_height_320') ? x_panel.toggleClass('').toggleClass('fixed_height_320') : '');
    button.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
    setTimeout(function () {
        x_panel.resize();
    }, 50);
});
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

    $(".floatMemberColorPallet").click(function (ev) {
        $('.memberinnermenu').removeClass($('.memberinnermenu').attr('class').split(" ")[3]);
        $('.runningbar').removeClass($('.runningbar').attr('class').split('runningbar ')[1]).addClass(' ' + ev.target.classList[1]);
        $('.memberinnermenu').addClass(ev.target.classList[1]);
        //$('.float_menu_toggle').removeClass().addClass('float_menu_toggle');
        //$('.float_menu_toggle').addClass(ev.target.classList[1]);
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

    //$('.color_list').click(function (ev) {
    //    event.preventDefault();
    //});

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
        if ($(e.target).parents().hasClass('color_list')) event.stopImmediatePropagation();
    });
});
/** /Gradient Color for SideBar**/

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


        $("#DataContainer .active.in").hide().show("slide", { direction: "left" }, 1000);


        if (pagename == 'claimListContainer') {
            setTimeout(function () {

                totalCount = [190, 110, 300, 340, 600, 300, 300, 190, 110, 300, 340, 279];

                var monthNames = ["January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"];
                chart = c3.generate({
                    bindto: '#M_claim_report',
                    data: {
                        x: 'x',
                        columns: [
                            ['x', "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
                            ['CAP', 130, 80, 200, 250, 400, 150, 250, 130, 80, 200, 250, 234],
                            ['FFS', 60, 30, 100, 90, 200, 150, 50, 60, 30, 100, 90, 45],
                        ],

                        type: 'bar',
                        groups: [
                            ['CAP', 'FFS']
                        ],
                        labels: {
                            format: function (value, ratio, id, d) {
                                value = ratio === 'CAP' ? '($' + (totalCount[id] * 3).toLocaleString() + '  #' + totalCount[id].toLocaleString() + ')' : null;
                                return value;
                            }
                        }

                    },
                    bar: {
                        width: {
                            ratio: 0.25 // this makes bar width 50% of length between ticks
                        }
                        // or
                        //width: 100 // this makes bar width 100px
                    },
                    axis: {
                        x: {
                            type: 'category',
                            categories: monthNames
                            //type: 'timeseries',
                            //tick: {
                            //    format: function (x) { return x.getDate() + "th" + monthNames[x.getMonth()]; }
                            //    //format: '%Y' // format string is also available for timeseries data
                            //}
                        },
                        y: {
                            label: 'Count'
                        }
                    },

                    grid: {

                        y: {
                            lines: [{ value: 0 }]
                        }
                    }, color: {
                        pattern: ['#ed9619', '#3C8DBC']
                    },
                    tooltip: {
                        format: {
                            //title: function (d) { return 'Data ' + d; },
                            value: function (value, ratio, id) {
                                value = '$' + (value * 3).toLocaleString() + ', #' + value.toLocaleString();
                                return value;
                            }
                            //            value: d3.format(',') // apply this format to both y and y2
                        }
                    }
                });
            }, 1000);


            $('select[name="selectyear_Claims"]').change(function () {
                if ($(this).val() !== "" || $(this).val() !== null) {
                    switch ($(this).val()) {
                        case "1":
                            totalCount = [190, 110, 300, 340, 600, 300, 300, 190, 110, 300, 340, 279];
                            chart.load({
                                columns: [
                              ['x', "January", "February", "March", "April", "May", "June",
                  "July", "August", "September", "October", "November", "December"],
                             ['CAP', 130, 80, 200, 250, 400, 150, 250, 130, 80, 200, 250, 234],
                              ['FFS', 60, 30, 100, 90, 200, 150, 50, 60, 30, 100, 90, 45],
                                ],
                                labels: {
                                    format: function (value, ratio, id, d) {
                                        value = ratio === 'CAP' ? '($' + (totalCount[id] * 3).toLocaleString() + '  #' + totalCount[id].toLocaleString() + ')' : null;
                                        return value;
                                    }
                                }
                            });
                            break;
                        case "2":
                            totalCount = [110, 160, 320, 750, 700, 450, 900, 360, 260, 600, 400, 770];
                            chart.load({
                                columns: [
                             ['x', "January", "February", "March", "April", "May", "June",
                 "July", "August", "September", "October", "November", "December"],
                            ['CAP', 100, 80, 120, 500, 300, 300, 650, 230, 180, 400, 150, 650],
                             ['FFS', 10, 80, 200, 250, 400, 150, 250, 130, 80, 200, 250, 120],
                                ],
                                labels: {
                                    format: function (value, ratio, id, d) {
                                        value = ratio === 'CAP' ? '($' + (totalCount[id] * 3).toLocaleString() + '  #' + totalCount[id].toLocaleString() + ')' : null;
                                        return value;
                                    }
                                }
                            });
                            break;
                        case "3":
                            totalCount = [170, 110, 270, 340, 400, 280, 250, 220, 130, 400, 350, 120];
                            chart.load({
                                columns: [
                              ['x', "January", "February", "March", "April", "May", "June",
                  "July", "August", "September", "October", "November", "December"],
                             ['CAP', 120, 70, 180, 250, 300, 140, 230, 140, 90, 250, 250, 60],
                              ['FFS', 50, 40, 90, 90, 100, 140, 20, 80, 40, 150, 100, 60],
                                ],
                                labels: {
                                    format: function (value, ratio, id, d) {
                                        value = ratio === 'CAP' ? '($' + (totalCount[id] * 3).toLocaleString() + '  #' + totalCount[id].toLocaleString() + ')' : null;
                                        return value;
                                    }
                                }
                            });
                            break;
                    }
                }

            });

            $('select[name="selectbasic_Claims"]').change(function () {
                if ($(this).val() !== "" || $(this).val() !== null) {
                    switch ($(this).val()) {
                        case "1":
                            $('.ClaimsTable2016').show();
                            $('.ClaimsTable2015').hide();
                            $('.ClaimsTable2014').hide();
                            break;
                        case "2":
                            $('.ClaimsTable2016').hide();
                            $('.ClaimsTable2015').show();
                            $('.ClaimsTable2014').hide();
                            break;
                        case "3":
                            $('.ClaimsTable2016').hide();
                            $('.ClaimsTable2015').hide();
                            $('.ClaimsTable2014').show();
                            break;
                    }
                }
            })

        }


        //$(".flyingtab").empty().append($(e).parent().html()).css({ "left": $(e).parent().offset().left, "top": $(e).parent().offset().top }).show('fastest', function () {
        //    $(this).animate({ "left": $(".chrome-tab-current").offset().left, "top": $(".chrome-tab-current").offset().top }, 1000, function () {
        //        $(this).hide("explode", function () {



        //        });
        //    });
        //});
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

    if (pageTitle.toLowerCase() == 'create auth') {
        $('#Realestatelabel').replaceWith("<strong>INTAKE</strong>");
    }
    else if (pageTitle.toLowerCase() == 'auth history') {
        $('#Realestatelabel').replaceWith("<strong>INTAKE</strong>");
    }
    else if (pageTitle.toLowerCase() == 'view history') {
        $('#Realestatelabel').replaceWith("<strong>INTAKE</strong>");
    }

};
/** /Page Container Animation**/

/** USE PCP USE MBR buttons**/
$('#mainBody').on('click', '.options_label', function (ev) {
    if (ev.target.localName == "label") {
        if ($(this).hasClass('selected')) $(this).removeClass('selected');
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
//var requestView = function (partialUrl, target) {
//    $.ajax({
//        type: "POST",        
//        data: partialUrl,
//        url: "/Home/GetPartial",
//        success: function (result) {
//            $("#" + target).html(result);
//        }
//    });
//};

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

/* /Utility Functions*/
var setBreadCrum = function (id) {
    $('#refnum').hide();
    $('#reftitle').hide();

    var idz = id;
    if (typeof id == 'object') {
        idz = $(id).children()[1].innerHTML.toLowerCase();
        if (Object.keys(id).length === 0) return;
    }
    //$('#breadcrumbArea').hide('fast');
    //$('#breadcrumbArea').hide('slide', { direction: 'left' });
    $('#breadcrumbArea').html("");
    var create = '<a href="#" class="btn btn-default"><i class="fa fa-user fa-1x"></i>&nbsp;Barbara Joy</a><a href="#" class="btn btn-default">UM</a> <a href="#" class="btn btn-default">INTAKE</a><a href="#" class="btn btn-success">CREATE AUTHORIZATION - EVELINE HEWETT</a>';
    var queue = '<a href="#" class="btn btn-default"><i class="fa fa-user fa-1x"></i>&nbsp;Barbara Joy</a><a href="#" class="btn btn-default">UM</a><a href="#" class="btn btn-success">FACILITY QUEUE</a>';
    var history = '<a href="#" class="btn btn-default"><i class="fa fa-user fa-1x"></i>&nbsp;Barbara Joy</a><a href="#" class="btn btn-default">UM</a><a href="#" class="btn btn-default">EVELINE HEWETT</a><a href="#" class="btn btn-success">AUTH HISTORY</a>'
    var viewhistory = '<a href="#" class="btn btn-default"><i class="fa fa-user fa-1x"></i>&nbsp;Barbara Joy</a><a href="#" class="btn btn-default">UM</a><a href="#" class="btn btn-default">EVELINE HEWETT</a><a href="#" class="btn btn-success">VIEW HISTORY</a>'
    var claims = '<a href="#" class="btn btn-default"><i class="fa fa-user fa-1x"></i>&nbsp;Barbara Joy</a><a href="#" class="btn btn-default">CLAIMS</a><a href="#" class="btn btn-default">EVELINE HEWETT</a><a href="#" class="btn btn-success">CLAIMS HISTORY</a>';
    var viewauth = '<a href="#" class="btn btn-default"><i class="fa fa-user fa-1x"></i>&nbsp;Barbara Joy</a><a href="#" class="btn btn-default">UM</a><a href="#" class="btn btn-default">EVELINE HEWETT</a><a href="#" class="btn btn-success">REF# 1607050015</a>';
    switch (idz) {
        case 'create new auth':
            $('#breadcrumbArea').html(create);
            break;
        case 'queue':
            $('#breadcrumbArea').html(queue);
            break;
        case 'auth history':
            $('#breadcrumbArea').html(history);
            break;
        case 'view history':
            $('#breadcrumbArea').html(viewhistory);
            $('#refnum').show();
            $('#reftitle').show();
            break;
        case 'claims':
            $('#breadcrumbArea').html(claims);
            break;
        case 'member':
            $('.chrome-tab-current').click();
            break;
        case 'ref# 1607050015':
            $('#breadcrumbArea').html(viewauth);
            $('#refnum').show();
            $('#reftitle').show();
            break;

    }
    //$('#breadcrumbArea').show('fast');
    //$('#breadcrumbArea').show('slide', { direction: 'left' });

};

/*Methods for Notification Color Management*/
var mouseOutMap = function () {
    $('#previewArea').css('background-color', "#FFFFFF")
};

var mouseOverColor = function (colorcode) {
    $('#previewArea').css('background-color', colorcode);
};

var updateNotificationColor = function (colorcode, posA, posB) {
    //var TempSettings = Object.assign({}, UserSettings);
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
            break;
        case "sidebariconcolor":
            $('#sidebar-menu,#sidebar-menu span,.inner_menu_item').attr({ 'style': 'color:' + colorcode + ' !important;' });
            $('#sidebar-menu .fa').attr('style', 'color:' + colorcode);
            break;
        case "tableheader":
            if ($('#customTheadBackClass')) $('#customTheadBackClass').remove();
            var theadcolor = '<style id="customTheadBackClass">.custom-thead-back > thead{background-color: ' + colorcode + ' !important;}</style>';
            $('head').append(theadcolor);
            break;
        case "tableheaderfont":
            if ($('#customTheadFontClass')) $('#customTheadFontClass').remove();
            var theadfont = '<style id="customTheadFontClass">.custom-thead-font > thead > tr> th {color: ' + colorcode + ' !important;}.custom-thead-font .styled-input > label {color: ' + colorcode + ' !important;}</style>';
            $('head').append(theadfont);
            break;
        case "tablestripe":
            if ($('#theadClass')) $('#theadClass').remove();
            var spp = '<style id="theadClass">.custom-table-striped > tbody > tr:nth-child(2n-1) { background-color: ' + colorcode + '; transition: all .125s ease-in-out;  }</style>';
            $('head').append(spp);
            break;
        case "tablebodyfont":
            if ($('#customTbodyFontClass')) $('#customTbodyFontClass').remove();
            var tbodyfont = '<style id="customTbodyFontClass">.custom-tbody > tbody td {color: ' + colorcode + ' !important;}.custom-tbody > tbody td label {color: ' + colorcode + ' !important;}</style>';
            $('head').append(tbodyfont);
            break;
    }
    $('#html5colorpicker').val(colorcode);
};
/*Methods for Notification Color Management ENDS*/


String.prototype.trimToLength = function (m) {
    return (this.length > m)
      ? jQuery.trim(this).substring(0, m).split(" ")[0] + "..."
      : this;
};

var setSideMenu = function () {
  
};


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
        success: function (data, textStatus, XMLHttpRequest) {
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