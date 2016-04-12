using System;
using System.Windows;

namespace SpreadsheetDemo {
    public partial class AddTransactionForm : Window {
        bool IsLeap(int year) { return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0); }

        int DaysInMonth(int month, int year) {
            int[,] days = { { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 }, { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 } };
            return days[Convert.ToInt32(IsLeap(year)), month];
        }
        public AddTransactionForm(bool income, string[] categorys) {
            InitializeComponent();
            this.Title = income ? "Add income" : "Add expense";
            categoryComboBox.Items.AddRange(categorys);
            categoryComboBox.Text = "Uncategorised";
            DateTime today = DateTime.Now;
            for (int i = 1; i <= 12; i++) {
                monthComboBox.Items.Add(System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(i));
            }
            monthComboBox.SelectedIndex = today.Month - 1;
            dayTextEdit.Value = today.Day;
            dayTextEdit.MinValue = 1;
        }

        private void OkButtonClick(object sender, EventArgs e) {
            DialogResult = true;
        }

        private void MonthComboBoxEditValueChanged(object sender, EventArgs e) {
            dayTextEdit.MaxValue = DaysInMonth(monthComboBox.SelectedIndex, 2013);
        }
    }
}
