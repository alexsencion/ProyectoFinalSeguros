"use strict";

angular.module("Authentication", []); 

angular.module("CompanyApp", [
    "Authentication",
    "ngRoute",
    "ngCookies",
    "ui.bootstrap",
    "angularUtils.directives.dirPagination"
])

    .factory("myHttpInterceptor", function($q, $location) {
        return {
            'request': function(config) {
                var currentAuthData = sessionStorage.getItem("currentUserAuthData");
                config.headers["Authorization"] = "Bearer " + currentAuthData; 
                return config;
            },

            'responseError': function(rejection) {
                switch (rejection.status) {
                    case 400:
                    case 404:
                        return $q.reject(rejection);
                    case 401:
                        swal("Access denied", "You don't have permisions to use this method", "error");
                        return $q.reject(rejection);
                    case 403:
                        $location.path("/");
                        return false;
                    case 500:
                        swal("Internal error", "Internal error, please, try again later", "error");
                        return false;
                }

                return $q.reject(rejection);
            }
        };
    })

    .config(["$locationProvider" , "$httpProvider", function($locationProvider, $httpProvider) {
        $httpProvider.interceptors.push("myHttpInterceptor");
        
        $locationProvider.hashPrefix("");
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });

    }]);
