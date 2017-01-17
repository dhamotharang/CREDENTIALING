$(document).ready(function () {

    // Method on-click of User Queue Tabs---
    $('.UserQueue_Side_Tabs').on('click', function (e) {
        var id = e.currentTarget.id;
        GetQueueData("UserQueueTabs", id);
        
    })
    // Method on-click of Category Tabs---
    $('.SystemIdentified_Side_Tabs').on('click', function (e) {
        var id = e.currentTarget.id;
        GetQueueData("SystemIdentifiedQueueTabs",id);
    })

    // Method to get the data and also highlighting the current tab-----
    function GetQueueData(info,ID){   
        // ---remove active class from all the tabs----
        $(".UserQueue_Side_Tabs").removeClass("active");
        $(".SystemIdentified_Side_Tabs").removeClass("active");
        //--- add class active for the clicked tab----
        $('#' + ID).addClass("active");
        $.ajax({
            type: 'GET',
            url: '/CM/Queue/GetUserQueueData?QueueType='+info,
            success: function (data) {
                // ----Make the Table body Empty----
                $("#queueBody").empty();
                // ---Push the html------
                $("#queueBody").html(data);
            }
        });
   }

   //  Sorting Function
    function sortTable(f, n) {
        var rows = $('#UserQueueTable  tr').get();
        rows.sort(function (a, b) {
            var A = getVal(a);
            var B = getVal(b);
            if (A > B) {
                return -1 * f;
            }
            if (A < B) {
                return 1 * f;
            }
            return 0;
        });
        function getVal(elm) {
            var v = $(elm).children('td').eq(n).text().toUpperCase();
            if ($.isNumeric(v)) {
                v = parseInt(v, 10);
            }
            return v;
        }
        $.each(rows, function (index, row) {
            $('#UserQueueTable').append(row);
        });
        prev_n = n;
    }

    // SORTING EVENTS:
    var f_all = 1;
    var n = 0;
    var prev_n = 0;
    $(".User-Queue-Sort").click(function () {
        f_all *= -1;
        n = $(this).parent().parent().prevAll().length; // CURRENT COLUMN POSITION
        if (prev_n != n) {
            f_all = -1;
            $(".User-Queue-Sort").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white'); //SORT ICONS
        }
        sortTable(f_all, n);
        if (n == prev_n) {
            if ($(this).hasClass('fa-sort')) {
                $(this).removeClass('fa-sort').addClass('fa-sort-asc').css('color', 'black');
            }
            else if ($(this).hasClass('fa-sort-asc')) {
                $(this).removeClass('fa-sort-asc').addClass('fa-sort-desc').css('color', 'black');
            }
            else {
                $(this).removeClass('fa-sort-desc').addClass('fa-sort-asc').css('color', 'black');
            }
        }
    });
});