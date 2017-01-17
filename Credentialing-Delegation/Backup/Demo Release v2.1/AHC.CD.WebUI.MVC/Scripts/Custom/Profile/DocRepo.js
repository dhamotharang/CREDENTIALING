

profileApp.controller('docRepo', function ($scope) {

   
    $('#file-dropzone').dropzone({
        url: "/Profile/Upload",
        maxFilesize: 100,
        paramName: "uploadfile",
        maxThumbnailFilesize: 5,
        init: function () {

            this.on('success', function (file, json) {
            });

            this.on('addedfile', function (file) {

            });

            this.on('drop', function (file) {
                alert('file');
            });
        }
    });



});