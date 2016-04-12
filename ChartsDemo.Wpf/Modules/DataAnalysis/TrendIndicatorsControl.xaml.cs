using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataAnalysis/TrendIndicatorsControl(.SL).xaml"),
     CodeFile("Modules/DataAnalysis/TrendIndicatorsControl.xaml.(cs)")]
    public partial class TrendIndicatorsControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public TrendIndicatorsControl() {
            InitializeComponent();
            chart.Diagram.Series[0].DataSource = CsvReader.ReadFinancialData("USDJPYDaily.csv");
        }

        public override bool SupportSidebarContent() {
            return false;
        }
    }
}
