(function () {
    angular.module('forumApp').controller('forumController', forumController);

    forumController.$inject = ['$stateParams', '$scope', '$http'];

    function forumController($stateParams, $scope, $http) {
        var self = this;

        console.log($stateParams);

        self.posts = [];
        self.currentDiscussion = null;
        self.newPostText = "";

        self.getDiscussionPosts = function (discussionId) {
            $http.get('/discussion/' + discussionId)
                .success(function (respData) {
                    self.currentDiscussion = respData;
                });
        };

        self.addPost = function () {
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

        self.getDiscussionPosts($stateParams['id']);
    }

})();