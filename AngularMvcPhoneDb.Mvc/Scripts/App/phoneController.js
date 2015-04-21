﻿app.controller('phoneController', function($scope, $document, $http) {

    $scope.phoneFormDisplayable = false;
    $scope.data = null;
    $scope.genErr = null;
    $scope.phoneDisplayable = false;

    $scope.phone = {
        manu: "",
        model: "",
        cpu: "",
        gpu: "",
        pixelWidth: 0,
        pixelHeight: 0,
        battCap: 0
    };

    $scope.displayForm = function() {
        $scope.phoneFormDisplayable = true;
        $scope.phoneDisplayable = false;
    }

    $scope.displayPhone = function(id) {

        $scope.phoneFormDisplayable = false;
        $scope.phoneDisplayable = true;

        $http.get('/api/data/' + id).success(function(data, status) {
            $scope.selectedPhone = data;
        });
    }

    function getData() {

        $http.get('/api/data', { cache: false }).success(function(data, status) {
            $scope.data = data;
        });
    }

    $scope.addPhone = function() {

        $scope.phoneDisplayable = false;
        if ($scope.addPhoneForm.$pristine) {

            $scope.genErr = "Please enter some data";
            return;
        }

        $scope.genErr = null;

        if ($scope.addPhoneForm.$invalid) return;

        $http.post('/api/data', $scope.phone); //TODO: error handling
        getData();
        $scope.phoneFormDisplayable = false;
    }

    $document.ready(function() {
        getData();
    });
});

app.directive('addPhoneFormTemplate', function () {

    return {
        templateUrl: '/templates/addPhoneForm.html'
    };
});

app.directive('displayPhoneFormTemplate', function () {

    return {
        templateUrl: '/templates/displayPhoneForm.html'
    };
});
