using System;
using System.Data;
using System.Globalization;
using System.IO;
using DevExpress.Xpf.Editors;
namespace MapDemo {
    public partial class BubbleCharts : MapDemoModule {
        const double MinMag = 6.5;
        const double MaxMag = 9;
        DataTable dataTable;

        public DataTable Data { get { return dataTable; } }

        public BubbleCharts() {
            InitializeComponent();
            lbYearsFilter.SelectAll();
            DataContext = this;
            SetData();
        }
        DataTable CreateData() {
            DataSet ds = new DataSet();
            Stream documentStream = DataLoader.LoadStreamFromResources("/Data/Earthquakes.xml");
            ds.ReadXml(documentStream);
            return ds.Tables[0];
        }
        void SetData() {
            this.dataTable = CreateData();
            UpdateFilter(MinMag, MaxMag);
        }
        void UpdateFilter(double minMagnitude, double maxMagnitude) {
            if(Data != null) {
                string magnitudeFilter = GetMagnitudeFilter(minMagnitude, maxMagnitude);
                string yearFilter = GetYearFilterString();
                string filter = string.Format(CultureInfo.InvariantCulture, "({0}) AND ({1})", magnitudeFilter, yearFilter);
                Data.DefaultView.RowFilter = filter;
            }
        }
        string GetMagnitudeFilter(double min, double max) {
            return string.Format(CultureInfo.InvariantCulture, "mag >= {0} AND mag <= {1}", min, max);
        }
        string GetYearFilterString() {
            string filter = "";
            string baseStr = "(yr = {0}) OR ";

            foreach(ListBoxEditItem item in lbYearsFilter.SelectedItems) {
                int year = Convert.ToInt32(item.Tag);
                filter = string.Concat(filter, string.Format(baseStr, year));
            }
            if(filter.Length > 0)
                filter = filter.Remove(filter.Length - 4, 4);
            return (filter.Length > 0) ? filter : "FALSE";
        }
        void lbYearsFilter_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            Update();
        }
        void tbMagnitudeFilter_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            Update();
        }
        void Update(){
            TrackBarEditRange range = (TrackBarEditRange)tbMagnitudeFilter.EditValue;
            UpdateFilter(range.SelectionStart, range.SelectionEnd);
        }
    }
}
