(function () {
    angular.module('forumApp').controller('subforumController', subforumController);

    subforumController.$inject = ['$stateParams', '$http'];

    function subforumController($stateParams, $http) {
        var self = this;

        self.current = null;

        function getSubforum() {
            var titleToSearchFor = $stateParams['title'];

            $http.get('/subforum/' + titleToSearchFor)
                .success(function (res) {
                    console.log(res);
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