# Loads and registeres all services for the core package
# Author: Vibhakar Pathak
# Date: 2014/07/22
#
# Copyright � 2014 VRX Studios Inc.
# All Rights Reserved
'use strict'
define [
  'angular'
  'lodash'
  'core/app'
  'core/services/query'
  'core/services/auth-token'
  'core/services/app-director'
  'core/services/long-polling'
  'core/services/status-tracker'
  'core/services/http-errors-handler'
  'core/providers/wrapper'
  'core/util/create-event'
  'core/services/current-user'
], (
  ng
  _
  app
  createQueryService
  createAuthToken
  createAppDirectorService
  createPollingAlgorithm
  createStatusTrackerService  
  createHttpErrorsHandlerService
  createProviderWrapper
  createEvent
  createCurrentUserService
)->
  services = ng.module 'mvWebUi.services'
  
  # Container services to allows packages to register their queries as needed
  queryService = 'queryService'
  services.service queryService, createQueryService

  # Auth Token to store and retrieve login authentication information
  authToken = 'authToken'
  services.service authToken, [
    '$window'
    createAuthToken
  ]
  
  loginNotifier= 'loginNotifier'
  services.service loginNotifier, [
    '$log'
    createEvent loginNotifier
   ]
  # Observers can register to be notified when logout is requested
  logoutNotifier = 'logoutNotifier'
  services.service logoutNotifier, [
    '$log'
    authToken
    queryService
    ($log, authToken, queryService)->
      notifier = createEvent(logoutNotifier) $log
      notifier  ->
        authToken.clear()
        queryService.clear()
        return
      return notifier
  ]

  appReady = 'appReady'
  services.service appReady, ['$log', createEvent appReady ]

  # Creates a service function, which is used by other services such as the
  # StatusTracker service to connect to the API for puh notifications
  pollingAlgorithm = 'pollingAlgorithm'
  services.service pollingAlgorithm, createPollingAlgorithm
 
  currentUser = 'currentUserService'
  services.service currentUser, [
    'queryService'
    '$log'
    createCurrentUserService
  ]

  # AppDirector manages flow between packages. E.g. it handles post
  # login redirection to the default main package or to the refferer page
  app.provider 'appDirector', createAppDirectorService
  
  # A service used e.g. in the  mvStatusTracker directive to receive push
  # notifications when status updates are posted by the API
  .provider 'statusTracker',
    createProviderWrapper [
      pollingAlgorithm
      createStatusTrackerService
  ]
  .provider 'httpErrorInterceptor', createHttpErrorsHandlerService
  .config ['$httpProvider', (provider)->
    provider.interceptors.unshift 'httpErrorInterceptor'
    return
  ]
  return

