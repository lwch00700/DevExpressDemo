using DevExpress.Xpf.DemoBase.DemoTesting;
using System.Threading;
using System.Windows.Threading;
using System;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using System.Windows;
using System.Globalization;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using System.Linq;

namespace PdfViewerDemo.Tests {
    public class PdfViewerCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool AllowSwitchToTheTheme(Type moduleType, Theme theme) {
            return moduleType != typeof(MainDemoModule) || theme != Theme.HybridApp;
        }
    }
    public class PdfViewerDemoModulesAccessor : DemoModulesAccessor<PdfViewerDemoModule> {
        public PdfViewerDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BasePdfViewerDemoTestingFixture : BaseDemoTestingFixture {
        protected PdfViewerDemoModulesAccessor ModuleAccessor { get; private set; }
        public BasePdfViewerDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract PdfViewerDemoModulesAccessor GetModulesAccessor();
    }
}
