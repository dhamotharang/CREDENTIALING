$('.losBody').off('click', '.add_New_Los').on('click', '.add_New_Los', function () {
    TabManager.openFloatingModal('~/Areas/UM/Views/ViewAuth/LOS/_AddNewLOS.cshtml', '~/Areas/UM/Views/ViewAuth/LOS/_ADD_LOS_Header.cshtml', '~/Areas/UM/Views/ViewAuth/LOS/_LOS_Footer.cshtml', '', '');
    return;
});

//$('.edit_los_body').off('click', '.editLos').on('click', '.editLos', function () {
//    TabManager.openSideModal('~/Areas/UM/Views/ViewAuth/LOS/_editLOS.cshtml', 'Edit LOS', 'both', '', '', '');
//    return;
//});

$('.edit_los_body').off('click', '.editLos').on('click', '.editLos', function () {
    //TabManager.openSideModal('~/Areas/UM/Views/ViewAuth/LOS/_editLOS.cshtml', 'Edit LOS', 'both', '', '', '');
    TabManager.openFloatingModal('~/Areas/UM/Views/ViewAuth/LOS/_editLOS.cshtml', '~/Areas/UM/Views/ViewAuth/LOS/_Edit_LOS_Header.cshtml', '~/Areas/UM/Views/ViewAuth/LOS/_LOS_Footer.cshtml', '', '');
    return;
});