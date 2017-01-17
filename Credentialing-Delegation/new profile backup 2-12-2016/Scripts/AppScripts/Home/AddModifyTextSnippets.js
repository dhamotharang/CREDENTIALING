$('#addTextSnippetLi').off('click', '#addTextSnippetA').on('click', '#addTextSnippetA', function () {
    TabManager.openCenterModal('/Home/GetInsertTextList', 'INSERT TEXT', '', '');
});