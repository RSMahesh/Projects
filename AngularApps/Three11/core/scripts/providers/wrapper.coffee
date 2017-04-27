# Configurable provider, intended to configure settings for services,
# directives, filters, etc. at instantitiation of that object
#
# Author: Markus Westerholz
# Date: 2014/11/26
#
# Copyright © MediaValet, Inc.
# All rights reserved

define [
  'lodash'
  'core/util/assert'
],(
  _
  Assert
)->  ->
  ctorDefinition = _.first arguments
  Assert.true not _.isUndefined ctorDefinition, 'Service ctor not provided'
  if _.isArray ctorDefinition
    func = _.last ctorDefinition
    args = _.first ctorDefinition, ctorDefinition.length - 1
    createServiceConstructor = (config)->
      instanceArgs = _.clone args
      instanceArgs.push func config
      instanceArgs
  else
    Assert.true _.isFunction ctorDefinition, 'Provide either a function or an array with dependencies'
    createServiceConstructor = ctorDefinition

  ->
    # `this` will be the function instance. When Angular invokes the parent of
    # this function, this function will be returned. As a result, Angular will
    # treat the function as a ctor and call `new` on it, correctly setting
    # the `this` to this function
    #
    # Client code will append values to this fuction in the .config call,
    # which is why we inject `this` as the `config` object to the service ctor
    serviceCtor = createServiceConstructor @
    Object.defineProperty @, '$get', value: serviceCtor
    return
    
