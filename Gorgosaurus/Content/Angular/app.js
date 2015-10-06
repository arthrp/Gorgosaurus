(function () {
    var app = angular.module('forumApp', ['angularMoment', 'ui.bootstrap', 'ui.router']);

    app.config(function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/discussion/1');

        $stateProvider
            .state('discussion', {
                url: '/discussion/{id:int}',
                templateUrl: 'Content/Templates/forum.htm',
                controller: 'forumController as forumCtrl'
            });

    });
})();