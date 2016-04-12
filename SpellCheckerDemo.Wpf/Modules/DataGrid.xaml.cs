using System.Windows;
using System;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.SpellChecker;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core.Native;

namespace SpellCheckerDemo {
    public partial class DataGrid : SpellCheckerDemoModule {
        public DataGrid() {
            InitializeComponent();
            grid.ItemsSource = EmployeesData.DataSource;
        }

        bool stopChecking;
        protected void CheckGrid() {
            stopChecking = false;
            SubscribeToEvents();
            CheckCell(0, 0);
        }
        void SubscribeToEvents() {
            SpellChecker.CheckCompleteFormShowing += new DevExpress.XtraSpellChecker.FormShowingEventHandler(Checker_CheckCompleteFormShowing);
        }
        void UnsubscribeFromEvents() {
            SpellChecker.CheckCompleteFormShowing -= new DevExpress.XtraSpellChecker.FormShowingEventHandler(Checker_CheckCompleteFormShowing);
        }
        void Button_Click(object sender, RoutedEventArgs e) {
            CheckGrid();
        }
        void Checker_CheckCompleteFormShowing(object sender, DevExpress.XtraSpellChecker.FormShowingEventArgs e) {
            e.Handled = true;
        }
        void CheckCell(int rowIndex, int columnIndex) {
            GridColumn column = grid.Columns[columnIndex];
            if (column.IsGrouped)
                return;
            grid.CurrentColumn = column;
            grid.View.FocusedRowHandle = rowIndex;
            grid.View.ShowEditor();
            grid.UpdateLayout();

            BaseEdit activeEditor = grid.View.ActiveEditor;
            if (activeEditor == null || !SpellChecker.CanCheck(activeEditor))
                CheckNextCell();
            else {
                UnsubscribeFromEvents();
                SpellChecker.CheckCompleteFormShowing += new DevExpress.XtraSpellChecker.FormShowingEventHandler(Checker_CheckCompleteFormShowing);
                SpellChecker.AfterCheck += Checker_AfterCheck;
                SpellChecker.Check(activeEditor);
            }
        }
        void Checker_AfterCheck(object sender, EventArgs e) {
            SpellChecker.AfterCheck -= Checker_AfterCheck;
            SpellChecker.CheckCompleteFormShowing -= new DevExpress.XtraSpellChecker.FormShowingEventHandler(Checker_CheckCompleteFormShowing);
            SubscribeToEvents();
            stopChecking = (e as DevExpress.XtraSpellChecker.AfterCheckEventArgs).Reason == DevExpress.XtraSpellChecker.StopCheckingReason.User;
            CheckNextCell();
        }
        void CheckNextCell() {
            CheckNextCell(grid.View.FocusedRowHandle, (GridColumn)grid.CurrentColumn);
        }
        void CheckNextCell(int rowIndex, GridColumn column) {
            if (stopChecking) {
                UnsubscribeFromEvents();
                return;
            }
            int columnIndex = grid.Columns.IndexOf(column);
            int nextColumnIndex = columnIndex == grid.Columns.Count - 1 ? 0 : columnIndex + 1;
            int nextRowIndex = nextColumnIndex == 0 ? rowIndex + 1 : rowIndex;
            if (!grid.IsValidRowHandle(nextRowIndex)) {
                UnsubscribeFromEvents();
                DemoUtils.ShowDialog("Spelling", "Spelling check is complete", this.GetRootVisual());
            }
            else
                CheckCell(nextRowIndex, nextColumnIndex);
        }
        private void editCheckAsYouType_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            ApplySpellCheckMode((bool)e.NewValue);
        }
        private void grid_Loaded(object sender, RoutedEventArgs e) {
            ApplySpellCheckMode(true);
        }
    }
}
