using System;
using System.Windows.Controls;

namespace RichEditDemo.Controls {
    public partial class CustomizeMergeFieldsControl : UserControl {
        public CustomizeMergeFieldsControl(MergeFieldNameInfo[] mergeFieldsNames) {
            InitializeComponent();
            grid.ItemsSource = mergeFieldsNames;
        }
    }
}
