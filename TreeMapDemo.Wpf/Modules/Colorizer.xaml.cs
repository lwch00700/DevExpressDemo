using System;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Windows.Data;
using DevExpress.Xpf.TreeMap;

namespace TreeMapDemo {
    public partial class Colorizer : TreeMapDemoModule {
        public Colorizer() {
            InitializeComponent();
            DataContext = DataLoader.LoadXmlDocumentFromResources("/Data/USLargestCompanies2011.xml");
        }
    }

    public class ColorizersCollection : ObservableCollection<ColorizerInfo> { }

    public class ColorizerInfo {
        public TreeMapColorizerBase Colorizer { get; set; }
        public string ColorizerName { get; set; }
        public object Data { get; set; }
        public GroupDefinitionCollection Groups { get; set; }
        public override string ToString() {
            return ColorizerName;
        }
    }

    public class BoolToObjectConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool && ((bool)value))
                return parameter;
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
