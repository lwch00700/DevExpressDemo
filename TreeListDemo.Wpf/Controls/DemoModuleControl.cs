using System;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;
using DevExpress.Utils;
using DevExpress.Xpf.Printing;
using DevExpress.Mvvm;
using System.Windows.Input;

using DevExpress.Xpf.Core.Native;
using GridDemo;
using System.Windows.Media.Imaging;

namespace TreeListDemo {
    public class TreeListDemoModule : DemoModule {
        public static readonly DependencyProperty TreeListControlProperty;
        static TreeListDemoModule() {
            TreeListControlProperty = DependencyProperty.Register("TreeListControl", typeof(TreeListControl), typeof(TreeListDemoModule), new PropertyMetadata(null));
        }
        public TreeListDemoModule() {
            ThemeManager.AddThemeChangedHandler(this, ThemeNameChanged);
        }

        void ThemeNameChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e) {
        }
        protected override void Clear() {
            base.Clear();
            ThemeManager.RemoveThemeChangedHandler(this, ThemeNameChanged);
        }
        public TreeListControl TreeListControl {
            get { return (TreeListControl)GetValue(TreeListControlProperty); }
            set { SetValue(TreeListControlProperty, value); }
        }
        protected override object GetModuleDataContext() {
            if(TreeListControl == null) {
                TreeListControl = FindTreeList();
                if(TreeListControl != null)
                    TreeListControl.ShowBorder = ShowBorder;
            }
            return TreeListControl;
        }
        protected virtual bool ShowBorder { get { return false; } }
        protected virtual TreeListControl FindTreeList() {
            return (TreeListControl)DemoModuleControl.FindDemoContent(typeof(TreeListControl), (DependencyObject)DemoModuleControl.Content);
        }
        public sealed override bool SupportSidebarContent() {
            return true;
        }
        public sealed override bool IsSidebarButtonEnabled() {
            return GetExportView() != null;
        }
        protected virtual TreeListView GetExportView() {
            var treeList = FindTreeList();
            if(treeList != null)
                return treeList.View;
            return null;
        }
        public override object GetSidebarContent() {
            return new DemoModuleExportControl(GetExportView(), false);
        }
        public override System.Windows.Media.ImageSource GetSidebarIcon() {
            return new BitmapImage(new Uri(@"/TreeListDemo;component/Images/PrintIcon.png", UriKind.Relative));
        }
        public override System.Windows.Media.ImageSource GetSidebarIconSelected() {
            return new BitmapImage(new Uri(@"/TreeListDemo;component/Images/PrintIconSelected.png", UriKind.Relative));
        }
        public override string GetSidebarTag() {
            return "Export";
        }
    }

    public class PrintTreeListDemoModule : TreeListDemoModule {
        DXTabControl dxTabControlCore;
        public PrintTreeListDemoModule() {
            ShowPrintPreview = new DelegateCommand<object>(p => { OnShowPrintPreview((string)p); });
            ShowPrintPreviewInNewTab = new DelegateCommand<object>(p => { OnShowPrintPreviewInNewTab((string)p); });
        }
        public ICommand ShowPrintPreview { get; private set; }
        public ICommand ShowPrintPreviewInNewTab { get; private set; }
        public DXTabControl DXTabControl {
            get { return dxTabControlCore; }
            set {
                if(DXTabControl == value)
                    return;
                if(DXTabControl != null)
                    DXTabControl.TabHidden -= new TabControlTabHiddenEventHandler(OnTabHidden);
                dxTabControlCore = value;
                if(DXTabControl != null)
                    DXTabControl.TabHidden += new TabControlTabHiddenEventHandler(OnTabHidden);
            }
        }
        protected TreeListView View { get { return TreeListControl.View; } }
        protected override TreeListControl FindTreeList() {
            if(DXTabControl == null) return null;
            return (TreeListControl)((DXTabItem)DXTabControl.Items[0]).Content;
        }
        protected virtual void OnShowPrintPreview(string documentName) {
            PrintHelper.ShowPrintPreviewDialog(LayoutHelper.FindParentObject<Window>(this), (IPrintableControl)TreeListControl.View, documentName);
        }
        protected virtual void OnShowPrintPreviewInNewTab(string documentName) {
            PrintableControlLink link = new PrintableControlLink((IPrintableControl)TreeListControl.View);
            DocumentPreviewControl preview = new DocumentPreviewControl() { DocumentSource = link };
            DXTabItem tabItem = CreateTabItem(preview, documentName);
            tabItem.Tag = link;
            DXTabControl.Items.Add(tabItem);
            DXTabControl.SelectedItem = tabItem;
            link.CreateDocument(true);
        }
        protected virtual DXTabItem CreateTabItem(DocumentPreviewControl preview, string documentName) {
            return new DXTabItem() { AllowHide = DefaultBoolean.True, Content = preview, Header = documentName };
        }
        protected void RemoveTab(DXTabItem tabItem) {
            if(tabItem.Tag != null)
                ((PrintableControlLink)tabItem.Tag).Dispose();
            tabItem.Content = null;
            DXTabControl.Items.Remove(tabItem);
        }
        protected override void Clear() {
            base.Clear();
            for(int i = DXTabControl.Items.Count - 1; i >= 0; i--) {
                RemoveTab((DXTabItem)DXTabControl.Items[i]);
            }
            DXTabControl = null;
        }
        void OnTabHidden(object sender, TabControlTabHiddenEventArgs e) {
            RemoveTab((DXTabItem)DXTabControl.Items[e.TabIndex]);
        }
    }
}
