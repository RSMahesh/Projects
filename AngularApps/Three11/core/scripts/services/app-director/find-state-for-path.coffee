# Scans to a state based on a url's path
#
# Author: Markus Westerholz
# Date: 2015/07/28
#
# Copyright © 2015 MediaValet, Inc.
# All rights reserved
'use strict'

define [
  'lodash'
  'core/util/props'
  'core/util/assert'
], (
  _
  props
  Assert
)->

  IsIgnoredState = Object.freeze
    '': true
    'app': true

  isParameterPlaceholder = (s)-> s?.length > 0 and s[0] is ':'

  createStateWrapper = (parent, state, stateUrlParts, stateParameterValues)->

    if parent?.isSiblingWithSameUrlBase state
      return createStateWrapper(
        parent.parent
        state
        stateUrlParts
        stateParameterValues
      )

    if _.isEmpty stateParameterValues
      stateUrl = state.url
    else
      stateUrl = do ->
        parametersIndex = 0
        _.reduce(
          stateUrlParts
          (acc, stateUrlPart)->

            if _.isEmpty stateUrlPart
              acc
            else
              acc = "#{acc}/" if parent? or not _.isEmpty acc
              if isParameterPlaceholder stateUrlPart
                "#{acc}#{stateParameterValues[parametersIndex++].value}"
              else
                "#{acc}#{stateUrlPart}"
          ''
        ) + (stateParameterValues.query or '')

    props (Object.create state),
      
      # Returns the parent sate, if defined, else returns null
      
      parent:
        get:-> parent
      
      # The fully parsed and extended url to the wrapped state

      url:
        get:-> "#{parent?.url or ''}#{stateUrl}"
      
      # Gets the original state

      raw:
        get:-> state
      
      # The full url to the state without inecting any parameter values

      rawUrl:
        get:-> "#{parent?.rawUrl or ''}#{state.url}"
      
      # Count of subelements in the total path of the wpraeed state, including
      # all parent states

      pathLength:
        get:-> (parent?.pathLength or 0) + stateUrlParts.length

      # Check if @candidateState is a child state of the wrapped state

      isParentOf:
        value: (candidateState)->
          (candidateState.name.indexOf state.name) is 0

      # Sibling states may be defined with similat url. E.g. we might have
      #
      # [
      #   {name: 'gallery.thumbs', url: 'galleries/:id'}
      #   {name: 'gallery.asset-details', url: 'galleries/:id/:asserId'}
      # ]
      #
      # If we check 'gallery.thumbs' first, 'isParentOf' will fail, but we
      # don't want to dismiss the 'gallery' path, so we need to verify that
      # the path doesn't match instead.
      #
      # This method facilitates that check
      
      isSiblingWithSameUrlBase:
        value: (candidateState)->
          (candidateState.url.indexOf @rawUrl) is 0

  
  findStatestUrlPartsIndex = (statesCollection, searchUrl)->

    # Object to track perviously dsimissed parents, which helps us to speed up
    # the search somewhat - if the parentis dismissed as a candidate to match
    # the url, then child states cannot match the url either

    dismissedParents = props {},
      wasParentDismissed: value: (childState)->
        endOfParentStateName = childState?.lastIndexOf '.'
        endOfParentStateName isnt -1 and
          !!dismissedParents[childState.substring 0, endOfParentStateName]

    lastParent = undefined

    currentUrlPartIndex = 0
    urlParts = _.reject (searchUrl.split '/'), _.isEmpty
    parameters = {}

    _.each statesCollection, (state)->
     
      # 1. We cannot compare states without urls, since we are looking by url
      #    for a state
      # 2. ui-router defines a default root state that defines no state.name
      
      return unless (state.url and state.name)

      if not lastParent? or
      (lastParent.isParentOf state) or
      (lastParent.isSiblingWithSameUrlBase state)

        if dismissedParents.wasParentDismissed state.name
           
          # Parent was already flagged, so flag this state, too and move on
          # to the next state

          dismissedParents[state.name] = state
          return
        else
          
          stateUrl = state.url
          stateUrlParts = _.reject (stateUrl.split '/'), _.isEmpty

          if stateUrl.length > 1 and stateUrl[0] is '/'

            # The only reason a state's url would start with a '/' is that
            # it is extending the url of a pareant state - top-level states
            # start w/o a leading '/'
            currentUrlPartIndex = lastParent?.pathLength or 0

            getStatePathLength = -> lastParent.pathLength

          else
            getStatePathLength = -> stateUrlParts.length


          isLast = (index)-> stateUrlParts.length - index is 1

          stateParameterValues = []

          _.each stateUrlParts, (stateUrlPart, index)->

            # Note: the below implies that we cannot have a root state with just
            # an id. E.g. markus.medivalet.com/:id would not work, because
            # that format would apply to any state

            if isParameterPlaceholder stateUrlPart
              do ->

                values =
                  urlParts[currentUrlPartIndex++]

                valuesSplit =
                  values.split '?'

                paramKey =
                  _.first stateUrlPart.split '?'

                stateParameterValues.push
                  key: (_.rest paramKey).join ''
                  value: _.first valuesSplit

                if valuesSplit.length > 1
                  IgnoreChars =
                    '?': true
                    '&': true
                 
                  queryParams =
                    _.reject ((_.last valuesSplit).split /(\?|&)/g), _.isEmpty

                  _.each queryParams, (param)->
                    unless IgnoreChars[param]
                      separatorIndex = param.indexOf '='
                      key = param.substr 0, separatorIndex
                      value = param.substring separatorIndex + 1
                      stateParameterValues.push(
                        key: key, value: value, query: true
                      )
                    return true
                  stateParameterValues.query = "?#{_.last valuesSplit}"

                return

            else if stateUrlPart is urlParts[currentUrlPartIndex]
              ++currentUrlPartIndex
            else
              dismissedParents[state.name] = state
              
              # isn't a match, so we are done with iterating over url parts
              # for this state
   
              return false

            if isLast index

              lastParent = createStateWrapper(
                lastParent
                state
                stateUrlParts
                stateParameterValues
              )

              _.assign parameters, _.reduce(
                stateParameterValues
                (acc, param)->
                  acc[param.key] = decodeURIComponent param.value
                  acc
                {}
              )

              unless urlParts.length is getStatePathLength()

                # If the lengths aren't the same, then the current state might
                # be a related state, but not the one we are looking for since
                # we have a differenet count of urlParts in the searchUrl than
                # we have in the state's url
                
                currentUrlPartIndex = 0

          
              # all done for this state

              return false

          # still a match, and we are not done - except if the url is shorter
          # than the the state's url

          return currentUrlPartIndex < urlParts.length

      return lastParent?.url isnt searchUrl

    if lastParent?.url is searchUrl
      do ->
        resultState = _.assign {}, lastParent.raw
        resultState.parameters = parameters unless _.isEmpty parameters
        resultState

  [
    '$state'
    'url'
    ($state, url)->
     findStatestUrlPartsIndex $state.get(), url
  ]

