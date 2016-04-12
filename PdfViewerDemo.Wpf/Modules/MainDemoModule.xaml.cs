using DevExpress.DemoData.Utils;
using DevExpress.Xpf.DemoBase;
using DevExpress.Mvvm.POCO;
using System.Reflection;

namespace PdfViewerDemo {
    [CodeFile("ViewModels/MainViewModel.(cs)")]
    public partial class MainDemoModule : PdfViewerDemoModule {
        public MainDemoModule() {
            InitializeComponent();
            var currentAssembly = Assembly.GetExecutingAssembly();
            DataContext = ViewModelSource.Create(() =>
                new MainViewModel { PdfStream = AssemblyHelper.GetResourceStream(currentAssembly, "Data/Demo.pdf", true) });
        }
    }
}
