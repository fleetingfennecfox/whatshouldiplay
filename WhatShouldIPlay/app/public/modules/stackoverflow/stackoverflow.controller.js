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
        vm.hello = "Popular Gaming Questions";
        vm.item;
        vm.results;

        //THE FOLD

        function _getQuestions() {
            console.log("get");
            vm.AjaxService.get("https://api.stackexchange.com/2.2/posts?order=asc&sort=activity&site=gaming&filter=!froe)wRH8NTRQ3(BThufUSjeoJWBHZXzzhN&key=VR553ry0boP9*pIWGsWpVA((")
                .then(vm.getQuestionsSuccess)
                .catch(vm.error);
        }

        function _getQuestionsSuccess(res) {
            console.log(res);
            vm.results = res.data.items;
        }

        function _error(err) {
            console.log(err);
        }
    }
})();