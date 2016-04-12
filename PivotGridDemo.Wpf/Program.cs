using System;
using System.Windows;
using DevExpress.DemoData;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.DemoData.Helpers;
using System.Globalization;
using System.Threading;
using DevExpress.DemoData.Models;

namespace PivotGridDemo {
    public class Program {
        [STAThread]
        static void Main(string[] args) {
            StartupBase.Run<Startup>(null);
        }
    }
    public class Startup : DemoStartup {
        public static void InitDemo() {
            NWindContext.Preload();
        }
        protected override bool GetDebug() {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
        protected override Type GetFixtureTypeForXBAPOrSLTesting() {
            return null;
        }

        protected override Application CreateApplication(Application app) {
            SetCultureInfo();
            return base.CreateApplication(app);
        }

        void SetCultureInfo() {
            CultureInfo demoCI = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            demoCI.NumberFormat.CurrencyDecimalDigits = 2;
            demoCI.NumberFormat.CurrencyDecimalSeparator = ".";
            demoCI.NumberFormat.CurrencyGroupSeparator = ",";
            demoCI.NumberFormat.CurrencyGroupSizes = new int[] { 3 };
            demoCI.NumberFormat.CurrencyNegativePattern = 0;
            demoCI.NumberFormat.CurrencyPositivePattern = 0;
            demoCI.NumberFormat.CurrencySymbol = "$";
            Thread.CurrentThread.CurrentCulture = demoCI;
        }
    }
}
