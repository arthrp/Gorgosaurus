(function () {
    angular.module('forumApp').controller('forumController', forumController);

    forumController.$inject = ['$scope', '$http'];

    function forumController($scope, $http) {
        var self = this;

        self.posts = [
            { id: 1, postText: "Haha" },
            { id: 5, postText: "Test" }
        ];


    }

})();