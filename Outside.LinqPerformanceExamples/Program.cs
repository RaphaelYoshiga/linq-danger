using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Outside.LinqPerformanceExamples
{
    class Program
    {
        private static readonly Random _random = new Random();

        static void Main(string[] args)
        {
            var numberOfProducts = 100000;

            Console.WriteLine($"Starting to consolidate. Number of products: {numberOfProducts}");
            GenerateTwoDifferentProductStocksSetsAndConsolidate(numberOfProducts);
            Console.Read();
        }

        private static void GenerateTwoDifferentProductStocksSetsAndConsolidate(int numberOfProducts)
        {
            var productStocks = GenerateProductStocks(numberOfProducts);
            var anotherProductStocks = GenerateProductStocks(numberOfProducts);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var consolidatedStocks = StockConsolidator.ConsolidateStocks(productStocks, anotherProductStocks).ToList();
            Console.WriteLine($"Time taken to consolidate {stopwatch.Elapsed.TotalMilliseconds} ms");

            stopwatch.Restart();
            var consolidatedWithDictionaryu = StockConsolidator.ConsolidateStocksUsingDictionary(productStocks, anotherProductStocks).ToList();
            Console.WriteLine($"Time taken to consolidate with dictionary {stopwatch.Elapsed.TotalMilliseconds} ms");
        }

        private static List<ProductStock> GenerateProductStocks(int numberOfProducts)
        {
            var productStocks = new List<ProductStock>();
            for (int i = 0; i < numberOfProducts; i++)
            {
                var productStock = new ProductStock()
                {
                    Sku = i,
                    StockLevel = _random.Next(0, 500)
                };
                productStocks.Add(productStock);
            }

            return productStocks;
        }
    }

    public class ProductStock
    {
        public int Sku { get; set; }
        public int StockLevel { get; set; }
    }
}
