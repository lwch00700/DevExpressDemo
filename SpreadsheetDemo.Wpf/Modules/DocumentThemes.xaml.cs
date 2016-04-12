using System;
using System.Globalization;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Demos;

namespace SpreadsheetDemo {
    public partial class DocumentThemes : SpreadsheetDemoModule {
        DocumentThemesDocumentGenerator generator;

        public DocumentThemes() {
            InitializeComponent();
            this.generator = new DocumentThemesDocumentGenerator();
            this.spreadsheetControl1.Options.Culture = DefaultCulture;
            InitializeWorkbook();
            this.spreadsheetControl1.Document.History.Clear();
        }
        void InitializeWorkbook() {
            generator.Generate(spreadsheetControl1.Document);
            lbDocumentTheme.SelectedIndex = 0;
        }

        private void lbDocumentTheme_SelectedIndexChanged(object sender, System.Windows.RoutedEventArgs e) {
            if (!this.generator.IsValidDocument())
                return;

            spreadsheetControl1.BeginUpdate();

            switch (lbDocumentTheme.SelectedIndex) {
                case 0:
                    this.generator.ApplyThemeGreen();
                    break;
                case 1:
                    this.generator.ApplyThemeOrange();
                    break;
                case 2:
                    this.generator.ApplyThemeRed();
                    break;
                case 3:
                    this.generator.ApplyThemeViolet();
                    break;
                default:
                    this.generator.ApplyThemeBlue();
                    break;
            }

            this.generator.FormatCells();
            spreadsheetControl1.EndUpdate();
            spreadsheetControl1.Document.History.Clear();
        }
    }
}
