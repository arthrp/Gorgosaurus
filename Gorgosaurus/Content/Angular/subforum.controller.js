(function () {
    angular.module('forumApp').controller('subforumController', subforumController);

    subforumController.$inject = ['$uibModal', '$stateParams', '$http', '$scope', '$rootScope'];

    function subforumController($uibModal, $stateParams, $http, $scope, $rootScope) {
        var self = this;

        self.current = null;
        self.currentUsername = $rootScope.username;
        self.pagesArr = [];
        self.baseUrl = 'subforum/' + $stateParams['title'] + '/';
        $scope.subforumTitle = $stateParams['title'];

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

        self.getPageUrl = function (pageNumber) {
            return self.baseUrl + '?page=' + pageNumber;
        };

        function getSubforum() {
            var titleToSearchFor = $stateParams['title'];
            var page = ($stateParams['page']) ? $stateParams['page'] : 0;

            //console.log('page is', page, $stateParams['page']);

            $http.get('/subforum/' + titleToSearchFor + '?page=' + page)
                .success(function (res) {
                    console.log('res',res);
                    self.current = res;
                    self.pagesArr = new Array(res.totalPages);

                    console.log(self.pagesArr);
                    $rootScope.pageTitle = self.current.title;
                })
                .error(function (res) {
                    console.log(res);
                    self.current = null;
                });
        }

        getSubforum();
    }
})();