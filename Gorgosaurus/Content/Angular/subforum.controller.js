(function () {
    angular.module('forumApp').controller('subforumController', subforumController);

    subforumController.$inject = ['$uibModal', '$stateParams', '$http'];

    function subforumController($uibModal, $stateParams, $http) {
        var self = this;

        self.current = null;

        self.addDiscussion = function () {
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'myModalContent.html',
                controller: 'addDiscussionModal',
                size: 300
            });

            var data = { title: "Lala"+Math.random(), subforumId: self.current.id };

            console.log('data', data);

            $http.post('/discussion/add', data)
                .success(function (res) {
                    console.log(res);
                    getSubforum();
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