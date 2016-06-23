chatApp.factory('DataService', ['$http', function ($http) {
    var DataService = this;

/*----------------------------------- REST ---------------------------------------*/

    var get = function (controller, action, param) {
        var items = $http(
            {
                method: 'GET',
                url: '/' + controller + '/' + action + '/' + param
            })
             .then(function (response) {
                 return response.data;
             });
        return items;
    };

    var post = function (controller, action, data) {
        $http({
            method: 'POST',
            url: '/' + controller + '/' + action,
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

    DataService.getAppInfo = function () {
        return {
            AppId: $('#application-id').val()
        }
    };

return DataService;
}]);


