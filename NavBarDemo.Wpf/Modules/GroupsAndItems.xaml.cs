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
using System.Collections;

namespace NavBarDemo {
 public partial class GroupsAndItems : NavBarDemoModule {
  ArrayList images;
        NavBarItem newItem = null;
  public GroupsAndItems() {
   InitializeComponent();
   images = (ArrayList)FindResource("Images");
  }

        private void Add_Item(object sender, RoutedEventArgs e) {
   NavBarGroup group = (NavBarGroup)navBar.ActiveGroup;
            if (newItem != null) {
                newItem.IsEnabled = true;
                newItem = null;
                AddPreviewItem();
                return;
            }
   group.Items.Add(new NavBarItem() { Content = "Item " + (group.Items.Count + 1).ToString(), ImageSource=((Image)images[newItemImageList.SelectedIndex]).Source });
  }
  private void Add_Group(object sender, RoutedEventArgs e) {
   NavBarGroup group = new NavBarGroup() { Header = "Group " + (navBar.Groups.Count + 1).ToString(), ImageSource = ((Image)images[newItemImageList.SelectedIndex]).Source };
   if(newGroupImageStyleList.SelectedItem.ToString() == "Large Images") {
                group.ItemLayoutSettings = new LayoutSettings { ImageHorizontalAlignment = System.Windows.HorizontalAlignment.Center, TextHorizontalAlignment = System.Windows.HorizontalAlignment.Center, ImageDocking = Dock.Top };
                group.ItemImageSettings = new ImageSettings { Height = 32, Width = 32 };
   } else {
                group.ItemImageSettings = new ImageSettings { Height = 16, Width = 16 };
   }
   navBar.Groups.Add(group);
  }
  private void Delete_Group(object sender, RoutedEventArgs e) {
   if(navBar.Groups.Count != 0) navBar.Groups.RemoveAt(navBar.Groups.Count - 1);
  }
  private void Delete_Item(object sender, RoutedEventArgs e) {
   NavBarGroup group = (NavBarGroup)navBar.ActiveGroup;
   group.Items.Remove(group.SelectedItem);
  }

        void Add_MouseEnter(object sender, MouseEventArgs e) {
            AddPreviewItem();
        }

        private void AddPreviewItem() {
            NavBarGroup group = (NavBarGroup)navBar.ActiveGroup;
            if (group == null) return;
            newItem = new NavBarItem() { Content = "Item " + (group.Items.Count + 1).ToString(), ImageSource = ((Image)images[newItemImageList.SelectedIndex]).Source, IsEnabled = false };
            group.Items.Add(newItem);
        }
        void Add_MouseLeave(object sender, MouseEventArgs e) {
            NavBarGroup group = (NavBarGroup)navBar.ActiveGroup;
            if (group == null || newItem==null) return;
            group.Items.Remove(newItem);
        }

 }
}
