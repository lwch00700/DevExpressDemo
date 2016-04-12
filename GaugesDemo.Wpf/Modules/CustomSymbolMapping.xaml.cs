using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Gauges;
using DevExpress.Mvvm;

namespace GaugesDemo {
    public partial class CustomSymbolMapping : GaugesDemoModule {
        public CustomSymbolMapping() {
            TrafficLightDataGenerator dataGenerator = new TrafficLightDataGenerator();
            InitializeComponent();
            trafficLightsGrid.DataContext = dataGenerator;
        }
    }
    public enum TrafficLightStates {
        RedLightEnabled,
        GreenRightLightBlinking,
        YellowRedLightEnabled,
        GreenLeftLightEnabeld,
        GreenLeftLightBlinking,
        YellowLightEnabled
    };

    public class TrafficLightDataGenerator : BindableBase {
        public string ExpectationTime {
            get { return GetProperty(() => ExpectationTime); }
            set { SetProperty(() => ExpectationTime, value); }
        }
        public bool IsTimerGreen {
            get { return GetProperty(() => IsTimerGreen); }
            set { SetProperty(() => IsTimerGreen, value); }
        }
        public bool IsRedSegmentEnabled {
            get { return GetProperty(() => IsRedSegmentEnabled); }
            set { SetProperty(() => IsRedSegmentEnabled, value); }
        }
        public bool IsYellowSegmentEnabled {
            get { return GetProperty(() => IsYellowSegmentEnabled); }
            set { SetProperty(() => IsYellowSegmentEnabled, value); }
        }
        public bool IsGreenLeftSegmentEnabled {
            get { return GetProperty(() => IsGreenLeftSegmentEnabled); }
            set { SetProperty(() => IsGreenLeftSegmentEnabled, value); }
        }
        public bool IsGreenRightSegmentEnabled {
            get { return GetProperty(() => IsGreenRightSegmentEnabled); }
            set { SetProperty(() => IsGreenRightSegmentEnabled, value); }
        }
        public bool IsGangerRedSegmentEnabled {
            get { return GetProperty(() => IsGangerRedSegmentEnabled); }
            set { SetProperty(() => IsGangerRedSegmentEnabled, value); }
        }
        public bool IsGangerGreenSegmentEnabled {
            get { return GetProperty(() => IsGangerGreenSegmentEnabled); }
            set { SetProperty(() => IsGangerGreenSegmentEnabled, value); }
        }

        const int gangerGreenLightCycles = 2;
        const int gangerRedLightCycles = 4;
        const int changeTicksCount = 3;
        const int startCycles = 3;

        static bool[] redSegmentStates = { true, true, true, false, false, false };
        static bool[] yellowSegmentStates = { false, false, true, false, false, true };
        static bool[] greenLeftSegmentStates = { false, false, false, true, true, false };
        static bool[] greenRightSegmentStates = { true, true, false, false, false, false };
        static bool[] greenLeftBlinkingStates = { false, false, false, false, true, false };
        static bool[] greenRightBlinkingStates = { false, true, false, false, false, false };
        static bool[] gangerGreenSegmentStates = { false, false, false, true, true, false };
        static bool[] gangerRedSegmentStates = { true, true, true, false, false, true };
        static bool[] gangerGreenBlinkingStates = { false, false, false, false, true, false };
        static bool[] blinkingStates = { false, true, false, false, true, false };


        DispatcherTimer blinkingTimer = new DispatcherTimer();
        DispatcherTimer timer = new DispatcherTimer();
        TrafficLightStates currentState = TrafficLightStates.RedLightEnabled;
        int expectationTicksCount;
        int currentChangeTicksCount;
        bool isGreenLeftLightBlinking = false;
        bool isGreenRightLightBlinking = false;
        bool isGangerGreenLightBlinking = false;

        public TrafficLightDataGenerator() {
            ExpectationTime = "";
            IsTimerGreen = false;
            IsRedSegmentEnabled = true;
            IsYellowSegmentEnabled = false;
            IsGreenLeftSegmentEnabled = false;
            IsGreenRightSegmentEnabled = true;
            IsGangerRedSegmentEnabled = true;
            IsGangerGreenSegmentEnabled = false;

            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = new TimeSpan(0, 0, 1);
            expectationTicksCount = startCycles * changeTicksCount;
            blinkingTimer.Tick += new EventHandler(OnBlinkingTimedEvent);
            blinkingTimer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            currentChangeTicksCount = changeTicksCount;
            timer.Start();
        }
        void ChangeTrafficLite() {
            if(currentState == TrafficLightStates.GreenLeftLightBlinking)
                expectationTicksCount = gangerRedLightCycles * changeTicksCount;
            if(currentState == TrafficLightStates.YellowRedLightEnabled)
                expectationTicksCount = gangerGreenLightCycles * changeTicksCount;
            if(currentState == TrafficLightStates.YellowLightEnabled)
                currentState = TrafficLightStates.RedLightEnabled;
            else
                currentState = (TrafficLightStates)((int)currentState + 1);
            IsGreenLeftSegmentEnabled = greenLeftSegmentStates[(int)currentState];
            IsGreenRightSegmentEnabled = greenRightSegmentStates[(int)currentState];
            IsYellowSegmentEnabled = yellowSegmentStates[(int)currentState];
            IsRedSegmentEnabled = redSegmentStates[(int)currentState];
            IsGangerGreenSegmentEnabled = gangerGreenSegmentStates[(int)currentState];
            IsGangerRedSegmentEnabled = gangerRedSegmentStates[(int)currentState];
            IsTimerGreen = !IsGangerRedSegmentEnabled;
            isGreenLeftLightBlinking = greenLeftBlinkingStates[(int)currentState];
            isGreenRightLightBlinking = greenRightBlinkingStates[(int)currentState];
            isGangerGreenLightBlinking = gangerGreenBlinkingStates[(int)currentState];
            if(blinkingStates[(int)currentState])
                blinkingTimer.Start();
            else
                blinkingTimer.Stop();
        }
        void ChangeExpectationTime() {
            expectationTicksCount--;
            ExpectationTime = expectationTicksCount.ToString();
        }
        void ChangeBlinkingState() {
            if(isGangerGreenLightBlinking)
                IsGangerGreenSegmentEnabled = !IsGangerGreenSegmentEnabled;
            if(isGreenLeftLightBlinking)
                IsGreenLeftSegmentEnabled = !IsGreenLeftSegmentEnabled;
            if(isGreenRightLightBlinking)
                IsGreenRightSegmentEnabled = !IsGreenRightSegmentEnabled;
        }
        void OnTimedEvent(object source, EventArgs e) {
            if(currentChangeTicksCount == 0) {
                currentChangeTicksCount = changeTicksCount;
                ChangeTrafficLite();
            }
            currentChangeTicksCount--;
            ChangeExpectationTime();
        }
        void OnBlinkingTimedEvent(object source, EventArgs e) {
            ChangeBlinkingState();
        }
    }

    public class SegmentsStatesProvider {
        StatesMaskConverter converter = new StatesMaskConverter();

        public StatesMask RoundSegmentsMappingMask {
            get {
                return (StatesMask)converter.ConvertFromString(@"0 0 0 0 1 1 1 1 1 0 0 0 0
                                                                                                            0 0 1 1 1 1 1 1 1 1 1 0 0
                                                                                                            0 1 1 1 1 1 1 1 1 1 1 1 0
                                                                                                            0 1 1 1 1 1 1 1 1 1 1 1 0
                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 1
                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 1
                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 1
                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 1
                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 1
                                                                                                            0 1 1 1 1 1 1 1 1 1 1 1 0
                                                                                                            0 1 1 1 1 1 1 1 1 1 1 1 0
                                                                                                            0 0 1 1 1 1 1 1 1 1 1 0 0
                                                                                                            0 0 0 0 1 1 1 1 1 0 0 0 0");
            }
        }
        public StatesMask ArrowSegmentsMappingMask {
            get {
                return (StatesMask)converter.ConvertFromString(@"0 0 0 0 0 0 0 0 0 0 0 0 0
                                                                                                            0 0 0 0 0 0 0 1 0 0 0 0 0
                                                                                                            0 0 0 0 0 0 0 1 1 0 0 0 0
                                                                                                            0 0 0 0 0 0 0 1 1 1 0 0 0
                                                                                                            1 1 1 1 1 1 1 1 1 1 1 0 0
                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 0
                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 1
                                                                                                            1 1 1 1 1 1 1 1 1 1 1 1 0
                                                                                                            1 1 1 1 1 1 1 1 1 1 1 0 0
                                                                                                            0 0 0 0 0 0 0 1 1 1 0 0 0
                                                                                                            0 0 0 0 0 0 0 1 1 0 0 0 0
                                                                                                            0 0 0 0 0 0 0 1 0 0 0 0 0
                                                                                                            0 0 0 0 0 0 0 0 0 0 0 0 0");
            }
        }
    }
}
