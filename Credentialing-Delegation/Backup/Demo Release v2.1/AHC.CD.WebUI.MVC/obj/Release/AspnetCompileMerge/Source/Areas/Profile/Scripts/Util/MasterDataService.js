// service responsible for getting all active profile master data

profileApp.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

    this.getPractitioners = function (URL, level, profileId) {
        return $http({
            url: URL,
            method: "POST",
            data: { practitionerLevel: level, profileID: profileId }
        }).then(function (value) { return value.data; });
    };    

    this.getProviderLevels = function (URL, profileId) {
        return $http({
            url: URL,
            method: "POST",
            data: { profileID: profileId }
        }).then(function (value) { return value.data; });
    };   
}]);