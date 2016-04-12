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
using EditorsDemo.Utils;
using DevExpress.Xpf.Editors;
using System.Windows.Markup;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace EditorsDemo {
    public partial class ColorEditModule : EditorsDemoModule {
        public ColorEditModule() {
            InitializeComponent();
            InitSources();
        }
        void InitSources() {
            List<ChipSize> chipSizeSource = new List<ChipSize>();
            chipSizeSource.Add(ChipSize.Default);
            chipSizeSource.Add(ChipSize.Large);
            chipSizeSource.Add(ChipSize.Medium);
            chipSizeSource.Add(ChipSize.Small);
            chkChipSize.ItemsSource = chipSizeSource;
        }
    }
}
