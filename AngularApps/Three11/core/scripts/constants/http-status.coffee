# Defines all Status codes we use with the app
#
# Author: Markus Westerholz
# Date: 2014/09/11
#
# Copyright © 2014 VRX Studios Inc.
# All Rights Reserved

define [
 'core/util/prop'
],(
  prop
)->

  httpStatus = {}
  
  appendReadOnlyProperty = (value, name)->
    prop httpStatus, name, enumerable:true, value: value, writable:false
    return

  appendReadOnlyProperty ((val)-> 200 < val or val >= 600), 'isUnknown'

  # Successful
  appendReadOnlyProperty 200, 'OK'
  appendReadOnlyProperty 201, 'Created'
  appendReadOnlyProperty 202, 'Acccepted'
  appendReadOnlyProperty 204, 'No Content'

  appendReadOnlyProperty ((val)-> 200 <= val < 300 ), 'wasSuccesful'

  # Redirect
  appendReadOnlyProperty 304, 'Not Modified'

  appendReadOnlyProperty ((val)-> 200 <= val < 400 ), 'wasProcessed'
  
  # Client Errors
  appendReadOnlyProperty 400, 'Bad Request'
  appendReadOnlyProperty 401, 'Unauthorized'
  appendReadOnlyProperty 403, 'Forbidden'
  appendReadOnlyProperty 404, 'Not Found'
  appendReadOnlyProperty 405, 'Not Allowed'
  appendReadOnlyProperty 406, 'Not Acceptable'
  appendReadOnlyProperty 408, 'Request Timeout'
  appendReadOnlyProperty 409, 'Conflict'
  appendReadOnlyProperty 410, 'Gone'

  appendReadOnlyProperty ((val)-> 400 <= val < 500 ), 'hasClientError'

  # Server Errors
  appendReadOnlyProperty 500, 'Internal ServerError'
  appendReadOnlyProperty 501, 'Not Implemented'
  appendReadOnlyProperty 503, 'Service Unavailable'
  
  appendReadOnlyProperty ((val)-> 500 <= val < 600 ), 'hasServerError'
  appendReadOnlyProperty ((val)-> 400 <= val < 600 ), 'hasError'


  return httpStatus
