# Manages global application behaviour, such as redirecting to packages (e.g.
# login, app), checking authentication expiry, etc.
#
# Author: Markus Westerholz
# Date: 2014/11/26
#
# Copyright © MediaValet, Inc.
# All rights reserved
'use strict'
define [
  'lodash'
  'core/util/props'
  './app-director/find-state-for-path'
], (
  _
  props
  findStateForPath
)->->
  theProvider = undefined
  createDirector = [
    '$injector'
    'authToken'
    'loginNotifier'
    'logoutNotifier'
    'appReady'
    ($injector, authToken, loginNotifier, logoutNotifier, onAppReady)->

      theService = {}
      Login = 'login'
      Home = 'home'

      $state = $injector.get '$state'

      defineProperty = _.bindKey Object, 'defineProperty', theService
      bindToServiceFunc = (func)-> _.bindKey theService, func

      # States may define 'initialzedIn' and 'default' properties, indicating
      # a redirect to a different state. Here, we look through those properties
      # to find the final state we want to end up at in initialization

      getFinalState = (state)->

        parameters = state.parameters

        while state.default?
          state = do ->
            defaultState = $state.get state.default

            defaultState.transitionParams =
                _.assign (state.transitionParams or {}), parameters
        
            defaultState

        if state.initializedIn?
          state = do ->
            
            # Manual search prevents crash if state is not found

            initState =
              _.find $state.get(), (s)-> s.name is state.initializedIn

            if initState
              initState.transitionParams =
                _.assign (initState.transitionParams or {}), parameters
            return initState or state

        state
   
      # Removes leading and trailing '/' from 'url'

      trimUrl = (url)->
        startIndex = if (_.first url) is '/' then 1 else 0
        length = url.length - (if (_.last url) is '/' then 1 else 0)
        url[startIndex...length]
      
      # Go to a state defined in 'theProvider'
      
      goToState = (state)-> ->
        targetState = theProvider[state]
        return unless targetState?

        if stateDef = $state.get targetState
          
          # Note: During unit tests, stateDef might be undefined after the call
          #       to 'get'. To simplify testing, we, therefore, check if the
          #       stateDef is defined.
          
          targetState = stateDef.default if stateDef.default

        $state.go targetState, undefined, reload: true

      defineProperty Login, value: goToState "#{Login}State"
      defineProperty Home, value: goToState "#{Home}State"

      $location = $injector.get '$location'
      query = $location?.search?()

      if query?
        referrer = query.referrer
        query = _.omit query, 'referrer'

      goToStateWhenReady = (goToStateFunc)->
        return ->
          detach = onAppReady ->
            logoutDetach()
            detach()
            goToStateFunc()
            return
          logoutDetach = logoutNotifier ->
            detach()
            logoutDetach()
            return
          return
      
      goHomeWhenReady = goToStateWhenReady bindToServiceFunc Home

      if referrer
        goToInitialState = do ->

          trimedUrl = trimUrl referrer
          state = $injector.invoke findStateForPath, $injector, url:trimedUrl

          unless state?
            goToLocation = goHomeWhenReady
          else

            state = getFinalState state

            parameters = state.parameters

            if state.isPublic
              if parameters?
                goToPublicState = _.bindKey $state, 'transitionTo', state.name,
                  parameters
              else
                goToPublicState  = _.bindKey $state, 'go', state.name
              
              goToLocation = ->
                
                # We  notify by default if we are logged in, even if we hit a
                # public space.
                #
                # If we are not logged in, we need to inform the app that we are
                # starting an anonymous session, meaning we are 'logged in' as an
                # anonymous user.
                
                loginNotifier() unless authToken.isLoggedIn
                detach = onAppReady ->
                  detach()
                  goToPublicState()

            else

              stateName = state.name
              
              if parameters?
                state.transitionParams =
                  _.assign state.transitionParams or{} , parameters

                goToLocation =
                  goToStateWhenReady _.bindKey $state, 'go', stateName, parameters
              
              else
                goToLocation =
                  goToStateWhenReady _.bindKey $state, 'go', stateName



            # '$rootScope' might not be defined in some unit tests
            $rootScope = $injector.get '$rootScope'
            if $rootScope?
            
              do (state, parameters)->
                # We expose the inital parameters and state until we navigate
                # away from the inital state
               
                defineProperty 'initialParameters', get: -> parameters
                defineProperty 'initialState',      get: -> state
                
                detach = $rootScope.$on '$stateChangeStart',
                  (evt, to, toParams, from)->
                    if from.name is state.name
                      detach()
                      parameters = undefined
                      state = undefined
                    return

         
          return goToLocation if authToken.isLoggedIn or state.isPublic

          return do ->
            detach = loginNotifier ->
              detach()
              goToLocation()
              return
            return bindToServiceFunc Login
      else
        if authToken.isLoggedIn
          goToInitialState = do ->
            finalState = getFinalState $state.get theProvider["#{Home}State"]
            goToStateWhenReady _.bindKey $state, 'go', finalState

        else
          goToInitialState = do ->
            detach = loginNotifier ->
              detach()
              goHomeWhenReady()
              return
            return bindToServiceFunc Login

      goToInitialState()
      
      logoutNotifier ->
        detach = loginNotifier ->
          detach()
          goHomeWhenReady()
          return
        theService.login()
        return
      
      loginNotifier() if authToken.isLoggedIn

      return theService
  ]

  theProvider = props {},
    $get: value: createDirector
