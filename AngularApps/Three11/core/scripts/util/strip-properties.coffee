# Strips the properties from a given object.
#
# Author: Vipul Agarwal
# Created: 2014/10/08
#
# Copyright © 2014 VRX Studios Inc.
# All Rights Reserved
'use strict'
define [
  'lodash'
],(
  _
)->(obj) ->
  _.forIn obj, (value, key)->
    delete obj[key] unless _.isFunction value
    return
  return obj
