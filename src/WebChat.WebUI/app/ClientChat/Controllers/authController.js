var chatApp = angular.module('chatApp');

chatApp.controller('authController', ["$scope", "$sce", "AuthService",
function AuthController($scope, $sce, AuthService) {

    $scope.Login = function () {
        AuthService.Login($scope.UserName, $scope.UserEmail)
    };

    $scope.$watch(function () { return AuthService.LoginErrors }, function (newVal, oldVal) {
        if (typeof newVal !== 'undefined') {
            $scope.Errors = AuthService.LoginErrors;
        }
    });

    //external login page
    AuthService.getExternalAuthHtml().then(function (html) {
        $scope.authExternalView = $sce.trustAsHtml(html);
    });
}]);

