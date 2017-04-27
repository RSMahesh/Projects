# Wrapping a queue-set, this set will remove the least recently used item, when
# its capacity is reached and a new item is added
#
# Author: Markus Westerholz
# Date: 2015/03/27
#
# Copyright © 2015 MediaValet, Inc.
# All rights reserved
'use strict'

define [
  'core/util/assert'
  'core/util/props'
  'core/util/queue-set'
], (
  Assert
  props
  createQueueSet
)->(capacity)->

  Assert capacity > 0, 'Capacity for LRU Set must be greater than 0'

  obj = Object

  # Looks like PantomJs won't let me redefine non-enumerable properties - need
  # to take prototypical approach
  
  theSet = obj.create createQueueSet()
  queueSet = obj.getPrototypeOf theSet

  props theSet,
    capacity: get: -> capacity
    enqueue:
      configurable: true
      value: (value)->
        @dequeue() unless (@length) < capacity
        queueSet.enqueue value
    get:
      configurable: true
      value:(key)->
        val = queueSet.get key

        # re-enqueue to push item to top of the queue
        (queueSet.enqueue val) if val?
        
        return val
