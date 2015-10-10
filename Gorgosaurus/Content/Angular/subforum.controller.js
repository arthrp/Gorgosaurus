(function () {
    angular.module('forumApp').controller('subforumController', subforumController);

    subforumController.$inject = ['$stateParams', '$http'];

    function subforumController($stateParams, $http) {
        var self = this;

        self.current = null;

        function getSubforumDiscussions() {
            var titleToSearchFor = $stateParams['title'];

            $http.get('/subforum/' + titleToSearchFor)
                .success(function (res) {
                    self.current = res;
                });
        }

        getSubforumDiscussions();
    }
})();