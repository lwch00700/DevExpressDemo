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
using System.ComponentModel;

namespace EditorsDemo {
    public partial class BindingValidationModule : EditorsDemoModule {
        public BindingValidationModule() {
            InitializeComponent();
            DataContext = new RegisterNewAccountWithDataErrorInfo();
        }
        private void Keyboard_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) {
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Thank you!");
        }

        private void txtCardType_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            BindingExpression expression = BindingOperations.GetBindingExpression(txtCardNumber, BaseEdit.EditValueProperty);
            if (expression != null)
                expression.UpdateTarget();
        }
        private void txtMail_EditValueChanged(object sender, RoutedEventArgs e) {
            if(txtConfirmMail == null)
                return;
            BindingExpression expression = BindingOperations.GetBindingExpression(txtConfirmMail, BaseEdit.EditValueProperty);
            if(expression != null)
                expression.UpdateTarget();
        }
        private void txtConfirmMail_EditValueChanged(object sender, RoutedEventArgs e) {
            if(txtMail == null)
                return;
            BindingExpression expression = BindingOperations.GetBindingExpression(txtMail, BaseEdit.EditValueProperty);
            if (expression != null)
                expression.UpdateTarget();
        }

    }

    public class RegisterNewAccountWithDataErrorInfo : RegisterNewAccount, IDataErrorInfo {
        #region IDataErrorInfo Members
    string IDataErrorInfo.Error { get { return Error; } }
    string IDataErrorInfo.this[string columnName] {
        get {
            switch(columnName) {
                case "Login":
                    return ValidateLogin(Login) ? string.Empty : Error;
                case "Mail":
                    return ValidateMail(Mail, ConfirmMail) ? string.Empty : Error;
                case "ConfirmMail":
                    return ValidateMail(Mail, ConfirmMail) ? string.Empty : Error;
                case "Age":
                    return ValidateAge(Age) ? string.Empty : Error;
                case "CardType":
                    return ValidateCardType(CardType) ? string.Empty : Error;
                case "CardExpDate":
                    return ValidateCardExpDate(CardExpDate) ? string.Empty : Error;
                case "CardNumber":
                    return ValidateCardNumber(CardType, CardNumber) ? string.Empty : Error;
            }
            return string.Empty;
        }
    }
        #endregion
    }

    public class EmptyStringValidationRule : ValidationRule {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo) {
            return new ValidationResult(!string.IsNullOrEmpty((string)value), "Empty");
        }
    }
}
