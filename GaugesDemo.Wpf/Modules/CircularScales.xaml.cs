using System;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Gauges;

namespace GaugesDemo {
    public partial class CircularScales : GaugesDemoModule {
        const int NewYorkTimeOffset = 19;
        const int LondonTimeOffset = 0;
        const int MoscowTimeOffset = 4;
        DispatcherTimer timer = new DispatcherTimer();

        public CircularScales() {
            InitializeComponent();
            UpdateTime();
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            CreateCustomLabels(watchNewYorkScale);
            CreateCustomLabels(watchLondonScale);
            CreateCustomLabels(watchMoscowScale);
        }
        void OnTimedEvent(object source, EventArgs e) {
            UpdateTime();
        }
        void UpdateTime() {
            double minutes = (double)DateTime.UtcNow.Minute;
            double seconds = (double)DateTime.UtcNow.Second ;
            hourIndicatorNewYork.Value = (DateTime.UtcNow.Hour + NewYorkTimeOffset) % 12 + minutes / 60.0;
            hourIndicatorLondon.Value = (DateTime.UtcNow.Hour + LondonTimeOffset) % 12 + minutes / 60.0;
            hourIndicatorMoscow.Value = (DateTime.UtcNow.Hour + MoscowTimeOffset) % 12 + minutes / 60.0;
            minuteIndicatorNewYork.Value = ((DateTime.UtcNow.Minute + seconds / 60.0) / 60.0) * 12;
            minuteIndicatorLondon.Value = ((DateTime.UtcNow.Minute + seconds / 60.0) / 60.0) * 12;
            minuteIndicatorMoscow.Value = ((DateTime.UtcNow.Minute + seconds / 60.0) / 60.0) * 12;
            secondIndicatorNewYork.Value = (seconds / 60.0) * 12;
            secondIndicatorLondon.Value = (seconds / 60.0) * 12;
            secondIndicatorMoscow.Value = (seconds / 60.0) * 12;
        }
        void UserCustomLabels_Checked(object sender, RoutedEventArgs e) {
            ChangeVisibilityLabelsAndCustomLabels(watchNewYorkScale, userCustomLabelsCheckEdit.IsChecked.Value, !userCustomLabelsCheckEdit.IsChecked.Value);
            ChangeVisibilityLabelsAndCustomLabels(watchLondonScale, userCustomLabelsCheckEdit.IsChecked.Value, !userCustomLabelsCheckEdit.IsChecked.Value);
            ChangeVisibilityLabelsAndCustomLabels(watchMoscowScale, userCustomLabelsCheckEdit.IsChecked.Value, !userCustomLabelsCheckEdit.IsChecked.Value);
        }
        void ShowLabels_Unchecked(object sender, RoutedEventArgs e) {
            ChangeVisibilityLabelsAndCustomLabels(watchNewYorkScale, false, false);
            ChangeVisibilityLabelsAndCustomLabels(watchLondonScale, false, false);
            ChangeVisibilityLabelsAndCustomLabels(watchMoscowScale, false, false);
        }
        void CreateCustomLabels(ArcScale scale) {
            for (int i = 1; i < 13; i++) {
                ScaleCustomLabel label = new ScaleCustomLabel() { RenderTransformOrigin = new Point(0.5, 0.5) };
                label.Value = i;
                label.Offset = scale.LabelOptions.Offset;
                label.Content = Utils.ConvertArabicToRoman(i);
                label.Visible = false;
                scale.CustomLabels.Add(label);
            }
        }
        void ChangeVisibilityLabelsAndCustomLabels(ArcScale scale, bool showCustomLabels, bool showLabels) {
            foreach (ScaleCustomLabel label in scale.CustomLabels)
                label.Visible = showCustomLabels;
            scale.ShowLabels = showLabels ? DefaultBoolean.True : DefaultBoolean.False;
        }
    }
}
