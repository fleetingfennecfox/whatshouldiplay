
//app.public.js
(function () {
    'use strict';
    window.APP = window.APP || {}; 
    APP.NAME = "publicApp";
    angular
        .module(APP.NAME, ['ui.router', APP.NAME + '.routes', 'ngMaterial', 'angular-img-cropper']);
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

//fileupload\fileupload.controller.js
(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("fileUploadController", FileUploadController);

    FileUploadController.$inject = ["$scope", "ajaxService"];

    function FileUploadController($scope, AjaxService) {
        var vm = this;
        //Injections
        vm.$scope = $scope;
        //Services
        vm.AjaxService = AjaxService;
        //Functions
        vm.uploadFile = _uploadFile;
        vm.uploadFileSuccess = _uploadFileSuccess;
        vm.error = _error;
        //Variables
        vm.item = {};
        vm.cropper = {};
        vm.cropper.sourceImage = null;
        vm.cropper.croppedImage = null;
        vm.bounds = {};
        vm.bounds.top = 0;
        vm.bounds.right = 0;
        vm.bounds.bottom = 0;
        vm.bounds.left = 0;

        //THE FOLD

        function _uploadFile() {
            var image = vm.cropper.croppedImage;
            var imageInfo = image.split(",");
            var getExtension = imageInfo[0].split("/");
            var extension = getExtension[1].split(";");

            vm.item.encodedImagefile = imageInfo[1];
            vm.item.fileExtension = "." + extension[0];

            vm.AjaxService.post("/api/upload/file", vm.item)
                .then(vm.uploadFileSuccess)
                .catch(vm.error);
        }

        function _uploadFileSuccess(res) {
            console.log(res);
        }

        function _error(err) {
            console.log(err);
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

//gameProfile\gameProfile.controller.js
(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("gameProfileController", GameProfileController);

    GameProfileController.$inject = ["$scope", "ajaxService"];

    function GameProfileController($scope, AjaxService) {
        var vm = this;
        //Injections
        vm.$scope = $scope;
        vm.AjaxService = AjaxService;
        //Functions
        vm.$onInit = _onInit;
        vm.addGame = _addGame;
        vm.addGameSuccess = _addGameSuccess;
        vm.getAllGames = _getAllGames;
        vm.getAllGamesSuccess = _getAllGamesSuccess;
        vm.getGameById = _getGameById;
        vm.getGameByIdSuccess = _getGameByIdSuccess;
        vm.updateGame = _updateGame;
        vm.updateGameSuccess = _updateGameSuccess;
        vm.deleteGame = _deleteGame;
        vm.deleteGameSuccess = _deleteGameSuccess;
        vm.getAttributes = _getAttributes;
        vm.getAttributesSuccess = _getAttributesSuccess;
        vm.error = _error;
        vm.cancelUpdate = _cancelUpdate;
        vm.confirmDeletion = _confirmDeletion;
        vm.cancelDeletion = _cancelDeletion;
        //Variables
        vm.hello = "Games Index";
        vm.item;
        vm.gamesList;
        vm.platforms;
        vm.genres;
        vm.studios;
        vm.directors;
        vm.editing = false;
        vm.deleting = false;

        //THE FOLD

        function _onInit() {
            _getAttributes();
            _getAllGames();
        }

        function _addGame() {
            vm.AjaxService.post("/api/games/add", vm.item)
                .then(vm.addGameSuccess)
                .catch(vm.error);
        }

        function _addGameSuccess(res) {
            vm.hello = "Add Successful!";
            console.log(res);
            vm.item = {};
            _getAllGames();
        }

        function _getAllGames() {
            vm.AjaxService.get("/api/games/getall")
                .then(vm.getAllGamesSuccess)
                .catch(vm.error);
        }

        function _getAllGamesSuccess(res) {
            vm.gamesList = res.data;
            vm.item = {};
        }

        function _getGameById(id) {
            vm.AjaxService.get("/api/games/get/" + id)
                .then(vm.getGameByIdSuccess)
                .catch(vm.error);
        }

        //Right now using this to populate the fields, but use for details later as well
        function _getGameByIdSuccess(res) {
            console.log(res);
            vm.item.title = res.data.title;

            vm.item.platforms = [];
            vm.item.genres = [];
            vm.item.studio = [];
            vm.item.directors = [];

            for (var i = 0; i < res.data.platforms.length; i++) {
                vm.item.platforms.push(res.data.platforms[i].id);
            }

            for (var i = 0; i < res.data.genres.length; i++) {
                vm.item.genres.push(res.data.genres[i].id);
            }

            vm.item.studio = res.data.studios.id;

            for (var i = 0; i < res.data.directors.length; i++) {
                vm.item.directors.push(res.data.directors[i].id);
            }

            vm.item.id = res.data.id;
            vm.editing = true;
        }

        function _updateGame() {
            vm.AjaxService.put("/api/games/update/", vm.item)
                .then(vm.updateGameSuccess)
                .catch(vm.error);
        }

        function _updateGameSuccess(res) {
            if (res.data) {
                vm.hello = "Update Successful!";
                vm.editing = false;
                vm.item = {};
                _getAllGames();
            } else {
                vm.hello = "Update Failed";
            }
        }

        function _deleteGame() {
            vm.AjaxService.delete("/api/games/delete/" + vm.item.id)
                .then(vm.deleteGameSuccess)
                .catch(vm.error);
        }

        function _deleteGameSuccess(res) {
            if (res.data) {
                vm.hello = "Delete Success!";
                vm.deleting = false;
                vm.item = {};
                _getAllGames();
            } else {
                vm.hello = "Delete Failed";
            }
        }

        function _getAttributes() {
            vm.AjaxService.get("/api/games/attributes/")
                .then(vm.getAttributesSuccess)
                .catch(vm.error);
        }

        function _getAttributesSuccess(res) {
            vm.platforms = res.data.platforms;
            vm.genres = res.data.genres;
            vm.studios = res.data.studios;
            vm.directors = res.data.directors;
        }

        function _error(err) {
            vm.hello = "Error!";
            console.log(err);
        }

        function _cancelUpdate() {
            vm.item = {};
            vm.editing = false;
        }

        function _confirmDeletion(id) {
            vm.deleting = true;
            vm.item.id = id;
        }

        function _cancelDeletion() {
            vm.deleting = false;
            vm.item = {};
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

//stackoverflow\stackoverflow.controller.js
(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("stackoverflowController", StackoverflowController);

    StackoverflowController.$inject = ["$scope", "ajaxService"];

    function StackoverflowController($scope, AjaxService) {
        var vm = this;
        //Injections
        vm.$scope = $scope;
        vm.AjaxService = AjaxService;
        //Functions
        vm.getQuestions = _getQuestions;
        vm.getQuestionsSuccess = _getQuestionsSuccess;
        vm.error = _error;
        //Variables
        vm.hello = "Hello from stack overflow!";
        vm.item;

        //THE FOLD

        function _getQuestions() {
            console.log("get");
            vm.AjaxService.get("https://api.stackexchange.com/2.2/questions?order=desc&sort=activity&site=stackoverflow")
                .then(vm.getQuestionsSuccess)
                .catch(vm.error);
        }

        function _getQuestionsSuccess(res) {
            console.log(res);
        }

        function _error(err) {
            console.log(err);
        }
    }
})();

//webscraper\webscraper.controller.js
(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("webScraperController", WebScraperController);

    WebScraperController.$inject = ["$scope", "ajaxService"];

    function WebScraperController($scope, AjaxService) {
        var vm = this;
        //Injections
        vm.$scope = $scope;
        vm.AjaxService = AjaxService;
        //Functions
        vm.getPosts = _getPosts;
        vm.getPostsSuccess = _getPostsSuccess;
        vm.savePost = _savePost;
        vm.savePostSuccess = _savePostSuccess;
        vm.error = _error;
        //Variables
        vm.hello = "Hello from a scraper!";
        vm.item = {};
        vm.results;

        //THE FOLD

        function _getPosts() {
            vm.AjaxService.get("/api/scraper/getall")
                .then(vm.getPostsSuccess)
                .catch(vm.error);
        }

        function _getPostsSuccess(res) {
            vm.hello = "Get Success";
            vm.results = res.data;
            console.log(res);
        }

        function _savePost(title, url) {
            vm.item.title = title;
            vm.item.url = url;

            vm.AjaxService.post("/api/scraper/save", vm.item)
                .then(vm.savePostSuccess)
                .catch(vm.error);
        }

        function _savePostSuccess(res) {
            vm.hello = "Add Success! " + res.data;
            console.log(res);
        }

        function _error(err) {
            vm.hello = "Error!";
            console.log(err);
        }
    }
})();