var chatApp = angular.module('chatApp');

chatApp.controller('chatListController', ["$scope", "$rootScope", "ChatService", "DataService",
    function ChatListController($scope, $rootScope, ChatService, DataService) {

        ChatService.ConnectNewAgent();
        var maxDialogLinks = 6;
        var dialogsLinks = new Array();

        var dialogLink = function (dialog) {
            this.Dialog = dialog;
            this.isEmpty = function () {
                if (this.Dialog === null)
                    return true;
                return false;
            }
        }

        $scope.$watch(function () { return ChatService.getDialogs(); }, function (newVal, oldVal) {
            if (typeof newVal !== 'undefined') {
                dialogsLinks = [];
                var dialogs = ChatService.getDialogs();
                for (var i = 0; i < maxDialogLinks; i++) {
                    if (dialogs[i] != null)
                        dialogsLinks.push(new dialogLink(dialogs[i]))
                    else
                        dialogsLinks.push(new dialogLink(null));
                }
                $rootScope.Dialogs = dialogs;
                $rootScope.DialogsLinks = dialogsLinks;
            }
        });

    }]);