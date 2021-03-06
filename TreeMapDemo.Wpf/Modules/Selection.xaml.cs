﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;
using DevExpress.Xpf.TreeMap;

namespace TreeMapDemo {
    public partial class Selection : TreeMapDemoModule {
        DashboardViewModel viewModel = new DashboardViewModel();

        public Selection() {
            InitializeComponent();
            DataContext = ViewModelSource.Create(() => new DashboardViewModel());
        }

        void ShapefileLoader_ShapesLoaded(object sender, ShapesLoadedEventArgs e) {
            ((DashboardViewModel)DataContext).SetMapItems(e.Shapes);
        }
    }

    class CountriesInfoDataReader {
        static List<GDPStatisticByYear> LoadStatistic(XElement exportOfGoodsDynamic) {
            List<GDPStatisticByYear> statistic = new List<GDPStatisticByYear>();
            foreach (XElement exportOfGoodsDynamicItem in exportOfGoodsDynamic.Elements("GDPByYear")) {
                int year = int.Parse(exportOfGoodsDynamicItem.Element("Year").Value);
                double exportOfGoodsPercent = double.Parse(exportOfGoodsDynamicItem.Element("GDP").Value, CultureInfo.InvariantCulture);
                GDPStatisticByYear popDynamicItem = new GDPStatisticByYear(year, exportOfGoodsPercent);
                statistic.Add(popDynamicItem);
            }
            return statistic;
        }

        public static List<CountryStatisticInfo> Load() {
            List<CountryStatisticInfo> data = new List<CountryStatisticInfo>();
            try {
                XDocument Top10LargestCountries_xml = DataLoader.LoadXDocumentFromResources("/Data/CountriesGDPByYears.xml");
                foreach (XElement countryInfo in Top10LargestCountries_xml.Root.Elements("CountryInfo")) {
                    string name = countryInfo.Element("Name").Value;
                    string gdp = countryInfo.Element("GDP").Value;
                    string continent = countryInfo.Element("Continent").Value;
                    List<GDPStatisticByYear> statistic = LoadStatistic(countryInfo.Element("Statistic"));
                    CountryStatisticInfo countryInfoInstance = new CountryStatisticInfo(name, continent, Convert.ToDouble(gdp), statistic);
                    data.Add(countryInfoInstance);
                }
            }
            catch {
            }
            return data;
        }
    }

    [POCOViewModel]
    public class DashboardViewModel : ViewModelBase {
        public virtual List<CountryStatisticInfo> CountriesData { get; set; }
        public virtual CountryStatisticInfo SelectedCountry { get; set; }
        public virtual string ChartTitle { get; set; }
        public virtual Uri MapFileUri { get; set; }

        protected void OnSelectedCountryChnged() {

        }

        public DashboardViewModel() {
            MapFileUri = DataLoader.GetResourceUri("/Data/CountriesGDP.shp");
            CountriesData = CountriesInfoDataReader.Load();
        }

        public void SetMapItems(IList<MapItem> layerMapItemCollection) {
            foreach (MapItem item in layerMapItemCollection) {
                string shapeName = (string)item.Attributes["NAME"].Value;
                if (shapeName == "Others")
                    item.Visible = false;
                CountryStatisticInfo countryInfo = CountriesData.Find(info => info.Name == shapeName);
                if (countryInfo != null) {
                    countryInfo.Shape = item;
                    item.Attributes.Add(new MapItemAttribute() { Name = "CountryInfo", Type = typeof(CountryStatisticInfo), Value = countryInfo });
                }
            }
            SelectedCountry = CountriesData[25];
        }
    }

    public class CountryStatisticInfo {
        readonly string name;
        readonly string continent;
        readonly List<GDPStatisticByYear> statistic;
        readonly double gdp;
        WeakReference shape;

        public string Name {
            get { return name; }
        }
        public string Continent {
            get { return continent; }
        }
        public List<GDPStatisticByYear> GDPDynamic {
            get { return statistic; }
        }
        public double GDP {
            get { return gdp; }
        }
        public MapItem Shape {
            get { return (shape != null) ? (MapItem)shape.Target : null; }
            set {
                if (value == null)
                    shape = null;
                else {
                    if (shape == null || shape.Target != value) {
                        shape = new WeakReference(value);
                    }
                }
            }
        }

        public CountryStatisticInfo(string name, string continent, double gdp, List<GDPStatisticByYear> statistic) {
            this.name = name;
            this.continent = continent;
            this.statistic = statistic;
            this.gdp = gdp;
        }
    }

    public class GDPStatisticByYear {
        int year;
        double gdp;

        public int Year { get { return year; } }
        public double GDP { get { return gdp; } }

        public GDPStatisticByYear(int year, double gdp) {
            this.year = year;
            this.gdp = gdp;
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
            PaletteBase chartColors = (PaletteBase)value;
            DevExpress.Xpf.Map.ColorCollection mapColors = new DevExpress.Xpf.Map.ColorCollection();
            DoubleCollection rangeStops = (DoubleCollection)parameter;
            int colorsCount = (int)(rangeStops[rangeStops.Count - 1] - rangeStops[0]) + 1;
            for (int i = 0; i < colorsCount; i++)
                mapColors.Add(chartColors[i]);
            return mapColors;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    class ColorsMultiConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            DevExpress.Xpf.Charts.CustomPalette chartPalette = new DevExpress.Xpf.Charts.CustomPalette();
            PaletteBase palette = (values[0] == null) ? null : (PaletteBase)values[0];
            CountryStatisticInfo countryInfo = (values[0] == null) ? null : (CountryStatisticInfo)values[1];
            if (countryInfo != null && countryInfo.Shape != null) {
                int index = Int16.Parse(countryInfo.Shape.Attributes["MAP_COLOR"].Value.ToString());
                chartPalette.Colors.Add(palette[index]);
            }
            return chartPalette;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }
}
