using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Reports.UserDesigner;
using DevExpress.XtraExport;
using DevExpress.XtraReports.UI;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.Xpf.Printing;

namespace GridDemo {
    static class ReportDesignerHelper {
        public static void ShowDesigner(IGridViewFactory<ColumnWrapper, RowBaseWrapper> owner) {
            XtraReport report = new XtraReport();
            ReportGenerationExtensions<ColumnWrapper, RowBaseWrapper>.Generate(report, owner);
            var reportDesigner = new ReportDesigner();
            reportDesigner.Loaded += (s, e) => {
                reportDesigner.OpenDocument(report);
            };
            reportDesigner.ShowWindow(owner as FrameworkElement);
        }
        public static void ShowDesignerPrintPreview(XtraReport report) {
            DocumentPreviewControl preview = new DocumentPreviewControl();
            preview.DocumentSource = report;
            Window w = new Window();
            w.Content = preview;
            report.CreateDocument(true);
            w.ShowDialog();
        }
    }
}
