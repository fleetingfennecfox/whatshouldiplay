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
            //vm.AjaxService.post("/api/games/add", vm.item)
            //    .then(vm.addGameSuccess)
            //    .catch(vm.error);
            console.log(vm.item);
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