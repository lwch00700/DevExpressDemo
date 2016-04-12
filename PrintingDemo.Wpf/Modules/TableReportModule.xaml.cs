using System;
using System.IO;
using System.Windows;
using System.Windows.Data;
using DevExpress.Demos.DataSources;
using DevExpress.Utils;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting;
using CreateAreaEventArgs = DevExpress.Xpf.Printing.CreateAreaEventArgs;

namespace PrintingDemo {
    public partial class TableReportModule : ModuleBase {
        public TableReportModule() {
            InitializeComponent();

            var resources = this.Resources["detailTextStyle"] as Style;

            Binding borderDashStyleBinding = new Binding();
            borderDashStyleBinding.Source = BorderStyleService.Instance;
            PropertyPath path = new PropertyPath(BorderStyleService.BorderDashStyleProperty);
            borderDashStyleBinding.Path = path;

            resources.Setters.Add(new Setter(ExportSettings.BorderDashStyleProperty, borderDashStyleBinding));
        }
    }

    public class BorderStyleService: DependencyObject {
        public BorderDashStyle BorderDashStyle {
            get { return (BorderDashStyle)GetValue(BorderDashStyleProperty); }
            set { SetValue(BorderDashStyleProperty, value); }
        }
        public static readonly DependencyProperty BorderDashStyleProperty =
            DependencyProperty.Register("BorderDashStyle", typeof(BorderDashStyle), typeof(BorderStyleService), new PropertyMetadata(BorderDashStyle.Solid));

        readonly static BorderStyleService instance = new BorderStyleService();
        public static BorderStyleService Instance {
            get { return instance; }
        }
    }

    public class TableReportModuleViewModel : ModuleViewModelBase {
        Fishes fishes;
        Fishes Fishes {
            get {
                if(fishes == null) {
                    Stream stream = AssemblyHelper.GetResourceStream(typeof(TableReportModuleViewModel).Assembly, "Data/biolife.txt", true);
                    fishes = new Fishes(stream);
                }
                return fishes;
            }
        }
        Array borderDashStyleValues;

        public Array BorderDashStyleValues {
            get { if(borderDashStyleValues == null)
                borderDashStyleValues = Enum.GetValues(typeof(BorderDashStyle));
            return borderDashStyleValues;
            }
        }

        public BorderDashStyle BorderDashStyle {
            get { return BorderStyleService.Instance.BorderDashStyle; }
            set {
                if(Enum.Equals(BorderStyleService.Instance.BorderDashStyle, value))
                    return;
                BorderStyleService.Instance.BorderDashStyle = value;
                RaisePropertyChanged("BorderDashStyle");
                CreateDocument();
            }
        }

        protected override TemplatedLink CreateLink() {
            SimpleLink link = new SimpleLink();
            link.PageHeaderTemplate = PageHeaderTemplate;
            link.DetailTemplate = DetailTemplate;
            link.PageFooterTemplate = PageFooterTemplate;
            link.DetailCount = this.Fishes.Count;
            link.CreateDetail += link_CreateDetail;
            link.DocumentName = "Fishes";
            return link;

        }
        void link_CreateDetail(object sender, CreateAreaEventArgs e) {
            e.Data = Fishes[e.DetailIndex];
        }
    }
}
