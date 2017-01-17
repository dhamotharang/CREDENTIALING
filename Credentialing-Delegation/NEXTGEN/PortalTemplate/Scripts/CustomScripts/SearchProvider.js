var SearchProvider = angular.module('SearchProvider', []);
SearchProvider.controller('SearchProviderController', ['$scope', '$rootScope', '$http', '$window', '$filter', '$timeout', 'httpPreConfig', function ($scope, $rootScope, $http, $window, $filter, $timeout, httpPreConfig) {
    $scope.ProviderList = [
          { ProviderID: 1, Photo: '~/Content/Images/images (1).jpg', Name: 'Dr. Parikshit Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 2, Photo: '~/Content/Images/images (2).jpg', Name: 'Dr. Pritam Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 3, Photo: '~/Content/Images/images (3).jpg', Name: 'Dr. Sarkar Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 4, Photo: '~/Content/Images/images (4).jpg', Name: 'Dr. Natasha Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 5, Photo: '~/Content/Images/images (1).jpg', Name: 'Dr. Sanjay Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 6, Photo: '~/Content/Images/images (2).jpg', Name: 'Dr. Prakash Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 7, Photo: '~/Content/Images/images (3).jpg', Name: 'Dr. Prakash Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 8, Photo: '~/Content/Images/images (4).jpg', Name: 'Dr. Prakash Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 9, Photo: '~/Content/Images/images (4).jpg', Name: 'Dr. Prakash Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 10, Photo: '~/Content/Images/images (3).jpg', Name: 'Dr. Prakash Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 11, Photo: '~/Content/Images/images (2).jpg', Name: 'Dr. Prakash Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 12, Photo: '~/Content/Images/images (1).jpg', Name: 'Dr. Prakash Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 13, Photo: '~/Content/Images/images (2).jpg', Name: 'Dr. Prakash Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 14, Photo: '~/Content/Images/images (3).jpg', Name: 'Dr. Prakash Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 15, Photo: '~/Content/Images/images (4).jpg', Name: 'Dr. Prakash Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 16, Photo: '~/Content/Images/images (1).jpg', Name: 'Dr. Prakash Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
    ]
    console.log($scope.ProviderList);
}]);
