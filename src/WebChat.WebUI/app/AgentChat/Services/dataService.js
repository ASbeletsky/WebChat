chatApp.factory('DataService', ['$http', function ($http) {
    var DataService = this;

/*----------------------------------- REST ---------------------------------------*/

    var get = function (controller, action, param) {
        var items = $http(
            {
                method: 'GET',
                url: '/api/' + controller + '/' + action + '/' + param
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
    DataService.GetPhoto = function () {
        return get('client', 'photo');
    }

    DataService.GetClientInfo = function(clientId){
        return get('client', 'info', clientId);
    }

return DataService;
}]);

chatApp.factory('customerAppData', function ($http, $q) {
    var promise = null;
    return {
        getAppKey: function (appId) {
            if (promise) {
                return promise;
            }
            promise = $http({ method: 'GET', url: '/api/CustomerApp/Key/' + appId })
                .then(function (response) {
                    return response.data;
                });
            return promise;
        }
    }
});

