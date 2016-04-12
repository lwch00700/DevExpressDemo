using System;
using System.Windows.Threading;
using DevExpress.Mvvm;

namespace GaugesDemo {
    public partial class LinearScales : GaugesDemoModule {
        public LinearScales() {
            InitializeComponent();
            EqualizerDataGenerator dataGenerator = new EqualizerDataGenerator();
            gauge.DataContext = dataGenerator;
        }
    }

    public class EqualizerDataGenerator : BindableBase {
        public double Frequency32 {
            get { return GetProperty(() => Frequency32); }
            set { SetProperty(() => Frequency32, value); }
        }
        public double Frequency64 {
            get { return GetProperty(() => Frequency64); }
            set { SetProperty(() => Frequency64, value); }
        }
        public double Frequency125 {
            get { return GetProperty(() => Frequency125); }
            set { SetProperty(() => Frequency125, value); }
        }
        public double Frequency250 {
            get { return GetProperty(() => Frequency250); }
            set { SetProperty(() => Frequency250, value); }
        }
        public double Frequency500 {
            get { return GetProperty(() => Frequency500); }
            set { SetProperty(() => Frequency500, value); }
        }
        public double Frequency1K {
            get { return GetProperty(() => Frequency1K); }
            set { SetProperty(() => Frequency1K, value); }
        }
        public double Frequency2K {
            get { return GetProperty(() => Frequency2K); }
            set { SetProperty(() => Frequency2K, value); }
        }
        public double Frequency4K {
            get { return GetProperty(() => Frequency4K); }
            set { SetProperty(() => Frequency4K, value); }
        }
        public double Frequency8K {
            get { return GetProperty(() => Frequency8K); }
            set { SetProperty(() => Frequency8K, value); }
        }
        public double Frequency16K {
            get { return GetProperty(() => Frequency16K); }
            set { SetProperty(() => Frequency16K, value); }
        }

        const int MaxValue = 100;
        const int UpdateIneterval = 500;

        readonly Random random = new Random();
        readonly DispatcherTimer timer = new DispatcherTimer();

        public EqualizerDataGenerator() {
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = new TimeSpan(0, 0, 0, 0, UpdateIneterval);
            timer.Start();
        }
        void OnTimedEvent(object source, EventArgs e) {
            Frequency32 = random.Next(MaxValue);
            Frequency64 = random.Next(MaxValue);
            Frequency125 = random.Next(MaxValue);
            Frequency250 = random.Next(MaxValue);
            Frequency500 = random.Next(MaxValue);
            Frequency1K = random.Next(MaxValue);
            Frequency2K = random.Next(MaxValue);
            Frequency4K = random.Next(MaxValue);
            Frequency8K = random.Next(MaxValue);
            Frequency16K = random.Next(MaxValue);
        }
    }
}
