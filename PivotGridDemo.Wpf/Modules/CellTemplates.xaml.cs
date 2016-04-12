using System;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Xpf.DemoBase;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.DemoData.Models;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.IO;

namespace PivotGridDemo.PivotGrid {
    public partial class CellTemplates : PivotGridDemoModule {
        #region static stuff
        public static readonly DependencyProperty IsCellTemplateVisibleProperty;
        public static readonly DependencyProperty IsCellValueVisibleProperty;
        public static readonly DependencyProperty IsCellShareVisibleProperty;

        static CellTemplates() {
            Type ownerType = typeof(CellTemplates);
            IsCellTemplateVisibleProperty = DependencyProperty.Register("IsCellTemplateVisible", typeof(bool),
                ownerType, new PropertyMetadata(true, OnCellTemplateVisiblePropertyChanged));
            IsCellValueVisibleProperty = DependencyProperty.Register("IsCellValueVisible", typeof(bool),
                ownerType, new PropertyMetadata(true));
            IsCellShareVisibleProperty = DependencyProperty.Register("IsCellShareVisible", typeof(bool),
                ownerType, new PropertyMetadata(true));
        }
        static void OnCellTemplateVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((CellTemplates)d).OnCellTemplateVisibleChanged();
        }
        #endregion

        public CellTemplates() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().ProductReports.ToList();
        }

        ProductReport toPr(DataRow row) {
            return new ProductReport {
                CategoryName = (string)row["CategoryName"],
                ProductName = (string)row["ProductName"],
                ProductSales = (decimal)row["ProductSales"],
                ShippedDate = (DateTime)row["ShippedDate"]
            };
        }

        string print(ProductReport rep) {
            return string.Format("{0}|{1}|{2}|{3}",
                rep.CategoryName, rep.ProductName, rep.ProductSales, rep.ShippedDate);
        }

        public bool IsCellTemplateVisible {
            get { return (bool)GetValue(IsCellTemplateVisibleProperty); }
            set { SetValue(IsCellTemplateVisibleProperty, value); }
        }

        public bool IsCellValueVisible {
            get { return (bool)GetValue(IsCellValueVisibleProperty); }
            set { SetValue(IsCellValueVisibleProperty, value); }
        }

        public bool IsCellShareVisible {
            get { return (bool)GetValue(IsCellShareVisibleProperty); }
            set { SetValue(IsCellShareVisibleProperty, value); }
        }

        void OnCellTemplateVisibleChanged() {
            if(IsCellTemplateVisible) {
                fieldSales.CellTemplate = (DataTemplate)Resources["CellTemplate"];
                fieldYear.Width = 200;
            } else {
                fieldSales.CellTemplate = null;
                fieldYear.Width = 100;
            }
        }

        void TemplatesList_SelectionChanged(object sender, RoutedEventArgs e) {
            switch(templatesList.SelectedIndex) {
                case 0:
                    IsCellTemplateVisible = false;
                    break;
                case 1:
                    IsCellTemplateVisible = true;
                    IsCellValueVisible = false;
                    IsCellShareVisible = true;
                    break;
                case 2:
                    IsCellTemplateVisible = true;
                    IsCellValueVisible = true;
                    IsCellShareVisible = true;
                    break;
            }
        }
    }

    public class RoundConverter : MarkupExtension, IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return System.Convert.ToInt32(value);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
