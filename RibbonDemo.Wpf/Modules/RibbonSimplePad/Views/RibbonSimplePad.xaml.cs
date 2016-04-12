using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Utils;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Ribbon;
using System.ComponentModel;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using DevExpress.Xpf.DemoBase;
using System.Windows.Interop;
using DevExpress.Xpf.Utils;
using System.Windows.Navigation;

namespace RibbonDemo {

    [CodeFiles(@"Modules/RibbonSimplePad/Views/RibbonSimplePad.xaml;
                 Modules/RibbonSimplePad/Views/RibbonSimplePad.xaml.(cs);
                 Modules/RibbonSimplePad/ViewModels/SimplePadViewModel.(cs);
                 Modules/RibbonSimplePad/Views/BackstageViewPanes.xaml;
                 Modules/RibbonSimplePad/Views/TemplateSelectors.xaml;
                 Modules/RibbonSimplePad/ViewModels/InlineImageViewModel.(cs);
                 Modules/RibbonSimplePad/ViewModels/RecentItemViewModel.(cs);
                 Modules/RibbonSimplePad/ViewModels/RibbonSimplePadOptionsViewModel.(cs);
                 Modules/RibbonSimplePad/Services/BackstageViewService.(cs);
                 Modules/RibbonSimplePad/Services/InlineImageService.(cs);
                 Modules/RibbonSimplePad/TemplateSelectors/InlineImageContentTemplateSelector.(cs);
                 Modules/RibbonSimplePad/TemplateSelectors/TemplateSelectorDictionary.(cs);
                 Modules/RibbonSimplePad/TemplateSelectors/TextMarkerStyleTemplateSelector.(cs);
                 Modules/RibbonSimplePad/Views/BackstageViewCommonStyles.xaml")]
    public partial class RibbonSimplePad : RibbonDemoModule {
        public static readonly DependencyProperty FontEditWidthProperty =
            DependencyProperty.Register("FontEditWidth", typeof(double?), typeof(RibbonSimplePad), new FrameworkPropertyMetadata(null));

        public double? FontEditWidth {
            get { return (double)GetValue(FontEditWidthProperty); }
            set { SetValue(FontEditWidthProperty, value); }
        }

        public RibbonSimplePad() {
            InitializeComponent();
            richControl.Document.Blocks.Add(new Paragraph(new Run("Select the image below to show a contextual tab.")));
            richControl.Document.Blocks.Add(new Paragraph(new InlineUIContainer() { Child = new InlineImage(InlineImageViewModel.Create("/RibbonDemo;component/Images/Clipart/caCompClientEnabled.png")) }));
            ModuleAppear += OnModuleAppear;
            ThemeManager.ThemeChanged += (s,e) => FontEditWidth = ThemeManager.GetIsTouchEnabled(this) ? 90 : 50;
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e) {
            base.OnLoaded(sender, e);
            RibbonControl.SetCurrentValue(RibbonControl.RibbonStyleProperty, GetRibbonStyle());
            RibbonControl.SetCurrentValue(RibbonControl.RibbonTitleBarVisibilityProperty, GetTitleBarVisibility());
        }


        void OnModuleAppear(object sender, RoutedEventArgs e) {
            if(!this.IsInDesignTool()) {
                richControl.SetFocus();
            }
        }
        protected virtual RibbonTitleBarVisibility GetTitleBarVisibility() {
            return RibbonTitleBarVisibility.Auto;
        }
        protected virtual RibbonStyle GetRibbonStyle() {
            return RibbonStyle.Office2010;
        }
        protected override bool CanLeave() {
            RibbonControl.CloseApplicationMenu();
            return base.CanLeave();
        }
    }
}
