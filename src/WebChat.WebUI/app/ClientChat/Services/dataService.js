
chatApp.factory('DataService', ['$http', function ($http) {
    var DataService = this;

/*----------------------------------- REST ---------------------------------------*/

    var get = function (controller, action) {
        var items = $http(
            {
                method: 'GET',
                url: '/api/' + controller + '/' + action
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
/*---------------------------------------------------------------------------------*/
    DataService.postLocation = function (x, y) {
        var coordinates = { latitude: x, longitude: y };
        post('Client', 'SetLocation', coordinates);
    };

    return DataService;
}]);

chatApp.factory('customerAppData', function ($http, $q) {
    var promise = null;
    return {
        getAppKey: function () {
            if (promise) {
                return promise;
            }
            var appId = localStorage.getItem("webChatAppId");
            promise = $http({ method: 'GET', url: '/api/CustomerApp/Key/' + appId  })
                .then(function (response) {
                    return response.data;
                });
            return promise;
        }
    }
});

