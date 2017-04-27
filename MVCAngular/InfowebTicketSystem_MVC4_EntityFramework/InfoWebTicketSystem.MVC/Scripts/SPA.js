
      var ticketApp = angular.module('ticketApp', ['ngRoute']);
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
.when('/contact', {
    templateUrl: 'pages/contact.html',
    controller: 'contactController'
});
});

//============================================================================================






// I act a repository for the remote friend collection.
ticketApp.service(
"indexService",
function ($http, $q) {

    // Return public API.
    return ({
        addFriend: addFriend,
        getFriends: getFriends,
        removeFriend: removeFriend,
        getLoginViewModel: getLoginViewModel,
        loginUser: loginUser

    });


    // ---
    // PUBLIC METHODS.
    // ---

    // getLoginViewModel
    function getLoginViewModel() {

        var request = $http({
            method: "get",
            url: "/ViewModalFactory/GetLoginViewModel",
            params: {
                action: "get"
            }
        });

        return (request.then(handleSuccess, handleError));
    }

    // Login 
    function loginUser(_loginViewModel) {

        var request = $http({
            method: "post",
            url: "/Account/Login",
            params: {
                action: "add"
            },
            data: {
                model: _loginViewModel
            }
        });

        return (request.then(handleSuccess, handleError));
    }


    // I add a friend with the given name to the remote collection.
    function addFriend(name) {

        var request = $http({
            method: "post",
            url: "api/index.cfm",
            params: {
                action: "add"
            },
            data: {
                name: name
            }
        });

        return (request.then(handleSuccess, handleError));
    }


    // I get all of the friends in the remote collection.
    function getFriends() {

        var request = $http({
            method: "get",
            url: "api/index.cfm",
            params: {
                action: "get"
            }
        });

        return (request.then(handleSuccess, handleError));
    }


    // I remove the friend with the given ID from the remote collection.
    function removeFriend(id) {

        var request = $http({
            method: "delete",
            url: "api/index.cfm",
            params: {
                action: "delete"
            },
            data: {
                id: id
            }
        });

        return (request.then(handleSuccess, handleError));

    }

    function handleError(response) {

        // The API response from the server should be returned in a
        // nomralized format. However, if the request was not handled by the
        // server (or what not handles properly - ex. server error), then we
        // may have to normalize it on our end, as best we can.
        if (
        !angular.isObject(response.data) ||
        !response.data.message
        ) {

            return ($q.reject("An unknown error occurred."));

        }

        // Otherwise, use expected error message.
        return ($q.reject(response.data.message));

    }


    // I transform the successful response, unwrapping the application data
    // from the API response payload.
    function handleSuccess(response) {
        return (response.data);
    }

}
);

//=============================================================================================================================
// create the controller and inject Angular's $scope
ticketApp.controller('mainController', function ($scope, indexService) {
    $scope.rightPanelUrl = "/index/LoginPanel";
    // $scope.LoginViewModel = friendService.getLoginViewModel();
    indexService.getLoginViewModel()
        .then(
             function (data) {
                 $scope.LoginViewModel = data;
                 //    alert($scope.LoginViewModel.UserName);
             }
        );
    $scope.LoginUser = function () {
        indexService.loginUser($scope.LoginViewModel).then(
             function (data) {
                 if (data == "true") {
                     $scope.rightPanelUrl = '/index/LogOutPanel';
                 }
             }
        );

    }
});




ticketApp.controller('newTicketController', function ($scope) {
    $scope.message = 'Look! I am an about page.';
});

ticketApp.controller('contactController', function ($scope) {
    $scope.message = 'Contact us! JK. This is just a demo.';
});

