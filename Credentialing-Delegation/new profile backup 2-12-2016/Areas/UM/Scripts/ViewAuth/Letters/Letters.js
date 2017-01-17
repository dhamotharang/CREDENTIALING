$('#lettersTable').off('click', '.preview-Approval').on('click', '.preview-Approval', function () {

    TabManager.openSideModal('/UM/Letter/LetterPreview?ID=10', 'Approval Letter', 'cancel', '', '', '');
    return;
});
//$('#lettersTable').off('click', '.preview-Denial').on('click', '.preview-Denial', function () {
//    TabManager.openSideModal('~/Areas/UM/Views/Common/Letter/_DenialLetter.cshtml', 'Denial Letter', 'cancel', '', '', '');
//    return;
//});
//$('#lettersTable').off('click', '.preview-Nomnc').on('click', '.preview-Nomnc', function () {
//    TabManager.openSideModal('~/Areas/UM/Views/Common/Letter/_NOMNCLetter.cshtml', 'NOMNC Letter', 'cancel', '', '', '');
//    return;
//});
//$('#lettersTable').off('click', '.preview-Denc').on('click', '.preview-Denc', function () {
//    TabManager.openSideModal('~/Areas/UM/Views/Common/Letter/_DENCLetter.cshtml', 'DENC Letter', 'cancel', '', '', '');
//    return;
//});
