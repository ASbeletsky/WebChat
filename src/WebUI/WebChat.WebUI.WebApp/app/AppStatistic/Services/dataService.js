app.factory('DataService', ['$http', function ($http) {
    var DataService = this;

    /*----------------------------------- REST ---------------------------------------*/

    var get = function (controller, action, id) {
        var items = $http(
            {
                method: 'GET',
                url: '/api/' + controller + '/' + action + '/' + id
            })
             .then(function (response) {
                 return response.data;
             });
        return items;
    };

    var post = function (controller, action, data) {
        $http({
            method: 'POST',
            url: '/api/' + controller + '/' + action,
            data: data
        });
    }

    /*---------------------------------------------------------------------------------------*/

    DataService.GetChatDuration = function (appId, startDate, endDate) {
        var params = {
                appId: appId,
                startDate: startDate,
                endDate: endDate
            }
        return post('Statistic', 'ChatDuration', params)
    }

    return DataService;
}]);