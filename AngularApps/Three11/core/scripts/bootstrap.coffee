'use strict'
define [
 'angular',
 'lodash',
 'core/config',
 'core/myalert'
 'core/app',
 'core/routerConfig',
 'core/constants-main',
 'core/controllers-main',
 'core/directives-main',
 'core/models-main',
 'core/services-main'
],(
  ng
  _
  Config
  myAlert)-> 
  ng.module 'mvWebUi'
  .constant 'Config', Config
  .run [ 'Restangular', 'Config', (Restangular, Config)->
    Restangular.setBaseUrl Config.api
    Restangular.setDefaultHeaders 'Content-Type': 'application/json'
  ]
 
  require [
    'core/_packageloader'
  ]
  return