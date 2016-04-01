var bookStoreApp = angular.module("bookStoreApp", ['ngRoute', 'ngAnimate', 'bookStoreCtrls', 'bookStoreFilters', 'bookStoreServices', 'bookStoreDriectives']);
//var bookStoreApp = angular.module("bookStoreApp", ['ngRoute', 'ngAnimate', 'bookStoreCtrls']);

//注册路由
bookStoreApp.config(function ($routeProvider) {
    $routeProvider.when('/Hello', {
        templateUrl: "/App/tpls/Hello.html",
        controller: "HelloCtrl"
    }).when('/Books', {
        templateUrl: "/App/tpls/BookList.html",
        controller: "BookListCtrl"
    }).otherwise({
        redirectTo: "/Hello"
    });

});