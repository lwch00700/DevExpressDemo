using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Navigation;
using DevExpress.Mvvm.UI.Interactivity;

namespace GridDemo {
    public class HyperLinkAttachedBehavior : Behavior<Hyperlink> {
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.RequestNavigate += OnRequestNavigate;
        }
        protected override void OnDetaching() {
            AssociatedObject.RequestNavigate -= OnRequestNavigate;
            base.OnDetaching();
        }
        private void OnRequestNavigate(object sender, RequestNavigateEventArgs e) {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
