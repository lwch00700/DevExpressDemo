using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using DevExpress.Utils;
using DevExpress.Xpf.Gauges;
using DevExpress.Mvvm;

namespace GaugesDemo {
    public enum PressureState {
        Low,
        Normal,
        High,
        Undefined
    }

    public enum TemperatureState {
        Low,
        High
    }

    public class PredefinedElementKindToCircularGaugeModel : IValueConverter {
        #region IValueConverter implementation
        object IValueConverter.Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            PredefinedElementKind gaugeModelKind = value as PredefinedElementKind;
            if(gaugeModelKind != null && gaugeModelKind.Type.BaseType == typeof(CircularGaugeModel))
                return Activator.CreateInstance(gaugeModelKind.Type);
            return value;
        }
        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class PredefinedElementKindToLinearGaugeModel : IValueConverter {
        #region IValueConverter implementation
        object IValueConverter.Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            PredefinedElementKind gaugeModelKind = value as PredefinedElementKind;
            if(gaugeModelKind != null && gaugeModelKind.Type.BaseType == typeof(LinearGaugeModel))
                return Activator.CreateInstance(gaugeModelKind.Type);
            return value;
        }
        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class PredefinedElementKindToDigitalGaugeModel : IValueConverter {
        #region IValueConverter implementation
        object IValueConverter.Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            PredefinedElementKind gaugeModelKind = value as PredefinedElementKind;
            if(gaugeModelKind != null && gaugeModelKind.Type.BaseType == typeof(DigitalGaugeModel))
                return Activator.CreateInstance(gaugeModelKind.Type);
            return value;
        }
        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class DemoValuesProvider {
        public IEnumerable<PredefinedElementKind> PredefinedCircularGaugeModelKinds { get { return CircularGaugeControl.PredefinedModels; } }
        public IEnumerable<PredefinedElementKind> PredefinedLinearGaugeModelKinds { get { return LinearGaugeControl.PredefinedModels; } }
        public IEnumerable<PredefinedElementKind> PredefinedDigitalGaugeModelKinds { get { return DigitalGaugeControl.PredefinedModels; } }
    }

    public class StringToEasingFunctionConvert : IValueConverter {
        #region IValueConverter implementation
        object IValueConverter.Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            string functionEase = value as string;
            IEasingFunction returnFunctionEase = null;
            if(functionEase != null)
                switch(functionEase) {
                    case "ElasticEase":
                        returnFunctionEase = new ElasticEase();
                        ((ElasticEase)returnFunctionEase).Springiness = 8;
                        ((ElasticEase)returnFunctionEase).Oscillations = 10;
                        break;
                    case "BounceEase":
                        returnFunctionEase = new BounceEase();
                        ((BounceEase)returnFunctionEase).Bounces = 8;
                        ((BounceEase)returnFunctionEase).Bounciness = 2;
                        break;
                    case "BackEase":
                        returnFunctionEase = new BackEase();
                        ((BackEase)returnFunctionEase).Amplitude = 1;
                        break;
                    case "CircleEase":
                        returnFunctionEase = new CircleEase();
                        break;
                    case "CubicEase":
                        returnFunctionEase = new CubicEase();
                        break;
                    case "ExponentialEase":
                        returnFunctionEase = new ExponentialEase();
                        ((ExponentialEase)returnFunctionEase).Exponent = 5;
                        break;
                    case "PowerEase":
                        returnFunctionEase = new PowerEase();
                        ((PowerEase)returnFunctionEase).Power = 5;
                        break;
                    case "QuadraticEase":
                        returnFunctionEase = new QuadraticEase();
                        break;
                    case "QuarticEase":
                        returnFunctionEase = new QuarticEase();
                        break;
                    case "QuinticEase":
                        returnFunctionEase = new QuinticEase();
                        break;
                    case "SineEase":
                        returnFunctionEase = new SineEase();
                        break;
                }
            return returnFunctionEase;
        }
        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class DoubleToTimeSpanConvert : IValueConverter {
        #region IValueConvector implementation
        object IValueConverter.Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            double doubleValue = (double)value;
            return new TimeSpan(0, 0, (int)Math.Ceiling(doubleValue));
        }
        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class BoolToDefaultBooleanConvert : IValueConverter {
        #region IValueConvector implementation
        object IValueConverter.Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            bool booleanValue = (bool)value;
            return booleanValue ? DefaultBoolean.True : DefaultBoolean.False;
        }
        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class BoolToSymbolPresentationConverter : IValueConverter {
        static SolidColorBrush redBrush = new SolidColorBrush(Color.FromArgb(230, 255, 56, 56));
        static SolidColorBrush transparentRedBrush = new SolidColorBrush(Color.FromArgb(25, 255, 56, 56));
        static SolidColorBrush greenBrush = new SolidColorBrush(Color.FromArgb(230, 27, 255, 20));
        static SolidColorBrush transparentGreenBrush = new SolidColorBrush(Color.FromArgb(25, 27, 255, 20));
        static SolidColorBrush yellowBrush = new SolidColorBrush(Color.FromArgb(230, 238, 255, 20));
        static SolidColorBrush transparentYellowBrush = new SolidColorBrush(Color.FromArgb(25, 238, 255, 20));
        static SolidColorBrush transparentBrush = new SolidColorBrush(Colors.Transparent);

        static DefaultMatrix8x14Presentation redSegmentPresentation = new DefaultMatrix8x14Presentation() { FillActive = redBrush, FillInactive = transparentBrush };
        static DefaultMatrix8x14Presentation gangerRedSegmentPresentation = new DefaultMatrix8x14Presentation() { FillActive = redBrush, FillInactive = transparentBrush };
        static DefaultMatrix8x14Presentation yellowSegmentPresentation = new DefaultMatrix8x14Presentation() { FillActive = yellowBrush, FillInactive = transparentBrush };
        static DefaultMatrix8x14Presentation greenLeftSegmentPresentation = new DefaultMatrix8x14Presentation() { FillActive = greenBrush, FillInactive = transparentBrush };
        static DefaultMatrix8x14Presentation greenRightSegmentPresentation = new DefaultMatrix8x14Presentation() { FillActive = greenBrush, FillInactive = transparentBrush };
        static DefaultMatrix8x14Presentation gangerGreenSegmentPresentation = new DefaultMatrix8x14Presentation() { FillActive = transparentGreenBrush, FillInactive = transparentBrush };
        static DefaultFourteenSegmentsPresentation timerPresentation = new DefaultFourteenSegmentsPresentation() { FillActive = greenBrush, FillInactive = transparentBrush };

        #region IValueConvector implementation
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(targetType.BaseType == typeof(SymbolPresentation)) {
                string currentSegment = (string)parameter;
                bool isSegmentEnabled = (bool)value;

                switch(currentSegment) {
                    case "Red": {
                            if(isSegmentEnabled)
                                redSegmentPresentation.FillActive = redBrush;
                            else
                                redSegmentPresentation.FillActive = transparentRedBrush;
                            return redSegmentPresentation;
                        }
                    case "Yellow": {
                            if(isSegmentEnabled)
                                yellowSegmentPresentation.FillActive = yellowBrush;
                            else
                                yellowSegmentPresentation.FillActive = transparentYellowBrush;
                            return yellowSegmentPresentation;
                        }
                    case "GreenLeft": {
                            if(isSegmentEnabled)
                                greenLeftSegmentPresentation.FillActive = greenBrush;
                            else
                                greenLeftSegmentPresentation.FillActive = transparentGreenBrush;
                            return greenLeftSegmentPresentation;
                        }
                    case "GreenRight": {
                            if(isSegmentEnabled)
                                greenRightSegmentPresentation.FillActive = greenBrush;
                            else
                                greenRightSegmentPresentation.FillActive = transparentGreenBrush;
                            return greenRightSegmentPresentation;
                        }
                    case "GangerGreen": {
                            if(isSegmentEnabled)
                                gangerGreenSegmentPresentation.FillActive = greenBrush;
                            else
                                gangerGreenSegmentPresentation.FillActive = transparentGreenBrush;
                            return gangerGreenSegmentPresentation;
                        }
                    case "GangerRed": {
                            if(isSegmentEnabled)
                                gangerRedSegmentPresentation.FillActive = redBrush;
                            else
                                gangerRedSegmentPresentation.FillActive = transparentRedBrush;
                            return gangerRedSegmentPresentation;
                        }
                    default: {
                            if(isSegmentEnabled)
                                timerPresentation.FillActive = greenBrush;
                            else
                                timerPresentation.FillActive = redBrush;
                            return timerPresentation;
                        }
                }
            }
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class BoolToVisibilityConverter : IValueConverter {
        #region IValueConvector implementation
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(targetType == typeof(Visibility)) {
                bool isSegmentEnabled = (bool)value;
                if(isSegmentEnabled)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public static class Utils {
        public static string ConvertArabicToRoman(int arabic) {
            string roman = "";
            bool bigNumber = false;
            string[] numerationSystemRoman = new string[] { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };
            int[] numerationSystemArabic = new int[] { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };
            while(arabic > 0) {
                bigNumber = true;
                for(int i = 1; i < numerationSystemRoman.Length; i++)
                    if(arabic < numerationSystemArabic[i]) {
                        roman += numerationSystemRoman[i - 1];
                        arabic -= numerationSystemArabic[i - 1];
                        bigNumber = false;
                        break;
                    }
                if(bigNumber) {
                    roman += numerationSystemRoman[numerationSystemRoman.Length - 1];
                    arabic -= numerationSystemArabic[numerationSystemRoman.Length - 1];
                }
            }
            return roman;
        }
    }

    public class GaugeRandomDataGenerator : BindableBase {
        public double NeedleValue {
            get { return GetProperty(() => NeedleValue); }
            set { SetProperty(() => NeedleValue, value); }
        }
        public double RangeBarValue {
            get { return GetProperty(() => RangeBarValue); }
            set { SetProperty(() => RangeBarValue, value); }
        }
        public double MarkerValue {
            get { return GetProperty(() => MarkerValue); }
            set { SetProperty(() => MarkerValue, value); }
        }
        public double LevelBarValue {
            get { return GetProperty(() => LevelBarValue); }
            set { SetProperty(() => LevelBarValue, value); }
        }

        const double defaultUpdateInterval = 1000;

        readonly double minValue;
        readonly double maxValue;
        readonly Random random = new Random();
        readonly DispatcherTimer updateTimer;

        double ValuesRnage { get { return maxValue - minValue; } }

        public GaugeRandomDataGenerator(double minValue, double maxValue, double updateInterval) {
            this.minValue = minValue;
            this.maxValue = maxValue;
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromMilliseconds(updateInterval);
            this.updateTimer.Tick += OnTimerTick;
        }
        public GaugeRandomDataGenerator(double minValue, double maxValue)
            : this(minValue, maxValue, defaultUpdateInterval) {
        }
        void OnTimerTick(object sender, EventArgs e) {
            NeedleValue = minValue + ValuesRnage * random.NextDouble();
            RangeBarValue = minValue + ValuesRnage * random.NextDouble();
            MarkerValue = minValue + ValuesRnage * random.NextDouble();
            LevelBarValue = minValue + ValuesRnage * random.NextDouble();
        }
        public void Start() {
            updateTimer.Start();
        }
        public void Stop() {
            updateTimer.Stop();
        }
    }
}
