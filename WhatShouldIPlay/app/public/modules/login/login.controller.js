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
        vm.loginSuccess = _loginSuccess;
        vm.error = _error;
        //Variables
        vm.hello = "Login";
        vm.item;

        //THE FOLD

        function _login() {
            vm.AjaxService.post("/api/login/", vm.item)
                .then(vm.loginSuccess)
                .catch(vm.error);
        }

        function _loginSuccess(res) {
            console.log(res);
        }

        function _error(err) {
            console.log(err);
        }
    }
})();