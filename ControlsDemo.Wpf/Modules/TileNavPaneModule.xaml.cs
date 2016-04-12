using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.WindowsUI;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
using DevExpress.Mvvm;
namespace ControlsDemo {
    [CodeFile("ViewModels/TileNavBaseViewModel.(cs)")]
    [CodeFile("ViewModels/TileNavPaneViewModel.(cs)")]
    [CodeFile("Modules/Views/TileNavDetailsView.xaml")]
    [CodeFile("Modules/Views/TileNavDetailsView.xaml.(cs)")]
    [CodeFile("Modules/Views/TileNavPaneHomeView.xaml")]
    [CodeFile("Modules/Views/TileNavPaneHomeView.xaml.(cs)")]
    public partial class TileNavPaneModule : ControlsDemoModule {
        public TileNavPaneModule() {
            InitializeComponent();
        }
        protected override void RaiseIsPopupContentInvisibleChanged(DependencyPropertyChangedEventArgs e) {
            base.RaiseIsPopupContentInvisibleChanged(e);
            if(object.Equals(e.NewValue, true)) {
                tileNavPane.CloseFlyout();
            }
        }
    }
}
