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

namespace RichEditDemo {
    public partial class SelectProductControl : PopupControlBase {
        public static readonly DependencyProperty ProductsProperty;

        static SelectProductControl() {
            ProductsProperty = DependencyProperty.Register("Products", typeof(List<string>), typeof(SelectProductControl), new PropertyMetadata(null));
        }

        public SelectProductControl() {
            InitializeComponent();
        }
        public SelectProductControl(List<string> list) {
            Guard.ArgumentNotNull(list, "list");
            Products = list;
            InitializeComponent();
            Dispatcher.BeginInvoke(new Action(() => this.listBox.Focus()));
        }

        public List<string> Products {
            get { return (List<string>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        protected override void SetEditValue() {
            SetEditValueCore((string)this.listBox.SelectedItem);
        }
        protected override void OnOwnerWindowChanged() {
            if (OwnerWindow != null)
                OwnerWindow.Caption = "Select a product";
        }
        private void listBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            if (this.listBox.SelectedIndex >= 0)
                PerformCommit();
        }
    }
}
