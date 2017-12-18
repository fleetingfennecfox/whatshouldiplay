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
            })
            .state({
                name: 'games',
                url: '/games',
                templateUrl: '/app/public/modules/gameProfile/gameProfile.html',
                title: 'Games',
                controller: 'gameProfileController as gPCtrl'
            })
            .state({
                name: 'scraper',
                url: '/scraper',
                templateUrl: '/app/public/modules/webscraper/webscraper.html',
                title: 'Webcraper',
                controller: 'webScraperController as scrapeCtrl'
            })
            .state({
                name: 'stackoverflow',
                url: '/stackoverflow',
                templateUrl: '/app/public/modules/stackoverflow/stackoverflow.html',
                title: 'Stack Overflow',
                controller: 'stackoverflowController as stackCtrl'
            })
            .state({
                name: 'fileupload',
                url: '/fileupload',
                templateUrl: '/app/public/modules/fileupload/fileupload.html',
                title: 'File Upload',
                controller: 'fileUploadController as uploadCtrl'
            });
    }
})();