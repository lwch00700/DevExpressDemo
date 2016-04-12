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
using DevExpress.Xpf.Editors.Validation;

namespace EditorsDemo {
    public partial class ValidationSettings : UserControl {
        public static readonly DependencyProperty FocusedEditorProperty =
            DependencyProperty.Register("FocusedEditor", typeof(DependencyObject), typeof(ValidationSettings), new PropertyMetadata(null, new PropertyChangedCallback(FocusedEditorPropertyChanged)));
        static void FocusedEditorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            d.SetValue(FrameworkElement.DataContextProperty, e.NewValue);
        }
        public DependencyObject FocusedEditor {
            get { return (DependencyObject)GetValue(FocusedEditorProperty); }
            set { SetValue(FocusedEditorProperty, value); }
        }
        public ValidationSettings() {
            InitializeComponent();
        }
    }
}
