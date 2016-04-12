using System;
using System.IO;
using System.Reflection;
using DevExpress.XtraRichEdit.Internal;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Services;
using DevExpress.Office.Services;

namespace RichEditDemo {
    public partial class LoadSaveHtml : RichEditDemoModule {
        public LoadSaveHtml() {
            InitializeComponent();
            IDocumentImportManagerService service = richEdit.GetService<IDocumentImportManagerService>();
            if (service != null) {
                service.UnregisterImporter(service.GetImporter(DocumentFormat.PlainText));
                service.UnregisterImporter(service.GetImporter(DocumentFormat.Rtf));
                service.UnregisterImporter(service.GetImporter(DocumentFormat.Mht));
                service.UnregisterImporter(service.GetImporter(DocumentFormat.OpenXml));
                service.UnregisterImporter(service.GetImporter(DocumentFormat.WordML));
                service.UnregisterImporter(service.GetImporter(DocumentFormat.OpenDocument));
            }


            HtmlLoadHelper.Load("html_sample.htm", richEdit);
        }

        void richEdit_DocumentLoaded(object sender, EventArgs e) {
            richEdit.HorizontalScrollBarVisibility = System.Windows.Visibility.Collapsed;
            try {
                string fileName = richEdit.Options.DocumentSaveOptions.CurrentFileName;
                if (!String.IsNullOrEmpty(fileName)) {
                    webBrowser.Source = new Uri("file://" + fileName);
                    using (StreamReader reader = new StreamReader(fileName)) {
                        edtPlainHtml.Text = reader.ReadToEnd();
                    }
                }
            }
            catch {
            }
        }

        void richEdit_EmptyDocumentCreated(object sender, EventArgs e) {
            webBrowser.Source = null;
            edtPlainHtml.Text = String.Empty;
        }
        protected override void RaiseModuleAppear() {
            base.RaiseModuleAppear();
            richEdit_DocumentLoaded(this, EventArgs.Empty);
        }
        protected override bool CanLeave() {
            webBrowser.Source = null;
            return true;
        }

        protected override void RaiseBeforeModuleAppear() {
            base.RaiseBeforeModuleAppear();
            string fileName = richEdit.Options.DocumentSaveOptions.CurrentFileName;
            if (!String.IsNullOrEmpty(fileName)) {
                webBrowser.Source = new Uri("file://" + fileName);
            }
        }

    }

}
