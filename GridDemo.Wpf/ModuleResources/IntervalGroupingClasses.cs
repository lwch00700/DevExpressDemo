using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.DemoBase;

namespace GridDemo {
    #region GroupIntervalData

    public class GroupIntervalData {
        public static Random rnd = new Random();
        public static DataView Invoices { get { return CreateInvoicesDataTable().DefaultView; } }
        public static DataView Products { get { return CreateProductsDataTable().DefaultView; } }
        static DateTime GetDate(bool range) {
            DateTime ret = DateTime.Now;
            int r = rnd.Next(20);
            if(range) {
                if(r > 1) ret = ret.AddDays(rnd.Next(80) - 40);
                if(r > 18) ret = ret.AddMonths(rnd.Next(18));
            } else {
                ret = ret.AddDays(rnd.Next(r * 30) - r * 15);
            }
            return ret;
        }
        static decimal GetCount() {
            return rnd.Next(50) + 10;
        }
        static DataTable CreateInvoicesDataTable() {
            var invoices = NWindContext.Create().Invoices.ToList();
            DataTable tbl = new DataTable("GroupInterval");
            tbl.Columns.Add("Country", typeof(string));
            tbl.Columns.Add("City", typeof(string));
            tbl.Columns.Add("OrderDate", typeof(DateTime));
            tbl.Columns.Add("UnitPrice", typeof(decimal));
            tbl.Columns.Add("Region", typeof(string));
            foreach(var invoice in invoices) {
                tbl.Rows.Add(new object[] { invoice.Country, invoice.City, GetDate(true), invoice.UnitPrice, invoice.Region });
            }
            return tbl;
        }
        static DataTable CreateProductsDataTable() {
            const int rowCount = 1000;
            var products = NWindContext.Create().Products.ToList();
            DataTable tbl = new DataTable("GroupInterval");
            tbl.Columns.Add("ProductName", typeof(string));
            tbl.Columns.Add("UnitPrice", typeof(decimal));
            tbl.Columns.Add("Count", typeof(int));
            tbl.Columns.Add("OrderDate", typeof(DateTime));
            tbl.Columns.Add("OrderSum", typeof(decimal), "[UnitPrice] * [Count]");
            for(int i = 0; i < rowCount; i++) {
                Product product = products[rnd.Next(products.Count - 1)];
                tbl.Rows.Add(new object[] { product.ProductName, product.UnitPrice, GetCount(), GetDate(false) });
            }
            return tbl;
        }
    }


    #endregion
}
