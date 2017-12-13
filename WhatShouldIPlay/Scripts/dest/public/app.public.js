
//app.public.js
(function () {
    'use strict';
    window.APP = window.APP || {}; 
    APP.NAME = "publicApp";
    angular
        .module(APP.NAME, ['ui.router', APP.NAME + '.routes']);
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

    LoginController.$inject = ["$scope"];

    function LoginController($scope) {
        var vm = this;
        vm.$scope = $scope;
        vm.hello = "Hello from login!";

        //THE FOLD


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
                name: 'login',
                url: '/login',
                templateUrl: '/app/public/modules/login/login.html',
                title: 'Login',
                controller: 'loginController as loginCtrl'
            });;
    }
})();