var chatApp = angular.module('chatApp');

chatApp.controller('chatController', ["$scope", "ChatService", "DataService", "AuthService",
    function ChatController($scope, ChatService, DataService, AuthService) {

        $scope.chat = ChatService;

        $scope.$watch(function () { return ChatService.agent; }, function (newVal, oldVal) {
            if (typeof newVal !== 'undefined') {
                $scope.agent = ChatService.agent;
            }
        });

        $scope.$watch(function () { return ChatService.allMessages }, function (newVal, oldVal) {
            if (typeof newVal !== 'undefined') {
                $scope.Messages = ChatService.allMessages;
            }
        });

        $scope.sendMessage = function () {
            $scope.chat.sendNewMessage($scope.message);
            $('#message').val('').focus();
        }

        $scope.EndChat = function () {
            window.location = $("base").first().attr("href") + '/chat/EndChat';
        }

    }]);

chatApp.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter, { 'event': event });
                });

                event.preventDefault();
            }
        });
    };
});