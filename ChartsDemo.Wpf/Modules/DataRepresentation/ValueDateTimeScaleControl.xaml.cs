using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataRepresentation/ValueDateTimeScaleControl(.SL).xaml"),
     CodeFile("Modules/DataRepresentation/ValueDateTimeScaleControl.xaml.(cs)")]
    public partial class ValueDateTimeScaleControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public ValueDateTimeScaleControl() {
            InitializeComponent();
            chart.Diagram.Series[0].DataSource = CreateDataSource();
            series.ToolTipPointPattern = "Movie: {A}\nRelease Date: {V:d}";
        }
        void ChartsDemoModule_ModuleAppear(object sender, System.Windows.RoutedEventArgs e) {
            chart.Animate();
        }
        List<Movie> CreateDataSource() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/Movies.xml");
            List<Movie> result = new List<Movie>();
            if (document != null) {
                foreach (XElement element in document.Element("Movies").Elements()) {
                    Movie movie = new Movie();
                    movie.Name = element.Element("Name").Value;
                    movie.ProductionBudget = Convert.ToDouble(element.Element("ProductionBudget").Value, CultureInfo.InvariantCulture);
                    movie.WorlwideGrosses = Convert.ToDouble(element.Element("WorlwideGrosses").Value, CultureInfo.InvariantCulture);
                    movie.ReleaseDate = element.Element("ReleaseDate").Value;
                    result.Add(movie);
                }
            }
            return result;
        }
    }

    public class Movie {
        string name;
        string releaseDate;
        double productionBudget;
        double worlwideGrosses;

        public string Name { get { return name; } set { name = value; } }
        public string ReleaseDate { get { return releaseDate; } set { releaseDate = value; } }
        public double ProductionBudget { get { return productionBudget; } set { productionBudget = value; } }
        public double WorlwideGrosses { get { return worlwideGrosses; } set { worlwideGrosses = value; } }
    }
}
