$('.editButton').click(function(){
    TabManager.getDynamicContent('/Encounters/EncounterCRUD/EditEncounter','fullBodyContainer');
});

$('.cancelButton').click(function () {
    TabManager.closeCurrentlyActiveTab();
    TabManager.navigateToTab({
        tabAction: 'Encounter List',
        tabTitle: 'Encounters',
        tabPath: '/Encounters/EncounterList/ShowAllEncounters',
        tabContainer: 'fullBodyContainer'
    });
});