using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Utils;
using DevExpress.Mvvm;
using DevExpress.DemoData.Models;
using System.Data.Entity;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    [CodeFile("ModuleResources/PersistentRowStateTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/PersistentRowStateClasses.(cs)")]
    public partial class PersistentRowState : GridDemoModule {
        public static readonly DependencyProperty SetCursorPositionCommandProperty;
        public static readonly DependencyProperty CanExecuteHandlerCommandProperty;
        CarsContext context = new CarsContext();
        static PersistentRowState() {
            Type ownerType = typeof(PersistentRowState);
            SetCursorPositionCommandProperty = DependencyPropertyManager.Register("SetCursorPositionCommand", typeof(ICommand), ownerType);
            CanExecuteHandlerCommandProperty = DependencyPropertyManager.Register("CanExecuteHandlerCommand", typeof(ICommand), ownerType);
        }
        public PersistentRowState() {
            DataContext = this;
            SetCursorPositionCommand = new DelegateCommand<FrameworkElement>(SetCursorPosition);
            CanExecuteHandlerCommand = new DelegateCommand<CanExecuteRoutedEventArgs>(OnDescriptionTextBoxCommandPreviewCanExecute);
            InitializeComponent();
            grid.ItemsSource = context.Cars.ToList();
            UpdateView();
        }
        private void viewListBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(grid == null)
                return;
            UpdateView();
        }
        void UpdateView() {
            if(viewListBox.SelectedIndex == 0)
                grid.View = (GridViewBase)FindResource("gridView");
            if(viewListBox.SelectedIndex == 1)
                grid.View = (GridViewBase)FindResource("cardView");
        }
        private void SetCursorPosition(FrameworkElement sender) {
            this.sender = sender;
            Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(SetCursorPositionCore));
        }
        FrameworkElement sender;
        void SetCursorPositionCore() {
            Point point = sender.PointToScreen(new Point(sender.ActualWidth / 2, sender.ActualHeight / 2));
            UnsafeNativeMethods.SetCursorPos((int)point.X, (int)point.Y);
        }
        void OnDescriptionTextBoxCommandPreviewCanExecute(CanExecuteRoutedEventArgs e) {
            if(e.Command == EditingCommands.MoveDownByLine ||
                e.Command == EditingCommands.MoveUpByLine ||
                e.Command == EditingCommands.MoveUpByPage ||
                e.Command == EditingCommands.MoveDownByPage) {
                e.ContinueRouting = true;
            }
        }
        public ICommand SetCursorPositionCommand {
            get { return (ICommand)GetValue(SetCursorPositionCommandProperty); }
            set { SetValue(SetCursorPositionCommandProperty, value); }
        }
        public ICommand CanExecuteHandlerCommand {
            get { return (ICommand)GetValue(CanExecuteHandlerCommandProperty); }
            set { SetValue(CanExecuteHandlerCommandProperty, value); }
        }
        protected override DataViewBase GetExportView() {
            return null;
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
