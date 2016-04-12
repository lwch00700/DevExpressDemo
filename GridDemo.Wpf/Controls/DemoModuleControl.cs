using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Utils.Themes;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core.Native;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    public class GridDemoModule : DemoModule {
        public static readonly DependencyProperty GridControlProperty;

        static GridDemoModule() {
            GridControlProperty = DependencyProperty.Register("GridControl", typeof(GridControl), typeof(GridDemoModule), new PropertyMetadata(null));
        }

        public ICommand SetItemsSourceCommand { get; private set; }

        public GridDemoModule() {
            SetItemsSourceCommand = DevExpress.Mvvm.Native.DelegateCommandFactory.Create<object>(
                new Action<object>((parameter) => SetItemsSource(parameter)),
                new Func<object, bool>((parameter) => GridControl != null), false);
        }
        void SetItemsSource(object source) {
            GridControl.ItemsSource = source;
        }

        protected virtual bool IsGridBorderVisible { get { return false; } }
        public bool UseGridControlWrapperAsDataContext { get; set; }
        public GridControl GridControl {
            get { return (GridControl)GetValue(GridControlProperty); }
            set { SetValue(GridControlProperty, value); }
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            if(GridControl == null) {
                GridControl = FindGrid();
                if(GridControl != null)
                    GridControl.ShowBorder = IsGridBorderVisible;
            }
        }
        protected override object GetModuleDataContext() {
            if(UseGridControlWrapperAsDataContext)
                return new GridControlWrapper(GridControl);
            return GridControl;
        }
        public virtual GridControl FindGrid() {
            return (GridControl)DemoModuleControl.FindDemoContent(typeof(GridControl), (DependencyObject)DemoModuleControl.Content);
        }
        protected override void RaiseIsPopupContentInvisibleChanged(DependencyPropertyChangedEventArgs e) {
            base.RaiseIsPopupContentInvisibleChanged(e);
            if(IsPopupContentInvisible && GridControl != null)
                GridControl.View.HideColumnChooser();
        }
        protected override bool CanLeave() {
            if(GridControl == null)
                return true;
            GridControl.View.CommitEditing();
            return true;
        }
        #region Printing And Exporting

        protected virtual bool AllowDataAwareExport { get { return true; } }
        protected virtual bool AllowWYSIWYGExport { get { return true; } }

        public sealed override bool SupportSidebarContent() {
            return true;
        }
        public sealed override bool SupportSidebar2Content() {
            return true;
        }
        public sealed override bool IsSidebarButtonEnabled() {
            return GetExportView() != null;
        }
        protected virtual DataViewBase GetExportView() {
            GridControl grid = FindGrid();
            if(grid != null && grid.View is IPrintableControl)
                return grid.View;
            return null;
        }
        public override object GetSidebarContent() {
            DataViewBase exportView = GetExportView();
            return new DemoModuleExportControl(exportView, AllowDataAwareExport && exportView is TableView, AllowWYSIWYGExport, GetReportView() != null);
        }
        public override System.Windows.Media.ImageSource GetSidebarIcon() {
            return new BitmapImage(new Uri(@"/GridDemo;component/Images/PrintIcon.png", UriKind.Relative));
        }
        public override System.Windows.Media.ImageSource GetSidebarIconSelected() {
            return new BitmapImage(new Uri(@"/GridDemo;component/Images/PrintIconSelected.png", UriKind.Relative));
        }
        public override string GetSidebarTag() {
            return "Export";
        }
        #endregion
        #region Reports
        protected virtual IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            GridControl grid = FindGrid();
            if(grid != null && grid.View is IGridViewFactory<ColumnWrapper, RowBaseWrapper>)
                return grid.View as IGridViewFactory<ColumnWrapper, RowBaseWrapper>;
            return null;
        }
        public override bool IsSidebar2ButtonEnabled() {
            return GetReportView() != null;
        }
        public override object GetSidebar2Content() {
            var reportView = GetReportView();
            if(reportView != null)
                return new ReportsSideBarControl(GetType().Name, reportView);
            return null;
        }
        public override System.Windows.Media.ImageSource GetSidebar2Icon() {
            return new BitmapImage(new Uri(@"/GridDemo;component/Images/ReportIcon.png", UriKind.Relative));
        }
        public override System.Windows.Media.ImageSource GetSidebar2IconSelected() {
            return new BitmapImage(new Uri(@"/GridDemo;component/Images/ReportIconSelected.png", UriKind.Relative));
        }
        public override string GetSidebar2Tag() {
            return "Reports";
        }
        #endregion
    }
    public class GridControlWrapper : System.ComponentModel.INotifyPropertyChanged {
        GridControl grid;
        public GridControl GridControl {
            get {
                return grid;
            }
            set {
                if(grid == value)
                    return;
                grid = value;
                OnPropertyChanged("GridControl");
            }
        }
        public GridControlWrapper(GridControl gridControl) {
            GridControl = gridControl;
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(String propertyName) {
            if((this.PropertyChanged != null)) {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class PrintViewGridDemoModule : GridDemoModule {
        public static LinkPreviewModel CreateLinkPreviewModel(IPrintableControl printableControl) {
            PrintableControlLink link = new PrintableControlLink(printableControl as IPrintableControl);
            return new LinkPreviewModel(link);
        }
        protected virtual DXTabControl DXTabControl { get { return null; } }
        public override GridControl FindGrid() {
            return DXTabControl != null ? (GridControl)((DXTabItem)DXTabControl.Items[0]).Content : null;
        }
        public void ShowPrintPreview() {
            Window owner = LayoutHelper.FindParentObject<Window>(this);
            DevExpress.Xpf.Grid.Printing.PrintHelper.ShowPrintPreviewDialog(owner, (IPrintableControl)GridControl.View, "Grid Document");
        }
        public void ShowPrintPreviewInNewTab(GridControl grid, DXTabControl tabControl, string tabName) {
            PrintableControlLink link = new PrintableControlLink((IPrintableControl)grid.View);
            DocumentPreviewControl preview = new DocumentPreviewControl() { DocumentSource = link };

            DXTabItem tabItem = new DXTabItem() { AllowHide = DefaultBoolean.True, Content = preview, Header = tabName };
            tabItem.Tag = link;
            tabControl.Items.Add(tabItem);
            tabControl.SelectedItem = tabItem;

            link.CreateDocument(true);
        }
        protected void DisposePrintPreviewTabContent(DXTabItem tabItem) {
            if(tabItem.Tag != null)
                ((PrintableControlLink)tabItem.Tag).Dispose();
            DXTabControl.Items.Remove(tabItem);
        }
        protected override void Clear() {
            base.Clear();
            for(int i = DXTabControl.Items.Count - 1; i >= 1; i--) {
                DisposePrintPreviewTabContent((DXTabItem)DXTabControl.Items[i]);
            }
        }
        protected void newWindowButton_Click(object sender, RoutedEventArgs e) {
            ShowPrintPreview();
        }
        protected virtual void ShowPreviewInNewTab() { }
    }
    public class CountryToFlagImageConverter : BytesToImageSourceConverter {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            foreach(Country item in CountriesData.DataSource) {
                if((item.Name == (string)value || item.NWindName == (string)value))
                    return base.Convert(item.Flag, targetType, parameter, culture);
            }
            return null;
        }
    }
}
namespace CommonDemo {
    public class CommonDemoModule : GridDemo.GridDemoModule {
    }
}
