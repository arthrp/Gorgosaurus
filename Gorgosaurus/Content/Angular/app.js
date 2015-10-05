(function () {
    var app = angular.module('forumApp', ['angularMoment', 'ui.bootstrap', 'ui.router']);

    app.config(function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/test');

        $stateProvider
            .state('test', {
                url: '/test',
                templateUrl: 'Content/Templates/test.htm'
            });

    });
})();