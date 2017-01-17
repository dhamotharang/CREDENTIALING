var wrapper = document.getElementById("signature-pad"),
    clearButton = wrapper.querySelector("[data-action=clear]"),
    saveButton = wrapper.querySelector("[data-action=save]"),
    canvas = wrapper.querySelector("canvas"),
    signaturePad;

// Adjust canvas coordinate space taking into account pixel ratio,
// to make it look crisp on mobile devices.
// This also causes canvas to be cleared.
//function resizeCanvas() {
//    // When zoomed out to less than 100%, for some very strange reason,
//    // some browsers report devicePixelRatio as less than 1
//    // and only part of the canvas is cleared then.
//    var ratio = Math.max(window.devicePixelRatio || 1, 1);
//    canvas.width = canvas.offsetWidth * ratio;
//    canvas.height = canvas.offsetHeight * ratio;
//    canvas.getContext("2d").scale(ratio, ratio);
//}

//window.onresize = resizeCanvas;
//resizeCanvas();

signaturePad = new SignaturePad(canvas);

clearButton.addEventListener("click", function (event) {
    signaturePad.clear();
});

saveButton.addEventListener("click", function (event) {
    if (signaturePad.isEmpty() || $('#filename').val() == '') {
        alert("Please provide both name and signature.");
    } else {
        console.log(signaturePad.toDataURL());
        //window.open(signaturePad.toDataURL());
        var fileName = $('#filename').val();
        var Pic = signaturePad.toDataURL();
        Pic = Pic.replace(/^data:image\/(png|jpg);base64,/, "")

        // Sending the image data to Server
        $.ajax({
            type: 'POST',
            url: '/Home/saveSign',
            data: {dataSign:{ "imageData" : Pic , "fileName":fileName}},
            //contentType: 'application/json;',
            //dataType: 'json',
            success: function (data) {
				 signaturePad.clear();
                alert(data);
            }
        });
    }
});
