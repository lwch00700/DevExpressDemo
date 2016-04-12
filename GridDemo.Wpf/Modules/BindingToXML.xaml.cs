using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.DemoBase;
using System.Xml;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    [CodeFile("ModuleResources/BindingToXMLClasses.(cs)")]
    public partial class BindingToXML : GridDemoModule {
        public BindingToXML() {
            InitializeComponent();
            Assembly assembly = typeof(BindingToXML).Assembly;
            Stream stream = EmployeesWithPhotoData.GetDataStream();
            XmlDocument document = new XmlDocument();
            document.Load(stream);
            grid.ItemsSource = document.SelectNodes("/Employees/Employee");
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
