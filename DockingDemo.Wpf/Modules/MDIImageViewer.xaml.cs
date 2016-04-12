using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;
using Microsoft.Win32;
using System.Windows.Data;

namespace DockingDemo {
    public partial class MDIImageViewer : DockingDemoModule {
        public MDIImageViewer() {
            InitializeComponent();
            DocumentPanel pane1 = OpenImage(new Uri("/DockingDemo;component/Images/Audi_TT_Roadster.jpg", UriKind.Relative));
            DocumentPanel pane2 = OpenImage(new Uri("/DockingDemo;component/Images/BMW_760i_Sedan.jpg", UriKind.Relative));
            DocumentPanel.SetMDILocation(pane1, new System.Windows.Point(50, 50));
            DocumentPanel.SetMDILocation(pane2, new System.Windows.Point(250, 150));
            dockManager.DockItemActivated += OnDockItemActivated;
            DataContext = new CommandsModel() { Target = dockManager.GetItem("mdiContainer") };

        }
        protected override void RaiseAfterModuleDisappear() {
            base.RaiseAfterModuleDisappear();
            ClearValue(DataContextProperty);
        }
        void OnDockItemActivated(object sender, DockItemActivatedEventArgs ea) {
            DocumentPanel panel = ea.Item as DocumentPanel;
            if(panel != null)
                bFileName.Content = Path.GetFileName(panel.Caption as string);
            else bFileName.Content = null;
        }
        DocumentPanel OpenImage(Uri uri) {
            BitmapImage img = null;
            try {
                img = new BitmapImage(uri);
            }
            catch {
                System.Windows.MessageBox.Show("Wrong picture format");
            }
            return (img != null) ? CreateImageWindow(uri.OriginalString, img) : null;
        }
        DocumentPanel CreateImageWindow(string caption, ImageSource source) {
            DocumentPanel panel = dockManager.DockController.AddDocumentPanel(mdiContainer);
            panel.Content = new Image() { Source = source, Margin = new System.Windows.Thickness(5) };
            panel.Caption = caption;
            return panel;
        }
        void bOpen_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All Picture Files |*.bmp;*.gif;*.jpg;*.jpeg;*.ico;*.png";
            bool? result = ofd.ShowDialog();
            if(result.HasValue && result.Value) {
                OpenImage(new Uri(ofd.FileName, UriKind.Absolute));
            }
        }
        void bHome_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            NavigateHomePage();
        }
        void bAbout_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            ShowAbout();
        }
    }
}
