using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.DemoData.Models;

namespace GridDemo {
    public class ProductSaleDataGenerator {
        readonly Random random = new Random();
        readonly List<string> products = null;

        public ProductSaleDataGenerator() {
            products = NWindContext.Create().Invoices.Select(invoice => invoice.ProductName).Distinct().ToList();
        }

        public List<ProductSaleData> GenerateSales(int count) {
            List<ProductSaleData> sales = new List<ProductSaleData>();
            var countries = NWindContext.Create().CountriesArray;
            return Enumerable.Range(0, count).Select(index => {
                return CreateSale(countries[index % countries.Length], products[index % products.Count], DateTime.Today.Year - index % 9);
            }).ToList();
        }
        public ProductSaleData CreateSale(string country, string product, int year) {
            var sale = new ProductSaleData();
            sale.Country = country;
            sale.ProductName = product;
            sale.Year = year;
            sale.Sales = random.Next(50000000, 500000000);
            sale.SalesVsTarget = (random.NextDouble() - 0.5) / 5;
            sale.Profit = random.Next(-30000000, 50000000);
            sale.CustomersSatisfaction = Math.Round((random.NextDouble() + 1) * 2.5, 1);
            sale.MarketShare = random.NextDouble() / 3 + 0.15;
            return sale;
        }
    }
}
