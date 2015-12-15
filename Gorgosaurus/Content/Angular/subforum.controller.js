(function () {
    angular.module('forumApp').controller('subforumController', subforumController);

    subforumController.$inject = ['$uibModal', '$stateParams', '$http', '$scope'];

    function subforumController($uibModal, $stateParams, $http, $scope) {
        var self = this;

        self.current = null;

        self.addDiscussion = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'myModalContent.html',
                controller: 'addDiscussionModal as addDiscussionCtrl',
                size: 300,
                resolve: {
                    subforumId: function () {
                        return self.current.id;
                    }
                }
            });

            modalInstance.result.then(function (res) {
                if (res) {
                    getSubforum();
                }
            });
        };

        function getSubforum() {
            var titleToSearchFor = $stateParams['title'];

            $http.get('/subforum/' + titleToSearchFor)
                .success(function (res) {
                    console.log('res',res);
                    self.current = res;
                })
                .error(function (res) {
                    console.log(res);
                    self.current = null;
                });
        }

        getSubforum();
    }
})();