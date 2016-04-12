using System;
using System.Globalization;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet.Demos;

namespace SpreadsheetDemo {
    public partial class Invoice : SpreadsheetDemoModule {
        InvoiceDocumentGenerator generator;

        public Invoice() {
            InitializeComponent();
            this.spreadsheetControl1.Options.Culture = DefaultCulture;
            generator = new InvoiceDocumentGenerator(spreadsheetControl1.Document);
            PrepareGoods();
            gridGoods.DataContext = generator.InvoiceGoods;
            Process();
            ribbonControl1.SelectedPage = pageHome;
        }

        #region PrepareGoods - populate goods table with names and prices
        void PrepareGoods() {
            generator.ClearGoods();
            generator.AddGoods(new InvoiceInMemoryData("Chai", 18));
            generator.AddGoods(new InvoiceInMemoryData("Chang", 19));
            generator.AddGoods(new InvoiceInMemoryData("Aniseed Syrup", 10));
            generator.AddGoods(new InvoiceInMemoryData("Chef Anton's Cajun Seasoning", 22));
            generator.AddGoods(new InvoiceInMemoryData("Chef Anton's Gumbo Mix", 21.35));
            generator.AddGoods(new InvoiceInMemoryData("Grandma's Boysenberry Spread", 25));
            generator.AddGoods(new InvoiceInMemoryData("Uncle Bob's Organic Dried Pears", 30));
            generator.AddGoods(new InvoiceInMemoryData("Northwoods Cranberry Sauce", 40));
            generator.AddGoods(new InvoiceInMemoryData("Mishi Kobe Niku", 97));
            generator.AddGoods(new InvoiceInMemoryData("Ikura", 31));
            generator.AddGoods(new InvoiceInMemoryData("Queso Cabrales", 21));
            generator.AddGoods(new InvoiceInMemoryData("Queso Manchego La Pastora", 38));
            generator.AddGoods(new InvoiceInMemoryData("Konbu", 6));
            generator.AddGoods(new InvoiceInMemoryData("Tofu", 23.25));
            generator.AddGoods(new InvoiceInMemoryData("Genen Shouyu", 15.5));
            generator.AddGoods(new InvoiceInMemoryData("Pavlova", 17.45));
            generator.AddGoods(new InvoiceInMemoryData("Alice Mutton", 39));
            generator.AddGoods(new InvoiceInMemoryData("Carnarvon Tigers", 62.5));
            generator.AddGoods(new InvoiceInMemoryData("Teatime Chocolate Biscuits", 9.2));
            generator.AddGoods(new InvoiceInMemoryData("Sir Rodney's Marmalade", 81));
            generator.AddGoods(new InvoiceInMemoryData("Sir Rodney's Scones", 10));
            generator.AddGoods(new InvoiceInMemoryData("Gustaf's Knäckebröd", 21));
            generator.AddGoods(new InvoiceInMemoryData("Tunnbröd", 9));
            generator.AddGoods(new InvoiceInMemoryData("Guaraná Fantástica", 4.5));
            generator.AddGoods(new InvoiceInMemoryData("NuNuCa Nuß-Nougat-Creme", 14));
            generator.AddGoods(new InvoiceInMemoryData("Gumbär Gummibärchen", 31.23));
            generator.AddGoods(new InvoiceInMemoryData("Schoggi Schokolade", 43.9));
            generator.AddGoods(new InvoiceInMemoryData("Rössle Sauerkraut", 45.6));
            generator.AddGoods(new InvoiceInMemoryData("Thüringer Rostbratwurst", 123.79));
            generator.AddGoods(new InvoiceInMemoryData("Nord-Ost Matjeshering", 25.89));
            generator.AddGoods(new InvoiceInMemoryData("Gorgonzola Telino", 12.5));
            generator.SelectRandomGoods();
        }
        #endregion

        void Process() {
            TransferInvoiceParameters(generator);
            generator.Generate();
        }

        void TransferInvoiceParameters(InvoiceDocumentGenerator generator) {
            generator.ApplyParameter(InvoiceDocumentGenerator.FromAddressId, editFromCompanyStreet.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.FromCityId, editFromCompanyCity.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.FromCompanyId, editFromCompany.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.FromCompanySloganId, editFromCompanySlogan.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.FromContactNameId, editFromCompanyContactName.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.FromEMailId, editFromCompanyEmail.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.FromFaxId, editFromCompanyFax.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.FromPhoneId, editFromCompanyPhone.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.FromStateId, editFromCompanyState.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.FromZipId, editFromCompanyZip.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.ToCityId, editToCity.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.ToCompanyId, editToCompany.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.ToNameId, editToName.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.ToPhoneId, editToPhone.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.ToStateId, editToState.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.ToStreetId, editToStreet.Text);
            generator.ApplyParameter(InvoiceDocumentGenerator.ToZipId, editToZip.Text);
        }

        void gridGoods_CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e) {
            if (e.IsGetData) {
                e.Value = generator.GetGoodsAttribute(e.Column.FieldName, e.ListSourceRowIndex);
            }
            if (e.IsSetData) {
                if (generator.SetGoodsAttribute(e.Column.FieldName, e.ListSourceRowIndex, e.Value)) {
                    int rowIndex = gridGoods.GetRowHandleByListIndex(e.ListSourceRowIndex);
                    gridGoods.RefreshRow(rowIndex);
                }
            }
        }

        void PART_Editor_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            gridGoods.View.PostEditor();
        }

        void tabControl1_SelectionChanged(object sender, DevExpress.Xpf.Core.TabControlSelectionChangedEventArgs e) {
            if (IsInitialized && (e.NewSelectedIndex == 1))
                Process();
        }
    }
}
