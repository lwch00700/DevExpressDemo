using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.PivotGrid;
using DevExpress.DemoData.Models;
using System.Linq;

namespace PivotGridDemo.PivotGrid {
    public partial class ChartsIntegration : PivotGridDemoModule {

        static readonly string[] ProductFilterValues = {"Chai", "Chang", "Chocolade", "Filo Mix", "Geitost", "Ikura", "Konbu",
            "Maxilaku", "Pavlova", "Spegesild", "Tofu", "Tourtière"};
        public ChartsIntegration() {
            InitializeComponent();
        }
        void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            ChartFactory.InitComboBox(cbChartType, null);
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
            SetFilter();
            SetSelection();
        }
        void SetFilter() {
            fieldProductName.FilterValues.ValuesIncluded = ProductFilterValues;
            fieldOrderYear.FilterValues.SetValues(new object[] { fieldOrderYear.GetUniqueValues()[0] }, FieldFilterType.Included, true);
        }
        void SetSelection() {
            pivotGrid.SetSelectionByFieldValues(false, new object[] { "Chai" });
            pivotGrid.SetSelectionByFieldValues(false, new object[] { "Chocolade" });
        }
        void cbChartType_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if(cbChartType.SelectedIndex < 0)
                return;
            chartControl.Diagram = ChartFactory.GenerateDiagram((Type)((ComboBoxEditItem)cbChartType.SelectedItem).Tag, ceShowPointsLabels.IsChecked);
            pivotGrid.ChartProvideEmptyCells = IsProvideEmptyCells();
        }
        void ceShowPointsLabels_Checked(object sender, RoutedEventArgs e) {
            chartControl.Diagram.SeriesTemplate.LabelsVisibility = object.Equals(ceShowPointsLabels.IsChecked, true);
            chartControl.CrosshairEnabled = object.Equals(ceShowPointsLabels.IsChecked, false);
        }

        void oncrChartDataVerticalSelectedIndexChanged(object sender, RoutedEventArgs e) {
            pivotGrid.ChartProvideDataByColumns = crChartDataVertical.SelectedIndex == 1;
        }

        void chartControl_BoundDataChanged(object sender, RoutedEventArgs e) {
            if(chartControl.Diagram is SimpleDiagram2D)
                ConfigurePie();
            if(chartControl.Diagram is SimpleDiagram3D)
                ConfigurePie();
            CheckWarningVisivility();
        }

        void ConfigurePie() {
            Dictionary<PieSeries, int> counts = new Dictionary<PieSeries, int>();
            foreach(PieSeries series in chartControl.Diagram.Series) {
                counts.Add(series, GetPointsCount(series));
                series.Titles.Add(new Title() { Content = series.DisplayName, Dock = Dock.Bottom, HorizontalAlignment = System.Windows.HorizontalAlignment.Center, FontSize = 12, VerticalAlignment = System.Windows.VerticalAlignment.Center });
                series.ShowInLegend = false;
            }

            int max = 0;
            PieSeries maxSeries = null;
            foreach(KeyValuePair<PieSeries, int> pair in counts)
                if(max < pair.Value) {
                    max = pair.Value;
                    maxSeries = pair.Key;
                }

            if(maxSeries == null)
                return;
            List<string> values = new List<string>();
            foreach(SeriesPoint point in maxSeries.Points)
                values.Add(point.Argument);

            maxSeries.ShowInLegend = true;

            if(chartControl.Diagram is SimpleDiagram2D)
                foreach(PieSeries series in chartControl.Diagram.Series) {
                    foreach(SeriesPoint point in maxSeries.Points)
                        if(!values.Contains(point.Argument)) {
                            series.ShowInLegend = true;
                            values.Add(point.Argument);
                        }
                }
        }

        int GetPointsCount(PieSeries series) {
            int count = 0;
            for(int i = 0; i < series.Points.Count; i++)
                if(!double.IsNaN(series.Points[i].Value))
                    count++;
            return count;
        }

        void CheckWarningVisivility() {
            PivotCellBaseEventArgs cellInfo;
            bool showWarning = false;
            if(pivotGrid.MultiSelection.SelectedCells.Count == 0) {
                cellInfo = pivotGrid.GetCellInfo(pivotGrid.FocusedCell.X, pivotGrid.FocusedCell.Y);
                showWarning = (cellInfo.ColumnValueType == FieldValueType.GrandTotal && !pivotGrid.ChartProvideColumnGrandTotals)
                                || (cellInfo.RowValueType == FieldValueType.GrandTotal && !pivotGrid.ChartProvideRowGrandTotals);
            }
            else {
                foreach(System.Drawing.Point cell in pivotGrid.MultiSelection.SelectedCells) {
                    cellInfo = pivotGrid.GetCellInfo(cell.X, cell.Y);
                    if((cellInfo.ColumnValueType == FieldValueType.GrandTotal && !pivotGrid.ChartProvideColumnGrandTotals)
                        || (cellInfo.RowValueType == FieldValueType.GrandTotal && !pivotGrid.ChartProvideRowGrandTotals)) {
                        showWarning = true;
                    }
                    else {
                        showWarning = false;
                        break;
                    }
                }
            }
            warningChart.Visibility = showWarning ? Visibility.Visible : Visibility.Collapsed;
        }
        bool IsProvideEmptyCells() {
            if ((chartControl.Diagram is SimpleDiagram2D)
                || (chartControl.Diagram is SimpleDiagram3D)
                )
                return true;
            return false;
        }
        void pivotGrid_CustomChartDataSourceData(object sender, PivotCustomChartDataSourceDataEventArgs e) {
            if (IsProvideEmptyCells()) {
                if (e.ItemDataMember == PivotChartItemDataMember.Value && e.Value == DBNull.Value)
                    e.Value = 0;
            }
        }
    }
    internal static class ChartFactory {
        static readonly Type XYDiagram2DType = typeof(XYDiagram2D);
        static readonly Type XYDiagram3DType = typeof(XYDiagram3D);
        static readonly Type SimpleDiagram3DType = typeof(SimpleDiagram3D);
        static readonly Type SimpleDiagram2DType = typeof(SimpleDiagram2D);
        static readonly Type DefaultSeriesType = typeof(BarStackedSeries2D);

        static Dictionary<Type, SeriesTypeDescriptor> seriesTypes;
        public static Dictionary<Type, SeriesTypeDescriptor> SeriesTypes {
            get {
                if(seriesTypes == null)
                    seriesTypes = CreateSeriesTypes();
                return seriesTypes;
            }
        }
        static Dictionary<Type, SeriesTypeDescriptor> CreateSeriesTypes() {
            Dictionary<Type, SeriesTypeDescriptor> seriesTypes = new Dictionary<Type, SeriesTypeDescriptor>();
            seriesTypes.Add(typeof(AreaFullStackedSeries2D), new SeriesTypeDescriptor { DiagramType = XYDiagram2DType, DisplayText = "Area Full-Stacked Series 2D" });
            seriesTypes.Add(typeof(AreaSeries2D), new SeriesTypeDescriptor { DiagramType = XYDiagram2DType, DisplayText="Area Series 2D"});
            seriesTypes.Add(typeof(AreaStackedSeries2D),  new SeriesTypeDescriptor { DiagramType = XYDiagram2DType, DisplayText="Area Stacked Series 2D"});
            seriesTypes.Add(typeof(BarFullStackedSeries2D), new SeriesTypeDescriptor { DiagramType =  XYDiagram2DType, DisplayText="Bar Full-Stacked Series 2D"});
            seriesTypes.Add(typeof(BarStackedSeries2D), new SeriesTypeDescriptor { DiagramType =  XYDiagram2DType, DisplayText="Bar Stacked Series 2D"});
            seriesTypes.Add(typeof(LineSeries2D), new SeriesTypeDescriptor { DiagramType = XYDiagram2DType, DisplayText="Line Series 2D" });
            seriesTypes.Add(typeof(PointSeries2D), new SeriesTypeDescriptor { DiagramType = XYDiagram2DType, DisplayText = "Point Series 2D" });
            seriesTypes.Add(typeof(AreaSeries3D), new SeriesTypeDescriptor { DiagramType = XYDiagram3DType, DisplayText="Area Series 3D" });
            seriesTypes.Add(typeof(AreaStackedSeries3D), new SeriesTypeDescriptor { DiagramType = XYDiagram3DType, DisplayText="Area Stacked Series 3D" });
            seriesTypes.Add(typeof(AreaFullStackedSeries3D), new SeriesTypeDescriptor { DiagramType = XYDiagram3DType, DisplayText="Area Full-Stacked Series 3D" });
            seriesTypes.Add(typeof(BarSeries3D), new SeriesTypeDescriptor { DiagramType = XYDiagram3DType, DisplayText="Bar Series 3D" });
            seriesTypes.Add(typeof(PointSeries3D), new SeriesTypeDescriptor { DiagramType = XYDiagram3DType, DisplayText="Point Series 3D" });
            seriesTypes.Add(typeof(PieSeries3D), new SeriesTypeDescriptor { DiagramType = SimpleDiagram3DType, DisplayText="Pie Series 3D" });
            seriesTypes.Add(typeof(PieSeries2D), new SeriesTypeDescriptor { DiagramType = SimpleDiagram2DType, DisplayText = "Pie Series 2D" });
            return seriesTypes;
        }

        public class SeriesTypeDescriptor {
            public Type DiagramType { get; set; }
            public string DisplayText { get; set; }
        }

        public static int CompareComboItemsByStringContent(ComboBoxEditItem first, ComboBoxEditItem second) {
            string firstStr = first.Content as string;
            return firstStr == null ? -1 : firstStr.CompareTo(second.Content as string);
        }
        public static void InitComboBox(ComboBoxEdit comboBox, Type[] diagramFilter) {
            List<ComboBoxEditItem> itemsList = new List<ComboBoxEditItem>();
            ComboBoxEditItem item, selectedItem = null;
            foreach(Type seriesType in SeriesTypes.Keys) {
                SeriesTypeDescriptor sd = SeriesTypes[seriesType];
                if(diagramFilter == null || Array.IndexOf(diagramFilter, sd.DiagramType) >= 0) {
                    item = new ComboBoxEditItem();
                    item.Content = sd.DisplayText;
                    item.Tag = seriesType;
                    itemsList.Add(item);
                    if(object.Equals(seriesType, DefaultSeriesType))
                        selectedItem = item;
                }
            }
            itemsList.Sort(CompareComboItemsByStringContent);
            comboBox.Items.AddRange(itemsList.ToArray());
            comboBox.SelectedItem = selectedItem;
        }
        public static Diagram GenerateDiagram(Type seriesType, bool? showPointsLabels) {
            Series seriesTemplate = CreateSeriesInstance(seriesType);
            Diagram diagram = CreateDiagramBySeriesType(seriesType);
            if (diagram is XYDiagram2D)
                PrepareXYDiagram2D(diagram as XYDiagram2D);
            if (diagram is XYDiagram3D)
                PrepareXYDiagram3D(diagram as XYDiagram3D);
            if (diagram is Diagram3D)
                ((Diagram3D)diagram).RuntimeRotation = true;
            diagram.SeriesDataMember = "Series";
            seriesTemplate.ArgumentDataMember = "Arguments";
            seriesTemplate.ValueDataMember = "Values";
            if(seriesTemplate.Label == null)
                seriesTemplate.Label = new SeriesLabel();
            seriesTemplate.LabelsVisibility = (bool)showPointsLabels == true;
            if(seriesTemplate is PieSeries2D
                 || seriesTemplate is PieSeries3D
                ) {
                seriesTemplate.LegendTextPattern = "{A}";
                seriesTemplate.Label.TextPattern = "{A}: {VP:P0}";
            } else {
                seriesTemplate.LegendTextPattern = "{A}: {V:G}";
                seriesTemplate.ShowInLegend = true;
            }
            diagram.SeriesTemplate = seriesTemplate;
            return diagram;
        }
        static void PrepareXYDiagram2D(XYDiagram2D diagram) {
            if(diagram == null) return;
            diagram.AxisX = new AxisX2D();
            diagram.AxisX.Label = new AxisLabel();
            diagram.AxisX.Label.Staggered = true;
        }
        static void PrepareXYDiagram3D(XYDiagram3D diagram) {
            if(diagram == null) return;
            diagram.AxisX = new AxisX3D();
            diagram.AxisX.Label = new AxisLabel();
            diagram.AxisX.Label.Visible = false;
        }
        public static Series CreateSeriesInstance(Type seriesType) {
            Series series = (Series)Activator.CreateInstance(seriesType);
            ISupportTransparency supportTransparency = series as ISupportTransparency;
            if(supportTransparency != null) {
                bool flag = series is AreaSeries2D;
                flag = flag || series is AreaSeries3D;
                if(flag)
                    supportTransparency.Transparency = 0.4;
                else
                    supportTransparency.Transparency = 0;
            }
            return series;
        }
        static Diagram CreateDiagramBySeriesType(Type seriesType) {
            return (Diagram)Activator.CreateInstance(SeriesTypes[seriesType].DiagramType);
        }
    }
}
