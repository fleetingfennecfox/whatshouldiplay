(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("loginController", LoginController);

    LoginController.$inject = ["$scope"];

    function LoginController($scope) {
        var vm = this;
        vm.$scope = $scope;
        vm.hello = "Hello from login!";

        //THE FOLD


    }
})();