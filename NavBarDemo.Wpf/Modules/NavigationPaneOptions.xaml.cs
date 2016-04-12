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
using DevExpress.Xpf.NavBar;

namespace NavBarDemo {
 public partial class NavigationPaneOptions : NavBarDemoModule {
  public NavigationPaneOptions() {
   InitializeComponent();
   calendar.DateTime = DateTime.Today;
  }
  private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
   navBar.HorizontalAlignment = object.Equals(((ListBox)sender).SelectedItem, ExpandButtonMode.Normal) ? HorizontalAlignment.Left : HorizontalAlignment.Right;
  }
 }

    public class ImageTreeViewItem  {
        public string Header { get; set; }
        public string ImageSource { get; set; }
    }
}
