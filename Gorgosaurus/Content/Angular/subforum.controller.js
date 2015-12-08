(function () {
    angular.module('forumApp').controller('subforumController', subforumController);

    subforumController.$inject = ['$stateParams', '$http'];

    function subforumController($stateParams, $http) {
        var self = this;

        self.current = null;

        self.addDiscussion = function () {
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