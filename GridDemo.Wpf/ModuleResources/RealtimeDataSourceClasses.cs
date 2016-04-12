using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using DevExpress.Mvvm;

namespace GridDemo {
    public class ChangeThread {
        readonly ObservableCollection<MarketData> collection = new ObservableCollection<MarketData>();
        public IList List { get { return this.collection; } }
        public int InterEventDelay = 1024000;
        public bool NeedStop;
        readonly Stopwatch sw;
        readonly static Stopwatch backgroundSw = new Stopwatch();
        readonly static object syncObject = new object();
        public static bool UserRealtimeSource = true;
        static int backgroungTimerElapsed { get { lock(syncObject) return (int)backgroundSw.ElapsedMilliseconds; } }
        int changesFromPriopChangePerSecond = 0;
        int priorChanges;
        public int ChangePerSecond {
            get {
                if(Interlocked.CompareExchange(ref changesFromPriopChangePerSecond, 0, 0) == 0) {
                    return priorChanges;
                } else {
                    int changes = Interlocked.Exchange(ref changesFromPriopChangePerSecond, 0);
                    TimeSpan changeReportInterval = sw.Elapsed;
                    priorChanges = Convert.ToInt32(changes / changeReportInterval.TotalSeconds);
                    sw.Restart();
                    return priorChanges;
                }
            }
        }
        public ChangeThread() {
            this.NeedStop = false;
            this.sw = new Stopwatch();
            sw.Start();
            collection.Add(new MarketData("ANR"));
            collection.Add(new MarketData("FE"));
            collection.Add(new MarketData("GT"));
            collection.Add(new MarketData("PRGO"));
            collection.Add(new MarketData("APD"));
            collection.Add(new MarketData("PPL"));
            collection.Add(new MarketData("AES"));
            collection.Add(new MarketData("AVB"));
            collection.Add(new MarketData("IBM"));
            collection.Add(new MarketData("GAS"));
            collection.Add(new MarketData("EFX"));
            collection.Add(new MarketData("GPC"));
            collection.Add(new MarketData("ICE"));
            collection.Add(new MarketData("IVZ"));
            collection.Add(new MarketData("KO"));
            collection.Add(new MarketData("CCE"));
            collection.Add(new MarketData("SO"));
            collection.Add(new MarketData("STI"));
            collection.Add(new MarketData("BWA"));
            collection.Add(new MarketData("HRL"));
            collection.Add(new MarketData("WFM"));
            collection.Add(new MarketData("LM"));
            collection.Add(new MarketData("TROW"));
            collection.Add(new MarketData("K"));
            collection.Add(new MarketData("EXPE"));
            collection.Add(new MarketData("PCAR"));
            collection.Add(new MarketData("TRIP"));
            collection.Add(new MarketData("WHR"));
            collection.Add(new MarketData("WMT"));
            collection.Add(new MarketData("NU"));
            collection.Add(new MarketData("HST"));
            collection.Add(new MarketData("CVH"));
            collection.Add(new MarketData("LMT"));
            collection.Add(new MarketData("MAR"));
            collection.Add(new MarketData("CVC"));
            collection.Add(new MarketData("RF"));
            collection.Add(new MarketData("VMC"));
            collection.Add(new MarketData("PHM"));
            collection.Add(new MarketData("MU"));
            collection.Add(new MarketData("IRM"));
            collection.Add(new MarketData("AMT"));
            collection.Add(new MarketData("BXP"));
            collection.Add(new MarketData("STT"));
            collection.Add(new MarketData("PBCT"));
            collection.Add(new MarketData("FISV"));
            collection.Add(new MarketData("BLL"));
            collection.Add(new MarketData("MTB"));
            collection.Add(new MarketData("DIS"));
            collection.Add(new MarketData("LH"));
            collection.Add(new MarketData("AKAM"));
            collection.Add(new MarketData("CPB"));
            collection.Add(new MarketData("MYL"));
            collection.Add(new MarketData("LIFE"));
            collection.Add(new MarketData("LEG"));
            collection.Add(new MarketData("SCG"));
            collection.Add(new MarketData("CNX"));
            collection.Add(new MarketData("COL"));
            collection.Add(new MarketData("MCHP"));
            collection.Add(new MarketData("GR"));
            collection.Add(new MarketData("DUK"));
            collection.Add(new MarketData("BAC"));
            collection.Add(new MarketData("NUE"));
            collection.Add(new MarketData("UNM"));
            collection.Add(new MarketData("DLTR"));
            collection.Add(new MarketData("ABC"));
            collection.Add(new MarketData("TEG"));
            collection.Add(new MarketData("RRD"));
            collection.Add(new MarketData("EQR"));
            collection.Add(new MarketData("EXC"));
            collection.Add(new MarketData("BA"));
            collection.Add(new MarketData("CME"));
            collection.Add(new MarketData("NTRS"));
            collection.Add(new MarketData("VTR"));
            collection.Add(new MarketData("FITB"));
            collection.Add(new MarketData("PG"));
            collection.Add(new MarketData("KR"));
            collection.Add(new MarketData("M"));
            collection.Add(new MarketData("SNI"));
            collection.Add(new MarketData("ETN"));
            collection.Add(new MarketData("CLF"));
            collection.Add(new MarketData("PH"));
            collection.Add(new MarketData("KEY"));
            collection.Add(new MarketData("SHW"));
            collection.Add(new MarketData("HD"));
            collection.Add(new MarketData("AFL"));
            collection.Add(new MarketData("TSS"));
            collection.Add(new MarketData("CMI"));
            collection.Add(new MarketData("HBAN"));
            collection.Add(new MarketData("AEP"));
            collection.Add(new MarketData("BIG"));
            collection.Add(new MarketData("LTD"));
            collection.Add(new MarketData("ESRX"));
            collection.Add(new MarketData("GLW"));
            collection.Add(new MarketData("WPI"));
            collection.Add(new MarketData("MON"));
            collection.Add(new MarketData("AAPL"));
            collection.Add(new MarketData("DF"));
            collection.Add(new MarketData("T"));
            collection.Add(new MarketData("CMA"));
            collection.Add(new MarketData("THC"));
            collection.Add(new MarketData("LUV"));
            collection.Add(new MarketData("TXN"));
            collection.Add(new MarketData("TIE"));
            collection.Add(new MarketData("PX"));
        }
        public void Do() {
            Random rndRow = new Random();
            do {
                Stopwatch watch = Stopwatch.StartNew();
                int row = rndRow.Next(0, collection.Count);
                collection[row].Update();
                Interlocked.Increment(ref changesFromPriopChangePerSecond);
                for(; ; ) {
                    var elapsed = watch.ElapsedTicks;
                    var left = InterEventDelay - elapsed;
                    if(left <= 0) {
                        while(!UserRealtimeSource && backgroungTimerElapsed > 12)
                            Thread.Sleep(2);
                        break;
                    }
                    if(left > 2000)
                        Thread.Sleep(1);
                    else
                        Thread.Sleep(0);
                }
                watch.Stop();
            } while(!NeedStop);
        }
        public void Stop() {
            this.NeedStop = true;
        }
        public static void OnIdle(object sender, EventArgs e) {
            backgroundSw.Restart();
        }
    }
    public class MarketData : BindableBase {
        readonly static Random rnd = new Random();
        const double Max = 950;
        const double Min = 350;
        string tickerCore;
        double lastCore;
        double chgPercentCore;
        double openCore;
        double highCore;
        double lowCore;
        DateTime dateCore;
        double dayValCore;

        public MarketData(string name) {
            tickerCore = name;
            openCore = NextRnd() * (Max - Min) + Min;
            dayValCore = Open;
            UpdateInternal(Open);
        }
        public string Ticker { get { return tickerCore; } }
        public double Last { get { return lastCore; } }
        public double ChgPercent { get { return chgPercentCore; } }
        public double Open { get { return openCore; } }
        public double High { get { return highCore; } }
        public double Low { get { return lowCore; } }
        public DateTime Date { get { return dateCore; } }
        public double DayVal { get { return dayValCore; } }

        public void Update() {
            double value = DayVal - (Max - Min) * 0.05 + NextRnd() * (Max - Min) * 0.1;
            if(value <= Min) value = Min;
            if(value >= Max) value = Max;
            UpdateInternal(value);
        }
        void UpdateInternal(double value) {
            lastCore = DayVal;
            dayValCore = value;
            chgPercentCore = (DayVal - Last) * 100.0 / DayVal;
            highCore = Math.Max(Open, Math.Max(DayVal, Last));
            lowCore = Math.Min(Open, Math.Min(DayVal, Last));
            dateCore = DateTime.Now;
            RaisePropertyChanged("");
        }
        static double NextRnd() {
            double value = 0;
            for(int i = 0; i < 5; i++)
                value += rnd.NextDouble();
            return value / 5;
        }
    }
}
