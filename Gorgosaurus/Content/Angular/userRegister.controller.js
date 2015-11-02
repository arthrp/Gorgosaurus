(function () {
    angular.module('forumApp').controller('userRegisterController', userRegisterController);

    userRegisterController.$inject = ['$http']

    function userRegisterController($http) {
        var self = this;

        self.newUserInfo = {
            username: 'Hello',
            password: '',
            name: '',
            surname: ''
        };

        self.registerUser = function () {
            console.log('registering', self.newUserInfo);

            $http.post('/account/create', self.newUserInfo)
            .success(function (res) {
                console.log('user created');
            });
        }
    }
})();