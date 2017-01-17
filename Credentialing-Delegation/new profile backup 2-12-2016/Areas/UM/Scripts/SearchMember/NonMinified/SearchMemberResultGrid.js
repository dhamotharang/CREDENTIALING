$(function () {
    $('#tableBodyWrapper').css('height', $(window).height() - 327);
    $("#resultTableHeader .filer-search").keyup(function () {
        var n = $(this).parent().parent().prevAll().length;
        var data = this.value.toUpperCase().split(" ");
        var allRows = $("#searchResultGrid").find("tr");
        if (this.value == "") {
            allRows.show();
            $('#resultCount').empty().append(getCountOfResultOnFilter(allRows));
            return;
        }
        allRows.hide();
        var filteredRows = allRows.filter(function (i, v) {
            for (var d = 0; d < data.length; ++d) {
                if ($(this).children('td').eq(n).text().toUpperCase().indexOf(data[d]) > -1) {
                    return true;
                }
            }
            return false;
        });
        filteredRows.show();
        $('#resultCount').empty().append(getCountOfResultOnFilter(filteredRows));
    });
    var f_all = 1;
    var n = 0;
    var prev_n = 0;
    $("#resultTableHeader .fa").click(function () {
        f_all *= -1;
        n = $(this).parent().parent().prevAll().length; // CURRENT COLUMN POSITION
        if (prev_n != n) {
            f_all = -1;
            $("#resultTableHeader .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white'); //SORT ICONS
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

    function sortTable(f, n) {
        var rows = $('#searchResultGrid tr').get();
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
            $('#searchResultGrid').append(row);
        });
        prev_n = n;
    }

  
});

function getCountOfResultOnFilter(elementArray) {
    var count = 0;
    $.each(elementArray, function (i, v) {
        count++;
    });
    return count;
}
