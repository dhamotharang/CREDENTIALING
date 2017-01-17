//$(function(){
//    $('.ActionButtons').off('click', '#View').on('click', '#View', function () {
//        alert();
//        TabManager.getDynamicContent('/CredAxis/Documents/ListOfDocuments', 'DocumentRepositoryMainViewPanel');
//    }).off('click', '#Reports').on('click', '#Reports', function () {
//        alert(
//        TabManager.getDynamicContent('/CredAxis/Documents/Reports', 'DocumentRepositoryMainViewPanel');
//    })
//});

function View(object){
    TabManager.getDynamicContent('/CredAxis/Documents/ListOfDocuments', 'DocumentRepositoryMainViewPanel');
    $('.docRepoActionButton').removeClass('active');
    $(object).addClass('active')
}

function Reports(object){
    TabManager.getDynamicContent('/CredAxis/Documents/Reports', 'DocumentRepositoryMainViewPanel');
    $('.docRepoActionButton').removeClass('active');
    $(object).addClass('active');
}

function PackageGeneration(object){
    TabManager.getDynamicContent('/CredAxis/Documents/PackageGeneration', 'DocumentRepositoryMainViewPanel');
    $('.docRepoActionButton').removeClass('active');
    $(object).addClass('active')
}