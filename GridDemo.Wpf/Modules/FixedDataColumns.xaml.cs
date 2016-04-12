using System;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Core.Native;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Input;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.DemoBase;
using DevExpress.Mvvm;

namespace GridDemo {
    [CodeFile("ModuleResources/RoutedEventsHelper.(cs)")]
    [CodeFile("ModuleResources/FixedDataColumnsTemplates(.SL).xaml")]
    [CodeFile("Controls/Converters.(cs)")]
    public partial class FixedDataColumns : GridDemoModule {
        public FixedDataColumns() {
            ClosePopupCommand = new DelegateCommand<RoutedEventHandlerArgs>(ClosePopup);
            DataContext = this;
            InitializeComponent();
        }
        private void ClosePopup(RoutedEventHandlerArgs obj) {
            RadioButtonList_SelectionChanged(obj.Sender, (EditValueChangedEventArgs)obj.Args);
        }
        private void RadioButtonList_SelectionChanged(object sender, EditValueChangedEventArgs e) {
            FrameworkElement popupRoot = LayoutHelper.FindRoot((DependencyObject)sender) as FrameworkElement;
            if((popupRoot != null) && (popupRoot.Parent is Popup))
                (popupRoot.Parent as Popup).IsOpen = false;
        }
        public ICommand ClosePopupCommand { get; private set; }
    }
}
