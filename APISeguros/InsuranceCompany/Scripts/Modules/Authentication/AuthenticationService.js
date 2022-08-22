"use strict";

angular.module("Authentication")
.factory("authenticationService",["$http", "$cookies", "$window", 
    function($http, $cookies, $window) {
        var service = {};
        var grantType = "password";
        var token;
        service.Login = function(username, password, callback, errorCallback) {
            var parameter = JSON.stringify({ username: username, password: password, grant_type: grantType });
            $http({
                method: "POST",
                url: "/token",
                headers: { 'Content-Type': "application/x-www-form-urlencoded" },
                params : parameter,
                transformRequest: function(obj) {
                    var str = [];
                    for (var p in obj)
                        if (obj.hasOwnProperty(p))
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                data: { username: username, password: password, grant_type: grantType }
            }).then(function(response) {
                token = response.data.access_token;
                response = { success: status = 200 };
                callback(response, token);
            }, function (response) {
                errorCallback(response);
            });
        };

        service.SetCredentials = function(username, token) {
            $window.sessionStorage.setItem("currentUserName", username);
            $window.sessionStorage.setItem("currentUserAuthData", token);
            $http.defaults.headers.common["Authorization"] = "Bearer " + token;
        };

        service.ClearCredentials = function() {
            $window.sessionStorage.removeItem("currentUser");
            $window.sessionStorage.removeItem("currentUserAuthData");
            $cookies.remove("globals");
            $http.defaults.headers.common.Authorization = "Bearer ";
        };

        return service;
    }
]);
