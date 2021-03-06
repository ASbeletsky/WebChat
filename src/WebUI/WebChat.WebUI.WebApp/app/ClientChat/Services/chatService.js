﻿chatApp.factory('ChatService',
    ["$rootScope", "Hub", '$location', 'DataService',
    function ($rootScope, Hub, $location, DataService) {
        var baseSiteUrlPath = $("base").first().attr("href");

        var ChatService = this;
            ChatService.dialogId = 0;
            ChatService.agent = { Name: 'AgentX', Rank: 'Support Agent' };

/*----------------------------------------------- Начало диалога ----------------------------------------*/
            ChatService.ConnectNewClient = function () {
                hub.promise.done(function () {
                    var appId = DataService.getAppInfo().AppId;
                    hub.onNewClient(appId);
                    
                });
            };
/*------------------------------------------------ Все сообщения -----------------------------------------*/
            ChatService.allMessages = [];

            ChatService.addMessage = function (userName, chatMessage, time) {
                ChatService.allMessages.push(new MessageModel({
                    UserName: userName,
                    ChatMessage: chatMessage,
                    Time: time
                }));
            }
/*-------------------------------------------------- Сообщение ------------------------------------------*/

            var MessageModel = function (message) {
                if (!message) message = {};
                var MessageModel = {
                    UserName: message.UserName,
                    ChatMessage: message.ChatMessage,
                    Time : message.Time
                }
                return MessageModel;
            }

            ChatService.sendNewMessage = function (MessageText) {
                var appId = DataService.getAppInfo.AppId;
                    var message = {
                        AppId: appId,
                        DialogId: ChatService.dialogId,
                        Text: MessageText
                    };
                    hub.onNewMessage(message);
            };
/*---------------------------------------------- Настройки хаба ----------------------------------------*/
        var hub = new Hub("chatHub", {
            listeners: {
                'AuthorizeRedirect' : function(){
                    window.location = baseSiteUrlPath + 'chat/auth';
                },
                'WaitAgentRedirect' :function(){
                    window.location = baseSiteUrlPath + 'chat/WaitAgent';
                },
                'BeginDialog' : function(){
                    ChatService.ConnectNewClient();
                    SendLocation();
                },
                'addNewMessageToPage': function (data) {
                    var message = JSON.parse(data);
                    ChatService.addMessage(message.UserName, message.Text);
                    $rootScope.$apply();
                },
                'UpdateMessagesInDialog': function (data) {
                    var messages = data.Messages;
                    ChatService.allMessages = [];
                    for (var i = 0; i < messages.length; i++) {
                        ChatService.addMessage(messages[i].SenderName, messages[i].Text, messages[i].FormatedSendedAt);
                    }
                    $rootScope.$apply();
                },
                'receiveDialogConfig': function (data) {
                    var config = JSON.parse(data);
                    ChatService.dialogId = config.DialogId;
                    ChatService.agent = config.Agent;
                    $rootScope.$apply();
                }
            },
            methods: ['onNewClient', 'onNewMessage', 'onEndDialog'],
            errorHandler: function (error) {
                console.error(error);
            },
            hubDisconnected: function () {
                if (hub.connection.lastError) {
                    hub.connection.start();
                }
            },

            logging: true
        });

/*----------------------------------------------- Обмен данными ---------------------------------------*/
        var SendLocation = function () {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {

                    latitude = position.coords.latitude;
                    longitude = position.coords.longitude;
                    DataService.postLocation(latitude, longitude);
                });
            }
        }

/*------------------------------------------- Завершение диалога ---------------------------------------*/
        ChatService.EndChat = function () {
            hub.onEndDialog();
        }

        return ChatService;
    }]);