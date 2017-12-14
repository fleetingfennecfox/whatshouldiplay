(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("loginController", LoginController);

    LoginController.$inject = ["$scope", "ajaxService"];

    function LoginController($scope, AjaxService) {
        var vm = this;
        //Injections
        vm.$scope = $scope;
        vm.AjaxService = AjaxService;
        //Functions
        vm.login = _login;
        vm.success = _success;
        vm.error = _error;
        //Variables
        vm.hello = "Hello from login!";
        vm.item;

        //THE FOLD

        function _login() {
            vm.item = {
                email: "email@email.com",
                password: "password"
            };

            vm.AjaxService.post("/api/login/", vm.item)
                .then(vm.success)
                .catch(vm.error);
        }

        function _success(res) {
            console.log(res);
        }

        function _error(err) {
            console.log(err);
        }
    }
})();