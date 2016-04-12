using System.Windows;
using System.Windows.Controls;

namespace GaugesDemo {
    public class ButtonExt : Button {
        public bool IsPressedExt {
            get { return (bool)GetValue(IsPressedExtProperty); }
            set { SetValue(IsPressedExtProperty, value); }
        }
        public static readonly DependencyProperty IsPressedExtProperty =
            DependencyProperty.Register("IsPressedExt", typeof(bool), typeof(ButtonExt), new PropertyMetadata(false));
        protected override void OnIsPressedChanged(DependencyPropertyChangedEventArgs e) {
            base.OnIsPressedChanged(e);
            IsPressedExt = IsPressed;
        }
    }
}
