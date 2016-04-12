using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;

namespace ChartsDemo {
    public class BindingUsingSeriesTemplateViewModel {
        IEnumerable<GSP> dataSource;
        IEnumerable<ChartDataBindingControlSeriesViewModel> series;

        public BindingUsingSeriesTemplateViewModel() {
            ShowLabels = true;
            SelectedSeries = this.Series.First();
        }
        public IEnumerable<GSP> DataSource {
            get {
                if(dataSource == null) {
                    dataSource = DataLoader.LoadXmlFromResources("/Data/GSP.xml")
                        .Element("GSPs").Elements()
                        .Select(e => new GSP(e.Element("Region").Value, e.Element("Year").Value, Convert.ToDouble(e.Element("Product").Value, CultureInfo.InvariantCulture)))
                        .ToList();
                }
                return dataSource;
            }
        }
        public IEnumerable<ChartDataBindingControlSeriesViewModel> Series {
            get {
                if(series == null) {
                    var series1 = new ChartDataBindingControlSeriesViewModel("Year", "Region", true);
                    var series2 = new ChartDataBindingControlSeriesViewModel("Region", "Year", false);
                    series = new ChartDataBindingControlSeriesViewModel[] { series1, series2 };
                }
                return series;
            }
        }
        public virtual bool ShowLabels { get; set; }
        public virtual ChartDataBindingControlSeriesViewModel SelectedSeries { get; set; }
        public virtual IChartAnimationService ChartAnimationService { get { return null; } }
        public void OnModuleAppear() {
            ChartAnimationService.Animate();
        }
        protected void OnSelectedSeriesChanged(ChartDataBindingControlSeriesViewModel oldValue) {
            if(ChartAnimationService != null)
                ChartAnimationService.Animate();
        }
    }
    public class ChartDataBindingControlSeriesViewModel {
        public ChartDataBindingControlSeriesViewModel(string dataMember, string argumentDataMember, bool staggered) {
            DataMember = dataMember;
            ArgumentDataMember = argumentDataMember;
            Staggered = staggered;
        }
        public string DataMember { get; private set; }
        public string ArgumentDataMember { get; private set; }
        public bool Staggered { get; private set; }
    }
}
