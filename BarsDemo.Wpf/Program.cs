using System;
using DevExpress.Xpf.DemoBase;
using DevExpress.DemoData;
using System.Windows;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.DemoData.Helpers;

namespace BarsDemo {
    public class Program {
        [STAThread]
        static void Main(string[] args) {
            StartupBase.Run<Startup>(null);
        }
    }
    public class Startup : DemoStartup {
        public static void InitDemo() {
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
    }
}
