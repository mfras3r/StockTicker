var app = angular.module("stockTicker", []);
app.value("stockUrl", "http://pipeline/");
app.factory("stockHubProxy", [
	"$rootScope", "stockUrl",
	function($rootScope, stockUrl) {
		function stockFactory(serverUrl, hubName) {
			var connection = $.hubConnection(stockUrl);
			var proxy = connection.createHubProxy(hubName);

			return {
				on: function(eventName, callback) {
					proxy.on(eventName, function(result) {
						$rootScope.$apply(function() {
							if (callback) {
								callback(result);
							}
						});
					});
				},
				invoke: function(methodName, callback) {
					proxy.invoke(methodName)
						.done(function(result) {
							$rootScope.$apply(function() {
								if (callback) {
									callback(result);
								}
							});
						});
				},
				start: function(onInitCallback) {
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
app.controller("StockController", [
	"$scope", "$log", "stockHubProxy",
	function($scope, $log, stockHubProxy) {
		function setStock(stock) {
			$scope.stocks[stock.Symbol] = {
				open: stock.DayOpen,
				price: stock.Price,
				change: stock.Change,
				percentChange: stock.PercentChange,
				direction: stock.Change === 0 ? '' : stock.Change > 0 ? "▲" : "▼"
			};
		}

		$scope.stocks = {};
		$log.log("Trying to connect to stock service.");

		var stockDataHub = stockHubProxy(stockHubProxy.defaultServer, "stockTicker");
		stockDataHub.on("updateStockPrice", function (stock) {
			setStock(stock);
			//$log.log("updateStockPrice", stock);
		});

		stockDataHub.start(function () {
			stockDataHub.invoke("getAllStocks", function (stocks) {
				_.forEach(stocks, function(stock) {
					setStock(stock);
				});
				//$log.log("getAllStocks", stocks);
			});
		});
		$log.log("Connected to stock service.");
	}
]);

app.filter('percentage', ['$filter', function ($filter) {
	return function (input, decimals) {
		return $filter('number')(input * 100, decimals) + '%';
	};
}]);