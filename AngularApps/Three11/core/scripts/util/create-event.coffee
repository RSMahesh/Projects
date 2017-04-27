# Helper script to create event service.
#
# # Create an event service:
#
#   createEvent = require 'core/util/create-event'
#   myEvent = 'myEvent'
#   module.service myEvent, ['$log', createEvent myEvent]
#
#
# # To register observers of the event:
#
#   controllers.controller 'observingController', [
#     '$scope'
#     'myEvent'
#     ($scope, myEvent)->
#
#       onMyEvent = (data)->
#         unless data?
#           # You don't have to send data at all
#           $scope.message = "Message received"
#
#         else if _.isFunction data
#           # You can send functions on notifications, which you can chooser
#           # to handle in any way you like.
#           # Here, I chose to invoke the send function on #scope
#           "Function received, Result: #{data.call $scope}"
#         else
#           # You can send any kind of data and handle, store, process that
#           # data as you choose.
#           $window.alert "Message: #{JSON.stringify data}"
#         return
#
#       # Finally, we register event
#       detach = myEvent onMyEvent
#
#       # and make sure we detach when the current scrope is destroyed
#       $scope.$on '$destroy', detach
#
#       return
#   ]
#
# # To raise an event:
#
#   controllers.controller 'notifier', [
#     '$scope'
#     'myEvent'
#     ($scope, myEvent)->
#       Object.defineProperties $scope,
#
#         sendEmptyMessage:
#           value:->
#             myEvent()
#
#         sendMessageWithData:
#           value:->
#             myEvent "Hello from notifier"
#
#         sendMessageWithFunction:
#           value:->
#             func = -> @message = "Hello from function send from notifier"
#             # If you want to send a function to be invoked, you must set
#             # the second argument - forwardFunction - to 'true'. Otherwise,
#             # you are simply registering a new observer
#             myEvent func, true
#
#      return
#   ]
#
# Author: Markus Westerholz
# Date: 2014/11/28
#
# Copyright © 2014 MediaValet, Inc.
# All rights reserved

define [
  'core/util/make-observable'
  'lodash'
], (
  makeObservable
  _
) ->
  return (name)->
    name = 'anonymous event' unless name?
    ($log)->
      _observable = makeObservable (event: ->
        $log?.log "<#{name}> raised at #{new Date().toISOString?()}"
        return arguments[0] if arguments.length > 0 and arguments[0]?), $log
 
      (data, forwardFunction)->
        isFunction = _.isFunction data
        unless isFunction and not forwardFunction
          _observable.event data
        else
          _observable.on 'event', data
