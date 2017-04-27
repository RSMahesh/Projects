'use strict'

define [
  'angular'
  'router'
  'uibootstrap'
  'restangular'
  'core/modules/cryptojs'
], (ng)->
  # base names space for the app
  appNamespace = 'mvWebUi'

  # namespace for packages to inject service modules
  servicesNamespace = "#{appNamespace}.services"
  ng.module servicesNamespace,[]

  # namespace for packages to inject filter modules
  filtersNamespace = "#{appNamespace}.filters"
  ng.module filtersNamespace,[]

  # namespace for packages to inject directive modules
  directivesNamespace  = "#{appNamespace}.directives"
  ng.module directivesNamespace ,[]

  # namespace for packages to inject data modules (models)
  dalNamespace = "#{appNamespace}.data"
  ng.module dalNamespace ,[]

  # namespace for packages to inject error handler modules
  errorsNamespace = "#{appNamespace}.errors"
  ng.module errorsNamespace ,[]

  # namespace for packages to inject controller modules
  controllersNamespace = "#{appNamespace}.controllers"
  ng.module controllersNamespace,[
    servicesNamespace
    dalNamespace
  ]

  # namespace to hold app-wide constants
  constNamespace = "#{appNamespace}.constants"
  ng.module constNamespace, []

  # instantiate main module, injecting tool and namespace depenecies
  ng.module appNamespace, [
    'ui.router'
    'ui.bootstrap'    
    'restangular'
    'CryptoJS'
    servicesNamespace
    filtersNamespace
    directivesNamespace
    dalNamespace
    controllersNamespace
    constNamespace
    errorsNamespace
  ]
