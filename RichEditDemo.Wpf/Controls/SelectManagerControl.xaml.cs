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
using DevExpress.Utils;
using DevExpress.Office.Utils;

namespace RichEditDemo {
    public partial class SelectManagerControl : PopupControlBase {
        public static readonly DependencyProperty ManagersProperty;
        public static readonly DependencyProperty InfosProperty;

        static SelectManagerControl() {
            ManagersProperty = DependencyProperty.Register("Managers", typeof(List<string>), typeof(SelectManagerControl), new PropertyMetadata(null));
            InfosProperty = DependencyProperty.Register("Infos", typeof(List<string>), typeof(SelectManagerControl), new PropertyMetadata(null));
        }

        public SelectManagerControl() {
            InitializeComponent();
        }
        public SelectManagerControl(List<string> managers, List<string> infos) {
            Guard.ArgumentNotNull(infos, "infos");
            if (managers.Count != infos.Count)
                Exceptions.ThrowArgumentException("infos", infos);
            Infos = infos;
            Managers = managers;
            InitializeComponent();
            Dispatcher.BeginInvoke(new Action(() => this.listBox.Focus()));
        }

        public List<string> Managers {
            get { return (List<string>)GetValue(ManagersProperty); }
            set { SetValue(ManagersProperty, value); }
        }
        public List<string> Infos {
            get { return (List<string>)GetValue(InfosProperty); }
            set { SetValue(InfosProperty, value); }
        }

        protected override void SetEditValueCore(object value) {
            string item = (string)value;
            int index = Managers.IndexOf(item);
            if (index < 0)
                return;
            base.SetEditValueCore(String.Format("{0}, {1}", item, Infos[index]));
        }
        protected override void SetEditValue() {
            SetEditValueCore((string)this.listBox.SelectedItem);
        }
        protected override void OnOwnerWindowChanged() {
            if (OwnerWindow != null)
                OwnerWindow.Caption = "Select a manager";
        }
        private void listBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            if (this.listBox.SelectedIndex >= 0)
                PerformCommit();
        }
    }
}
