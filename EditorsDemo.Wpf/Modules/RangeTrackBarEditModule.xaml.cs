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

namespace EditorsDemo {
    public partial class RangeTrackBarEditModule : EditorsDemoModule {
        public RangeTrackBarEditModule() {
            InitializeComponent();
            DemoModuleControl.OptionsContent.DataContext = editor;
            cbTickPlacement.ItemsSource = new BaseKindHelper<TickPlacement>().GetEnumMemberList();
        }

        private void CheckEdit_Checked(object sender, RoutedEventArgs e) {
            editor.StyleSettings = new TrackBarZoomRangeStyleSettings();
        }
        private void CheckEdit_Unchecked(object sender, RoutedEventArgs e) {
            editor.StyleSettings = new TrackBarRangeStyleSettings();
        }
    }
}
