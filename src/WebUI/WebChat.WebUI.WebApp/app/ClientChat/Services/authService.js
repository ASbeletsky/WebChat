chatApp.factory('AuthService', ["$http", '$rootScope', 'customerAppData',
function ($http, $rootScope, customerAppData) {
    var authService = this;
    var baseSiteUrlPath = $("base").first().attr("href");

    var userName = "Вы";
    authService.UserName = userName;


    

    authService.LoginErrors = []

    authService.Login = function (name, email) {
        userName = name;
        if (email != undefined)
        {   
            customerAppData.getAppKey().then(function (appKey) {
                $http({
                    method: 'POST',
                    url: baseSiteUrlPath + '/Client/EmailLogin',
                    data: { appKey: appKey, userName: name, userEmail: email }
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
            }); 
        }       
    };

    var promise = null;
    authService.getExternalAuthHtml = function () {
        if (promise) {
            return promise;
        }
        promise = $http({ method: 'GET', url: baseSiteUrlPath + '/Client/AuthPage' })
            .then(function (response) {
                return response.data;
            });
        return promise;
    };

    return authService;
}]);