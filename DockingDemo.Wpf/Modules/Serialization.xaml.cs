using System.IO;
using System.Reflection;
using System.Windows;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Layout.Core;

namespace DockingDemo {
    public partial class Serialization : DockingDemoModule {
        public Serialization() {
            InitializeComponent();
        }
        Stream memoryStream;
        protected virtual void SaveLayout() {
            Ref.Dispose(ref memoryStream);
            memoryStream = new MemoryStream();
            dockManager.SaveLayoutToStream(memoryStream);
            deserializeButton.IsEnabled = true;
        }
        protected virtual void RestoreLayout() {
            if(memoryStream == null) return;
            memoryStream.Seek(0, SeekOrigin.Begin);
            dockManager.RestoreLayoutFromStream(memoryStream);
        }
        void serializeButton_Click(object sender, RoutedEventArgs e) {
            SaveLayout();
        }
        void deserializeButton_Click(object sender, RoutedEventArgs e) {
            RestoreLayout();
        }
        void loadSampleLayoutButton_Click(object sender, RoutedEventArgs e) {
            Assembly thisExe = typeof(Serialization).Assembly;
            string name = string.Format("{0}.xml", layoutSampleName.SelectedItem.ToString().ToLower());
            using(Stream resourceStream = AssemblyHelper.GetEmbeddedResourceStream(thisExe, DemoHelper.GetPath("Layouts/", thisExe) + name, true)) {
                dockManager.RestoreLayoutFromStream(resourceStream);
            }
        }
    }
}
