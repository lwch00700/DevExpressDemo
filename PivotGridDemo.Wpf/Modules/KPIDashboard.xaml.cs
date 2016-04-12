using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Data.PivotGrid;
using DevExpress.Xpf.Core;
using System.Windows.Controls;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class KPIDashboard : PivotGridDemoModule {
        public KPIDashboard() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().ProductReports.ToList();
            SalesTarget = GetSalesTarget();
            UpdateKPIs();
        }

        protected double SalesTarget { get; private set; }

        void pivotGrid_GridLayout(object sender, RoutedEventArgs e) {
            UpdateKPIs();
        }

        void UpdateKPIs() {
            if(avgTransGauge == null) return;

            double avgTrans = GetAverageTransaction();
            avgTransGauge.Min = avgTrans < 400 ? avgTrans - 100 : 400;
            avgTransGauge.Max = (avgTrans > 800 ? (Math.Ceiling(avgTrans / 100) + 1) * 100 : 800);
            avgTransGauge.Value = avgTrans;

            double salesToLastYear = GetSalesToLastYear();
            if(salesToLastYear < 0) {
                salesToLastYearGauge.Visibility = Visibility.Collapsed;
                salesToLastYearNA.Visibility = Visibility.Visible;
            } else {
                salesToLastYearGauge.Visibility = Visibility.Visible;
                salesToLastYearNA.Visibility = Visibility.Collapsed;
                salesToLastYearGauge.Min = salesToLastYear < 1 ? 0 : 1;
                salesToLastYearGauge.Max = salesToLastYear < 1 ? 1 : (salesToLastYear > 2 ? Math.Ceiling(salesToLastYear) + 1 : 2);
                salesToLastYearGauge.Value = salesToLastYear;
            }

            double salesToTarget = GetSalesToTarget();
            salesToTargetGauge.Min = salesToTarget < 1 ? 0 : 1;
            salesToTargetGauge.Max = salesToTarget < 1 ? 1 : (salesToTarget > 2 ? Math.Ceiling(salesToTarget) + 1 : 2);
            salesToTargetGauge.Value = salesToTarget;
        }

        double GetAverageTransaction() {
            PivotDrillDownDataSource ds = pivotGrid.CreateDrillDownDataSource();
            if(ds.RowCount == 0) return 0;
            double transactionSum = 0;
            for(int i = 0; i < ds.RowCount; i++) {
                transactionSum += Convert.ToDouble(ds[i]["ProductSales"]);
            }
            return transactionSum / ds.RowCount;
        }

        double GetSalesToLastYear() {
            if(fieldYear.FilterValues.ValuesIncluded.Count() != 2
                    || pivotGrid.GetFieldCountByArea(FieldArea.DataArea) != 1) return -1;
            object[] years = fieldYear.FilterValues.ValuesIncluded;
            Array.Sort(years);
            double thisYear = Convert.ToDouble(pivotGrid.GetCellValue(new object[] { years[1] }, null)),
                lastYear = Convert.ToDouble(pivotGrid.GetCellValue(new object[] { years[0] }, null));
            return lastYear != 0 ? thisYear / lastYear : 1;
        }

        double GetSalesTarget() {
            if(!fieldYear.FilterValues.IsEmpty)
                throw new ArgumentException("!fieldYear.FilterValues.IsEmpty");
            object[] years = fieldYear.GetUniqueValues();
            Array.Sort(years);
            double year0Sales = Convert.ToDouble(pivotGrid.GetCellValue(new object[] { years[0] }, null));
            if(year0Sales == 0)
                throw new ArgumentException("no sales");
            return year0Sales * Convert.ToDouble(Math.Pow(1.3, years.Length));
        }

        double GetSalesToTarget() {
            if(pivotGrid.GetFieldCountByArea(FieldArea.DataArea) != 1) return 1;
            double sales = Convert.ToDouble(pivotGrid.GetCellValue(new object[0], new object[0]));
            return SalesTarget != 0 ? sales / SalesTarget : 1;
        }

        void ResetField(PivotGridField field, FieldArea area, int areaIndex) {
            field.Area = area;
            field.AreaIndex = areaIndex;
            field.FilterValues.Clear();
            field.FilterValues.FilterType = FieldFilterType.Excluded;
        }

        void Reset() {
            if(pivotGrid == null) return;
            pivotGrid.BeginUpdate();
            ResetField(fieldCategory, FieldArea.RowArea, 0);
            ResetField(fieldProduct, FieldArea.RowArea, 1);
            ResetField(fieldSales, FieldArea.DataArea, 0);
            ResetField(fieldYear, FieldArea.ColumnArea, 0);
            ResetField(fieldQuarter, FieldArea.ColumnArea, 1);
            pivotGrid.EndUpdate();
        }

        private void ListBoxEdit_SelectionChanged(object sender, RoutedEventArgs e) {
            switch(dashboardList.SelectedIndex) {
                case 0:
                    Reset();
                    pivotGrid.ExpandAll();
                    break;
                case 1:
                    pivotGrid.BeginUpdate();
                    Reset();
                    fieldYear.FilterValues.ValuesIncluded = new object[] { fieldYear.GetUniqueValues()[0], fieldYear.GetUniqueValues()[1] };
                    pivotGrid.EndUpdate();
                    pivotGrid.ExpandAllRows();
                    pivotGrid.CollapseAllColumns();
                    break;
                case 2:
                    pivotGrid.BeginUpdate();
                    Reset();
                    fieldCategory.FilterValues.ValuesIncluded = new object[] { "Beverages", "Condiments" };
                    pivotGrid.EndUpdate();
                    pivotGrid.ExpandAllColumns();
                    pivotGrid.CollapseAllRows();
                    break;
            }
        }
    }
    public class SimpleGaugeControl : Control {
        public static DependencyProperty MinProperty;
        public static DependencyProperty MaxProperty;
        public static DependencyProperty ValueProperty;
        public static DependencyProperty ValueStringFormatProperty;
        static SimpleGaugeControl() {
            Type ownerType = typeof(SimpleGaugeControl);
            MinProperty = DependencyProperty.Register("Min", typeof(double), ownerType, new PropertyMetadata(0d));
            MaxProperty = DependencyProperty.Register("Max", typeof(double), ownerType, new PropertyMetadata(10d));
            ValueProperty = DependencyProperty.Register("Value", typeof(double), ownerType, new PropertyMetadata(3d, UpdateDisplayText));
            ValueStringFormatProperty = DependencyProperty.Register("ValueStringFormat", typeof(string), ownerType, new PropertyMetadata(string.Empty, UpdateDisplayText));
        }
        static void UpdateDisplayText(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((SimpleGaugeControl)d).UpdateDisplayText();
        }
        ContentControl textControl;
        public SimpleGaugeControl() {
            this.SetDefaultStyleKey(typeof(SimpleGaugeControl));
        }
        public double Value {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public double Min {
            get { return (double)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }
        public double Max {
            get { return (double)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }
        public string ValueStringFormat {
            get { return (string)GetValue(ValueStringFormatProperty); }
            set { SetValue(ValueStringFormatProperty, value); }
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            textControl = GetTemplateChild("PART_Text") as ContentControl;
            UpdateDisplayText();
        }
        void UpdateDisplayText() {
            if(textControl == null)
                return;
            string format = !string.IsNullOrEmpty(ValueStringFormat) ? "{0:" + ValueStringFormat + "}" : "{0}";
            textControl.Content = string.Format(format, Value);
        }
    }
}
