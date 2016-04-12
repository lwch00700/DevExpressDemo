#pragma warning disable 1591

namespace PivotGridDemo.Controls
{
 using System.Data.Linq;
 using System.Data.Linq.Mapping;
 using System.Data;
 using System.Collections.Generic;
 using System.Reflection;
 using System.Linq;
 using System.Linq.Expressions;
 using System.ComponentModel;
 using System;


 [global::System.Data.Linq.Mapping.DatabaseAttribute(Name="PivotGridDemoDB")]
 public partial class PivotGridDemoDBDataContext : System.Data.Linq.DataContext
 {

  private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCategory(Category instance);
    partial void UpdateCategory(Category instance);
    partial void DeleteCategory(Category instance);
    partial void InsertSalesPerson(SalesPerson instance);
    partial void UpdateSalesPerson(SalesPerson instance);
    partial void DeleteSalesPerson(SalesPerson instance);
    partial void InsertCustomer(Customer instance);
    partial void UpdateCustomer(Customer instance);
    partial void DeleteCustomer(Customer instance);
    partial void InsertOrder(Order instance);
    partial void UpdateOrder(Order instance);
    partial void DeleteOrder(Order instance);
    partial void InsertProduct(Product instance);
    partial void UpdateProduct(Product instance);
    partial void DeleteProduct(Product instance);
    partial void InsertSale(Sale instance);
    partial void UpdateSale(Sale instance);
    partial void DeleteSale(Sale instance);
    #endregion

  public PivotGridDemoDBDataContext(string connection) :
    base(connection, mappingSource)
  {
   OnCreated();
  }

  public PivotGridDemoDBDataContext(System.Data.IDbConnection connection) :
    base(connection, mappingSource)
  {
   OnCreated();
  }

  public PivotGridDemoDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
    base(connection, mappingSource)
  {
   OnCreated();
  }

  public PivotGridDemoDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
    base(connection, mappingSource)
  {
   OnCreated();
  }

  public System.Data.Linq.Table<Category> Categories
  {
   get
   {
    return this.GetTable<Category>();
   }
  }

  public System.Data.Linq.Table<SalesPerson> SalesPersons
  {
   get
   {
    return this.GetTable<SalesPerson>();
   }
  }

  public System.Data.Linq.Table<Customer> Customers
  {
   get
   {
    return this.GetTable<Customer>();
   }
  }

  public System.Data.Linq.Table<Order> Orders
  {
   get
   {
    return this.GetTable<Order>();
   }
  }

  public System.Data.Linq.Table<Product> Products
  {
   get
   {
    return this.GetTable<Product>();
   }
  }

  public System.Data.Linq.Table<Sale> Sales
  {
   get
   {
    return this.GetTable<Sale>();
   }
  }
 }

 [global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Categories")]
 public partial class Category : INotifyPropertyChanging, INotifyPropertyChanged
 {

  private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

  private int _OID;

  private string _CategoryName;

  private EntitySet<Product> _Products;

    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCategoryIDChanging(int value);
    partial void OnCategoryIDChanged();
    partial void OnCategoryNameChanging(string value);
    partial void OnCategoryNameChanged();
    #endregion

  public Category()
  {
   this._Products = new EntitySet<Product>(new Action<Product>(this.attach_Products), new Action<Product>(this.detach_Products));
   OnCreated();
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Name="OID", Storage="_OID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
  public int CategoryID
  {
   get
   {
    return this._OID;
   }
   set
   {
    if ((this._OID != value))
    {
     this.OnCategoryIDChanging(value);
     this.SendPropertyChanging();
     this._OID = value;
     this.SendPropertyChanged("CategoryID");
     this.OnCategoryIDChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryName", DbType="NVarChar(100)")]
  public string CategoryName
  {
   get
   {
    return this._CategoryName;
   }
   set
   {
    if ((this._CategoryName != value))
    {
     this.OnCategoryNameChanging(value);
     this.SendPropertyChanging();
     this._CategoryName = value;
     this.SendPropertyChanged("CategoryName");
     this.OnCategoryNameChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.AssociationAttribute(Name="Category_Product", Storage="_Products", ThisKey="CategoryID", OtherKey="CategoryID")]
  public EntitySet<Product> Products
  {
   get
   {
    return this._Products;
   }
   set
   {
    this._Products.Assign(value);
   }
  }

  public event PropertyChangingEventHandler PropertyChanging;

  public event PropertyChangedEventHandler PropertyChanged;

  protected virtual void SendPropertyChanging()
  {
   if ((this.PropertyChanging != null))
   {
    this.PropertyChanging(this, emptyChangingEventArgs);
   }
  }

  protected virtual void SendPropertyChanged(String propertyName)
  {
   if ((this.PropertyChanged != null))
   {
    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
   }
  }

  private void attach_Products(Product entity)
  {
   this.SendPropertyChanging();
   entity.Category = this;
  }

  private void detach_Products(Product entity)
  {
   this.SendPropertyChanging();
   entity.Category = null;
  }
 }

 [global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SalesPeople")]
 public partial class SalesPerson : INotifyPropertyChanging, INotifyPropertyChanged
 {

  private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

  private int _OID;

  private string _SalesPersonName;

  private EntitySet<Order> _Orders;

    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSalesPersonIDChanging(int value);
    partial void OnSalesPersonIDChanged();
    partial void OnSalesPersonNameChanging(string value);
    partial void OnSalesPersonNameChanged();
    #endregion

  public SalesPerson()
  {
   this._Orders = new EntitySet<Order>(new Action<Order>(this.attach_Orders), new Action<Order>(this.detach_Orders));
   OnCreated();
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Name="OID", Storage="_OID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
  public int SalesPersonID
  {
   get
   {
    return this._OID;
   }
   set
   {
    if ((this._OID != value))
    {
     this.OnSalesPersonIDChanging(value);
     this.SendPropertyChanging();
     this._OID = value;
     this.SendPropertyChanged("SalesPersonID");
     this.OnSalesPersonIDChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SalesPersonName", DbType="NVarChar(100)")]
  public string SalesPersonName
  {
   get
   {
    return this._SalesPersonName;
   }
   set
   {
    if ((this._SalesPersonName != value))
    {
     this.OnSalesPersonNameChanging(value);
     this.SendPropertyChanging();
     this._SalesPersonName = value;
     this.SendPropertyChanged("SalesPersonName");
     this.OnSalesPersonNameChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.AssociationAttribute(Name="SalesPeople_Order", Storage="_Orders", ThisKey="SalesPersonID", OtherKey="SalesPersonID")]
  public EntitySet<Order> Orders
  {
   get
   {
    return this._Orders;
   }
   set
   {
    this._Orders.Assign(value);
   }
  }

  public event PropertyChangingEventHandler PropertyChanging;

  public event PropertyChangedEventHandler PropertyChanged;

  protected virtual void SendPropertyChanging()
  {
   if ((this.PropertyChanging != null))
   {
    this.PropertyChanging(this, emptyChangingEventArgs);
   }
  }

  protected virtual void SendPropertyChanged(String propertyName)
  {
   if ((this.PropertyChanged != null))
   {
    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
   }
  }

  private void attach_Orders(Order entity)
  {
   this.SendPropertyChanging();
   entity.SalesPerson = this;
  }

  private void detach_Orders(Order entity)
  {
   this.SendPropertyChanging();
   entity.SalesPerson = null;
  }
 }

 [global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Customers")]
 public partial class Customer : INotifyPropertyChanging, INotifyPropertyChanged
 {

  private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

  private int _OID;

  private string _CustomerName;

  private EntitySet<Order> _Orders;

    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCustomerIDChanging(int value);
    partial void OnCustomerIDChanged();
    partial void OnCustomerNameChanging(string value);
    partial void OnCustomerNameChanged();
    #endregion

  public Customer()
  {
   this._Orders = new EntitySet<Order>(new Action<Order>(this.attach_Orders), new Action<Order>(this.detach_Orders));
   OnCreated();
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Name="OID", Storage="_OID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
  public int CustomerID
  {
   get
   {
    return this._OID;
   }
   set
   {
    if ((this._OID != value))
    {
     this.OnCustomerIDChanging(value);
     this.SendPropertyChanging();
     this._OID = value;
     this.SendPropertyChanged("CustomerID");
     this.OnCustomerIDChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerName", DbType="NVarChar(100)")]
  public string CustomerName
  {
   get
   {
    return this._CustomerName;
   }
   set
   {
    if ((this._CustomerName != value))
    {
     this.OnCustomerNameChanging(value);
     this.SendPropertyChanging();
     this._CustomerName = value;
     this.SendPropertyChanged("CustomerName");
     this.OnCustomerNameChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.AssociationAttribute(Name="Customer_Order", Storage="_Orders", ThisKey="CustomerID", OtherKey="CustomerID")]
  public EntitySet<Order> Orders
  {
   get
   {
    return this._Orders;
   }
   set
   {
    this._Orders.Assign(value);
   }
  }

  public event PropertyChangingEventHandler PropertyChanging;

  public event PropertyChangedEventHandler PropertyChanged;

  protected virtual void SendPropertyChanging()
  {
   if ((this.PropertyChanging != null))
   {
    this.PropertyChanging(this, emptyChangingEventArgs);
   }
  }

  protected virtual void SendPropertyChanged(String propertyName)
  {
   if ((this.PropertyChanged != null))
   {
    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
   }
  }

  private void attach_Orders(Order entity)
  {
   this.SendPropertyChanging();
   entity.Customer = this;
  }

  private void detach_Orders(Order entity)
  {
   this.SendPropertyChanging();
   entity.Customer = null;
  }
 }

 [global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Orders")]
 public partial class Order : INotifyPropertyChanging, INotifyPropertyChanged
 {

  private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

  private int _OID;

  private System.Nullable<int> _SalesPerson;

  private System.Nullable<int> _Customer;

  private System.Nullable<System.DateTime> _OrderDate;

  private EntitySet<Sale> _Sales;

  private EntityRef<Customer> _Customer1;

  private EntityRef<SalesPerson> _SalesPeople;

    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnOrderIDChanging(int value);
    partial void OnOrderIDChanged();
    partial void OnSalesPersonIDChanging(System.Nullable<int> value);
    partial void OnSalesPersonIDChanged();
    partial void OnCustomerIDChanging(System.Nullable<int> value);
    partial void OnCustomerIDChanged();
    partial void OnOrderDateChanging(System.Nullable<System.DateTime> value);
    partial void OnOrderDateChanged();
    #endregion

  public Order()
  {
   this._Sales = new EntitySet<Sale>(new Action<Sale>(this.attach_Sales), new Action<Sale>(this.detach_Sales));
   this._Customer1 = default(EntityRef<Customer>);
   this._SalesPeople = default(EntityRef<SalesPerson>);
   OnCreated();
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Name="OID", Storage="_OID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
  public int OrderID
  {
   get
   {
    return this._OID;
   }
   set
   {
    if ((this._OID != value))
    {
     this.OnOrderIDChanging(value);
     this.SendPropertyChanging();
     this._OID = value;
     this.SendPropertyChanged("OrderID");
     this.OnOrderIDChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Name="SalesPerson", Storage="_SalesPerson", DbType="Int")]
  public System.Nullable<int> SalesPersonID
  {
   get
   {
    return this._SalesPerson;
   }
   set
   {
    if ((this._SalesPerson != value))
    {
     this.OnSalesPersonIDChanging(value);
     this.SendPropertyChanging();
     this._SalesPerson = value;
     this.SendPropertyChanged("SalesPersonID");
     this.OnSalesPersonIDChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Name="Customer", Storage="_Customer", DbType="Int")]
  public System.Nullable<int> CustomerID
  {
   get
   {
    return this._Customer;
   }
   set
   {
    if ((this._Customer != value))
    {
     this.OnCustomerIDChanging(value);
     this.SendPropertyChanging();
     this._Customer = value;
     this.SendPropertyChanged("CustomerID");
     this.OnCustomerIDChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderDate", DbType="DateTime")]
  public System.Nullable<System.DateTime> OrderDate
  {
   get
   {
    return this._OrderDate;
   }
   set
   {
    if ((this._OrderDate != value))
    {
     this.OnOrderDateChanging(value);
     this.SendPropertyChanging();
     this._OrderDate = value;
     this.SendPropertyChanged("OrderDate");
     this.OnOrderDateChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.AssociationAttribute(Name="Order_Sale", Storage="_Sales", ThisKey="OrderID", OtherKey="OrderID")]
  public EntitySet<Sale> Sales
  {
   get
   {
    return this._Sales;
   }
   set
   {
    this._Sales.Assign(value);
   }
  }

  [global::System.Data.Linq.Mapping.AssociationAttribute(Name="Customer_Order", Storage="_Customer1", ThisKey="CustomerID", OtherKey="CustomerID", IsForeignKey=true)]
  public Customer Customer
  {
   get
   {
    return this._Customer1.Entity;
   }
   set
   {
    Customer previousValue = this._Customer1.Entity;
    if (((previousValue != value)
       || (this._Customer1.HasLoadedOrAssignedValue == false)))
    {
     this.SendPropertyChanging();
     if ((previousValue != null))
     {
      this._Customer1.Entity = null;
      previousValue.Orders.Remove(this);
     }
     this._Customer1.Entity = value;
     if ((value != null))
     {
      value.Orders.Add(this);
      this._Customer = value.CustomerID;
     }
     else
     {
      this._Customer = default(Nullable<int>);
     }
     this.SendPropertyChanged("Customer");
    }
   }
  }

  [global::System.Data.Linq.Mapping.AssociationAttribute(Name="SalesPeople_Order", Storage="_SalesPeople", ThisKey="SalesPersonID", OtherKey="SalesPersonID", IsForeignKey=true)]
  public SalesPerson SalesPerson
  {
   get
   {
    return this._SalesPeople.Entity;
   }
   set
   {
    SalesPerson previousValue = this._SalesPeople.Entity;
    if (((previousValue != value)
       || (this._SalesPeople.HasLoadedOrAssignedValue == false)))
    {
     this.SendPropertyChanging();
     if ((previousValue != null))
     {
      this._SalesPeople.Entity = null;
      previousValue.Orders.Remove(this);
     }
     this._SalesPeople.Entity = value;
     if ((value != null))
     {
      value.Orders.Add(this);
      this._SalesPerson = value.SalesPersonID;
     }
     else
     {
      this._SalesPerson = default(Nullable<int>);
     }
     this.SendPropertyChanged("SalesPerson");
    }
   }
  }

  public event PropertyChangingEventHandler PropertyChanging;

  public event PropertyChangedEventHandler PropertyChanged;

  protected virtual void SendPropertyChanging()
  {
   if ((this.PropertyChanging != null))
   {
    this.PropertyChanging(this, emptyChangingEventArgs);
   }
  }

  protected virtual void SendPropertyChanged(String propertyName)
  {
   if ((this.PropertyChanged != null))
   {
    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
   }
  }

  private void attach_Sales(Sale entity)
  {
   this.SendPropertyChanging();
   entity.Order = this;
  }

  private void detach_Sales(Sale entity)
  {
   this.SendPropertyChanging();
   entity.Order = null;
  }
 }

 [global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Products")]
 public partial class Product : INotifyPropertyChanging, INotifyPropertyChanged
 {

  private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

  private int _OID;

  private string _ProductName;

  private System.Nullable<int> _Category;

  private EntitySet<Sale> _Sales;

  private EntityRef<Category> _Category1;

    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnProductIDChanging(int value);
    partial void OnProductIDChanged();
    partial void OnProductNameChanging(string value);
    partial void OnProductNameChanged();
    partial void OnCategoryIDChanging(System.Nullable<int> value);
    partial void OnCategoryIDChanged();
    #endregion

  public Product()
  {
   this._Sales = new EntitySet<Sale>(new Action<Sale>(this.attach_Sales), new Action<Sale>(this.detach_Sales));
   this._Category1 = default(EntityRef<Category>);
   OnCreated();
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Name="OID", Storage="_OID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
  public int ProductID
  {
   get
   {
    return this._OID;
   }
   set
   {
    if ((this._OID != value))
    {
     this.OnProductIDChanging(value);
     this.SendPropertyChanging();
     this._OID = value;
     this.SendPropertyChanged("ProductID");
     this.OnProductIDChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProductName", DbType="NVarChar(100)")]
  public string ProductName
  {
   get
   {
    return this._ProductName;
   }
   set
   {
    if ((this._ProductName != value))
    {
     this.OnProductNameChanging(value);
     this.SendPropertyChanging();
     this._ProductName = value;
     this.SendPropertyChanged("ProductName");
     this.OnProductNameChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Name="Category", Storage="_Category", DbType="Int")]
  public System.Nullable<int> CategoryID
  {
   get
   {
    return this._Category;
   }
   set
   {
    if ((this._Category != value))
    {
     this.OnCategoryIDChanging(value);
     this.SendPropertyChanging();
     this._Category = value;
     this.SendPropertyChanged("CategoryID");
     this.OnCategoryIDChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.AssociationAttribute(Name="Product_Sale", Storage="_Sales", ThisKey="ProductID", OtherKey="ProductID")]
  public EntitySet<Sale> Sales
  {
   get
   {
    return this._Sales;
   }
   set
   {
    this._Sales.Assign(value);
   }
  }

  [global::System.Data.Linq.Mapping.AssociationAttribute(Name="Category_Product", Storage="_Category1", ThisKey="CategoryID", OtherKey="CategoryID", IsForeignKey=true)]
  public Category Category
  {
   get
   {
    return this._Category1.Entity;
   }
   set
   {
    Category previousValue = this._Category1.Entity;
    if (((previousValue != value)
       || (this._Category1.HasLoadedOrAssignedValue == false)))
    {
     this.SendPropertyChanging();
     if ((previousValue != null))
     {
      this._Category1.Entity = null;
      previousValue.Products.Remove(this);
     }
     this._Category1.Entity = value;
     if ((value != null))
     {
      value.Products.Add(this);
      this._Category = value.CategoryID;
     }
     else
     {
      this._Category = default(Nullable<int>);
     }
     this.SendPropertyChanged("Category");
    }
   }
  }

  public event PropertyChangingEventHandler PropertyChanging;

  public event PropertyChangedEventHandler PropertyChanged;

  protected virtual void SendPropertyChanging()
  {
   if ((this.PropertyChanging != null))
   {
    this.PropertyChanging(this, emptyChangingEventArgs);
   }
  }

  protected virtual void SendPropertyChanged(String propertyName)
  {
   if ((this.PropertyChanged != null))
   {
    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
   }
  }

  private void attach_Sales(Sale entity)
  {
   this.SendPropertyChanging();
   entity.Product = this;
  }

  private void detach_Sales(Sale entity)
  {
   this.SendPropertyChanging();
   entity.Product = null;
  }
 }

 [global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Sales")]
 public partial class Sale : INotifyPropertyChanging, INotifyPropertyChanged
 {

  private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

  private int _OID;

  private System.Nullable<int> _Order;

  private System.Nullable<int> _Product;

  private System.Nullable<int> _Quantity;

  private System.Nullable<decimal> _UnitPrice;

  private EntityRef<Order> _Order1;

  private EntityRef<Product> _Product1;

    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSaleIDChanging(int value);
    partial void OnSaleIDChanged();
    partial void OnOrderIDChanging(System.Nullable<int> value);
    partial void OnOrderIDChanged();
    partial void OnProductIDChanging(System.Nullable<int> value);
    partial void OnProductIDChanged();
    partial void OnQuantityChanging(System.Nullable<int> value);
    partial void OnQuantityChanged();
    partial void OnUnitPriceChanging(System.Nullable<decimal> value);
    partial void OnUnitPriceChanged();
    #endregion

  public Sale()
  {
   this._Order1 = default(EntityRef<Order>);
   this._Product1 = default(EntityRef<Product>);
   OnCreated();
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Name="OID", Storage="_OID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
  public int SaleID
  {
   get
   {
    return this._OID;
   }
   set
   {
    if ((this._OID != value))
    {
     this.OnSaleIDChanging(value);
     this.SendPropertyChanging();
     this._OID = value;
     this.SendPropertyChanged("SaleID");
     this.OnSaleIDChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Order]", Storage="_Order", DbType="Int")]
  public System.Nullable<int> OrderID
  {
   get
   {
    return this._Order;
   }
   set
   {
    if ((this._Order != value))
    {
     this.OnOrderIDChanging(value);
     this.SendPropertyChanging();
     this._Order = value;
     this.SendPropertyChanged("OrderID");
     this.OnOrderIDChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Name="Product", Storage="_Product", DbType="Int")]
  public System.Nullable<int> ProductID
  {
   get
   {
    return this._Product;
   }
   set
   {
    if ((this._Product != value))
    {
     this.OnProductIDChanging(value);
     this.SendPropertyChanging();
     this._Product = value;
     this.SendPropertyChanged("ProductID");
     this.OnProductIDChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Quantity", DbType="Int")]
  public System.Nullable<int> Quantity
  {
   get
   {
    return this._Quantity;
   }
   set
   {
    if ((this._Quantity != value))
    {
     this.OnQuantityChanging(value);
     this.SendPropertyChanging();
     this._Quantity = value;
     this.SendPropertyChanged("Quantity");
     this.OnQuantityChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UnitPrice", DbType="Money")]
  public System.Nullable<decimal> UnitPrice
  {
   get
   {
    return this._UnitPrice;
   }
   set
   {
    if ((this._UnitPrice != value))
    {
     this.OnUnitPriceChanging(value);
     this.SendPropertyChanging();
     this._UnitPrice = value;
     this.SendPropertyChanged("UnitPrice");
     this.OnUnitPriceChanged();
    }
   }
  }

  [global::System.Data.Linq.Mapping.AssociationAttribute(Name="Order_Sale", Storage="_Order1", ThisKey="OrderID", OtherKey="OrderID", IsForeignKey=true)]
  public Order Order
  {
   get
   {
    return this._Order1.Entity;
   }
   set
   {
    Order previousValue = this._Order1.Entity;
    if (((previousValue != value)
       || (this._Order1.HasLoadedOrAssignedValue == false)))
    {
     this.SendPropertyChanging();
     if ((previousValue != null))
     {
      this._Order1.Entity = null;
      previousValue.Sales.Remove(this);
     }
     this._Order1.Entity = value;
     if ((value != null))
     {
      value.Sales.Add(this);
      this._Order = value.OrderID;
     }
     else
     {
      this._Order = default(Nullable<int>);
     }
     this.SendPropertyChanged("Order");
    }
   }
  }

  [global::System.Data.Linq.Mapping.AssociationAttribute(Name="Product_Sale", Storage="_Product1", ThisKey="ProductID", OtherKey="ProductID", IsForeignKey=true)]
  public Product Product
  {
   get
   {
    return this._Product1.Entity;
   }
   set
   {
    Product previousValue = this._Product1.Entity;
    if (((previousValue != value)
       || (this._Product1.HasLoadedOrAssignedValue == false)))
    {
     this.SendPropertyChanging();
     if ((previousValue != null))
     {
      this._Product1.Entity = null;
      previousValue.Sales.Remove(this);
     }
     this._Product1.Entity = value;
     if ((value != null))
     {
      value.Sales.Add(this);
      this._Product = value.ProductID;
     }
     else
     {
      this._Product = default(Nullable<int>);
     }
     this.SendPropertyChanged("Product");
    }
   }
  }

  public event PropertyChangingEventHandler PropertyChanging;

  public event PropertyChangedEventHandler PropertyChanged;

  protected virtual void SendPropertyChanging()
  {
   if ((this.PropertyChanging != null))
   {
    this.PropertyChanging(this, emptyChangingEventArgs);
   }
  }

  protected virtual void SendPropertyChanged(String propertyName)
  {
   if ((this.PropertyChanged != null))
   {
    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
   }
  }
 }
}
#pragma warning restore 1591
