var Medusa = angular.module("Medusa", ['ngRoute', 'ngResource', 'ngAnimate']);
Medusa.controller("HomeController", function ($scope) {
    $scope.hello = "MEDUSA Manager";
});
