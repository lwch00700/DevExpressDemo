using System;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.Printing;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Printing.PreviewControl.Native;

namespace PrintingDemo.Tests {
    static class DocumentBuildCompletedConditionFactory {
        class ConditionAdapter {
            BaseDemoTestingFixture fixture;

            public ConditionAdapter(BaseDemoTestingFixture fixture) {
                if(fixture == null)
                    throw new ArgumentNullException("fixture");

                this.fixture = fixture;
            }

            public bool EvaluateCondition() {
                if(fixture.DemoBaseTesting.CurrentDemoModule == null)
                    return true;
                DocumentPreviewControl viewer = BaseTestingFixture.HelperActions.FindElementByType<DocumentPreviewControl>(fixture.DemoBaseTesting.CurrentDemoModule);
                IDocumentViewModel model = viewer.Document;
                bool documentBuilt = model == null ? false : !model.IsCreating && model.Pages.Count > 0;

                return documentBuilt;
            }
        }
        public static Func<bool> Create(BaseDemoTestingFixture fixture) {
            ConditionAdapter conditionAdapter = new ConditionAdapter(fixture);
            return () => conditionAdapter.EvaluateCondition();
        }
    }
}
