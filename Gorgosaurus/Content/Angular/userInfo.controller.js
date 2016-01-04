(function () {
    angular.module('forumApp').controller('userInfoController', userInfoController);

    userInfoController.$inject = ['$http', '$stateParams'];

    function userInfoController($http, $stateParams) {
        var self = this;

        self.userInfo = {};

        self.getUser = function(){
            var username = $stateParams['username'];

            $http.get('/user/' + username)
            .success(function (res) {
                self.userInfo = res;
            })
            .error(function (res) {
                console.log(res);
            });
        }

        self.getUser();
    }
})();