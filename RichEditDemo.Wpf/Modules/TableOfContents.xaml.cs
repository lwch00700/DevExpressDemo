using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase;
using DevExpress.XtraRichEdit.Services;
using DevExpress.Office.Services;

using System.Data;
using DevExpress.DemoData.Models;
using DevExpress.DemoData;

namespace RichEditDemo {
    public partial class TableOfContents : RichEditDemoModule {
        NWindDataLoader NWind { get; set; }
        object ds;
        bool isFirstLoad;

        public TableOfContents() {
            InitializeComponent();
            ribbon.SelectedPage = pageReferences;
            isFirstLoad = true;

            OpenXmlLoadHelper.Load("TableOfContents.docx", sourceRichEditControl);
            NWind = Resources["NWindDataLoader"] as NWindDataLoader;
            this.ds = NWindContext.Create().Employees.ToList();

            IUriStreamService uriService = (IUriStreamService)sourceRichEditControl.GetService(typeof(IUriStreamService));
            uriService.RegisterProvider(new DBUriStreamProviderBase<DevExpress.DemoData.Models.Employee>(NWindContext.Create().Employees.ToList(),
                (es, id) => es.First(e => e.EmployeeID == id).Photo));
        }

        private void sourceRichEditControl_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            SetFocus(sourceRichEditControl);
            if (isFirstLoad) {
                sourceRichEditControl.ApplyTemplate();
                sourceRichEditControl.Options.MailMerge.DataSource = ds;
                sourceRichEditControl.Options.MailMerge.ViewMergedData = true;

                MergeToNewDocument();
                isFirstLoad = false;
                targetRichEditControl.Document.CaretPosition = targetRichEditControl.Document.CreatePosition(0);
            }
        }
        private void mergeToNewDocument_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            MergeToNewDocument();
        }
        private void MergeToNewDocument() {
            targetRichEditControl.ApplyTemplate();

            MailMergeOptions options = sourceRichEditControl.CreateMailMergeOptions();
            options.MergeMode = MergeMode.NewSection;
            options.LastRecordIndex = 5;
            sourceRichEditControl.MailMerge(options, targetRichEditControl.Document);
            Document targetDocument = targetRichEditControl.Document;

            InsertContentHeading(targetDocument);

            Field field = targetDocument.Fields.Create(targetDocument.Paragraphs[1].Range.Start, "TOC \\h");
            targetDocument.InsertSection(field.Range.End);
            field.Update();

            ParagraphStyle paragraphStyle = targetDocument.ParagraphStyles["TOC 1"];
            if (paragraphStyle != null)
                paragraphStyle.Bold = true;

            resultingDocumentTabItem.IsEnabled = true;
            tabControl.SelectedItem = resultingDocumentTabItem;
        }
        private void InsertContentHeading(Document targetDocument) {
            DocumentRange hRange = targetDocument.InsertText(targetDocument.Range.Start, "Contents\r\n");
            CharacterProperties charProperties = targetDocument.BeginUpdateCharacters(hRange);
            charProperties.FontSize = 16;
            charProperties.ForeColor = DXColor.Blue;
            targetDocument.EndUpdateCharacters(charProperties);
            targetDocument.Paragraphs[0].Style = targetDocument.ParagraphStyles[0];
            targetDocument.Paragraphs[0].Alignment = ParagraphAlignment.Center;
        }
        private void tabControl_SelectionChanged(object sender, DevExpress.Xpf.Core.TabControlSelectionChangedEventArgs e) {
            if (tabControl == null || richEditControlProvider == null)
                return;

            bool isSelectedSourceControl = object.ReferenceEquals(tabControl.SelectedItem, templateTabItem);
            pageMailings.IsVisible = isSelectedSourceControl;
            pageReferences.IsVisible = !isSelectedSourceControl;
            if (isSelectedSourceControl) {
                ribbon.SelectedPage = pageMailings;
                targetRichEditControl.BarManager = null;
                sourceRichEditControl.BarManager = barManager1;
                targetRichEditControl.Ribbon = null;
                sourceRichEditControl.Ribbon = ribbon;
                richEditControlProvider.RichEditControl = sourceRichEditControl;
            }
            else {
                ribbon.SelectedPage = pageReferences;
                sourceRichEditControl.BarManager = null;
                targetRichEditControl.BarManager = barManager1;
                sourceRichEditControl.Ribbon = null;
                targetRichEditControl.Ribbon = ribbon;
                richEditControlProvider.RichEditControl = targetRichEditControl;

            }
        }
        public static object GetEmployeeData() {
            string path = DemoUtils.GetRelativePath("TOC_Employees.xml");
            System.Data.DataSet EmployeeDataDS = new System.Data.DataSet();
            EmployeeDataDS.ReadXml(path);
            return EmployeeDataDS.Tables[1];
        }
    }
}
