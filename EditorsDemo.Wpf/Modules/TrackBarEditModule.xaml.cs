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
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using System.Windows.Markup;
using EditorsDemo.Utils;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace EditorsDemo {
    public partial class TrackBarEditModule : EditorsDemoModule {
        public TrackBarEditModule() {
            InitializeComponent();
            DemoModuleControl.OptionsContent.DataContext = editor;
            InitSources();
        }
        void InitSources() {
            cbTickPlacement.ItemsSource = new TickPlacement[] { TickPlacement.Both, TickPlacement.BottomRight, TickPlacement.None, TickPlacement.TopLeft };
        }
        void CheckEdit_Checked(object sender, RoutedEventArgs e) {
            editor.StyleSettings = new TrackBarZoomStyleSettings();
        }
        void CheckEdit_Unchecked(object sender, RoutedEventArgs e) {
            editor.StyleSettings = new TrackBarStyleSettings();
        }
    }
}
