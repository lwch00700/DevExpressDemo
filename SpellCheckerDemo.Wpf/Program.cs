using System;
using System.Runtime.InteropServices;
using System.Windows;
using DevExpress.DemoData;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.DemoData.Helpers;

namespace SpellCheckerDemo {
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
    public static class WinFormsMessageBoxHelper {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

        public static void Show(string message) {
            uint uType = 0x10000 | 0x10;
            WinFormsMessageBoxHelper.MessageBox(IntPtr.Zero, message, "", uType);
        }
    }
}
