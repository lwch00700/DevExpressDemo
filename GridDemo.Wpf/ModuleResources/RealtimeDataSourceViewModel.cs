using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.DemoBase.Helpers;
using System.Threading;
using System.Windows.Threading;
using System.Collections;
using DevExpress.Mvvm;

namespace GridDemo {
    public class RealtimeDataSourceViewModel : BindableBase {
        readonly ChangeThread changeThread;
        readonly Thread thread;
        readonly DispatcherTimer timerShow;
        readonly DispatcherTimer backgroundTimer;

        public RealtimeDataSourceViewModel() {
            changeThread = new ChangeThread();
            ThreadStart chtrDo = (ThreadStart)Delegate.CreateDelegate(typeof(ThreadStart), changeThread, "Do");
            thread = new Thread(chtrDo);
            thread.IsBackground = true;
            changeThread.NeedStop = false;
            timerShow = new DispatcherTimer(TimeSpan.FromSeconds(0.5), DispatcherPriority.Send, TimerShowCallback, Dispatcher.CurrentDispatcher);
            EventHandler onIdle = (EventHandler)Delegate.CreateDelegate(typeof(EventHandler), typeof(ChangeThread), "OnIdle");
            backgroundTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(10), DispatcherPriority.Background, onIdle, Dispatcher.CurrentDispatcher);
            thread.Start();
        }

        public event EventHandler TimerShowTick;
        public int ChangePerSecond { get { return changeThread.ChangePerSecond; } }
        public int InterEventDelay { set { Interlocked.Exchange(ref changeThread.InterEventDelay, value); } }
        bool withRealTimeSource;
        public bool WithRealTimeSource {
            get { return withRealTimeSource; }
            set { SetProperty(ref withRealTimeSource, value, () => WithRealTimeSource); }
        }
        public IList List { get { return changeThread.List; } }
        public void Update() {
            WithRealTimeSource = !WithRealTimeSource;
            if(WithRealTimeSource) {
                ChangeThread.UserRealtimeSource = true;
            } else {
                ChangeThread.UserRealtimeSource = false;
            }
        }
        public void Dispose() {
            changeThread.Stop();
            timerShow.Stop();
            backgroundTimer.Stop();
        }
        void TimerShowCallback(object sender, EventArgs e) {
            RaiseTimerShowTickEvent();
        }
        void RaiseTimerShowTickEvent() {
            if(TimerShowTick != null)
                TimerShowTick(this, EventArgs.Empty);
        }
    }
}
