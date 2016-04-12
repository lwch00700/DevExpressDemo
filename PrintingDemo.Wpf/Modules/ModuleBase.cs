using System.ComponentModel;
using DevExpress.Xpf.DemoBase;

namespace PrintingDemo {
    public class ModuleBase : DemoModule {
        public ModuleBase() {
            Loaded += ModuleBase_Loaded;
        }

        private void ModuleBase_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            if(!DesignerProperties.GetIsInDesignMode(this) && ViewModel != null) {
                ViewModel.CreateDocument();
            }
        }

        public ModuleViewModelBase ViewModel {
            get {
                return (ModuleViewModelBase)DataContext;
            }
        }

        protected override void Clear() {
            base.Clear();
            if(ViewModel != null) {
                ViewModel.ClearDocument();
            }
        }

        protected virtual bool NeedChangeEditorsTheme { get { return false; } }
    }
}
