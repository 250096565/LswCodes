var app = angular.module('superHome', []);

/*app.controller("superCtrl", function ($scope) {
    $scope.loadData = function () {
        console.log("这是我的超能力");
    }
});
app.controller("superCtrl2", function ($scope) {
    $scope.loadData2 = function () {
        console.log("这是我的超能力-2");
    }
});

app.directive("superPower", function () {
    return {
        restrict: "AE",
        template: "<div>算天算地算自己</div>",
        link: function (scope, element, attr) {
            element.bind("mouseenter", function () {
                scope.$apply(attr.power);
            });
        }
    };
})*/

app.directive("superPower", function () {
    return {
        scope: {},
        controller: function ($scope) {
            $scope.powers = [];
            this.addp1 = function () {
                $scope.powers.push("吃饭");
            };
            this.addp2 = function () {
                $scope.powers.push("睡觉");
            };
            this.addp3 = function () {
                $scope.powers.push("写代码");
            };
        },
        link: function (scope, element, attr) {
            element.addClass("btn btn-primary");
            element.bind("mouseenter", function () {
                console.log(scope.powers);
            });
        }
    }
});

app.directive("super1", function () {
    return {
        require: "^superPower",
        link: function (scope, element, attr, superPowerCtrl) {
            superPowerCtrl.addp1();
        }
    }
})
app.directive("super2", function () {
    return {
        require: "^superPower",
        link: function (scope, element, attr, superPowerCtrl) {
            superPowerCtrl.addp2();
        }
    }
})
app.directive("super3", function () {
    return {
        require: "^superPower",
        link: function (scope, element, attr, superPowerCtrl) {
            superPowerCtrl.addp3();
        }
    }
})