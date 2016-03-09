var chatApp = angular.module('chatApp');

chatApp.controller('endChatController', ["$scope","ChatService",
function AuthController($scope, ChatService) {
    var baseSiteUrlPath = $("base").first().attr("href");

    $scope.EndChat = function () {
        ChatService.EndChat();
        window.location = baseSiteUrlPath + 'chat/auth';
    }

    $scope.CancelEndChat = function () {
        window.location = baseSiteUrlPath + 'chat';
    }
}]);