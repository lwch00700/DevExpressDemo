using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI.Interactivity;
using System.Windows;
using System.Windows.Input;

namespace LayoutControlDemo.Helpers {
    public class MouseWheelScrollingBehavior : Behavior<FrameworkElement> {
        public static readonly DependencyProperty DisableScrollingProperty = DependencyProperty.Register("DisableScrolling", typeof(bool), typeof(MouseWheelScrollingBehavior),
            new PropertyMetadata(false, (d, e) => ((MouseWheelScrollingBehavior)d).Update((bool)e.NewValue)));
        public bool DisableScrolling { get { return (bool)GetValue(DisableScrollingProperty); } set { SetValue(DisableScrollingProperty, value); } }
        protected override void OnAttached() {
            base.OnAttached();
            Update(DisableScrolling);
        }
        protected override void OnDetaching() {
            Update(true);
            base.OnDetaching();
        }
        void Update(bool disableScrolling) {
            AssociatedObject.Do(x => x.PreviewMouseWheel -= OnPreviewMouseWheel);
            if(DisableScrolling)
                AssociatedObject.Do(x => x.PreviewMouseWheel += OnPreviewMouseWheel);
        }
        void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            e.Handled = true;
        }
    }
}
