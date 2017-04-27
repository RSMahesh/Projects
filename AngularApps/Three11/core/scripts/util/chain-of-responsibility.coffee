# Implements a chain-of-responsibility container object, allowing to register
# functors via a provider
#
# locals: [optional] object defining resolve to be forwarded to all child
#         functions on instantiation
#
# Functions are added to the chain through the provider by calling 'push'
# on the provider
#
# Example:
# define [
#   'angular'
#   'some-filter-function'
# ],(
#   ng
#   someFilterFunction
# )->
#  ng.module('mvWebUi')
#  .config ['SomeFilterChain', (filterChainProvider)->
#    filterChainProvider.push ['dependency', someFilterFunction]
#  ]
#
# Functions are invoked in reverse order of adding them, meaning the last
# function added will be the first invoked, while the first one will be invoked
# last.
#
# Functions should check the last argumet passed in, if they don't handle the
# context passed in as the last arguments, similar to middleware in Express.
#
# Example:
#
# chainableFunction = ->(context)->
#   /* do your thing, function*/
#   if result?
#     return result
#   else
#     next = _.last arguments
#     return next?(context)
#
# Author: Markus Westerholz
# Date: 2015/01/23
#
# Copyright © 2015 MediaValet, Inc.
# All rights reserved
'use strict'
define [
  'lodash'
], (
  _
  Assert
)->->
  _locals = arguments[0]
  _funcDefs = []

  createChain = ($injector)->
    _.reduce(
      _funcDefs
      (head, funcCtor)->
        _.partialRight ($injector.invoke funcCtor, null, _locals), head or undefined
      null
    )

  $get: ['$injector', createChain]
  
  push:(func)->
    _funcDefs.push func
    return @
