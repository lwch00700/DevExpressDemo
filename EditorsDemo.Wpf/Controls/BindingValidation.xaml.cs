using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors;

namespace EditorsDemo {
    public partial class BindingValidation : UserControl {
        List<string> users = new List<string>();
        public BindingValidation() {
            InitializeComponent();
            users.Add("TestUser");
        }

        private void ConfirmMailValidate(object sender, ValidationEventArgs e) {
            e.IsValid = object.Equals(e.Value, txtMail.Text);
        }
        private void LoginValidate(object sender, ValidationEventArgs e) {
            if(users.Contains(txtLogin.Text))
                e.IsValid = false;
        }

        private void FirstNameValidate(object sender, ValidationEventArgs e) {
            string value = (string)e.Value;
            e.IsValid = !string.IsNullOrEmpty(value);
        }

        private void SecondNameValidate(object sender, ValidationEventArgs e) {
            string value = (string)e.Value;
            e.IsValid = !string.IsNullOrEmpty(value);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            txtMail.DoValidate();
            users.Add(txtLogin.Text);
        }
    }
}
