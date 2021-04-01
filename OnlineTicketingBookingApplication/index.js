var angpp = angular.module('ticketBookingApp', ['ui.router', 'ui.bootstrap', 'ngStorage', 'ngMaterial', 'ngMessages', 'angular.filter']);
angpp.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
    // $locationProvider.hashPrefix('!');//To Add ! After # in URL
    // $locationProvider.html5Mode(true); //To Remove # from URL
    $urlRouterProvider.otherwise("/login");  //default display page (default routing)
    $stateProvider
    var loginState = {
        name: 'login',
        url: '/login',
        templateUrl: 'views/loginregistration.html',
        controller: 'loginregistrationController'
    }
    $stateProvider.state(loginState);
    var reservationState = {
        name: 'reservation',
        url: '/reservation',
        templateUrl: 'views/reservation.html',
        controller: 'reservationController'
    }
    $stateProvider.state(reservationState);
})