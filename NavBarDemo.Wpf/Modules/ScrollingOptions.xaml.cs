using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.NavBar;
using DevExpress.Xpf.Editors;

namespace NavBarDemo {
 public partial class ScrollingOptions : NavBarDemoModule {
  static ScrollingOptions() {
  }
  public ScrollingOptions() {
   InitializeComponent();

   GenerateContent(8, 10);
  }

  void GenerateContent(int groupsCount, int itemsCount) {
   navBar.Groups.Clear();
   for(int i = 0; i < groupsCount; i++) {
    NavBarGroup group = new NavBarGroup();
    group.Header = "Group " + (i + 1).ToString();
    for(int j = 0; j < itemsCount; j++) {
     NavBarItem item = new NavBarItem();
     item.Content = "Item " + (j + 1).ToString();
     group.Items.Add(item);
    }
    navBar.Groups.Add(group);
   }
  }
  protected override ExplorerBarView GetExplorerBarView() {
   GenerateContent(8, 10);
   return base.GetExplorerBarView();
  }
  protected override NavigationPaneView GetNavigationPaneView() {
   GenerateContent(5, 30);
   return base.GetNavigationPaneView();
  }
  protected override SideBarView GetSideBarView() {
   GenerateContent(5, 30);
   return base.GetSideBarView();
  }
        protected override void SelectView(object sender, RoutedEventArgs e) {
            base.SelectView(sender, e);
            lbScrollMode.SelectedIndex = 0;
        }
        void lbScrollMode_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            ListBoxEdit lbScrollMode = (ListBoxEdit)sender;
            ScrollMode scrollMode = (ScrollMode)((ListBoxEditItem)lbScrollMode.SelectedItem).Content;
            ScrollingSettings.SetScrollMode(navBar.View, scrollMode);
        }
 }
    public class ScrollModeToVisibilityConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((ScrollMode)value) == ScrollMode.Buttons ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class ViewToEnabledConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return !(value is ExplorerBarView);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
