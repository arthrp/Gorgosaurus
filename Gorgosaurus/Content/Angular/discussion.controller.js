(function () {
    angular.module('forumApp').controller('discussionController', discussionController);

    discussionController.$inject = ['$stateParams', '$scope', '$http'];

    function discussionController($stateParams, $scope, $http) {
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
                    self.getDiscussionPosts($stateParams['id']);
                });

            self.newPostText = "";
        };

        self.removePost = function (postId) {
            $http.delete('/post/remove/' + postId)
                .success(function (data) {
                    console.log('deleted');
                    self.getDiscussionPosts($stateParams['id']);
                });            
        };

        self.getDiscussionPosts($stateParams['id']);
    }

})();