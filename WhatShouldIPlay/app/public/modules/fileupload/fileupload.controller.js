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