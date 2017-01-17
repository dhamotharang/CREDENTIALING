$(document).ready(function () {
    /*when Clicked on the result It should navigate to search result*/
    $('.info-result').click(function () {
        $('.search-result-overview').hide();
        $('#serch-default-message').hide();
        $('#search-results').show();
        $('.search-word').html($('#searchbox').val());
        SResultOpen();
    })
    /* on esc event hide the data */
    $(window).bind('keyup', function (event) {
        // esc event
        if (event.keyCode === 27) {
            $('.search-result-overview').hide();
        }
        // enter event
        if (event.keyCode === 13) {
            if ($('#searchbox').val() != '') {
                $('.search-result-overview').hide();
            }
        }
    });
    /*Drop down smart search*/
    $('#searchbox').keyup(function () {
        $val = $('#searchbox').val();
        if ($val != '') {
            $('.search-result-overview').show(100);//css("display", "block");
            valLenght = $val.length;
            if (valLenght == 1) {
                $('.um-searchResult').html("50");
                $('.claims-searchResult').html("10");
            } else if (valLenght > 1 && valLenght < 3) {
                $('.um-searchResult').html("29");
                $('.claims-searchResult').html("2");
            } else {
                $('.um-searchResult').html("5");
                $('.claims-searchResult').html("1");
            }
        } else {
            $('.search-result-overview').css("display", "none");
        }
    })
})