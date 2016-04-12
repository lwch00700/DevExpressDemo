using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TreeListDemo {
    public partial class UnboundColumns : TreeListDemoModule {
        public UnboundColumns() {
            InitializeComponent();
            this.treeList.View.ExpandAllNodes();
        }

        private void showExpressionEditorButton_Click(object sender, RoutedEventArgs e) {
            treeList.View.ShowUnboundExpressionEditor(treeList.Columns[(string)((ListBoxItem)columnsList.SelectedItem).Tag]);
        }
    }
    public class BooleanToVisibilityConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value is Boolean && (bool)value)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
