using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors;
using System.ComponentModel;
using DevExpress.Xpf.Editors.Validation;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System.Windows.Interop;
using System.Windows.Threading;

namespace EditorsDemo {
    public partial class DXWindowBorderHighlightingEffects : EditorsDemoModule {
        public DXWindowBorderHighlightingEffects() {
            InitializeComponent();
        }

        protected override void RaiseIsPopupContentInvisibleChanged(DependencyPropertyChangedEventArgs e) {
            base.RaiseIsPopupContentInvisibleChanged(e);
            if(IsPopupContentInvisible)
                if(window != null) window.Close();
        }

        Size desiredWindowSize = new Size(400, 200);
        protected Rect GetWindowSuggestedRect(Rect parentRect) {
            return new Rect(parentRect.Left + parentRect.Width / 2d - desiredWindowSize.Width, parentRect.Top + parentRect.Height / 2d - desiredWindowSize.Height, desiredWindowSize.Width, desiredWindowSize.Height);
        }
        DXWindow window;
        Window rootWindow;

        void SetBorderEffectCustomColors() {
            window.BorderEffectActiveColor = new SolidColorBrush(activeColor.Color);
            window.BorderEffectInactiveColor = new SolidColorBrush(inactiveColor.Color);
        }

        void ShowWindow() {
            if(window != null) window.Close();
            window = new DXWindow();

            if(enableEffect.IsChecked.HasValue) window.BorderEffect = enableEffect.IsChecked.Value ? BorderEffect.Default : BorderEffect.None;
            if(enableCustomization.IsChecked.Value) SetBorderEffectCustomColors();
            rootWindow = LayoutHelper.GetRoot(this) as Window;
            if(rootWindow != null) {
                window.SetBounds(GetWindowSuggestedRect(rootWindow.GetBounds()));
                window.Icon = rootWindow.Icon;
                rootWindow.Closed += rootWindow_Closed;
                window.Owner = rootWindow;
            }
            window.Title = "DXWindow";
            window.Topmost = true;
            window.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            ShowWindow();
        }

        void rootWindow_Closed(object sender, EventArgs e) {
            if(window != null) window.Close();
        }

        private void enableCustomization_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            if(window == null) return;
            if((bool)e.NewValue) SetBorderEffectCustomColors();
            else window.BorderEffectReset();
        }

        private void enableEffect_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            if(window == null) return;
            if(enableCustomization.IsChecked.Value) SetBorderEffectCustomColors();
            if((bool)e.NewValue) window.BorderEffect = BorderEffect.Default;
            else {
                window.BorderEffectReset();
                window.BorderEffect = BorderEffect.None;
            }

        }

        private void activeColor_ColorChanged(object sender, RoutedEventArgs e) {
            if(window != null) window.BorderEffectActiveColor = new SolidColorBrush(activeColor.Color);
        }

        private void inactiveColor_ColorChanged(object sender, RoutedEventArgs e) {
            if(window != null) window.BorderEffectInactiveColor = new SolidColorBrush(inactiveColor.Color);
        }
    }
}
