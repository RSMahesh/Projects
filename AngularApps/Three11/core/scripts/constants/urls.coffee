define [
  'angular'
  'core/config'
  'core/app'
], (ng, Config)->
  constants = ng.module 'mvWebUi.constants'
  constants.constant 'urls.base', Config.api
  constants.constant 'videoStreamingServerConfig', Config.video
  constants.constant 'urls.login', 'authorization/token'
  constants.constant 'urls.registration', "public/#{Config.host}/users"
  constants.constant 'urls.termsAndConditions', "public/#{Config.host}/termsAndConditions"
  constants.constant 'urls.orgUnit', "public/organizationalUnits/#{Config.host}"
  constants.constant 'urls.recoverPassword', 'public/users/recoverpassword'
  return
