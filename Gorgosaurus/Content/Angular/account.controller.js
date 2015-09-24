(function () {
    angular.module('forumApp').controller('accountController', accountController);

    accountController.$inject = ['$scope', '$http'];

    function accountController($scope, $http) {
        var self = this;

        self.username = "Tester";
        self.greetingText = "";

        $scope.loginPopover = {
            content: '',
            templateUrl: 'myPopoverTemplate.html',
            title: 'Log in'
        };

        //var myPopover = $popover(document.getElementById('loginPrompt'),
        //    { html: true,  title: 'My Title', content: 'My Content' });

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

        self.login = function () {
            console.log('here');
        };

        self.getCurrentUser();
    }
})();