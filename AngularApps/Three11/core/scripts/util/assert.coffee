# Utility function to facilitae assert functionality
#
# Author: Markus Westerholz
# Date: 2014/09/10

$window = @

check = (condition, message)->
  if !condition
    debugger if $window.MV_BREAK_ON_FAILED_ASSERT
    throw new Error(message)
  return

assert = (condition, message)-> assert.true condition, message

assert.true = (condition, message)-> check condition, message
assert.false = (condition, message)-> assert.true !condition, message

assert.isInRange = (candidate, lowerBound, upperBound, message) ->
  check(candidate? or lowerBound? or upperBound?, "Assert.isInRange: Undefined/null argument")
  check(lowerBound <= upperBound, "Assert.isInRange: lowerBound must be less than or equal to upperBound")
  check(lowerBound <= candidate < upperBound, message)

assert.isObjectInstance = (candidate, message)->
  assert.false (_.isUndefined candidate) or (_.isNull candidate) or not (_.isObject candidate),
    message
  return true

assert.isFunction = (candidate, message)->
  assert.isObjectInstance candidate, message
  assert.true _.isFunction candidate, message
  return true

define ['lodash'], ->
  return assert

