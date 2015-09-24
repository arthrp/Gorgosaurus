(function () {
    angular.module('forumApp').controller('accountController', accountController);

    accountController.$inject = ['$scope', '$http'];

    function accountController($scope, $http) {
        var self = this;

        self.loginInfo = { username: "Tester", password: "" };
        self.loggedInUsername = "";
        self.greetingText = "";

        self.isLoggedIn = false;

        $scope.loginPopover = {
            templateUrl: 'myPopoverTemplate.html',
            title: 'Log in'
        };

        self.init = function(){
            self.getCurrentUser();
        };

        self.getCurrentUser = function () {
            $http.get("/account/current").success(function (resp) {
                if (!resp) {
                    self.loggedInUsername = "";
                    self.isLoggedIn = false;
                }
                else {
                    self.loggedInUsername = resp;
                    self.isLoggedIn = true;                  
                }
                
                self.updateGreetingText();
                //console.log(self.greetingText);
            });
        };

        self.updateGreetingText = function () {
            if(self.isLoggedIn){
                self.greetingText = "Hello, "+self.loggedInUsername;
            }
            else{
                 self.greetingText = "Hello, guest";
            }
        };

        self.login = function () {
            var loginData = { username: self.loginInfo.username, password: self.loginInfo.password };

            $http.post("/account/login", loginData)
                .success(function (res) {
                    console.log(res);
                    
                    self.getCurrentUser();
                });            
        };

        self.logout = function () {
            $http.post("/account/logout")
                .success(function (res) {
                    console.log(res);

                    self.getCurrentUser();
                });              
        };

        self.init();
    }
})();