using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Resources;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Editors;
using System.Globalization;
using DevExpress.Utils;
using DevExpress.Mvvm;
using System.IO;

namespace ChartsDemo {
    public static class DataLoader {
        public static XDocument LoadXmlFromResources(string fileName) {
            try {
                fileName = "/ChartsDemo;component" + fileName;
                Uri uri = new Uri(fileName, UriKind.RelativeOrAbsolute);
                StreamResourceInfo info = Application.GetResourceStream(uri);
                return XDocument.Load(info.Stream);
            } catch {
                return null;
            }
        }
    }

    public static class DemoModuleControlHelper {
        internal static void PrepareComboBox(ComboBoxEdit comboBox, params string[] items) {
            foreach(string item in items)
                comboBox.Items.Add(item);
            comboBox.SelectedIndex = 0;
        }
    }

    public static class ToolTipControlHelper {
        internal static void PrepareToolTipPositionComboBox(ComboBoxEdit comboBox) {
            comboBox.Items.Add("Mouse Pointer");
            comboBox.Items.Add("Relative");
            comboBox.Items.Add("Free");
            comboBox.SelectedIndex = 0;
        }
        internal static void PrepareToolTipLocationComboBox(ComboBoxEdit comboBox) {
            comboBox.Items.Add("Top Right");
            comboBox.Items.Add("Top Left");
            comboBox.Items.Add("Bottom Right");
            comboBox.Items.Add("Bottom Left");
            comboBox.SelectedIndex = 0;
        }
        internal static ToolTipLocation GetLocationFromComboBox(int selectedIndex) {
            switch(selectedIndex) {
                case 0:
                    return ToolTipLocation.TopRight;
                case 1:
                    return ToolTipLocation.TopLeft;
                case 2:
                    return ToolTipLocation.BottomRight;
                default:
                    return ToolTipLocation.BottomLeft;
            }
        }
    }

    public static class ResolveOverlappingModeHelper {
        public static void PrepareListBox(ListBoxEdit listBox, int index) {
            listBox.Items.Add("None");
            listBox.Items.Add("Default");
            listBox.Items.Add("Hide Overlapped");
            listBox.Items.Add("Justify Around Point");
            listBox.Items.Add("Justify All Around Point");
            listBox.SelectedIndex = index;
        }
        public static ResolveOverlappingMode GetMode(ListBoxEdit listBox) {
            switch(listBox.SelectedIndex) {
                case 0:
                    return ResolveOverlappingMode.None;
                case 1:
                    return ResolveOverlappingMode.Default;
                case 2:
                    return ResolveOverlappingMode.HideOverlapped;
                case 3:
                    return ResolveOverlappingMode.JustifyAroundPoint;
                case 4:
                    return ResolveOverlappingMode.JustifyAllAroundPoint;
                default:
                    return ResolveOverlappingMode.None;
            }
        }
    }

    public static class RangeArea2DHelper {
        public static void PrepareComboBox(ComboBoxEdit comboBox, int index) {
            comboBox.Items.Add("One Label");
            comboBox.Items.Add("Two Labels");
            comboBox.Items.Add("Min Value Label");
            comboBox.Items.Add("Max Value Label");
            comboBox.Items.Add("Value1 Label");
            comboBox.Items.Add("Value2 Label");
            comboBox.SelectedIndex = index;
        }
        public static RangeAreaLabelKind GetMode(ComboBoxEdit comboBox) {
            switch(comboBox.SelectedIndex) {
                case 0:
                    return RangeAreaLabelKind.OneLabel;
                case 1:
                    return RangeAreaLabelKind.TwoLabels;
                case 2:
                    return RangeAreaLabelKind.MinValueLabel;
                case 3:
                    return RangeAreaLabelKind.MaxValueLabel;
                case 4:
                    return RangeAreaLabelKind.Value1Label;
                case 5:
                    return RangeAreaLabelKind.Value2Label;
                default:
                    return RangeAreaLabelKind.TwoLabels;
            }
        }
    }

    public static class Marker2DModelKindHelper {
        public static Marker2DKind FindActualMarker2DModelKind(Type modelType) {
            IEnumerable<Marker2DKind> marker2DKinds = Marker2DModel.GetPredefinedKinds();
            foreach(Marker2DKind marker2DKind in marker2DKinds) {
                if(Object.Equals(marker2DKind.Type, modelType))
                    return marker2DKind;
            }
            return null;
        }
    }

    public static class Bar3DModelKindHelper {
        public static void SetModel(ChartControl chart, Bar3DModel model) {
            foreach(BarSeries3D series in chart.Diagram.Series)
                series.Model = model;
        }
        public static Bar3DKind FindActualBar3DModelKind(Bar3DModel model) {
            if(model == null)
                return null;
            IEnumerable<Bar3DKind> bar3DKinds = Bar3DModel.GetPredefinedKinds();
            foreach(Bar3DKind bar3DKind in bar3DKinds) {
                if(Object.Equals(bar3DKind.Type, model.GetType()))
                    return bar3DKind;
            }
            return null;
        }
    }

    public static class Pie3DModelKindHelper {
        public static void SetModel(ChartControl chart, Pie3DModel model) {
            foreach(PieSeries3D series in chart.Diagram.Series)
                series.Model = model;
        }
        public static Pie3DKind FindActualPie3DModelKind(Pie3DModel model) {
            if(model == null)
                return null;
            IEnumerable<Pie3DKind> pie3DKinds = Pie3DModel.GetPredefinedKinds();
            foreach(Pie3DKind pie3DKind in pie3DKinds) {
                if(Object.Equals(pie3DKind.Type, model.GetType()))
                    return pie3DKind;
            }
            return null;
        }
    }

    public static class Marker3DModelKindHelper {
        public static void SetModel(ChartControl chart, Marker3DModel model) {
            foreach(MarkerSeries3D series in chart.Diagram.Series)
                series.Model = model;
        }
        public static Marker3DKind FindActualMarker3DModelKind(Marker3DModel model) {
            if(model == null)
                return null;
            IEnumerable<Marker3DKind> marker3DKinds = Marker3DModel.GetPredefinedKinds();
            foreach(Marker3DKind marker3DKind in marker3DKinds) {
                if(Object.Equals(marker3DKind.Type, model.GetType()))
                    return marker3DKind;
            }
            return null;
        }
    }

    public static class Pie2DModelKindHelper {
        public static Pie2DKind FindActualPie2DModelKind(Type modelType) {
            IEnumerable<Pie2DKind> pie2DKinds = Pie2DModel.GetPredefinedKinds();
            foreach(Pie2DKind pie2DType in pie2DKinds) {
                if(Object.Equals(pie2DType.Type, modelType))
                    return pie2DType;
            }
            return null;
        }
    }

    public static class Bar2DModelKindHelper {
        public static Bar2DKind FindActualBar2DModelKind(Type modelType) {
            IEnumerable<Bar2DKind> bar2DKinds = Bar2DModel.GetPredefinedKinds();
            foreach(Bar2DKind bar2DKind in bar2DKinds) {
                if(Object.Equals(bar2DKind.Type, modelType))
                    return bar2DKind;
            }
            return null;
        }
    }

    public static class RangeBar2DModelKindHelper {
        public static RangeBar2DKind FindActualRangeBar2DModelKind(Type modelType) {
            IEnumerable<RangeBar2DKind> bar2DKinds = RangeBar2DModel.GetPredefinedKinds();
            foreach(RangeBar2DKind bar2DKind in bar2DKinds) {
                if(Object.Equals(bar2DKind.Type, modelType))
                    return bar2DKind;
            }
            return null;
        }
    }

    public static class Stock2DModelKindHelper {
        public static Stock2DKind FindActualStock2DModelKind(Type modelType) {
            IEnumerable<Stock2DKind> stock2DKinds = Stock2DModel.GetPredefinedKinds();
            foreach(Stock2DKind stock2DKind in stock2DKinds) {
                if(Object.Equals(stock2DKind.Type, modelType))
                    return stock2DKind;
            }
            return null;
        }
    }

    public static class CandleStick2DModelKindHelper {
        public static CandleStick2DKind FindActualCandleStick2DModelKind(Type modelType) {
            IEnumerable<CandleStick2DKind> candleStick2DKinds = CandleStick2DModel.GetPredefinedKinds();
            foreach(CandleStick2DKind candleStick2DKind in candleStick2DKinds) {
                if(Object.Equals(candleStick2DKind.Type, modelType))
                    return candleStick2DKind;
            }
            return null;
        }
    }

    public class FinancialPoint {
        string argument;
        DateTime dateTimeArgument;
        double highValue;
        double lowValue;
        double openValue;
        double closeValue;

        public string Argument { get { return argument; } set { argument = value; } }
        public DateTime DateTimeArgument { get { return dateTimeArgument; } set { dateTimeArgument = value; } }
        public double HighValue { get { return highValue; } set { highValue = value; } }
        public double LowValue { get { return lowValue; } set { lowValue = value; } }
        public double OpenValue { get { return openValue; } set { openValue = value; } }
        public double CloseValue { get { return closeValue; } set { closeValue = value; } }
    }

    public class IndustryBubblePoint : BindableBase {
        public IndustryBubblePoint() {
            Name = string.Empty;
            NumberOfCases = 0;
            Rate = 0;
        }
        public string Name {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }
        public int NumberOfCases {
            get { return GetProperty(() => NumberOfCases); }
            set { SetProperty(() => NumberOfCases, value); }
        }
        public double Rate {
            get { return GetProperty(() => Rate); }
            set { SetProperty(() => Rate, value); }
        }
    }

    public class SeriesTypeItem {
        readonly Type diagramType;
        readonly Type seriesType;
        readonly string seriesName;
        readonly int seriesCount;

        public Type DiagramType { get { return diagramType; } }
        public Type SeriesType { get { return seriesType; } }
        public int SeriesCount { get { return seriesCount; } }

        public SeriesTypeItem(Type diagramType, Type seriesType, string seriesName) : this(diagramType, seriesType, seriesName, 1) { }
        public SeriesTypeItem(Type diagramType, Type seriesType, string seriesName, int seriesCount) {
            this.diagramType = diagramType;
            this.seriesType = seriesType;
            this.seriesName = seriesName;
            this.seriesCount = seriesCount;
        }
        public override string ToString() {
            return seriesName;
        }
    }

    public class DemoValuesProvider {
        public IEnumerable<Bubble2DLabelPosition> Bubble2DLabelPositions { get { return DevExpress.Utils.EnumExtensions.GetValues(typeof(Bubble2DLabelPosition)).Cast<Bubble2DLabelPosition>(); } }
        public IEnumerable<Bar2DLabelPosition> Bar2DLabelPositions { get { return DevExpress.Utils.EnumExtensions.GetValues(typeof(Bar2DLabelPosition)).Cast<Bar2DLabelPosition>(); } }
        public IEnumerable<Funnel2DLabelPosition> Funnel2DLabelPositions { get { return DevExpress.Utils.EnumExtensions.GetValues(typeof(Funnel2DLabelPosition)).Cast<Funnel2DLabelPosition>(); } }
        public IEnumerable<RangeAreaLabelKind> RangeAreaLabelKinds { get { return DevExpress.Utils.EnumExtensions.GetValues(typeof(RangeAreaLabelKind)).Cast<RangeAreaLabelKind>(); } }
        public IEnumerable<Bar2DKind> PredefinedBar2DKinds { get { return Bar2DModel.GetPredefinedKinds(); } }
        public IEnumerable<Marker2DKind> PredefinedMarker2DKinds { get { return Marker2DModel.GetPredefinedKinds(); } }
        public IEnumerable<CandleStick2DKind> PredefinedCandleStick2DKinds { get { return CandleStick2DModel.GetPredefinedKinds(); } }
        public IEnumerable<Stock2DKind> PredefinedStock2DKinds { get { return Stock2DModel.GetPredefinedKinds(); } }
        public IEnumerable<Pie2DKind> PredefinedPie2DKinds { get { return Pie2DModel.GetPredefinedKinds(); } }
        public IEnumerable<RangeBar2DKind> PredefinedRangeBar2DKinds { get { return RangeBar2DModel.GetPredefinedKinds(); } }
        public IEnumerable<ScrollBarAlignment> ScrollBarAlignments { get { return DevExpress.Utils.EnumExtensions.GetValues(typeof(ScrollBarAlignment)).Cast<ScrollBarAlignment>(); } }
    }

    public enum CircularFunction {
        TaubinsHeart,
        Cardioid,
        Lemniskate
    }

    public enum ScatterCircularFunction {
        ArchimedeanSpiral,
        Rose,
        Folium
    }

    public class FunctionsPointGenerator {
        public static List<Point> GeneratePoints(CircularFunction f) {
            switch(f) {
                case CircularFunction.TaubinsHeart:
                    return GeneratePointsOfTaubinsHeart();
                case CircularFunction.Cardioid:
                    return GeneratePointsOfCardioid();
                case CircularFunction.Lemniskate:
                    return GeneratePointsOfLemniskate();
                default:
                    return null;
            }
        }
        public static List<Point> GenerateDegreeScatterPoints(ScatterCircularFunction function) {
            return GenerateScatterPoints(function, new DegreeScatterFunctionCalculator());
        }
        public static List<Point> GenerateRadianScatterPoints(ScatterCircularFunction function) {
            return GenerateScatterPoints(function, new RadianScatterFunctionCalculator());
        }

        static List<Point> GenerateScatterPoints(ScatterCircularFunction function, ScatterFunctionCalculatorBase calculator) {
            return calculator.GenerateScatterFunctionPoints(function);
        }
        static List<Point> GeneratePointsOfLemniskate() {
            List<Point> list = new List<Point>();
            for(double x = 0; x < 360; x += 5) {
                double xRadian = DegreeToRadian(x);
                double cos = Math.Cos(2 * xRadian);
                double y = Math.Pow(Math.Abs(cos), 2);
                list.Add(new Point(x, y));
            }
            return list;
        }

        static List<Point> GeneratePointsOfCardioid() {
            List<Point> list = new List<Point>();
            const double a = 200;
            for(double x = 0; x < 360; x += 15) {
                double y = 2 * a * Math.Cos(DegreeToRadian(x));
                list.Add(new Point(x, y));
            }
            return list;
        }

        static List<Point> GeneratePointsOfTaubinsHeart() {
            List<Point> list = new List<Point>();
            for(double x = 0; x < 360; x += 15) {
                double xRadian = DegreeToRadian(x);
                double y = 2 - 2 * Math.Sin(xRadian) + Math.Sin(xRadian) * Math.Sqrt(Math.Abs(Math.Cos(xRadian))) / (Math.Sin(xRadian) + 1.4);
                list.Add(new Point(x, y));
            }
            return list;
        }

        static double DegreeToRadian(double degree) {
            return 2 * Math.PI / 360 * degree;
        }
    }

    public abstract class ScatterFunctionCalculatorBase {
        const int spiralIntervalsCount = 72;
        const int roseIntervalsCount = 288;
        const int foliumSegmentIntervalsCount = 30;

        const double roseParameter = 7.0 / 4.0;
        const double foliumDistanceLimit = 3.0;

        protected abstract double NormalizeAngle(double angle);
        protected abstract double ToRadian(double angle);
        protected abstract double FromDegrees(double angle);

        List<Point> FilterPointsByRange(List<Point> points, double min, double max) {
            List<Point> resultPoints = new List<Point>();
            foreach (Point point in points) {
                double pointValue = point.Y;
                if (pointValue <= max && pointValue >= min)
                    resultPoints.Add(point);
            }
            return resultPoints;
        }
        void CreatePolarFunctionPoints(double minAngleDegree, double maxAngleDegree, int intervalsCount, Func<double, double> function, List<Point> points) {
            double minAngle = FromDegrees(minAngleDegree);
            double maxAngle = FromDegrees(maxAngleDegree);
            double angleStep = (maxAngle - minAngle) / (double)intervalsCount;
            for (int pointIndex = 0; pointIndex <= intervalsCount; pointIndex++) {
                double angle = minAngle + pointIndex * angleStep;
                double angleRadians = ToRadian(angle);
                double distance = function(angleRadians);
                double normalAngle = NormalizeAngle(angle);
                points.Add(new Point(normalAngle, distance));
            }
        }
        double ArchimedeanSpiralFunction(double angleRadians) {
            return angleRadians;
        }
        double PolarRoseFunction(double angleRadians) {
            return Math.Max(0.0, Math.Sin(roseParameter * angleRadians));
        }
        double PolarFoliumFunction(double angleRadians) {
            double sin = Math.Sin(angleRadians);
            double cos = Math.Cos(angleRadians);
            return (3.0 * sin * cos)/ (Math.Pow(sin, 3.0) + Math.Pow(cos, 3.0));
        }

        public List<Point> GenerateScatterFunctionPoints(ScatterCircularFunction function) {
            List<Point> points = new List<Point>();
            switch (function) {
                case ScatterCircularFunction.ArchimedeanSpiral:
                    CreatePolarFunctionPoints(0.0, 720.0, spiralIntervalsCount, ArchimedeanSpiralFunction, points);
                    break;
                case ScatterCircularFunction.Rose:
                    CreatePolarFunctionPoints(0.0, 1440.0, roseIntervalsCount, PolarRoseFunction, points);
                    break;
                case ScatterCircularFunction.Folium:
                    CreatePolarFunctionPoints(120.0, 180.0, foliumSegmentIntervalsCount, PolarFoliumFunction, points);
                    CreatePolarFunctionPoints(0.0, 90.0, foliumSegmentIntervalsCount, PolarFoliumFunction, points);
                    CreatePolarFunctionPoints(270.0, 330.0, foliumSegmentIntervalsCount, PolarFoliumFunction, points);
                    points = FilterPointsByRange(points, 0.0, foliumDistanceLimit);
                    break;
            }
            return points;
        }
    }

    public class RadianScatterFunctionCalculator : ScatterFunctionCalculatorBase {
        protected override double NormalizeAngle(double angle) {
            return angle % (Math.PI * 2.0);
        }
        protected override double ToRadian(double angle) {
            return angle;
        }
        protected override double FromDegrees(double angle) {
            return angle * Math.PI / 180.0;
        }
    }

    public class DegreeScatterFunctionCalculator : ScatterFunctionCalculatorBase {
        protected override double NormalizeAngle(double angle) {
            return angle % 360;
        }
        protected override double ToRadian(double angle) {
            return angle * Math.PI / 180.0;
        }
        protected override double FromDegrees(double angle) {
            return angle;
        }
    }

    public class Bar2DKindToTickmarksLengthConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Bar2DKind bar2DKind = value as Bar2DKind;
            if(bar2DKind != null) {
                switch(bar2DKind.Name) {
                    case "Glass Cylinder":
                        return 18;
                    case "Quasi-3D Bar":
                        return 9;
                }
            }
            return 5;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class Bar2DKindToBar2DModelConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Bar2DKind bar2DKind = value as Bar2DKind;
            if(bar2DKind != null)
                return Activator.CreateInstance(bar2DKind.Type);
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class RangeBar2DKindToRangeBar2DModelConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            RangeBar2DKind bar2DKind = value as RangeBar2DKind;
            if(bar2DKind != null)
                return Activator.CreateInstance(bar2DKind.Type);
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class Marker2DKindToMarker2DModelConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Marker2DKind marker2DKind = value as Marker2DKind;
            if(marker2DKind != null)
                return Activator.CreateInstance(marker2DKind.Type);
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class CandleStick2DKindToCandleStick2DModelConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            CandleStick2DKind candleStick2DKind = value as CandleStick2DKind;
            if(candleStick2DKind != null)
                return Activator.CreateInstance(candleStick2DKind.Type);
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class Stock2DKindToStock2DModelConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Stock2DKind stock2DKind = value as Stock2DKind;
            if(stock2DKind != null)
                return Activator.CreateInstance(stock2DKind.Type);
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class Pie2DKindToPie2DModelConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Pie2DKind pie2DKind = value as Pie2DKind;
            if(pie2DKind != null)
                return Activator.CreateInstance(pie2DKind.Type);
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class MarkerSizeToLabelIndentConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((double)value) / 2;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class IsCheckedToVisibilityConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if((bool)value)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class BoolToResolveOverlappingModeConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            bool booleanValue = (bool)value;
            if(booleanValue == true)
                return ResolveOverlappingMode.Default;
            else
                return ResolveOverlappingMode.None;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class StringToRotationDirectionConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string str = value as String;
            if(str == null || targetType != typeof(CircularDiagramRotationDirection))
                return null;
            if(str == "Clockwise")
                return CircularDiagramRotationDirection.Clockwise;
            else
                return CircularDiagramRotationDirection.Counterclockwise;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class StringToCircularDiagramShapeStyleConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string str = value as string;
            if(str == null || targetType != typeof(CircularDiagramShapeStyle))
                return null;
            if(str == "Circle")
                return CircularDiagramShapeStyle.Circle;
            else
                return CircularDiagramShapeStyle.Polygon;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class StringToCrosshairSnapModeConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string str = value as string;
            if(str == null || targetType != typeof(CrosshairSnapMode))
                return null;
            if(str == "Nearest Argument")
                return CrosshairSnapMode.NearestArgument;
            else
                return CrosshairSnapMode.NearestValue;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public static class PaletteSelectorHelper {
        static Palette actualPalette = new OfficePalette();

        public static Palette ActualPalette {
            get { return actualPalette; }
            set { actualPalette = value; }
        }
    }

    public static class CsvReader {
        const string FileNamePrefix = "/ChartsDemo;component/Data/";

        public static List<FinancialPoint> ReadFinancialData(string fileName) {
            string longFileName = string.Empty;
            StreamReader reader;
            var dataSource = new List<FinancialPoint>();
            try {
                longFileName = FileNamePrefix + fileName;
                Uri uri = new Uri(longFileName, UriKind.RelativeOrAbsolute);
                StreamResourceInfo info = Application.GetResourceStream(uri);
                reader = new StreamReader(info.Stream);
                while (!reader.EndOfStream) {
                    string line = reader.ReadLine();
                    var values = line.Split(',');
                    var point = new FinancialPoint();
                    point.DateTimeArgument = DateTime.ParseExact(values[0], "yyyy.MM.dd", null);
                    point.OpenValue = double.Parse(values[1], CultureInfo.InvariantCulture);
                    point.HighValue = double.Parse(values[2], CultureInfo.InvariantCulture);
                    point.LowValue = double.Parse(values[3], CultureInfo.InvariantCulture);
                    point.CloseValue = double.Parse(values[4], CultureInfo.InvariantCulture);
                    dataSource.Add(point);
                }
            }
            catch {
                throw new Exception("It's impossible to load " + fileName);
            }
            return dataSource;
        }
    }
}
