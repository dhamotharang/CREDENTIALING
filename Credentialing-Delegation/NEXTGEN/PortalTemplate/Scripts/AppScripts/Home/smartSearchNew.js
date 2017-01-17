$(document).ready(function () {

    $('#searchbox').keyup(function () {        
        $searchInputWidth = $("#searchbox").width();
        $('.smartResult').css('width', $searchInputWidth);      
        $('.part1Of12').css('width', $searchInputWidth / 12);
        $('.part4Of12').css('width', 4 * ($searchInputWidth / 12));
        $('.part7Of12').css('width', 7 * ($searchInputWidth / 12) - 30);
        $searchVal = $('#searchbox').val();
        if ($searchVal === null || $searchVal === "" || $searchVal === undefined) {
            $('.smartResult').hide();
        } else {
            firstChar = $searchVal.charAt(0);
            if (firstChar === '#') {
                $('.provider').hide();
                $('.memberId').show();
                $('.memberName').hide();
            } else if (firstChar === '@') {
                $('.provider').hide();
                $('.memberId').hide();
                $('.memberName').show();
            } else if (firstChar === ':') {
                $('.provider').show();
                $('.memberId').hide();
                $('.memberName').hide();
            }
            else {
                $('.provider').show();
                $('.memberId').show();
                $('.memberName').show();
            }
            $('.smartResult').show();
        }


    })
})