$(document).ready(function () {

    /* File-Icon With Cancel on Top */
    singlefile = '<div class="col-md-2 portfolio-item"' +
                                '<span><i class="fa fa-file-pdf-o fa-2x"></i>' + '<span class="badge bg-red closedoc" onclick="closeThisDoc(this)"><i class="fa fa-times"></i></span>' + '</span>' +
                           '</div>';

    /* Function for Multiple File Uploading in Auth-Review Tab */
    uploadedcount = 0;
    uploadedfiles = [];
    var docPrevious = null;
    $('.fileUpload').on("change", function (e) {
        var category = this.id;
        var uploadedcounts = 0;
        var doctypeObj = { type: "", files: [] };
        fileinputid = $(this).attr('id');        // id of the input for Respective DocumentType
        doctypeObj.type = fileinputid;
        currentuploadinput = fileinputid;
        if (fileinputid != docPrevious) {
            uploadedcounts = parseInt($(this).parent().parent().children("div.DocsCount_Div").children("span").html());
            if (!isNaN(uploadedcounts) && (function (x) { return (x | 0) === x; })(parseFloat(uploadedcounts))) {
                uploadedcounts = uploadedcounts;
            } else {
                uploadedcounts = 0;
            }
        }
        else
            uploadedcounts = uploadedcounts;

        //$(this).parent().parent().children("Div.Docs_Div").prepend('<span class="arrow-left">' + '<i class="fa fa-chevron-left fa-2x"></i>' + ' </span>' + '<span class="arrow-right">' + '<i class="fa fa-chevron-right fa-2x"></i> ' + '</span>');
        $(this).parent().parent().children("Div.Docs_Div").children("div.offer-pg-cont").addClass("offer-pg-cont-current");
        $(this).parent().parent().children("Div.Docs_Div").children("div.offer-pg-cont").addClass("offer-pg-cont-current").html("");
        var docsdivid = $(this).parent().parent().children("Div.Docs_Div").children("div.offer-pg-cont").attr('id');   // Id of the Div over which file html's are created

        $.each(e.target.files, function (index, value) {
            e.target.files[index].Category = fileinputid;
            uploadedfiles.push(e.target.files[index]);
        })

        var doctemplate = "";
        /* Removing Duplicacy in Files Uploaded Starts*/
        var arrResult = {};
        for (i = 0, n = uploadedfiles.length; i < n; i++) {
            var item = uploadedfiles[i];
            arrResult[item.name + "-" + item.Category] = item;
        }
        var i = 0;
        var nonDuplicatedArray = [];
        for (var item in arrResult) {
            nonDuplicatedArray[i++] = arrResult[item];
        }
        uploadedfiles = nonDuplicatedArray;
        /* Removing Duplicacy in Files Uploaded Ends*/

        var filesdata = [];
        var lbl = "";
        $.each(uploadedfiles, function (index, value) {
            if (uploadedfiles[index].Category == category) {
                filesdata.push(uploadedfiles[index]);
                //doctemplate = doctemplate + singlefile;             //Creating HTML Template for files
                lbl = lbl + '<span>' + uploadedfiles[index].name + '</span>' + '<br>';
            }
        })
        $('.offer-pg-cont-current').html(lbl);
        $(this).parent().parent().children("div.DocsCount_Div").children("span").html(filesdata.length);
        //$('#' + docsdivid).html('<div class="offer-pg">' + doctemplate + '</div>');   //Appending Created HTML Template to Respective Div
        uploadedcounts = e.target.files.length + uploadedcounts;
        docPrevious = fileinputid;
    });

    /* Hover on Files */
    $('.offer-pg-cont').tooltip({ title: titleSetter, trigger: "hover", html: true, placement: "top" });

    /* Right-Scroll and Left-Scroll after Uploading Multiple Files */
    currentuploadinput = "";
    $(".Docs_Div").on("click", ".arrow-left", function () {
        var id = $(this).parent().parent().children('div.Docs_Div').children('div.offer-pg-cont-current').attr('id');
        var currentHorizontalPosition = $("#" + id).scrollLeft();
        if (currentuploadinput == $(this).parent().parent().children('div.ReviewUpload').find("input").attr('id'))
            // $("#" + id).animate({ scrollLeft: "-=" + 100 });
            $("#" + id).animate({ 'scrollLeft': currentHorizontalPosition - 100 }, 'swing');
        else
            //$("#" + id).animate({ scrollLeft: "-=" + 0 });
            $("#" + id).animate({ 'scrollLeft': currentHorizontalPosition - 100 }, 'swing');
    });

    $(".Docs_Div").on("click", ".arrow-right", function () {
        var id = $(this).parent().parent().children('div.Docs_Div').children('div.offer-pg-cont-current').attr('id');
        var currentHorizontalPosition = $("#" + id).scrollLeft();
        if (currentuploadinput == $(this).parent().parent().children('div.ReviewUpload').find("input").attr('id'))
            // $("#" + id).animate({ scrollLeft: "+=" + 100 });
            $("#" + id).animate({ 'scrollLeft': currentHorizontalPosition + 100 }, 'swing');
        else
            //$("#" + id).animate({ scrollLeft: "+=" + 0 });
            $("#" + id).animate({ 'scrollLeft': currentHorizontalPosition + 100 }, 'swing');
    });

})

/* To Close the Respective File in Uploaded Files */
function closeThisDoc(item) {
    var filesdata = [];
    var cat = $(item).parents('.doctyperow').children().last().children().first()[0].id;
    $.each(uploadedfiles, function (index, value) {
        if (uploadedfiles[index].Category == cat) {
            filesdata.push(uploadedfiles[index]);
            filesdata[filesdata.length - 1].parentIndex = index;
        }
    })
    uploadedfiles.splice(filesdata[$(event.currentTarget).parent().parent().index()].parentIndex, 1);
    filesdata.splice($(event.currentTarget).parent().parent().index().parentIndex, 1);
    $(item).parents('.doctyperow').children('.DocsCount_Div').find('span').html(filesdata.length);
    if (filesdata.length == 0) {
        $(item).parents('.doctyperow').children(".Docs_Div").children('.arrow-right').remove();
        $(item).parents('.doctyperow').children(".Docs_Div").children('.arrow-left').remove();
        $(item).parents('.doctyperow').children(".Docs_Div").children(".offer-pg-cont").removeClass("offer-pg-cont-current").html('<span class="nodocsattached">No Documents Attached</span>');
    }
    else {
        $(item).parent().remove();
    }
    titleSetter(cat);
}

/* Tooltip Title Setter Function to Display the File Names Uploaded */
function titleSetter(idve) {
    if (!idve) var id = $(this).parent().parent().children().find("input").attr('id');
    else var id = idve;
    var label = "";
    $.each(uploadedfiles, function (index, value) {
        if (uploadedfiles[index].Category == id) {
            label = label + '<span><i class="fa fa-file-pdf-o"></i></span><label class="text-uppercase wrapWord filenamelabel">' + uploadedfiles[index].name + ' </label><br/>';
        }
    })
    if (label == "") {
        $(".tooltip.fade.top.in").attr("style", "display:none !important");
    }
    return label;
}