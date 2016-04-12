using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Utils;
using System.Windows.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using DevExpress.Mvvm;

namespace GridDemo {
    public class ProcessInfoAnimationElement : FrameworkContentElement {
        public static readonly DependencyProperty CpuUsageProperty;
        public static readonly DependencyProperty MemoryUsageColorProperty;
        public static readonly DependencyProperty RevealProgressProperty;
        static ProcessInfoAnimationElement() {
            CpuUsageProperty = DependencyPropertyManager.Register("CpuUsage", typeof(double), typeof(ProcessInfoAnimationElement), new PropertyMetadata(0d));
            MemoryUsageColorProperty = DependencyPropertyManager.Register("MemoryUsageColor", typeof(Color), typeof(ProcessInfoAnimationElement), new PropertyMetadata(Color.FromArgb(0xFF, 0x92, 0x96, 0x9C)));
            RevealProgressProperty = DependencyPropertyManager.Register("RevealProgress", typeof(double), typeof(ProcessInfoAnimationElement), new PropertyMetadata(1d));
        }
        public double CpuUsage {
            get { return (double)GetValue(CpuUsageProperty); }
            set { SetValue(CpuUsageProperty, value); }
        }
        public Color MemoryUsageColor {
            get { return (Color)GetValue(MemoryUsageColorProperty); }
            set { SetValue(MemoryUsageColorProperty, value); }
        }
        public double RevealProgress {
            get { return (double)GetValue(RevealProgressProperty); }
            set { SetValue(RevealProgressProperty, value); }
        }
    }
    public class ProcessInfo : BindableBase {
        int pid;
        string nameCore;
        int memoryUsageCore, cpuUsageCore;
        bool isNew;
        CpuUsageHistory history = new CpuUsageHistory();
        public object History { get { return history; } }
        public ProcessInfo(int pid, string name, int memoryUsage, int cpuUsage, bool isNew) {
            this.pid = pid;
            this.nameCore = name;
            this.memoryUsageCore = memoryUsage;
            this.cpuUsageCore = cpuUsage;
            this.isNew = isNew;
        }
        internal bool IsDeleting { get; set; }
        public int PID {
            get { return pid; }
            set { SetProperty(ref pid, value, "PID"); }
        }
        public string Name {
            get { return nameCore; }
            set { SetProperty(ref nameCore, value, "Name"); }
        }
        public int MemoryUsage {
            get { return memoryUsageCore; }
            set { SetProperty(ref memoryUsageCore, value, "MemoryUsage"); }
        }
        public int CpuUsage {
            get { return cpuUsageCore; }
            set { SetProperty(ref cpuUsageCore, value, "CpuUsage"); }
        }
        [Browsable(false)]
        public ProcessGenerator Owner { get; set; }
    }
    public class ProcessInfoList : ObservableCollection<ProcessInfo> {
    }
    public class ProcessGenerator : BindableBase {
        public enum ProcessUpdateMode { AddRemoveUpdate, AddRemove, Update }
        enum UpdateType { Add, Remove, Change }

        const double DefaultUpdateInterval = 200d;
        const double DefaultUpdateDuration = 600d;
        const double DefaultUpdateHistoryInterval = 1000d;
        const int DefaultProcessMaxCount = 25, DefaultProcessMinCount = 10;
        readonly string[] ProcessNames = new string[] {"wininit.exe", "svchost.exe", "svchost.exe", "svchost.exe", "svchost.exe", "System", "devenv.exe",
                                                           "intetinfo.exe", "lsm.exe", "lsass.exe", "winlogon.exe", "services.exe"};

        double updateIntervalCore = DefaultUpdateInterval;
        double updateHistoryIntervalCore = DefaultUpdateHistoryInterval;
        int processMaxCountCore = DefaultProcessMaxCount;
        int processMinCountCore = DefaultProcessMinCount;
        ProcessUpdateMode updateModeCore = ProcessUpdateMode.AddRemoveUpdate;

        Dispatcher dispatcher;
        ProcessInfoList processes;
        DispatcherTimer updateTimer;
        int realProcessCount;
        ManagingLiveData module;
        Random random = new Random();
        Dictionary<int, ProcessInfoAnimationElement> animationElements = new Dictionary<int, ProcessInfoAnimationElement>();
        public ProcessGenerator(ManagingLiveData module) {
            dispatcher = Dispatcher.CurrentDispatcher;
            this.processes = new ProcessInfoList();
            this.updateTimer = new DispatcherTimer();
            this.updateTimer.Interval = TimeSpan.FromMilliseconds(DefaultUpdateInterval);
            this.updateTimer.Tick += OnTimerTick;
            this.updateHistoryTimer = new DispatcherTimer();
            this.updateHistoryTimer.Interval = TimeSpan.FromMilliseconds(DefaultUpdateHistoryInterval);
            this.updateHistoryTimer.Tick += OnHistoryTimerTick;
            this.module = module;
        }
        public ProcessInfoList Processes { get { return processes; } }
        protected internal DispatcherTimer UpdateTimer { get { return updateTimer; } }
        delegate void UpdateDelegate();
        void OnUpdateIntervalChanged() {
            this.UpdateTimer.Interval = TimeSpan.FromMilliseconds(UpdateInterval);
        }
        void OnUpdateHistoryIntervalChanged() {
            this.updateHistoryTimer.Interval = TimeSpan.FromMilliseconds(UpdateHistoryInterval);
        }
        void OnTimerTick(object sender, EventArgs e) {
            dispatcher.BeginInvoke(new Action(UpdateProcessList), DispatcherPriority.Background);
        }
        DispatcherTimer updateHistoryTimer;
        void OnHistoryTimerTick(object sender, EventArgs e) {
            foreach(ProcessInfo item in Processes) {
                ((CpuUsageHistory)item.History).AddHistoryValue(item.CpuUsage);
            }
        }
        protected void UpdateProcessList() {
            PerformRandomUpdate();
        }
        UpdateType GetUpdateType() {
            if(CanAddRemove) {
                if(CanUpdate) return (UpdateType)random.Next(3);
                return (UpdateType)random.Next(2);
            }
            return UpdateType.Change;
        }
        bool CanAddRemove { get { return UpdateMode == ProcessUpdateMode.AddRemoveUpdate || UpdateMode == ProcessUpdateMode.AddRemove; } }
        bool CanUpdate { get { return UpdateMode == ProcessUpdateMode.AddRemoveUpdate || UpdateMode == ProcessUpdateMode.Update; } }

        class ProcessToRemove {
            public ProcessInfo ProcToRemove { get; set; }
            public ProcessGenerator ProcGenerator { get; set; }

            public void OnStoryboardCompleted(object sender, EventArgs e) {
                ProcGenerator.RemoveProcess(ProcToRemove);
            }
        }
        protected void PerformRandomUpdate() {
            TimeSpan duration = TimeSpan.FromMilliseconds(600);
            ProcessInfo info;
            switch(GetUpdateType()) {
                case UpdateType.Add:
                    info = AddProcess(true);
                    if(info != null && module.newRowCheckBox.IsChecked.Value) {
                        Storyboard storyboard = GetStoryboard("newRowStoryboard");
                        BeginStoryboard(storyboard, info, ProcessInfoAnimationElement.RevealProgressProperty);
                    }
                    break;
                case UpdateType.Remove:
                    if(realProcessCount > 0 && realProcessCount > ProcessMinCount) {
                        info = GetRandomProcess();
                        realProcessCount--;
                        if(module.deleteRowCheckBox.IsChecked.Value) {
                            info.IsDeleting = true;
                            Storyboard storyboard = GetStoryboard("deleteRowStoryboard");
                            ProcessToRemove procToRemove = new ProcessToRemove() { ProcToRemove = info, ProcGenerator = this };
                            storyboard.Completed += new EventHandler(procToRemove.OnStoryboardCompleted);
                            BeginStoryboard(storyboard, info, ProcessInfoAnimationElement.RevealProgressProperty);
                        } else {
                            RemoveProcess(info);
                        }
                    }
                    break;
                case UpdateType.Change:
                    if(realProcessCount > 0) {
                        ModifyMemoryUsage();
                        ModifyCpuUsage();
                    }
                    break;
            }
        }
        void RemoveProcess(ProcessInfo info) {
            Processes.Remove(info);
            animationElements.Remove(info.PID);
        }
        Storyboard GetStoryboard(string resourceKey) {
            return StoryboardContainer.CreateStoryboard(module, resourceKey);
        }
        void BeginStoryboard(Storyboard storyboard, ProcessInfo info, DependencyProperty property) {
            Storyboard.SetTargetProperty(storyboard, new PropertyPath(property.GetName()));
            Storyboard.SetTarget(storyboard, GetAnimationElement(info));
            storyboard.Begin();
        }
        void ModifyMemoryUsage() {
            ProcessInfo info = GetRandomProcess();
            double oldMemoryUsageValue = info.MemoryUsage;
            info.MemoryUsage = random.Next(10000);

            if(module.memoryUsageCheckBox.IsChecked.Value) {
                Storyboard storyboard = GetStoryboard(oldMemoryUsageValue < info.MemoryUsage ? "memoryUsageIncreasedColorStoryboard" : "memoryUsageDecreasedColorStoryboard");
                BeginStoryboard(storyboard, info, ProcessInfoAnimationElement.MemoryUsageColorProperty);
            }
        }
        void ModifyCpuUsage() {
            ProcessInfo info = GetRandomProcess();
            info.CpuUsage = random.Next(60);
            Storyboard storyboard;
            if(module.cpuUsageCheckBox.IsChecked.Value) {
                storyboard = GetStoryboard("cpuUsageChangeStoryboard");
                DoubleAnimationUsingKeyFrames animation = (DoubleAnimationUsingKeyFrames)storyboard.Children[0];
                animation.KeyFrames[0].Value = GetAnimationElement(info).CpuUsage;
                animation.KeyFrames[1].Value = info.CpuUsage;
            } else {
                storyboard = new Storyboard();
                storyboard.Children.Add(new DoubleAnimation() { Duration = TimeSpan.Zero, To = info.CpuUsage });
            }
            BeginStoryboard(storyboard, info, ProcessInfoAnimationElement.CpuUsageProperty);
        }
        ProcessInfo GetRandomProcess() {
            while(true) {
                ProcessInfo info = Processes[random.Next(Processes.Count)];
                if(!info.IsDeleting)
                    return info;
            }
        }
        int counter;
        ProcessInfo AddProcess(bool isNew) {
            ProcessInfo info = null;
            if(realProcessCount < ProcessMaxCount) {
                string name = ProcessNames[random.Next(ProcessNames.Length)];
                counter++;
                info = new ProcessInfo(counter, name, random.Next(10000), random.Next(100), isNew);
                GetAnimationElement(info).CpuUsage = info.CpuUsage;
                Processes.Insert(random.Next(Processes.Count), info);
                realProcessCount++;
            }
            return info;
        }
        public void Initialize() {
            this.counter = 0;
            Processes.Clear();
            realProcessCount = 0;
            for(int i = 0; i < ProcessMaxCount; i++)
                AddProcess(false);
        }
        public void Start() {
            UpdateTimer.Start();
            updateHistoryTimer.Start();
        }
        public void Stop() {
            UpdateTimer.Stop();
            updateHistoryTimer.Stop();
        }
        public ProcessInfoAnimationElement GetAnimationElement(ProcessInfo info) {
            ProcessInfoAnimationElement result = null;
            if(!animationElements.TryGetValue(info.PID, out result)) {
                result = new ProcessInfoAnimationElement();
                animationElements.Add(info.PID, result);
            }
            return result;
        }
        public double UpdateInterval {
            get { return updateIntervalCore; }
            set { SetProperty(ref updateIntervalCore, value, "UpdateInterval", OnUpdateIntervalChanged); }
        }
        public double UpdateHistoryInterval {
            get { return updateHistoryIntervalCore; }
            set { SetProperty(ref updateHistoryIntervalCore, value, "UpdateHistoryInterval", OnUpdateHistoryIntervalChanged); }
        }
        public int ProcessMaxCount {
            get { return processMaxCountCore; }
            set { SetProperty(ref processMaxCountCore, value, "ProcessMaxCount"); }
        }
        public int ProcessMinCount {
            get { return processMinCountCore; }
            set { SetProperty(ref processMinCountCore, value, "ProcessMinCount"); }
        }
        public ProcessUpdateMode UpdateMode {
            get { return updateModeCore; }
            set { SetProperty(ref updateModeCore, value, "UpdateMode"); }
        }
    }
    public class LiveDataChartElement : FrameworkElement {
        const int BarWidth = 4;
        const int BarSpace = 1;
        const int SegmentHeight = 2;
        const int SegmentSpace = 1;
        const double MaxBarHeight = 19;
        public static readonly DependencyProperty CpuUsageHistoryProperty;
        public static readonly DependencyProperty BarBrushesProperty;
        static LiveDataChartElement() {
            CpuUsageHistoryProperty = DependencyProperty.Register("CpuUsageHistory", typeof(CpuUsageHistory), typeof(LiveDataChartElement), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnCpuUsageHistoryChanged)));
            BarBrushesProperty = DependencyProperty.Register("BarBrushes", typeof(IList), typeof(LiveDataChartElement), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        }
        static void OnCpuUsageHistoryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((LiveDataChartElement)d).OnCpuUsageHistoryChanged((CpuUsageHistory)e.OldValue);
        }
        public CpuUsageHistory CpuUsageHistory {
            get { return (CpuUsageHistory)GetValue(CpuUsageHistoryProperty); }
            set { SetValue(CpuUsageHistoryProperty, value); }
        }
        public IList BarBrushes {
            get { return (IList)GetValue(BarBrushesProperty); }
            set { SetValue(BarBrushesProperty, value); }
        }
        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);
            if(CpuUsageHistory == null || BarBrushes == null)
                return;
            int barCount = (int)Math.Floor((ActualWidth - BarWidth) / (BarWidth + BarSpace) + 1);
            int startHitoryIndex = Math.Max(0, CpuUsageHistory.Count - barCount);
            for(int i = startHitoryIndex; i < CpuUsageHistory.Count; i++) {
                int placeIndex = i - startHitoryIndex;
                double height = Math.Round(MaxBarHeight * (CpuUsageHistory[i] / 100d));
                int segmentCount = (int)Math.Floor((height - SegmentHeight) / (SegmentHeight + SegmentSpace) + 1);
                for(int j = 0; j < segmentCount; j++) {
                    Rect rect = new Rect(placeIndex * (BarWidth + BarSpace), ActualHeight - j * (SegmentHeight + SegmentSpace), BarWidth, SegmentHeight);
                    drawingContext.DrawRectangle((Brush)BarBrushes[j], null, rect);
                }
            }
        }
        void OnCpuUsageHistoryChanged(CpuUsageHistory oldValue) {
            if(oldValue != null)
                oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(OnCpuUsageHistoryCollectionChanged);
            if(CpuUsageHistory != null)
                CpuUsageHistory.CollectionChanged += new NotifyCollectionChangedEventHandler(OnCpuUsageHistoryCollectionChanged);
            InvalidateVisual();
        }

        void OnCpuUsageHistoryCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            InvalidateVisual();
        }
    }
    public class CpuUsageHistory : ObservableCollection<double> {
        public void AddHistoryValue(double value) {
            Add(value);
            if(Count > 100)
                RemoveAt(0);
        }
    }
}
