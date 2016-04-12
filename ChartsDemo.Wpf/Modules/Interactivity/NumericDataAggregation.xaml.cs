using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/NumericDataAggregation(.SL).xaml"),
     CodeFile("Modules/Interactivity/NumericDataAggregation.xaml.(cs)")]
    public partial class NumericDataAggregation : ChartsDemoModule {
        public NumericDataAggregation() {
            InitializeComponent();
        }
        void ChartsDemoModule_Loaded(object sender, RoutedEventArgs e) {
            DataContext = new AggregationDataContext();
        }
    }

    public class AggregationDataContext {
        const int pointCount = 100000;

        readonly List<NumericPoint> _series = new List<NumericPoint>();

        public List<NumericPoint> Series { get { return _series; } }

        public string Title { get { return "Data Aggregation (" + pointCount.ToString() + " points)"; } }
        public string SeriesTitle { get { return "Random data"; } }

        public AggregationDataContext() {
            FillPoints(_series);
        }

        void FillPoints(List<NumericPoint> series) {
            if (series != null) {
                double value = 0;
                double argument = 0;
                Random random = new Random();

                for (double i = 0; i < pointCount; i++) {
                    series.Add(new NumericPoint(argument, value));
                    value += (random.NextDouble() * 10.0 - 5.0);
                    argument += random.NextDouble();
                }
            }
        }

    }

    public class NumericPoint {
        public double Argument { get; private set; }
        public double Value { get; private set; }

        public NumericPoint(double argument, double value) {
            this.Argument = argument;
            this.Value = value;
        }
    }
}
