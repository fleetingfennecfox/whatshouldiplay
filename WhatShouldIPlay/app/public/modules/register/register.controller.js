(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("registerController", RegisterController);

    RegisterController.$inject = ["$scope", "ajaxService"];

    function RegisterController($scope, AjaxService) {
        var vm = this;
        //Injections
        vm.$scope = $scope;
        vm.AjaxService = AjaxService;
        //Functions
        vm.register = _register;
        vm.registerSuccess = _registerSuccess;
        vm.error = _error;
        //Variables
        vm.hello = "Hello from registration!";
        vm.item;

        //THE FOLD

        function _register() {
            vm.AjaxService.post("/api/register/", vm.item)
                .then(vm.registerSuccess)
                .catch(vm.error);
        }

        function _registerSuccess(res) {
            console.log(res);
        }

        function _error(err) {
            console.log(err);
        }
    }
})();