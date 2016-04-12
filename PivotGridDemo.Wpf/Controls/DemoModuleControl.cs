using System;
using System.Windows;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.Utils.Themes;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Core;
using DependencyPropertyManager = System.Windows.DependencyProperty;

namespace PivotGridDemo.PivotGrid {
    public class PivotGridDemoModule : DemoModule {
        WeakReference Pivot = null;
        public static readonly DependencyProperty PivotGridControlProperty;

        static PivotGridDemoModule() {
            Type ownerType = typeof(PivotGridDemoModule);
            PivotGridControlProperty = DependencyPropertyManager.Register("PivotGridControl", typeof(PivotGridControl),
                ownerType, new PropertyMetadata(null, OnPivotGridControlChanged));
        }

        public static void OnPivotGridControlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if(e.NewValue == null)
                return;
            ((PivotGridDemoModule)d).Pivot = new WeakReference(e.NewValue);
    }

        public PivotGridDemoModule() {
            Loaded += delegate(object sender, RoutedEventArgs e) {
                OnLoaded();
            };
        }

        protected override void RaiseIsPopupContentInvisibleChanged(System.Windows.DependencyPropertyChangedEventArgs e) {
            base.RaiseIsPopupContentInvisibleChanged(e);
            EnsurePivot();
            if(Pivot == null)
                return;
            PivotGridControl lastPivot = Pivot.Target as PivotGridControl;
            if(IsPopupContentInvisible && lastPivot != null)
                lastPivot.IsFieldListVisible = false;
        }

        public PivotGridControl PivotGridControl {
            get { return (PivotGridControl)GetValue(PivotGridControlProperty); }
            set { SetValue(PivotGridControlProperty, value); }
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            EnsurePivot();
        }

        void EnsurePivot() {
            if(PivotGridControl != null)
                return;
            DemoModuleControl.DemoContent = DemoModuleControl.FindDemoContent(typeof(PivotGridControl), (DependencyObject)Content);
            if(DemoModuleControl.DemoContent as FrameworkElement == null)
                return;
            PivotGridControl = LayoutHelper.FindElement(((FrameworkElement)DemoModuleControl.DemoContent), IsPivot) as PivotGridControl;
        }

        bool IsPivot(FrameworkElement element) {
            return element is PivotGridControl;
        }

        protected virtual void OnLoaded() {
            if(PivotGridControl != null && object.Equals(PivotGridControl.RowTotalsLocation, FieldRowTotalsLocation.Tree))
                PivotGridControl.BestFit(FieldArea.RowArea, true, false);
        }
        protected override object GetModuleDataContext() {
            return DemoModuleControl.FindDemoContent(typeof(PivotGridControl), (DependencyObject)Content);
        }
        protected virtual bool NeedChangeEditorsTheme { get { return false; } }
        protected override bool CanLeave() {
            return true;
        }
        protected override void Clear() {
            PivotGridControl = null;
        }

        public void ShowPrintPreview(PivotGridControl pivotGrid) {
            Window owner = LayoutHelper.FindParentObject<Window>(this);
            pivotGrid.ShowPrintPreview(owner);
        }
    }

}
