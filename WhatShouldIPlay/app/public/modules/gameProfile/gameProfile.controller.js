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
        vm.hello = "Hello from a games profile!";
        vm.item;

        //THE FOLD

        function _addGame() {
            vm.AjaxService.post("/api/games/add", vm.item)
                .then(vm.addGameSuccess)
                .catch(vm.error);
        }

        function _addGameSuccess(res) {
            vm.hello = "Add Success! " + res.data;
            console.log(res);
            vm.item = {};
        }

        function _getAllGames() {
            vm.AjaxService.get("/api/games/getall")
                .then(vm.getAllGamesSuccess)
                .catch(vm.error);
        }

        function _getAllGamesSuccess(res) {
            vm.hello = "Get All Success!";
            console.log(res);
            vm.item = {};
        }

        function _getGameById() {
            vm.AjaxService.get("/api/games/get/" + vm.item.id)
                .then(vm.getGameByIdSuccess)
                .catch(vm.error);
        }

        function _getGameByIdSuccess(res) {
            vm.hello = "Get By Id Success! " + res.data.id;
            console.log(res);
            vm.item = {};
        }

        function _updateGame() {
            //Throws an error if somnething put into director before and taken out

            vm.AjaxService.put("/api/games/update/", vm.item)
                .then(vm.updateGameSuccess)
                .catch(vm.error);
        }

        function _updateGameSuccess(res) {
            if (res.data) {
                vm.hello = "Update Success!";
                vm.item = {};
            } else {
                vm.hello = "Update Fail";
            }
            console.log(res);
        }

        function _deleteGame() {
            vm.AjaxService.delete("/api/games/delete/" + vm.item.id)
                .then(vm.deleteGameSuccess)
                .catch(vm.error);
        }

        function _deleteGameSuccess(res) {
            if (res.data) {
                vm.hello = "Delete Success!";
                vm.item = {};
            } else {
                vm.hello = "Delete Fail";
            }
            console.log(res);
        }

        function _error(err) {
            vm.hello = "Error!";
            console.log(err);
        }
    }
})();