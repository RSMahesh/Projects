# The createModalActionControllers utility method creates all of the 'action controllers'
# for a given feature's models.
#
# Each of these action controllers define the actions that can be performed on a particular
# model, and what happens when they're performed (opening the modal).
#
# The models passed in are defined like so:
#    <modelName>:
#      <Action1>: <options>
#      <Action2>: <options>
#       ...
#      <ActionN>: <options>
#
# Author: Sterling Graham
# Date: 2014/09/14
#
# Copyright � 2014 VRX Studios Inc.

define [
  'core/controllers/modal'
], ->
  return (constantsModule, controllersModule, featureName, models) ->
    for modelName, modelConfig of models
      capitalizedModelName = modelName[0].toUpperCase() + modelName[1..]
      modalOptionsName = "#{featureName}#{capitalizedModelName}ModalOptions"
      controllerName = "#{featureName}#{capitalizedModelName}ModalController"

      modalStateNamePrefix = "#{featureName}#{capitalizedModelName}"
      stateId = "#{featureName}#{capitalizedModelName}State"
      do (modalOptionsName, controllerName) ->
        constantsModule.constant stateId, modalStateNamePrefix
        constantsModule.constant modalOptionsName, modelConfig
        controllersModule.controller controllerName, [
          '$scope'
          'modalStateModel'
          'mvModalService'
          modalOptionsName
          stateId
          require 'core/controllers/modal'
          ]
    return
