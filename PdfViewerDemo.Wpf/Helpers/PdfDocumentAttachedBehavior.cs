using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.PdfViewer;
using DevExpress.Xpf.PdfViewer.Helpers;

namespace PdfViewerDemo {
    public class PdfDocumentAttachedBehavior : Behavior<PdfViewerControl> {
        MainViewModel Model { get { return AssociatedObject.DataContext as MainViewModel; } }
        protected override void OnAttached() {
            base.OnAttached();
            PdfViewerHelper.AddFunctionalLimitsOccuredHandler(AssociatedObject, FunctionalLimitsOccuresHandler);
        }
        void FunctionalLimitsOccuresHandler() {
            if(Model == null)
                return;
            Model.ShowFunctionalLimitsMessage();
        }
    }
}
