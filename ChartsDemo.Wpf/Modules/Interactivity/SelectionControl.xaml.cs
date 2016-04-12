using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Map;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/SelectionControl(.SL).xaml"),
     CodeFile("Modules/Interactivity/SelectionControl.xaml.(cs)")]
    public partial class SelectionControl : ChartsDemoModule {
        DashboardViewModel viewModel = new DashboardViewModel();

        public override ChartControl ActualChart {
            get { return pieChart; }
        }

        public SelectionControl() {
            InitializeComponent();
            DataContext = viewModel;
        }

        void ShapefileLoader_ShapesLoaded(object sender, ShapesLoadedEventArgs e) {
            viewModel.SetMapItems(e.Shapes);
        }
    }

    public class CountryStatisticInfo {
        readonly string name;
        readonly double area;
        readonly List<PopulationStatisticByYear> statistic;
        WeakReference shape;

        public string Name {
            get { return name; }
        }
        public double AreaMSqrKilometers {
            get { return area; }
        }
        public List<PopulationStatisticByYear> PopulationDynamic {
            get { return statistic; }
        }
        public MapItem Shape {
            get { return (shape != null) ? (MapItem)shape.Target : null; }
            set {
                if (value == null)
                    shape = null;
                else {
                    if (shape == null || shape.Target != value)
                        shape = new WeakReference(value);
                }
            }
        }

        public CountryStatisticInfo(string name, double areaMSqrKilometers, List<PopulationStatisticByYear> statistic) {
            this.name = name;
            this.area = areaMSqrKilometers;
            this.statistic = statistic;
        }
    }

    public class PopulationStatisticByYear {
        int year;
        double populationMillionsOfPeople;
        double urbanPercent;

        public int Year {
            get { return year; }
        }
        public double PopulationMillionsOfPeople {
            get { return populationMillionsOfPeople; }
        }
        public double UrbanPercent {
            get { return urbanPercent; }
        }
        public double RuralPercent {
            get { return 100 - urbanPercent; }
        }

        public PopulationStatisticByYear(int year, double populationMillionsOfPeople, double urbanPercent) {
            this.year = year;
            this.populationMillionsOfPeople = populationMillionsOfPeople;
            this.urbanPercent = urbanPercent;
        }
    }

    class CountriesInfoDataReader {
        const string UriToXmlFile = "/Data/Top10LagerstCountriesInfo.xml";

        static List<PopulationStatisticByYear> LoadStatistic(XElement populationDynamic) {
            List<PopulationStatisticByYear> statistic = new List<PopulationStatisticByYear>();
            foreach (XElement populationDynamicItem in populationDynamic.Elements("PopulationStatisticByYear")) {
                int year = int.Parse(populationDynamicItem.Element("Year").Value);
                long population = long.Parse(populationDynamicItem.Element("Population").Value);
                double urbanPercent = double.Parse(populationDynamicItem.Element("UrbanPercent").Value, CultureInfo.InvariantCulture);
                PopulationStatisticByYear popDynamicItem = new PopulationStatisticByYear(year, (double)population / 1000000.0, urbanPercent);
                statistic.Add(popDynamicItem);
            }
            return statistic;
        }

        public static List<CountryStatisticInfo> Load() {
            List<CountryStatisticInfo> data = new List<CountryStatisticInfo>();
            try {
                XDocument Top10LagerstCountriesInfo_xml = DataLoader.LoadXmlFromResources(UriToXmlFile);
                foreach (XElement countryInfo in Top10LagerstCountriesInfo_xml.Root.Elements("CountryInfo")) {
                    string name = countryInfo.Element("Name").Value;
                    double areaSqKm = uint.Parse(countryInfo.Element("AreaSqrKilometers").Value);
                    List<PopulationStatisticByYear> statistic = LoadStatistic(countryInfo.Element("Statistic"));
                    CountryStatisticInfo countryInfoInstance = new CountryStatisticInfo(name, areaSqKm / 1000000, statistic);
                    data.Add(countryInfoInstance);
                }
            }
            catch {
            }
            return data;
        }
    }

    public class DashboardViewModel : INotifyPropertyChanged {
        readonly List<CountryStatisticInfo> countriesData;
        CountryStatisticInfo selectedCountry;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<CountryStatisticInfo> CountriesData {
            get { return countriesData; }
        }
        public CountryStatisticInfo SelectedCountry {
            get { return selectedCountry; }
            set {
                if (selectedCountry != value) {
                    selectedCountry = value;
                    OnPropertyChanged("SelectedCountry");
                }
            }
        }

        public DashboardViewModel() {
            countriesData = CountriesInfoDataReader.Load();
            SelectedCountry = countriesData[0];
        }

        void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler propertyChangedEventHendler = PropertyChanged;
            if (propertyChangedEventHendler != null)
                propertyChangedEventHendler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetMapItems(IList<MapItem> layerMapItemCollection) {
            foreach (MapItem item in layerMapItemCollection) {
                string shapeName = (string)item.Attributes["NAME"].Value;
                CountryStatisticInfo countryInfo = countriesData.Find(info => info.Name == shapeName);
                countryInfo.Shape = item;
                item.Attributes.Add(new MapItemAttribute() { Name = "CountryInfo", Type = typeof(CountryStatisticInfo), Value = countryInfo });
            }
        }
    }

    public class SelectedMapItemConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return null;
            CountryStatisticInfo country = (CountryStatisticInfo)value;
            return country.Shape;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            MapItem selectedShape = (MapItem)value;
            return selectedShape.Attributes["CountryInfo"].Value;
        }
    }

    public class ChartPaletteToMapColorsConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Palette chartColors = (Palette)value;
            DoubleCollection rangeStops = (DoubleCollection)parameter;
            int colorsCount = (int)(rangeStops[rangeStops.Count - 1] - rangeStops[0]) + 1;
            DevExpress.Xpf.Map.ColorCollection mapColors = new DevExpress.Xpf.Map.ColorCollection();
            for (int i = 0; i < colorsCount; i++)
                mapColors.Add(chartColors[i]);
            return mapColors;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
