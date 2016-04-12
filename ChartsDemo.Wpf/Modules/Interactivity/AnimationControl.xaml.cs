using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/AnimationControl(.SL).xaml"),
     CodeFile("Modules/Interactivity/AnimationControl.xaml.(cs)")]
    public partial class AnimationControl : ChartsDemoModule {
        static List<DataPoint> CreateDataSource() {
            List<DataPoint> dataSource = new List<DataPoint>();
            dataSource.Add(DataPoint.CreateDataPoint("A", 15, 8, 3));
            dataSource.Add(DataPoint.CreateDataPoint("B", 13, 12, 10));
            dataSource.Add(DataPoint.CreateDataPoint("C", 7, 4, 6));
            dataSource.Add(DataPoint.CreateDataPoint("D", 5, 9, 6));
            dataSource.Add(DataPoint.CreateDataPoint("E", 23, 15, 8));
            dataSource.Add(DataPoint.CreateDataPoint("F", 21, 19, 10));
            return dataSource;
        }
        static List<DataPoint> CreatePieDataSource() {
            List<DataPoint> dataSource = new List<DataPoint>();
            Random random = new Random(0);
            for (int i = 0; i < 16; i++)
                dataSource.Add(DataPoint.CreatePieDataPoint("1", random.NextDouble() * 3 + 1));
            return dataSource;
        }
        static List<DataPoint> CreateFunnelDataSource() {
            List<DataPoint> dataSource = new List<DataPoint>();
            dataSource.Add(DataPoint.CreatePieDataPoint("Visited a Web Site", 9152));
            dataSource.Add(DataPoint.CreatePieDataPoint("Downloaded a Trial", 6870));
            dataSource.Add(DataPoint.CreatePieDataPoint("Contacted to Support", 5121));
            dataSource.Add(DataPoint.CreatePieDataPoint("Subscribed", 2224));
            dataSource.Add(DataPoint.CreatePieDataPoint("Renewed", 1670));
            return dataSource;
        }
        static List<DataPoint> CreatePolarDataSource() {
            List<DataPoint> dataSource = new List<DataPoint>();
            Random random = new Random();
            for (int i = 0; i < 7; i++)
                dataSource.Add(DataPoint.CreateDataPoint(Math.Floor(random.NextDouble() * 360), Math.Floor(random.NextDouble() * 25)));
            return dataSource;
        }
        static List<DataPoint> CreateBarDataSource() {
            List<DataPoint> dataSource = new List<DataPoint>();
            dataSource.Add(DataPoint.CreateBarDataPoint("A", 1, 3, 8, 15));
            dataSource.Add(DataPoint.CreateBarDataPoint("B", 2, 10, 12, 13));
            dataSource.Add(DataPoint.CreateBarDataPoint("C", 5, 6, 7, 4));
            dataSource.Add(DataPoint.CreateBarDataPoint("D", -2, -3, -1.5, -1.3));
            dataSource.Add(DataPoint.CreateBarDataPoint("E", -2.1, -3.2, -1, -0.6));
            dataSource.Add(DataPoint.CreateBarDataPoint("F", -2.4, -3.8, -0.7, -4));
            return dataSource;
        }
        static List<DataPoint> CreateScatterDataSource() {
            List<DataPoint> dataSource = new List<DataPoint>();
            dataSource.Add(DataPoint.CreateScatterDataPoint("A", 15));
            dataSource.Add(DataPoint.CreateScatterDataPoint("B", 11));
            dataSource.Add(DataPoint.CreateScatterDataPoint("C", 7));
            dataSource.Add(DataPoint.CreateScatterDataPoint("D", 9));
            dataSource.Add(DataPoint.CreateScatterDataPoint("C", 23));
            dataSource.Add(DataPoint.CreateScatterDataPoint("B", 21));
            return dataSource;
        }
        static List<DataPoint> CreateBubbleDataSource() {
            List<DataPoint> dataSource = new List<DataPoint>();
            dataSource.Add(DataPoint.CreateBubbleDataPoint("A", 10, 5.9));
            dataSource.Add(DataPoint.CreateBubbleDataPoint("B", 5, 4.9));
            dataSource.Add(DataPoint.CreateBubbleDataPoint("C", 2, 4.6));
            dataSource.Add(DataPoint.CreateBubbleDataPoint("D", 5, 3));
            dataSource.Add(DataPoint.CreateBubbleDataPoint("E", 2, 2.9));
            dataSource.Add(DataPoint.CreateBubbleDataPoint("F", 4, 2.8));
            dataSource.Add(DataPoint.CreateBubbleDataPoint("G", 2, 2.6));
            dataSource.Add(DataPoint.CreateBubbleDataPoint("H", 3, 2.5));
            dataSource.Add(DataPoint.CreateBubbleDataPoint("I", 4, 2.4));
            return dataSource;
        }
        static List<DataPoint> CreateRangeDataSource() {
            List<DataPoint> dataSource = new List<DataPoint>();
            dataSource.Add(DataPoint.CreateBarDataPoint("A", 3, 13, 5, 15));
            dataSource.Add(DataPoint.CreateBarDataPoint("B", 5, 8, 3, 11));
            dataSource.Add(DataPoint.CreateBarDataPoint("C", 2, 9, 6, 11));
            dataSource.Add(DataPoint.CreateBarDataPoint("D", -2, -8, -1, -9));
            dataSource.Add(DataPoint.CreateBarDataPoint("E", -1, -6, -3, -9));
            dataSource.Add(DataPoint.CreateBarDataPoint("F", -3, -7, -2, -6));
            return dataSource;
        }
        static List<DataPoint> CreateFinancialDataSource() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/Dell.xml");
            List<DataPoint> dataSource = new List<DataPoint>();
            if (document != null) {
                foreach (XElement element in document.Element("Dell").Elements()) {
                    dataSource.Add(DataPoint.CreateFinancialDataPoint(
                        element.Element("Argument").Value,
                        Convert.ToDouble(element.Element("LowValue").Value, CultureInfo.InvariantCulture),
                        Convert.ToDouble(element.Element("HighValue").Value, CultureInfo.InvariantCulture),
                        Convert.ToDouble(element.Element("OpenValue").Value, CultureInfo.InvariantCulture),
                        Convert.ToDouble(element.Element("CloseValue").Value, CultureInfo.InvariantCulture)));
                }
            }
            return dataSource;
        }
        static int GetDefaultAnimationIndex(IEnumerable<AnimationKind> animationKinds, Type defaultAnimationType) {
            int index = 0;
            foreach (AnimationKind animationKind in animationKinds) {
                if (animationKind.Type != null && animationKind.Type.Equals(defaultAnimationType) && animationKind.Name != "None")
                    return index;
                index++;
            }
            return 0;
        }
        static AnimationKind GetNoneAnimation(IEnumerable<AnimationKind> animationKinds) {
            if (animationKinds == null)
                return new AnimationKind(null, "None");
            IEnumerator<AnimationKind> enumerator = animationKinds.GetEnumerator();
            enumerator.MoveNext();
            AnimationKind current = enumerator.Current;
            if (current == null)
                return new AnimationKind(null, "None");
            else
                return new AnimationKind(current.Type, "None");
        }
        static void InitializeAnimationListBoxEdit(ListBoxEdit listBoxEdit, IEnumerable<AnimationKind> animationKinds, Type defaultAnimationType) {
            List<AnimationKind> allAnimationKinds = new List<AnimationKind>();
            listBoxEdit.ClearValue(ListBoxEdit.SelectedIndexProperty);
            AnimationKind noneAnimation = GetNoneAnimation(animationKinds);
            allAnimationKinds.Add(noneAnimation);
            allAnimationKinds.AddRange(animationKinds);
            listBoxEdit.ItemsSource = allAnimationKinds;
            listBoxEdit.SelectedIndex = GetDefaultAnimationIndex(allAnimationKinds, defaultAnimationType);
        }

        bool loading = false;
        List<DataPoint> dataSource;
        List<DataPoint> pieDataSource;
        List<DataPoint> funnelDataSource;
        List<DataPoint> polarDataSource;
        List<DataPoint> barDataSource;
        List<DataPoint> bubbleDataSource;
        List<DataPoint> scatterDataSource;
        List<DataPoint> rangeDataSource;
        List<DataPoint> financialDataSource;

        Series FirstSeries { get { return chart.Diagram != null && chart.Diagram.Series.Count > 0 ? chart.Diagram.Series[0] : null; } }
        bool UnwindAnimationSupported { get { return FirstSeries != null && (FirstSeries is AreaSeries2D || FirstSeries is AreaStackedSeries2D || FirstSeries is LineSeries2D); } }
        Type DefaultPointAnimationType {
            get {
                if (FirstSeries != null) {
                    if (FirstSeries is BarSideBySideSeries2D || FirstSeries is RangeBarSeries2D)
                        return typeof(Bar2DGrowUpAnimation);
                    if (FirstSeries is BarStackedSeries2D)
                        return typeof(Bar2DDropInAnimation);
                    if (FirstSeries is PointSeries2D)
                        return typeof(Marker2DSlideFromLeftAnimation);
                    if (FirstSeries is BubbleSeries2D)
                        return typeof(Marker2DWidenAnimation);
                    if (FirstSeries is LineSeries2D || FirstSeries is AreaSeries2D || FirstSeries is RangeAreaSeries2D)
                        return typeof(Marker2DFadeInAnimation);
                    if (FirstSeries is AreaStackedSeries2D)
                        return typeof(AreaStacked2DFadeInAnimation);
                    if (FirstSeries is FinancialSeries2D)
                        return typeof(Stock2DSlideFromTopAnimation);
                    if (FirstSeries is PieSeries2D)
                        return typeof(Pie2DGrowUpAnimation);
                    if (FirstSeries is FunnelSeries2D)
                        return typeof(Funnel2DWidenAnimation);
                    if (FirstSeries is CircularSeries2D)
                        return typeof(CircularMarkerSlideToCenterAnimation);
                }
                return null;
            }
        }
        Type DefaultSeriesAnimationType {
            get {
                if (FirstSeries != null) {
                    if (FirstSeries is LineSeries2D)
                        return typeof(Line2DStretchFromNearAnimation);
                    if (FirstSeries is AreaSeries2D || FirstSeries is RangeAreaSeries2D)
                        return typeof(Area2DStretchFromNearAnimation);
                    if (FirstSeries is AreaStackedSeries2D)
                        return typeof(Area2DDropFromFarAnimation);
                    if (FirstSeries is CircularAreaSeries2D)
                        return typeof(CircularAreaZoomInAnimation);
                    if (FirstSeries is CircularLineSeries2D)
                        return typeof(CircularLineZoomInAnimation);
                }
                return null;
            }
        }
        public override ChartControl ActualChart { get { return chart; } }

        public AnimationControl() {
            InitializeComponent();
            loading = true;
            try {
                dataSource = CreateDataSource();
                pieDataSource = CreatePieDataSource();
                funnelDataSource = CreateFunnelDataSource();
                polarDataSource = CreatePolarDataSource();
                barDataSource = CreateBarDataSource();
                bubbleDataSource = CreateBubbleDataSource();
                scatterDataSource = CreateScatterDataSource();
                rangeDataSource = CreateRangeDataSource();
                financialDataSource = CreateFinancialDataSource();
                InitializeSeriesComboBox();
                cbSeriesTypes.SelectedIndex = 0;
            }
            finally {
                loading = false;
            }
        }
        void InitializeSeriesComboBox() {
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(BarSideBySideSeries2D), "2D Side-By-Side Bars", 3));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(BarStackedSeries2D), "2D Stacked Bars", 3));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(BarFullStackedSeries2D), "2D Full-Stacked Bars", 3));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(BarSideBySideStackedSeries2D), "2D Side-By-Side Stacked Bars", 4));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(BarSideBySideFullStackedSeries2D), "2D Side-By-Side Full-Stacked Bars", 4));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(RangeBarOverlappedSeries2D), "2D Overlapped Range Bars", 2));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(RangeBarSideBySideSeries2D), "2D Side-By-Side Range Bars", 2));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(PointSeries2D), "2D Points", 3));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(BubbleSeries2D), "2D Bubbles", 1));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(LineSeries2D), "2D Lines", 3));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(LineStackedSeries2D), "2D Stacked Lines", 3));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(LineFullStackedSeries2D), "2D Full-Stacked Lines", 3));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(LineStepSeries2D), "2D Step Lines", 1));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(LineScatterSeries2D), "2D Scatter Lines", 1));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(AreaSeries2D), "2D Areas", 3));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(AreaStackedSeries2D), "2D Stacked Areas", 3));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(AreaFullStackedSeries2D), "2D Full-Stacked Areas", 3));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(AreaStepSeries2D), "2D Step Areas", 1));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(RangeAreaSeries2D), "2D Range Areas", 1));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(StockSeries2D), "2D Stocks", 1));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(CandleStickSeries2D), "2D Candle-Sticks", 1));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(SimpleDiagram2D), typeof(PieSeries2D), "2D Pie", 1));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(SimpleDiagram2D), typeof(NestedDonutSeries2D), "2D Nested Donut", 2));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(SimpleDiagram2D), typeof(FunnelSeries2D), "2D Funnel", 1));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(RadarDiagram2D), typeof(RadarAreaSeries2D), "2D Radar Area", 1));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(RadarDiagram2D), typeof(RadarLineSeries2D), "2D Radar Line", 1));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(RadarDiagram2D), typeof(RadarPointSeries2D), "2D Radar Point", 1));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(PolarDiagram2D), typeof(PolarAreaSeries2D), "2D Polar Area", 1));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(PolarDiagram2D), typeof(PolarLineSeries2D), "2D Polar Line", 1));
            cbSeriesTypes.Items.Add(new SeriesTypeItem(typeof(PolarDiagram2D), typeof(PolarPointSeries2D), "2D Polar Point", 1));
        }
        void InitializeSeries(Series series, int seriesNumber) {
            if (series is BarSideBySideSeries2D)
                BarSideBySideSeries2D.SetLabelPosition(series.Label, Bar2DLabelPosition.Outside);
            if (series is BarSeries2D)
                series.DataSource = barDataSource;
            else if (series is LineScatterSeries2D)
                series.DataSource = scatterDataSource;
            else
                series.DataSource = dataSource;
            if (series is LineSeries2D)
                ((LineSeries2D)series).MarkerVisible = true;
            if (series is AreaSeries2D)
                ((AreaSeries2D)series).MarkerVisible = true;
            series.ArgumentDataMember = "Argument";
            series.ValueDataMember = "Value" + seriesNumber.ToString();
        }
        void InitializePieSeries(PieSeries2D pieSeries) {
            pieSeries.DataSource = pieDataSource;
            pieSeries.ArgumentDataMember = "Argument";
            pieSeries.ValueDataMember = "Value";
        }
        void InitializeFunnelSeries(FunnelSeries2D funnelSeries) {
            funnelSeries.DataSource = funnelDataSource;
            funnelSeries.ArgumentDataMember = "Argument";
            funnelSeries.ValueDataMember = "Value";
            funnelSeries.Label.TextPattern = "{A}: {V}";
        }
        void InitializePolarSeries(CircularSeries2D funnelSeries) {
            funnelSeries.DataSource = polarDataSource;
            funnelSeries.ArgumentDataMember = "NumericalArgument";
            funnelSeries.ValueDataMember = "Value";
        }
        void InitializeBubbleSeries(BubbleSeries2D series) {
            series.ColorEach = true;
            series.MinSize = 1;
            series.MaxSize = 2;
            series.DataSource = bubbleDataSource;
            series.ArgumentDataMember = "Argument";
            series.ValueDataMember = "Value";
            series.WeightDataMember = "Weight";
        }
        void InitializeRangeBarSeries(RangeBarSeries2D series, int seriesNumber) {
            series.DataSource = rangeDataSource;
            series.ArgumentDataMember = "Argument";
            if (seriesNumber == 1) {
                series.ValueDataMember = "Value1";
                series.Value2DataMember = "Value2";
            }
            else if (seriesNumber == 2) {
                series.ValueDataMember = "Value3";
                series.Value2DataMember = "Value4";
            }
        }
        void InitializeRangeAreaSeries(RangeAreaSeries2D series) {
            series.DataSource = rangeDataSource;
            series.ArgumentDataMember = "Argument";
            series.ValueDataMember = "Value1";
            series.Value2DataMember = "Value2";
        }
        void InitializeFinancialSeries(FinancialSeries2D series) {
            series.ArgumentScaleType = ScaleType.DateTime;
            series.DataSource = financialDataSource;
            series.ArgumentDataMember = "Argument";
            series.LowValueDataMember = "LowValue";
            series.HighValueDataMember = "HighValue";
            series.OpenValueDataMember = "OpenValue";
            series.CloseValueDataMember = "CloseValue";
        }
        void UpdateSeries(Series series, int index) {
            if (series is BubbleSeries2D) {
                InitializeBubbleSeries(series as BubbleSeries2D);
                return;
            }
            else if (series is FinancialSeries2D) {
                InitializeFinancialSeries(series as FinancialSeries2D);
                XYDiagram2D diagram = chart.Diagram as XYDiagram2D;
                if (diagram != null)
                    AxisY2D.SetAlwaysShowZeroLevel(diagram.ActualAxisY.ActualWholeRange, false);
                return;
            }
            if (series is PieSeries2D) {
                PieSeries2D pieSeries = series as PieSeries2D;
                if (!(pieSeries is NestedDonutSeries2D))
                    pieSeries.HoleRadiusPercent = 0;
                InitializePieSeries(pieSeries);
                return;
            }
            if (series is FunnelSeries2D) {
                InitializeFunnelSeries(series as FunnelSeries2D);
                return;
            }
            if (series is RangeBarSeries2D) {
                RangeBarSeries2D rangeBar = series as RangeBarSeries2D;
                InitializeRangeBarSeries(series as RangeBarSeries2D, index + 1);
                series.LabelsVisibility = false;
                if (rangeBar is RangeBarOverlappedSeries2D && index == 1)
                    rangeBar.BarWidth = 0.2;
                return;
            }
            if (series is RangeAreaSeries2D) {
                InitializeRangeAreaSeries(series as RangeAreaSeries2D);
                return;
            }
            if (series is CircularSeries2D) {
                InitializePolarSeries(series as CircularSeries2D);
                return;
            }

            InitializeSeries(series, index + 1);
        }
        void UpdateSeries() {
            SeriesTypeItem seriesTypeItem = cbSeriesTypes.SelectedItem as SeriesTypeItem;
            if (seriesTypeItem != null) {
                chart.BeginInit();
                try {
                    chart.Diagram = (Diagram)Activator.CreateInstance(seriesTypeItem.DiagramType);
                    for (int i = 0; i < seriesTypeItem.SeriesCount; i++) {
                        Series series = (Series)Activator.CreateInstance(seriesTypeItem.SeriesType);
                        if (loading)
                            series.AnimationAutoStartMode = AnimationAutoStartMode.SetStartState;
                        series.Label = new SeriesLabel();

                        UpdateSeries(series, i);

                        ISupportStackedGroup supportStackedGroup = series as ISupportStackedGroup;
                        if (supportStackedGroup != null)
                            supportStackedGroup.StackedGroup = i % 2;
                        ISupportTransparency supportTransparency = series as ISupportTransparency;
                        if (supportTransparency != null)
                            supportTransparency.Transparency = 0.3;
                        series.LabelsVisibility = true;
                        series.Label.ResolveOverlappingMode = ResolveOverlappingMode.Default;
                        chart.Diagram.Series.Add(series);
                    }
                }
                finally {
                    chart.EndInit();
                }
            }
        }
        void PrepareAnimation() {
            if (FirstSeries != null) {
                if (FirstSeries.GetSeriesAnimation() != null) {
                    for (int i = 0; i < chart.Diagram.Series.Count; i++) {
                        Series series = chart.Diagram.Series[i];
                        TimeSpan beginTime = TimeSpan.FromMilliseconds(Math.Round((double)series.GetSeriesAnimation().Duration.TotalMilliseconds / 4.0) * i);
                        series.GetSeriesAnimation().BeginTime = beginTime;
                        if (series.GetPointAnimation() != null)
                            series.GetPointAnimation().BeginTime = beginTime;
                    }
                }
                else if (FirstSeries.GetPointAnimation() != null) {
                    for (int i = 0; i < chart.Diagram.Series.Count; i++) {
                        Series series = chart.Diagram.Series[i];
                        series.GetPointAnimation().BeginTime = TimeSpan.FromMilliseconds(series.GetPointAnimation().PointDelay.TotalMilliseconds * i);
                        series.GetPointAnimation().PointDelay = TimeSpan.FromMilliseconds(series.GetPointAnimation().PointDelay.TotalMilliseconds * chart.Diagram.Series.Count);
                    }
                }
            }
        }
        void ChangeAnimation() {
            if (chart.Diagram != null) {
                if (lbPointAnimation.SelectedItem != null) {
                    foreach (Series series in chart.Diagram.Series) {
                        var animationKind = (AnimationKind)lbPointAnimation.SelectedItem;
                        if (animationKind.Name == "None" && animationKind.Type != null) {
                            var pointAnimation = (SeriesPointAnimationBase)Activator.CreateInstance(animationKind.Type);
                            pointAnimation.Duration = new TimeSpan(0);
                            pointAnimation.PointDelay = new TimeSpan(0);
                            pointAnimation.BeginTime = new TimeSpan(0);
                            series.SetPointAnimation(pointAnimation);
                        }
                        else
                            series.SetPointAnimation(animationKind.Type != null ? (SeriesPointAnimationBase)Activator.CreateInstance(animationKind.Type) : null);
                    }
                }
                if (lbSeriesAnimation.SelectedItem != null) {
                    foreach (Series series in chart.Diagram.Series) {
                        var animationKind = (AnimationKind)lbSeriesAnimation.SelectedItem;
                        if (animationKind.Name == "None" && animationKind.Type != null) {
                            var seriesAnimation = (SeriesAnimationBase)Activator.CreateInstance(animationKind.Type);
                            seriesAnimation.Duration = new TimeSpan(0);
                            seriesAnimation.BeginTime = new TimeSpan(0);
                            series.SetSeriesAnimation(seriesAnimation);
                        }
                        else
                            series.SetSeriesAnimation(animationKind.Type != null ? (SeriesAnimationBase)Activator.CreateInstance(animationKind.Type) : null);
                    }
                }
            }
            PrepareAnimation();
        }

        void cbSeriesTypes_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            UpdateSeries();
            if (FirstSeries != null) {
                InitializeAnimationListBoxEdit(lbPointAnimation, FirstSeries.GetPredefinedPointAnimationKinds(), DefaultPointAnimationType);
                InitializeAnimationListBoxEdit(lbSeriesAnimation, FirstSeries.GetPredefinedSeriesAnimationKinds(), DefaultSeriesAnimationType);
                if (!loading)
                    chart.Animate();
            }
        }
        void lbPointAnimation_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            ChangeAnimation();
            if (!loading)
                chart.Animate();
        }
        void lbSeriesAnimation_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            ChangeAnimation();
            if (!loading)
                chart.Animate();
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }

    public class DataPoint {
        public static DataPoint CreateDataPoint(string argument, double value1, double value2, double value3) {
            return new DataPoint(argument, double.NaN, value1, value2, value3, double.NaN, double.NaN);
        }
        public static DataPoint CreateBarDataPoint(string argument, double value1, double value2, double value3, double value4) {
            return new DataPoint(argument, double.NaN, value1, value2, value3, value4, double.NaN);
        }
        public static DataPoint CreateScatterDataPoint(string argument, double value1) {
            return new DataPoint(argument, double.NaN, value1, double.NaN, double.NaN, double.NaN, double.NaN);
        }
        public static DataPoint CreateBubbleDataPoint(string argument, double value, double weight) {
            return new DataPoint(argument, value, double.NaN, double.NaN, double.NaN, double.NaN, weight);
        }
        public static DataPoint CreateFinancialDataPoint(string argument, double lowValue, double highValue, double openValue, double closeValue) {
            return new DataPoint(argument, double.NaN, lowValue, highValue, openValue, closeValue, double.NaN);
        }
        public static DataPoint CreatePieDataPoint(string argument, double value) {
            return new DataPoint(argument, value, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);
        }
        public static DataPoint CreateDataPoint(double argument, double value) {
            return new DataPoint(argument, value);
        }

        public object Argument { get; private set; }
        public double NumericalArgument { get; private set; }
        public double Value { get; private set; }
        public double Value1 { get; private set; }
        public double Value2 { get; private set; }
        public double Value3 { get; private set; }
        public double Value4 { get; private set; }
        public double Weight { get; private set; }
        public double LowValue { get; private set; }
        public double HighValue { get; private set; }
        public double OpenValue { get; private set; }
        public double CloseValue { get; private set; }

        DataPoint(string argument, double value, double value1, double value2, double value3, double value4, double weight) {
            Argument = argument;
            Value = value;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
            Weight = weight;
            LowValue = value1;
            HighValue = value2;
            OpenValue = value3;
            CloseValue = value4;
        }
        DataPoint(double argument, double value) {
            this.NumericalArgument = argument;
            this.Value = value;
        }
    }
}
