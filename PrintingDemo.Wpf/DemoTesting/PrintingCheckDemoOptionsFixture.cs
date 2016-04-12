using System;
using System.Windows;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.DataNodes;

namespace PrintingDemo.Tests {
    public class CheckDemoOptionsFixture : BaseDemoTestingFixture {
        GroupsModule GroupedReportModule { get { return (GroupsModule)DemoBaseTesting.CurrentDemoModule; } }
        MultiColumnModule ColumnReportModule { get { return (MultiColumnModule)DemoBaseTesting.CurrentDemoModule; } }

        protected override void CreateActions() {
            base.CreateActions();
            CreateCheckGroupedReportModuleActions();
            CreateCheckMultiColumnReportModuleActions();
        }

        void CreateCheckGroupedReportModuleActions() {
            AddLoadModuleActions(typeof(GroupsModule));

            AddAction_ToggleCheckEdit(() => GroupedReportModule.repeatHeaderEveryPage, true, () => ((CollectionViewLink)GroupedReportModule.ViewModel.Link).GroupInfos[0].RepeatHeaderEveryPage);
            WaitDocumentBuildCompleted();

            AddAction_ToggleCheckEdit(() => GroupedReportModule.repeatHeaderEveryPage, false, () => ((CollectionViewLink)GroupedReportModule.ViewModel.Link).GroupInfos[0].RepeatHeaderEveryPage);
            WaitDocumentBuildCompleted();

            AddAction_ToggleCheckEdit(() => GroupedReportModule.keepTogether, GroupUnion.WholePage, () => ((CollectionViewLink)GroupedReportModule.ViewModel.Link).GroupInfos[0].Union);
            WaitDocumentBuildCompleted();

            AddAction_ToggleCheckEdit(() => GroupedReportModule.pageBreakAfter, true, () => ((CollectionViewLink)GroupedReportModule.ViewModel.Link).GroupInfos[0].PageBreakAfter);
            WaitDocumentBuildCompleted();
        }

        void CreateCheckMultiColumnReportModuleActions() {
            AddLoadModuleActions(typeof(MultiColumnModule));
            foreach(PageNumberKind pageNumberKind in (System.Collections.IEnumerable)DispatchExpr(() => ColumnReportModule.pageNumberKindEdit.ItemsSource)) {
                IAmAlive("Changing the number kind");
                PageNumberKind temp_pageNumberKind = pageNumberKind;
                Dispatch(() => { ColumnReportModule.pageNumberKindEdit.EditValue = temp_pageNumberKind; });
                WaitDocumentBuildCompleted();

                foreach(HorizontalAlignment pageNumberAlignment in (System.Collections.IEnumerable)DispatchExpr(() => ColumnReportModule.pageNumberAlignmentEdit.ItemsSource)) {
                    HorizontalAlignment temp_pageNumberAlignment = pageNumberAlignment;
                    Dispatch(() => { ColumnReportModule.pageNumberAlignmentEdit.EditValue = temp_pageNumberAlignment; });
                    WaitDocumentBuildCompleted();

                    foreach(PageNumberLocation pageNumberLocation in (System.Collections.IEnumerable)DispatchExpr(() => ColumnReportModule.pageNumberLocationEdit.ItemsSource)) {
                        PageNumberLocation temp_pageNumberLocation = pageNumberLocation;
                        Dispatch(() => { ColumnReportModule.pageNumberLocationEdit.EditValue = temp_pageNumberLocation; });
                        WaitDocumentBuildCompleted();
                    }
                }
            }

            Dispatch(() => { ColumnReportModule.downThenAcross.IsChecked = true; });
            WaitDocumentBuildCompleted();
            Dispatch(() => { ColumnReportModule.acrossThenDown.IsChecked = true; });
            WaitDocumentBuildCompleted();
            Dispatch(() => { EditorsActions.SetEditValue(ColumnReportModule.pageNumberFormatEdit, "Page {test}"); });
            WaitDocumentBuildCompleted();
        }

        void AddAction_ToggleCheckEdit(Func<CheckEdit> getCheckEdit, object expectedValue, Func<object> getActualValue) {
            Dispatch(() => { EditorsActions.ToggleCheckEdit(getCheckEdit()); });
            Assert.AreEqual(expectedValue, DispatchExpr(getActualValue));
        }

        void WaitDocumentBuildCompleted() {
            AddConditionAction(DocumentBuildCompletedConditionFactory.Create(this), null);
        }
    }
}
