using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Editors;

namespace DockingDemo {
    public partial class ItemsVisibility : DockingDemoModule {
        #region static
        public static readonly DependencyProperty SelectedEmployeeProperty;
        static ItemsVisibility() {
            SelectedEmployeeProperty = DependencyProperty.Register("SelectedEmployee", typeof(Employee), typeof(ItemsVisibility), new PropertyMetadata(null));
        }
        #endregion static
        public Employee SelectedEmployee {
            get { return (Employee)GetValue(SelectedEmployeeProperty); }
            set { SetValue(SelectedEmployeeProperty, value); }
        }
        public ItemsVisibility() {
            InitializeComponent();
            dockManager.Loaded += dockManager_Loaded;
            SelectedEmployee = list[0];
            Binding currentTeamBinding = new Binding()
            {
                Path = new PropertyPath("SelectedEmployee"),
                Source = this
            };
            dockManager.SetBinding(FrameworkElement.DataContextProperty, currentTeamBinding);
        }
        void dockManager_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            UpdateItemsVisibility();
        }
        void UpdateItemsVisibility() {
            BaseLayoutItem[] items = dockManager.GetItems();
            foreach(BaseLayoutItem item in items) {
                LayoutControlItem controlItem = item as LayoutControlItem;
                if(controlItem != null && controlItem.Control is TextEdit) {
                    TextEdit edit = (TextEdit)controlItem.Control;
                    edit.IsReadOnly = !editMode;
                    if(editMode || edit.EditValue != null)
                        controlItem.Visibility = Visibility.Visible;
                    else
                        controlItem.Visibility = Visibility.Collapsed;
                }
            }
        }
        bool editMode = false;
        void Button_Click(object sender, RoutedEventArgs e) {
            Button buttonEdit = (Button)sender;
            editMode = !editMode;
            if(editMode) {
                buttonEdit.Content = "End edit";
            }
            else {
                buttonEdit.Content = "Start edit";
            }
            UpdateItemsVisibility();
        }
        int currentPerson = 0;
        void ButtonPrev_Click(object sender, RoutedEventArgs e) {
            currentPerson--;
            if(currentPerson < 0) currentPerson = list.Count - 1;
            SelectedEmployee = list[currentPerson % list.Count];
            UpdateItemsVisibility();

        }
        void ButtonNext_Click(object sender, RoutedEventArgs e) {
            SelectedEmployee = list[++currentPerson % list.Count];
            UpdateItemsVisibility();
        }
        List<Employee> list = Employee.CreateSampleData();
    }
}
