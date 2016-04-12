using System;
using System.Windows;
using System.Windows.Data;
using GridDemo;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Utils;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.DemoBase;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    [CodeFile("ModuleResources/AutoFilterRowTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/AutoFilterRowClasses.(cs)")]
 public partial class AutoFilterRow : GridDemoModule {
  public AutoFilterRow() {
   InitializeComponent();
   grid.ItemsSource = OutlookDataGenerator.CreateOutlookDataTable(1000);
            ComboBoxEditSettings settings = new ComboBoxEditSettings() { IsTextEditable = false };
            ComboBoxEdit.SetupComboBoxSettingsEnumItemSource<Priority>(settings);
            colPriority.EditSettings = settings;
  }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
 }
}
