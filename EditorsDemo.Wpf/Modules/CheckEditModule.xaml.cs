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
using System.ComponentModel;
using DevExpress.Xpf.Editors.Validation;
using EditorsDemo.Utils;

namespace EditorsDemo {
    public partial class CheckEditModule : EditorsDemoModule {
        public CheckEditModule() {
            InitializeComponent();
            InitSources();
        }
        void InitSources() {
            List<ClickMode> modes = new List<ClickMode>();
            modes.Add(ClickMode.Hover);
            modes.Add(ClickMode.Press);
            modes.Add(ClickMode.Release);
            cboClickMode.ItemsSource = modes;
        }
    }
}
