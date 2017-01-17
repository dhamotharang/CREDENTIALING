$('#ViewQueues').off('click', '.perform_next').on('click', '.perform_next', function () {
    //$("#queueBody").on('click', '.perform_next', function (e) {
    event.preventDefault();
    var MethodName = '';
    var id = $(this).parent().attr('id');
    
    //$(this).parent().addClass("current");
    //$(this).parent().siblings().removeClass("current");
    var url = $(this).data("partial");
    var index = $(this).closest('tr').index() + 1;
    $(this).hide();
    
    //TabManager.getDynamicContent(url, "ViewProcess", MethodName, "");
    $.ajax({
        type: 'GET',
        url: '/CredAxis/ReturnPartial?path='+url,
        success: function (data) {
            $('#queueBody > tr:nth-child(' + index + ')').after(data);
            $('.next_step').show();
            $('#VerfiyResult').show();
            $('.innerContainerArea, #ViewQueues').animate({ scrollTop: '+=300px' }, 800);
              $('[data-toggle="tooltip"]').tooltip();
        }
    });

});


$('#ViewQueues').off('click', '.next_step').on('click', '.next_step', function () {
    //$("#queueBody").on('click', '.perform_next', function (e) {
    event.preventDefault();
    var MethodName = '';
    var id = $(this).parent().attr('id');

    //$(this).parent().addClass("current");
    //$(this).parent().siblings().removeClass("current");
    var url = $(this).data("partial");
    var index = $(this).closest('tr').parent().parent().closest('tr').index();
    $(this).closest('tr').parent().parent().closest('tr').remove();

    //TabManager.getDynamicContent(url, "ViewProcess", MethodName, "");
    $.ajax({
        type: 'GET',
        url: '/CredAxis/ReturnPartial?path=' + url,
        success: function (data) {
            $('#queueBody > tr:nth-child(' + index + ')').after(data);
            $('.next_step').show();
            $('.pre-scrollable, #ViewQueues').animate({ scrollTop: '+=300px' }, 800);
            $('#VerfiyResult').show();
            $('[data-toggle="tooltip"]').tooltip();

        }
    });

});

$('#ViewQueues').off('click', '.close_Process').on('click', '.close_Process', function () {
    //$("#queueBody").on('click', '.perform_next', function (e) {
    event.preventDefault();
    var MethodName = '';
    var id = $(this).parent().attr('id');

    //$(this).parent().addClass("current");
    //$(this).parent().siblings().removeClass("current");
  
    var index = $(this).closest('tr').parent().parent().closest('tr').index();
    $(this).closest('tr').parent().parent().closest('tr').remove();
    $('#queueBody > tr:nth-child(' + index + ')').remove();
    //TabManager.getDynamicContent(url, "ViewProcess", MethodName, "");
 

});

$('#ViewQueues').off('click', '.close_x').on('click', '.close_x', function () {
    //$("#queueBody").on('click', '.perform_next', function (e) {
    event.preventDefault();
    var index = $(this).parent().parent().index();
    $(this).parent().parent().remove();
    $('#queueBody > tr:nth-child('+index+')').children('td').find('.perform_next').show()

});

$('#ViewQueues').off('click', '.package_Generate').on('click', '.package_Generate', function () {
    $('.Package_here').show();
    $('.package_Generate').hide();
    $('.next_step').show();
});

$('#perform').click(function(){
$('#VerfiyResult').show();
});