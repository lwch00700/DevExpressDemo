using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Demos;
using DevExpress.Xpf.Editors;
using DevExpress.Mvvm;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;

namespace SpreadsheetDemo {
    public partial class PersonalFinance : SpreadsheetDemoModule {
        HomeAccountingDocumentGenerator generator;
        public PersonalFinance() {
            DataContext = ViewModelSource.Create(() => new HomeAccountingViewModel());
            InitializeComponent();
            spreadsheetControl1.Options.Culture = DefaultCulture;
            IWorkbook book = spreadsheetControl1.Document;
            GenerateDocument(book);
            spreadsheetControl1.ReadOnly = true;
            spreadsheetControl1.Options.Culture = DefaultCulture;
            spreadsheetControl1.Document.History.Clear();
            AddIncome();
            ribbonControl1.SelectedPage = pageHome;
        }
        void GenerateDocument(IWorkbook book) {
            spreadsheetControl1.BeginUpdate();
            try {
                generator = new HomeAccountingDocumentGenerator(book);
                generator.Generate();
            }
            finally {
                spreadsheetControl1.EndUpdate();
            }
        }

        void AddExpense() {
            IsIncome = false;
            string[] expenseCategorysArray = new string[generator.ExpenseCategorys.Count];
            generator.ExpenseCategorys.CopyTo(expenseCategorysArray);
            InitAddTransaction(false, expenseCategorysArray);
        }
        bool IsIncome { get; set; }
        void AddIncome() {
            IsIncome = true;
            string[] incomeCategoriesArray = new string[generator.IncomeCategorys.Count];
            generator.IncomeCategorys.CopyTo(incomeCategoriesArray);
            InitAddTransaction(true, incomeCategoriesArray);
        }
        public void InitAddTransaction(bool income, string[] categories) {
            HomeAccountingViewModel vm = DataContext as HomeAccountingViewModel;
            categoryComboBox.ItemsSource = categories;
            vm.Category = "Uncategorised";
            vm.TransactionDate = DateTime.Today;
        }

        private void ListBoxEditSelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (((ListBoxEdit)sender).SelectedItem.ToString() == "Income")
                AddIncome();
            else
                AddExpense();
        }
        private void AddClick(object sender, RoutedEventArgs e) {
            HomeAccountingViewModel vm = DataContext as HomeAccountingViewModel;
            spreadsheetControl1.BeginUpdate();
            try {
                generator.AddTransaction(IsIncome, vm.TransactionDate, (float)Convert.ToDouble(vm.Sum, CultureInfo.CurrentCulture), vm.Category);
            }
            finally {
                spreadsheetControl1.EndUpdate();
            }
        }
    }

    [POCOViewModel]
    public class HomeAccountingViewModel : BindableBase {
        public HomeAccountingViewModel() {
            Sum = 0;
            Category = string.Empty;
        }
        public virtual decimal Sum { get; set; }
        public virtual DateTime TransactionDate { get; set; }
        public virtual string Category { get; set; }
    }
}
