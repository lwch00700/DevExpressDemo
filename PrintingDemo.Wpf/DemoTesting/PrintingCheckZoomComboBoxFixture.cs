using System;
using System.Windows;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.DemoBase.Helpers.TextColorizer;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.DemoBase;

namespace PrintingDemo.Tests {
    public class PrintingCheckZoomComboBoxFixture : CheckAllDemosFixture {

        protected override bool AllowSwitchToTheTheme(Type moduleType, Theme theme) {
            return false;
        }

        protected override bool AllowCheckCodeFile(Type moduleType, CodeLanguage fileLanguage) {
            return false;
        }

        bool IsDocumentViewerControl(FrameworkElement element) {
            return element is DocumentPreviewControl;
        }
    }
}
