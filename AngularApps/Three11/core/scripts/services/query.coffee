# The query service is responsible for executing and monitoring HTTP requests
# to the server.
#
# When you want to execute a query, you inject the query service instance
# and pass in a 'query' object.
#
# To add a body, params and headers to the query object you call 'setBody(<object>)',
# 'appendParam(<key>, <value>)', and 'appendHeader(<key>, <value>)' respectively.
#
#   Eg: With the following 'JsonHomeDocument':
#           "category": {
#               "href": "/categories/{id}",
#               "hints": {
#                   "allow": ["GET", "PUT", "PATCH", "DELETE"],
#           }
#
#   If we want to create a query object to represent a 'PUT' request with id 123
#   we call 'replaceCategory(123)'. After the query object is created we can then
#   add the body, headers, and params to the query object before we run the query
#   using 'QueryService.sendQuery(query)', which will return a promise.
#
# Author: Markus Westerholz
# Date: 2014/09/10
#
# Copyright © 2014 VRX Studios Inc.
# All Rights Reserved
'use strict'
define [
  'lodash'
  'core/util/props'
  'core/util/assert'
], (
  _
  props
  Assert
)->->
  _resources = {}
  
  $$resources = (k, v)->
    if v?
      _resources[k] = v
    else if k?
      _resources[k]
    else
      undefined

  theService = props {},
    setLoginQuery: value: (query)->
      Assert.isObjectInstance query,
        'Expected object as login query definition'
      props theService,

        login: value: (credentials)-> theService.run query.login credentials

   
    # Executes the query
    # TODO: Add default error handling, such as login redirects, logging, etc.
    run: value:(query)->query()

    clear: value: ->
      delKey = (v,k)-> delete @[k]
      _.forIn @, _.bind delKey , @
      _resources = {}
      return

    $$resources: value: $$resources

