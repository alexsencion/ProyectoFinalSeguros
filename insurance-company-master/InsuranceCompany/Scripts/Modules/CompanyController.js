"use strict";

angular.module("CompanyApp").controller("GetAllClientsController", function ($http) {
    var cc = this;
    cc.clients = [];
    cc.click = function () {
        $http.get("/companyApi/clients")
            .then(function(response) {
                cc.clients = response.data;
            });
    }
});

angular.module("CompanyApp").controller("ClientByIdController", function ($http) {
    var cc = this;
    cc.click = function () {
        var id = cc.clientId;
        $http.get("/companyApi/clientbyId/" + id).then(function(response) {
            cc.client = null;
            if (response.data == null || response.data.length === 0) {
                swal("Client not found", "Client id " + id + " not found. Try a different id", "info");
            } else {
                cc.client = response.data;
            }
        });
    }
});

angular.module("CompanyApp").controller("ClientByNameController", function ($http) {
    var cc = this;
    cc.click = function () {
        var name = cc.name;
        $http.get("/companyApi/clientByName/" + name).then(function(response) {
            cc.clients = [];
            if (response.data == null || response.data.length === 0) {
                swal("Client not found", "Client with name: " + name + " not found. Try with a new name", "info");
            } else {
                cc.clients = response.data;
            }
        });
    }
});

angular.module("CompanyApp").controller("PoliciesByNameController", function ($http) {
    var cc = this;
    cc.click = function () {
        var name = cc.name;
        $http.get("/companyApi/policiesByClientName/" + name).then(function(response) {
            if (response.data == null || response.data.length === 0) {
                swal("Policy not found",
                    "Policies for " + name + " not found. Try with a different client name",
                    "info");
            } else {
                cc.policies = response.data;
            }
        });
    }
});

angular.module("CompanyApp").controller("ClientByPolicyController", function ($http) {
    var cc = this;
    cc.click = function () {
        var policy = cc.policy;
        $http.get("/companyApi/clientPolicy/" + policy).then(function(response) {
            if (response.data == null || response.data.length === 0) {
                swal("Client not found", "Client with policy " + policy + " not found. Try different one", "info");
            } else {
                cc.client = response.data;
            };
        });
    }
});

angular.module("CompanyApp").controller("menuController", function ($http, $window, authenticationService) {
    var self = this;
    self.user = window.sessionStorage.getItem("currentUserName");
    self.click = function () {
        authenticationService.ClearCredentials();
        $window.location.href = "/";
    }
});