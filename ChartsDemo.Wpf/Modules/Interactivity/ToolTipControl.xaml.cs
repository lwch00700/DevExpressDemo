using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/ToolTipControl(.SL).xaml"),
     CodeFile("Modules/Interactivity/ToolTipControl.xaml.(cs)")]
    public partial class ToolTipControl : ChartsDemoModule{
        ToolTipMousePosition ttMousePosition = new ToolTipMousePosition();
        ToolTipRelativePosition ttRelativePosition = new ToolTipRelativePosition();
        ToolTipFreePosition ttFreePosition = new ToolTipFreePosition() { Offset=new Point(16, 16) };

        public override ChartControl ActualChart { get { return chart; } }

        public ToolTipControl() {
            InitializeComponent();
            ttFreePosition.DockTarget = defaultPane;
            ToolTipControlHelper.PrepareToolTipPositionComboBox(cbToolTipPosition);
            ToolTipControlHelper.PrepareToolTipLocationComboBox(cbToolTipLocation);
            chart.DataSource = GetDataSource();
        }

        void cbToolTipPosition_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            ChangeOptionsView();
            switch(cbToolTipPosition.SelectedIndex){
                case 0:
                    ttMousePosition.Location = ToolTipControlHelper.GetLocationFromComboBox(cbToolTipLocation.SelectedIndex);
                    chart.ToolTipOptions.ToolTipPosition = ttMousePosition;
                    break;
                case 1:
                    ttRelativePosition.Location = ToolTipControlHelper.GetLocationFromComboBox(cbToolTipLocation.SelectedIndex);
                    chart.ToolTipOptions.ToolTipPosition = ttRelativePosition;
                    break;
                case 2:
                    chart.ToolTipOptions.ToolTipPosition = ttFreePosition;
                    break;
            }
        }
        void cbToolTipLocation_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            ttMousePosition.Location = ToolTipControlHelper.GetLocationFromComboBox(cbToolTipLocation.SelectedIndex);
            ttRelativePosition.Location = ToolTipControlHelper.GetLocationFromComboBox(cbToolTipLocation.SelectedIndex);
        }
        void ToolTipController_ToolTipOpening(object sender, ChartToolTipEventArgs e) {
            ToolTipData toolTipData = e.Hint as ToolTipData;
            int colorNumber = e.Series.Points.IndexOf(e.SeriesPoint);
            Color seriesColor = e.ChartControl.Palette[colorNumber];
            toolTipData.SeriesBrush = new SolidColorBrush(seriesColor);
        }
        void ChangeOptionsView() {
            if (cbToolTipPosition.SelectedIndex == 2) {
                cbToolTipLocation.IsEnabled = false;
                chbShowBeak.IsEnabled = false;
            }
            else {
                cbToolTipLocation.IsEnabled = true;
                chbShowBeak.IsEnabled = true;
            }
        }
        List<GDP> GetGDPs() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/GDPofG8.xml");
            List<GDP> result = new List<GDP>();
            if (document != null) {
                foreach (XElement element in document.Element("G8GDPs").Elements()) {
                    string country = element.Element("Country").Value;
                    int year = int.Parse(element.Element("Year").Value);
                    decimal product = Convert.ToDecimal(element.Element("Product").Value, CultureInfo.InvariantCulture);
                    result.Add(new GDP(country, year, product));
                }
            }
            return result;
        }
        List<G8Member> GetDataSource() {
            List<GDP> GDPs = GetGDPs();
            List<G8Member> countries = new List<G8Member>();
            const int yearsInDecade = 10;
            for(int countryCounter = 0; countryCounter < 8; countryCounter++){
                List<GDP> countryGDPs = new List<GDP>();
                for (int countryValuesCounter = 0; countryValuesCounter < yearsInDecade; countryValuesCounter++) {
                    countryGDPs.Add(GDPs[countryCounter * yearsInDecade + countryValuesCounter]);
                }
                countries.Add(new G8Member(countryGDPs));
            }
            return countries;
        }
    }
    public class G8Member {
        string countryName;
        decimal gdpIn2010;
        ToolTipData toolTipData;
        public G8Member(List<GDP> GDPs) {
            this.toolTipData = new ToolTipData(GDPs, GDPs[0].Country);
            this.countryName = GDPs[0].Country;
            this.gdpIn2010 = GDPs[9].Product;
        }
        public string CountryName {
            get { return countryName; }
        }
        public decimal GDPin2010 {
            get { return gdpIn2010; }
        }
        public ToolTipData ToolTipData {
            get { return toolTipData; }
            set { toolTipData = value; }
        }
    }

    public class GDP {
        string country;
        decimal product;
        int year;
        public GDP(string country, int year, decimal product) {
            this.country = country;
            this.year = year;
            this.product = product;
        }
        public string Country {
            get { return country; }
        }
        public decimal Product {
            get { return product; }
        }
        public int Year {
            get { return year; }
        }
    }

    public class ToolTipData {
        List<GDP> gdps;
        string title;
        SolidColorBrush seriesBrush;

        public ToolTipData(List<GDP> gdps, string countryName) {
            this.gdps = gdps;
            this.title = countryName + " GDP History";
        }
        public List<GDP> GDPs {
            get { return gdps; }
        }
        public SolidColorBrush SeriesBrush {
            get { return seriesBrush; }
            set { seriesBrush = value; }
        }
        public string Title {
            get { return title; }
        }
    }
}
