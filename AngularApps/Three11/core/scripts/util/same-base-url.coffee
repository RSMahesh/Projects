# Util script to comapre two URLS to determine, if they have the same base url.
# The assumption is that all urls tested this way are of format 'someUrl' or
# 'someUrl/:someId'
#
# Author: Markus Westerholz
# Date: 2014/12/10
#
# Copyright © 2014 MediaValet, Inc.
# All rights reserved

define [
  'lodash'
], (
  _
)->

  guidExpr = /(\/(?:(?:[0-9a-f]+)-?){5})|(\/(?:[^_\/]+_[^\/]+))/g

  (lhsRaw, rhsRaw)->
    lhs = lhsRaw.replace guidExpr, '{id}'
    rhs = rhsRaw.replace guidExpr, '{id}'
    
    # Compare all but queries
    
    (_.first (lhs.split '?')) is _.first (rhs.split '?')

