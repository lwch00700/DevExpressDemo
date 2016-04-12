using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class CustomChartData : PivotGridDemoModule {
        enum RowFieldValueExportRule { ProductName = 0, CategoryAndProduct = 1, CategoryEncoded = 2 };
        static string[] Categories = new string[] { "Beverages", "Condiments", "Confections", "Dairy Products",
            "Grains/Cereals", "Meat/Poultry", "Produce", "Seafood" };
        RowFieldValueExportRule rowFieldValueExportRule;

        RowFieldValueExportRule RowExportRule {
            get { return rowFieldValueExportRule; }
            set { rowFieldValueExportRule = value; }
        }
        public CustomChartData() {
            InitializeComponent();
            ChartFactory.InitComboBox(cbChartType, new Type[] { InitDiagram() });
            RowExportRule = RowFieldValueExportRule.ProductName;
            pivotGrid.ChartProvideRowFieldValuesAsType = typeof(string);
            cbRowFieldValuesExportRule.Items.AddRange(new string[] { "ProductName", "Category/ProductName",
                "Encoded Product Category" });
            cbRowFieldValuesExportRule.SelectedIndex = 0;
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
            pivotGrid.MultiSelection.SetSelection(CreateSelectedPoints());
            SetMeasureUnits(DateTimeMeasurementUnit.Year, DateTimeMeasurementUnit.Quarter, DateTimeMeasurementUnit.Month);
            CollapseValues();
        }
        Type InitDiagram() {
            XYDiagram2D xyDiagram = new XYDiagram2D();
            xyDiagram.SeriesDataMember = "Series";
            xyDiagram.AxisX = new AxisX2D();
            xyDiagram.AxisX.DateTimeScaleOptions = new ManualDateTimeScaleOptions() { MeasureUnit = DateTimeMeasureUnit.Year, GridAlignment = DateTimeGridAlignment.Year, AutoGrid = false };
            xyDiagram.AxisX.WholeRange = new DevExpress.Xpf.Charts.Range();
            xyDiagram.AxisY = new AxisY2D();
            xyDiagram.AxisY.WholeRange = new DevExpress.Xpf.Charts.Range();
            xyDiagram.AxisX.Label = new AxisLabel();
            chartControl.Diagram = xyDiagram;
            return chartControl.Diagram.GetType();
        }
        System.Drawing.Point[] CreateSelectedPoints() {
            System.Drawing.Point[] points = new System.Drawing.Point[pivotGrid.ColumnCount * 12];
            for(int i = 0; i < pivotGrid.ColumnCount; i++) {
                for(int j = 0; j < 12; j++)
                    points[i + j * pivotGrid.ColumnCount] = new System.Drawing.Point(i, j + 1);
            }
            return points;
        }
        void SetMeasureUnits(params DateTimeMeasurementUnit[] units) {
            object prevUnit = (cbChartDateMeasureUnit.SelectedItem == null || string.IsNullOrEmpty(cbChartDateMeasureUnit.SelectedItem.ToString())) ?
                null : Enum.Parse(typeof(DateTimeMeasurementUnit), cbChartDateMeasureUnit.SelectedItem.ToString());
            string prevItem = "";
            cbChartDateMeasureUnit.Items.Clear();
            foreach(DateTimeMeasurementUnit unit in units) {
                string unitName = Enum.GetName(typeof(DateTimeMeasurementUnit), unit);
                cbChartDateMeasureUnit.Items.Add(unitName);
                if(prevUnit != null && object.Equals(unit, (DateTimeMeasurementUnit)prevUnit))
                    prevItem = unitName;
            }
            if(!string.IsNullOrEmpty(prevItem))
                cbChartDateMeasureUnit.SelectedItem = prevItem;
            else
                cbChartDateMeasureUnit.SelectedIndex = 0;
        }
        void CollapseValues() {
            fieldCategoryName.CollapseAll();
            fieldCategoryName.ExpandValue("Condiments");
        }
        Char EncodeCategoryName(string categoryName) {
            for(int i = 0; i < Categories.Length; i++) {
                if(Categories[i] == categoryName)
                    return Convert.ToChar(Convert.ToInt32('A') + i);
            }
            return 'Z';
        }
        void cbChartType_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            Series seriesTemplate = ChartFactory.CreateSeriesInstance((Type)((ComboBoxEditItem)cbChartType.SelectedItem).Tag);
            seriesTemplate.ArgumentDataMember = "Arguments";
            seriesTemplate.ArgumentScaleType = ScaleType.DateTime;
            seriesTemplate.ValueDataMember = "Values";
            if(seriesTemplate.Label == null)
                seriesTemplate.Label = new SeriesLabel();
            seriesTemplate.LabelsVisibility = ceShowPointsLabels.IsChecked.HasValue && ceShowPointsLabels.IsChecked.Value;
            seriesTemplate.LegendTextPattern = "{A}: {V:G}";
            chartControl.Diagram.SeriesTemplate = seriesTemplate;
        }
        void ceShowPointsLabels_Checked(object sender, RoutedEventArgs e) {
            chartControl.Diagram.SeriesTemplate.LabelsVisibility = ceShowPointsLabels.IsChecked.HasValue && ceShowPointsLabels.IsChecked.Value;
        }
        void cbChartDateMeasureUnit_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            XYDiagram2D diagram = (XYDiagram2D)chartControl.Diagram;
            DateTimeMeasureUnit unit = (DateTimeMeasureUnit)Enum.Parse(typeof(DateTimeMeasureUnit), cbChartDateMeasureUnit.SelectedItem.ToString());
            diagram.AxisX.DateTimeScaleOptions = new ManualDateTimeScaleOptions() { MeasureUnit = unit, GridAlignment = (DateTimeGridAlignment)unit, AutoGrid = false };
            switch(unit) {
                case DateTimeMeasureUnit.Year:
                    diagram.AxisX.Label.TextPattern = "{A:yyyy}";
                    break;
                case DateTimeMeasureUnit.Quarter:
                    diagram.AxisX.Label.TextPattern = "{A:q}";
                    break;
                case DateTimeMeasureUnit.Month:
                    diagram.AxisX.Label.TextPattern = "{A:y}";
                    break;
                default:
                    break;
            }
        }
        void pivotGrid_FieldExpandedInGroupChanged(object sender, PivotFieldEventArgs e) {
            if(!fieldYear.ExpandedInFieldsGroup) {
                SetMeasureUnits(DateTimeMeasurementUnit.Year);
                return;
            }
            if(!fieldQuarter.ExpandedInFieldsGroup) {
                SetMeasureUnits(DateTimeMeasurementUnit.Year, DateTimeMeasurementUnit.Quarter);
                return;
            }
            SetMeasureUnits(DateTimeMeasurementUnit.Year, DateTimeMeasurementUnit.Quarter, DateTimeMeasurementUnit.Month);
        }
        void cbRowFieldValuesExportRule_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            RowExportRule = (RowFieldValueExportRule)cbRowFieldValuesExportRule.SelectedIndex;
            pivotGrid.ChartProvideRowFieldValuesAsType = RowExportRule == RowFieldValueExportRule.CategoryEncoded ? typeof(Char) : typeof(string);
            pivotGrid.RefreshData();
        }
        void pivotGrid_CustomChartDataSourceData(object sender, PivotCustomChartDataSourceDataEventArgs e) {
            if(e.ItemType == PivotChartItemType.CellItem) {
                if(e.Value == DBNull.Value || (decimal)e.Value < Convert.ToDecimal(seCellZeroValueThreshold.Value))
                    e.Value = 0;
            }
            if(e.ItemType == PivotChartItemType.RowItem) {
                bool isCategoryNameField = object.Equals(e.FieldValueInfo.Field, fieldCategoryName);
                switch(RowExportRule) {
                    case RowFieldValueExportRule.ProductName:
                        e.Value = isCategoryNameField ? e.FieldValueInfo.Value.ToString() + " Category" : e.FieldValueInfo.Value;
                        break;
                    case RowFieldValueExportRule.CategoryAndProduct:
                        e.Value = isCategoryNameField ? e.FieldValueInfo.Value.ToString() + '/' + "Total" :
                            e.FieldValueInfo.GetHigherLevelFieldValue(fieldCategoryName).ToString() + '/' + e.FieldValueInfo.Value.ToString();
                        break;
                    case RowFieldValueExportRule.CategoryEncoded:
                        string categoryName = isCategoryNameField ?
                            e.FieldValueInfo.Value.ToString() : e.FieldValueInfo.GetHigherLevelFieldValue(fieldCategoryName).ToString();
                        e.Value = EncodeCategoryName(categoryName);
                        break;
                }
            }
        }
        void seCellZeroValueThreshold_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            pivotGrid.RefreshData();
        }
    }
}
