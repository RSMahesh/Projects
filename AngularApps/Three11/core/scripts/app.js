// Generated by CoffeeScript 1.9.2
(function() {
  'use strict';
  define(['angular', 'router', 'uibootstrap', 'restangular', 'core/modules/cryptojs'], function(ng) {
    var appNamespace, constNamespace, controllersNamespace, dalNamespace, directivesNamespace, errorsNamespace, filtersNamespace, servicesNamespace;
    appNamespace = 'mvWebUi';
    servicesNamespace = appNamespace + ".services";
    ng.module(servicesNamespace, []);
    filtersNamespace = appNamespace + ".filters";
    ng.module(filtersNamespace, []);
    directivesNamespace = appNamespace + ".directives";
    ng.module(directivesNamespace, []);
    dalNamespace = appNamespace + ".data";
    ng.module(dalNamespace, []);
    errorsNamespace = appNamespace + ".errors";
    ng.module(errorsNamespace, []);
    controllersNamespace = appNamespace + ".controllers";
    ng.module(controllersNamespace, [servicesNamespace, dalNamespace]);
    constNamespace = appNamespace + ".constants";
    ng.module(constNamespace, []);
    return ng.module(appNamespace, ['ui.router', 'ui.bootstrap', 'restangular', 'CryptoJS', servicesNamespace, filtersNamespace, directivesNamespace, dalNamespace, controllersNamespace, constNamespace, errorsNamespace]);
  });

}).call(this);
