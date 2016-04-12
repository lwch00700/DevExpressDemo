using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Xml.Linq;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/CurrencyExchangeRates(.SL).xaml"),
     CodeFile("Modules/Interactivity/CurrencyExchangeRates.xaml.(cs)")]
    public partial class CurrencyExchangeRates : ChartsDemoModule {

        public CurrencyExchangeRates() {
            InitializeComponent();
        }

        void ChartsDemoModule_Loaded(object sender, RoutedEventArgs e) {
            DataContext = new ExchangeRatesModel();
        }
    }

    public class ExchangeRatesModel {
        string rate1Name;
        string rate2Name;

        readonly List<PriceByDate> _rate1 = new List<PriceByDate>();
        readonly List<PriceByDate> _rate2 = new List<PriceByDate>();

        public List<PriceByDate> Rate1 {
            get { return _rate1; }
        }
        public List<PriceByDate> Rate2 {
            get { return _rate2; }
        }
        public string Rate1Title {
            get { return rate1Name; }
        }
        public string Rate2Title {
            get { return rate2Name; }
        }

        public ExchangeRatesModel() {
            rate1Name = "GBPUSD";
            rate2Name = "EURUSD";
            LoadPoints(_rate1, LoadFromFile("/Data/GbpUsdRate.xml"));
            LoadPoints(_rate2, LoadFromFile("/Data/EurUsdRate.xml"));
        }
        XDocument LoadFromFile(string xmlFile) {
            return DataLoader.LoadXmlFromResources(xmlFile);
        }
        void LoadPoints(List<PriceByDate> rate, XDocument document) {
            if (rate != null && document != null) {
                foreach (XElement element in document.Descendants("CurrencyRate")) {
                    DateTime date = DateTime.Parse(element.Element("DateTime").Value);
                    double price = double.Parse(element.Element("Rate").Value, CultureInfo.InvariantCulture);
                    rate.Add(new PriceByDate(date, price));
                }
            }
        }
    }
}
