var routeApp = angular.module('RouteApp', ['ui.router']);
//$stateProvider, $urlRouterProvider
routeApp.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/index');
    $stateProvider
        .state('index', {
            url: '/index',
            views: {
                '': {
                    templateUrl: 'tpls/index.html'
                },
                'topbar@index': {
                    templateUrl: 'tpls/topbar.html'
                },
                'main@index': {
                    templateUrl: 'tpls/main.html'
                }
            }
        }).state('index.recommendRead', {
            url: '/recommendRead',
            views: {
                'main@index': {
                    templateUrl: 'tpls/recommendRead.html',
                    controller: function ($scope, $state) {
                        $scope.addBook = function () {
                            $state.go("index.recommendRead.addBook");
                        }
                    }
                }
            }
        }).state('index.recommendRead.tuiL', {
            url: '/tuiL',
            templateUrl: 'tpls/books/tuiL.html'
        }).state('index.recommendRead.wenX', {
            url: '/wenX',
            templateUrl: 'tpls/books/wenX.html'
        }).state('index.recommendRead.wangL', {
            url: '/wangL',
            templateUrl: 'tpls/books/wangL.html'
        }).state('index.recommendRead.addBook', {
            url: '/addBook',
            templateUrl: 'tpls/addBook.html',
            controller: function ($scope, $state) {
                $scope.backToPrevious = function () {
                    window.history.back();
                }
            }
        }).state('index.about', {
            url: '/about',
            views: {
                'main@index': {
                    templateUrl: 'tpls/about.html'

                }

            }
        }).state('index.xiaos', {
            url: '/xiaos',
            views: {
                'main@index': {
                    templateUrl: 'tpls/xiaos.html'

                }

            }
        }).state('index.following', {
            url: '/following',
            views: {
                'main@index': {
                    templateUrl: 'tpls/following.html'

                }

            }
        });


})