using System;
using System.Windows;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Utils;


namespace BarsDemo {
    public class BarsDemoModule : DemoModule {
        public static readonly DependencyProperty BarManagerProperty =
            DependencyPropertyManager.Register("BarManager", typeof(BarManager), typeof(BarsDemoModule), new FrameworkPropertyMetadata(null));
        public BarManager Manager {
            get { return (BarManager)GetValue(BarManagerProperty); }
            set { SetValue(BarManagerProperty, value); }
        }
        static BarsDemoModule() {
            BarNameScope.IsScopeOwnerProperty.OverrideMetadata(typeof(BarsDemoModule), new FrameworkPropertyMetadata(true));
        }
        public BarsDemoModule() {

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }
        protected virtual void OnLoaded(object sender, RoutedEventArgs e) {
            VisualTreeEnumerator en = new VisualTreeEnumerator(this);
            while(en.MoveNext()) {
                BarManager _manager = en.Current as BarManager;
                if(_manager != null) {
                    DemoModuleControl.DemoContent = _manager;
                    Manager = _manager;
                    break;
                }
            }
        }
        protected virtual void OnUnloaded(object sender, RoutedEventArgs e) { }
        protected override bool CanLeave() {
            return true;
        }
    }
}
