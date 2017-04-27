ticketApp.config(function ($routeProvider) {
    $routeProvider
    .when('/', {
        templateUrl: '/index/IndexPartail',
        controller: 'mainController'
    })
    .when('/submit_ticket', {
        templateUrl: '/NewTicket/SelectDepartment',
        controller: 'newTicketController'
    })
    .when('/submit_select_department', {
        templateUrl: '/NewTicket/CollectTicketDetail',
        controller: 'newTicketController'
    })
    .when('/submit_confirmation', {
        templateUrl: '/NewTicket/SubmitConfirmation',
        controller: 'newTicketController'
    })
    .when('/contact', {
        templateUrl: 'pages/contact.html',
        controller: 'contactController'
    });
});
