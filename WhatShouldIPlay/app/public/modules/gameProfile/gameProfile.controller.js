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
        vm.error = _error;
        //Variables
        vm.hello = "Games Index";
        vm.item;
        vm.gamesList;
        vm.editing = false;

        //THE FOLD

        function _onInit() {
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

        function _getGameByIdSuccess(res) {
            console.log(res);
            vm.item.title = res.data.title;
            vm.item.platforms = res.data.platforms;
            vm.item.genres = res.data.genres;
            vm.item.studio = res.data.studio;
            vm.item.directors = res.data.directors;
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

        function _deleteGame(id) {
            vm.AjaxService.delete("/api/games/delete/" + id)
                .then(vm.deleteGameSuccess)
                .catch(vm.error);
        }

        function _deleteGameSuccess(res) {
            if (res.data) {
                vm.hello = "Delete Success!";
                vm.item = {};
                _getAllGames();
            } else {
                vm.hello = "Delete Failed";
            }
        }

        function _error(err) {
            vm.hello = "Error!";
            console.log(err);
        }
    }
})();