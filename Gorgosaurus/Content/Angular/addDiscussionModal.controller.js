(function () {
    angular.module('forumApp').controller('addDiscussionModal', addDiscussionModal);

    addDiscussionModal.$inject = ['$scope', '$http', '$uibModalInstance', 'subforumId'];

    function addDiscussionModal($scope, $http, $uibModalInstance, subforumId) {
        var self = this;

        self.title = "";
        self.content = "";

        self.add = function () {
            var dataToSend = { title: self.title, subforumId: subforumId, firstPostText: self.content };
            console.log('data', dataToSend);

            $http.post('/discussion/add', dataToSend)
                .success(function (res) {
                    addInitialPost();

                    $uibModalInstance.close(true);
                })
                .error(function (res) {
                    $uibModalInstance.close(false);
                });
        }

        function addInitialPost() {
            var data = { "postText": self.content };
        }

        self.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }
    }

})();