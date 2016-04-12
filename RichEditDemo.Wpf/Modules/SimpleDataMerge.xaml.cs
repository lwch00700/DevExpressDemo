using System;
using System.Collections.Generic;
using DevExpress.XtraRichEdit.API.Native;
using RichEditDemo.Controls;
using DevExpress.Xpf.Core;

namespace RichEditDemo {
    public partial class SimpleDataMerge : RichEditDemoModule {
        List<Employee> employees;
        MergeFieldNameInfo[] mergeFieldsNamesInfo;

        public SimpleDataMerge() {
            InitializeComponent();
            ribbon.SelectedPage = pageMailings;
            RtfLoadHelper.Load("MailMergeSimple.rtf", richEdit);
        }

        void richEdit_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.ApplyTemplate();

            this.employees = GenerateEmployeeList();
            this.mergeFieldsNamesInfo = CreateMergeFieldsNamesInfo();
            richEdit.Options.MailMerge.DataSource = employees;
            richEdit.Options.MailMerge.ViewMergedData = true;
        }


        #region Employee list generation
        static string[] firstName = { "Nancy", "Andrew", "Janet", "Margaret", "Steven", "Michael", "Robert", "Laura", "Anne" };
        static string[] lastName = { "Davolio", "Fuller", "Leverling", "Peacock", "Buchanan", "Suyama", "King", "Callahan", "Dodsworth" };
        static string[] city = { "Seattle", "Tacoma", "Kirkland", "Redmond", "London", "London", "London", "Seattle", "London" };
        static string[] country = { "USA", "USA", "USA", "USA", "UK", "UK", "UK", "USA", "UK" };
        static string[] address = { "507 - 20th Ave. E. Apt. 2A", "908 W. Capital Way", "722 Moss Bay Blvd.", "4110 Old Redmond Rd.", "14 Garrett Hill", "Coventry House Miner Rd.", "Edgeham Hollow Winchester Way", "4726 - 11th Ave. N.E.", "7 Houndstooth Rd." };
        static string[] position = { "Sales Representative", "Vice President, Sales", "Sales Representative", "Sales Representative", "Sales Manager", "Sales Representative", "Sales Representative", "Inside Sales Coordinator", "Sales Representative" };
        static char[] gender = { 'F', 'M', 'F', 'F', 'M', 'M', 'M', 'F', 'F' };
        static string[] phone = { "(206) 555-9857", "(206) 555-9482", "(206) 555-3412", "(206) 555-8122", "(71) 555-4848", "(71) 555-7773", "(71) 555-5598", "(206) 555-1189", "(71) 555-4444" };
        static string[] companyName = { "Consolidated Holdings", "Around the Horn", "North/South", "Island Trading", "White Clover Markets", "Trail's Head Gourmet Provisioners", "The Cracker Box", "The Big Cheese", "Rattlesnake Canyon Grocery", "Split Rail Beer & Ale", "Hungry Coyote Import Store", "Great Lakes Food Market" };
        List<Employee> GenerateEmployeeList() {
            List<Employee> employees = new List<Employee>();
            for (int i = 0; i < 10; i++)
                employees.Add(CreateEmployee(i));
            return employees;
        }
        Employee CreateEmployee(int seed) {
            Employee result = new Employee();
            Random rnd = new Random(seed);
            int countryIndex = rnd.Next(0, country.Length);
            result.Country = country[countryIndex];
            result.Address = address[countryIndex];
            result.City = city[countryIndex];
            result.LastName = lastName[rnd.Next(0, lastName.Length)];
            int firstNameIndex = rnd.Next(0, firstName.Length);
            result.FirstName = firstName[firstNameIndex];
            result.Gender = gender[firstNameIndex];
            result.HiringDate = DateTime.Now.AddDays(-(rnd.Next(0, 2000)));
            result.Position = position[rnd.Next(0, position.Length)];
            result.Phone = phone[rnd.Next(0, phone.Length)];
            result.CompanyName = companyName[rnd.Next(0, companyName.Length)];
            result.HRManagerName = String.Format("{0} {1}", firstName[rnd.Next(0, firstName.Length)], lastName[rnd.Next(0, lastName.Length)]);
            return result;
        }
        #endregion
        #region Merge field name list generation
        protected internal virtual MergeFieldNameInfo[] CreateMergeFieldsNamesInfo() {
            List<MergeFieldNameInfo> result = new List<MergeFieldNameInfo>();
            result.Add(CreateMergeFieldNameInfo("FirstName"));
            result.Add(CreateMergeFieldNameInfo("LastName"));
            result.Add(CreateMergeFieldNameInfo("HiringDate"));
            result.Add(CreateMergeFieldNameInfo("Address"));
            result.Add(CreateMergeFieldNameInfo("City"));
            result.Add(CreateMergeFieldNameInfo("Country"));
            result.Add(CreateMergeFieldNameInfo("Position"));
            result.Add(CreateMergeFieldNameInfo("CompanyName"));
            result.Add(CreateMergeFieldNameInfo("Gender"));
            result.Add(CreateMergeFieldNameInfo("Phone"));
            result.Add(CreateMergeFieldNameInfo("HRManagerName"));

            return result.ToArray();
        }
        MergeFieldNameInfo CreateMergeFieldNameInfo(string fieldName) {
            string displayName = CreateDisplayName(fieldName);
            return new MergeFieldNameInfo(new MergeFieldName(fieldName, displayName));
        }
        string CreateDisplayName(string fieldName) {
            fieldName = fieldName.Replace('_', ' ');
            fieldName = fieldName.Replace('.', ' ');

            char prevChar = ' ';
            int count = fieldName.Length;
            System.Text.StringBuilder sb = new System.Text.StringBuilder(count);
            for (int i = 0; i < count; i++) {
                char ch = fieldName[i];
                if (Char.IsLower(prevChar) && Char.IsUpper(ch))
                    sb.Append(' ');
                sb.Append(ch);
                prevChar = ch;
            }
            return sb.ToString();
        }
        #endregion

        private void richEdit_CustomizeMergeFields(object sender, DevExpress.XtraRichEdit.CustomizeMergeFieldsEventArgs e) {
            e.MergeFieldsNames = CalculateAllowedFieldsNames();
        }
        MergeFieldName[] CalculateAllowedFieldsNames() {
            List<MergeFieldName> result = new List<MergeFieldName>();
            foreach (MergeFieldNameInfo fieldNameInfo in mergeFieldsNamesInfo) {
                if (fieldNameInfo.CanShow) {
                    MergeFieldName fieldName = fieldNameInfo.FieldName;
                    string displayName = String.Format("{0} ({1})", fieldName.DisplayName, fieldName.Name);
                    result.Add(new MergeFieldName(fieldName.Name, displayName));
                }
            }
            return result.ToArray();
        }
        void OnCustomizeMergeFields(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            CustomizeMergeFieldsControl control = new CustomizeMergeFieldsControl(mergeFieldsNamesInfo);

            FloatingContainerParameters parameters = new FloatingContainerParameters();
            parameters.Title = "Customize merge fields";
            parameters.CloseOnEscape = true;
            parameters.AllowSizing = true;

            FloatingContainer.ShowDialogContent(control, this, System.Windows.Size.Empty, parameters, true);
        }
    }

    public class Employee {
        string firstName;
        string lastName;
        DateTime hiringDate;
        string address;
        string city;
        string country;
        string position;
        string companyName;
        char gender;
        string phone;
        string hrManagerName;

        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public DateTime HiringDate { get { return hiringDate; } set { hiringDate = value; } }
        public string Address { get { return address; } set { address = value; } }
        public string City { get { return city; } set { city = value; } }
        public string Country { get { return country; } set { country = value; } }
        public string Position { get { return position; } set { position = value; } }
        public string CompanyName { get { return companyName; } set { companyName = value; } }
        public char Gender { get { return gender; } set { gender = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        public string HRManagerName { get { return hrManagerName; } set { hrManagerName = value; } }
    }
    public class MergeFieldNameInfo {
        #region Fields
        MergeFieldName fieldName;
        bool canShow;
        #endregion

        public MergeFieldNameInfo(MergeFieldName fieldName) {
            this.fieldName = fieldName;
            this.canShow = true;
        }

        #region Properties
        public MergeFieldName FieldName { get { return fieldName; } }
        public bool CanShow { get { return canShow; } set { canShow = value; } }
        #endregion
    }
}
