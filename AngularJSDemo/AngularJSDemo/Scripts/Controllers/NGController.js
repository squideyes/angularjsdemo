
var app = angular.module('myApp', ['ngGrid']);

app.controller('MyCtrl', ['$scope', '$http', function ($scope, $http, $apply) {
    $scope.items = [];
    $scope.SortDirection = "-";
    $scope.filterText = "";
    
    // filter
    $scope.filterOptions = {
        filterText: "",
        useExternalFilter: false
    };

    // paging
    $scope.totalServerItems = 0;
    $scope.pagingOptions = {
        pageSizes: [5,10,25, 50, 100],
        pageSize: 5,
        currentPage: 1
    };

    // sort
    $scope.sortOptions =
        {
        fields: ["Id"],
        directions: ["asc"]
    };

    // grid
    $scope.gridOptions =
        {
        data: "items",
        columnDefs: [
            { field: "Id", displayName: "ID", width: "10%" },
            { field: "Module", displayName: "Module", width: "30%" },
            { field: "LogLevel", displayName: "Log Level", width: "30%" },
            { field: "UserName", displayName: "Username", width: "20%" },
            { displayName: 'Actions', cellTemplate: 
             '<div class="grid-action-cell">'+
             '<a ng-click="$event.stopPropagation(); ShowDetails(row.entity.Id);" href="#">Details</a></div>'
            }
            
        ],
            
        enablePaging: true,
        enablePinning: false,
        pagingOptions: $scope.pagingOptions,
        //filterOptions: $scope.filterOptions,
        keepLastSelected: true,
        multiSelect: false,
        showColumnMenu: false,
        //showFilter: true,
        showGroupPanel: false,
        showFooter: true,
        sortInfo: $scope.sortOptions,
        totalServerItems: "totalServerItems",
        useExternalSorting: true,
        enableSorting: true,
        i18n: "en"
    };


    $scope.ShowDetails= function (ID)
    {
        $http({
            url: "/odata/LogItems("+ID+")",
            method: "GET"
        }).success(function (data, status, headers, config)
        {
            $('#myModal').modal({
                show: 'true'
            });
            $scope.Module = data.Module;
            $scope.LoggedOn = data.LoggedOn;
            $scope.LogLevel = data.LogLevel;
            $scope.UserName = data.UserName;
            $scope.Message = data.Message;

        }).error(function (data, status, headers, config) {
            alert(JSON.stringify(data));
        });

    };

    $scope.selectGridRow = function () {
        if ($scope.selectedItem[0].total != 0) {
            $location.path('items/' + $scope.selecteditem[0].id);
        }
    };

    $scope.refresh = function ()
    {
        var sb = [];
        for (var i = 0; i < $scope.sortOptions.fields.length; i++)
        {
            sb.push($scope.sortOptions.directions[i] === "desc" ? "+" : "-");
            sb.push($scope.sortOptions.fields[i]);
        }
        
        var p =
            {
                name: $scope.filterText,
                pageNumber: $scope.pagingOptions.currentPage,
                pageSize: $scope.pagingOptions.pageSize,
                sortInfo: sb.join("")
            };

        $http({
            url: "/odata/LogItems",
            method: "GET",
            params: p
        }).success(function (data, status, headers, config) {
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.value;

        }).error(function (data, status, headers, config) {
            alert(JSON.stringify(data));
        });

        setTimeout(function ()
        {
            
        }, 100);
    };

    // watches
    $scope.$watch('pagingOptions', function (newVal, oldVal) {
        if (newVal !== oldVal)
        {
            
            $scope.refresh();
        }
    }, true);

    $scope.$watch('filterOptions', function (newVal, oldVal)
    {
        if (newVal !== oldVal)
        {
            
            $scope.refresh();
        }
    }, true);

    $scope.$watch('sortOptions', function (newVal, oldVal)
    {
        if (newVal !== oldVal)
        {
            $scope.refresh();
        }
    }, true);

    $scope.refresh();
}]);