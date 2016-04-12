using System.Windows;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.Printing;

namespace PrintingDemo.Tests {
    public class PrintingCheckAllDemosFixture : CheckAllDemosFixture {
        protected override void CreateSetCurrentDemoActions(ModuleDescription module, bool checkMemoryLeaks) {
            base.CreateSetCurrentDemoActions(module, checkMemoryLeaks);
            AddConditionAction(DocumentBuildCompletedConditionFactory.Create(this), null);
            AddSimpleAction(delegate() {
                if(DemoBaseTesting.CurrentDemoModule != null) {
                    DocumentPreviewControl activePreview = LayoutHelper.FindElement(DemoBaseTesting.CurrentDemoModule, IsDocumentViewer) as DocumentPreviewControl;
                    Assert.IsNotNull(activePreview);
                }
            });
        }

        bool IsDocumentViewer(FrameworkElement element) {
            return element is DocumentPreviewControl;
        }

        protected override bool CheckMemoryLeaks(System.Type moduleType) {
            return true;
        }
    }
}
