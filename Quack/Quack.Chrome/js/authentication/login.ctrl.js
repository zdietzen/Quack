﻿angular.module('app').controller('LoginController', function ($scope) {
   
    $scope.googleLoginCallback = function (token) {
        alert('Google login callback');
    };

    $scope.onGoogleSignIn = function (googleUser) {
        alert('Google sign in');
        var id_token = googleUser.getAuthResponse().id_token;
    };
});