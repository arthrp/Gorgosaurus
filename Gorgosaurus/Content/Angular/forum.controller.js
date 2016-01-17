(function () {
    angular.module('forumApp').controller('forumController', forumController);

    forumController.$inject = ['$http'];

    function forumController($http) {
        var self = this;

        self.subforums = [];
        self.forumTitle = "";

        self.getSubforums = function () {
            $http.get("/subforums")
                .success(function (res) {
                    self.subforums = res.subforums;
                    self.forumTitle = res.forumTitle;
                });
        };

        self.getSubforums();
    }
})();