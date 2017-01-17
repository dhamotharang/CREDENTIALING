
    /** @description Getting 837 TableList.
    */

    $.ajax({
        type: 'GET',
        url: '/Billing/FileManagement/Get835TableList?Received=true&Generated=true',
        success: function (data) {
            $('#835fileTable').html(data);
        }
    });





