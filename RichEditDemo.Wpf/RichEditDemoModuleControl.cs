using System;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.RichEdit;
using DevExpress.Xpf.Utils;


namespace RichEditDemo {
    public class RichEditDemoModule : DemoModule {
        public static readonly DependencyProperty RichEditControlProperty;

        static RichEditDemoModule() {
            RichEditControlProperty = DependencyPropertyManager.Register("RichEditControl", typeof(RichEditControl), typeof(RichEditDemoModule), new FrameworkPropertyMetadata(null));
        }

        public RichEditControl RichEditControl {
            get { return (RichEditControl)GetValue(RichEditControlProperty); }
            set { SetValue(RichEditControlProperty, value); }
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            if (RichEditControl != null) {
                RichEditControl.Loaded -= OnRichEditControlLoaded;
                RichEditControl = null;
            }
            ObtainRichEditControl();
        }

        void OnRichEditControlLoaded(object sender, RoutedEventArgs e) {
            SetFocus(RichEditControl);
        }
        protected override void RaiseModuleAppear() {
            base.RaiseModuleAppear();
            ObtainRichEditControl();
            SetFocus(RichEditControl);
        }

        protected override object GetModuleDataContext() {
            return FindRichEdit();
        }
        void ObtainRichEditControl() {
            if (RichEditControl == null)
                RichEditControl = FindRichEdit();
            if (RichEditControl != null) {
                RichEditControl.Loaded += OnRichEditControlLoaded;
                new RichEditDemoExceptionsHandler(RichEditControl).Install();
            }
        }
        RichEditControl FindRichEdit() {
            return (RichEditControl)DemoModuleControl.FindDemoContent(typeof(RichEditControl), (DependencyObject)Content);
        }
        protected internal virtual void SetFocus(RichEditControl control) {
            if (control == null)
                return;
            if (control.KeyCodeConverter != null)
                control.KeyCodeConverter.Focus();
        }
        protected override void RaiseIsPopupContentInvisibleChanged(DependencyPropertyChangedEventArgs e) {
            base.RaiseIsPopupContentInvisibleChanged(e);
            if (RichEditControl != null)
                RichEditControl.ShowHoverMenu = !IsPopupContentInvisible;
        }

        protected override string XamlSuffix { get { return ".xaml"; } }
    }
}
