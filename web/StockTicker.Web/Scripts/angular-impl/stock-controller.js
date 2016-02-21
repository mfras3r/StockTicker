angular.module("stockTicker").controller("StockController", [
	"$scope", "$log", "stockHubProxy",
	function ($scope, $log, stockHubProxy) {
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
				_.forEach(stocks, function (stock) {
					setStock(stock);
				});
				//$log.log("getAllStocks", stocks);
			});
		});
		$log.log("Connected to stock service.");
	}
]);