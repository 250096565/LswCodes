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

    //设置值
    $scope.setPagingData = function (data, page, pageSize) {
        var pagedData = data.slice((page - 1) * pageSize, page * pageSize);
        $scope.books = pagedData;
        $scope.totalServerItems = data.length;
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };
    //看看现在传的什么
    console.log($stateParams);

    $scope.getPagedDataAsync = function (pageSize, page, searchText) {
        setTimeout(function () {
            var _data;
            if (searchText) {
                //如果有值则搜索
                var ft = searchText.toLowerCase();
                loadBookListService.BookList($stateParams.bookType).success(function (data, status) {
                    _data = data.Data.filter(function (item) {
                        return JSON.stringify(item).toLowerCase().indexOf(ft) != 1;
                    });
                });


                $scope.setPagingData(_data, page, pageSize);
            } else {
                loadBookListService.BookList($stateParams.bookType).success(function (data, status) {
                    _data = data.Data;
                });
                $scope.setPagingData(_data, page, pageSize);
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
            enableCellEdit: true
        }, {
            field: 'Author',
            displayName: '作者',
            enableCellEdit: true,
            width: 220
        },
        {
            field: 'Explain',
            displayName: '描述',
            enableCellEdit: true,
            width: 220
        },
        {
            field: 'BookId',
            displayName: '操作',
            enableCellEdit: false,
            sortable: false,
            pinnable: false,
            cellTemplate: '<div><a ui-sref="bookdetail({bookId:row.getProperty(col.field)})" id="{{row.getProperty(col.field)}}">详情</a></div>'
        }],
        enablePaging: true,
        showFooter: true,
        totalServerItems: 'totalServerItems',
        pagingOptions: $scope.pagingOptions,
        filterOptions: $scope.filterOptions
    };
});

