chatApp.factory('AuthService', ["$http", '$rootScope', 'DataService',
function ($http, $rootScope, DataService) {
    var authService = this;
    var baseSiteUrlPath = $("base").first().attr("href");

    var userName = "Вы";
    authService.UserName = userName;
    authService.LoginErrors = []

    authService.Login = function (name, email) {
        userName = name;
        if (email != undefined) {
            var appId = DataService.getAppInfo().AppId;
            $http({
                method: 'POST',
                url: baseSiteUrlPath + 'ClientAccount/EmailLogin',
                data: { appId: appId, userName: name, userEmail: email }
            })
            .success(function (response) {
                if (response.result == 'Redirect') {
                    window.location = response.url;
                }
                if (response.result == 'InvalidLogin') {
                    authService.LoginErrors = response.errors;
                    $rootScope.apply();
                }
            })
        }
    };

    var promise = null;
    authService.getExternalAuthHtml = function () {
        if (promise) {
            return promise;
        }
        promise = $http({ method: 'GET', url: baseSiteUrlPath + 'ClientAccount/AuthPage' })
            .then(function (response) {
                return response.data;
            });
        return promise;
    };

    return authService;
}]);