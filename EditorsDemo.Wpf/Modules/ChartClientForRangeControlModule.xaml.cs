using System;
using System.Windows;
using System.Xml.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;
using DevExpress.Xpf.Charts.RangeControlClient;
using DevExpress.Xpf.Editors;

namespace EditorsDemo {
    public partial class ChartClientForRangeControlModule : EditorsDemoModule {
        const int dataCount = 100;
        public ChartClientModel ChartClientModel { get; set; }

        public ChartClientForRangeControlModule() {
            InitializeComponent();
            lbGridAlignment.SelectedIndex = 0;
            ChartClientModel = new ChartClientModel();
            ChartClientModel.NumericItemsSource = GenerateNumericData();
            ChartClientModel.DateTimeItemsSource = GenerateDateTimeData();
            this.DataContext = this;
        }
        double[] GenerateNumericData() {
            double[] data = new double[dataCount];
            Random rand = new Random();
            double value = 0;
            for (int i = 0; i < dataCount; i++) {
                value += (rand.NextDouble() - 0.5);
                data[i] = value;
            }
            return data;
        }
        List<DateTimeItem> GenerateDateTimeData() {
            List<DateTimeItem> data = new List<DateTimeItem>();
            DateTime now = DateTime.Now.Date;
            Random rand = new Random();
            double value = 0;
            for (int i = 0; i < dataCount; i++) {
                now = now.AddDays(1);
                value += (rand.NextDouble() - 0.5);
                data.Add(new DateTimeItem() { Argument = now, Value = value + Math.Sin(i * 0.6) });
            }
            return data;
        }
    }
    public class ChartClientModel : FrameworkElement {
        public static readonly DependencyProperty NumericItemsSourceProperty =
            DependencyProperty.Register("NumericItemsSource", typeof(object), typeof(ChartClientModel),
            new PropertyMetadata(null));
        public static readonly DependencyProperty DateTimeItemsSourceProperty =
            DependencyProperty.Register("DateTimeItemsSource", typeof(object), typeof(ChartClientModel),
            new PropertyMetadata(null));
        public static readonly DependencyProperty MinimumGridSpacingProperty =
            DependencyProperty.Register("MinimumGridSpacing", typeof(double), typeof(ChartClientModel),
            new PropertyMetadata(1.0));
        public static readonly DependencyProperty MiddleGridSpacingProperty =
            DependencyProperty.Register("MiddleGridSpacing", typeof(double), typeof(ChartClientModel),
            new PropertyMetadata(3.0));
        public static readonly DependencyProperty MaximumGridSpacingProperty =
            DependencyProperty.Register("MaximumGridSpacing", typeof(double), typeof(ChartClientModel),
            new PropertyMetadata(5.0));
        public static readonly DependencyProperty DateTimeGridAlignmentProperty;
        public static readonly DependencyProperty GridSpacingVisibilityProperty =
            DependencyProperty.Register("GridSpacingVisibility", typeof(Visibility), typeof(ChartClientModel),
           new PropertyMetadata(Visibility.Collapsed));

        protected static void DateTimeGridAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ChartClientModel model = d as ChartClientModel;
            if (model != null && e.NewValue != null) {
                DateTimeGridAlignment gridAlignment = (DateTimeGridAlignment)(((ListBoxEditItem)(e.NewValue)).Tag);
                model.GridSpacingVisibility = gridAlignment.Equals(DateTimeGridAlignment.Auto) ? Visibility.Collapsed : Visibility.Visible;
                model.UpdateGridSpacing(gridAlignment);
            }
        }

        static ChartClientModel() {
            DateTimeGridAlignmentProperty = DependencyProperty.Register("DateTimeGridAlignment", typeof(object), typeof(ChartClientModel), new PropertyMetadata(DateTimeGridAlignment.Day, DateTimeGridAlignmentChanged));
        }

        public double MinimumGridSpacing {
            get { return (double)GetValue(MinimumGridSpacingProperty); }
            set { SetValue(MinimumGridSpacingProperty, value); }
        }
        public double MiddleGridSpacing {
            get { return (double)GetValue(MiddleGridSpacingProperty); }
            set { SetValue(MiddleGridSpacingProperty, value); }
        }
        public double MaximumGridSpacing {
            get { return (double)GetValue(MaximumGridSpacingProperty); }
            set { SetValue(MaximumGridSpacingProperty, value); }
        }
        public object NumericItemsSource {
            get { return GetValue(NumericItemsSourceProperty); }
            set { SetValue(NumericItemsSourceProperty, value); }
        }
        public object DateTimeItemsSource {
            get { return GetValue(DateTimeItemsSourceProperty); }
            set { SetValue(DateTimeItemsSourceProperty, value); }
        }
        public Visibility GridSpacingVisibility {
            get { return (Visibility)GetValue(GridSpacingVisibilityProperty); }
            set { SetValue(GridSpacingVisibilityProperty, value); }
        }

        void UpdateGridSpacing(DateTimeGridAlignment gridAlignment) {
            MinimumGridSpacing = GetMinimumGridSpacing(gridAlignment);
            MaximumGridSpacing = GetMaximumGridSpacing(gridAlignment);
            MiddleGridSpacing = (MinimumGridSpacing + MaximumGridSpacing) / 2;
        }
        double GetMaximumGridSpacing(DateTimeGridAlignment gridAlignment) {
            switch (gridAlignment) {
                case DateTimeGridAlignment.Day:
                    return 15;
                case DateTimeGridAlignment.Month:
                    return 3;
                case DateTimeGridAlignment.Week:
                    return 6;
                case DateTimeGridAlignment.Auto:
                default:
                    return 5;
            }
        }
        double GetMinimumGridSpacing(DateTimeGridAlignment gridAlignment) {
            switch (gridAlignment) {
                case DateTimeGridAlignment.Day:
                    return 5;
                case DateTimeGridAlignment.Month:
                    return 1;
                case DateTimeGridAlignment.Week:
                    return 2;
                case DateTimeGridAlignment.Auto:
                default:
                    return 1;
            }
        }
    }
    public class DateTimeItem {
        public object Argument { get; set; }
        public object Value { get; set; }
    }
    public enum ChartViewType {
        Area,
        Bar,
        Line
    }
    public class ChartViewTypeConverter : IValueConverter {
        RangeControlClientView Parse(string type) {
            if (type == "Area")
                return new RangeControlClientAreaView();
            if (type == "Bar")
                return new RangeControlClientBarView();
            if (type == "Line")
                return new RangeControlClientLineView();
            return null;
        }
        RangeControlClientView Parse(ChartViewType type) {
            if (type == ChartViewType.Area)
                return new RangeControlClientAreaView();
            if (type == ChartViewType.Bar)
                return new RangeControlClientBarView();
            if (type == ChartViewType.Line)
                return new RangeControlClientLineView();
            return null;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string)
                return Parse(value as string);
            if (value is ChartViewType)
                return Parse((ChartViewType)value);
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is RangeControlClientAreaView)
                return ChartViewType.Area;
            if (parameter is RangeControlClientBarView)
                return ChartViewType.Bar;
            if (parameter is RangeControlClientLineView)
                return ChartViewType.Line;
            return string.Empty;
        }
    }
}
