using DevExpress.Xpf.DemoBase.DemoTesting;
using System;
using DevExpress.Xpf.Bars;
using System.Reflection;
using DevExpress.Xpf.Core.Native;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace RibbonDemo.Tests {
    public class RibbonCheckAllDemosFixture : CheckAllDemosFixture {
    }
    public class RibbonDemoModulesAccessor : DemoModulesAccessor<RibbonDemoModule> {
        public RibbonDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
        public BarManager Manager { get { return DemoModule.Manager; } }
    }

    public abstract class BaseRibbonDemoTestingFixture : BaseDemoTestingFixture {
        readonly RibbonDemoModulesAccessor modulesAccessor;
        public BaseRibbonDemoTestingFixture() {
            modulesAccessor = new RibbonDemoModulesAccessor(this);
        }
        public BarManager Manager { get { return modulesAccessor.Manager; } }
    }

    public class CheckDemoOptionsFixture : BaseRibbonDemoTestingFixture {

    }
}
