using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.WindowsUI;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Controls;
using DevExpress.Mvvm;
using DevExpress.Xpf.Navigation;
namespace ControlsDemo {
    [CodeFile("ViewModels/TileNavBaseViewModel.(cs)")]
    [CodeFile("ViewModels/TileBarViewModel.(cs)")]
    [CodeFile("Modules/Views/TileNavDetailsView.xaml")]
    [CodeFile("Modules/Views/TileNavDetailsView.xaml.(cs)")]
    [CodeFile("Modules/Views/TileBarHomeView.xaml")]
    [CodeFile("Modules/Views/TileBarHomeView.xaml.(cs)")]
    [CodeFile("Services/TileBarService.(cs)")]
    public partial class TileBarModule : ControlsDemoModule {
        public TileBarModule() {
            InitializeComponent();
        }
        protected override void RaiseIsPopupContentInvisibleChanged(DependencyPropertyChangedEventArgs e) {
            base.RaiseIsPopupContentInvisibleChanged(e);
            if(object.Equals(e.NewValue, true)) {
                tileBar.CloseFlyout();
            }
        }
    }
}
