using System.ComponentModel;
using DevExpress.Xpf.DemoBase;
using EditorsDemo.ViewModels;

namespace EditorsDemo {
    [CodeFile("ModuleResources/LookUpEditTemplates(.SL).xaml")]
    public partial class ComboBoxEditWithWcfInstantFeedbackSource : EditorsDemoModule {
        public ComboBoxEditWithWcfInstantFeedbackSource() {
            InitializeComponent();
            WCFInstantFeedbackViewModel viewModel = new WCFInstantFeedbackViewModel();
            DataContext = viewModel;
        }

        void viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) {
        }

    }
}
