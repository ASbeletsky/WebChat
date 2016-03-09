var chatApp = angular.module('chatApp');

chatApp.controller('chatController', ["$scope", '$routeParams', "ChatService", "DataService",
    function ChatController($scope, $routeParams, ChatService, DataService) {

        var activeDialogId = function () {
            var idInString = $routeParams.id;
            if (idInString.charAt(0) === ':')
                idInString = idInString.slice(1);
            return parseInt(idInString);
        }();
        
        ChatService.setActiveDialog(activeDialogId);
        
        $scope.$watch(function () { return ChatService.getActiveDialog() }, function (newVal, oldVal) {
            if (typeof newVal !== 'undefined') {
                $scope.Dialog = ChatService.getActiveDialog();
                $scope.Messages = $scope.Dialog.getMessages();
                $scope.Client = $scope.Dialog.Client;

                DataService.GetClientInfo($scope.Client.Id).then(function (info) {
                    $scope.Client.Info = info;
                });
            }
        });


        

        $scope.newMessage = '';
        $scope.sendMessage = function () {
            $scope.Dialog.SendMessage($scope.newMessage);
            $('#message').val('').focus();
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