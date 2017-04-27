define ['lodash'], (_)->
  return ($window)->
    theService = {}
    obj = Object
    defineProperty = _.bind obj.defineProperty, null, theService
    enumerable = enumerable: true
    keys = [
      'value'
      'validFor'
      'type'
    ]

    for key in keys
      do (key)->
        theProperty =
          _.extend {
          get: -> $window.localStorage[key]
          set: (value)-> $window.localStorage[key] = value }, enumerable
        defineProperty key, theProperty
        return

    defineProperty 'clear', value: ->
      for key in keys
        delete $window.localStorage[key]
      return
    
    timeStampKey = 'timeStamp'

    defineProperty timeStampKey,
      _.extend { value: ->
        $window.localStorage[timeStampKey] = Date.now()
        @
      } , enumerable
    
    defineProperty 'isStale', get: ->
      timeStamp = $window.localStorage[timeStampKey]
      return false unless timeStamp

      now = Date.now()
      return (now - timeStamp) >= @validFor
    
    defineProperty 'isLoggedIn', get: ->
      @value? and @type? and @validFor? and not @isStale

      
    return theService
