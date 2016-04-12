using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.DemoBase;
using DevExpress.DemoData.Models;
using System.Data.Entity;
using System.Linq;

namespace GridDemo {
    [CodeFile("ModuleResources/CardViewTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/CardViewPrintingTemplates(.SL).xaml")]
    public partial class CardView : GridDemoModule {
        CarsContext context = new CarsContext();
        public CardView() {
            InitializeComponent();
            grid.ItemsSource = context.Cars.ToList();
        }

        private void maxCardCountInRowSpinEdit_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            if(view == null)
                return;
            if(maxCardCountInRowValueRadioButton.IsChecked.Value)
                view.MaxCardCountInRow = (int)maxCardCountInRowSpinEdit.Value;
        }

        private void maxCardCountInRowNoLimitRadioButton_Checked(object sender, RoutedEventArgs e) {
            if(view == null)
                return;
            maxCardCountInRowSpinEdit.IsEnabled = false;
            view.MaxCardCountInRow = int.MaxValue;
        }

        private void maxCardCountInRowValueRadioButton_Checked(object sender, RoutedEventArgs e) {
            if(view == null)
                return;
            maxCardCountInRowSpinEdit.IsEnabled = true;
            view.MaxCardCountInRow = (int)maxCardCountInRowSpinEdit.Value;
        }

        private void ComboBoxEdit_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            MemoEditSettings settings = new MemoEditSettings()
            {
                ShowIcon = false,
                PopupWidth = 500,
                PopupHeight = 300,
                MemoTextWrapping = TextWrapping.Wrap,
                MemoVerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };
            if(object.Equals((CardLayout)e.NewValue, CardLayout.Rows))
                settings.MaxWidth = 300d;
            grid.Columns["Description"].EditSettings = settings;

        }
    }
}
