# Module wrapper for CryptoJS, which is loaded and installed globally
#
# Author: Markus Westerholz
# Date: 2015/01/15
#
# Copyright @ 2015 MediaValet, Inc.
# All rights reserved
'use strict'
define [
  'angular'
  'lodash'
  'md5'
],(
  ng
  _
) ->
  ng.module 'CryptoJS', []
  .service 'md5', ->
    (input)->
      if _.isObject input
        s = JSON.stringify input
      else
        s = input
      (CryptoJS.MD5 "#{s}").toString()
