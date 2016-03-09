(function () {
    var chatApp = angular.module('chatApp', ['ngRoute', 'SignalR']);

    chatApp.config(function ($routeProvider, $locationProvider) {

        var baseSiteUrlPath = $("base").first().attr("href");
        var baseTemplateUrl = baseSiteUrlPath + "app/ClientChat/Views/";

        $routeProvider
            .when('/chat/auth',
                {
                    templateUrl: baseTemplateUrl + 'AuthView.html',
                    controller: 'authController'
                })
            .when('/chat',
                {
                    templateUrl: baseTemplateUrl + 'ChatView.html',
                    controller: 'chatController'
                })
            .when('/chat/WaitAgent',
                {
                    templateUrl: baseTemplateUrl + 'AgentNotFoundView.html',
                    controller: 'waitAgentController'
                })
            .when('/chat/EndChat',
                {
                    templateUrl: baseTemplateUrl + 'EndChatView.html',
                    controller: 'endChatController'
                })
            .otherwise({
                redirectTo: function () {

                    if (window.location.pathname == baseSiteUrlPath + "chat") {
                        window.location = baseSiteUrlPath + "chat/index";
                    }
                }
            });

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    });
   
})();