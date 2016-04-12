using System;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Core;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Interop;

namespace DevExpress.Xpf.LayoutControlDemo {
    public interface IHyperlink : IControl {
        bool IsActive { get; set; }
        string NavigateUri { get; set; }
    }

    [TemplateVisualState(Name = "Active", GroupName = "CommonStates")]
    [TemplatePart(Name = Hyperlink.HeaderControlName, Type = typeof(TextBlock))]
    public class Hyperlink : DXButton, IHyperlink {
        #region Dependency Properties

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(Hyperlink), null);
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(Hyperlink),
                new PropertyMetadata(new PropertyChangedCallback(OnIsActiveChanged)));
        public static readonly DependencyProperty NavigateUriProperty =
            DependencyProperty.Register("NavigateUri", typeof(string), typeof(Hyperlink),
                new PropertyMetadata(new PropertyChangedCallback(OnNavigateUriChanged)));
        public static readonly DependencyProperty SubheaderProperty =
            DependencyProperty.Register("Subheader", typeof(string), typeof(Hyperlink), null);
        public static readonly DependencyProperty SubheaderForegroundProperty =
            DependencyProperty.Register("SubheaderForeground", typeof(Brush), typeof(Hyperlink), null);

        static void OnIsActiveChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ((Hyperlink)o).OnIsActiveChanged();
        }
        static void OnNavigateUriChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ((Hyperlink)o).OnNavigateUriChanged();
        }

        #endregion Dependency Properties

        public Hyperlink() {
            DefaultStyleKey = typeof(Hyperlink);
        }

        public new HyperlinkController Controller { get { return (HyperlinkController)base.Controller; } }
        public string Header {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public bool IsActive {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        public string NavigateUri {
            get { return (string)GetValue(NavigateUriProperty); }
            set { SetValue(NavigateUriProperty, value); }
        }
        public string Subheader {
            get { return (string)GetValue(SubheaderProperty); }
            set { SetValue(SubheaderProperty, value); }
        }
        public Brush SubheaderForeground {
            get { return (Brush)GetValue(SubheaderForegroundProperty); }
            set { SetValue(SubheaderForegroundProperty, value); }
        }

        protected override ControlControllerBase CreateController() {
            return new HyperlinkController(this);
        }

        #region Template

        internal const string HeaderControlName = "HeaderControl";

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            HeaderControl = GetTemplateChild(HeaderControlName) as TextBlock;
            UpdateTemplateHyperlink();
        }

        protected void UpdateTemplateHyperlink() {
            if (!BrowserInteropHelper.IsBrowserHosted || string.IsNullOrEmpty(NavigateUri) || HeaderControl == null)
                return;
            var _hyperlink = new System.Windows.Documents.Hyperlink();
            _hyperlink.Inlines.Add(new System.Windows.Documents.Run(HeaderControl.Text));
            _hyperlink.NavigateUri = new Uri(NavigateUri);
            _hyperlink.TargetName = "_blank";
            _hyperlink.Foreground = HeaderControl.Foreground;
            _hyperlink.TextDecorations = null;
            HeaderControl.Inlines.Clear();
            HeaderControl.Inlines.Add(_hyperlink);
        }

        protected TextBlock HeaderControl { get; private set; }

        #endregion Template

        protected virtual void OnIsActiveChanged() {
            Controller.OnIsActiveChanged();
        }
        protected virtual void OnNavigateUriChanged() {
            UpdateTemplateHyperlink();
        }
    }

    public class HyperlinkController : DXButtonController {
        public HyperlinkController(IHyperlink control)  : base(control) {
        }

        public override void UpdateState(bool useTransitions) {
            if(IHyperlink.IsActive)
                VisualStateManager.GoToState(Control, "Active", useTransitions);
            else
                base.UpdateState(useTransitions);
        }

        public IHyperlink IHyperlink { get { return IControl as IHyperlink; } }

        protected void NavigateToUri() {
            Process.Start(IHyperlink.NavigateUri);
        }
        protected override void OnClick() {
            if (string.IsNullOrEmpty(IHyperlink.NavigateUri))
                base.OnClick();
            else
                if (!BrowserInteropHelper.IsBrowserHosted)
                    NavigateToUri();
        }
        protected internal virtual void OnIsActiveChanged() {
            UpdateState(false);
        }
    }
}
