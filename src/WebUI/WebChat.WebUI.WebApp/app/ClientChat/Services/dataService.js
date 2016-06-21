
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

    DataService.getAppInfo = function(){
        return {
            AppId : localStorage.getItem("webChatAppId")
        }
    };
   
    return DataService;
}]);


