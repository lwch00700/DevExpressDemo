using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataBinding/TagPropertyControl(.SL).xaml"),
     CodeFile("Modules/DataBinding/TagPropertyControl.xaml.(cs)")]
    public partial class TagPropertyControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public TagPropertyControl() {
            InitializeComponent();
            chart.Diagram.Series[0].DataSource = CreateDataSource();
        }
        void ChartsDemoModule_ModuleAppear(object sender, System.Windows.RoutedEventArgs e) {
            chart.Animate();
        }
        void chart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e) {
            e.LegendText = ((Country)e.SeriesPoint.Tag).OfficialName;
        }
        List<Country> CreateDataSource() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/Countries.xml");
            List<Country> countries = new List<Country>();
            if (document != null) {
                foreach (XElement element in document.Element("Countries").Elements()) {
                    double area = Convert.ToDouble(element.Element("Area").Value, CultureInfo.InvariantCulture);
                    string name = element.Element("Name").Value;
                    string officialName = element.Element("OfficialName").Value;
                    countries.Add(new Country(name, officialName, area));
                }
            }
            return countries;
        }
    }

    public class Country {
        readonly double area;
        readonly string name;
        readonly string officialName;

        public double Area { get { return area; } }
        public string Name { get { return name; } }
        public string OfficialName { get { return officialName; } }

        public Country(string name, string officialName, double area) {
            this.name = name;
            this.area = area;
            this.officialName = officialName;
        }
    }
}
