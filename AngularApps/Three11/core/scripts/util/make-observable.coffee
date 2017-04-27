# Wraps an object and makes specified functions observable
#
# Author: Markus Westerholz
# Date: 2014/11/01
#
# Copyright © VRX Studios, Inc.
# All rights reserved
'use strict'
define [
  'lodash'
  'core/util/create-observer-container'
  'core/util/assert'
], (_, createObserverContainer, Assert)->

  defineProp = Object.defineProperty
  
  return (obj, $log)->
    _obs = {}
    theObservable = Object.create obj
    
    notify = (key, args) ->
      
      unless args?
        args = [obj]
      else
        args = [args]
      
      _obs[key].notify.apply null, args
      return

    createPropertyValue = (key)->
      prop = obj[key]
      if _.isFunction prop
        value =
          value: ->
            args =  Array.prototype.slice.call arguments
            result = prop.apply obj, args
            notify key, result
            return result
      else
        value =
          get: -> return obj[key]
          set: (value)->
            unless obj[key] == value
              obj[key] = value
              notify key
            return value
      return value

    appendProperty = (key)->
      defineProp theObservable, key,
        _.extend {configurable: true, enumerable: true}, createPropertyValue key
      return
    
    # Extends the observer containers functionality by also deleting empty
    # containers
    detach = (key, d)->
      d()
      if _obs[key].isEmpty
        delete _obs[key]
        delete theObservable[key]
      return

    defineProp theObservable, 'on',
      configurable: true
      value: (key, func)->
        Assert.true key of obj
        _obs[key]= createObserverContainer $log unless _.has _obs, key
        # if the observable already has the key as own property, we skip
        # re-assignment
        appendProperty(key) unless _.has theObservable, key
        return _.bind detach, null, key, (_obs[key].append func)

    return theObservable

