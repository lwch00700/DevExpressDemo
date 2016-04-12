using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.SpellChecker;

namespace SpellCheckerDemo.Tests {
    public class SpellCheckerDemoModuleAccessor : DemoModulesAccessor<SpellCheckerDemoModule> {
        public SpellCheckerDemoModuleAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
        public SpellChecker SpellChecker { get { return DemoModule.SpellChecker; } }
    }
    public abstract class BaseSpellCheckerTestingFixture : BaseDemoTestingFixture {
        readonly SpellCheckerDemoModuleAccessor modulesAccessor;
        public BaseSpellCheckerTestingFixture() {
            this.modulesAccessor = new SpellCheckerDemoModuleAccessor(this);
        }
        public SpellChecker SpellChecker { get { return this.modulesAccessor.SpellChecker; } }
    }
    public class SpellCheckerCheckAllDemosFixture : CheckAllDemosFixture {
    }
}
