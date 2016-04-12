using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GridDemo.Controls;
using System.Data.EntityClient;
using System.Data.Common;
using DevExpress.DemoData.Helpers;
using System.Data;
using DevExpress.Xpf.DemoBase;
using DevExpress.DemoData.Models;
using System.Data.Entity;
using DevExpress.DemoData;

namespace GridDemo {
    public partial class MasterDetailViewViaEntityFramework : GridDemoModule {
        public MasterDetailViewViaEntityFramework() {
            InitializeComponent();
            try {
                grid.ItemsSource = new NWindDataLoader().Customers;
            } catch(EntityException e) {
                MessageBox.Show("Connection could not be established." + Environment.NewLine + e.Message, "Connection Error", MessageBoxButton.OK);
            }
        }
    }
}
