var app = angular.module('StatisticApp');

app.controller('appStatisticController', ['$scope', '$routeParams', 'ChartService',
    function AppStatisticController($scope, $routeParams, ChartService) {

        $scope.labels = ["January", "February", "March", "April", "May", "June", "July"];
        $scope.series = ['Series A', 'Series B'];
        $scope.data = [
          [65, 59, 80, 81, 56, 55, 40],
          [28, 48, 40, 19, 86, 27, 90]
        ];
        $scope.onClick = function (points, evt) {
            console.log(points, evt);
        };

        $scope.graph = ChartService.ChatDurationGraph(5, new Date(2015, 10, 12), new Date(2015, 11, 13));
}]);