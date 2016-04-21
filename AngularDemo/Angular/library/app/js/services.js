var services = angular.module("bookServices", []);

//这里用于写服务,加载书籍列表在这里进行
services.factory("loadBookListService", function ($http) {
    var doRequest = function (param) {
        return $http.get("/handlers/Books.ashx?action=GetBooks&type=" + param);
    };

    return {
        BookList: function (param) {
            return doRequest(param);
        }
    }
});

