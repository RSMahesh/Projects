# The modal state model tracks a simgle value to track, if a particular modal
# should be visible, or not. It implements the observer pattern to notify
# interested parties (e.g. controllers in charge of opening particular instance
# of modals) when a state change occurss
#
# Author: Vibhakar Pathak, Markus Westerholz
# Date: 2014/07/22

define ->
  # Checks if the observers container contains a member property for the
  # stateName
  #
  # observers: The object representing a collection of observers
  # stateName: The name of the state for which to check
  #
  # returns 'true' iff observers defines a member for the stateName

  hasObserversListForState = ( observers, stateName )->
    return observers and observers.hasOwnProperty stateName

  # Notifies all observers iterested in the current state that that state
  # has been entered
  notifyObservers = (observers, state, data) ->
    if hasObserversListForState observers, state
      for func in observers[state]
        func(data)
    return
  return ->
    _modalState = undefined
    _observers = {}

   # Resets the modal state
    clear: ->
      _modalState = undefined
      return @

    # Gets the state of the currently expected modal state
    get: ->
      return _modalState

    # Returns true if a list of observers already exists for a
    # given state
    hasState: (stateName) ->
      return _observers[stateName]?

    # Sets the state of the currently expected modal state. Calling this method
    # will invoke ObserverNotification with any additional data needed by
    # the modal.
    #
    # state: the state to set
    # data: the data being forwarded
    changeState: (state, data)->
      _modalState = state
      notifyObservers _observers, _modalState, data
      return @

    # registers an observer for the particular stateName.
    # stateName: a string identifyng the name of the state for which to invoke
    #            the action
    # action: a function with signature 'void func()' to invoke
    onModalStateChange: (stateName, action) ->
      if not hasObserversListForState _observers, stateName
        _observers[stateName] = []
      _observers[stateName].push action
      return @
