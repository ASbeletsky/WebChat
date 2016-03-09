(function () {
    var app = angular.module('StatisticApp', ['ngRoute', 'chart.js']);

    app.config(function ($routeProvider, $locationProvider) {

        var baseSiteUrlPath = $("base").first().attr("href");
        var baseTemplateUrl = baseSiteUrlPath + "app/AppStatistic/Views/";

        $routeProvider
            .when('/statistic/app/:id/', {
                    templateUrl: baseTemplateUrl + 'AppStatisticView.html',
                    controller: 'appStatisticController'
                })            
            .otherwise({
                redirectTo: function () {
                    if (window.location.pathname == baseSiteUrlPath + "statistic") {
                        window.location = baseSiteUrlPath + "statistic/index";
                    }
                }
            });

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    });

})();