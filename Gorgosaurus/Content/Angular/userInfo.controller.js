(function () {
    angular.module('forumApp').controller('userInfoController', userInfoController);

    userInfoController.$inject = ['$http', '$stateParams'];

    function userInfoController($http, $stateParams) {
        var self = this;

        self.userInfo = {};
        self.errorText = null;

        self.getUser = function(){
            var username = $stateParams['username'];
            self.errorText = null;

            $http.get('/user/' + username)
            .success(function (res) {
                self.userInfo = res;
            })
            .error(function (res) {
                console.log(res);

                self.errorText = 'You must be logged in to view users\' information.';
            });
        }

        self.getUser();
    }
})();