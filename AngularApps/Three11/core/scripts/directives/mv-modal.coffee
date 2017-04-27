# Directive wrapping bootstrap modal functionality
#
# Author: Markus Westerholz
# Date: 2012/12/12
#
# Copyright © 2014 MediaValet, Inc.
# All rights reserved
'use strict'

define [
  'lodash'
  'core/util/props'
], (
  _
  defineProperties
)->($injector)->
  $modalService = $injector.get '$modal'
  $log = $injector.get '$log'
  logError = (e)->$log.error e

  theDirective =
    restrict: 'A'
    scope:
      id: '@mvModal'
      activeClass: '@'
      onModalAccept: '&'
      onModalDismiss: '&'
    transclude: true
    replace: true
    template: '<button data-ng-transclude data-ng-click=";open()"/>'
    compile:(element, attributes)->
      
      extractInstanceData = ($injector.get '$parse') attributes.mvModalData
      
      createModalData = (scope, config, attributes)->
        configData = config.data
        instanceData = extractInstanceData(scope.$parent)

        if configData? and instanceData?
          configData = _(configData)
          instanceData = _(instanceData)

          if configData.isObject() and instanceData.isObject()
            return (configData.extend instanceData.value()).value()
          else if configData.isObject()
            return configData.extend(instance: instanceData.value()).value()
          else if instanceData.isObject()
            return instanceData.extend(config: configData.value()).value()
          else
            return config: configData.value(), instance: instanceData.value()
        else if configData?
          return configData
        else
          return instanceData


      createScopeIfData = (scope, config, attributes)->
        modalData = createModalData scope, config, attributes
        if modalData?
          cscope = scope.$new()
          cscope.modalData = modalData
          return cscope


      ($scope, element, attributes)->
        _isOpen = false
        _modalConfig = _($injector.get "#{$scope.id}Modal").cloneDeep()

        reset = ->
          element.removeClass $scope.activeClass if $scope.activeClass
          _isOpen = false
          return


        defineProperties $scope,
          isOpen: get:-> _isOpen
          open: value:->
            return if _isOpen
            _modalConfig.scope = createScopeIfData $scope, _modalConfig, attributes
            _isOpen = true
            element.addClass $scope.activeClass if $scope.activeClass

            onAccept = (result)->
              try
                $scope.onModalAccept $result: result
              catch e
                logError e
              finally
                reset()
              return

            onDismiss= (reason)->
              try
                $scope.onModalDismiss $reason: reason
              catch e
                logError e
              finally
                reset()
              return

            $modalService.open _modalConfig
              .result.then onAccept, onDismiss
