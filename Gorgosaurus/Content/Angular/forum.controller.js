(function () {
    angular.module('forumApp').controller('forumController', forumController);

    forumController.$inject = ['$scope'];

    function forumController($scope) {
        var self = this;

        $scope.posts = [
            { id: 1, postText: "Haha" },
            { id: 5, postText: "Test" }
        ];
    }

})();