# Creates an object to manage observers in a hash table
#
# Author: Markus Westerholz
# Date: 2014/11/01
#
# Copyright © VRX Studios, Inc.
# All rights reserved

define ['lodash', 'core/util/assert'], (_, Assert) ->
  return ($log)->
    theContainer = {}
    # TODO: replace numeric ids with hash ids instead - very small potential
    # for (very) long running instances to override old IDs
    _id = -1

    # functor to be returned when registering observer functions. Handles
    # removing registered observers on demand
    detach  = (id)->
      delete theContainer[id]
      return

    Object.defineProperties theContainer,
      # Handles registration of a function as an observer
      # @pre _.isFunction func
      # @returns returns a function to be used to detach and observer function
      append:
        value:(func)->
          Assert.true (_.isFunction func), 'function expected'
          theContainer[++_id] = func
          return _.bind detach, null, _id
      
      # @returns returns 'true' if the container instance has no callbacks
      #          registered
      isEmpty: get: -> _.isEmpty theContainer

      # Handles notification of all observers registered with 'this' instance
      # @note any arguments are forwarded to the observer functions
      notify:
        value: ->
          args = Array.prototype.slice.call arguments
          _.forIn theContainer, (func)->
            try
              func.apply null, args
            catch err
              $log?.debug "About to throw error: #{err}"
              throw err

    return theContainer
