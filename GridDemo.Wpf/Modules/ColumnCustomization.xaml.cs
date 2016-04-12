using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.DemoBase;
using System.Windows.Input;
using System.Windows.Threading;

namespace GridDemo {
    [CodeFile("ModuleResources/ColumnCustomizationClasses.(cs)")]
    public partial class ColumnCustomization : GridDemoModule {
        public static readonly DependencyProperty ColumnChooserTypeProperty = DependencyProperty.Register("ColumnChooserType", typeof(ColumnChooserType), typeof(ColumnCustomization),
            new PropertyMetadata(ColumnChooserType.Default, new PropertyChangedCallback(OnColumnChooserTypeChanged)));

        static void OnColumnChooserTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ColumnCustomization)d).ColumnChooserTypeChanged((ColumnChooserType)e.NewValue);
        }

        public ColumnChooserType ColumnChooserType {
            get { return (ColumnChooserType)GetValue(ColumnChooserTypeProperty); }
            set { SetValue(ColumnChooserTypeProperty, value); }
        }

        public ColumnCustomization() {
            InitializeComponent();
            UpdateToggleButtonContent();
        }
        protected override void RaiseIsPopupContentInvisibleChanged(DependencyPropertyChangedEventArgs e) {
            base.RaiseIsPopupContentInvisibleChanged(e);
            if(!IsPopupContentInvisible) {
                Dispatcher.BeginInvoke(new Action(gridView.ShowColumnChooser), DispatcherPriority.Render, null);

            }
        }

        void columnChooser_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) {
            e.Handled = true;
        }

        void ColumnChooserTypeChanged(ColumnChooserType newChooserType) {
            if(newChooserType == ColumnChooserType.Default) {
                columnChooser.View = null;
                grid.View.ClearValue(GridViewBase.ColumnChooserFactoryProperty);
            }
            else {
                columnChooser.View = gridView;
                grid.View.ColumnChooserFactory = new DemoColumnChooser(columnChooser);
                gridView.ShowColumnChooser();
            }
        }

        private void showHideButton_Toggle(object sender, RoutedEventArgs e) {
            UpdateToggleButtonContent();
        }

        void UpdateToggleButtonContent() {
            showHideButton.Content = showHideButton.IsChecked.Value ? "Hide Column Chooser" : "Show Column Chooser";
        }
        protected override object GetModuleDataContext() {
            base.GetModuleDataContext();
            return this;
        }

        private void customColumnChooserExpander_Expanded(object sender, RoutedEventArgs e) {
            customColumnChooserExpander.Header = "Hide Column Chooser";
        }

        private void customColumnChooserExpander_Collapsed(object sender, RoutedEventArgs e) {
            customColumnChooserExpander.Header = "Show Column Chooser";
        }
    }
}
