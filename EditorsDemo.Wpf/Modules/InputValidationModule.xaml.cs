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
using DevExpress.Xpf.Editors.Validation;
using DevExpress.Xpf.Core;
using DevExpress.Mvvm;

namespace EditorsDemo {
    public partial class InputValidationModule : EditorsDemoModule {
        RegisterNewAccount Account { get; set; }
        public InputValidationModule() {
            Account = new RegisterNewAccount();
            InitializeComponent();
            InitSources();
            DataContext = Account;
            SetJoinButtonBindings();
            Loaded += new RoutedEventHandler(ModuleLoaded);
        }
        void ModuleLoaded(object sender, RoutedEventArgs e) {
            txtLogin.Focus();
            SetSettingsContext(txtLogin);
        }
        void SetJoinButtonBindings() {
            Binding binding = new Binding() { Path = new PropertyPath(ValidationService.HasValidationErrorProperty) };
            binding.Source = validationContainer;
            binding.Converter = new NegationConverterExtension();
            btnJoin.SetBinding(IsEnabledProperty, binding);
        }
        void InitSources() {
            txtCardType.ItemsSource = RegisterNewAccount.Cards;
        }
        private void ConfirmMailValidate(object sender, ValidationEventArgs e) {
            string error = string.Empty;
            e.IsValid = Account.ValidateMail(txtMail.EditValue as string, (string)e.Value);
            e.ErrorContent = Account.Error;
        }
        protected void LoginValidate(object sender, ValidationEventArgs e) {
            e.IsValid = Account.ValidateLogin((string)e.Value);
            e.ErrorContent = Account.Error;
        }
        private void FirstNameValidate(object sender, ValidationEventArgs e) {
            e.IsValid = Account.ValidateName((string)e.Value);
            e.ErrorContent = Account.Error;
        }
        private void SecondNameValidate(object sender, ValidationEventArgs e) {
            e.IsValid = Account.ValidateName((string)e.Value);
            e.ErrorContent = Account.Error;
        }
        private void txtCardType_Validate(object sender, ValidationEventArgs e) {
            e.IsValid = Account.ValidateCardType((string)e.Value);
            e.ErrorContent = Account.Error;
        }
        private void txtCardNumber_Validate(object sender, ValidationEventArgs e) {
            e.IsValid = Account.ValidateCardNumber(txtCardType.EditValue as string, (string)e.Value);
            e.ErrorContent = Account.Error;
        }
        private void txtCardType_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if(txtCardNumber != null) txtCardNumber.DoValidate();
        }
        private void txtCardExpDate_Validate(object sender, ValidationEventArgs e) {
            e.IsValid = e.Value != null ? Account.ValidateCardExpDate((DateTime)e.Value) : false;
            e.ErrorContent = Account.Error;
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Thank you!");
        }
        private void txtMail_EditValueChanged(object sender, RoutedEventArgs e) {
            if(txtConfirmMail != null)
                txtConfirmMail.DoValidate();
        }
        private void TextEdit_GotFocus(object sender, RoutedEventArgs e) {
            SetSettingsContext(sender);
        }
        void SetSettingsContext(object context) {
            settings.DataContext = context;
        }
    }
    public class InvalidValueBehaviorHelper : DependencyObject {
        public static readonly DependencyProperty InvalidValueBehaviorProperty;

        static InvalidValueBehaviorHelper() {
            InvalidValueBehaviorProperty = DependencyProperty.RegisterAttached("InvalidValueBehavior", typeof(InvalidValueBehavior), typeof(InvalidValueBehaviorHelper),
                new PropertyMetadata(InvalidValueBehavior.AllowLeaveEditor, null));
        }

        public static InvalidValueBehavior GetInvalidValueBehavior(DependencyObject d) {
            return (InvalidValueBehavior)d.GetValue(InvalidValueBehaviorProperty);
        }
        public static void SetInvalidValueBehavior(DependencyObject d, InvalidValueBehavior value) {
            d.SetValue(InvalidValueBehaviorProperty, value);
        }
    }
    public class CardInfo {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
    public class RegisterNewAccount : BindableBase {
        public static readonly List<CardInfo> Cards = new List<CardInfo>();
        static RegisterNewAccount() {
            Cards.Add(new CardInfo() { Name = "VISA", DisplayName = "VISA (13 digits)" });
            Cards.Add(new CardInfo() { Name = "Master Card", DisplayName = "Master Card (16 digits)" });
            Cards.Add(new CardInfo() { Name = "American Express", DisplayName = "American Express (15 digits)" });
            Cards.Add(new CardInfo() { Name = "Diners Club", DisplayName = "Diners Club (13 digits)" });
        }
        public RegisterNewAccount() {
            Login = "TestUser";
            Mail = "testmail@devexpress.com";
            FirstName = "John";
            LastName = "Joe";
            CardType = "VISA";
        }

        string error;
        DateTime cardExpDate = DateTime.Today.AddMonths(-3);

        public string Error { get { return error; } }

        public string Login {
            get { return GetProperty(() => Login); }
            set { SetProperty(() => Login, value); }
        }
        public string Mail {
            get { return GetProperty(() => Mail); }
            set { SetProperty(() => Mail, value); }
        }
        public string ConfirmMail {
            get { return GetProperty(() => ConfirmMail); }
            set { SetProperty(() => ConfirmMail, value); }
        }
        public string FirstName {
            get { return GetProperty(() => FirstName); }
            set { SetProperty(() => FirstName, value); }
        }
        public string LastName {
            get { return GetProperty(() => LastName); }
            set { SetProperty(() => LastName, value); }
        }
        public decimal Age {
            get { return GetProperty(() => Age); }
            set { SetProperty(() => Age, value); }
        }
        public string CardType {
            get { return GetProperty(() => CardType); }
            set { SetProperty(() => CardType, value); }
        }
        public string CardNumber {
            get { return GetProperty(() => CardNumber); }
            set { SetProperty(() => CardNumber, value); }
        }
        public DateTime CardExpDate {
            get { return GetProperty(() => CardExpDate); }
            set { SetProperty(() => CardExpDate, value); }
        }

        void SetError(bool isValid, string errorString) {
            if(isValid)
                error = string.Empty;
            else
                error = errorString;
        }

        public bool ValidateName(string name) {
            bool isValid = !string.IsNullOrEmpty(name);
            SetError(isValid, "Name can't be empty");
            return isValid;
        }
        public bool ValidateMail(string mail, string confirmMail) {
            bool isValid = object.Equals(mail, confirmMail);
            SetError(isValid, "Two specified e-mail addresses are different");
            return isValid;
        }
        public bool ValidateCardNumber(string type, string number) {
            bool isValid = false;
            switch(type) {
                case "VISA":
                    isValid = ValidateVISA(number);
                    break;
                case "Master Card":
                    isValid = ValidateMasterCard(number);
                    break;
                case "American Express":
                    isValid = ValidateAmericanExpress(number);
                    break;
                case "Diners Club":
                    isValid = ValidateDinersClub(number);
                    break;
                default:
                    isValid = false;
                    break;
            }
            SetError(isValid, "Invalid number");
            return isValid;
        }
        public bool ValidateLogin(string login) {
            bool isValid = login != "TestUser";
            SetError(isValid, "A user with this name is already registered");
            return isValid;
        }
        public bool ValidateAge(decimal age) {
            bool isValid = age > 21 && age < 200;
            if(age < 21) {
                SetError(isValid, "Sorry, you're too young to visit our site!");
                return false;
            } else if(age > 200) {
                SetError(isValid, "Congratulations! You're the oldest man on Earth!");
                return false;
            }
            return true;
        }
        public bool ValidateCardType(string type) {
            bool isValid = type == "American Express" || type == "VISA";
            SetError(isValid, "Sorry, cards of this type are not accepted");
            return isValid;
        }
        bool ValidateVISA(string num) {
            return !string.IsNullOrEmpty(num) && num.Length == 13;
        }
        bool ValidateMasterCard(string num) {
            return !string.IsNullOrEmpty(num) && num.Length == 16;
        }
        bool ValidateAmericanExpress(string num) {
            return !string.IsNullOrEmpty(num) && num.Length == 15;
        }
        bool ValidateDinersClub(string num) {
            return !string.IsNullOrEmpty(num) && num.Length == 14;
        }
        public bool ValidateCardExpDate(DateTime expDate) {
            bool isValid = expDate.CompareTo(DateTime.Today) > 0;
            SetError(isValid, "Sorry, your card has expired");
            return isValid;
        }
    }
}
