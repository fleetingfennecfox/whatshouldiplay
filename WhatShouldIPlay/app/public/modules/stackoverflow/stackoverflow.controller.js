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