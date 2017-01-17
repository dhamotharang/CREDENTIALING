var homeApp = angular.module("HomeApp", ['smart-table']);

homeApp.controller("homeCtrl", function ($scope, $filter) {
    $scope.selectedRole = "SuperAdmin";
    $scope.allMenuItems = angular.copy(menudata);

    $scope.ShowContent = 'ProviderList';
    $scope.ShowProviderProfile = function (ProviderID) {
        //$("#SearchResultID").hide();
        //$("#ProviderProfileID").show();
        //$scope.ShowContent = 'ProviderView';

        $.ajax({
            type: 'GET',
            url: '/AddProvider/GetProviderProfile',
            success: function (data) {
                $('#ProviderProfile').html(data);
                $('.provider_list').hide();
            }
        });
        
    }
    $scope.BackToSearchResult = function () {
        $('#ProviderProfile').remove();
        $('.provider_list').show();
        alert();
    }

    $scope.filterMenu = function () {
        var filteredData = $filter('filter')($scope.allMenuItems, { Role: $scope.selectedRole });
        $scope.menuItem = filteredData[0].RoleMenu;
    };
    $scope.filterMenu();

    $scope.selectanewrole = function (role) {
        $scope.selectedRole = role;
        $scope.filterMenu();
    }
    $scope.ProviderList = [
          { ProviderID: 1, Photo: "/Resources/Images/author.jpg", Name: 'Dr. Jennine Martin', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 2, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1111111111, Specialty: ' Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 3, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 2222222222, Specialty: ' Dentist', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 4, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 3333333333, Specialty: ' Dentist, Neuro Surgon', Location: 'Alaska', Group: 'Access2' },
          { ProviderID: 5, Photo: '/Resources/Images/images (1).jpg', Name: 'Dr. Sanjay Singh', NPI: 4444444444, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 6, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 7, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 8, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 9, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 10, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 11, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 12, Photo: '/Resources/Images/images (1).jpg', Name: 'Dr. Sanjay Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 13, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 14, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 15, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
          { ProviderID: 16, Photo: '/Resources/Images/images (1).jpg', Name: 'Dr. Sanjay Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' }
    ]
    $scope.ProviderList1 = angular.copy($scope.ProviderList);
    console.log($scope.ProviderList);

    //--------------Provider Profile start-------------

    $scope.ProviderProfileObj = {};

    $scope.IsEditGeneralInformation = false;
    $scope.EditGeneralInformation = function () {
        $scope.IsEditGeneralInformation = true;
    }
    $scope.CancelGeneralInformation = function () {
        $scope.IsEditGeneralInformation = false;
    }

    $scope.IsIdentificationLiscense = false;
    $scope.EditIdentificationLiscense = function () {
        $scope.IsIdentificationLiscense = true;
    }
    $scope.CancelIdentificationLiscense = function () {
        $scope.IsIdentificationLiscense = false;
    }

    $scope.IsEducationHistory = false;
    $scope.EditEducationHistory = function () {
        $scope.IsEducationHistory = true;
    }
    $scope.CancelEducationHistory = function () {
        $scope.IsEducationHistory = false;
    }

    $scope.IsSpecialtyBoard = false;
    $scope.EditSpecialtyBoard = function () {
        $scope.IsSpecialtyBoard = true;
    }
    $scope.CancelSpecialtyBoard = function () {
        $scope.IsSpecialtyBoard = false;
    }

    $scope.IsPracticeLocation = false;
    $scope.EditPracticeLocation = function () {
        $scope.IsPracticeLocation = true;
    }
    $scope.CancelPracticeLocation = function () {
        $scope.IsPracticeLocation = false;
    }

    //--------------Provider Profile end-------------

});

$(document).ready(function () {


    $('.navbar-menu li a').on('click', function () {
        $('.navbar-menu li a').removeClass('active-nav');
        $(this).addClass('active-nav');
        var divID = $(this).attr('href');
        $('html,body').animate({
            scrollTop: $(divID).offset().top - 100
        },
    'slow');
    });

    $('.navbar-menu li a').on('hover', function () {
        $('.navbar-menu li a').removeClass('active-nav');
        $(this).addClass('active-nav');
    })


    $('.scrollable-container').bind('DOMMouseScroll onwheel mousewheel onmousewheel ontouchmove', function (event) {

        $('.target').each(function () {
            if ($(window).scrollTop() >= $(this).offset().top - 100) {
                var id = $(this).attr('id');
                $('.navbar-menu li a').removeClass('active-nav');
                $('.navbar-menu li a[href=#' + id + ']').addClass('active-nav');
            }
        });
    });


});

