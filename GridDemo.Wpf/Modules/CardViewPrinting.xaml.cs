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
using DevExpress.Xpf.Core;

namespace GridDemo {
    [CodeFile("ModuleResources/CardViewPrintingTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/CardViewTemplates(.SL).xaml")]
    public partial class CardViewPrinting : PrintViewGridDemoModule {
        CarsContext context = new CarsContext();
        public CardViewPrinting() {
            InitializeComponent();
            printStyleChooser.SelectedIndex = 0;
            ShowPreviewInNewTab();
            printStyleChooser.SelectedIndex = 1;
            ShowPreviewInNewTab();
        }

        protected override DataViewBase GetExportView() {
            return cardView;
        }

        protected override DXTabControl DXTabControl { get { return tabControl; } }

        private void printStyleChooser_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(cardView == null) return;
            switch(printStyleChooser.SelectedIndex) {
                case 0: cardView.Style = (Style)Resources["DefaultCardViewPrintStyle"];
                    break;
                case 1: cardView.Style = (Style)Resources["UniformCardSizePrintStyle"];
                    break;
            }
        }
        protected override void ShowPreviewInNewTab() {
            ShowPrintPreviewInNewTab(grid, tabControl, string.Format("{0} Style Preview", printStyleChooser.SelectedItem));
        }
        protected void newTabButton_Click(object sender, RoutedEventArgs e) {
            ShowPreviewInNewTab();
        }
        protected void tabControl_TabHidden(object sender, TabControlTabHiddenEventArgs e) {
            DisposePrintPreviewTabContent((DXTabItem)DXTabControl.Items[e.TabIndex]);
        }
    }
}
