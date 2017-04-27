# Configurable service that allows clients to register to be notified when the
# server sends a message ti clients
#
# Author: Markus Westerholz
# Date: 2015/01/08
#
# Copyright © 2014 MediaValet, Inc.
# All rights reserved
'use strict'
define [
  'lodash'
  'core/util/capitalize'
  'core/util/create-observer-container'
  'core/util/props'
  'core/util/prop'
  'core/util/assert'
],(
  _
  capitalize
  createObserverContainer
  defineProperties
  defineProperty
  Assert
)->(config)->(startPolling)->
  
  _observers = {}
  _errorObservers = {}
  _polls = {}

  prepareObserversAndStart = (funcKey, id)->
    bindNotify = (col)-> _(col[id].notify).bind(col).value()
    notifySuccess = bindNotify _observers
    notifyFailure = bindNotify _errorObservers
    _polls[id] = startPolling funcKey, notifySuccess, notifyFailure
    return

  registerForStatusUpdate =(funcKey, id)->(onSuccess, onError) ->
    if onSuccess?
      detach = _observers[id].append onSuccess

    if onError?
      detach = do (detach, onError)->
        detachError = _errorObservers[id].append onError
        if detach?
          _.compose detach, detachError
        else
          detachError

    prepareObserversAndStart funcKey, id  unless _(_polls).has id
    return ->
      detach()
      hasObs = (col, id)-> col[id]? and not col[id].isEmpty
      unless ((hasObs _observers, id) or (hasObs _errorObservers, id))
        # invoking this will stop polling and return enable function
        # -> Stop polling if no more observers
        _polls[id]?()
        delete _polls[id]
    return

  appendIfQuery = (obj, funcKey, id)->
    
    if startPolling.isValidQuery funcKey
            
      extFuncKey = "of#{capitalize id}"
      
      Assert.false _.has obj, extFuncKey
      
      defineProperty obj, extFuncKey,
        enumerable: true,
        configurable: true
        value: registerForStatusUpdate funcKey, id

      _observers[id] = createObserverContainer()
      _errorObservers[id] = createObserverContainer()

    return obj

  theService = {}
  defineProperties theService,
    isTracking: value: (resourceKey)-> _polls[resourceKey]?
    init: value: ->
      _(config).reduce appendIfQuery, theService
      return
    clear: value: ->
      _(@).keys().each ((key)-> delete @[key]), @
      _observers = {}
      _errorObservers = {}
      _(_polls).forIn (cancel)->
        cancel()
        return
      _polls = {}
      return


