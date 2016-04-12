using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Grid.Native;
using System.Linq;
using DevExpress.DemoData.Models;
using System.Collections.Generic;

namespace GridDemo.Tests {
    public class GridCheckAllDemosFixture : CheckAllDemosFixture {
    }
    #region GridDemoModulesAccessor
    public class GridDemoModulesAccessor : DemoModulesAccessor<GridDemoModule> {
        public GridDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
        public DevExpress.Xpf.Grid.GridControl GridControl { get { return DemoModule.GridControl; } }
        public DevExpress.Xpf.Grid.GridViewBase View { get { return (DevExpress.Xpf.Grid.GridViewBase)GridControl.View; } }
        public DevExpress.Xpf.Grid.TableView TableView { get { return (DevExpress.Xpf.Grid.TableView)View; } }
        public DevExpress.Xpf.Grid.CardView CardView { get { return (DevExpress.Xpf.Grid.CardView)View; } }
        public StandardTableView TableViewModule { get { return (StandardTableView)DemoModule; } }
        public MultiRowSelection MultiSelectionModule { get { return (MultiRowSelection)DemoModule; } }
        public CopyPasteOperations CopyPasteModule { get { return (CopyPasteOperations)DemoModule; } }
        public LargeDataSource LargeDataSetModule { get { return (LargeDataSource)DemoModule; } }
        public GridDemo.DragDrop DragDropModule { get { return (GridDemo.DragDrop)DemoModule; } }
        public GridDemo.FilterControl FilterControlModule { get { return (GridDemo.FilterControl)DemoModule; } }
        public GridDemo.HitTesting HitTestModule { get { return (GridDemo.HitTesting)DemoModule; } }
        public GridDemo.Filtering FilteringModule { get { return (GridDemo.Filtering)DemoModule; } }
  public UnboundColumns UnboundColumnsModule { get { return (UnboundColumns)DemoModule; } }
    }
    #endregion
    public abstract class BaseGridDemoTestingFixture : BaseDemoTestingFixture {
        readonly GridDemoModulesAccessor modulesAccessor;
        public BaseGridDemoTestingFixture() {
            modulesAccessor = new GridDemoModulesAccessor(this);
        }
        public DevExpress.Xpf.Grid.GridControl GridControl { get { return modulesAccessor.GridControl; } }
        public DevExpress.Xpf.Grid.GridViewBase View { get { return modulesAccessor.View; } }
        public DevExpress.Xpf.Grid.TableView TableView { get { return modulesAccessor.TableView; } }
        public DevExpress.Xpf.Grid.CardView CardView { get { return modulesAccessor.CardView; } }
        public StandardTableView TableViewModule { get { return modulesAccessor.TableViewModule; } }
        public MultiRowSelection MultiSelectionModule { get { return modulesAccessor.MultiSelectionModule; } }
        public CopyPasteOperations CopyPasteModule { get { return modulesAccessor.CopyPasteModule; } }
        public LargeDataSource LargeDataSetModule { get { return modulesAccessor.LargeDataSetModule; } }
        public GridDemo.DragDrop DragDropModule { get { return modulesAccessor.DragDropModule; } }
        public GridDemo.FilterControl FilterControlModule { get { return modulesAccessor.FilterControlModule; } }
        public GridDemo.HitTesting HitTestModule { get { return modulesAccessor.HitTestModule; } }
        public GridDemo.Filtering FilteringModule { get { return modulesAccessor.FilteringModule; } }
  public UnboundColumns UnboundColumnsModule { get { return modulesAccessor.UnboundColumnsModule; } }
    }
    public class CheckDemoOptionsFixture : BaseGridDemoTestingFixture {
        protected override void CreateActions() {
            base.CreateActions();
            AddSimpleAction(CreateCheckDemosActions);
        }
        void CreateCheckDemosActions() {
            CreateCheckFilterControlDemoActions();
            CreateCheckHitTestDemoActions();
            CreateCheckUnboundColumnsDemoActions();
            CreateCheckFilteringDemoActions();
            CreateCheckMultiSelectionDemoActions();
            CreateCheckCopyPasteDemoActions();
            CreateCheckTableViewDemoActions();
            CreateCheckFixedColumnsDemoActions();
            CreateSetCurrentDemoActions(null, false);
        }
        bool IsListBox(FrameworkElement element) {
            return element is System.Windows.Controls.ListBox;
        }
        int GetListBoxEditCount(ListBoxEdit listBoxEdit) {
            return ((System.Collections.IList)listBoxEdit.ItemsSource).Count;
        }
        #region UnboundColumns
        void CreateCheckUnboundColumnsDemoActions() {
            AddLoadModuleActions(typeof(GridDemo.UnboundColumns));
            AddSimpleAction(delegate() {
                Assert.AreEqual(0, GridControl.Columns["OrderID"].GroupIndex);
                Assert.AreEqual(1, GridControl.GroupCount);
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-1));
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-2));
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-3));
                Assert.IsTrue(UnboundColumnsModule.showExpressionEditorButton.IsEnabled);
                Assert.IsTrue(GridControl.Columns["Total"].AllowUnboundExpressionEditor);
                Assert.IsTrue(GridControl.Columns["DiscountAmount"].AllowUnboundExpressionEditor);
                Assert.AreEqual(3, UnboundColumnsModule.columnsList.Items.Count);

                System.Windows.Controls.ListBox listBox;
                listBox = (System.Windows.Controls.ListBox)LayoutHelper.FindElement(UnboundColumnsModule.columnsList, IsListBox);

                Assert.AreEqual("Discount Amount", ((System.Windows.Controls.ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(0)).Content);
                Assert.AreEqual("Total", ((System.Windows.Controls.ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(1)).Content);
                Assert.AreEqual("Total Scale", ((System.Windows.Controls.ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(2)).Content);
                Assert.AreEqual("DiscountAmount", ((System.Windows.Controls.ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(0)).Tag);
                Assert.AreEqual("Total", ((System.Windows.Controls.ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(1)).Tag);
                Assert.AreEqual("TotalScale", ((System.Windows.Controls.ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(2)).Tag);

                Assert.IsNull(LayoutHelper.FindElementByName(View.GetCellElementByRowHandleAndColumn(3, GridControl.Columns["Total"]), "arrow"));
                Assert.IsNotNull(LayoutHelper.FindElementByName(View.GetCellElementByRowHandleAndColumn(4, GridControl.Columns["Total"]), "arrow"));
                Assert.AreEqual((decimal)0.0, GridControl.GetCellValue(0, GridControl.Columns["DiscountAmount"]));
                Assert.AreEqual((decimal)168.0, GridControl.GetCellValue(0, GridControl.Columns["Total"]));
                Assert.AreEqual((decimal)0.4, GridControl.GetCellValue(0, GridControl.Columns["TotalScale"]));
                Assert.IsTrue(Math.Abs((double)223.0 - Convert.ToDouble(GridControl.GetCellValue(6, GridControl.Columns["DiscountAmount"]))) < 0.001);
                Assert.IsTrue(Math.Abs((double)1261.4 - Convert.ToDouble(GridControl.GetCellValue(6, GridControl.Columns["Total"]))) < 0.001);
                Assert.IsTrue(Math.Abs((double)0.7614 - Convert.ToDouble(GridControl.GetCellValue(6, GridControl.Columns["TotalScale"]))) < 0.001);

                AssertShowExpressionEditor("Round([UnitPrice] * [Quantity] - [Total])");
                UnboundColumnsModule.columnsList.SelectedIndex = 1;
                UpdateLayoutAndDoEvents();
                AssertShowExpressionEditor("[UnitPrice] * [Quantity] * (1 - [Discount])");
                UnboundColumnsModule.columnsList.SelectedIndex = 2;
                UpdateLayoutAndDoEvents();
                AssertShowExpressionEditor("Iif([Total] < 1000, 0.4, Min(0.5 + ([Total] - 1000) / 1000, 1.2))");

                GridControl.Columns["DiscountAmount"].UnboundExpression = "[UnitPrice] * [Quantity] * (1 - [Discount])+[Discount][[Discount]]";
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(DevExpress.Data.UnboundErrorObject.Value, GridControl.GetCellValue(0, GridControl.Columns["DiscountAmount"]));
            });
        }
        ExpressionEditorControl expressionEditorControl = null;
        void UnboundExpressionEditorHandler(object sender, DevExpress.Xpf.Grid.UnboundExpressionEditorEventArgs e) {
            expressionEditorControl = e.ExpressionEditorControl;
        }
        void AssertShowExpressionEditor(string expectedExpression) {
            View.UnboundExpressionEditorCreated += UnboundExpressionEditorHandler;
            try {
                UIAutomationActions.ClickButton(UnboundColumnsModule.showExpressionEditorButton);
                UpdateLayoutAndDoEvents();
                Assert.IsTrue(expressionEditorControl.IsVisible);
                Assert.AreEqual(expectedExpression, expressionEditorControl.Expression);
                UIAutomationActions.ClickButton(LayoutHelper.FindParentObject<DialogControl>(expressionEditorControl).CancelButton);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(expressionEditorControl.IsVisible);
                expressionEditorControl = null;
            } finally {
                View.UnboundExpressionEditorCreated -= UnboundExpressionEditorHandler;
            }
        }
        #endregion
    #region hit test
        bool IsGroupGridRow(FrameworkElement element) {
            return element is DevExpress.Xpf.Grid.GroupRowControl;
        }
        void CreateCheckHitTestDemoActions() {
            AddLoadModuleActions(typeof(GridDemo.HitTesting));
            AddSimpleAction(delegate() {
                Assert.IsTrue(HitTestModule.showHitInfoCheckEdit.IsChecked.Value);
                Assert.AreEqual(0, HitTestModule.viewsListBox.SelectedIndex);
                Assert.AreEqual(GridControl, HitTestModule.hitInfoPopup.PlacementTarget);
                Assert.AreEqual(PlacementMode.Mouse, HitTestModule.hitInfoPopup.Placement);
                MouseActions.MouseMove(GridControl, -5, -5);
                Assert.IsFalse(HitTestModule.hitInfoPopup.IsOpen);
                MouseActions.MouseMove(GridControl, 5, 5);
                Assert.IsTrue(HitTestModule.hitInfoPopup.IsOpen);
                Assert.AreEqual(0d, HitTestModule.hitInfoPopup.HorizontalOffset);
                Assert.AreEqual(0d, HitTestModule.hitInfoPopup.VerticalOffset);

                EditorsActions.ToggleCheckEdit(HitTestModule.showHitInfoCheckEdit);
                Assert.IsFalse(HitTestModule.hitInfoPopup.IsOpen);
                EditorsActions.ToggleCheckEdit(HitTestModule.showHitInfoCheckEdit);
                Assert.IsTrue(HitTestModule.hitInfoPopup.IsOpen);

                MouseActions.LeftMouseDown(GridControlHelper.GetColumnHeaderElements(GridControl, GridControl.Columns[0])[0], 5, 5);
                MouseActions.MouseMove(GridControlHelper.GetColumnHeaderElements(GridControl, GridControl.Columns[0])[0], 25, 5);
                Assert.IsFalse(HitTestModule.hitInfoPopup.IsOpen);
                MouseActions.LeftMouseUp(GridControlHelper.GetColumnHeaderElements(GridControl, GridControl.Columns[0])[0], 5, 5);
                Assert.IsTrue(HitTestModule.hitInfoPopup.IsOpen);

                TableView.ShowColumnChooser();
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(HitTestModule.hitInfoPopup.IsOpen);
                TableView.HideColumnChooser();
                UpdateLayoutAndDoEvents();
                Assert.IsTrue(HitTestModule.hitInfoPopup.IsOpen);

                Assert.AreEqual(4, HitTestModule.hitIfoItemsControl.Items.Count);
                Assert.AreEqual("HitTest", GetHitInfoNameTextControl(0).NameValue);
                Assert.AreEqual("ColumnHeader", GetHitInfoNameTextControl(0).TextValue);
                Assert.AreEqual("Column", GetHitInfoNameTextControl(1).NameValue);
                Assert.AreEqual("ID", GetHitInfoNameTextControl(1).TextValue);
                Assert.AreEqual("RowHandle", GetHitInfoNameTextControl(2).NameValue);
                Assert.AreEqual("No row", GetHitInfoNameTextControl(2).TextValue);
                Assert.AreEqual("CellValue", GetHitInfoNameTextControl(3).NameValue);
                Assert.AreEqual("", GetHitInfoNameTextControl(3).TextValue);

                MouseActions.MouseMove(LayoutHelper.FindElementByName(GridControl, "PART_NewItemRow"), 35, 5);
                Assert.AreEqual(4, HitTestModule.hitIfoItemsControl.Items.Count);
                Assert.AreEqual("New Item Row", GetHitInfoNameTextControl(2).TextValue);

                MouseActions.MouseMove(LayoutHelper.FindElementByName(GridControl, "PART_FilterRow"), 35, 5);
                Assert.AreEqual(4, HitTestModule.hitIfoItemsControl.Items.Count);
                Assert.AreEqual("Auto Filter Row", GetHitInfoNameTextControl(2).TextValue);

                MouseActions.MouseMove(View.GetCellElementByRowHandleAndColumn(0, GridControl.Columns[0]), 35, 5);
                Assert.AreEqual(4, HitTestModule.hitIfoItemsControl.Items.Count);
                Assert.AreEqual("0 (data row)", GetHitInfoNameTextControl(2).TextValue);
                Assert.AreEqual("10248", GetHitInfoNameTextControl(3).TextValue);

                AssertAdditionalElements();

                MouseActions.MouseMove(LayoutHelper.FindElement(LayoutHelper.FindElement(GridControl, IsGroupGridRow), e => e is DevExpress.Xpf.Grid.GroupRowIndicator), 5, 5);
                Assert.AreEqual(5, HitTestModule.hitIfoItemsControl.Items.Count);
                Assert.AreEqual("RowIndicatorState", GetHitInfoNameTextControl(4).NameValue);
                Assert.AreEqual("Focused", GetHitInfoNameTextControl(4).TextValue);

                HitTestModule.viewsListBox.SelectedIndex = 1;
                CardView.CardLayout = DevExpress.Xpf.Grid.CardLayout.Rows;
                UpdateLayoutAndDoEvents();
                Assert.IsNotNull(CardView);
                AssertAdditionalElements();
            });
        }

        private void AssertAdditionalElements() {
            GridControl.Columns[0].GroupIndex = 0;
            UpdateLayoutAndDoEvents();
            MouseActions.MouseMove(View.GetRowElementByRowHandle(-1), View.GetRowElementByRowHandle(-1).ActualWidth / 2, View.GetRowElementByRowHandle(-1).ActualHeight / 2);
            Assert.AreEqual(4, HitTestModule.hitIfoItemsControl.Items.Count);
            Assert.AreEqual("-1 (group row)", GetHitInfoNameTextControl(2).TextValue);
            Assert.AreEqual(null, GetHitInfoNameTextControl(3).TextValue);

            MouseActions.MouseMove(LayoutHelper.FindElement(View.GetRowElementByRowHandle(-1), element => (string)element.GetValue(System.Windows.Controls.TextBlock.TextProperty) == "ID: 10248"), 5, 5);
            Assert.AreEqual(5, HitTestModule.hitIfoItemsControl.Items.Count);
            Assert.AreEqual("GroupValue", GetHitInfoNameTextControl(4).NameValue);
            Assert.AreEqual("OrderID: 10248", GetHitInfoNameTextControl(4).TextValue);

            MouseActions.MouseMove(LayoutHelper.FindElement(View.GetRowElementByRowHandle(-1), element => (string)element.GetValue(System.Windows.Controls.TextBlock.TextProperty) == "Count=3"), 5, 5);
            Assert.AreEqual(5, HitTestModule.hitIfoItemsControl.Items.Count);
            Assert.AreEqual("GroupSummary", GetHitInfoNameTextControl(4).NameValue);
            Assert.AreEqual("Count=3", GetHitInfoNameTextControl(4).TextValue);

            GridControl.ClearGrouping();
            UpdateLayoutAndDoEvents();
            MouseActions.MouseMove(LayoutHelper.FindElement(View, element => (string)element.GetValue(System.Windows.Controls.TextBlock.TextProperty) == "Count=2155"), 5, 5);
            Assert.AreEqual(4, HitTestModule.hitIfoItemsControl.Items.Count);
            Assert.AreEqual("FixedTotalSummary", GetHitInfoNameTextControl(3).NameValue);
            Assert.AreEqual("Count=2155", GetHitInfoNameTextControl(3).TextValue);
        }
        NameTextControl GetHitInfoNameTextControl(int intdex) {
            return (NameTextControl)VisualTreeHelper.GetChild(HitTestModule.hitIfoItemsControl.ItemContainerGenerator.ContainerFromIndex(intdex), 0);
        }
    #endregion

        T GetElementByName<T>(FrameworkElement root, string name) where T : FrameworkElement {
            return (T)LayoutHelper.FindElementByName(root, name);
        }

    #region table view
        void CreateCheckTableViewDemoActions() {
            AddLoadModuleActions(typeof(StandardTableView));
            AddSimpleAction(delegate() {
                Assert.IsTrue(TableView.AutoWidth);
                Assert.IsTrue(TableView.AllowSorting);
                Assert.IsTrue(TableView.AllowGrouping);
                Assert.IsTrue(TableView.AllowColumnMoving);
                Assert.IsTrue(TableView.AllowResizing);
                Assert.IsTrue(TableView.AllowBestFit);
                Assert.IsTrue(TableView.ShowHorizontalLines);
                Assert.IsTrue(TableView.ShowVerticalLines);

                EditorsActions.ToggleCheckEdit(TableViewModule.autoWidthCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.AutoWidth);

                EditorsActions.ToggleCheckEdit(TableViewModule.allowSortingCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.AllowSorting);
                Assert.IsFalse(GridControl.Columns["OrderID"].ActualAllowSorting);
                Assert.IsFalse(GridControl.Columns["OrderID"].ActualAllowGrouping);

                EditorsActions.ToggleCheckEdit(TableViewModule.allowSortingCheckEdit);
                EditorsActions.ToggleCheckEdit(TableViewModule.allowGroupingCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.AllowGrouping);
                Assert.IsTrue(GridControl.Columns["OrderID"].ActualAllowSorting);
                Assert.IsFalse(GridControl.Columns["OrderID"].ActualAllowGrouping);

                EditorsActions.ToggleCheckEdit(TableViewModule.allowMovingCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.AllowColumnMoving);
                Assert.IsFalse(GridControl.Columns["OrderID"].ActualAllowMoving);

                EditorsActions.ToggleCheckEdit(TableViewModule.allowResizingCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.AllowResizing);
                Assert.IsFalse(GridControl.Columns["OrderID"].ActualAllowResizing);

                EditorsActions.ToggleCheckEdit(TableViewModule.allowBestFitCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.AllowBestFit);

                EditorsActions.ToggleCheckEdit(TableViewModule.showHorizontalLinesCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.ShowHorizontalLines);

                EditorsActions.ToggleCheckEdit(TableViewModule.showVerticalLinesCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.ShowVerticalLines);

                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Row, TableViewModule.NavigationStyleComboBox.SelectedItem);
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Row, View.NavigationStyle);
                TableViewModule.NavigationStyleComboBox.SelectedItem = DevExpress.Xpf.Grid.GridViewNavigationStyle.Cell;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Cell, View.NavigationStyle);
                TableViewModule.NavigationStyleComboBox.SelectedItem = DevExpress.Xpf.Grid.GridViewNavigationStyle.None;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.None, View.NavigationStyle);
            });
        }
    #endregion
    #region Multi-Selection
        void CreateCheckMultiSelectionDemoActions() {
            AddLoadModuleActions(typeof(MultiRowSelection));
            AddSimpleAction(delegate() {
                Assert.AreEqual(GridControl.View, TableView);
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Cell, TableView.NavigationStyle);
                AssertMuliSelection(TableView);
                MultiSelectionModule.viewsListBox.SelectedIndex = 1;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(GridControl.View, CardView);
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Row, CardView.NavigationStyle);
                GridControl.SelectItem(0);
                AssertMuliSelection(CardView);
            });
        }
        void AssertMuliSelection(DevExpress.Xpf.Grid.GridViewBase gridView) {
            AssertMultiSelectMode(gridView, true);

            MultiSelectionModule.selectionModeListBox.EditValue = DevExpress.Xpf.Grid.MultiSelectMode.None;
            UpdateLayoutAndDoEvents();
            AssertMultiSelectMode(gridView, false);
            Assert.IsFalse(MultiSelectionModule.ProductsMultiSelectionOptionsControl.IsEnabled);
            Assert.IsFalse(MultiSelectionModule.PriceMultiSelectionOptionsControl.IsEnabled);
            MultiSelectionModule.selectionModeListBox.EditValue = DevExpress.Xpf.Grid.MultiSelectMode.Row;
            UpdateLayoutAndDoEvents();
            AssertMultiSelectMode(gridView, true);
            Assert.IsTrue(MultiSelectionModule.ProductsMultiSelectionOptionsControl.IsEnabled);
            Assert.IsTrue(MultiSelectionModule.PriceMultiSelectionOptionsControl.IsEnabled);

            Assert.AreEqual(77, ((IEnumerable<Product>)((ComboBoxEdit)MultiSelectionModule.ProductsMultiSelectionOptionsControl.comboBoxControl).ItemsSource).Count());
            Assert.AreEqual(1, gridView.Grid.SelectedItems.Count);

            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.SelectButton);
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(39, gridView.Grid.SelectedItems.Count);
            Assert.AreEqual(39, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox));

            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.UnselectButton);
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(1, gridView.Grid.SelectedItems.Count);
            Assert.AreEqual(1, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox));

            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.ReselectButton);
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(38, gridView.Grid.SelectedItems.Count);
            Assert.AreEqual(38, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox));

            gridView.Grid.UnselectAll();
            gridView.Grid.SelectRange(0, 3);

            Assert.AreEqual("Grand Total=${0:N}", gridView.VisibleColumns[4].TotalSummaries[0].Item.DisplayFormat);
            Assert.AreEqual(Convert.ToString(607.4, System.Globalization.CultureInfo.CurrentCulture), gridView.VisibleColumns[4].TotalSummaries[0].Value.ToString());
            GridControl.UnselectAll();

            int count = ((MultiSelectionModule.PriceMultiSelectionOptionsControl.comboBoxControl.ItemsSource) as System.Collections.Generic.List<Range>).Count;
            Assert.AreEqual(9, count);
            for(int i = 0; i < count; i++) {
                MultiSelectionModule.PriceMultiSelectionOptionsControl.comboBoxControl.SelectedIndex = i;
                UIAutomationActions.ClickButton(MultiSelectionModule.PriceMultiSelectionOptionsControl.SelectButton);
                UpdateLayoutAndDoEvents();
            }
            Assert.AreEqual(GridControl.VisibleRowCount, gridView.Grid.SelectedItems.Count);
            GridControl.UnselectAll();

            GridControl.GroupBy("OrderID");
            UpdateLayoutAndDoEvents();
            GridControl.SelectItem(0);
            GridControl.SelectItem(10);
            Assert.AreEqual("Grand Total=${0:N}", ((DevExpress.Xpf.Grid.GroupRowData)gridView.RootRowsContainer.Items[0]).GroupSummaryData[0].SummaryItem.DisplayFormat);
            Assert.AreEqual(168, Convert.ToInt32(((DevExpress.Xpf.Grid.GroupRowData)gridView.RootRowsContainer.Items[0]).GroupSummaryData[0].SummaryValue));
            Assert.AreEqual(336, Convert.ToInt32(((DevExpress.Xpf.Grid.GroupRowData)gridView.RootRowsContainer.Items[3]).GroupSummaryData[0].SummaryValue));
            Assert.AreEqual(504, Convert.ToInt32(gridView.VisibleColumns[4].TotalSummaries[0].Value));
            GridControl.ClearGrouping();
            GridControl.UnselectAll();
        }
        void AssertMultiSelectMode(DevExpress.Xpf.Grid.GridViewBase view, bool isMultiSelect){
            Assert.AreEqual(isMultiSelect ? DevExpress.Xpf.Grid.MultiSelectMode.Row : DevExpress.Xpf.Grid.MultiSelectMode.None, view.DataControl.SelectionMode);
        }
    #endregion
    #region Copy Paste
        void CreateCheckCopyPasteDemoActions() {
            AddLoadModuleActions(typeof(CopyPasteOperations));
            AddSimpleAction(delegate() {
                Assert.IsTrue(CopyPasteModule.firstGrid.ClipboardCopyMode != DevExpress.Xpf.Grid.ClipboardCopyMode.None);
                Assert.IsTrue(CopyPasteModule.secondGrid.ClipboardCopyMode != DevExpress.Xpf.Grid.ClipboardCopyMode.None);
                EditorsActions.ToggleCheckEdit(CopyPasteModule.allowCopyingtoClipboardCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsTrue(CopyPasteModule.firstGrid.ClipboardCopyMode == DevExpress.Xpf.Grid.ClipboardCopyMode.None);
                Assert.IsTrue(CopyPasteModule.secondGrid.ClipboardCopyMode == DevExpress.Xpf.Grid.ClipboardCopyMode.None);
                EditorsActions.ToggleCheckEdit(CopyPasteModule.allowCopyingtoClipboardCheckEdit);
                UpdateLayoutAndDoEvents();
                AssertGridsAndButtonState(FocusedGrid.None, false, false, false, false);

                Assert.AreEqual(10, CopyPasteModule.firstGrid.VisibleRowCount);
                Assert.AreEqual(0, CopyPasteModule.secondGrid.VisibleRowCount);

                Assert.AreEqual(0, CopyPasteModule.CopyPasteDemoTabControl.SelectedIndex);

                ClickOnGrid(CopyPasteModule.firstGrid);
                CopyPasteModule.firstGrid.SelectRange(1, 4);
                UpdateLayoutAndDoEvents();
                AssertGridsAndButtonState(FocusedGrid.First, true, true, false, true);

                Assert.AreEqual(0, CopyPasteModule.PasteRule.SelectedIndex);
                UIAutomationActions.ClickButton(CopyPasteModule.CopyButton);
                UpdateLayoutAndDoEvents();
                ClickOnGrid(CopyPasteModule.secondGrid);
                UpdateLayoutAndDoEvents();
                AssertGridsAndButtonState(FocusedGrid.Second, false, false, true, false);

                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton);
            });
            AddEventAction(CopyPasteOperations.PasteCompetedEvent, () => DemoBaseTesting.CurrentDemoModule, null, null);
            AddSimpleAction(delegate() {
                Assert.AreEqual(5, CopyPasteModule.secondGrid.VisibleRowCount);
                ClickOnGrid(CopyPasteModule.firstGrid);
                UpdateLayoutAndDoEvents();
                AssertGridsAndButtonState(FocusedGrid.First, true, true, true, true);
                UIAutomationActions.ClickButton(CopyPasteModule.DeleteButton);
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(5, CopyPasteModule.firstGrid.VisibleRowCount);

                CopyPasteModule.CopyPasteDemoTabControl.SelectedIndex = 1;
                UpdateLayoutAndDoEvents();
                CopyPasteModule.textEdit.Focus();
                UpdateLayoutAndDoEvents();
                AssertGridsAndButtonState(FocusedGrid.First, false, false, true, false);

                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton);
                UpdateLayoutAndDoEvents();
                Assert.AreEqual("Id From Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 40));
                CopyPasteModule.textEdit.Select(0, 8);
                UpdateLayoutAndDoEvents();
                CopyPasteModule.textEdit.Focus();
                UpdateLayoutAndDoEvents();
                AssertGridsAndButtonState(FocusedGrid.First, true, true, true, true);
                UIAutomationActions.ClickButton(CopyPasteModule.CutButton);
                UpdateLayoutAndDoEvents();
                Assert.AreEqual("Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 32));
                CopyPasteModule.textEdit.Focus();
                UpdateLayoutAndDoEvents();
                CopyPasteModule.textEdit.CaretIndex = 0;
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton);
                UpdateLayoutAndDoEvents();
                Assert.AreEqual("Id From Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 40));
                CopyPasteModule.textEdit.SelectAll();
                UpdateLayoutAndDoEvents();
                CopyPasteModule.textEdit.Focus();
                UpdateLayoutAndDoEvents();
                UIAutomationActions.ClickButton(CopyPasteModule.CopyButton);
                UpdateLayoutAndDoEvents();
                CopyPasteModule.textEdit.Focus();
                UpdateLayoutAndDoEvents();
                UIAutomationActions.ClickButton(CopyPasteModule.DeleteButton);
                UpdateLayoutAndDoEvents();
                Assert.AreEqual("", CopyPasteModule.textEdit.Text);
                CopyPasteModule.textEdit.Focus();
                UpdateLayoutAndDoEvents();
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton);
                UpdateLayoutAndDoEvents();
                Assert.AreEqual("Id From Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 40));

                CopyPasteModule.CopyPasteDemoTabControl.SelectedIndex = 0;
                UpdateLayoutAndDoEvents();
                CopyPasteModule.secondGrid.UnselectAll();
                CopyPasteModule.secondGrid.SelectItem(2);
                UpdateLayoutAndDoEvents();
                ((GridDemo.CopyPasteOutlookData)((DevExpress.Xpf.Grid.GridViewBase)CopyPasteModule.secondGrid.View).Grid.SelectedItems[0]).From = "QWERTY";
                ClickOnGrid(CopyPasteModule.secondGrid);
                UpdateLayoutAndDoEvents();
                UIAutomationActions.ClickButton(CopyPasteModule.CutButton);
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(4, CopyPasteModule.secondGrid.VisibleRowCount);

                CopyPasteModule.firstGrid.View.FocusedRowHandle = 2;
                ClickOnGrid(CopyPasteModule.firstGrid);
                UpdateLayoutAndDoEvents();
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton);
            });
            AddEventAction(CopyPasteOperations.PasteCompetedEvent, () => DemoBaseTesting.CurrentDemoModule, null, null);
            AddSimpleAction(delegate() {
                Assert.AreEqual(6, CopyPasteModule.firstGrid.VisibleRowCount);
                Assert.AreEqual("QWERTY", ((GridDemo.CopyPasteOutlookData)CopyPasteModule.firstGrid.GetRow(3)).From);

                CopyPasteModule.PasteRule.SelectedIndex = 1;
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton);
            });
            AddEventAction(CopyPasteOperations.PasteCompetedEvent, () => DemoBaseTesting.CurrentDemoModule, null, null);
            AddSimpleAction(delegate() {
                Assert.AreEqual(7, CopyPasteModule.firstGrid.VisibleRowCount);
                Assert.AreEqual("QWERTY", ((GridDemo.CopyPasteOutlookData)CopyPasteModule.firstGrid.GetRow(6)).From);
            });
        }
        void ClickOnGrid(DevExpress.Xpf.Grid.GridControl grid) {
            MouseActions.LeftMouseDown(grid.View, 2, 2);
            MouseActions.LeftMouseUp(grid.View, 2, 2);
        }
        void AssertGridsAndButtonState(FocusedGrid focusedGrid, bool copyBtn, bool cutBtn, bool pasteBtn, bool delBtn) {
            Assert.AreEqual(focusedGrid, CopyPasteModule.FocusedGrid);
            Assert.AreEqual(copyBtn, CopyPasteModule.CopyButton.IsEnabled);
            Assert.AreEqual(cutBtn, CopyPasteModule.CutButton.IsEnabled);
            Assert.AreEqual(pasteBtn, CopyPasteModule.PasteButton.IsEnabled);
            Assert.AreEqual(delBtn, CopyPasteModule.DeleteButton.IsEnabled);
        }
    #endregion
    #region Filter Control
        void CreateCheckFilterControlDemoActions() {
            AddLoadModuleActions(typeof(FilterControl));
            AddSimpleAction(delegate() {
                Assert.IsTrue(View.AllowFilterEditor);
                CriteriaOperator filterCriteria = new BinaryOperator("OrderID", 10248, BinaryOperatorType.Equal);
                Assert.AreEqual(filterCriteria, FilterControlModule.filterEditor.FilterCriteria);
                Assert.AreEqual(filterCriteria, GridControl.FilterCriteria);
                Assert.AreEqual(false, FilterControlModule.showGroupCommandsIcon.IsChecked);
                Assert.AreEqual(false, FilterControlModule.showOperandTypeIcon.IsChecked);
                Assert.AreEqual(true, FilterControlModule.showToolTips.IsChecked);
                Assert.IsFalse(FilterControlModule.filterEditor.ShowGroupCommandsIcon);
                Assert.IsFalse(FilterControlModule.filterEditor.ShowOperandTypeIcon);
                Assert.IsTrue(FilterControlModule.filterEditor.ShowToolTips);
                EditorsActions.ToggleCheckEdit(FilterControlModule.showGroupCommandsIcon);
                UpdateLayoutAndDoEvents();
                Assert.IsTrue(FilterControlModule.filterEditor.ShowGroupCommandsIcon);
                EditorsActions.ToggleCheckEdit(FilterControlModule.showOperandTypeIcon);
                UpdateLayoutAndDoEvents();
                Assert.IsTrue(FilterControlModule.filterEditor.ShowOperandTypeIcon);
                EditorsActions.ToggleCheckEdit(FilterControlModule.showToolTips);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(FilterControlModule.filterEditor.ShowToolTips);

                filterCriteria = new BinaryOperator("OrderID", 10249L, BinaryOperatorType.Equal);
                FilterControlModule.filterEditor.FilterCriteria = filterCriteria;
                UpdateLayoutAndDoEvents();
                UIAutomationActions.ClickButton(FilterControlModule.ApplyFilterButton);
                Assert.AreEqual(filterCriteria, GridControl.FilterCriteria);
                filterCriteria = new BinaryOperator("OrderID", 10250L, BinaryOperatorType.Equal);
                GridControl.FilterCriteria = filterCriteria;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(filterCriteria, FilterControlModule.filterEditor.FilterCriteria);

                View.FilterEditorCreated += new DevExpress.Xpf.Grid.FilterEditorEventHandler(View_FilterEditorCreated);
                View.ShowFilterEditor(null);
                UpdateLayoutAndDoEvents();
                Assert.IsNotNull(FilterEditorFromGrid);
                Assert.IsTrue(FilterEditorFromGrid.ShowGroupCommandsIcon);
                Assert.IsTrue(FilterEditorFromGrid.ShowOperandTypeIcon);
                Assert.IsFalse(FilterEditorFromGrid.ShowToolTips);

                DialogControl filterDialogControl = LayoutHelper.FindParentObject<DialogControl>(FilterEditorFromGrid);
                Assert.IsNotNull(filterDialogControl);
                UIAutomationActions.ClickButton(filterDialogControl.CancelButton);
            });
        }
        DevExpress.Xpf.Editors.Filtering.FilterControl FilterEditorFromGrid = null;
        void View_FilterEditorCreated(object sender, DevExpress.Xpf.Grid.FilterEditorEventArgs e) {
            FilterEditorFromGrid = e.FilterControl;
        }
    #endregion
    #region Filtering
        void CreateCheckFilteringDemoActions() {
            AddLoadModuleActions(typeof(Filtering));
            AddSimpleAction(delegate() {
                Assert.IsTrue((bool)FilteringModule.allowFilterEditor.IsChecked);
                Assert.IsTrue((bool)FilteringModule.allowFilteringCheckEdit.IsChecked);
                Assert.AreEqual(0, FilteringModule.showFilterPanelModeListBox.SelectedIndex);
                Assert.AreEqual(0, FilteringModule.viewsListBox.SelectedIndex);
                Assert.AreEqual(DevExpress.Xpf.Grid.ShowFilterPanelMode.Default, View.ShowFilterPanelMode);
                FilteringModule.showFilterPanelModeListBox.SelectedIndex = 1;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(DevExpress.Xpf.Grid.ShowFilterPanelMode.ShowAlways, View.ShowFilterPanelMode);

                DevExpress.Xpf.Grid.GridColumn column = FilteringModule.colCountry;
                Assert.AreEqual(DefaultBoolean.Default, column.AllowColumnFiltering);

                Assert.AreEqual(DefaultBoolean.Default, column.AllowColumnFiltering);
                CheckEdit updateImmediatelyCheckBox = FilteringModule.immediateUpdateCountryColumnFilterCheckEdit;
                Assert.AreEqual(true, updateImmediatelyCheckBox.IsChecked);
                Assert.AreEqual(true, column.ImmediateUpdateColumnFilter);
                EditorsActions.ToggleCheckEdit(updateImmediatelyCheckBox);
                Assert.AreEqual(false, column.ImmediateUpdateColumnFilter);
                EditorsActions.ToggleCheckEdit(updateImmediatelyCheckBox);
                Assert.AreEqual(true, column.ImmediateUpdateColumnFilter);

                FilteringModule.showFilterPanelModeListBox.SelectedIndex = 2;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(DevExpress.Xpf.Grid.ShowFilterPanelMode.Never, View.ShowFilterPanelMode);
                FilteringModule.viewsListBox.SelectedIndex = 1;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(typeof(DevExpress.Xpf.Grid.CardView), CardView.GetType());
            });
        }
    #endregion

        void CreateCheckFixedColumnsDemoActions() {
            AddLoadModuleActions(typeof(FixedDataColumns));
        }
    }
    public class BugFixesFixture : BaseGridDemoTestingFixture {
        protected override void CreateActions() {
            base.CreateActions();
            AddSimpleAction(CreateCheckDemosActions);
        }
        void CreateCheckDemosActions() {
            Create_B144178_Actions();
            CreateSetCurrentDemoActions(null, false);
        }
    #region B144178
        void Create_B144178_Actions() {
            AddLoadModuleActions(typeof(LargeDataSource));
            AddSimpleAction(delegate() {
                AssertPrioritySorting("Priority(7)");
            });
            AddLoadModuleActions(typeof(CellEditors));
            AddSimpleAction(delegate() {
                AssertPrioritySorting("Priority");
            });
            AddLoadModuleActions(typeof(AutoFilterRow));
            AddSimpleAction(delegate() {
                AssertPrioritySorting("Priority");
            });
        }

        private void AssertPrioritySorting(string fieldName) {
            GridControl.SortBy(fieldName);
            for(int i = 0; i < 10; i++) {
                Assert.AreEqual(Priority.Low, (Priority)GridControl.GetCellValue(i, fieldName));
                Assert.AreEqual(Priority.High, (Priority)GridControl.GetCellValue(GridControl.VisibleRowCount - 1 - i, fieldName));
            }
        }
        #endregion
    }
}
