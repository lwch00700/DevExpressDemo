using System;
using System.Linq;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.Core.Native;

namespace ChartsDemo.Tests {
    public class ChartsCheckAllDemosFixture : CheckAllDemosFixture {
        Type[] skipMemoryLeaksCheckModules = new Type[] { };

        protected override bool CheckMemoryLeaks(Type moduleTyle) {
            return !skipMemoryLeaksCheckModules.Contains(moduleTyle);
        }
    }
}
