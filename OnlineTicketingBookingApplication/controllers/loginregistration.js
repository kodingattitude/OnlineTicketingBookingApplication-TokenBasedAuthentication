
angpp.controller('loginregistrationController',
    ['$scope', '$http', 'serviceBasePath', '$rootScope', '$window', '$state', '$sessionStorage', '$stateParams',
function ($scope, $http, serviceBasePath, $rootScope, $window, $state, $sessionStorage, $stateParams) {
    //if (!$sessionStorage.IsLogin) {
        $sessionStorage.IsLogin = false;
        $rootScope.IsLogin = false;
        $sessionStorage.LoginUserName = "";
        sessionStorage.LoginUserId = "";
    //}
    $scope.TCCheckBox = false;
    $scope.Login = function () {
        $scope.ErrorMessage = "";
        if ($scope.loginform.$valid) {
            //var data = $.param({UserName:$scope.LoginUserName,Password:$scope.LoginPassword});
            //$http.get(serviceBasePath + '/api/LoginRegister/Login?' + data).then(function (response) {
            //    $sessionStorage.IsLogin = true;
            //    $rootScope.IsLogin = true;
            //    $sessionStorage.LoginUserName = $scope.LoginUserName;
            //    $rootScope.LoginUserName = $scope.LoginUserName;
            //    $scope.data = response.data;
            //    $scope.RegisterErrorMessage = $scope.data;
            //    //alert($scope.data);
            //    if ($scope.data.indexOf("Successfull") != -1) {
            //        $state.go('reservation');
            //    }
                

            //}, function (error) {
            //    $scope.isDisabled = false;
            //})
            //{ 'Content-Type': 'application/x-www-form-urlencoded' }
            //// Token Based Authentication
             var requestbody = $.param({ 'username': $scope.LoginUserName, 'password': $scope.LoginPassword, 'grant_type': 'password' });
           // var requestbody = { username: $scope.LoginUserName, password: $scope.LoginPassword, grant_type: 'password' };
             $http.post(serviceBasePath + '/token', requestbody,{'Content-Type':'application/x-www-form-urlencoded'}).then(function (response) {
                 var data = response.data;
                 $scope.RegisterErrorMessage = $scope.data;
                 if (response.data.access_token) {
                $sessionStorage.access_token = response.data.access_token;
                $sessionStorage.IsLogin = true;
                    $rootScope.IsLogin = true;
                    $sessionStorage.LoginUserName = $scope.LoginUserName;
                    $rootScope.LoginUserName = $scope.LoginUserName;
                    $scope.data = response.data;
                    //alert($scope.data);
                        $state.go('reservation');
                    }
            })

        }

    }
    $scope.ClearRegisterForm = function () {

        $scope.Registerform.$setPristine();
        $scope.Registerform.$setUntouched();
        $scope.Registerform.$submitted = false;
        $scope.RegisterUserName = '';
        $scope.RegisterEmail = '';
        $scope.RegisterMobile = '';
        $scope.RegisterPassword = '';
        //$scope.Registerform.TCCheck = true;
        //$scope.TCCheck = false;

        $scope.loginform.$setPristine();
        $scope.loginform.$setUntouched();
        $scope.loginform.$submitted = false;
        $scope.LoginUserName = '';
        $scope.LoginPassword = '';

    }
    $scope.CheckBoxClick = function (value) {
        if (value) {
            $scope.TCCheckBox ? $scope.TCCheckBox = false : $scope.TCCheckBox = true;
        }

    }
    $scope.iaccept = function () {
        $scope.TCCheckBox = true;
    }
    $scope.Register = function () {
        console.log('register');
        $scope.RegisterErrorMessage = "";
        $scope.RegisterSubmitted = true;
        if ($scope.Registerform.$valid) {
            $scope.isDisabled = true;
            var requestBody = {
                "UserName": $scope.RegisterUserName, "Password": $scope.RegisterPassword, "Email": $scope.RegisterEmail, "Mobile": $scope.RegisterMobile
            }
            $http.post(serviceBasePath + '/api/LoginRegister/Register', requestBody).then(function (response) {
                $scope.data = response.data;
                $scope.RegisterErrorMessage = $scope.data;
                alert($scope.data);
                if ($scope.data.indexOf("Successfully") != -1) {
                    $window.location.reload();
                    $state.go('login', { UserId: null });
                }
                else {
                    $scope.isDisabled = false;
                }

            }, function (error) {
                $scope.isDisabled = false;
            })
        }


    }

   

  

}]);


