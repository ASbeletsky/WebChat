(function () {
    var chatApp = angular.module('chatApp', ['ngRoute', 'SignalR']);

    chatApp.config(function ($routeProvider, $locationProvider) {

        var baseSiteUrlPath = $("base").first().attr("href");
        var baseTemplateUrl = baseSiteUrlPath + "app/AgentChat/Views/";

        $routeProvider
            .when('/agent-dashboard/dialog/:id/', {
                templateUrl: baseTemplateUrl + 'ChatView.html',
                controller: 'chatController'
            })
            .when('/agent-dashboard/client', {
                templateUrl: baseTemplateUrl + 'ClientView.html',
                controller: 'clientController'
            })
            .otherwise({
                redirectTo: function () {

                    if (window.location.pathname == baseSiteUrlPath + "agent-dashboard") {
                        window.location = baseSiteUrlPath + "agent/index";
                    }
                }
            });

            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });
        });

})();