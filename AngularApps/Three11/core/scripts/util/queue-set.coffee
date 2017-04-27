# Implementation of a queue, with the additional limitation of acting like a
# unique set
#
# Author: Markus Westerholz
# Date: 2014/10/14
#
# Copyright © VRX Studos, Inc.
# All rights reserved
'use strict'
define [
  'lodash'
  'core/util/props'
  'core/util/assert'
], (
  _
  props
  Assert
)->

  # ctor function for a new queue-set instance
  # @intialData [optional] if set, instantiatesthe queue to the data in the
  #                        object provided. The format is the JSONified version
  #                        of the toJSON output
  #
  return (initialData) ->
    # -------------------------------------------------------------------------
    hasKey = (value)-> value?.key?

    # -------------------------------------------------------------------------
    assertHasKey = (value)->
      Assert.true(hasKey(value), '\'key\' property not defined')
      return

    # -------------------------------------------------------------------------
    _first =  undefined
    _last = undefined
    _index = {}
    
    if initialData?
      _first = initialData.first
      _last = initialData.last
      _index = initialData.index

    theList = {}
     
    # -------------------------------------------------------------------------
    # Creates a node with (p)revious, (n)ext, and (v)alue
    # properties. Using short names to reduce script somewhat
    # since properties cannot be minified (see angularjs
    # cachefactory code for a similar implementations)
    createNode = (value) ->
      node = {}
      node.n = undefined
      node.p = undefined
      node.v = value
      return node

    # -------------------------------------------------------------------------
    updateFirstAndLast =(prev, next)->
      if _.isEmpty _index
        _first = _last = undefined
      else
        prev = next unless prev?
        _first = prev.v.key unless prev?.p?
        
        next = prev unless next?
        _last = next.v.key unless next?.n?


    # -------------------------------------------------------------------------
    remove = (node)->

      prev = _index[node.p]
      next = _index[node.n]

      prev.n = node.n if prev
      next.p = node.p if next

      node.p = undefined
      node.n = undefined
      
      delete _index[node.v.key]
      updateFirstAndLast prev, next

      return

    # -------------------------------------------------------------------------
    injectBefore = (injected, original)->
      Assert.true original and injected

      injected.n = original.v.key
      injected.p = original.p

      previous = _index[original.p]
      previous.n = injected.v.key if previous
      
      original.p = injected.v.key

      updateFirstAndLast injected, original

      return
    
    # -------------------------------------------------------------------------
    injectValue  = (value, nextKey)->
      assertHasKey value

      if value.key is nextKey
        node = _index[value.key]
        node.v = value
        return

      node = _index[value.key]
      next = _index[nextKey]

      if node
        remove node
        node.v = value
      else
        node =  createNode value

      unless _first?
        _first = _last = node.v.key
      else
        injectBefore node, next
      _index[value.key] = node
      return
     
    props theList,

      # Gets the first item of the list, if set
      first: get:-> @get(_first) if _first?

      # Gets the last item of the list, if set
      last: get:-> @get(_last) if _last?
     
      # If a value for the key is not in the list, then it is injected at the
      # beginning of the set
      #
      # If a value does exists for the key, the old value is removed from
      # the set and the new value placed at the beginnig of the set
      #
      # @value An object, minimally of the form { key: KEY }

      enqueue:
        configurable: true
        value: (value)->
          injectValue value, _first
          return

      # Removes the last value form the set and returns it
      #
      # @returns The last value or `undefined`, if the set was empty

      dequeue:
        value: ->
          return undefined unless _last?
          node = _index[_last]
          remove node
          return node.v

      # Checks if a value for the key is present

      has:
        value: (key)-> _.has _index, key

      # Returns the value for the given key, iff a value for the key is
      # contained in the list
      get:
        configurable: true
        value: (key)-> _index[key].v if _.has _index, key
 
      # Returns the count of values in the array
      length: get: -> _.size _index
      
      # Generates a JSON string, allowing this object to be serialized
      toJSON: value: -> first: _first,last: _last,index: _index


      clear: value: ->
        _first = undefined
        _last  = undefined
        _index = {}

    return theList
