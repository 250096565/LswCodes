var bookListmodule = angular.module("BookListModule", []);

//这里是控制器.模块化式开发

/**
 * 这里是书籍列表模块
 * @type {[type]}
 */
bookListmodule.controller("BookListCtrl", function ($scope, $http, $state, $stateParams, loadBookListService) {
    //不知道这是干啥的
    $scope.filterOptions = {
        filterText: "",
        useExternalFilter: true
    };
    //这是总数吧
    $scope.totalServerItems = 0;
    $scope.pagingOptions = {
        pageSizes: [5, 10, 20],
        pageSize: 10,
        currentPage: 1
    };

    //设置值
    $scope.setPagingData = function (data, page, pageSize) {
        var pagedData =data.slice((page - 1) * pageSize, page * pageSize);
        $scope.books = pagedData;
        $scope.totalServerItems = data.length;

        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };


    $scope.getPagedDataAsync = function (pageSize, page, searchText) {
        setTimeout(function () {
            var _data;
            if (searchText) {
                //如果有值则搜索
                var ft = searchText.toLowerCase();
                loadBookListService.BookList($stateParams.bookType).success(function (data) {

                    _data = data.Data.filter(function (item) {
                        return JSON.stringify(item).toLowerCase().indexOf(ft) != 1;
                    });
                });


                $scope.setPagingData(_data, page, pageSize);
            } else {
                loadBookListService.BookList($stateParams.bookType).success(function (data) {
                    _data = data.Data;
                    $scope.setPagingData(_data, page, pageSize);
                });
            }
        }, 100);
    };
    //请求
    $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage);

    $scope.$watch('pagingOptions', function (newVal, oldVal) {
        if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
            $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
        }
    }, true);
    $scope.$watch('filterOptions', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
        }
    }, true);

    $scope.gridOptions = {
        data: 'books',
        rowTemplate: '<div style="height: 100%"><div ng-style="{ \'cursor\': row.cursor }" ng-repeat="col in renderedColumns" ng-class="col.colIndex()" class="ngCell ">' +
            '<div class="ngVerticalBar" ng-style="{height: rowHeight}" ng-class="{ ngVerticalBarVisible: !$last }"> </div>' +
            '<div ng-cell></div>' +
            '</div></div>',
        multiSelect: false,
        enableCellSelection: true,
        enableRowSelection: false,
        enableCellEdit: true,
        enablePinning: true,
        columnDefs: [{
            field: 'Index',
            displayName: '序号',
            width: 60,
            pinnable: false,
            sortable: false
        }, {
            field: 'Name',
            displayName: '书名',
            enableCellEdit: true,
            width: 220
        }, {
            field: 'Author',
            displayName: '作者',
            enableCellEdit: true,
            width: 220
        },
        {
            field: 'Time',
            displayName: '时间',
            enableCellEdit: true,
            width: 220
        },
        {
            field: 'Explain',
            displayName: '描述',
            enableCellEdit: true,
            width: 690
        }],
        enablePaging: true,
        showFooter: true,
        totalServerItems: 'totalServerItems',
        pagingOptions: $scope.pagingOptions,
        filterOptions: $scope.filterOptions
    };
});


bookListmodule.controller("addBookCtrl", function ($scope, $http) {

    $scope.book = {
        type: "1"
    }
    $scope.addBook = function () {

        var book = new Object();

        book.Name = $scope.book.name;
        book.Author = $scope.book.author;
        book.Type = $scope.book.type;
        book.Explain = $scope.book.explain;

        $http.post("/handlers/Books.ashx?action=Addbook&name=" + book.Name + "&author=" + book.Author + "&Type=" + book.Type + "&explain=" + book.Explain + "").success(function (data) {
            if (data.Status != "1") {
                alert("服务器繁忙,请稍后再试");
            } else {
                window.location.href = "/Index.html#/Index/0";
            }
        });
    };


});

