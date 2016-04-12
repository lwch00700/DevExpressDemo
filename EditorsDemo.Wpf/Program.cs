using System;
using System.Windows;
using DevExpress.DemoData;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.DemoData.Helpers;
using DevExpress.DemoData.Models;

namespace EditorsDemo {
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
    }
}
