(function () {
    var app = angular.module('forumApp', ['angularMoment', 'ui.bootstrap', 'ui.router']);

    app.config(function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/home');

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
            })
            .state('userRegister', {
                url: '/register',
                templateUrl: 'Content/Templates/userRegistration.htm',
                controller: 'userRegisterController as userRegCtrl'
            })
            .state('userInfo', {
                url: '/user/{username}',
                templateUrl: 'Content/Templates/userInfo.htm',
                controller: 'userInfoController as userInfoCtrl'
            })
            .state('forum', {
                url: '/home',
                templateUrl: 'Content/Templates/forum.htm',
                controller: 'forumController as forumCtrl'
            });

    });
})();