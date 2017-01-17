    $('body').off('click', '.OpenProfileBuilder').on('click', '.OpenProfileBuilder', function () {

        TabManager.openSideModal('~/Areas/CredAxis/Views/ProviderProfile/OtherTabs/Summary/_ProfileBuilder.cshtml', 'PROFILE BUILDER', 'cancel');
    });
    
   