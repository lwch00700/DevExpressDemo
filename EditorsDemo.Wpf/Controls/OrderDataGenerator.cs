using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.DemoBase;
using System.Data;
using DevExpress.Xpf.DemoBase.DataClasses;
using EmployeesWithPhotoData = DevExpress.Xpf.DemoBase.DataClasses.EmployeesWithPhotoData;
using Employee = DevExpress.Xpf.DemoBase.DataClasses.Employee;
using Order = DevExpress.DemoData.Models.Order;

namespace GridDemo {
    public class OrderDataGenerator {
        static object SyncRoot = new object();
        static List<string> customerNames = new List<string>();
        static List<CategoryData> categoryData = new List<CategoryData>();
        static List<ProductData> productData = new List<ProductData>();
        int count;
        List<OrderData> cachedOrders = new List<OrderData>();

        static List<string> ExtractCustomerNames() {
            if(customerNames.Count == 0) {
                var customers = NWindContext.Create().Customers.ToList();
                customerNames.Capacity = customers.Count;
                foreach(var row in customers) {
                    customerNames.Add(row.ContactName);
                }
            }
            return customerNames;
        }
        public static List<CategoryData> ExtractCategoryDataList() {
            if(categoryData.Count == 0) {
                var categories = NWindContext.Create().Categories.ToList();
                categoryData.Capacity = categories.Count;
                foreach(var row in categories) {
                    categoryData.Add(new CategoryData() {
                        Name = row.CategoryName,
                        Picture = row.Icon25
                    });
                }
            }
            return categoryData;
        }
        public static List<ProductData> ExtractProductDataList(List<CategoryData> categoriesList) {
            if(productData.Count == 0) {
                var categoryProducts = NWindContext.Create().CategoryProducts.ToList();
                productData.Capacity = categoryProducts.Count;
                Random rand = new Random();
                foreach(var row in categoryProducts) {
                    productData.Add(new ProductData() {
                        Category = FindCategory(categoriesList, row.CategoryName),
                        Name = row.ProductName,
                        Price = (decimal)(rand.Next(20) + rand.Next(99)) / 100m
                    });
                }
            }
            return productData;
        }

        static CategoryData FindCategory(List<CategoryData> categoriesList, string name) {
            foreach(CategoryData category in categoriesList) {
                if(category.Name == name) return category;
            }
            return null;
        }

        List<OrderData> GenerateOrders(int generateCount, int startFrom) {
            List<OrderData> result = new List<OrderData>(generateCount);
            List<string> customerNames = ExtractCustomerNames();
            List<CategoryData> categoriesList = ExtractCategoryDataList();
            List<ProductData> productsList = ExtractProductDataList(categoriesList);

            OnGenerateOrderDataStarted(EventArgs.Empty);
            Random rand = new Random();
            int generateCountPerCent = generateCount / 100;
            for(int i = 0; i < generateCount; i++) {
                ProductData randomProduct = productsList[rand.Next(productsList.Count)];
                string randomName = customerNames[rand.Next(customerNames.Count)];
                OrderData data = new OrderData();
                data.OrderId = i + startFrom;
                data.OrderDate = DateTime.Today.Subtract(TimeSpan.FromDays(rand.Next(180)));
                data.CustomerName = randomName;
                data.Quantity = rand.Next(200) + 1;
                data.CustomerID = i + 1;
                data.ProductCategory = randomProduct.Category;
                data.ProductName = randomProduct.Name;
                data.Price = randomProduct.Price;
                data.IsReady = (rand.Next(2) == 0);
                result.Add(data);
                if(((i + 1) % generateCountPerCent) == 0) {
                    OnGenerateOrderDataProgress(new GenerateOrderDataProgressEventArgs(Convert.ToDouble((i + 1) / generateCountPerCent)));
                }
            }
            OnGenerateOrderDataCompleted(EventArgs.Empty);
            return result;
        }

        protected virtual void OnGenerateOrderDataStarted(EventArgs e) {
            if(GenerateOrderDataStarted != null) {
                GenerateOrderDataStarted(this, e);
            }
        }
        protected virtual void OnGenerateOrderDataCompleted(EventArgs e) {
            if(GenerateOrderDataCompleted != null) {
                GenerateOrderDataCompleted(this, e);
            }
        }
        protected virtual void OnGenerateOrderDataProgress(GenerateOrderDataProgressEventArgs e) {
            if(GenerateOrderDataProgress != null) {
                GenerateOrderDataProgress(this, e);
            }
        }

        public OrderDataGenerator(int count) {
            this.count = count;
        }

        public int Count {
            get { return count; }
            set { count = value; }
        }

        public List<OrderData> GetOrders() {
            List<OrderData> result;
            lock(SyncRoot) {
                if(Count > cachedOrders.Count) {
                    cachedOrders.AddRange(GenerateOrders(Count - cachedOrders.Count, cachedOrders.Count + 1));
                }
                result = cachedOrders.GetRange(0, Count);
            }
            return result;
        }
        public List<CategoryData> GetCategories() {
            return ExtractCategoryDataList();
        }

        public event EventHandler GenerateOrderDataStarted;
        public event EventHandler GenerateOrderDataCompleted;
        public event EventHandler<GenerateOrderDataProgressEventArgs> GenerateOrderDataProgress;
    }

    public class GenerateOrderDataProgressEventArgs : EventArgs {
        double progress;

        public GenerateOrderDataProgressEventArgs(double progress) {
            this.progress = progress;
        }
        public double Progress {
            get { return progress; }
        }
    }

    public class CategoryData : IComparable, IComparable<CategoryData> {
        public string Name { get; set; }
        public byte[] Picture { get; set; }
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

    public class OrderData {
        public int OrderId { get; set; }
        public bool IsReady { get; set; }
        public string CustomerName { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public CategoryData ProductCategory { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class EmployeesWithChartSource {
        public EmployeesWithChartSource() {
            LoadData();
        }

        void LoadData() {
            Employees = new List<EmployeeWithChart>();
            List<Employee> employees = EmployeesWithPhotoData.DataSource;
            Dictionary<int, int> dict = EmployeesWithPhotoData.OrdersRelationsDictionary;
            NWindContext context = NWindContext.Create();
            List<Order> orders = context.Orders.ToList();
            List<Invoice> invoices = context.Invoices.ToList();
            foreach(Employee empl in employees) {
                List<ChartPoint> chartPoints = new List<ChartPoint>();
                foreach(Order order in orders.Where(o => dict[(int)o.OrderID] == empl.Id)) {
                    ChartPoint cp = new ChartPoint();
                    cp.ArgumentMember = order.OrderDate;
                    invoices.Where(invoice => invoice.OrderID == order.OrderID).ToList().ForEach(invoice => cp.ValueMember += Convert.ToInt32(invoice.Quantity * invoice.UnitPrice));
                    chartPoints.Add(cp);
                }
                Employees.Add(new EmployeeWithChart(empl, chartPoints));
            }
        }

        public List<EmployeeWithChart> Employees { get; private set; }
    }
    public class EmployeeWithChart {
        public EmployeeWithChart(Employee employee, List<ChartPoint> chartSource) {
            ChartSource = chartSource;
            FullName = String.Format("{0} {1}", employee.FirstName, employee.LastName);
            JobTitle = employee.JobTitle;
            CountryRegionName = employee.CountryRegionName;
            BirthDate = employee.BirthDate;
            EmailAddress = employee.EmailAddress;
        }

        public string JobTitle { get; private set; }
        public string CountryRegionName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string EmailAddress { get; private set; }

        public string FullName { get; private set; }
        public List<ChartPoint> ChartSource { get; private set; }
    }
    public class ChartPoint {
        public DateTime? ArgumentMember { get; internal set; }
        public int ValueMember { get; set; }
    }
}
