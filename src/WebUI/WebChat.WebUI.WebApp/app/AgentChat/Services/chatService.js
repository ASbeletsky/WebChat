chatApp.factory('ChatService',
    ["$rootScope", "Hub", "DataService",
    function ($rootScope, Hub, DataService) {
        var baseSiteUrlPath= $("base").first().attr("href")
        var ChatService = this;
/*---------------------------------------------------------- Появление учасников ----------------------------------*/
        ChatService.ConnectNewAgent = function () {
            hub.promise.done(function () {
                var appId = DataService.getAppInfo().AppId;
                hub.onConnectAgent(appId);
            });
        };

/*-------------------------------------------- Все диалоги -----------------------------------*/
//Private:
        var _dialogs = [];

        var getDialogById = function(dialogId){
            for (var i = 0; i < _dialogs.length; i++) {
                if (_dialogs[i].DialogId === dialogId) {
                    return _dialogs[i];
                }
            }
        };

//Public:
        ChatService.getDialogs = function () {            
            return _dialogs;
        }

        ChatService.setDialogs = function (dialogs) {
            _dialogs = [];
            if($.isArray(dialogs)){
                for (var i = 0; i < dialogs.length; i++) {
                    _dialogs.push(new Dialog(dialogs[i].DialogId, dialogs[i].Client, dialogs[i].AppId));
                }
            }
            else {
                _dialogs.push(new Dialog(dialogs.DialogId, dialogs.Client, dialogs.AppId));
            }

            if (_dialogs.length > 0) {
                if (_active_dialog_id === undefined) {
                    _active_dialog_id = _dialogs[0].DialogId;
                }
                $('#wait-clients').hide();
            }
            else{
                $('#wait-clients').show();
            }

            ChatService.setActiveDialog(_active_dialog_id);
        }

/*-------------------------------------------- CRUD с диалогами -----------------------------------*/

        ChatService.AddDialog = function (id, client, appId) {
            _dialogs.push(new Dialog(id, client, appId));
        }

        ChatService.RemoveDialog = function (id) {
            var dailog = getDialogById(id);
            var index = _dialogs.indexOf(dailog);
            _dialogs.splice(index, 1);
        }

/*-------------------------------------------- Активный диалог -------------------------------------*/
//Private:
        var _active = undefined;
        var _active_dialog_id = undefined;

//Public:
        ChatService.getActiveDialog = function () {
            return _active;
        }

        ChatService.setActiveDialog = function (id) {
            _active_dialog_id = id;
            _active = getDialogById(id);           
        }              

/*----------------------------------------------- Клиенты -------------------------------------------------*/
             
        function Client(id, name, photoUrl) {
            this.ClientId = id;
            this.Name = name;
            this.PhotoUrl = photoUrl;
        };

/*----------------------------------------------- Диалог ---------------------------------------------------*/
        
        function Dialog(id, client, appKey) {
            this.DialogId = id;
            this.Client = client;
            this.AppKey = appKey;

            var _messages = [];
            this.getMessages = function () {
                if (_messages.length === 0)
                    hub.onAllMessageInDialog(this.DialogId);
                return _messages;
            };

            this.AddMessage = function (sender, message, time) {
                _messages.push(new ChatMessage(sender, message, time));
            };
            this.SendMessage = function (MessageText) {
                var message = {
                    AppKey: _active.AppKey,
                    DialogId: _active.DialogId,
                    Text: MessageText
                };
                hub.onNewMessage(message);
            };
        };

/*------------------------------------------------ Сообщение ----------------------------------------*/

        function ChatMessage(client, message, time) {
            this.Client = client,
            this.Message = message
            this.Time = time;
        };

/*-----------------------------------------------Настройки Хаба -------------------------------------*/     
        
        var hub = new Hub("chatHub", {
            listeners: {
                'RegisterAsAgent': function(){
                    ChatService.ConnectNewAgent();
                },
                'addNewMessageToPage': function (data) {
                    var message = JSON.parse(data);
                    var currentDialog = getDialogById(message.DialogId);
                    currentDialog.AddMessage(message.UserName, message.Text, message.Time);
                    $rootScope.$apply();
                },
                'addDialog': function (data) {
                    var dialog = JSON.parse(data);
                    ChatService.AddDialog(dialog.DialogId, dialog.Client, dialog.AppId);
                    location = location.href;
                },
                'UpdateDialogs': function (data) {                    
                    if (data instanceof Array) {
                        var dialogs = [];
                        for (var i = 0; i < data.length; i++) {
                            dialogs.push(JSON.parse(data[i]));
                        }
                    } else {
                        var dialogs = JSON.parse(data);
                    }                   
                    ChatService.setDialogs(dialogs);
                    $rootScope.$apply();
                    animate_menu();
                },
                'RemoveDialog' : function(dialogId){
                    ChatService.RemoveDialog(dialogId);
                    window.location = baseSiteUrlPath + 'agent-dashboard'
                },
                'UpdateMessagesInDialog': function (data) {
                    var currentDialog = getDialogById(data.DialogId);
                    var messages = data.Messages;
                    for (var i = 0; i < messages.length; i++) {
                        currentDialog.AddMessage(messages[i].SenderName, messages[i].Text, messages[i].FormatedSendedAt);
                    }
                    $rootScope.$apply();
                },
            },
            methods: ['onConnectAgent', 'onNewMessage', 'onAllMessageInDialog', 'onGetClientInfo'],
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

        return ChatService;
}]);