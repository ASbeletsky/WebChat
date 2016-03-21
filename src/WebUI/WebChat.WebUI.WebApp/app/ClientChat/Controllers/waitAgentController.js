var chatApp = angular.module('chatApp');

chatApp.controller('waitAgentController', ["$scope",
function AuthController($scope) {
    var baseSiteUrlPath = $("base").first().attr("href");

    $scope.FindAgent = function () {
        window.location = baseSiteUrlPath +  'chat'
    }
}]);