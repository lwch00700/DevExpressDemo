using System;
using DevExpress.Xpf.DemoBase.DemoTesting;

namespace DevExpress.Xpf.LayoutControlDemo.Tests {
    public class LayoutControlCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return true;
        }
    }
}
