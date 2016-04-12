using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace ControlsDemo {
    public partial class Moon : UserControl, INotifyPropertyChanged {
        public static readonly DependencyProperty DateProperty = DependencyProperty.Register("Date", typeof(DateTime), typeof(Moon), new PropertyMetadata(DateTime.Now, new PropertyChangedCallback(OnDateChanged)));
        public DateTime Date { get { return (DateTime)GetValue(DateProperty); } set { SetValue(DateProperty, value); } }
        public double Phase { get { return Calculator.Phase; } }
        public string PhaseName { get { return Calculator != null ? Calculator.PhaseName : ""; } }
        MoonPhaseType PhaseType { get { return Calculator.PhaseType; } }
        MoonPhaseCalculator Calculator { get; set; }

        public Moon() {
            InitializeComponent();
            OnDateChangedCore();
            SizeChanged += OnSizeChanged;
        }

        protected static void OnDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((Moon)d).OnDateChangedCore();
        }
        protected virtual void OnDateChangedCore() {
            Calculator = new MoonPhaseCalculator(Date);
            UpdatePhase();
            RaisePropertyChangedEvent();
        }
        protected virtual void OnSizeChanged(object sender, SizeChangedEventArgs e) {
            UpdatePhase();
        }
        protected virtual void UpdatePhase() {
            if(ActualWidth == 0 || ActualHeight == 0)
                return;
            Point upPoint = new Point(ActualWidth / 2, 2);
            Point downPoint = new Point(ActualWidth / 2, ActualHeight - 2);
            Figure.StartPoint = upPoint;
            UpBottomArc.Point = downPoint;
            BottomUpArc.Point = upPoint;
            double xRadius = ActualWidth / 2 - 5;
            double yRadius = ActualHeight / 2 - 5;
            double k = Math.Abs(2 * Math.Abs(Phase) - 1);
            double upBottomK = PhaseType < MoonPhaseType.WaningGibbous ? k : 1;
            double bottomUpK = PhaseType > MoonPhaseType.Full ? k : 1;
            UpBottomArc.Size = new Size(xRadius * upBottomK, yRadius);
            BottomUpArc.Size = new Size(xRadius * bottomUpK, yRadius);
            UpBottomArc.SweepDirection = GetDirection((PhaseType == MoonPhaseType.WaxingGibbous || PhaseType == MoonPhaseType.Full));
            BottomUpArc.SweepDirection = GetDirection(PhaseType == MoonPhaseType.WaningGibbous);
        }
        protected static Visibility GetVisibility(bool isVisible) { return isVisible ? Visibility.Visible : Visibility.Collapsed; }
        SweepDirection GetDirection(bool isCounter) { return isCounter ? SweepDirection.Counterclockwise : SweepDirection.Clockwise; }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent() {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("PhaseName"));
        }
        #endregion
    }

    public enum MoonPhaseType { New, WaxingCrescent, FirstQuarter, WaxingGibbous, Full, WaningGibbous, LastQuarter, WaningCrescent };

    public class MoonPhaseCalculator {
        double BeginPhase { get; set; }
        double EndPhase { get; set; }
        public MoonPhaseType PhaseType { get; set; }
        public string PhaseName { get { return PhaseToName(PhaseType); } }
        public double Phase {
            get {
                switch(PhaseType) {
                    case MoonPhaseType.New:
                        return 0.0;
                    case MoonPhaseType.Full:
                        return 1.0;
                    case MoonPhaseType.FirstQuarter:
                        return 0.5;
                    case MoonPhaseType.LastQuarter:
                        return -0.5;
                    default:
                        return (BeginPhase + EndPhase) / 2;
                }
            }
        }
        public MoonPhaseCalculator(DateTime date) {
            DateTime beginDateTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime endDateTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            BeginPhase = MoonCalculator.GetPhase(beginDateTime);
            EndPhase = MoonCalculator.GetPhase(endDateTime);
            PhaseType = CalcPhaseType();
        }
        MoonPhaseType CalcPhaseType() {
            if(BeginPhase <= 0.0 && EndPhase >= 0.0)
                return MoonPhaseType.New;
            if(BeginPhase >= 0.0 && EndPhase <= 0.0)
                return MoonPhaseType.Full;
            if(BeginPhase <= 0.5 && EndPhase >= 0.5)
                return MoonPhaseType.FirstQuarter;
            if(BeginPhase <= -0.5 && EndPhase >= -0.5)
                return MoonPhaseType.LastQuarter;
            if(BeginPhase > 0.0 && EndPhase < 0.5)
                return MoonPhaseType.WaxingCrescent;
            if(BeginPhase > 0.5 && EndPhase < 1.0)
                return MoonPhaseType.WaxingGibbous;
            if(BeginPhase > -1.0 && EndPhase < -0.5)
                return MoonPhaseType.WaningGibbous;
            return MoonPhaseType.WaningCrescent;
        }
        string PhaseToName(MoonPhaseType phase) {
            string source = phase.ToString();
            char[] arr = source.ToCharArray();
            string result = arr[0].ToString();
            for(int i = 1; i < source.Length; i++) {
                char c = arr[i];
                if(Char.IsUpper(c))
                    result += " ";
                result += c.ToString();
            }
            return result;
        }
    }
}
