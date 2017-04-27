# Helper service which connects to a long polling resource
#
# Author: Markus Westerholz
# Date: 2015/01/09
#
# Copyright © 2015 MediaValet, Inc.
# All rights reserved
'use strict'
define [
  'lodash'
  'core/constants/http-status'
  'core/util/is-request-cancelled'
],(
  _
  HttpStatus
  isRequestCancelled
)->['queryService', '$timeout', (queryService, $timeout)->
  ReconnectDelayInMs = 4500

  longPolling = (funcKey, onSuccess, onFailure)->
    
    _isEnabled = false
    _isConnected = false
    _hash = undefined
    _activePromise = undefined

    connect = ->
      unless _isConnected
        _isConnected = true
        _activePromise = queryService[funcKey] hash: _hash
        _activePromise.then whenSuccess, whenFailure
      return

    whenSuccess = (result)->
      _isConnected = false
      _activePromise = undefined
      return unless _isEnabled
      _hash = result.hash
      onSuccess? result
      connect()
      return
   
    whenFailure = (reason)->
      _isConnected = false
      _activePromise = undefined
      return unless _isEnabled
      status = reason?.status
      if (HttpStatus[status]? and HttpStatus[status] is HttpStatus['Request Timeout'])
        connect() if _isEnabled
      else if _isEnabled and status <= 0 and not isRequestCancelled reason
        $timeout connect, ReconnectDelayInMs
      else
        onFailure? reason
      return

    enable = ->
      unless _isEnabled
        _isEnabled = true
        _hash = undefined
        connect()
      return cancel
    
    cancel = ->
      _isEnabled = false
      _hash = undefined
      _activePromise?.cancel?()
      _activePromise = undefined
      return enable
    
    return enable()

  
  longPolling.isValidQuery = (key)->
    _(queryService).has(key) and _(queryService[key]).isFunction()
  return longPolling
]
