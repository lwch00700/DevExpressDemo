using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Collections.Generic;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.DemoBase;
using System.Linq;
using System.Data;

namespace GridDemo {
    public struct Range {
        public string Text { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public override string ToString() {
            return Text;
        }
    }
    public class ProductIdToProductNameConverter : IValueConverter {
        static Dictionary<long, Product> products;

        static Dictionary<long, Product> Products {
            get {
                if(products == null)
                    products = NWindContext.Create().Products.ToDictionary(p => p.ProductID);
                return products;
            }
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            long id = (long)value;
            if(Products.ContainsKey(id)) {
                Product product = Products[id];
                return product != null ? product.ProductName : string.Empty;
            }
            return string.Empty;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
