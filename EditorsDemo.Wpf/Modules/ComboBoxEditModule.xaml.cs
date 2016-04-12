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
using System.Windows.Markup;
using System.IO;
using DevExpress.DemoData.Models;
using System.Data.Entity;

namespace EditorsDemo {
    public partial class ComboBoxEditModule : EditorsDemoModule {
        NWindContext context = NWindContext.Create();
        public static readonly string[] separators = new string[] { ",", ";", "/", "-" };
        public ComboBoxEditModule() {
            InitializeComponent();
            InitSources();
        }
        void InitSources() {
            string[] platforms = new string[] { "Win98", "Win2000", "WinNT", "WinXP", "Vista", "Win7" };
            checkedComboBox.ItemsSource = platforms;
            checkedComboBox.SelectedItems.Add(platforms[4]);
            checkedComboBox.SelectedItems.Add(platforms[5]);
            cboSeparators.ItemsSource = separators;

            defaultComboBox.ItemsSource = context.CountriesArray;
            radioComboBox.ItemsSource = context.Categories.ToList();
            nonEditableComboBox.ItemsSource = context.CountriesArray;
        }
        void CheckEdit_Checked(object sender, RoutedEventArgs e) {
            radioComboBox.ItemTemplate = (DataTemplate)Resources["productCategoryTemplate"];
        }
        void CheckEdit_Unchecked(object sender, RoutedEventArgs e) {
            radioComboBox.ClearValue(ComboBoxEdit.ItemTemplateProperty);
        }
    }
    public class BytesToBitmapConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
