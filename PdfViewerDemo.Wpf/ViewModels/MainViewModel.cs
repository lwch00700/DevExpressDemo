using DevExpress.Mvvm;
using System;
using System.IO;
using System.Windows;

namespace PdfViewerDemo {
    public class MainViewModel {
        readonly string message = "This document contains content that is not supported by the DevExpress PDF Viewer:"
                    + Environment.NewLine
                    + Environment.NewLine
                    + "- JPEG2000 images" + Environment.NewLine
                    + "- Separation, DeviceN and ICC-based color spaces" + Environment.NewLine
                    + "- Type 3 fonts";
        bool isNewDocument;
        public virtual Stream PdfStream { get; set; }
        public virtual IMessageBoxService MessageBoxService { get { throw new NotImplementedException(); } }
        public void ShowFunctionalLimitsMessage() {
            if(!isNewDocument)
                return;
            isNewDocument = false;
            MessageBoxService.Show(message, "Important", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }
        public void ShowNewDocument() {
            isNewDocument = true;
        }
    }
}
