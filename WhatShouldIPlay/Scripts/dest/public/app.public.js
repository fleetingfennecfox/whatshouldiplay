
//app.public.js
(function () {
    'use strict';
    window.APP = window.APP || {}; 
    APP.NAME = "publicApp";
    angular
        .module(APP.NAME, ['ui.router', APP.NAME + '.routes']);
})();

//ajax\ajax.service.js
(function () {
    "use strict";
    angular
        .module("publicApp")
        .factory("ajaxService", AjaxService);

    AjaxService.$inject = ["$http", "$q"];

    function AjaxService($http, $q) {
        return {
            get: _get,
            post: _post,
            put: _put,
            delete: _delete
        }
        function _get(apiendpoint) {
            return $http.get(apiendpoint)
                .then(_success)
                .catch(_error);
        }
        function _post(apiendpoint, data) {
            return $http.post(apiendpoint, data)
                .then(_success)
                .catch(_error);
        }
        function _put(apiendpoint, data) {
            return $http.put(apiendpoint, data)
                .then(_success)
                .catch(_error);
        }
        function _delete(apiendpoint) {
            return $http.delete(apiendpoint)
                .then(_success)
                .catch(_error);
        }
        function _success(response) {
            return response;
        }
        function _error(error) {
            return $q.reject(error);
        }
    }
})();

//home\home.controller.js
(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("homeController", HomeController);

    HomeController.$inject = ["$scope"];

    function HomeController($scope) {
        var vm = this;
        vm.$scope = $scope;
        vm.hello = "Hello from home!";

        //THE FOLD


    }
})();

//login\login.controller.js
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
        vm.hello = "Hello from login!";
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

//register\register.controller.js
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

//routes\app.routes.js
(function () {
    'use strict';
    var app = angular.module("publicApp" + '.routes', []);

    app.config(_configureStates);

    _configureStates.$inject = ['$stateProvider', '$locationProvider', '$urlRouterProvider'];

    function _configureStates($stateProvider, $locationProvider, $urlRouterProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false,
        });
        //$urlRouterProvider.otherwise('/home');
        $stateProvider
            .state({
                name: 'home',
                url: '/home',
                templateUrl: '/app/public/modules/home/home.html',
                title: 'Home',
                controller: 'homeController as homeCtrl'
            })
            .state({
                name: 'register',
                url: '/register',
                templateUrl: '/app/public/modules/register/register.html',
                title: 'Register',
                controller: 'registerController as regCtrl'
            })
            .state({
                name: 'login',
                url: '/login',
                templateUrl: '/app/public/modules/login/login.html',
                title: 'Login',
                controller: 'loginController as loginCtrl'
            });
    }
})();