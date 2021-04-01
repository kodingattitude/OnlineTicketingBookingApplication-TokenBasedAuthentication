
angpp.controller('reservationController',
    ['$scope', '$http', 'serviceBasePath', '$rootScope', '$window', '$state', '$sessionStorage', '$stateParams','razorpayKey',
function ($scope, $http, serviceBasePath, $rootScope, $window, $state, $sessionStorage, $stateParams, razorpayKey) {
    $rootScope.LoginUserName = $sessionStorage.LoginUserName;
    $rootScope.IsLogin = $sessionStorage.IsLogin;
    if (!$sessionStorage.IsLogin) {
        $state.go('login');
    }
    $scope.seatprice = 0;
    $scope.IGST = 0;
    $scope.CGST = 0;
    $scope.SGST = 0;
    $scope.selected_seats = [];
    $scope.Seats = [];
    for (var i = 1; i <= 24; i++) {
        $scope.Seats.push({ seat:i, image:'images/bus-seat.jpg' });
    }
    $scope.SelectedSeats = function (seat) {
        seat.price = 100;
        seat.selected = seat.selected ? false : true;
        if (seat.selected) {
            $scope.selected_seats.push(seat.seat);
            $scope.seatprice = parseFloat($scope.seatprice) + parseFloat(seat.price);
            $scope.SGST = parseFloat($scope.SGST) + parseFloat(seat.price) * 9 / 100;
            $scope.CGST = parseFloat($scope.CGST) + parseFloat(seat.price) * 9 / 100;
            $scope.IGST = parseFloat($scope.IGST) + parseFloat(seat.price) * 0 / 100;
        }
        else {
            var index = $scope.selected_seats.indexOf(seat.seat);
            $scope.selected_seats.splice(index, 1);
            $scope.seatprice = parseFloat($scope.seatprice) - parseFloat(seat.price);
            $scope.SGST = parseFloat($scope.SGST) - parseFloat(seat.price) * 9 / 100;
            $scope.CGST = parseFloat($scope.CGST) - parseFloat(seat.price) * 9 / 100;
            $scope.IGST = parseFloat($scope.IGST) - parseFloat(seat.price) * 0 / 100;
        }
        $scope.SelectedSeatsCount = $scope.selected_seats.length;
        $scope.TotalPrice = parseFloat($scope.seatprice) + parseFloat($scope.SGST) + parseFloat($scope.CGST) + parseFloat($scope.IGST);
    }
    $scope.borderstyle = function (seat) {
        if(seat.selected)
            return { "border": "3px solid #b404ae" };
        if(seat.IsDisabled)
        return { "border": "3px solid #FF5733" };
        // return { "--marker-border-top": "7px solid " + color };
    }
    $scope.SubmitSelectedSeats = function () {
       // var InItRazorPay = function () {

            var options = {
                "key": razorpayKey,
                "amount": $scope.TotalPrice * 100, // 2000 paise = INR 20
                "name": "Koding Atittude",
                "description": "Online Bus Ticket Bill Payment",
                "image": "images/65fce720150918013031.jpg",
                "handler": function (response) {

                    $http
                        .post(serviceBasePath + '/api/Reservation/UserReservation', {
                            "PaymentId": response.razorpay_payment_id,
                            "Amount": $scope.TotalPrice * 100,
                            "UserName": $sessionStorage.LoginUserName, "SelectedSeats": $scope.selected_seats
                        }, { headers: { 'Authorization': 'Bearer ' + $sessionStorage.access_token } })

                        .then(function (response) {
                            if (response.data.result) {
                                $scope.data = response.data;
                                $scope.ReservationErrorMessage = $scope.data; 
                            }
                            else {
                            }
                        }, function (error) {
                        });

                },
                "prefill": {
                    "email": 'kodeattitude@gmail.com',
                    "contact": '9985607607',
                    "method": "netbanking"
                },
                "notes": {
                    "address": 'RamachandraKumar',
                },
                "theme": {
                    "color": "#FF5733"
                }
            };
            var rzp1 = new Razorpay(options);

            rzp1.open();
        //}
        //var UserReservation = { UserName: $sessionStorage.LoginUserName, SelectedSeats: $scope.selected_seats }
        //$http.post(serviceBasePath + '/api/Reservation/UserReservation', UserReservation).then(function (response) {
        //    $scope.data = response.data;
        //    $scope.ReservationErrorMessage = $scope.data;        

        //}, function (error) {
        //})
    }
   
    var init = function () {
        $scope.selected_seats = [];
        $scope.IsDisabled = true;
       // $http.get(serviceBasePath + '/api/Reservation/GetUserReservationInfo?UserName=' + $sessionStorage.LoginUserName).then(function (response) {
        $http.get(serviceBasePath + '/api/Reservation/GetUserReservationInfo', { headers: { 'Authorization': 'Bearer ' + $sessionStorage.access_token } }).then(function (response) {
       // $http.get(serviceBasePath + '/api/Reservation/GetUserReservationInfo').then(function (response) {
            $scope.data = response.data;
            $scope.selected_seats = [];
           // $scope.selected_seats = response.data.SelectedSeats;
            response.data.forEach(function (seat) {
                if (seat.selected)
                $scope.selected_seats.push(seat.SelectedSeats);
                $scope.Seats[seat.SelectedSeats - 1].selected = seat.selected;
                $scope.Seats[seat.SelectedSeats - 1].IsDisabled = seat.IsDisabled;
                // $scope.Seats[seat - 1].IsDisabled = 
                $scope.SelectedSeatsCount = $scope.selected_seats.length;
                $scope.seatprice = parseFloat($scope.SelectedSeatsCount) * 100;
                $scope.SGST = parseFloat($scope.seatprice) * 9 / 100;
                $scope.CGST = parseFloat($scope.seatprice) * 9 / 100;
                $scope.IGST = parseFloat($scope.seatprice) * 0 / 100;
                $scope.TotalPrice = parseFloat($scope.seatprice) + parseFloat($scope.SGST) + parseFloat($scope.CGST) + parseFloat($scope.IGST);
            })
        }, function (error) {
        })
    }
    init();
}]);


