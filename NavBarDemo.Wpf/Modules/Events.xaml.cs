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
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;

namespace NavBarDemo {
 public partial class Events : NavBarDemoModule {
  public Events() {
   InitializeComponent();
            navBar.PreviewMouseDown += new MouseButtonEventHandler(navBar_MouseDown);
            navBar.PreviewMouseUp += new MouseButtonEventHandler(navBar_MouseUp);
            navBar.MouseMove += new MouseEventHandler(navBar_MouseMove);
            UpdateControlAvailability();
  }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            navBarControl = navBar;
        }
  protected override void SelectView(object sender, RoutedEventArgs e) {
   base.SelectView(sender, e);
            UpdateControlAvailability();
  }

        protected override ExplorerBarView GetExplorerBarView() {
            return (ExplorerBarView)FindResource("explorerBar");
        }
        protected override NavigationPaneView GetNavigationPaneView() {
            return (NavigationPaneView)FindResource("navigationPane");
        }
        protected override SideBarView GetSideBarView() {
            return (SideBarView)FindResource("sideBar");
        }

  void navBar_MouseDown(object sender, RoutedEventArgs e) {
   NavBarGroup group = navBar.View.GetNavBarGroup(e);
   NavBarItem item = navBar.View.GetNavBarItem(e);
   if(group != null || item != null)
    AddToStack("MouseDown: " + (item != null ? GetItemContent(item) : GetGroupHeader(group)), mouseDownCheckbox);
  }
  void navBar_MouseMove(object sender, RoutedEventArgs e) {
   NavBarGroup group = navBar.View.GetNavBarGroup(e);
   NavBarItem item = navBar.View.GetNavBarItem(e);
   if(group != null || item != null)
    AddToStack("MouseMove: " + (item != null ? GetItemContent(item) : GetGroupHeader(group)), mouseMoveCheckbox);
  }
  void navBar_MouseUp(object sender, RoutedEventArgs e) {
   NavBarGroup group = navBar.View.GetNavBarGroup(e);
   NavBarItem item = navBar.View.GetNavBarItem(e);
   if(group != null || item != null)
    AddToStack("MouseUp: " + (item != null ? GetItemContent(item) : GetGroupHeader(group)), mouseUpCheckbox);
  }
  void View_Click(object sender, RoutedEventArgs e) {
   NavBarGroup group = navBar.View.GetNavBarGroup(e);
   NavBarItem item = navBar.View.GetNavBarItem(e);
   if(group != null || item != null)
    AddToStack("Click: " + (item != null ? GetItemContent(item) : GetGroupHeader(group)), clickCheckbox);
  }

  private void Selection_Changing(object sender, NavBarItemSelectingEventArgs e) {
   AddToStack("ItemSelecting: "
    + "PrevGroup '" + GetGroupHeader(e.PrevGroup) + "', PrevItem '" + GetItemContent(e.PrevItem) + "'; "
    + "NewGroup '" + GetGroupHeader(e.NewGroup) + "', NewItem '" + GetItemContent(e.NewItem) + "'", selectionChangingCheckbox);
  }
  private void Selection_Changed(object sender, NavBarItemSelectedEventArgs e) {
   AddToStack("ItemSelected: Group '" + e.Group.Header.ToString() + "', Item '" + e.Item.Content.ToString() + "'", selectionChangedCheckbox);
  }

        void UpdateControlAvailability() {
            groupExpandedChangedCheckbox.IsEnabled = navBar.View is ExplorerBarView;
            groupExpandedChangingCheckbox.IsEnabled = navBar.View is ExplorerBarView;

            navPaneExpandedChangedCheckbox.IsEnabled = navBar.View is NavigationPaneView;
            navPaneExpandedChangingCheckbox.IsEnabled = navBar.View is NavigationPaneView;
        }
  void View_ActiveGroupChanging(object sender, NavBarActiveGroupChangingEventArgs e) {
   AddToStack("ActiveGroupChanging: " + "PrevGroup '" + GetGroupHeader(e.PrevGroup) + "'; " + "NewGroup '" + GetGroupHeader(e.NewGroup) + "'", activeGroupChangingCheckbox);
  }
  void View_ActiveGroupChanged(object sender, NavBarActiveGroupChangedEventArgs e) {
   AddToStack("ActiveGroupChanged: " + GetGroupHeader(e.Group), activeGroupChangedCheckbox);
  }

  void view_GroupExpandedChanging(object sender, NavBarGroupExpandedChangingEventArgs e) {
   AddToStack("GroupExpandedChanging: Group '" + GetGroupHeader(e.Group) + "', IsExpanded=" + e.IsExpanded.ToString(), groupExpandedChangingCheckbox);
  }
  void view_GroupExpandedChanged(object sender, NavBarGroupExpandedChangedEventArgs e) {
   AddToStack("GroupExpandedChanged: Group '" + GetGroupHeader(e.Group) + "', IsExpanded=" + e.IsExpanded.ToString(), groupExpandedChangedCheckbox);
  }

  void navPane_NavPaneExpandedChanging(object sender, NavPaneExpandedChangingEventArgs e) {
   AddToStack("NavPaneExpandedChanging: IsExpanded=" + e.IsExpanded, navPaneExpandedChangingCheckbox);
  }
  void navPane_NavPaneExpandingChanged(object sender, NavPaneExpandedChangedEventArgs e) {
   AddToStack("NavPaneExpandedChanged: IsExpanded=" + e.IsExpanded, navPaneExpandedChangedCheckbox);
  }

  string GetGroupHeader(NavBarGroup group) {
   return group != null ? group.Header.ToString() : "null";
  }
  string GetItemContent(NavBarItem item) {
   return item != null ? item.Content.ToString() : "null";
  }

  Queue<string> logQueue = new Queue<string>();
  const int logEntriesCount = 100;

  void AddToStack(string str, CheckEdit checkEdit) {
            if(!IsLoaded || !(bool)checkEdit.IsChecked)
                return;
   logQueue.Enqueue(str);
   if(logQueue.Count > logEntriesCount)
    logQueue.Dequeue();
   textBox.Text = string.Empty;
   StringBuilder builder = new StringBuilder();
   foreach(string text in logQueue) {
    builder.Append((builder.Length != 0 ? Environment.NewLine : string.Empty) + text);
   }
   textBox.Text = builder.ToString();
            TextBox textBoxCore = (TextBox)LayoutHelper.FindElement(textBox, (element) => (element is TextBox));
            textBoxCore.ScrollToEnd();
  }

  private void Button_Click(object sender, RoutedEventArgs e) {
   logQueue.Clear();
   textBox.Text = String.Empty;
  }
 }
}
