var chatApp = angular.module('chatApp');

chatApp.controller('clientController', ["$scope", '$routeParams', "ChatService", "DataService",
    function ClientController($scope, $routeParams, ChatService, DataService) {
        
        var GetPhoto = function () {
            DataService.GetPhoto().then(function (photoSrc) {
                $scope.photoSrc = photoSrc;
            })
        }
}]);