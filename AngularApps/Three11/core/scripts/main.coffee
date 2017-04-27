requirejs.config
 waitSeconds:30
 paths: {
	jquery: '/bower_components/jquery/dist/jquery'
	domReady: '/bower_components/requirejs-domready/domReady',
	angular: '/bower_components/angularjs/angular',
	lodash: '/bower_components/lodash/lodash',
 uibootstrap: '/bower_components/angular-bootstrap/ui-bootstrap-tpls'
	restangular: '/bower_components/restangular/dist/restangular'
	router: '/bower_components/angular-ui-router/release/angular-ui-router'
	JSONPatch: '/bower_components/fast-json-patch/dist/json-patch-duplex.min'
 cryptojs: '/bower_components/cryptojslib/components/core'
 md5: '/bower_components/cryptojslib/components/md5'
 core: '/core/scripts'
    
  }
 shim:
    angular:
      deps: ['jquery']
      exports: 'angular'
    router: ['angular'] 
    uibootstrap: ['angular']
    restangular:
      exports: 'restangular'
      deps: [
        'angular'
        'lodash'
      ]
    md5:
     deps:['cryptojs']
 deps: ['core/bootstrap']