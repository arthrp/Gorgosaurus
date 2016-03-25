(function () {
    angular.module('forumApp').controller('forumController', forumController);

    forumController.$inject = ['$http', '$rootScope'];

    function forumController($http, $rootScope) {
        var self = this;

        self.subforums = [];
        self.forumTitle = "";

        self.getSubforums = function () {
            $http.get("/subforums")
                .success(function (res) {
                    self.subforums = res.subforums;
                    $rootScope.pageTitle = res.forumTitle;
                });
        };

        self.getSubforums();
    }
})();