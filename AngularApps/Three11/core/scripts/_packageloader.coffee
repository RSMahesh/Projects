define ['angular','_requirejs-paths' ], (ng)->
  console.log 'Loading packages'
  require [
    'app/main'
    'browse/main'
    'login/main'
  ], ()->
    console.log 'Bootstrapping app'
    require ['domReady'], (document)->
      injector = ng.bootstrap document, ['mvWebUi'], strictDi: true
      #injector.invoke loadTpls
      #appDirector = injector.get 'appDirector'
      return
    return
  return