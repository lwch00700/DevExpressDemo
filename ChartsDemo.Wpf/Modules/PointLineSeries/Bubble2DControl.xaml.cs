using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/Bubble2DControl(.SL).xaml"),
     CodeFile("Modules/PointLineSeries/Bubble2DControl.xaml.(cs)")]
    public partial class Bubble2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public Bubble2DControl() {
            InitializeComponent();
            Series.DataSource = CreateDataSource();
            lbMarker.SelectedItem = Marker2DModelKindHelper.FindActualMarker2DModelKind(typeof(RingMarker2DModel));
            Series.ToolTipPointPattern = "{A} ({W})";
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        List<IndustryBubblePoint> CreateDataSource() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/Morbidity.xml");
            List<IndustryBubblePoint> result = new List<IndustryBubblePoint>();
            if (document != null) {
                foreach (XElement element in document.Element("Morbidity").Elements()) {
                    IndustryBubblePoint point = new IndustryBubblePoint();
                    point.Name = element.Element("Name").Value;
                    point.NumberOfCases = Convert.ToInt32(element.Element("NumberOfCases").Value, CultureInfo.InvariantCulture);
                    point.Rate = Convert.ToDouble(element.Element("Rate").Value, CultureInfo.InvariantCulture);
                    result.Add(point);
                }
            }
            return result;
        }
    }
}
