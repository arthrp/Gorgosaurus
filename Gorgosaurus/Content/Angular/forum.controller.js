(function () {
    angular.module('forumApp').controller('forumController', forumController);

    forumController.$inject = ['$scope', '$http'];

    function forumController($scope, $http) {
        var self = this;

        self.posts = [];
        self.newPostText = "";

        self.getDiscussionPosts = function (discussionId) {
            $http.get('/discussion/' + discussionId)
                .success(function (respData) {
                    self.posts = respData.posts;
                });
        };

        self.addPost = function () {
            console.log('posting');

            var data = { "postText": self.newPostText, "discussionId": 1 };

            $http.post('/post/add', data)
                .success(function (respData) {
                    console.log(respData);
                    self.getDiscussionPosts(1);
                });

            self.newPostText = "";
        };

        self.removePost = function (postId) {
            $http.delete('/post/remove/' + postId)
                .success(function (data) {
                    console.log('deleted');
                    self.getDiscussionPosts(1);
                });            
        };

        self.getDiscussionPosts(1);
    }

})();