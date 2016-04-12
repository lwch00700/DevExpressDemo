using DevExpress.DemoData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagramDemo.Data {
    public static class OrderDataGenerator {
        static List<CustomerData> ExtractCustomers(int count) {
            var customers = NWindContext.Create().Customers.Take(count).ToList();
            var customerData = new List<CustomerData>(count);
            foreach(var row in customers) {
                customerData.Add(new CustomerData(row.ContactName, row.CompanyName, row.Phone, row.City));
            }
            return customerData;
        }
        static List<CategoryData> ExtractCategoryDataList(int count) {
            List<CategoryData> categoryData = new List<CategoryData>(count);
            var categories = NWindContext.Create().Categories.Take(count).ToList();
            foreach(var row in categories) {
                categoryData.Add(new CategoryData(row.CategoryName, row.Description, row.Icon17));
            }
            return categoryData;
        }
        static List<ProductData> ExtractProductDataList(List<CategoryData> categoriesList) {
            List<ProductData> productData = new List<ProductData>();
            var categoryProducts = NWindContext.Create().CategoryProducts.ToList();
            productData.Capacity = categoryProducts.Count;
            Random rand = new Random();
            foreach(var row in categoryProducts) {
                var category = FindCategory(categoriesList, row.CategoryName);
                if(category == null)
                    continue;
                productData.Add(new ProductData() {
                    Category = category,
                    Name = row.ProductName,
                    Price = (decimal)(rand.Next(20) + rand.Next(99)) / 100m
                });
            }
            return productData;
        }

        static CategoryData FindCategory(List<CategoryData> categoriesList, string name) {
            foreach(CategoryData category in categoriesList) {
                if(category.Name == name) return category;
            }
            return null;
        }

        public static List<OrderData> GenerateOrders(int orderCount, int customerCount, int categoryCount) {
            List<OrderData> result = new List<OrderData>(orderCount);
            List<CustomerData> customers = ExtractCustomers(customerCount);
            List<CategoryData> categoriesList = ExtractCategoryDataList(categoryCount);
            List<ProductData> productsList = ExtractProductDataList(categoriesList);

            Random rand = new Random();
            int generateCountPerCent = orderCount / 100;
            for(int i = 0; i < orderCount; i++) {
                ProductData randomProduct = productsList[rand.Next(productsList.Count)];
                OrderData data = new OrderData();
                data.OrderId = i;
                data.OrderDate = DateTime.Today.Subtract(TimeSpan.FromDays(rand.Next(180)));
                data.Customer = customers[rand.Next(customers.Count)];
                data.Quantity = rand.Next(200) + 1;
                data.ProductCategory = randomProduct.Category;
                data.ProductName = randomProduct.Name;
                data.Price = randomProduct.Price;
                data.IsReady = (rand.Next(2) == 0);
                result.Add(data);
            }
            return result;
        }
    }
    public class CategoryData : IComparable, IComparable<CategoryData> {
        readonly string name;
        readonly string description;
        readonly byte[] picture;

        public string Name { get { return name; } }
        public string Description { get { return description; } }
        public byte[] Picture { get { return picture; } }

        public CategoryData(string name, string description, byte[] picture) {
            this.name = name;
            this.description = description;
            this.picture = picture;
        }

        public override string ToString() {
            return Name;
        }

        #region IComparable Members
        public int CompareTo(object obj) {
            if(obj is CategoryData)
                return CompareTo((CategoryData)obj);
            return -1;
        }
        #endregion
        #region IComparable<CategoryData> Members
        public int CompareTo(CategoryData other) {
            return StringComparer.CurrentCulture.Compare(Name, other.Name);
        }
        #endregion
    }
    public class ProductData {
        public string Name { get; set; }
        public CategoryData Category { get; set; }
        public decimal Price { get; set; }
        public override string ToString() {
            return Name;
        }
    }
    public class CustomerData {
        readonly string name;
        readonly string companyName;
        readonly string phoneNumber;
        readonly string city;

        public string Name { get { return name; } }
        public string CompanyName { get { return companyName; } }
        public string PhoneNumber { get { return phoneNumber; } }
        public string City { get { return city; } }

        public CustomerData(string name, string companyName, string phoneNumber, string city) {
            this.name = name;
            this.companyName = companyName;
            this.phoneNumber = phoneNumber;
            this.city = city;
        }
    }
    public class OrderData {
        public int OrderId { get; set; }
        public bool IsReady { get; set; }
        public CustomerData Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public CategoryData ProductCategory { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
