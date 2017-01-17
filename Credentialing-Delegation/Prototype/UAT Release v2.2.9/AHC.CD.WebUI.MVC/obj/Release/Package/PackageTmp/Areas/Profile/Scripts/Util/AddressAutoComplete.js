
profileApp.service('locationService', ['$http',  function ($http) {
    //location service to return array of locations relevant to the querystring
    this.getLocations = function (QueryString) {
        return $http.get("/Location/GetCities?city=" + QueryString)     //locationService makes an ajax call to fetch all the cities which have relevent names as the query string.
        .then(function (response) { return response.data; });     //Which is then returned to the controller method which called the service
    };

    //this.getStates = function () {
    //    return $http.get("/Location/GetStates")     //locationService makes an ajax call to fetch all the cities which have relevent names as the query string.
    //    .then(function (response) {
    //        return response.data;
    //    });     //Which is then returned to the controller method which called the service
    //};
}]);

profileApp.service('PractitionersService', ['$http', function ($http) {
    this.getPractitionersByType = function (type, query) {
        return $http.get("?type=" + query + "&query=" + query)
        .then(function (response) { return response.data; });
    };

    this.SupervisingPractitioners = function (query) {
        return $http.get("" + query)
        .then(function (response) { return response.data; });
    };
}]);