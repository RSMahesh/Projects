# Helper function that strips all functions from target
#
# Author: Markus Westerholz
# Date: 2015/01/20
#
# Copyright © 2015 MediaValet, Inc.
'use strict'
define ->
  IgnoreKey = _links: true, $$hashKey: true
  stripFunctions = (obj)-> _.omit obj, (v, k)-> IgnoreKey[k] or _.isFunction v
