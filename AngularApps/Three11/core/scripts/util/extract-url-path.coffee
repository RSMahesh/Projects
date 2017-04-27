# Extracts the path section (including query string if present) from a URL
#
# Author: Markus Westerholz
# Date: 2015/03/20
#
# Copyright © 2015 MediaValet, Inc.
# All rights reserved
'use strict'

define ->
  hostUrl = /^http(s)?:\/\/[^\/]+\//i
  (s)-> (s or '').replace hostUrl, ''
