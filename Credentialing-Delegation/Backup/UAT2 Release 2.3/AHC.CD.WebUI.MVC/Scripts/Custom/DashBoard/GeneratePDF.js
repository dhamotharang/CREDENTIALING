var doc = new jsPDF('p', 'pt', 'ledger');
var specialElementHandlers = {
    '#editor': function (element, renderer) {
        return true;
    }
};

margins = {
    top: 80,
    bottom: 60,
    left: 60,
    width: 700
};

function downloadTableInfo(elementId, title, elementType, documentType) {

    doc.fromHTML("<" + elementType + ">" + $('#' + elementId).html() + "</" + elementType + ">", margins.left, margins.top, {
        'width': margins.width, 'elementHandlers': specialElementHandlers
    },function (dispose) { doc.save('fileNameOfGeneretedPdf.pdf');}, margins);

    //doc.save(title + '.pdf');
}