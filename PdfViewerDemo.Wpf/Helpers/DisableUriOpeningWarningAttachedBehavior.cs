using System.Windows;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.PdfViewer;

namespace PdfViewerDemo {
    public class DisableUriOpeningWarningAttachedBehavior : Behavior<PdfViewerControl> {
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.UriOpening += AssociatedObject_UriOpening;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.UriOpening -= AssociatedObject_UriOpening;
        }
        void AssociatedObject_UriOpening(DependencyObject d, UriOpeningEventArgs e) {
            e.Handled = true;
        }
    }
}
