(function () {
    var app = angular.module('forumApp', ['angularMoment', 'ui.bootstrap', 'ui.router']);

    app.config(function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/discussion/1');

        $stateProvider
            .state('discussion', {
                url: '/discussion/{id:int}',
                templateUrl: 'Content/Templates/discussion.htm',
                controller: 'discussionController as forumCtrl'
            })
            .state('subforum', {
                url: '/subforum/{title}',
                templateUrl: 'Content/Templates/subforum.htm',
                controller: 'subforumController as subforumCtrl'
            });

    });
})();