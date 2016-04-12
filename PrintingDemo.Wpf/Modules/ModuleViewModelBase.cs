using System;
using System.ComponentModel;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Xpf.Printing;

namespace PrintingDemo {
    public abstract class ModuleViewModelBase : ViewModelBase {
        TemplatedLink linkCore;

        public DataTemplate PageHeaderTemplate { get; set; }
        public DataTemplate ReportHeaderTemplate { get; set; }
        public DataTemplate DetailTemplate { get; set; }
        public DataTemplate ReportFooterTemplate { get; set; }
        public DataTemplate PageFooterTemplate { get; set; }

        public TemplatedLink Link {
            get { return linkCore; }
            private set { SetProperty(ref linkCore, value, () => Link); }
        }

        public void CreateDocument() {
            if(Link == null) {
                Link = CreateLink();
                Link.PrintingSystem.PageSettingsChanged += OnPageSettingsChanged;
            }
            ProcessLink(Link);
            Link.CreateDocument(true);
        }

        protected virtual void ProcessLink(TemplatedLink link) { }

        public void ClearDocument() {
            if(Link != null) {
                Link.StopPageBuilding();
                Link.Dispose();
                linkCore = null;
            }
        }

        protected void OnPageSettingsChanged(object sender, EventArgs e) {
            Link.PaperKind = Link.PrintingSystem.PageSettings.PaperKind;
            Link.Margins = Link.PrintingSystem.PageSettings.Margins;
            Link.Landscape = Link.PrintingSystem.PageSettings.Landscape;
        }

        protected abstract TemplatedLink CreateLink();
    }
}
