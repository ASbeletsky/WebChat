app.factory('ChartService', ["$rootScope", "DataService", function ($rootScope, DataService) {
    var ChartService = this;

    ChartService.ChatDurationGraph = function (appId, startDate, endDate) {
        return DataService.GetChatDuration(appId, startDate, endDate);
    }

    return ChartService;
}]);