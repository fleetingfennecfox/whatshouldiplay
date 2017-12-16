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