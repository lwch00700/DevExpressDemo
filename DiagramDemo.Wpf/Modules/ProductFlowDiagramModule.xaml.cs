using DevExpress.Data.Filtering;
using DevExpress.Diagram.Core;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using DiagramDemo.Data;

namespace DiagramDemo {
    public partial class ProductFlowDiagramModule : DiagramDemoModule {
  #region Static

        public static readonly ReadOnlyCollection<OrderData> Data;
        public static readonly ReadOnlyCollection<CustomerData> Customers;
        public static readonly ReadOnlyCollection<CategoryData> ProductCategories;

        static ProductFlowDiagramModule() {
            var orders = OrderDataGenerator.GenerateOrders(50, 4, 5);
            var customers = orders.Select(x => x.Customer).Distinct().ToArray();
            var categories = orders.Select(x => x.ProductCategory).Distinct().ToArray();

            Customers = new ReadOnlyCollection<CustomerData>(customers);
            ProductCategories = new ReadOnlyCollection<CategoryData>(categories);
            Data = new ReadOnlyCollection<OrderData>(orders);
        }

        #endregion

        public ProductFlowDiagramModule() {
            InitializeComponent();
            CreateCustomers();
            CreateProducts();
            CreateConnections();
            diagramControl.SelectItem(diagramControl.Items.First());
        }

  #region Diagram items creation

        void CreateProducts() {
            AddItemsInLine(ProductCategories, new Point(600, 50), new Size(150, 105), 20, (product, item) => {
                ItemToProductCache[item] = product;
            });
        }
        void CreateCustomers() {
            var stylesEnumerator = DiagramShapeStyleId.Styles.GetEnumerator();
            AddItemsInLine(Customers, new Point(50, 100), new Size(150, 105), 20, (warehouse, item) => {
                stylesEnumerator.MoveNext();
                item.ThemeStyleId = stylesEnumerator.Current;
                ItemToWarehouseCache[item] = warehouse;
            });
        }
        void AddItemsInLine<T>(IEnumerable<T> itemsSource, Point startPosition, Size itemSize, double margin, Action<T, DiagramContentItem> itemAction) {
            Point position = startPosition;
            foreach(var dataItem in itemsSource) {
                var diagramItem = new DiagramContentItem() { Width = itemSize.Width, Height = itemSize.Height };
                diagramItem.CanCopy = diagramItem.CanDelete = false;
                diagramItem.Content = dataItem;
                diagramItem.Position = position;
                diagramItem.FontSize = 11;
                position.Offset(0, itemSize.Height + margin);
                diagramControl.Items.Add(diagramItem);
    itemAction(dataItem, diagramItem);
            }
        }
        void CreateConnections() {
            var contentItems = diagramControl.Items.OfType<DiagramContentItem>();
            const double minThickness = 1, maxThickness = 8;
            var groupedData = Data.GroupBy(x => new { x.Customer, x.ProductCategory }).ToArray();
            double minCount = groupedData.Min(x => x.Sum(d => d.Quantity));
            double maxCount = groupedData.Max(x => x.Sum(d => d.Quantity));
            foreach(var dataItem in groupedData) {
                var warehouseItem = contentItems.First(x => object.Equals(x.Content, dataItem.Key.Customer));
                var productItem = contentItems.First(x => object.Equals(x.Content, dataItem.Key.ProductCategory));
                var connector = new DiagramConnector() { BeginArrow = null, EndArrow = null };
                diagramControl.Items.Add(connector);
                connector.ThemeStyleId = warehouseItem.ThemeStyleId;
                connector.BeginItem = warehouseItem;
                connector.BeginItemPointIndex = 1;
                connector.EndItem = productItem;
                connector.EndItemPointIndex = 3;
                connector.Type = ConnectorType.Curved;
                double quantity = dataItem.Sum(x => x.Quantity);
                connector.StrokeThickness = minThickness + (maxThickness - minThickness) * (quantity - minCount) / (maxCount - minCount);
            }
        }

        #endregion
        #region Diagram items cache

        readonly Dictionary<IDiagramItem, CategoryData> ItemToProductCache = new Dictionary<IDiagramItem, CategoryData>();
        readonly Dictionary<IDiagramItem, CustomerData> ItemToWarehouseCache = new Dictionary<IDiagramItem, CustomerData>();

        CustomerData GetCustomerByItem(IDiagramItem item) {
            if(item == null)
                return null;
            CustomerData result;
            ItemToWarehouseCache.TryGetValue(item, out result);
            return result;
        }
        CategoryData GetProductByItem(IDiagramItem item) {
            if(item == null)
                return null;
            CategoryData result;
            ItemToProductCache.TryGetValue(item, out result);
            return result;
        }
        CustomerData GetCustomerByConnector(DiagramConnector connector) {
            return GetCustomerByItem(connector.BeginItem);
        }
        CategoryData GetProductByConnector(DiagramConnector connector) {
            return GetProductByItem(connector.EndItem);
        }

        #endregion

        void SelectionChanged(object sender, EventArgs e) {
            var selectedDiagramItem = diagramControl.PrimarySelection;
            gridControl.FilterCriteria = null;
            gridControl.ClearGrouping();
            if(GetCustomerByItem(selectedDiagramItem) != null) {
                gridControl.FilterCriteria = new BinaryOperator("Customer.Name", GetCustomerByItem(selectedDiagramItem).Name, BinaryOperatorType.Equal);
                gridControl.GroupBy("ProductCategory.Name");
            }
            else if(GetProductByItem(selectedDiagramItem) != null) {
                gridControl.FilterCriteria = new BinaryOperator("ProductCategory.Name", GetProductByItem(selectedDiagramItem).Name, BinaryOperatorType.Equal);
                gridControl.GroupBy("Customer.Name");
            }
            else if(selectedDiagramItem is DiagramConnector) {
                var connector = (DiagramConnector)selectedDiagramItem;
                var product = GetProductByConnector(connector);
                var customer = GetCustomerByConnector(connector);
                if(product != null && customer != null) {
                    var productFilter = new BinaryOperator("ProductCategory.Name", product.Name, BinaryOperatorType.Equal);
                    var customerFilter = new BinaryOperator("Customer.Name", customer.Name, BinaryOperatorType.Equal);
                    gridControl.FilterCriteria = new GroupOperator(GroupOperatorType.And, productFilter, customerFilter);
                }
            }
        }
    }
    public class FontSizeConverter : IValueConverter {
        public double FontSizeFactor { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (double)value * FontSizeFactor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
