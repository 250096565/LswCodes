var bookStoreCtrls = angular.module('bookStoreCtrls', []);

bookStoreCtrls.controller('HelloCtrl', function ($scope) {
    $scope.name = "LSW";
});

bookStoreCtrls.controller('BoolListCtrl', function ($scope) {
    $scope.books = [
        { title: "围城", author: "钱钟书" },
        { title: "嫌疑人X的献身", author: "东野圭吾" },
        { title: "白夜行", author: "东野圭吾" },
        { title: "山海经", author: "郭璞" }
    ];
});