using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DockingDemo.MVVM;

namespace DockingDemo {
    public partial class VS2010Docking : DockingDemoModule {
        public VS2010Docking() {
            InitializeComponent();
            this.DataContext = new VS2010MainWindowViewModel();
        }
        void bHome_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            NavigateHomePage();
        }
        void bAbout_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            ShowAbout();
        }
    }
}
