var app = angular.module("Myapp", []);
/*app.run(function ($templateCache) {
    $templateCache.put('tpls/hello.html', "<div>你好</div>");
}
);*/

/*app.directive('hello', function ($templateCache) {
    return {
        restrict: "AE",
        transclude:true,
        template: $templateCache.get("tpls/hello.html")

    }
});*/

app.directive("hello", function () {
    return {
        restrict: "AE",
        transclude: true,
        templateUrl: "tpls/hello.html"

    }
});
