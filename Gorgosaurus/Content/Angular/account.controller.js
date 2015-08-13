(function () {
    angular.module('forumApp').controller('accountController', accountController);

    accountController.$inject = ['$scope', '$http'];

    function accountController($scope, $http) {
        var self = this;

        self.greetingText = "";

        self.getCurrentUser = function () {
            $http.get("/account/current").success(function (resp) {
                if (!resp) {
                    self.greetingText = "Hello, guest";
                }
                else {
                    self.greetingText = "Hello, " + resp;
                }

                console.log(self.greetingText);
            });
        }

        self.getCurrentUser();
    }
})();