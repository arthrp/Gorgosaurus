(function () {
    angular.module('forumApp').controller('discussionController', discussionController);

    discussionController.$inject = ['$stateParams', '$scope', '$http', '$rootScope'];

    function discussionController($stateParams, $scope, $http, $rootScope) {
        var self = this;

        console.log('state params',$stateParams);

        self.posts = [];
        self.currentDiscussion = null;
        self.newPostText = "";
        self.subforumTitle = $stateParams['subforumName'];

        $rootScope.pageTitle = self.subforumTitle;

        self.getDiscussionPosts = function (discussionId) {
            $http.get('/discussion/' + discussionId)
                .success(function (respData) {
                    self.currentDiscussion = respData;
                });
        };

        self.addPost = function () {
            var data = { "postText": self.newPostText, "discussionId": $stateParams['id'] };

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