using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Gauges;
using System.Windows.Media.Imaging;

namespace GaugesDemo {
    public partial class CircularRanges : GaugesDemoModule {
        const int cloudyIndex = 0;
        const int snowyIndex = 1;
        const int rainyIndex = 2;
        const int sunnyIndex = 3;

        PressureState pressure;
        TemperatureState temperature = TemperatureState.High;

        public CircularRanges() {
            InitializeComponent();
        }
        void LowRangeIndicatorEnter(object sender, IndicatorEnterEventArgs e) {
            pressure = PressureState.Low;
            UpdateWeatherState();
        }
        void LowRangeIndicatorLeave(object sender, IndicatorLeaveEventArgs e) {
            ArcScaleRange range = sender as ArcScaleRange;
            if(range != null && e.NewValue < range.StartValueAbsolute) {
                pressure = PressureState.Undefined;
                UpdateWeatherState();
            }
        }
        void NormalRangeIndicatorEnter(object sender, IndicatorEnterEventArgs e) {
            pressure = PressureState.Normal;
            UpdateWeatherState();
        }
        void NormalRangeIndicatorLeave(object sender, IndicatorLeaveEventArgs e) {
            ArcScaleRange range = sender as ArcScaleRange;
            if(range != null) {
                pressure = e.NewValue < range.StartValueAbsolute ? PressureState.Low : PressureState.High;
                UpdateWeatherState();
            }
        }
        void HighRangeIndicatorEnter(object sender, IndicatorEnterEventArgs e) {
            pressure = PressureState.High;
            UpdateWeatherState();
        }
        void HighRangeIndicatorLeave(object sender, IndicatorLeaveEventArgs e) {
            ArcScaleRange range = sender as ArcScaleRange;
            if(range != null) {
                pressure = e.NewValue < range.StartValueAbsolute ? PressureState.Normal : PressureState.Undefined;
                UpdateWeatherState();
            }
        }
        void HighTemperatureIndicatorEnter(object sender, IndicatorEnterEventArgs e) {
            temperature = TemperatureState.High;
            UpdateWeatherState();
        }
        void HighTemperatureIndicatorLeave(object sender, IndicatorLeaveEventArgs e) {
            temperature = TemperatureState.Low;
            UpdateWeatherState();
        }
        void UpdateWeatherState() {
            switch(pressure) {
                case PressureState.Low:
                    stateIndicator.StateIndex = temperature == TemperatureState.Low ? snowyIndex : rainyIndex;
                    break;
                case PressureState.Normal:
                    stateIndicator.StateIndex = cloudyIndex;
                    break;
                case PressureState.High:
                    stateIndicator.StateIndex = sunnyIndex;
                    break;
                default:
                    stateIndicator.StateIndex = -1;
                    break;
            }
        }
    }
}
