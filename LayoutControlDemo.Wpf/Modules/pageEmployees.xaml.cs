using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.LayoutControl;

namespace DevExpress.Xpf.LayoutControlDemo {
    public partial class pageEmployees : LayoutControlDemoModule {
        public pageEmployees() {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(DemoModuleControl.Content, BitmapScalingMode.HighQuality);
        }

        private void GroupBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            var groupBox = (GroupBox)sender;
            groupBox.State = groupBox.State == GroupBoxState.Normal ? GroupBoxState.Maximized : GroupBoxState.Normal;
        }
    }

    public class EmployeeViewModel {
        public EmployeeViewModel(Employee employee) {
            Model = employee;
            ImageSource = ImageHelper.CreateImageFromStream(new MemoryStream(Model.ImageData));
        }

        public string AddressLine2 {
            get {
                string result = Model.City;
                if (!string.IsNullOrEmpty(Model.StateProvinceName))
                    result += ", " + Model.StateProvinceName;
                if (!string.IsNullOrEmpty(Model.PostalCode))
                    result += ", " + Model.PostalCode;
                if (!string.IsNullOrEmpty(Model.CountryRegionName))
                    result += ", " + Model.CountryRegionName;
                return result;
            }
        }
        public ImageSource ImageSource { get; private set; }
        public Employee Model { get; private set; }
    }

    public class EmployeesViewModel : List<EmployeeViewModel> {
        public EmployeesViewModel() : this(EmployeesWithPhotoData.DataSource) { }
        public EmployeesViewModel(IEnumerable<Employee> model) {
            foreach (Employee employee in model)
                Add(new EmployeeViewModel(employee));

            Sort(CompareByLastNameFirstName);
        }

        private int CompareByLastNameFirstName(EmployeeViewModel x, EmployeeViewModel y) {
            string value1 = x.Model.LastName + x.Model.FirstName;
            string value2 = y.Model.LastName + y.Model.FirstName;
            return string.Compare(value1, value2);
        }
    }
}
