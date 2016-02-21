angular.module("stockTicker").factory("stockHubProxy", [
	"$rootScope", "stockUrl",
	function ($rootScope, stockUrl) {
		function stockFactory(serverUrl, hubName) {
			var connection = $.hubConnection(stockUrl);
			var proxy = connection.createHubProxy(hubName);

			return {
				on: function (eventName, callback) {
					proxy.on(eventName, function (result) {
						$rootScope.$apply(function () {
							if (callback) {
								callback(result);
							}
						});
					});
				},
				invoke: function (methodName, callback) {
					proxy.invoke(methodName)
						.done(function (result) {
							$rootScope.$apply(function () {
								if (callback) {
									callback(result);
								}
							});
						});
				},
				start: function (onInitCallback) {
					connection.start().done(function () {
						if (onInitCallback) {
							onInitCallback();
						}
					});
				}
			};
		}

		return stockFactory;
	}
]);