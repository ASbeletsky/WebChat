chatApp.factory('ChatService',
    ["$rootScope", "Hub", "customerAppData", '$location', 'DataService',
    function ($rootScope, Hub, customerAppData, $location, DataService) {
        var baseSiteUrlPath = $("base").first().attr("href");

        var ChatService = this;
            ChatService.dialogId = 0;
            ChatService.agent = { Name: 'AgentX', Rank: 'Support Agent' };

/*----------------------------------------------- Начало диалога ----------------------------------------*/
            ChatService.ConnectNewClient = function () {
                hub.promise.done(function () {
                    customerAppData.getAppKey().then(function (appKey) {
                        hub.onNewClient(appKey);
                    });
                });
            };
/*------------------------------------------------ Все сообщения -----------------------------------------*/
            ChatService.allMessages = [];

            ChatService.addMessage = function (userName, chatMessage) {
                ChatService.allMessages.push(new MessageModel({
                    UserName: userName,
                    ChatMessage: chatMessage
                }));
            }
/*-------------------------------------------------- Сообщение ------------------------------------------*/

            var MessageModel = function (chatApp) {
                if (!chatApp) chatApp = {};
                var MessageModel = {
                    UserName: chatApp.UserName,
                    ChatMessage: chatApp.ChatMessage,
                }
                return MessageModel;
            }

            ChatService.sendNewMessage = function (MessageText) {
                customerAppData.getAppKey().then(function (appKey) {
                    var message = {
                        AppKey: appKey,
                        DialogId: ChatService.dialogId,
                        Text: MessageText
                    };
                    hub.onNewMessage(message);
                });
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
                        ChatService.addMessage(messages[i].UserName, messages[i].Text);
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