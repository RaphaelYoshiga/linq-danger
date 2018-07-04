using System.Collections.Generic;
using System.Linq;

namespace Outside.LinqPerformanceExamples
{
    class StockConsolidator
    {
        public static IEnumerable<ProductStock> ConsolidateStocksUsingDictionary(List<ProductStock> productStocks, List<ProductStock> anotherProductStocks)
        {
            var anotherProductStocksAsDictionary = anotherProductStocks.ToDictionary(p => p.Sku, p => p);
            foreach (var productStock in productStocks)
            {
                var otherProductStock = anotherProductStocksAsDictionary[productStock.Sku];
                yield return new ProductStock
                {
                    Sku = productStock.Sku,
                    StockLevel = productStock.StockLevel + otherProductStock.StockLevel
                };
            }
        }

        public static IEnumerable<ProductStock> ConsolidateStocks(List<ProductStock> productStocks, List<ProductStock> otherProductStocks)
        {
            foreach (var productStock in productStocks)
            {
                var otherProductStock = otherProductStocks.First(p => p.Sku == productStock.Sku);
                yield return new ProductStock
                {
                    Sku = productStock.Sku,
                    StockLevel = productStock.StockLevel + otherProductStock.StockLevel
                };
            }
        }
    }
}