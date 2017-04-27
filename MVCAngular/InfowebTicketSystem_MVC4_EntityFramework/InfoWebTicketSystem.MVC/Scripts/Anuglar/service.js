// I act a repository for the remote friend collection.
ticketApp.service("baseService", baseService);
ticketApp.service("indexService", indexService);
ticketApp.service("newTicketService", newTicketService);



function baseService($q) {

    return ({
        handleError: handleError,
        handleSuccess: handleSuccess,
    });

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



//=======================================================================
function indexService($http, $q, baseService) {

    return ({
        addFriend: addFriend,
        getFriends: getFriends,
        removeFriend: removeFriend,
        getLoginViewModel: getLoginViewModel,
        loginUser: loginUser

    });


    // PUBLIC METHODS.
    // getLoginViewModel
    function getLoginViewModel() {

        var request = $http({
            method: "get",
            url: "/ViewModalFactory/GetLoginViewModel",
            params: {
                action: "get"
            }
        });

        return (request.then(baseService.handleSuccess, baseService.handleError));
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

        return (request.then(baseService.handleSuccess, baseService.handleError));
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

        return (request.then(baseService.handleSuccess, baseService.handleError));
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

        return (request.then(baseService.handleSuccess, baseService.handleError));
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

        return (request.then(baseService.handleSuccess, baseService.handleError));

    }
}

//==========================================================================


function newTicketService($http, $q, baseService) {

    // Return public API.
    return ({
        getSelectDepartmentViewModel: getSelectDepartmentViewModel,
        selectDepartment: selectDepartment,
        getCollectTicketDetailViewModal: getCollectTicketDetailViewModal,
        submitTicketDetail: submitTicketDetail

    });


    // ---
    // PUBLIC METHODS.
    // ---
    function getSelectDepartmentViewModel() {

        var request = $http({
            method: "get",
            url: "/ViewModalFactory/GetSelectDepartmentViewModel",
            params: {
                action: "get"
            }
        });

        return (request.then(baseService.handleSuccess, baseService.handleError));
    }
    function selectDepartment(_selectDepartmentViewModel) {
        var request = $http({
            method: "post",
            url: "/NewTicket/SelectDepartment",
            params: {
                action: "add"
            },
            data: {
                model: _selectDepartmentViewModel
            }
        });

        return (request.then(baseService.handleSuccess, baseService.handleError));
    }

    function getCollectTicketDetailViewModal() {

        var request = $http({
            method: "get",
            url: "/ViewModalFactory/GetCollectTicketDetailViewModal",
            params: {
                action: "get"
            }
        });

        return (request.then(baseService.handleSuccess, baseService.handleError));
    }
    function submitTicketDetail(_collectTicketDetailViewModal) {

        var request = $http({
            method: "post",
            url: "/NewTicket/CollectTicketDetail",
            params: {
                action: "add"
            },
            data: {
                model: _collectTicketDetailViewModal
            }
        });

        return (request.then(baseService.handleSuccess, baseService.handleError));
    }

}


