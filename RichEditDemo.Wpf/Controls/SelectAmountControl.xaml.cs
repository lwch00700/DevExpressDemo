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
using DevExpress.Xpf.Editors;

namespace RichEditDemo {
    public partial class SelectAmountControl : PopupControlBase {
        public SelectAmountControl() {
            InitializeComponent();
            this.calcEdit.EditValue = 0M;
            Dispatcher.BeginInvoke(new Action(() => this.calcEdit.Focus()));
            SetEditValueCore(0M);
        }

        void calculator_ValueChanged(object sender, CalculatorValueChangedEventArgs e) {
            SetEditValue();
        }
        protected override void SetEditValue() {
            if (this.calcEdit.Value > 0) {
                SetEditValueCore(this.calcEdit.Value);
                return;
            }
        }
        protected override void OnOwnerWindowChanged() {
            if (OwnerWindow != null)
                OwnerWindow.Caption = "Enter the amount";
        }
        private void ButtonInfo_Click(object sender, RoutedEventArgs e) {
            PerformCommit();
        }
    }
}
