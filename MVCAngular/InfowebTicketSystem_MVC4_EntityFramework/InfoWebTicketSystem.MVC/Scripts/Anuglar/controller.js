
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


ticketApp.controller('newTicketController', function ($scope, $location,$cacheFactory, newTicketService) {
    //alert('newTicketController');
 

    $scope.getSelectDepartmentViewModel = function () {
        newTicketService.getSelectDepartmentViewModel()
           .then(
                function (data) {
                    $scope.SelectDepartmentViewModel = data;
                    //console.log($scope.SelectDepartmentViewMode);
                }
           );
    }

    $scope.selectDepartment = function () {
        console.log($scope.SelectDepartmentViewModel);
        newTicketService.selectDepartment($scope.SelectDepartmentViewModel).then(
             function (data) {
                 if (data == "true") {
                  //   $scope.getCollectTicketDetailViewModal();
                     $location.path("/submit_select_department");
                     console.log($location.path());
                 }
             }
        );
    }



    $scope.getCollectTicketDetailViewModal = function () {
           newTicketService.getCollectTicketDetailViewModal()
        .then(
             function (data) {
                 $scope.CollectTicketDetailViewModal = data;
                 console.log($scope.CollectTicketDetailViewModal);
             }
        );
    }
    $scope.submitTicketDetail = function () {
        console.log($scope.CollectTicketDetailViewModal);
        newTicketService.submitTicketDetail($scope.CollectTicketDetailViewModal).then(
             function (data) {
                 console.clear();
                 console.log(data);
                 var cache = $cacheFactory('myCache');
                 cache.put("SubmitConfirmationViewModal", data);
                 console.log(cache.get("SubmitConfirmationViewModal"));
                 $scope.SubmitConfirmationViewModal = data;
                 $location.path("/submit_confirmation");
             }
        );
    }



  //  alert($location.path());

    switch ($location.path()) {
        case "/submit_ticket":
            $scope.getSelectDepartmentViewModel();
            break;
        case "/submit_select_department":
            $scope.getCollectTicketDetailViewModal();
            break;
        case "/submit_confirmation":
            //alert("yyyyyyyyyyyyy");
            $scope.TestProp = "xyzww";
            alert($cacheFactory)
            alert(2);
            var cache123 = $cacheFactory('myCache');
            alert(cache123);
         //   $scope.SubmitConfirmationViewModal = cache.get("SubmitConfirmationViewModal");
            break;

    }

});

ticketApp.controller('contactController', function ($scope) {
    $scope.message = 'Contact us! JK. This is just a demo.';
});

