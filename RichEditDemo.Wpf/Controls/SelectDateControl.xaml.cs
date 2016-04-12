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

namespace RichEditDemo {
    public partial class SelectDateControl : PopupControlBase {
        public SelectDateControl() {
            InitializeComponent();
            Dispatcher.BeginInvoke(new Action(() => this.dateEdit.Focus()));
            this.dateEdit.DateTime = DateTime.Now;
        }

        protected override void OnOwnerWindowChanged() {
            if (OwnerWindow != null)
                OwnerWindow.Caption = "Select a date";
        }
        private void ButtonInfo_Click(object sender, RoutedEventArgs e) {
            PerformCommit();
        }
        protected override void SetEditValue() {
            SetEditValueCore(this.dateEdit.DateTime);
        }
    }
}
