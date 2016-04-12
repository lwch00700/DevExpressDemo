using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace GridDemo {
    [POCOViewModel]
    public class ServerModeLookUpEditViewModel {
        public virtual List<OrderData> Orders { get; set; }
        public virtual IQueryable People { get; set; }

        protected ServerModeLookUpEditViewModel() {
            Orders = new OrderDataGenerator(200).GetOrders();
        }

        public void OnLoaded() {
            if(string.IsNullOrEmpty(ServerModeOptions.SQLConnectionString))
                ShowConnectionWizard("Return");
            UpdatePeopleSource();
        }
        public void Configure() {
            ShowConnectionWizard("Start Demo");
            UpdatePeopleSource();
        }
        void ShowConnectionWizard(string demoString) {
            if(DevExpress.Xpf.DemoBase.DemoTesting.DemoTestingHelper.IsTesting)
                return;
            SQLConnectionWindow window = new SQLConnectionWindow("Return", new PeopleGeneratorProvider()) { Description = ServerModeOptions.GetLookUpDescription() };
            if(System.Windows.Application.Current != null)
                window.Owner = System.Windows.Application.Current.MainWindow;
            window.ShowDialog();
            ServerModeOptions.SQLConnectionString = window.GetDataBaseConnectionString();
        }

        void UpdatePeopleSource() {
            People = ServerModeOptions.IsCorrectConnection ? new Controls.PersonDataContext(ServerModeOptions.SQLConnectionString).Person : null;
        }
    }
}
