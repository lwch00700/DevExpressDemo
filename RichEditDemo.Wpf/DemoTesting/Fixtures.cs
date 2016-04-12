using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.XtraRichEdit;
using DevExpress.Xpf.RichEdit;

namespace RichEditDemo.Tests {
    public class RichEditDemoModuleAccessor : DemoModulesAccessor<RichEditDemoModule> {
        public RichEditDemoModuleAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
        public RichEditControl RichEditControl { get { return DemoModule.RichEditControl; } }
    }
    public abstract class BaseRichEditTestingFixture : BaseDemoTestingFixture {
        readonly RichEditDemoModuleAccessor modulesAccessor;
        public BaseRichEditTestingFixture() {
            this.modulesAccessor = new RichEditDemoModuleAccessor(this);
        }
        public RichEditControl RichEditControl { get { return this.modulesAccessor.RichEditControl; } }
    }
    public class FakeModuleFixture : BaseRichEditTestingFixture {
        protected override void CreateActions() {
            AddLoadModuleActions(typeof(CharacterFormatting));
            CharacterFormatting dayViewModule = (CharacterFormatting)DemoBaseTesting.CurrentDemoModule;
            RichEditViewType viewType = DispatchExpr(() => dayViewModule.richEdit.ActiveViewType);
            Assert.AreEqual(RichEditViewType.PrintLayout, viewType);
        }
    }
    public class RichEditCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool CanRunModule(Type moduleType) {
            return moduleType != typeof(MasterDetailMailMerge);
        }
    }
}
