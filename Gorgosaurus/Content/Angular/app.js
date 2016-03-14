(function () {
    var app = angular.module('forumApp', ['angularMoment', 'ui.bootstrap', 'ui.router', 'ui.router.state', 'ncy-angular-breadcrumb']);

    app.config(function ($breadcrumbProvider) {
        $breadcrumbProvider.setOptions({
            prefixStateName: 'forum',
            template: 'bootstrap3'
        });
    })

    app.config(function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/home');

        $stateProvider
            .state('subforum', {
                url: '/subforum/{title}',
                templateUrl: 'Content/Templates/subforum.htm',
                controller: 'subforumController as subforumCtrl',
                ncyBreadcrumb: {
                    label: '{{subforumTitle}}',
                    parent: function ($scope) {
                        return 'forum';
                    }
                }
            })
            .state('discussion', {
                url: '/subforum/{subforumName}/discussion/{id:int}',
                templateUrl: 'Content/Templates/discussion.htm',
                controller: 'discussionController as forumCtrl',
                ncyBreadcrumb: {
                    label: 'Discussion',
                    parent: function ($scope) {
                        $scope.subforumTitle = $scope.forumCtrl['subforumTitle'];
                        return 'subforum({title:"' + $scope.subforumTitle + '"})';
                    }
                }
            })
            .state('userRegister', {
                url: '/register',
                templateUrl: 'Content/Templates/userRegistration.htm',
                controller: 'userRegisterController as userRegCtrl',
                ncyBreadcrumb: {
                    label: 'Register'
                }

            })
            .state('userInfo', {
                url: '/user/{username}',
                templateUrl: 'Content/Templates/userInfo.htm',
                controller: 'userInfoController as userInfoCtrl',
                ncyBreadcrumb: {
                    label: 'User'
                }
            })
            .state('forum', {
                url: '/home',
                templateUrl: 'Content/Templates/forum.htm',
                controller: 'forumController as forumCtrl',
                ncyBreadcrumb: {
                    label: 'Home'
                }
            });

    });

    app.run(function ($rootScope, $state, $breadcrumb) {
        $rootScope.isActive = function (stateName) {
            return $state.includes(stateName);
        }

        $rootScope.getLastStepLabel = function () {
            return 'Angular-Breadcrumb';
        }
    });
})();