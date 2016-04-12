using System;
using System.Linq;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Services;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Collections.Generic;
using System.Globalization;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.Office.Services;

using System.Data;
using DevExpress.DemoData.Models;

namespace RichEditDemo {
    public partial class MasterDetailMailMerge : RichEditDemoModule {
        List<Category> dataSetCategories;
        List<Product> dataSetProducts;
        List<OrderDetail> dataSetOrderDetails;
        CultureInfo cultureInfo;
        List<Product> currentDataSetProducts;
        int categoryID = -1;

        public MasterDetailMailMerge() {
            InitializeComponent();

            RtfLoadHelper.Load("MasterDetailMailMergeTemplate.rtf", templateRichEditControl);
            RtfLoadHelper.Load("MasterDetailMailMergeMaster.rtf", masterRichEditControl);
            RtfLoadHelper.Load("MasterDetailMailMergeDetail.rtf", detailRichEditControl);

            dataSetCategories = NWindContext.Create().Categories.ToList();
            dataSetProducts = NWindContext.Create().Products.ToList();
            dataSetOrderDetails = NWindContext.Create().OrderDetails.ToList();

            IUriStreamService uriService = (IUriStreamService)masterRichEditControl.GetService(typeof(IUriStreamService));
            uriService.RegisterProvider(new DBUriStreamProviderBase<Category>(NWindContext.Create().Categories.ToList(),
                (cs, id) => cs.First(c => c.CategoryID == id).Picture));

            cultureInfo = new CultureInfo("en-US");
        }

        void resultingRichEditControl_CalculateDocumentVariable(object sender, CalculateDocumentVariableEventArgs e) {
            if (e.VariableName == "Categories") {
                masterRichEditControl.Options.MailMerge.DataSource = dataSetCategories.ToList();

                IRichEditDocumentServer result = masterRichEditControl.CreateDocumentServer();
                result.CalculateDocumentVariable += result_CalculateDocumentVariable;
                masterRichEditControl.MailMerge(result.Document);
                result.CalculateDocumentVariable -= result_CalculateDocumentVariable;

                e.Value = result;
                e.Handled = true;
            }
        }
        void result_CalculateDocumentVariable(object sender, CalculateDocumentVariableEventArgs e) {
            ArgumentCollection arguments = e.Arguments;
            int currentCategoryID = GetID(arguments[0].Value);
            if (currentCategoryID == -1)
                return;
            if (categoryID != currentCategoryID) {
                currentDataSetProducts = GetData(currentCategoryID).ToList();
                categoryID = currentCategoryID;
            }

            if (e.VariableName == "Products") {
                detailRichEditControl.Options.MailMerge.DataSource = currentDataSetProducts;

                IRichEditDocumentServer result = detailRichEditControl.CreateDocumentServer();

                MailMergeOptions options = detailRichEditControl.CreateMailMergeOptions();
                options.MergeMode = MergeMode.JoinTables;
                result.CalculateDocumentVariable += detail_CalculateDocumentVariable;
                detailRichEditControl.MailMerge(options, result.Document);
                result.CalculateDocumentVariable -= detail_CalculateDocumentVariable;

                e.Value = result;
                e.Handled = true;
            }
            if (e.VariableName == "LowestPrice") {
                e.Value = currentDataSetProducts.Min(p => p.UnitPrice);
                e.Handled = true;
            }
            if (e.VariableName == "HighestPrice") {
                e.Value = currentDataSetProducts.Max(p => p.UnitPrice);
                e.Handled = true;
            }
            if (e.VariableName == "ItemsCount") {
                e.Value = currentDataSetProducts.Count();
                e.Handled = true;
            }
            if (e.VariableName == "TotalSales") {
                e.Value = GetTotalSales(arguments);
                e.Handled = true;
            }
        }
        protected internal virtual void detail_CalculateDocumentVariable(object sender, CalculateDocumentVariableEventArgs e) {
            int productId = GetID(e.Arguments[0].Value);
            if (productId == -1)
                return;
            if (e.VariableName == "UnitPrice") {
                e.Value = GetUnitPrice(e.Arguments);
                e.Handled = true;
            }
        }
        protected internal virtual int GetID(string value) {
            int result;
            if (Int32.TryParse(value, out result))
                return result;
            return -1;
        }
        protected internal virtual List<Product> GetData(int categoryID) {
            return dataSetProducts
                   .Where(o => (bool)(o.CategoryID == (long?)categoryID))
                   .ToList();
        }
        protected internal virtual object GetTotalSales(ArgumentCollection arguments) {
            decimal sum = NWindContext.Create().OrderDetailsExtended.Sum(o => o.ExtendedPrice);
            return String.Format(cultureInfo, "{0:C2}", sum);
        }
        protected internal virtual object GetUnitPrice(ArgumentCollection arguments) {
            decimal price = (decimal)currentDataSetProducts
                .First(p => p.ProductID == Convert.ToInt64(arguments[0].Value))
                .UnitPrice;
            return String.Format(cultureInfo, "{0:C2}", price);
        }
        void DemoModuleControl_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            ribbon.SelectedPage = pageMailings;
            MergeToNewDocument();
            ShowAllFieldCodes(templateRichEditControl);
            ShowAllFieldCodes(masterRichEditControl);
            ShowAllFieldCodes(detailRichEditControl);
        }
        void mergeToNewDocument_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            MergeToNewDocument();
        }
        static List<int> fakeDataSource = CreateFakeDataSource();
        static List<int> CreateFakeDataSource() {
            List<int> result = new List<int>();
            result.Add(0);
            return result;
        }
        protected internal virtual void MergeToNewDocument() {
            masterRichEditControl.ApplyTemplate();
            detailRichEditControl.ApplyTemplate();
            resultingRichEditControl.ApplyTemplate();

            templateRichEditControl.Options.MailMerge.DataSource = fakeDataSource;
            templateRichEditControl.MailMerge(resultingRichEditControl.Document);

            tabControl.SelectedItem = resultingDocumentTabItem;
        }
        protected internal virtual void ShowAllFieldCodes(IRichEditControl control) {
            ShowAllFieldCodesCommand command = new ShowAllFieldCodesCommand(control);
            command.Execute();
        }
        private void tabControl_SelectionChanged(object sender, DevExpress.Xpf.Core.TabControlSelectionChangedEventArgs e) {
            if (tabControl == null || richEditControlProvider == null)
                return;

            RichEditControl selectedRichEditControl = GetSelectedRichEditControl();
            ResetBarManagers();
            selectedRichEditControl.BarManager = barManager1;
            selectedRichEditControl.Ribbon = ribbon;
            richEditControlProvider.RichEditControl = selectedRichEditControl;

            bool isSelectedResultingControl = object.ReferenceEquals(selectedRichEditControl, resultingRichEditControl);
            bool isPageMailingsSelected = pageMailings.IsSelected;
            bool isPageMailingsWithoutMergeSelected = pageMailingsWithoutMerge.IsSelected;
            bool oldPageMailingsVisible = pageMailings.IsVisible;
            pageMailings.IsVisible = !isSelectedResultingControl;
            pageMailingsWithoutMerge.IsVisible = isSelectedResultingControl;

            if (oldPageMailingsVisible != pageMailings.IsVisible) {
                pageMailings.IsSelected = !isSelectedResultingControl && isPageMailingsWithoutMergeSelected;
                pageMailingsWithoutMerge.IsSelected = isSelectedResultingControl && isPageMailingsSelected;
            }
        }
        protected internal virtual RichEditControl GetSelectedRichEditControl() {
            object selectedTabItem = tabControl.SelectedItem;
            if (selectedTabItem == templateTabItem)
                return templateRichEditControl;
            if (selectedTabItem == masterTabItem)
                return masterRichEditControl;
            if (selectedTabItem == detailTabItem)
                return detailRichEditControl;
            if (selectedTabItem == resultingDocumentTabItem)
                return resultingRichEditControl;
            return null;
        }
        protected internal virtual void ResetBarManagers() {
            templateRichEditControl.BarManager = null;
            masterRichEditControl.BarManager = null;
            detailRichEditControl.BarManager = null;
            resultingRichEditControl.BarManager = null;
            templateRichEditControl.Ribbon = null;
            masterRichEditControl.Ribbon = null;
            detailRichEditControl.Ribbon = null;
            resultingRichEditControl.Ribbon = null;
        }
    }
}
