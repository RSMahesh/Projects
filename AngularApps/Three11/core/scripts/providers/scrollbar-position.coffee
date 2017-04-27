# Cache to keep track of the position of a particular scrollbar
#
# Author: Markus Westerholz
# Date: 2015/04/27
#
# Copyright © 2015 MediaValet, Inc.
# All rights reserved
'use strict'

define [
  'lodash'
  'core/util/props'
  'core/util/assert'
],(
  _
  props
  Assert
)->

  # Updates the postion of the Scrollbar model for 'key', when invoked
  
  createPositionUpdater = (key, scrollbar, $rootScope, $timeout)->
    _current = undefined

    setFractionScrolled = ->
      if _current?
        scrollbar.setFractionScrolled key, _current
        $rootScope.$apply()
      return

    updateFractionScrolled = ->
      _current = scrollbar.getFractionScrolled key
      $timeout setFractionScrolled, 0
      return

  provider = ->
    _observablesConfig = undefined

    serviceCtor = [
      '$timeout'
      '$rootScope'
      '$injector'
      ($timeout, $rootScope, $injector)->
        _theModel = {}
        _scrollPosition = {}
        _maxScrollValue = undefined

        # see comments for '@on'

        _.each _observablesConfig, (def)->

          observer =
            $injector.get def.observer

          callback =
            createPositionUpdater def.key, _theModel, $rootScope, $timeout
          
          observer.on def.evtName, callback
          
          return
        
        props _theModel,

          clear: value: (id)->
            if id?
              delete _scrollPosition[id]
            else
              _scrollPosition = {}

          getPosition: value: (id) -> _scrollPosition[id]?['pos'] or 0

          setPosition: value: (id, position) ->
            _scrollPosition[id] = {} unless _scrollPosition[id]?
            _scrollPosition[id]['pos'] = position
            return @

          setMaxScrollValue: value: (id, value) ->
            Assert _.isNumber value
            _maxScrollValue = value
          
          getFractionScrolled: value: (id) ->
            if(_scrollPosition[id] and _maxScrollValue and _maxScrollValue > 0)
              return _scrollPosition[id].pos / _maxScrollValue
            return 0

          setFractionScrolled: value: (id, fraction) ->
            return 0 unless _scrollPosition[id]? and _maxScrollValue?
            _theModel.setPosition(id, _maxScrollValue * fraction)
    ]

    props @,
    
      # Registers an event for a specific scrollbar - whenever the event is
      # invoked, the positon for that scollbar is updated
      #
      # @scrollbarId: The id of the scrollbar to update
      # @observerId:  The id of the observerable that raises the event
      # @eventKey:    The name of the function of the observerable for which to
      #              raise the callback
      #
      # @returns: the provider instance
      # Example:
      #
      #   app.provider 'scrollbarPostionsProvider', (provider)->
      #
      #     # When the 'zoomIn' function of the 'zoomModel' service instance
      #     # is called, this service will refresh the scrollbar position for
      #     # the 'browseAssetsScrollbar' instance
      #
      #     provider.on 'browseAssetsScrollbar', 'zoomModel', 'zoomIn'
      #     provider.on 'browseAssetsScrollbar', 'zoomModel', 'zoomOut'
      #     return
    
      on: value: (scrollbarId, observerId, eventKey)->
        _observablesConfig = _observablesConfig or []
        _observablesConfig.push
          key: scrollbarId
          observer: observerId
          evtName: eventKey
        @

      $get: value: serviceCtor

    return
