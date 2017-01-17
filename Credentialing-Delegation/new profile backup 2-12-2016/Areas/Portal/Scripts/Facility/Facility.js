
$(document).ready(function () {

    //TODO: Review line 3 to 54 and remove if it is not necessary
    $('#fullBodyContainer').css('overflow', 'hidden'); //DISABLE ADDITIONAL SCROLL
    $('.tableBodyDivision').css('height', ($(window).height() - 162)); // SET TABLE HEIGHT ACCORDING TO SCREEN RESOLUTION


    //TODO: Review line below  for its significance and remove if it is not fulfilling any purpose
    $(".datePicker").datetimepicker({ format: 'MM/DD/YYYY' });
 
    // TOOLTIPS:
    $('[data-toggle="tooltip"]').tooltip();

    // ADJUST TABLE HEIGHT ON RESIZING SCREEN RESOLUTION:
    $(window).resize(function () {
        $('.tableBodyDivision').css('height', ($(window).height() - 202));
    });
 
    function appendTabContent(data, tabcontentid) {
        $('#' + tabcontentid).html(data);
    }
            //Hide Add Facility Button for Other Facility Master Data
            $('body').off('click', '.faq,facT,.fpt,.fsq', function () { })
           .on('click', '.faq,facT,.fpt,.fsq', function () {
              $(".addExportBtn").hide();
           });


          $('body').off('click', '.fac', function () { })
            .on('click', '.fac', function () {
                $(".addExportBtn").show();
            });


    $('.innerTabsArea li a').on('click', function () {
        var url_path = $(this).attr('data-tab-path');
        $.ajax({
            type: 'GET',
            url: url_path,
            success: function (data) {
                appendTabContent(data, "queueTable");
            }
        })
    })
 
    $('body').off('click', '.addFacility', function () {})
 .on('click', '.addFacility', function () {
     $(".addExportBtn").hide();
     $(".CancelAddHospital").fadeIn(400);

     $.ajax({
         url: "/Facility/AddEditField",
         success: function (data) {
             $("#queueTable").replaceWith("<div class='tableBodyDivision col-lg-12' id='queueTable'>" + data + "</div>")
         }
     });
     $(".facilityField").fadeIn(400);
     $(".tableBodyDivision").fadeOut(400);
 });


    $('body').off('click', '.CancelAddFacility', function () {})
  .on('click', '.CancelAddFacility', function () {
    $(".addExportBtn").show();
    $(".CancelAddHospital").fadeOut(400);

    $.ajax({
        url: "/Facility/GetFacilityList",
        success: function (data) {
            $("#queueTable").replaceWith("<div class='tableBodyDivision col-lg-12' id='queueTable'>" + data + "</div>")
        }
    });
    $(".tableBodyDivision").fadeIn(400);
    $(".facilityField").fadeOut(400);

});



    //Filter Rows 
    //Showing and Hiding of filter rows
    $("#queueBody").off('click', '.filterButton').on('click', '.filterButton', function () {
        $(".filterRows").toggleClass("displayNone");
        $("#filterQueueArrow").toggleClass("fa-caret-up fa-caret-down");
    });


    $(".queueTabs")
        .off("click", ".ipaQueueMenu")
        .on("click", ".ipaQueueMenu", function () {
            $(".ipaQueueMenu").removeClass("active");
            $(this).addClass("active");
            //var tabId = $(this).attr("id");
            //$("#" + tabId).find('a.innerTabsLinks').addClass("topminus3");
            var queueType = $(this).attr('data-queue-type');
            var queuTab = $(this).attr('data-queue-tab');
            var data = { QueueType: queueType, QueueTab: queuTab }
        });
});