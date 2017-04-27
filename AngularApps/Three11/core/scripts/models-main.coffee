# Loads and registeres all models for the core package
# Author: mwesterholz
# Date: 2014/06/22
#
# Copyright © 2014 VRX Studios Inc.
# All Rights Reserved

define [
  'angular'
  'core/models/modal'
  'core/app'
], (
  ng
  ModalStateModel
) ->
  ng.module 'mvWebUi.data'
  .service 'modalStateModel', ModalStateModel

  return
