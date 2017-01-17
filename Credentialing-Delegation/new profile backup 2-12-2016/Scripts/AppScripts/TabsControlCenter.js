/**********************************************************************/
/*|                 File: TabsControlCenter.js                       |*/
/*|                 Author: Adarsh Nanwani                           |*/
/*|                 Creation Date: August 5th, 2016                  |*/
/**********************************************************************/
$(document).ready(function () {

    var TabsManager = function () {
        /* REQUIRED VARIABLES*/
        var TabData = [];
        var ViewsCache = [];
        var ActiveTab = {};
        var HeaderScripts = "";
        var HeaderLinks = "";

        var $footer = $('#footer');
        /* REQUIRED VARIABLES*/



        /* TAB CONTENT MANAGEMENT*/
        var getDefaultSettings = function () {
            return {
                "AutoFlush": true,
                "FloatMenu": false,
                "IsSubTab": false,
                "DefaultSubTab": "",
                "DefaultParentTab": "",
                "AllowMultiple": true,
                "HasSubTabs": false,
                "MoveRight": false
            };
        }

        var getActionSettings = function (ele) {
            var settings = getDefaultSettings();
            if (ele.data("tabAction")) {
                settings.Action = ele.data("tabAction");
            }
            if (ele.data("tabTitle")) {
                settings.Title = ele.data("tabTitle");
            }
            if (ele.data("tabPath")) {
                settings.Path = ele.data("tabPath");
            }
            if (ele.data("tabFloatMenuPath")) {
                settings.FloatMenuPath = ele.data("tabFloatMenuPath");
            }
            if (ele.data("tabContainer")) {
                settings.Container = ele.data("tabContainer");
            }
            if (ele.data("tabFloatmenu") != null) {
                ((ele.data("tabFloatmenu") == "true") || (ele.data("tabFloatmenu") == true)) ? settings.FloatMenu = true : settings.FloatMenu = false;
            }
            if (ele.data("tabAutoflush") != null) {
                ((ele.data("tabAutoflush") == "true") || (ele.data("tabAutoflush") == true)) ? settings.AutoFlush = true : settings.AutoFlush = false;
            }
            if (ele.data("tabIssubtab") != null) {
                ((ele.data("tabIssubtab") == "true") || (ele.data("tabIssubtab") == true)) ? settings.IsSubTab = true : settings.IsSubTab = false;
            }
            if (ele.data("tabHassubtabs") != null) {
                ((ele.data("tabHassubtabs") == "true") || (ele.data("tabHassubtabs") == true)) ? settings.HasSubTabs = true : settings.HasSubTabs = false;
            }
            if (ele.data("tabDefaultsubtab")) {
                settings.DefaultSubTab = ele.data("tabDefaultsubtab");
            }
            if (ele.data("tabDataPath")) {
                settings.DataPath = ele.data("tabDataPath");
            }
            if (ele.data("tabMoveright") != null) {
                ((ele.data("tabMoveright") == "true") || (ele.data("tabMoveright") == true)) ? settings.MoveRight = true : settings.MoveRight = false;
            }
            if (ele.data("tabUsertype")) {
                settings.UserType = ele.data("tabUsertype");
            }
            if ((ele.data("tabUsertype") == 'Member') && (ele.data("tabUserid") == "")) {
                settings.UserId = $('#MemberID').text();
            }
            else if ((ele.data("tabUsertype") == 'Provider') && (ele.data("tabUserid") == "")) {
                settings.UserId = $('#ProviderID').text();
            }
            else {
                if (ele.data("tabUserid")) settings.UserId = ele.data("tabUserid");
            }
            if (ele.data("tabDefaultparenttab") && (ele.data("tabDefaultparenttab") != "")) {
                settings.DefaultParentTab = ele.data("tabDefaultparenttab");
                settings.ParentTabContainer = ele.data("tabParentcontainer");

            }
            if (ele.data("tabParenttabtitle")) {
                if (ele.data("tabParenttabtitle").search('.active') > -1) {
                    settings.ParentTabTitle = $(ele.data("tabParenttabtitle")).text();
                }
                else {
                    settings.ParentTabTitle = ele.data("tabParenttabtitle");
                }
            }
            if (ele.data("tabMemberdata") && (ele.data("tabMemberdata") != "")) {
                settings.MemberData = ele.data("tabMemberdata");
            }
            if (ele.data("tabParenttabpath") && (ele.data("tabParenttabpath") != "")) {
                settings.ParentTabPath = ele.data("tabParenttabpath");
            }
            if (ele.data("tabParenttabrenderingarea") && (ele.data("tabParenttabrenderingarea") != "")) {
                settings.RenderingArea = ele.data("tabParenttabrenderingarea");
            }
            return settings;
        };

        var getRedirectActionSettings = function (ob) {
            var settings = getDefaultSettings();
            if (ob.tabAction) settings.Action = ob.tabAction;
            if (ob.tabTitle) settings.Title = ob.tabTitle;
            if (ob.tabPath) settings.Path = ob.tabPath;
            if (ob.tabContainer) settings.Container = ob.tabContainer;
            if (ob.tabFloatMenu != null) { ((ob.tabFloatMenu == "true") || (ob.tabFloatMenu == true)) ? settings.FloatMenu = true : settings.FloatMenu = false; }
            if (ob.tabAutoFlush != null) { ((ob.tabAutoFlush == "true") || (ob.tabAutoFlush == true)) ? settings.AutoFlush = true : settings.AutoFlush = false; }
            if (ob.tabIsSubTab != null) { ((ob.tabIsSubTab == "true") || (ob.tabIsSubTab == true)) ? settings.IsSubTab = true : settings.IsSubTab = false; }
            if (ob.tabHasSubTabs != null) { ((ob.tabHasSubTabs == "true") || (ob.tabHasSubTabs == true)) ? settings.HasSubTabs = true : settings.HasSubTabs = false; }
            if (ob.tabDefaultsubtab) settings.DefaultSubTab = ob.DefaultSubTab;
            if (ob.tabUserId) settings.UserId = ele.tabUserId;
            if (ob.tabFloatMenuPath) settings.FloatMenuPath = ob.tabFloatMenuPath;
            if (ob.tabUserType) settings.UserType = ob.tabUserType;
            if (ob.tabDataPath) settings.DataPath = ob.tabDataPath;
            if (ob.tabDataMoveRight) settings.MoveRight = ob.tabDataMoveRight;
            if (ob.tabDefaultParentTab && (ob.tabDefaultParentTab != "")) {
                settings.DefaultParentTab = ob.tabDefaultParentTab;
                settings.ParentTabContainer = ob.tabParentTabContainer;
                settings.ParentTabTitle = ob.tabParentTabTitle;
            }
            if (ob.tabMemberdata && (ob.tabMemberdata != "")) {
                settings.Memberata = ob.tabMemberdata;
            }
            if (ob.tabParentTabPath && (ob.tabParentTabPath != "")) {
                settings.ParentTabPath = ob.tabParentTabPath;
            }
            if (ob.tabParentTabRenderingArea && (ob.tabParentTabRenderingArea != "")) {
                settings.RenderingArea = ob.tabParentTabRenderingArea;
            }
            return settings;
        };

        var getParentActionSettings = function (tabData) {
            if (tabData.UserId) memData = getUserData(tabData.UserId);
            else memData = {};
            var parentTabData = {
                "Action": tabData.DefaultParentTab,
                "Title": tabData.ParentTabTitle,
                "Container": tabData.ParentTabContainer,
                "IsSubTab": false,
                "FloatMenu": tabData.FloatMenu,
                "FloatMenuPath": tabData.FloatMenuPath,
                "UserId": tabData.UserId,
                "UserType": tabData.UserType,
                "RenderingArea": tabData.RenderingArea,
                "ParentTabPath": tabData.ParentTabPath,
                "HasSubTabs": true,
                "MoveRight": false,
                "MemberData": memData
            };
            return parentTabData;
        }

        $('body').off("click", ".tab-navigation").on("click", ".tab-navigation", function (e) {
            getTabContent(e);
        });

        var getTabContent = function (e, tabData) {
            checkForViewsCacheUpdate();
            var tempSubTab = {};
            var SubTabs = [];
            var parentTabId = "";
            var Id = "";
            var action = "";
            var Title = "";
            var activeCheck = "";
            if (e != null) {
                if (typeof e === "string") { // Request originated from OUTER TABS
                    var tempActionSettings = tabData;
                    action = tempActionSettings.Action;
                }
                else { // Request originated from SIDEBAR
                    if (e == true) var tempActionSettings = getRedirectActionSettings(tabData);
                    else var tempActionSettings = getActionSettings($(e.currentTarget));
                    if (tempActionSettings.Action == null || tempActionSettings.Action == "") return;
                    action = tempActionSettings.Action;
                }
                var TabIDTitle = generateTabIdAndTitle(tempActionSettings, tempActionSettings.UserId); //Generate Unique ID for the requested TAB
                Id = TabIDTitle.Id;
                Title = TabIDTitle.Title;
                activeCheck = checkIfAlreadyActive(tempActionSettings, TabIDTitle, (typeof e != "string"));
                //if (Title) tempActionSettings.Title = Title;   MIGHT NEED IT NEED TO TEST
                //if (Id) tempActionSettings.Id = Id;
                if (activeCheck) return;

                if (TabIDTitle.TabStatus.exists) {
                    if (tempActionSettings.IsSubTab) getDataFromPreExistingTab(TabData[TabIDTitle.TabStatus.parentTabIndex].SubTabs[TabIDTitle.TabStatus.innerTabIndex].Id);
                    else getDataFromPreExistingTab(TabData[TabIDTitle.TabStatus.index].Id, true, Id);
                }

                hideAllContainers();
                setFloatMenuVisibility(tempActionSettings) // Set visibility for float Menu

                var tempTabData = tempActionSettings;
                tempTabData.SubTabs = SubTabs;
                tempTabData.Id = Id;
                tempTabData.IsActive = true;

                if (tempTabData.IsSubTab) {
                    var parentTabData = getParentActionSettings(tempTabData);
                    parentTabId = getTabContent("parent", parentTabData); //Generates the OUTER TAB and returns its "Id"
                    tempTabData.ParentTabId = parentTabId;
                }
                if (typeof tabData != 'undefined') makeSubTabsInActive(tempTabData.Id);

                setCurrentTabSettings(tempTabData, TabIDTitle);
                if (tempTabData.IsSubTab) $('#' + tempTabData.Container).html("");
                $('#' + tempTabData.Container).show();
                if (!tempTabData.IsSubTab) setupOuterTabs(tempTabData, tempTabData, Id);
                else setupInnerTabs(tempTabData, tempTabData, parentTabId);
                if (tempTabData.ParentTabPath && tempTabData.RenderingArea) {
                    showLoader(tempTabData.RenderingArea);
                    if (tempTabData.AutoFlush) {
                        getTabViewFromPartial(tempTabData.ParentTabPath, tempTabData.RenderingArea, tempTabData); //Gets the PARTIAL PAGE from backend and sets it to the CONTAINER area   
                    }
                    else {
                        var viewDuplicacyCheck = checkViewDuplicacy(tempTabData.ParentTabPath);
                        if (viewDuplicacyCheck.exists) getTabViewFromViewsCache(viewDuplicacyCheck.viewCacheIndex, tempTabData.RenderingArea, tempTabData);
                        else getTabViewFromPartial(tempTabData.ParentTabPath, tempTabData.RenderingArea, tempTabData); //Gets the PARTIAL PAGE from backend and sets it to the CONTAINER area   
                    }


                }
                if (tempTabData.Path) {
                    if (!tempTabData.HasSubTabs) { //For TABS with do not have any SUBTABS
                        TabManager.CurrentlyActiveTab.Id = tempTabData.Id;
                        TabManager.CurrentlyActiveTab.Path = tempTabData.Path;
                        TabManager.CurrentlyActiveTab.AutoFlush = tempTabData.AutoFlush;
                        TabManager.CurrentlyActiveTab.IsSubTab = tempTabData.IsSubTab;
                    }
                    showLoader(tempTabData.Container);
                    if (tempTabData.AutoFlush) {
                        getTabViewFromPartial(tempTabData.Path, tempTabData.Container, tempTabData); //Gets the PARTIAL PAGE from backend and sets it to the CONTAINER area  
                    }
                    else {
                        var viewDuplicacyCheck = checkViewDuplicacy(tempTabData.Path);
                        if (viewDuplicacyCheck.exists) getTabViewFromViewsCache(viewDuplicacyCheck.viewCacheIndex, tempTabData.Container, tempTabData);
                        else getTabViewFromPartial(tempTabData.Path, tempTabData.Container, tempTabData); //Gets the PARTIAL PAGE from backend and sets it to the CONTAINER area  
                    }
                }
                setBreadCrumb(tempTabData); //Set the breadcrumb for the current tab
                //if (tempActionSettings.DefaultSubTab)
                resetscreen();
                return tempTabData.Id;
            }
        };

        var checkIfAlreadyActive = function (tempActionSettings, TabIDTitle, click) {
            if (TabIDTitle.TabStatus.exists && click) {
                if ((tempActionSettings.IsSubTab && !TabData[TabIDTitle.TabStatus.parentTabIndex].IsActive) || (tempActionSettings.IsSubTab && TabData[TabIDTitle.TabStatus.parentTabIndex].IsActive && !TabData[TabIDTitle.TabStatus.parentTabIndex].SubTabs[TabIDTitle.TabStatus.innerTabIndex].IsActive)) {
                    getDataFromPreExistingTab(TabIDTitle.Id);
                }
                else if (!tempActionSettings.IsSubTab) {
                    getDataFromPreExistingTab(TabIDTitle.Id, true);
                }
                else {
                    //return true;
                }
            }
        };

        var getTabContentFromTabData = function (outerTabData, currentTabId) {
            checkForViewsCacheUpdate();
            var tempSubTab = [];
            var SubTabs = [];
            var parentTabId = "";
            var Id = "";
            var action = "";
            hideAllContainers();
            var tempActionSettings = outerTabData;
            action = outerTabData.Action;

            var Id = outerTabData.Id; //Generate Unique ID for the requested TAB
            setFloatMenuVisibility(tempActionSettings) // Set visibility for float Menu
            if (tempActionSettings.IsSubTab) {
                parentTabId = getTabContent(tempActionSettings.DefaultParentTab, userId, userType); //Generates the OUTER TAB and returns its "Id"    
            }
            else {
                if (tempActionSettings.FloatMenu) {
                    //var member = getUserData(Id);
                    //setUserData(member, 'member');
                }
            }
            if (tempActionSettings.IsSubTab) $('#' + tempActionSettings.Container).html("");
            $('#' + tempActionSettings.Container).show();
            if (tempActionSettings.HasSubTabs) {
                resetInnerTabs();
            }
            if (!tempActionSettings.IsSubTab) setupOuterTabs(tempActionSettings, outerTabData, Id);
            for (var i = 0; i < outerTabData.SubTabs.length; i++) {
                generateInnerTabs(outerTabData.SubTabs[i]);
                if (currentTabId) {
                    if (outerTabData.SubTabs[i].IsActive && (outerTabData.SubTabs[i].Id == currentTabId)) { //OPENING FROM A LINK
                        showLoader(outerTabData.SubTabs[i].Container);
                        if (outerTabData.SubTabs[i].AutoFlush) {
                            getTabViewFromPartial(outerTabData.SubTabs[i].Path, outerTabData.SubTabs[i].Container, outerTabData);
                        }
                        else {
                            var viewDuplicacyCheck = checkViewDuplicacy(outerTabData.SubTabs[i].Path);
                            if (viewDuplicacyCheck.exists) getTabViewFromViewsCache(viewDuplicacyCheck.viewCacheIndex, outerTabData.SubTabs[i].Container, outerTabData);
                            else getTabViewFromPartial(outerTabData.SubTabs[i].Path, outerTabData.SubTabs[i].Container, outerTabData);
                        }
                        setActiveFloatMenuItem(outerTabData.SubTabs[i].Action);
                    }
                }
                else {
                    if (outerTabData.SubTabs[i].IsActive) {  //ON CLICK OF CLOSE BUTTON
                        showLoader(outerTabData.SubTabs[i].Container);
                        if (outerTabData.SubTabs[i].IsSubTab) {
                            TabManager.CurrentlyActiveTab.Id = outerTabData.SubTabs[i].Id;
                            TabManager.CurrentlyActiveTab.Path = outerTabData.SubTabs[i].Path;
                            TabManager.CurrentlyActiveTab.AutoFlush = outerTabData.SubTabs[i].AutoFlush;
                            TabManager.CurrentlyActiveTab.IsSubTab = outerTabData.SubTabs[i].IsSubTab;
                        }
                        if (outerTabData.SubTabs[i].AutoFlush) {
                            getTabViewFromPartial(outerTabData.SubTabs[i].Path, outerTabData.SubTabs[i].Container, outerTabData);
                        }
                        else {
                            var viewDuplicacyCheck = checkViewDuplicacy(outerTabData.SubTabs[i].Path);
                            if (viewDuplicacyCheck.exists) getTabViewFromViewsCache(viewDuplicacyCheck.viewCacheIndex, outerTabData.SubTabs[i].Container, outerTabData);
                            else getTabViewFromPartial(outerTabData.SubTabs[i].Path, outerTabData.SubTabs[i].Container, outerTabData);
                        }
                        setActiveFloatMenuItem(outerTabData.SubTabs[i].Action);
                    }
                }
            }

            if (tempActionSettings.Path) {
                if (tempActionSettings.IsSubTab) {
                    TabManager.CurrentlyActiveTab.Id = tempActionSettings.Id;
                    TabManager.CurrentlyActiveTab.Path = tempActionSettings.Path;
                    TabManager.CurrentlyActiveTab.AutoFlush = tempActionSettings.AutoFlush;
                    TabManager.CurrentlyActiveTab.IsSubTab = tempActionSettings.IsSubTab;
                }
                showLoader(tempActionSettings.Container);
                if (tempActionSettings.AutoFlush) {
                    getTabViewFromPartial(tempActionSettings.Path, tempActionSettings.Container, tempActionSettings); //Gets the PARTIAL PAGE from backend and sets it to the CONTAINER area  
                }
                else {
                    var viewDuplicacyCheck = checkViewDuplicacy(tempActionSettings.Path);
                    if (viewDuplicacyCheck.exists) getTabViewFromViewsCache(viewDuplicacyCheck.viewCacheIndex, tempActionSettings.Container, tempActionSettings);
                    else getTabViewFromPartial(tempActionSettings.Path, tempActionSettings.Container, tempActionSettings); //Gets the PARTIAL PAGE from backend and sets it to the CONTAINER area   
                }
            }
            setCurrentTabSettings(tempActionSettings, outerTabData);
            setBreadCrumb(tempActionSettings); //Set the breadcrumb for the current tab
            resetscreen();
        }

        var generateInnerTabs = function (tempTabData) {
            if (tempTabData.IsActive) {
                var tab = '<li class="tabs innerTabs active"><a class="innerTabsLinks" role="tab" data-toggle="tab" aria-expanded="true" id="' + tempTabData.Id + '"><b>' + tempTabData.Title + '</b></a><span class="custombadge innertabclose innertabclose-red" data-parentid="' + tempTabData.Id + '"><i class="fa fa-times"></i></span> </li>';
            }
            else
                var tab = '<li class="tabs innerTabs"><a class="innerTabsLinks" role="tab" data-toggle="tab" aria-expanded="true" id="' + tempTabData.Id + '"><b>' + tempTabData.Title + '</b></a><span class="custombadge innertabclose innertabclose-red" data-parentid="' + tempTabData.Id + '"><i class="fa fa-times"></i></span> </li>';
            $('#innerTabsArea').append(tab);
        }

        var getDataFromPreExistingTab = function (TabId, type, currentTabId) {

            if (type) {
                var outerTabIndex = getOuterTabIndex(TabId);
                var outerTabData = TabData[outerTabIndex];
                if (outerTabData.SubTabs == 0) getTabContentFromTabData(outerTabData);
                else getTabContentFromTabData(outerTabData, currentTabId);
            }
            else {
                var tabData = getInnerTabIndex(TabId);
                var outerTabIndex = tabData.parentTabIndex;
                var innerTabIndex = tabData.innerTabIndex;
                InactiveAllInnerTabs(outerTabIndex);
                TabData[outerTabIndex].SubTabs[innerTabIndex].IsActive = true;
                var outerTabData = TabData[outerTabIndex];
                getTabContentFromTabData(outerTabData)
            }
        };

        var getDataOnTabClick = function (TabId, type) {
            hideAllTabPopovers();
            getDataFromPreExistingTab(TabId, type);
            setTimeout(function () { hideAllTabPopovers(); }, 500);
            if (!type) {
                //var member = getUserData(TabId);
                //if (member) setUserData(member, 'member');
            }
        };

        $('#outerTabsArea').on('click', '.outerTabsLinks', function (e) {
            if (!$(e.currentTarget).parent().hasClass("active")) {
                var TabId = $(e.currentTarget).attr("id");
                var type = $(e.currentTarget).data("tabtype");
                getDataOnTabClick(TabId, type);
            }
        });

        $('.tabs').on('click', '.outertabs', function (e) {
            if (!$(e.currentTarget).children('.outerTabsLinks').hasClass("active")) {
                var TabId = $(e.currentTarget).children('.outerTabsLinks').attr("id");
                var type = $(e.currentTarget).children('.outerTabsLinks').data("tabtype");
                getDataOnTabClick(TabId, type);
            }
        });

        $('#innerTabsArea').on('click', '.innerTabs', function (e) {
            if (!$(e.currentTarget).children('.innerTabsLinks').hasClass("active")) {
                var TabId = $(e.currentTarget).children('.innerTabsLinks').attr("id");
                getDataOnTabClick(TabId);
            }
        });

        $('#innerTabsArea').on('click', '.innerTabsLinks', function (e) {
            if (!$(e.currentTarget).parent().hasClass("active")) {
                var TabId = $(e.currentTarget).attr("id");
                getDataOnTabClick(TabId);
            }
        });

        var setCurrentTabSettings = function (settings, tabData) {
            ActiveTab.Id = tabData.Id;
            ActiveTab.Container = settings.Container;
            ActiveTab.AutoFlush = settings.AutoFlush;
            ActiveTab.IsSubTab = settings.IsSubTab;
            ActiveTab.SubTabs = settings.SubTabs;
            ActiveTab.Title = settings.Title;
            if (typeof tabData != 'undefined') {

            }
            if (settings.IsSubTab) {
                var indexData = getInnerTabIndex(settings.Id);
                if (typeof indexData != 'undefined') var parentIndex = indexData.parentTabIndex;
                if (typeof parentIndex != 'undefined') {
                    $('.outertabs').removeClass('active');
                    $('#' + TabData[parentIndex].Id).parent().addClass('active');
                }
            }
            else {
                $('.outertabs').removeClass('active');
                $('#' + settings.Id).parent().addClass('active');
            }
            //if (tabData.TabStatus.exists) {
            //    if (settings.IsSubTab) {
            //        ActiveTab.ParentTabId = tabData.TabStatus.parentTabId;
            //    }
            //}

        };

        var setupOuterTabs = function (tempActionSettings, tempTabData, Id) {
            if (TabData.length > 0) { //If more than one OUTER TABS are open
                var outerTabCheck = outerTabExists(tempTabData.Id); //Check if the OUTER TAB to be opened already exists
                if (outerTabCheck.exists) { //If requested OUTER TAB exists already
                    if (tempActionSettings.AutoFlush) { //If the OUTER TAB's Reload property is true

                    }
                    //setActiveOuterTab(outerTabCheck.index);
                }
                else { //If requested doesn't exist
                    TabData.push(tempTabData); //Push the CurrentAction Object to the array holding information of OPEN OUTER TABS
                    addOuterTab(tempTabData, TabData.length - 1, Id); //Add a new OUTER TAB
                }
            }
            else { //If no tabs are open
                TabData.push(tempTabData); //Push the CurrentAction Object to the array holding information of OPEN OUTER TABS
                addOuterTab(tempTabData, TabData.length - 1, Id); //Add a new OUTER TAB
            }
        };

        var setupInnerTabs = function (tempActionSettings, tempTabData, parentTabId) {
            var outerTabIndex = getOuterTabIndex(parentTabId);
            var outerTabData = TabData[outerTabIndex];
            var outerTabCheck = outerTabExists(outerTabData.Id);
            if ((tempActionSettings.Container == 'innerTabContainer' && !outerTabCheck.exists) || (outerTabData.SubTabs.length == 0)) {
                resetInnerTabs();
            }
            var innerTabCheck = innerTabExists(tempTabData.Id); //Check if the INNER TAB to be opened already exists
            if (innerTabCheck.exists) { //If requested INNER TAB exists already
                if (tempActionSettings.AutoFlush) { //If the INNER TAB's Reload property is true

                }
                setActiveInnerTab(innerTabCheck.innerTabIndex);
                setActiveFloatMenuItem(tempActionSettings.Action);
            }
            else { //If requested doesn't exist
                InactiveAllInnerTabs(outerTabIndex);
                addSubTabData(parentTabId, tempTabData); //Push the CurrentAction Object to the array holding information of OPEN OUTER TABS
                addInnerTab(tempTabData); //Add a new OUTER TAB
                setActiveFloatMenuItem(tempTabData.Action);
            }
        };


        function DisableEvents() {

            $('body').css({ 'pointer-events': 'none' });
        }

        function EnableEvents() {

            $('body').css({ 'pointer-events': 'all' });
        }


        var getTabViewFromPartial = function (partialUrl, target, setting) {

            DisableEvents();
            if (partialUrl.search('.cshtml') > -1) {
                if (setting.DataPath) {
                    var url = "/Home/GetPartial?partialURL=" + partialUrl + "&dataURL=" + setting.DataPath;
                }
                else {
                    var url = "/Home/GetPartial?partialURL=" + partialUrl;
                }
            }
            else {
                var url = partialUrl;
            }

            $.ajax({
                type: "POST",
                url: url,
                dataType: 'html',
                success: function (result) {
                    //flushContainerArea(); //Flush the currently loaded container area
                    var container = $("#" + target);
                    container.empty().html(result + '<div class="clearfix"></div>'); //+ $footerClone.html();
                    removeLoader(); //Remove the loading spinner
                    setContainerHeight(target);
                    addViewToCache(partialUrl, result);
                    checkAndPushInternalPLinksAndPScripts(container[0]);
                    setUserAttributes(setting);
                    EnableEvents();
                }
            });
        };

        var getTabViewFromViewsCache = function (index, target, setting) {
            //flushContainerArea();
            DisableEvents();
            var container = document.getElementById(target);
            container.innerHTML = ViewsCache[index].Data + '<div class="clearfix"></div>';
            //$('#' + target).html(ViewsCache[index].Data);
            //showLoader(target);
            //checkAndPushInternalPLinks(container);
            //checkAndPushInternalPScripts(container); //Parse the Node to find scripts and add them to head. If scripts already exist, then reload them.
            //$node = getPScriptsAndPLinks(container);
            checkAndPushInternalPLinksAndPScripts(container);
            removeLoader(); //Remove the loading spinner
            setContainerHeight(target);
            if (setting) setUserAttributes(setting);
            EnableEvents();
        };

        var setContainerHeight = function (id) {
            //$("#" + id).css("height", $('#mainBody').height() - 100);
        }

        var setUserAttributes = function (setting) {
            var userId = getMemberId();
            $(".member-actions").data("tabUserid", userId);
        };

        var setFloatMenuVisibility = function (setting) {
            if ((setting.FloatMenu == true) || (setting.FloatMenu == "true")) {
                setUserAttributes(setting);
                showFloatMenu();
                getDynamicContent(setting.FloatMenuPath, "FloatMenu");
            }
            else if ((setting.FloatMenu == false) || (setting.FloatMenu == "false")) hideFloatMenu();
        }

        var hideFloatMenu = function () {
            $('.float_menu_toggle').css('display', 'none');
            $('.work_menu_overlay').hide();
            $('.work_menu').hide();
            $('.runningheadercontent').hide();
            $('body').css('overflow', 'hidden')
            $('#backToMenu').hide();
            $('#backToMemberMenu').show();
            $('#backToMemberMenu').hide();
            $('#backToMenu').hide();
        };


        var showFloatMenu = function () {
            $('.work_menu_overlay').show();
            $('.work_menu').show();
            $('.runningheadercontent').show();
            $('body').css('overflow', 'visible')
            $('.float_menu_toggle').click();
            $('#backToMenu').show();
            $(".work_menu").removeAttr("style");
            $('#backToMemberMenu').hide();
        };

        var flushContainerArea = function () {
            $('.containerArea').html("");
        }

        var hideAllContainers = function () {
            $('.tab-template').hide();
        }

        var showLoader = function (id) {
            var $ob = $('#loadingSample');
            var $clon = $ob.clone().addClass("loadingArea").removeClass("hidden");
            $('#' + id).prepend($clon);
        };

        var removeLoader = function () {
            $('.loadingArea').fadeOut(function () {
                $(this).remove();
            });
        };


        var TabIDGenerator = function () {
            this.length = 8;
            this.timestamp = +new Date;
            var _getRandomInt = function (min, max) {
                return Math.floor(Math.random() * (max - min + 1)) + min;
            }
            this.generate = function () {
                var ts = this.timestamp.toString();
                var parts = ts.split("").reverse();
                var id = "";
                for (var i = 0; i < this.length; ++i) {
                    var index = _getRandomInt(0, parts.length - 1);
                    id += parts[index];
                }
                return id;
            }
        }
        /* /TAB CONTENT MANAGEMENT*/



        /* OUTER TABS MANAGEMENT*/
        var generateTabIdAndTitle = function (tempActionSettings, userId) {
            var ret = {};
            var tabStatus = {};
            if (tempActionSettings.IsSubTab) {
                if (userId) {
                    if (tempActionSettings.UserType == 'Member') {
                        if (userId == 'data-tab-usertype="Member"/') userId = $('#MemberID').text();
                        var member = getUserData(userId);
                        setUserData(member, 'member');
                        ret.Id = tempActionSettings.Title + member.Member.MemberMemberships[0].Membership.SubscriberID;
                    }
                    else if (tempActionSettings.UserType == 'Provider') {
                        if (userId == 'data-tab-usertype="Provider"/') userId = $('#ProviderID').text();
                        var provider = getUserData(userId, 'provider');
                        setUserData(provider, 'provider');
                        ret.Id = tempActionSettings.Title + provider.NPINumber;
                    }
                    ret.Id = ret.Id.replace(/\s/g, '');
                    ret.Id = ret.Id.replace(/\#/g, '');
                    ret.Title = tempActionSettings.Title;
                }
                else {
                    ret.Id = tempActionSettings.Title;
                    ret.Id = ret.Id.replace(/\s/g, '');
                    ret.Id = ret.Id.replace(/\#/g, '');
                    ret.Title = tempActionSettings.Title;
                }
                ret.TabStatus = innerTabExists(ret.Id);
            }
            else {
                if (userId) {
                    if (tempActionSettings.UserType == 'Member') {
                        if (userId == 'data-tab-usertype="Member"/') userId = $('#MemberID').text();
                        var member = getUserData(userId);
                        ret.Id = member.Member.MemberMemberships[0].Membership.SubscriberID;
                        ret.Title = member.Member.PersonalInformation.FirstName + ' ' + member.Member.PersonalInformation.LastName;
                    }
                    else if (tempActionSettings.UserType == 'Provider') {
                        if (userId == 'data-tab-usertype="Provider"/') userId = $('#ProviderID').text();
                        var provider = getUserData(userId, 'provider');
                        ret.Id = provider.NPINumber;
                        ret.Title = provider.FirstName + ' ' + provider.LastName;
                    }
                    ret.Id = ret.Id.replace(/\#/g, '');

                    //setUserData(member, 'member');
                }
                else {
                    ret.Id = tempActionSettings.Title;
                    ret.Id = ret.Id.replace(/\s/g, '');
                    ret.Id = ret.Id.replace(/\#/g, '');
                    ret.Title = tempActionSettings.Title;
                }
                ret.TabStatus = outerTabExists(ret.Id);
            }
            return ret;
        };

        var addOuterTab = function (tempTab, index, Id) {
            $('.outerTabsLinks').parent().removeClass("active");
            if (tempTab.MoveRight) {
                var tab = '<li class="outertabs tab-right active" ><a class="outerTabsLinks" role="tab" data-toggle="tab" aria-expanded="true" id="' + Id + '" data-tabType="true"><b>' + tempTab.Title + '</b></a><span class="custombadge outertabclose outertabclose-red" data-parentid="' + tempTab.Id + '"><i class="fa fa-times"></i></span><span class="hidden tabs-popover" data-parenttabid="' + tempTab.Id + '"></span> </li>';
                var children = $('#outerTabsArea').children();
                var index;
                var n = 0;
                for (var i = 0; i < children.length; i++) {
                    if ($(children[i]).hasClass('tab-right')) {
                        index = i;
                        n++;
                    }
                }
                if (n > 0) $(tab).insertAfter(children[index]);
                if (n == 0) {
                    for (var i = 0; i < children.length; i++) {
                        if (!$(children[i]).hasClass('tab-right')) {
                            index = i;
                            n++;
                        }
                    }
                    $(tab).insertAfter(children[index]);
                }
            }
            else {
                var tab = '<li class="outertabs active" ><a class="outerTabsLinks" role="tab" data-toggle="tab" aria-expanded="true" id="' + Id + '" data-tabType="true"><b>' + tempTab.Title + '</b></a><span class="custombadge outertabclose outertabclose-red" data-parentid="' + tempTab.Id + '"><i class="fa fa-times"></i></span><span class="hidden tabs-popover" data-parenttabid="' + tempTab.Id + '"></span> </li>';
                var index;
                var children = $('#outerTabsArea').children();
                if (children.length > 0) {
                    var n = 0;
                    for (var i = 0; i < children.length; i++) {
                        if ($(children[i]).hasClass('tab-right')) {
                            index = i;
                            n++;
                            break;
                        }
                    }
                    if (n == 0) $('#outerTabsArea').append(tab);
                    else $(tab).insertBefore(children[index]);
                }
                else {
                    $('#outerTabsArea').append(tab);
                }
            }
        };

        var outerTabExists = function (Id) { //Check if the OUTER TAB exists
            var ret = {};
            ret.exists = false;
            for (var i = 0; i < TabData.length; i++) {
                if (TabData[i].Id === Id) {
                    ret.exists = true;
                    ret.index = i;
                    return ret; //Returns "true" and index of OUTER TAB if it already exists
                }
            }
            return ret; //Returns "false" if OUTER TAB does not exist
        };

        var getOuterTabIndex = function (Id) { //Return the index of OUTER TAB in <ul> based on its unique id
            for (var i = 0; i < TabData.length; i++) {
                if (TabData[i].Id === Id) {
                    return i; //Returns the index "i" here
                }
            }
        };

        var setActiveOuterTab = function (index) {
            $('.outertabs').removeClass('active'); //Remove active class from all OUTER TABS
            $('.outertabs:nth-child(' + (index + 1) + ')').addClass("active"); //Make OUTER TAB with the sent index active
        };

        $('#outerTabsArea').on('click', '.outertabclose', function (e) {
            closeOuterTab($(e.currentTarget));
        });
        var closeOuterTab = function (ev) {
            if (TabData.length > 1) { //This method executes only when there is more than one tabs
                if (typeof ev == 'object') var Id = $(ev).data("parentid");
                else if (typeof ev == 'string') var Id = ev;
                var index = getOuterTabIndex(Id); //Get index of the tab to be closed. The index will be same for <li> in <ul> for DOM and tab in TabData array
                var $li = $('#' + Id).parent();
                $li.children().first().children().addClass("red"); //Adding animation to tab to be closed
                $li.addClass("animated slideOutUp"); //Adding animation to tab to be closed
                if (!$li.hasClass("active")) { //If tab to be closed is not an active Tab
                    removeOuterTab('', $li);
                }
                else {
                    if (index == 0) { //If tab to be closed is the first tab
                        removeOuterTab('first', $li);
                    }
                    else if (index > 0 && index < TabData.length - 1) { //If tab to be closed lies in between
                        removeOuterTab('inbetween', $li);
                    }
                    else { //If tab to be closed is the last tab
                        removeOuterTab('last', $li);
                    }
                }
                if (TabData[index].SubTabs.length > 0) resetInnerTabs();
                TabData.splice(index, 1); //Remove entry from TabData Array
            }
        };

        var removeOuterTab = function (val, $li) {
            setTimeout(function () {
                switch (val) {
                    case 'first':
                        $li.next().children().first().click();//[$a.parent().index() + 1]Simulate click on next child
                        $li.remove();
                        break;
                    case 'inbetween':
                        $li.next().children().first().click(); //Simulate click on next child
                        $li.remove();
                        break;
                    case 'last':
                        $li.prev().children().first().click(); //Simulate click on previous child
                        $li.remove();
                        break;
                    default:
                        $li.remove();
                        break;
                }
            }, 500);
        };

        var addSubTabData = function (parentTabId, tempTabData) {
            var index = getOuterTabIndex(parentTabId);
            TabData[index].SubTabs.push(tempTabData);
        };

        var makeSubTabsInActive = function (parentTabId) {
            var index = getOuterTabIndex(parentTabId);
            if (TabData[index]) {
                var SubTabs = TabData[index].SubTabs;
                for (var i = 0 ; i < SubTabs.length; i++) {
                    SubTabs[i].IsActive = false;
                }
            }
        };
        /* /OUTER TABS MANAGEMENT*/



        /* INNER TABS MANAGEMENT*/
        var addInnerTab = function (tempActionSettings) {
            $('.innerTabsLinks').parent().removeClass("active");
            var tab = '<li class="tabs innerTabs active"><a class="innerTabsLinks" role="tab" data-toggle="tab" aria-expanded="true" id="' + tempActionSettings.Id + '"><b>' + tempActionSettings.Title + '</b></a><span class="custombadge innertabclose innertabclose-red" data-parentid="' + tempActionSettings.Id + '"><i class="fa fa-times"></i></span> </li>';
            $('#innerTabsArea').append(tab);
        }

        var innerTabExists = function (Id) { //Check if the OUTER TAB exists
            var ret = {};
            ret.exists = false;
            for (var i = 0; i < TabData.length; i++) {
                if (TabData[i].SubTabs.length > 0) {
                    for (var j = 0; j < TabData[i].SubTabs.length; j++) {
                        if (TabData[i].SubTabs[j].Id === Id) {
                            ret.exists = true;
                            ret.parentTabIndex = i;
                            ret.innerTabIndex = j;
                            return ret; //Returns "true" and index of OUTER TAB if it already exists
                        }
                    }
                }
            }
            return ret; //Returns "false" if OUTER TAB does not exist
        };

        var getInnerTabIndex = function (Id) { //Return the index of INNER TAB in <ul> based on its unique id
            var data = {};
            for (var i = 0; i < TabData.length; i++) {
                if (TabData[i].SubTabs.length > 0) {
                    for (var j = 0; j < TabData[i].SubTabs.length; j++) {
                        if (TabData[i].SubTabs[j].Id === Id) {
                            data.parentTabIndex = i;
                            data.innerTabIndex = j;
                            return data; //Returns the index of Subtab and its Parent
                        }
                    }
                }
            }
        };

        var setActiveInnerTab = function (index) {
            $('.innerTabs').removeClass('active'); //Remove active class from all OUTER TABS
            $('.innerTabs:nth-child(' + (index + 1) + ')').addClass("active"); //Make OUTER TAB with the sent index active
        };

        var setActiveFloatMenuItem = function (action) {
            $(".member-actions").removeClass("active");
            $('a.member-actions[data-tab-action="' + action + '"]').addClass("active");
        };

        var resetInnerTabs = function () {
            $('#innerTabsArea').html("");
        };

        var InactiveAllInnerTabs = function (outerTabIndex) {
            if (TabData.length > 0) {
                if (TabData[outerTabIndex].SubTabs.length > 0) {
                    for (var i = 0; i < TabData[outerTabIndex].SubTabs.length; i++) {
                        TabData[outerTabIndex].SubTabs[i].IsActive = false;
                    }
                }
            }
        };

        $('#innerTabsArea').on('click', '.innertabclose', function (e) {
            closeInnerTab($(e.currentTarget));
        });
        var closeInnerTab = function (ev) {
            if (typeof ev == 'object') {
                var id = $(ev).data("parentid");
            }
            else {
                var id = ev;
            }
            var indexData = getInnerTabIndex(id);
            var parentIndex = indexData.parentTabIndex;
            if (TabData[parentIndex].SubTabs.length == 1) {
                closeOuterTab(TabData[parentIndex].Id);
                return;
            }
            var index = indexData.innerTabIndex; //Get index of the tab to be closed. The index will be same for <li> in <ul> for DOM and tab in TabData array
            var $li = $("#" + id).parent();
            $li.children().first().children().addClass("red"); //Adding animation to tab to be closed
            $li.addClass("animated slideOutUp"); //Adding animation to tab to be closed
            TabData[parentIndex].SubTabs.splice(index, 1); //Remove entry from TabData Array
            if (!$li.hasClass("active")) { //If tab to be closed is not an active Tab
                removeInnerTab("", $li);
            }
            else {
                if (index == 0) { //If tab to be closed is the first tab
                    removeInnerTab('first', $li, index);
                }
                else if ((index > 0) && (index < TabData[parentIndex].SubTabs.length - 1)) { //If tab to be closed lies in between
                    removeInnerTab('inbetween', $li, index);
                }
                else { //If tab to be closed is the last tab
                    removeInnerTab('last', $li, index);
                }
            }

        }

        var removeInnerTab = function (val, $li, index) {
            switch (val) {
                case 'first':
                    $li.next().children().first().click(); //Simulate click on next child
                    $li.remove();
                    break;
                case 'inbetween':
                    $li.next().children().first().click(); //Simulate click on next child
                    $li.remove();
                    break;
                case 'last':
                    $li.prev().children().first().click(); //Simulate click on previous child
                    $li.remove();
                    break;
                default:
                    $li.remove();
                    break;
            }
        };
        /* /INNER TABS MANAGEMENT*/



        /* SHOW AND MANAGE TABS ON HOVER*/
        $('#outerTabsArea').on('mouseover', '.outerTabsLinks', function (e) {
            //showAndManageInnerTabs(e);
        });
        var showAndManageInnerTabs = function (e) {
            hideAllTabPopovers();
            $('.popover_content').html("");
            if (!$(e.currentTarget).parent().hasClass("active")) {
                $popover = $(e.currentTarget).parent().children().last();
                var outerTabIndex = getOuterTabIndex($popover.data("parenttabid"));
                var outerTabData = TabData[outerTabIndex];
                if (outerTabData) {
                    if (outerTabData.SubTabs) {
                        if (outerTabData.SubTabs.length > 0) {
                            var content = '<ul class="popover_content">';
                            for (var st = 0; st < outerTabData.SubTabs.length; st++) {
                                content = content + '<li><a class="tabs-popover-link curson_pointer" data-parentTabId="' + outerTabData.SubTabs[st].Id + '">' + outerTabData.SubTabs[st].Title + '</a><span class="tab-close-from-hover" data-parentTabId="' + outerTabData.SubTabs[st].Id + '"><i class="fa fa-close"></i></span></li>'
                            }
                            content = content + '</ul>';
                            //$popover.html(content);
                            //$popover.removeClass("hidden");
                        }
                    }
                }
            }
        };

        $('#outerTabsArea').on('click', '.tabs-popover-link', function (e) {
            getDataOnTabClick($(e.currentTarget).data("parenttabid"));
        });

        $('#outerTabsArea').on('click', '.tab-close-from-hover', function (e) {
            closeTabFromHover($(e.currentTarget).data("parenttabid"));
        });

        $('#outerTabsArea').on('mouseout', '.tabs-popover', function () {
            hideAllTabPopovers();
        });
        $('body').on('mouseover', '#menuBar,.tab-template,#sidebar-menu,#menu-container,#work_menu,#breadCrumbArea', function () {
            hideAllTabPopovers();
        });

        var hideAllTabPopovers = function () {
            $('.tabs-popover').addClass("hidden");
        };
        var closeTabFromHover = function (id) {
            var indexes = getInnerTabIndex(id);
            if (TabData[indexes.parentTabIndex].SubTabs.length > 1) {
                TabData[indexes.parentTabIndex].SubTabs.splice(indexes.innerTabIndex, 1);
                $(".curson_pointer")[indexes.innerTabIndex].remove();
            }
            else {
                $(".tabs-popover").addClass("hidden");
                closeOuterTab(TabData[indexes.parentTabIndex].Id);
            }
        };

        $('#outerTabsArea').on('mouseover', '.tabs-popover', function (e) {
            showPopover(e);
        });
        var showPopover = function (e) {
            if (e) $(e.currentTarget).removeClass("hidden");
        };

        $('#outerTabsArea').on('mouseout', '.tabs-popover', function (e) {
            hidePopover(e);
        });
        var hidePopover = function (ev) {
            $(ev).addClass("hidden");
        };
        /* SHOW AND MANAGE TABS ON HOVER*/



        /* VIEWS MANAGEMENT*/
        var checkViewDuplicacy = function (Path) {
            var ret = {};
            if (ViewsCache.length > 0) {
                for (var i = 0; i < ViewsCache.length; i++) {
                    if (ViewsCache[i].Id == Path) {
                        ret.exists = true;
                        ret.viewCacheIndex = i;
                        return ret;
                    }
                }
                return false;
            }
            else {
                return false;
            }
        }
        var addViewToCache = function (Path, viewHtml) {
            ViewsCache.push({ "Id": Path, "Data": viewHtml });
        };
        var checkForViewsCacheUpdate = function () {
            //if (typeof TabManager.CurrentlyActiveTab.AutoFlush != 'undefined') {
            //    if (!TabManager.CurrentlyActiveTab.AutoFlush) {
            //        var $viewData = "";
            //        if (TabManager.CurrentlyActiveTab.IsSubTab) {
            //            if (!$('#' + TabManager.CurrentlyActiveTab.Id).parent().hasClass("active")) return;
            //            $viewData = $('#innerTabContainer');
            //        }
            //        else {
            //            $viewData = $('#fullBodyContainer');
            //            if (!$('#' + TabManager.CurrentlyActiveTab.Id).parent().hasClass("active")) return;
            //        }
            //        var $viewDataClone = $viewData.clone();
            //        for (var i = 0; i < $viewData.find('select').length; i++) {
            //            $($viewDataClone.find('select')[i]).val($viewData.find('select').eq(i).val())
            //        }
            //        for (var i = 0; i < $viewData.find('input[type="radio"]').length; i++) {
            //            $($viewDataClone.find('input[type="radio"]')[i]).val($viewData.find('input[type="radio"]').eq(i).val());
            //        }
            //        for (var i = 0; i < $viewData.find('input[type="checkbox"]').length; i++) {
            //            $($viewDataClone.find('input[type="checkbox"]')[i]).val($viewData.find('input[type="checkbox"]').eq(i).val());
            //        }
            //        for (var i = 0; i < $viewData.find('.variable_width_select').length; i++) {
            //            $($viewDataClone.find('.variable_width_select')[i]).val($viewData.find('.variable_width_select').eq(i).val());
            //        }
            //        //$viewData.find('select').each(function (i) {
            //        //    $viewDataClone.find('select').eq(i).val($(this).val())
            //        //});
            //        //$viewData.find('input[type="radio"]').each(function (i) {
            //        //    $viewDataClone.find('input[type="radio"]').eq(i).val($(this).val())
            //        //});
            //        //$viewData.find('input[type="checkbox"]').each(function (i) {
            //        //    $viewDataClone.find('input[type="checkbox"]').eq(i).val($(this).val())
            //        //});
            //        //$viewData.find('.variable_width_select').each(function (i) {
            //        //    $viewDataClone.find('.variable_width_select').eq(i).val($(this).val())
            //        //});

            //        var viewDuplicacyCheck = checkViewDuplicacy(TabManager.CurrentlyActiveTab.Path);
            //        ViewsCache[viewDuplicacyCheck.viewCacheIndex].Data = $viewDataClone.html();
            //    }
           // }
        }
        /* /VIEWS MANAGEMENT*/




        /* SCRIPTS AND CSS COMBINED MANAGEMENT*/
        var getAllScriptsAndLinksInHead = function () {
            for (var i = 0; i < $('head').children().length; i++) {
                if ($('head').children()[i].tagName === 'SCRIPT') {
                    HeaderScripts = HeaderScripts + $('head').children()[i].src.split("/")[$('head').children()[i].src.split("/").length - 1] + " ";
                }
                if ($('head').children()[i].tagName === 'LINK') {
                    HeaderLinks = HeaderLinks + $('head').children()[i].href.split("/")[$('head').children()[i].href.split("/").length - 1] + " ";
                }
            }
        };

        var getPScriptsAndPLinks = function (cont) {
            var $container = $("<div>", { id: "getter", "class": "hidden" });
            $container.append(cont);
            //$('body').append($container);
            var LinkScriptArray = $container.find('p-script,p-link');
            $('#getter').remove();
            var $return = $("<div>");
            for (var i = 0; i < LinkScriptArray.length; i++) {
                $return.append($(LinkScriptArray[i]));
            }
            return $return;
        };

        var checkAndPushInternalPLinksAndPScripts = function (node) {
            if (checkPScript(node) === true) { // Is "true" when the node is a "P-SCRIPT"
                addOrReloadScripts(node);
            }
            else if (checkPLink(node) === true) { // Is "true" when the node is a "P-LINK"
                addLinks(node);
            }
            else {
                var i = 0;
                var children = node.childNodes;
                while (i < children.length) {
                    checkAndPushInternalPLinksAndPScripts(children[i++]); //Recursive call for child nodes
                }
            }
            return node;
        };

        var checkPScript = function (node) {
            return node.tagName === 'P-SCRIPT'; //returns true if node is "P-SCRIPT" type
        };

        var addOrReloadScripts = function (node) {
            if ($(node).attr('filepath').search("http") > -1) {
                var scriptName = "http" + $(node).attr('filepath').split("=")[1];
            }
            else var scriptName = $(node).attr('filepath').split("/")[$(node).attr('filepath').split("/").length - 1]; //Find name of script from its complete path

            if (HeaderScripts.search(scriptName) < 0) { //If the script is not there in <head> section
                HeaderScripts = HeaderScripts + scriptName + " "; //Add the Script to "HeaderScripts" variable
                addScriptToHeadFromPScript(node); //Add the JS file to <head> Section
            }
            else { //If the script already exists in <head> section
                if ($(node).attr('filepath').search("http") < 0) {
                    reloadJsFileFromPScript(node); //Reload the a particular JS file in <head> section
                }

            }
        };

        var addScriptToHeadFromPScript = function (node) {
            var s = document.createElement('script'); //Creating <SCRIPT> element and setting it's properties
            s.type = 'text/javascript';
            s.src = $(node).attr("filepath"); //"filepath" attribute of custom-tag <p-script> becomes the "src" for this script
            if ($(node).attr("filepath").search("http" > -1)) s.defer = true;
            else s.async = true;
            var last_head_script = document.getElementById('head_script'); //Finding node for last script in <head> section
            last_head_script.parentNode.insertBefore(s, last_head_script); //Add the new <script> after the last script in <head> section
            //$("head").append('<script src="' + $(node).attr("filepath") + '"  type="text/javascript" defer"></script>')
        };

        var reloadJsFileFromPScript = function (node) {
            $('script[src="' + $(node).attr("filepath") + '"]').remove(); //Remove the said script file from <head> section
            addScriptToHeadFromPScript(node);  //Added JS file back to <head> section
        };

        var checkPLink = function (node) {
            return node.tagName === 'P-LINK'; //returns true if node is "P-LINK" type
        };

        var addLinks = function (node) {
            var linkName = $(node).attr('filepath').split("/")[$(node).attr('filepath').split("/").length - 1]; //Find name of script from its complete path
            if (HeaderLinks.search(linkName) < 0) { //If the Link is not there in <head> section
                HeaderLinks = HeaderLinks + linkName + " "; //Add the Link to "HeaderScripts" variable
                addLinkToHead(node); //Add the CSS file to <head> Section
            }
        };

        var addLinkToHead = function (node) {
            var s = document.createElement('link'); //Creating <SCRIPT> element and setting it's properties
            s.rel = 'stylesheet';
            s.href = $(node).attr("filepath"); //"filepath" attribute of custom-tag <p-script> becomes the "src" for this script
            var last_head_link = document.getElementById('head_link'); //Finding node for last script in <head> section
            last_head_link.parentNode.insertBefore(s, last_head_link); //Add the new <script> after the last script in <head> section
        };
        /* /SCRIPTS AND CSS COMBINED MANAGEMENT*/




        /* SCRIPTS FILES MANAGEMENT*/
        var getAllScriptsInHead = function () { //finds all the scripts present in the <head> section and add their names to HeaderScripts string variable
            for (var i = 0; i < $('head').children().length; i++) {
                if ($('head').children()[i].tagName === 'SCRIPT') {
                    HeaderScripts = HeaderScripts + $('head').children()[i].src.split("/")[$('head').children()[i].src.split("/").length - 1] + " ";
                }
            }
        };

        var checkAndPushInternalPScripts = function (node) { //Recursive function which finds all the P-Scripts in a DOM node
            if (checkPScript(node) === true) { // Is "true" when the node is a "P-SCRIPT"
                addOrReloadScripts(node);
            }
            else {
                var i = 0;
                var children = node.childNodes;
                while (i < children.length) {
                    checkAndPushInternalPScripts(children[i++]); //Recursive call for child nodes
                }
            }
            return node;
        };
        /* /SCRIPTS FILES MANAGEMENT*/



        /* CSS FILESMANAGEMENT*/
        var getAllLinksInHead = function () { //finds all the scripts present in the <head> section and add their names to HeaderScripts string variable
            for (var i = 0; i < $('head').children().length; i++) {
                if ($('head').children()[i].tagName === 'LINK') {
                    HeaderLinks = HeaderLinks + $('head').children()[i].href.split("/")[$('head').children()[i].href.split("/").length - 1] + " ";
                }
            }
        };

        var checkAndPushInternalPLinks = function (node) { //Recursive function which finds all the P-Links in a DOM node
            if (checkPLink(node) === true) { // Is "true" when the node is a "P-LINK"
                addLinks(node);
            }
            else {
                var i = 0;
                var children = node.childNodes;
                while (i < children.length) {
                    checkAndPushInternalPLinks(children[i++]); //Recursive call for child nodes
                }
            }
            return node;
        };
        /* /CSS FILESMANAGEMENT*/



        /* BREADCRUMB MANAGEMENT*/
        var setBreadCrumb = function (settings) {
            if (settings.IsSubTab) {
                var title = settings.Title;
                var tabData = getInnerTabIndex(settings.Id);
                var outerTabIndex = tabData.parentTabIndex;
                var innerTabIndex = tabData.innerTabIndex;
                $('#activeBreadcrumb').text(TabData[outerTabIndex].Title + " - " + title);
                document.title = TabData[outerTabIndex].Title + " - " + title + " | AHC";
            }
            else if (settings.SubTabs.length > 0) {
                for (var i = 0; i < settings.SubTabs.length; i++) {
                    if (settings.SubTabs[i].IsActive == true) {
                        var title = settings.SubTabs[i].Title;
                        break;
                    }
                }
                $('#activeBreadcrumb').text(settings.Title + " - " + title);
                document.title = settings.Title + " - " + title + " | AHC";
            }
            else {
                $('#activeBreadcrumb').text(settings.Title);
                document.title = settings.Title + " | AHC";
            }

        };
        /* /BREADCRUMB MANAGEMENT*/



        /* SERVICE STUB*/
        var getUserData = function (userID, userType) {
            if (userType) {
                if (userType.toLowerCase() == 'provider') {
                    if (ProviderServiceData.length > 0)
                        for (var m = 0; m < ProviderServiceData.length; m++) {
                            if (ProviderServiceData[m].NPINumber == userID) {
                                return ProviderServiceData[m];
                            }
                        }
                }
                else if (userType.toLowerCase() == 'member') {
                    if (MemberSearchData.length > 0)
                        for (var m = 0; m < MemberSearchData.length; m++) {
                            if (MemberSearchData[m].Member.MemberMemberships[0].Membership.SubscriberID == userID) {
                                return MemberSearchData[m];
                            }
                        }
                }
            }
            else {
                if (ProviderServiceData.length > 0)
                    for (var m = 0; m < ProviderServiceData.length; m++) {
                        if (ProviderServiceData[m].NPINumber == userID) {
                            return ProviderServiceData[m];
                        }
                    }
                for (var m = 0; m < MemberSearchData.length; m++) {
                    if (MemberSearchData[m].Member.MemberMemberships[0].Membership.SubscriberID == userID) {
                        return MemberSearchData[m];
                    }
                }
            }
        };

        var setUserData = function (userData, userType) {
            //if (userType.toLowerCase() == 'member') {
            //    $("#memberID").text(userData.MemberID);
            //    $("#memberName").text(userData.FirstName + " " + userData.LastName);
            //    (userData.Gender.toLowerCase() == 'female') ? $("#memberGender").text("F") : $("#memberGender").text("M")
            //    $("#memberDob").text(userData.DOB);
            //    $("#memberAge").text("");
            //    $("#memberSubGrp").text("");
            //    $("#memberEffDate").text(userData.EffectiveDate);
            //    $("#memberEligibility").text("12/31/2018");
            //    $("#memberPCP").text(userData.PCP);
            //    $("#memberPCPPhone").text(userData.PCPPhone);
            //}
        }
        /* /SERVICE STUB*/



        /* INITIALIZING STUFF*/
        getAllScriptsAndLinksInHead();
        //getAllScriptsInHead();  //Get all the scripts present in the <head> section
        //$(".inner_menu_item").first().click();
        setTimeout(function () {
            resetscreen();
        }, 1000);
        /* /INITIALIZING STUFF*/







        /* UTILITY FUNCTIONS*/

        this.CurrentlyActiveTab = {};
        var closeSubTab = function () {
            if (ActiveTab.IsSubTab) closeInnerTab(ActiveTab.Id);
        };
        this.closeCurrentlyActiveSubTab = function () {
            closeSubTab();
        };

        var closeParentTab = function () {
            var indexData = getInnerTabIndex(ActiveTab.Id);
            var parentIndex = indexData.parentTabIndex;
            closeOuterTab(TabData[parentIndex].Id);
        };
        this.closeCurrentlyActiveParentTab = function () {
            closeParentTab();
        };

        var closeCurrentTab = function () {
            if (!ActiveTab.IsSubTab) closeOuterTab(ActiveTab.Id);
        };
        this.closeCurrentlyActiveTab = function () {
            closeCurrentTab();
        };

        this.navigateToTab = function (ob) {
            getTabContent(true, ob)
        };

        this.loadOrReloadScriptsUsingId = function (id, callback, arg) {
            var container = document.getElementById(id);
            checkAndPushInternalPLinksAndPScripts(container);
            if (window[callback]) window[callback](arg);
        };

        this.loadOrReloadScriptsUsingHtml = function (html, callback, arg) {
            var container = document.createElement('div');
            container.id = "tempId";
            container.innerHTML = html;
            $('body').append(container);
            checkAndPushInternalPLinksAndPScripts(container);
            $('#tempId').remove();
            if (window[callback]) {
                if (arg) window[callback](arg);
                else window[callback];
            }

        };

        var openModal = function (targetUrl, title, buttons, print, callback, args, data) {
            function CloseModal() {
                $slide_modal.html('');
                $slide_modal.animate({ width: '0px' }, 400, 'swing', function () {
                    $modal_background.remove();
                });
            }
            if (buttons.toLowerCase() == 'both') var $template = '<div class="modal_background"><div class="slide_modal"><div class="modal_header" id="sideModalHeader"><h3 class="col-md-5">' + title + '</h3><div class="col-lg-6 pull-right"><span class="close_btn"><button class="btn  btn-cancel close_modal_btn pull-right" >Cancel</button></span>' + ((typeof print != 'undefined') ? (print != null) ? (print.toLowerCase() == 'print') ? '<span class="print_btn" ><button class="btn  btn-primary pull-right PrintBtn" id="modalPrintBtn" data-index="">Print</button></span>' : '' : '' : '') + '<span class="save_btn" ><button class="btn  btn-success pull-right SaveBtn" id="modalSaveBtn" data-index="">Save</button></span></div><div class="modal_body"></div></div></div>';
            if (buttons.toLowerCase() == 'none') var $template = '<div class="modal_background"><div class="slide_modal"><div class="modal_header" id="sideModalHeader"><h3 class="col-md-5">' + title + '</h3><div class="col-lg-6 pull-right">' + ((typeof print != 'undefined') ? (print != null) ? (print.toLowerCase() == 'print') ? '<span class="print_btn" ><button class="btn  btn-primary pull-right PrintBtn" id="modalPrintBtn" data-index="">Print</button></span>' : '' : '' : '') + '</div><div class="modal_body"></div></div></div>';
            if (buttons.toLowerCase() == 'cancel') var $template = '<div class="modal_background"><div class="slide_modal"><div class="modal_header" id="sideModalHeader"><h3 class="col-md-5">' + title + '</h3><div class="col-lg-6 pull-right"><span class="close_btn"><button class="btn  btn-cancel close_modal_btn pull-right" >Cancel</button></span>' + ((typeof print != 'undefined') ? (print != null) ? (print.toLowerCase() == 'print') ? '<span class="print_btn" ><button class="btn  btn-primary pull-right PrintBtn" id="modalPrintBtn" data-index="">Print</button></span>' : '' : '' : '') + '</div><div class="modal_body"></div></div></div>';
            if (buttons.toLowerCase() == 'save') var $template = '<div class="modal_background"><div class="slide_modal"><div class="modal_header" id="sideModalHeader"><h3 class="col-md-5">' + title + '</h3><div class="col-lg-6 pull-right"><span class="save_btn" ><button class="btn btn-success pull-right SaveBtn" id="modalSaveBtn" data-index="">Save</button></span>' + ((typeof print != 'undefined') ? (print != null) ? (print.toLowerCase() == 'print') ? '<span class="print_btn" ><button class="btn  btn-primary pull-right PrintBtn" id="modalPrintBtn" data-index="">Print</button></span>' : '' : '' : '') + '</div><div class="modal_body"></div></div></div>';


            $('body').append($template);
            var $window = $(window),
            $window_height = $window.height(),
            $window_width = $window.width(),
            $modal_background = $('.modal_background'),
            $slide_modal = $modal_background.find('.slide_modal');
            $modal_header = $slide_modal.find('.modal_header');
            $p = $modal_header.find('h3');
            $btn = $modal_header.find('div');
            $button = $btn.find('button');
            $('.slide_modal').attr("id", "slide_modal");
            $slide_modal.css({ height: $window_height + 'px', width: '0px' });
            $modal_background.css({ height: $window_height + 'px', width: $window_width + 'px' });
            $modal_header.css({ height: 50 + 'px', backgroundColor: '#4a88c7', 'margin-top': '-15px', 'margin-left': '-5px', 'margin-right': '-5px' });
            $p.css({ color: 'white', 'margin-top': '15px' });
            $btn.css({ 'margin-top': '10px' });
            //$button.css({ 'width': '60px', height: '30px', 'font-size': 'medium' });
            $slide_modal.animate({ width: $window_width * 0.8 + 'px' }, 400, 'swing', function () {

                //--------------------------click event for closing-------------------------------
                $modal_background.on('click', function () {
                    CloseModal();
                });

                $slide_modal.on('click', function (e) {
                    e.stopPropagation();
                });

                $slide_modal.find('.close_btn').on('click', function () {
                    CloseModal();
                });
                showLoader('slide_modal');
                if (targetUrl.search('.cshtml') > -1) {
                    var url = "/Home/GetPartial?partialURL=" + targetUrl;
                    if (typeof data == 'undefined') data = '';
                    $.ajax({
                        type: "POST",
                        url: url,
                        dataType: 'html',
                        success: function (result) {
                            $slide_modal.find('.modal_body').html(result);
                            TabManager.loadOrReloadScriptsUsingHtml(result, callback, args);
                            removeLoader();
                        }
                    });
                }
                else {
                    var url = targetUrl;
                    if (typeof data == 'undefined') data = '';
                    $.ajax({
                        type: "POST",
                        url: url,
                        dataType: 'html',
                        data: data,
                        success: function (result) {
                            $slide_modal.find('.modal_body').empty().html(result);
                            TabManager.loadOrReloadScriptsUsingHtml(result, callback, args);
                            removeLoader();
                        }
                    });
                }

            });
        }
        this.openSideModal = function (targetUrl, title, buttons, print, callback, args, data) {
            openModal(targetUrl, title, buttons, print, callback, args, data);
            setTimeout(function () {
                if (window[callback]) {
                    if (args != null) window[callback](args);
                    else window[callback]();
                }
            }, 500);
        };

        $('body').off("click", ".dynamic-content").on("click", ".dynamic-content", function (e) {
            if (((typeof $(this).data("callback")) != 'undefined') && ((typeof $(this).data("arguments")) != 'undefined') && ((typeof $(this).data("callback")) != '') && ((typeof $(this).data("arguments")) != '')) {
                getDynamicContent($(this).data("targeturl"), $(this).data("targetid"), $(this).data("callback"), $(this).data("arguments"));
            }
            else if ((typeof $(this).data("callback")) != 'undefined' && (typeof $(this).data("callback")) != '') {
                getDynamicContent($(this).data("targeturl"), $(this).data("targetid"), $(this).data("callback"));
            }
            else {
                if (((typeof $(this).data("targeturl")) != 'undefined') && ((typeof $(this).data("targetid")) != 'undefined') && ((typeof $(this).data("targeturl")) != '') && ((typeof $(this).data("targetid")) != '')) {
                    getDynamicContent($(this).data("targeturl"), $(this).data("targetid"));
                }
            }
        });
        var getDynamicContent = function (partialUrl, id, callback, args) {
            showLoader(id);
            if ((typeof partialUrl != 'undefined') && (typeof id != 'undefined')) {
                if (partialUrl.search('.cshtml') > -1) {
                    var url = "/Home/GetPartial?partialURL=" + partialUrl;
                }
                else {
                    var url = partialUrl;
                }
                //var viewDuplicacyCheck = checkViewDuplicacy(partialUrl);
                //if (viewDuplicacyCheck.exists) {
                //    getTabViewFromViewsCache(viewDuplicacyCheck.viewCacheIndex, id);
                //    if (window[callback]) {
                //        if (args != null) window[callback](args);
                //        else window[callback]();
                //    }
                //}
                //else {
                    $.ajax({
                        type: "POST",
                        url: url,
                        dataType: 'html',
                        success: function (result) {
                            var container = document.getElementById(id);
                            if (typeof container != 'undefined') {
                                container.innerHTML = result + '<div class="clearfix"></div>'; //+ $footerClone.html();
                                removeLoader(); //Remove the loading spinner
                                addViewToCache(partialUrl, result);
                                checkAndPushInternalPLinksAndPScripts(container);
                            }
                            if (window[callback]) {
                                if (args != null) window[callback](args);
                                else window[callback]();
                            }
                        }
                    });
              }
            //}
        }
        this.getDynamicContent = function (partialUrl, id, callback, args) {
            if (typeof partialUrl != 'undefined') getDynamicContent(partialUrl, id, callback, args);
        };

        var getCurrentMemberData = function () {
            if (ActiveTab.IsSubTab) {
                var indexData = getInnerTabIndex(ActiveTab.Id);
                if (indexData) {
                    var parentIndex = indexData.parentTabIndex;
                    //return TabData[parentIndex].MemberData;
                    var member = getUserData(TabData[parentIndex].UserId);
                    return member;
                }
                else {
                    var userId = $('#MemberID').text();
                    if (typeof userId != "undefined") {
                        var member = getUserData(userId, 'Member');
                        return member;
                    }
                    else {
                        var userId = $('#ProviderID').text();
                        if (typeof userId != "undefined") {
                            var member = getUserData(userId, 'Provider');
                            return member;
                        }
                    }
                }
            }
            else {
                var userId = ActiveTab.Id;
                if (typeof userId != "undefined") {
                    var member = getUserData(userId);
                    return member;
                }
            }
            return null;
        };
        this.getMemberData = function () {
            return getCurrentMemberData();
        }

        var getCurrentProviderData = function () {
            if (ActiveTab.IsSubTab) {
                var indexData = getInnerTabIndex(ActiveTab.Id);
                if (indexData) {
                    var parentIndex = indexData.parentTabIndex;
                    //return TabData[parentIndex].MemberData;
                    var provider = getUserData(TabData[parentIndex].UserId, 'provider');
                    return provider;
                }
                else {
                    var userId = $('#ProviderID').text();
                    if (typeof userId != "undefined") {
                        var provider = getUserData(userId, 'provider');
                        return provider;
                    }
                }
            }
            else {
                var userId = ActiveTab.Id;
                if (typeof userId != "undefined") {
                    var provider = getUserData(userId, 'provider');
                    return provider;
                }
            }
            return null;
        };
        this.getProviderData = function () {
            return getCurrentProviderData();
        };

        var getMemberId = function () {
            if (ActiveTab.IsSubTab) {
                var indexData = getInnerTabIndex(ActiveTab.Id);
                if (indexData) {
                    var parentIndex = indexData.parentTabIndex;
                    if (TabData[parentIndex].UserType.toUpperCase() == "MEMBER" || TabData[parentIndex].UserType.toUpperCase() == "PROVIDER") return TabData[parentIndex].UserId;
                }
            }
            else {
                ActiveTab.UserId;
            }
        };

        this.getMemberID = function () {
            getMemberId();
        };

        this.showLoadingSymbol = function (id) {
            showLoader(id);
        };

        this.hideLoadingSymbol = function () {
            removeLoader();
        };


        var setCenterModalHt = function () {
            $('#SharedModalContent').css("height", $(window).height() - 20);
            $('#SharedModal').css("height", $(window).height() - 20);
            $('#SharedModalBody').css({ 'height': $('#SharedModalContent').height() - $('#SharedModalHeader').height() - 30, "overflow-y": "auto" });
        };
        this.setCenterModalHeight = function () {
            setCenterModalHt();
        }

        var showCenterModal = function (targetUrl, title, callback, args) {
            $("#SharedModal").modal({
                backdrop: "static",
                show: true
            });
            setCenterModalHt();
            $("#SharedModalTitle").text(title);
            if (targetUrl.search('.cshtml') > -1) {
                var url = "/Home/GetPartial?partialURL=" + targetUrl;
            }
            else {
                var url = targetUrl;
            }
            $.ajax({
                type: "POST",
                url: url,
                dataType: 'html',
                success: function (result) {
                    $('#SharedModalBody').empty().html(result);
                    TabManager.loadOrReloadScriptsUsingHtml(result, callback, args);
                    removeLoader();
                    setTimeout(function () {
                        if (window[callback]) {
                            if (args != null) window[callback](args);
                            else window[callback]();
                        }
                    }, 500);
                }
            });
        }
        this.openCenterModal = function (targetUrl, title, callback, args) {
            showCenterModal(targetUrl, title, callback, args);
        };

        var renderPartial = function (targetUrl, id, color, callback, args) {
            if (targetUrl.search('.cshtml') > -1) {
                var url = "/Home/GetPartial?partialURL=" + targetUrl;
            }
            else {
                var url = targetUrl;
            }
            $.ajax({
                type: "POST",
                url: url,
                dataType: 'html',
                success: function (result) {
                    $('#' + id).empty().html(result);
                    if ((id != 'SharedFloatingModalBody') && (id != 'SharedFloatingModalBodyWhite')) {
                        TabManager.loadOrReloadScriptsUsingHtml(result);
                    }
                    else {
                        TabManager.loadOrReloadScriptsUsingHtml(result, callback, args);
                        removeLoader();
                        setTimeout(function () {
                            if (window[callback]) {
                                if (args != null) window[callback](args);
                                else window[callback]();
                            }
                        }, 500);
                    }
                }
            });
        };
        var showFloatingModal = function (bodyUrl, headerUrl, footerUrl, color, callback, args) {
            $('#SharedFloatingModalBody').html("");
            $('#SharedFloatingModalHeader').html("");
            $('#SharedFloatingModalFooter').html("");
            //$("#SharedFloatingModalTitle").text(title);
            if (typeof color != 'undefined') {
                $("#SharedFloatingModal" + color).modal({
                    backdrop: "static",
                    show: true
                }).draggable({
                    handle: ".modal-header"
                });
                renderPartial(bodyUrl, 'SharedFloatingModalBody' + color, color, callback, args);
                renderPartial(headerUrl, 'SharedFloatingModalHeader' + color);
                renderPartial(footerUrl, 'SharedFloatingModalFooter' + color);
            }
            else {
                $("#SharedFloatingModal").modal({
                    backdrop: "static",
                    show: true
                }).draggable({
                    handle: ".modal-header"
                });
                renderPartial(bodyUrl, 'SharedFloatingModalBody', "", callback, args);
                renderPartial(headerUrl, 'SharedFloatingModalHeader');
                renderPartial(footerUrl, 'SharedFloatingModalFooter');
            }

        };
        this.openFloatingModal = function (bodyUrl, headerUrl, footerUrl, color, callback, args) {
            showFloatingModal(bodyUrl, headerUrl, footerUrl, color, callback, args);
            if (callback) {
                setTimeout(function () {
                    if (window[callback]) {
                        if (typeof args != 'undefined') window[callback](args);
                        else window[callback]();
                    }
                }, 500);
            }
        };
        /* /UTILITY FUNCTIONS*/
    }
    TabManager = new TabsManager();
});


