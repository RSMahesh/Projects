# Service to download and expose current user information
# Author: Stanley Shen
# Date: 2015/03/03
#
# Copyright © 2015 MediaValet, Inc.
# All Rights Reserved

'use strict'

define [
  'lodash'
  'core/util/props'
  'core/util/strip-properties'
  'core/util/make-observable'
], (
  _
  props
  stripProps
  makeObservable
)-> (queryService, $log)->

  _activePromise = undefined
  _data = undefined

  theService = {}
  observableService = undefined

  onSuccess = (data)->
    _activePromise = undefined
    data = if _.isArray data then (_.first data) else data
    observableService.assign data
    return

  onError = (reason)->
    _activePromise = undefined
    theService.clear()
    $log?.error reason
    return

  fetchUserInfo = ->
    unless _activePromise
      theService.clear()
      _activePromise = queryService.usersCurrent()
      _activePromise.then onSuccess, onError
    return _activePromise

  clear = ->
    _activePromise?.cancel?()
    _activePromise = undefined
    _data = undefined
    stripProps theService
    return

  assign = (user)->
    @clear()
    _data = user
    _.assign theService, _data
    @

  observableService = makeObservable props theService,
    hasData: value: -> not _.isEmpty theService
    init: value: fetchUserInfo
    clear: value: _.bind clear, theService
    clone: value: -> _data?.clone()
    assign: enumerable: true, value: _.bind assign, theService

  #Storing original "on" function in private variable.
  $on = observableService.on
  delete observableService.on

  props observableService,
    onAssign: value: _.bind $on, makeObservable, 'assign'
