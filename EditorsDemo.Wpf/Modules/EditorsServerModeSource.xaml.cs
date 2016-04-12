using System;
using System.ComponentModel;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using EditorsDemo.ViewModels;
using GridDemo;
using GridDemo.Controls;
using System.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System.Collections.Generic;

namespace EditorsDemo {
    [CodeFile("ModuleResources/LookUpEditTemplates(.SL).xaml")]
    public partial class EditorsServerModeSource : EditorsDemoModule {
        public EditorsServerModeSource() {
            InitializeComponent();
        }
        protected override object GetModuleDataContext() {
            return DataContext;
        }
    }
    [POCOViewModel]
    public class EditorsServerModeViewModel {
        public virtual IQueryable People { get; set; }

        public void OnLoaded() {
            if (string.IsNullOrEmpty(ServerModeOptions.SQLConnectionString))
                ShowConnectionWizard("Return");
            UpdatePeopleSource();
        }
        public void Configure() {
            ShowConnectionWizard("Start Demo");
            UpdatePeopleSource();
        }
        void ShowConnectionWizard(string demoString) {
            if (DevExpress.Xpf.DemoBase.DemoTesting.DemoTestingHelper.IsTesting)
                return;
            SQLConnectionWindow window = new SQLConnectionWindow("Return", new PeopleGeneratorProvider()) { Description = ServerModeOptions.GetComboBoxDescription() };
            if (System.Windows.Application.Current != null)
                window.Owner = System.Windows.Application.Current.MainWindow;
            window.ShowDialog();
            ServerModeOptions.SQLConnectionString = window.GetDataBaseConnectionString();
        }

        void UpdatePeopleSource() {
            People = ServerModeOptions.IsCorrectConnection ? new PersonDataContext(ServerModeOptions.SQLConnectionString).Person : null;
        }
    }
}
