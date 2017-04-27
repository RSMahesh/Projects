# Utility script to bind enumerable properties of one object - the model -
# to another
#
# Author: Markus Westerholz
# Date: 2014/11/20
#
# Copyright © MediaValet, Inc.
# All rights reserved


define [
  'lodash'
  'core/util/assert'
],
(
  _
  Assert
)->
  (target, model)->
    obj = Object
    defineProperty = _.bind obj.defineProperty, null, target
    enumerable = enumerable: true

    _.forIn model, (prop, key)->
      Assert.false (_.has target, key), 'property already defined'

      if _.isFunction prop
        funcProp =
          value: ->
            # we want to make sure we allow to call modified property fuctions
            # as well, for example when observers register with observable
            # models after the model has been bound

            model[key].apply model, _.toArray arguments
        defineProperty key, _.extend funcProp, enumerable

      else
        boundProp =
          get:-> model[key]
          set:(value)-> model[key] = value
        defineProperty key, _.extend boundProp, enumerable

    return target
