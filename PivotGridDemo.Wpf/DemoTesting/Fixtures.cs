using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase.Helpers.TextColorizer;
using DevExpress.Xpf.Editors;
using System.Windows.Controls;
using DevExpress.Xpf.Editors.Helpers;

using DevExpress.Xpf.PivotGrid;
using PivotGridDemo.PivotGrid;
using DevExpress.Xpf.PivotGrid.Internal;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Core;

namespace PivotGridDemo.Tests {
    public class PivotGridCheckAllDemosFixture : CheckAllDemosFixture {

        protected override bool CanRunModule(Type moduleType) {
            return base.CanRunModule(moduleType) && moduleType.Name != "AsyncMode" && moduleType.Name != "ServerMode"
            ;
        }


        protected override void CreateCheckOptionsAction() {
            AddCheck0();
            CreateCheck1();
        }

        private void CreateCheck1() {
            if(DemoBaseTesting.CurrentDemoModule.GetType() == typeof(ChartsIntegration))
                Dispatch(HidePrefilterAction);
            Dispatch(AssertHeadersFilterButtonVisibilityB183058);
            Dispatch(AssertTextEditsWidthB185051);
            Dispatch(TextBlockTextTrimmingB185312);
            if(DemoBaseTesting.CurrentDemoModule.GetType() == typeof(PrintTemplates)) {
                PivotGridControl pivotGrid = DispatchExpr(() => FindElement<PivotGridControl>(DemoBaseTesting.CurrentDemoModule));
                FloatingContainer cont = DispatchExpr(() => PrintHelper.ShowPrintPreview(pivotGrid, pivotGrid));
                DispatchBusyWait(() => cont.IsOpen);
                System.Threading.Thread.Sleep(2000);
                Dispatch(() => { cont.Close(); });
            }
        }

        private void AddCheck0() {
            AddSimpleAction(ComboBoxItemsEditableB181973);
            AddSimpleAction(
                delegate() {
                    if(DemoBaseTesting.CurrentDemoModule.GetType() == typeof(ChartsIntegration))
                        ShowPrefilterAction();
                });
            AddSimpleAction(delegate() {
                if(DemoBaseTesting.CurrentDemoModule.GetType() == typeof(ChartsIntegration))
                    BestFitAction();
            });
        }

        void TextBlockTextTrimmingB185312() {
            FrameworkElement control = ((DemoModule)DemoBaseTesting.CurrentDemoModule).DemoModuleControl.OptionsContent;
            if(control == null) return;
            List<TextBlock> editors = FindAllElements<TextBlock>(control);
            foreach(TextBlock edit in editors) {
                bool trimmed = TextBlockService.CalcIsTextTrimmed(edit) && edit.DesiredSize.Width > (edit.ActualWidth + edit.Margin.Left + edit.Margin.Right);
                AssertLog.IsFalse(trimmed, "Text is trimmed: " + DemoBaseTesting.CurrentDemoModule.GetType().Name + " text=" + edit.Text + ", Name=" + edit.Name +
                    ", actual " + edit.ActualWidth + "px, desired:" + edit.DesiredSize.Width + "px");
            }
        }

        void AssertTextEditsWidthB185051() {
            FrameworkElement control = ((DemoModule)DemoBaseTesting.CurrentDemoModule).DemoModuleControl.OptionsContent;
            if(control == null) return;
            List<TextEdit> editors = FindAllElements<TextEdit>(control);
            foreach(TextEdit edit in editors) {
                if(edit.EditMode == EditMode.InplaceInactive || edit.IsPrintingMode == true)
                    continue;
                AssertLog.IsFalse(edit.Width == Double.PositiveInfinity, "TextEdit unlimited width: " + DemoBaseTesting.CurrentDemoModule.GetType().Name + " text=" + edit.Text + ", Name=" + edit.Name + " desired " + edit.ActualWidth + "px");
            }
        }

        private void AssertHeadersFilterButtonVisibilityB183058() {
            PivotGridControl pivotGrid = FindElement<PivotGridControl>(DemoBaseTesting.CurrentDemoModule);
            List<FieldHeader> headers = FindAllElements<FieldHeader>(pivotGrid);
            foreach(FieldHeader header in headers) {
                PivotGridField field = header.Field;
                bool fieldFiltered = GetIsFiltered(field);
                Visibility filterVisibility = fieldFiltered || header.IsMouseOver ? Visibility.Visible : Visibility.Hidden;
                if(!field.GetInternalField().ShowFilterButton)
                    filterVisibility = Visibility.Collapsed;
                if(filterVisibility != Visibility.Collapsed)
                    AssertLog.IsTrue(fieldFiltered == field.IsFiltered, "field.IsFiltered " +
                                                DemoBaseTesting.CurrentDemoModule.GetType().Name +
                                                                    " " + field.Name + " expected\real:" + fieldFiltered.ToString() + " \\ " + field.IsFiltered.ToString());
                AssertLog.IsTrue(filterVisibility == header.IsFilterButtonVisible, "header.IsFiltered " +
                                            DemoBaseTesting.CurrentDemoModule.GetType().Name +
                                                                " " + field.Name + " expected\real:" + filterVisibility.ToString() + " \\ " + header.IsFilterButtonVisible.ToString());
            }
        }

        bool GetIsFiltered(PivotGridField field) {
            PivotGridInternalField internalField = field.GetInternalField();
            return ((internalField.Group == null || internalField.GroupFilterMode.Equals(DevExpress.XtraPivotGrid.PivotGroupFilterMode.List)) && !internalField.FilterValues.IsEmpty)
                || field.Group != null && internalField.GroupFilterMode.Equals(DevExpress.XtraPivotGrid.PivotGroupFilterMode.Tree) && !field.Group.FilterValues.IsEmpty;
        }

        void ComboBoxItemsEditableB181973() {
            FrameworkElement control = ((DemoModule)DemoBaseTesting.CurrentDemoModule).DemoModuleControl.OptionsContent;
            if(control == null) return;
            List<ComboBoxEdit> editors = FindAllElements<ComboBoxEdit>(control);
            for(int i = 0;i < editors.Count;i++) {
                AssertLog.IsFalse((bool)editors[i].IsTextEditable, "ComboBoxEdit items editable: " + DemoBaseTesting.CurrentDemoModule.GetType().Name + " " + editors[i].Name);
                AssertLog.IsFalse(editors[i].SelectedIndex < 0, "ComboBoxEdit item is not selected: " + DemoBaseTesting.CurrentDemoModule.GetType().Name + " " + editors[i].Name);
            }
        }

        bool ChartGeneralOptionsCondition() {
            return DemoBaseTesting.CurrentDemoModule.GetType() == typeof(ChartsIntegration);
        }

        void BestFitAction() {
            PivotGridControl pivotGrid = FindElement<PivotGridControl>(DemoBaseTesting.CurrentDemoModule);
            pivotGrid.BestFit();
        }
        void HidePrefilterAction() {
            PivotGridControl pivotGrid = FindElement<PivotGridControl>(DemoBaseTesting.CurrentDemoModule);
            if(pivotGrid.PrefilterContainer != null)
                pivotGrid.PrefilterContainer.Close();
            UpdateLayoutAndDoEvents();
        }

        void ShowPrefilterAction() {
            PivotGridControl pivotGrid = FindElement<PivotGridControl>(DemoBaseTesting.CurrentDemoModule);
            pivotGrid.ShowPrefilter();
            UpdateLayoutAndDoEvents();
        }

        List<T> FindAllElements<T>(FrameworkElement element) where T : FrameworkElement {
            List<T> items = new List<T>();
            LayoutHelper.ForEachElement(element, d => addItem<T>(items, d));
            return items;
        }
        bool addItem<T>(List<T> items, FrameworkElement d) where T : FrameworkElement {
            if(d.GetType() == typeof(T)) items.Add((T)d);
            return true;
        }

        T FindElement<T>(FrameworkElement element) where T : FrameworkElement {
            return (T)LayoutHelper.FindElement(element, d => d.GetType() == typeof(T));
        }
    }
}
