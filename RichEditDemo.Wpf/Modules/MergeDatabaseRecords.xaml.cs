using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using System.Linq;
using System.IO;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Services;
using DevExpress.Xpf.Grid;
using DevExpress.Office.Services;

using System.Data;
using DevExpress.DemoData.Models;

namespace RichEditDemo {
    public partial class MergeDatabaseRecords : RichEditDemoModule {
        object ds;
        IEnumerable<object> CreateDataSource() {
            return (from e in NWindContext.Create().Employees
                from c in e.Customers
                select new {
                    EmployeeID = e.EmployeeID,
                    LastName = e.LastName,
                    FirstName = e.FirstName,
                    Title = e.Title,
                    TitleOfCourtesy = e.TitleOfCourtesy,
                    BirthDate = e.BirthDate,
                    HireDate = e.HireDate,
                    Employees = new {
                        Address = e.Address,
                        City = e.City,
                        Region = e.Region,
                        PostalCode = e.PostalCode,
                        Country = e.Country,
                    },
                    HomePhone = e.HomePhone,
                    Extension = e.Extension,
                    Photo = e.Photo,
                    Notes = e.Notes,
                    ReportsTo = e.ReportsTo,
                    Email = e.Email,
                    CustomerID = c.CustomerID,
                    CompanyName = c.CompanyName,
                    ContactName = c.ContactName,
                    ContactTitle = c.ContactTitle,
                    Customers = new {
                        Address = c.Address,
                        City = c.City,
                        Region = c.Region,
                        PostalCode = c.PostalCode,
                        Country = c.Country,
                    },
                    Phone = c.Phone,
                    Fax = c.Fax
                }).ToList();
        }
        public MergeDatabaseRecords() {
            InitializeComponent();
            RtfLoadHelper.Load("MailMerge.rtf", richEdit);
            this.ds = CreateDataSource();

            IUriStreamService uriService = (IUriStreamService)richEdit.GetService(typeof(IUriStreamService));
            uriService.RegisterProvider(new DBUriStreamProviderBase<DevExpress.DemoData.Models.Employee>(NWindContext.Create().Employees.ToList(),
                (es, id) => es.First(e => e.EmployeeID == id).Photo));
            richEdit.Loaded += richEdit_Loaded;
            gridControl1.Loaded += gridControl1_Loaded;
            gridControl1.View.FocusedRowHandleChanged += View_FocusedRowChanged;
        }

        void gridControl1_Loaded(object sender, RoutedEventArgs e) {
            InitializeGrid(ds);
        }

        void richEdit_Loaded(object sender, RoutedEventArgs e) {
            InitializeRichEdit(ds);
        }

        void InitializeRichEdit(object ds) {
            RichEditMailMergeOptions options = richEdit.Options.MailMerge;
            options.DataSource = null;
            options.DataSource = ds;
            options.ViewMergedData = true;
        }
        void InitializeGrid(object ds) {
            this.gridControl1.ItemsSource = ds;
            this.gridControl1.View.AllowEditing = false;
            this.gridControl1.View.ShowColumnHeaders = false;
            ((GridViewBase)this.gridControl1.View).ShowGroupPanel = false;
            this.gridControl1.SortInfo.AddRange(new DevExpress.Xpf.Grid.GridSortInfo("LastName", System.ComponentModel.ListSortDirection.Ascending));
        }
        void View_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowHandleChangedEventArgs e) {
            richEdit.Options.MailMerge.ActiveRecord = gridControl1.GetListIndexByRowHandle(e.RowData.RowHandle.Value);
        }
    }
    public class DBUriStreamProviderBase<T> : IUriStreamProvider {
        static readonly string prefix = "dbimg://";

        IEnumerable<T> ds;
        Func<IEnumerable<T>, long, byte[]> selector;

        public DBUriStreamProviderBase(IEnumerable<T> ds, Func<IEnumerable<T>, long, byte[]> selector) {
            this.ds = ds;
            this.selector = selector;
        }

        public object Ds { get { return ds; } }

        #region IUriStreamProvider Members
        Stream IUriStreamProvider.GetStream(string uri) {
            uri = uri.Trim();
            if (!uri.StartsWith(prefix))
                return null;
            string strId = uri.Substring(prefix.Length).Trim();
            int id;
            if (!int.TryParse(strId, out id))
                return null;
            byte[] bytes = selector(ds, id);
            if (bytes == null)
                return null;
            return new MemoryStream(bytes);
        }
        #endregion
    }
}
