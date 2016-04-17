(function () {
	angular.module('forumApp').controller('adminController', adminController);

	adminController.$inject = ['$http', '$rootScope'];

	function adminController($http, $rootScope) {
		var self = this;
		self.settings = [];
		self.isDenied = true;

		$rootScope.pageTitle = "Admin";

		self.loadSettings = function () {
			$http.get('/settings')
                .success(function (res) {
                	self.isDenied = false;
                	self.settings = res;
                })
                .error(function (res) {
                	console.log(res);
                	self.isDenied = true;
                });
		};

		self.applyValues = function () {
			$http.post('/settings', self.settings)
                .success(function () {
                	console.log('Successfully saved');
                });

		};

		self.loadSettings();
	}
})();