"use strict";

angular.module("Authentication")
 .controller("LoginController", ["$scope", "$location", "$window", "authenticationService",
    function ($scope, $location, $window, authenticationService) {
        // reset login status
        authenticationService.ClearCredentials();

        $scope.login = function () {  
            $scope.dataLoading = true;
            authenticationService.Login($scope.username, $scope.password, 
                function (response, token) {
                    if (response.success) {
                        $scope.dataLoading = false;
                        authenticationService.SetCredentials($scope.username, token);
                        $window.location.href = "/Home/Company";
                    }
                },
                function (response) {
                    $scope.dataLoading = false;
                    $scope.loginError = true;
                    $scope.error = response.data.error_description;
                });
        };
}]);