using System.IO;
using System.Reflection;
using System.Windows;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Layout.Core;
using DevExpress.Xpf.Docking;
using DockingDemo.ViewModels;

namespace DockingDemo {
    [CodeFile("ViewModels/SerializationViewModel.(cs)")]
    public partial class MVVMSerialization : DockingDemoModule {
        MVVMSerializationViewModel serializationViewModel;
        public MVVMSerialization() {
            InitializeComponent();
            deserializeButton.IsEnabled = false;
            DataContext = serializationViewModel = new MVVMSerializationViewModel();
        }
        Stream dockManagerStream;
        Stream viewsStream;
        protected virtual void SaveLayout() {
            Ref.Dispose(ref dockManagerStream);
            dockManagerStream = new MemoryStream();
            viewsStream = new MemoryStream();
            serializationViewModel.Store(viewsStream);
            dockManager.SaveLayoutToStream(dockManagerStream);
            deserializeButton.IsEnabled = true;
        }
        protected virtual void RestoreLayout() {
            if(dockManagerStream == null) return;
            dockManagerStream.Seek(0, SeekOrigin.Begin);
            viewsStream.Seek(0, SeekOrigin.Begin);
            serializationViewModel.Restore(viewsStream);
            dockManager.RestoreLayoutFromStream(dockManagerStream);
        }
        void serializeButton_Click(object sender, RoutedEventArgs e) {
            SaveLayout();
        }
        void deserializeButton_Click(object sender, RoutedEventArgs e) {
            RestoreLayout();
        }
        void loadSampleLayoutButton_Click(object sender, RoutedEventArgs e) {
            Assembly thisExe = typeof(Serialization).Assembly;

            string name1 = string.Format("views{0}.xml", (layoutSampleName.SelectedIndex + 1).ToString().ToLower());
            using(Stream resourceStream = AssemblyHelper.GetEmbeddedResourceStream(thisExe, DemoHelper.GetPath("Layouts/MVVMSerialization/", thisExe) + name1, true)) {
                serializationViewModel.Restore(resourceStream);
            }
            string name = string.Format("saved{0}.xml", layoutSampleName.SelectedItem.ToString().ToLower());
            using(Stream resourceStream = AssemblyHelper.GetEmbeddedResourceStream(thisExe, DemoHelper.GetPath("Layouts/MVVMSerialization/", thisExe) + name, true)) {
                dockManager.RestoreLayoutFromStream(resourceStream);
            }
        }
    }
    public class MVVMSerializationLayoutAdapter : ILayoutAdapter {
        #region ILayoutAdapter Members
        string ILayoutAdapter.Resolve(DockLayoutManager owner, object item) {
            BaseLayoutItem panelHost = owner.GetItem("PanelHost");
            if(panelHost == null) {
                panelHost = new LayoutGroup() { Name = "PanelHost", DestroyOnClosingChildren = false };
                owner.LayoutRoot.Add(panelHost);
            }
            return "PanelHost";
        }
        #endregion
    }
}
