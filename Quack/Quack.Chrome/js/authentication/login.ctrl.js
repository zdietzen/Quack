angular.module('app').controller('LoginController', function ($scope, authService, apiUrl) {

    $scope.authExternalProvider = function (provider) {

        var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

        var externalProviderUrl = apiUrl + "api/Account/ExternalLogin?provider=" + provider
                                                                    + "&response_type=token&client_id=374089372061-g28ovvr2b4pps4gurf9aluohu3727c0b"
                                                                    + "&redirect_uri=" + redirectUri;
        window.$windowScope = $scope;

        var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
    };

    $scope.$on('event:google-plus-signin-success', function (event, authResult) {
        authService.getUserInfoFromGoogle(authResult.access_token).then(function (data) {
            authSer 
        }, function (error) {
            console.log(error);
        });
    });
    $scope.$on('event:google-plus-signin-failure', function (event, authResult) {
        alert('fail');
    });
    $scope.googleLoginCallback = function (token) {
        alert('Google login callback');
    };

    $scope.onGoogleSignIn = function (googleUser) {
        alert('Google sign in');
        var id_token = googleUser.getAuthResponse().id_token;
    };
});