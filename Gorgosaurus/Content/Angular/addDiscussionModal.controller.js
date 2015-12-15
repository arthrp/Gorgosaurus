(function () {
    angular.module('forumApp').controller('addDiscussionModal', addDiscussionModal);

    addDiscussionModal.$inject = ['$scope', '$http', '$uibModalInstance', 'subforumId'];

    function addDiscussionModal($scope, $http, $uibModalInstance, subforumId) {
        var self = this;

        self.title = "";

        self.add = function () {
            var dataToSend = { title: self.title, subforumId: subforumId };
            console.log('data', dataToSend);

            $http.post('/discussion/add', dataToSend)
                .success(function (res) {
                    console.log(res);
                    $uibModalInstance.close(true);
                });
        }

        self.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }
    }

})();