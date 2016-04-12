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

namespace EditorsDemo {
    public partial class DatePickerModule : EditorsDemoModule {
        public DatePickerModule() {
            InitializeComponent();
            InitSources();
            editor.EditValue = DateTime.Today;
        }
        void InitSources() {
            List<MaskWrapper> dateMasks = new List<MaskWrapper>();
            dateMasks.Add(new MaskWrapper() { MaskName = "Short Date", MaskString = "d" });
            dateMasks.Add(new MaskWrapper() { MaskName = "Long Date", MaskString = "D" });
            dateMasks.Add(new MaskWrapper() { MaskName = "Month & Day", MaskString = "m" });
            dateMasks.Add(new MaskWrapper() { MaskName = "Year & Month", MaskString = "y" });
            dateMask.ItemsSource = dateMasks;
        }
        public class MaskWrapper {
            public string MaskName { get; set; }
            public string MaskString { get; set; }
        }
    }
}
