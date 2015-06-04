(function () {
    angular.module('forumApp').controller('forumController', forumController);

    forumController.$inject = ['$scope', '$http'];

    function forumController($scope, $http) {
        var self = this;

        self.posts = [
            { id: 1, postText: "Haha" },
            { id: 5, postText: "Test" }
        ];

        self.getDiscussionPosts = function (discussionId) {
            $http.get('/discussion/' + discussionId)
                .success(function (respData) {
                    self.posts = respData.posts;
                });
        };

        self.addPost = function () {
            console.log('posting');

            var data = { "postText": "Crap post "+Math.random(), "discussionId": 1 };

            $http.post('/post/add', data)
                .success(function (respData) {
                    console.log(respData);
                });
        };

        self.getDiscussionPosts(1);
    }

})();