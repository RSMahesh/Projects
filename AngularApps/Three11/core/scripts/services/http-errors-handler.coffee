# Implemented using the provider syntax, this service allows us to register
# conditional error handlers for HTTP related calls
#
# Author: Markus Westerholz
# Date: 2015/03/06
#
# Copyright © 2015 MediaValet, Inc.
# All rights reserved
'use strict'

define [
  'angular'
  'lodash'
  'core/util/prop'
  'core/util/chain-of-responsibility'
  'core/app'
], (
  ng
  _
  prop
  createChainOfResponsibility
)->

  MvKey = '$$mvKey'

  newInterceptor = (handlers, md5, $q)->
    appendKey = (config)->
      prop config, MvKey,
        enumerable: true
        configurable: true
        value: md5 config

      return config[MvKey]

    interceptor =
      request: (config)->
        appendKey config unless config[MvKey]?
        return (handlers 'request', config) if handlers?
        return config

      response: (response)->
        key = response?.config?[MvKey]
        if key?
          
          response = (handlers 'response', response) if handlers?

          # All is well, clear up

          delete response.config[MvKey]

        return response

      responseError: (response)->
        key = response?.config?[MvKey]
        processed = response
        if key?
          processed = (handlers 'responseError', response) if handlers?

          # if what we return is not a promise then we will delete the
          # key - a promise will mean that we still need to identify the
          # query
          
          delete processed.config?[MvKey] if processed? and 'then' of processed

        return processed or $q.reject response


  createProvider = ->
    _chainOfResponsibility = createChainOfResponsibility MvKey: MvKey
    theProvider = _.assign {}, _chainOfResponsibility
    
    theProvider.$get = ['$injector', 'md5', '$q', ($injector, md5, $q)->
      newInterceptor ($injector.invoke _chainOfResponsibility.$get), md5, $q
    ]

    return theProvider
