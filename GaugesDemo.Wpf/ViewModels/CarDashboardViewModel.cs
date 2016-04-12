using DevExpress.Mvvm;
using System;
using System.Windows.Input;
using System.Windows.Threading;

namespace GaugesDemo {
    public class CarDashboardViewModel {
        readonly DispatcherTimer timer = new DispatcherTimer();
        readonly DispatcherTimer timerInitialAnimation = new DispatcherTimer();
        readonly DispatcherTimer timerUpdateDateTime = new DispatcherTimer();
        double GearInteval { get { return 0.8 * (MaxSpeed / GearCount); } }
        void OnTimedEventInitialAnimation(object source, EventArgs e) {
            timerInitialAnimation.Stop();
            Speed = 0;
            TachometerValue = 0;
            timer.Start();
        }
        void OnTimedEvent(object source, EventArgs e) {
            Update(timer.Interval.TotalSeconds);
        }
        void Update(double deltaTime) {
            UpdateSpeed(10 * deltaTime);
            Gear = (int)Math.Min(GearCount, Math.Ceiling((Speed / GearInteval)));
            TachometerValue = Gear > 0 ? TachometerMaxValue * (Speed - GearInteval * (Gear - 1)) / GearInteval : 0;
            TachometerValue = Math.Max(0, Math.Min(TachometerMaxValue, TachometerValue));
            FuelLevel -= TachometerValue / TachometerMaxValue / 1000;
            if(((TachometerMaxValue / 2) < TachometerValue) || (CurrentEngineTemperature < NormalEngineTemperature))
                CurrentEngineTemperature += TachometerValue / TachometerMaxValue / 2.5;
            else
                CurrentEngineTemperature -= (TachometerMaxValue - TachometerValue) / TachometerMaxValue / 2.5;
            CurrentEngineTemperature = Math.Min(MaxEngineTemperature, CurrentEngineTemperature);
        }
        void UpdateSpeed(double delta) {
            if(IsAcceleratePressed)
                Speed += delta;
            else
                if(IsBrakePressed)
                    Speed -= delta;
            Speed = Math.Max(0, Math.Min(MaxSpeed, Speed));
        }
        public void Start() {
            timer.Stop();
            Speed = MaxSpeed;
            TachometerValue = TachometerMaxValue;
            timerInitialAnimation.Start();
        }
        void updateTimerAndDate(object source, EventArgs e) {
            CurrentTime = DateTime.Now.ToShortTimeString();
            CurrentDate = DateTime.Now.ToShortDateString();
        }
        public CarDashboardViewModel() {
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timerInitialAnimation.Tick += new EventHandler(OnTimedEventInitialAnimation);
            timerInitialAnimation.Interval = TimeSpan.FromMilliseconds(800);
            CurrentTime = DateTime.Now.ToShortTimeString();
            CurrentDate = DateTime.Now.ToShortDateString();
            timerUpdateDateTime.Interval = new TimeSpan(0, 0, 1);
            timerUpdateDateTime.Tick += new EventHandler(updateTimerAndDate);
            timerUpdateDateTime.Start();
            IsAcceleratePressed = false;
            IsBrakePressed = false;
            MaxSpeed = 120.0;
            Speed = 0;
            NormalEngineTemperature = 85;
            MaxEngineTemperature = 130;
            CurrentEngineTemperature = 20;
            TachometerMaxValue = 8000;
            TachometerValue = 900;
            GearCount = 6;
            Gear = 0;
            FuelLevel = 0.75;
            CurrentTime = "";
            CurrentDate = "";
        }
        public virtual bool IsAcceleratePressed { get; set; }
        public virtual bool IsBrakePressed { get; set; }
        public virtual double MaxSpeed { get; set; }
        public virtual double Speed { get; set; }
        public virtual double NormalEngineTemperature { get; set; }
        public virtual double MaxEngineTemperature { get; set; }
        public virtual double CurrentEngineTemperature { get; set; }
        public virtual double TachometerMaxValue { get; set; }
        public virtual double TachometerValue { get; set; }
        public virtual int GearCount { get; set; }
        public virtual int Gear { get; set; }
        public virtual double FuelLevel { get; set; }
        public virtual string CurrentTime { get; set; }
        public virtual string CurrentDate { get; set; }
    }
}
