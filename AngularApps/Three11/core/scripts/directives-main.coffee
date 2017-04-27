# Loads all core directives
# Author: Kulminder Singh
# Date: 2014/07/01
#
# Copyright © 2014 VRX Studios Inc.
# All Rights Reserved

define [
  'angular'
  'core/directives/mv-modal'
   'core/app'
], (
  ng
  createModalDirective
)->
  ng.module 'mvWebUi.directives'
  .directive 'mvModal', [
    '$injector'
    createModalDirective
  ]
  return
