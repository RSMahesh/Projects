# The ModalController attaches a function to the scope to open a modal
# for each 'action' found in the passed in modal options.
#
# Author: mwesterholz
# Date: 2014/07/07
#
# Available options for opening the $modalInstance are:
# templateUrl - a path to a template representing modal's content
# template - inline template representing the modal's content
# scope - a scope instance to be used for the modal's content.Defaults to $rootScope
# controller - a controller for a modal instance - it can initialize scope used by modal.
# controllerAs - an alternative to the controller-as syntax, matching the API of directive definitions.
# resolve - members that will be resolved and passed to the controller as locals;
#           it is equivalent of the resolve property for AngularJS routes
# backdrop - controls presence of a backdrop. Allowed values: true (default), false (no backdrop),
#           'static' - backdrop is present but modal window is not closed when clicking outside of the modal window.
# keyboard - indicates whether the dialog should be closable by hitting the ESC key, defaults to true
# backdropClass - additional CSS class(es) to be added to a modal backdrop template
# windowClass - additional CSS class(es) to be added to a modal window template
# windowTemplateUrl - a path to a template overriding modal's window template
# size - optional size of modal window. Allowed values: 'sm' (small) or 'lg' (large).
'use strict'

define ->
  return ($scope, modalStateModel, mvModalService, options, stateNamePrefix) ->
    capitalize = (s) -> s[0].toUpperCase() + s[1..]

    for actionName, optionValue of options
      # do included here, since the actionName changes each iteration
      # of the loop, hence the modalStateModel.set's reference to
      # 'actionName' would also change. Same for the 'optionValue'.
      do (actionName, optionValue) ->
        stateName = stateNamePrefix + capitalize(actionName)
        unless modalStateModel.hasState(stateName)
          modalStateModel.onModalStateChange(stateName, -> mvModalService.openModal optionValue)

        $scope[actionName] = (data) ->
          modalStateModel.changeState stateName, data

    $scope.result = ->
      return mvModalService.result()
