using System;
using System.Windows;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Utils;
using DevExpress.Xpf.Ribbon;
using System.Windows.Media;
using DevExpress.Xpf.Core;
using DevExpress.Mvvm;
using System.Linq;

namespace RibbonDemo {
    public class RibbonDemoModule : DemoModule {
        public static readonly DependencyProperty BarManagerProperty;
        public static readonly DependencyProperty RibbonProperty;

        static RibbonDemoModule() {
            Type ownerType = typeof(RibbonDemoModule);
            BarManagerProperty = DependencyPropertyManager.Register("BarManager", typeof(BarManager), ownerType, new FrameworkPropertyMetadata(null));
            RibbonProperty = DependencyPropertyManager.Register("Ribbon", typeof(RibbonControl), ownerType, new FrameworkPropertyMetadata(null));
        }
        public BarManager Manager {
            get { return (BarManager)GetValue(BarManagerProperty); }
            set { SetValue(BarManagerProperty, value); }
        }
        public RibbonControl Ribbon {
            get { return (RibbonControl)GetValue(RibbonProperty); }
            set { SetValue(RibbonProperty, value); }
        }
        public RibbonDemoModule() {
            Loaded += OnLoaded;
        }

        protected virtual bool NeedChangeEditorsTheme { get { return false; } }
        protected virtual void OnLoaded(object sender, RoutedEventArgs e) {
            Manager = (BarManager)DemoModuleControl.FindDemoContent(typeof(BarManager), this);
            Ribbon = (RibbonControl)DemoModuleControl.FindDemoContent(typeof(RibbonControl), this);
            DemoModuleControl.DemoContent = Manager;
        }
        protected override bool CanLeave() {
            return true;
        }
        protected override void RaiseIsPopupContentInvisibleChanged(DependencyPropertyChangedEventArgs e) {
            base.RaiseIsPopupContentInvisibleChanged(e);
            bool newValue = (bool)e.NewValue;
            if(Ribbon != null)
                Ribbon.AllowKeyTips = !newValue;
        }
    }
}
