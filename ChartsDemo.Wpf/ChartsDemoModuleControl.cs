using System;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Mvvm.Native;

namespace ChartsDemo {
    public class ChartsDemoModule : DemoModule {
        public const double ToolTipOffset = 10;

        public virtual ChartControl ActualChart {
            get {
                FieldInfo field = GetType().GetField("PART_ActualChart", BindingFlags.Instance | BindingFlags.NonPublic);
                if(field != null)
                    return (ChartControl)field.GetValue(this);
                return null;
            }
        }
        protected override object GetModuleDataContext() { return null; }
        public override bool SupportSidebarContent() {
            return true;
        }
        public override object GetSidebarContent() {
            return new PaletteChooser(ActualChart);
        }
        public override void UpdateSidebarContent(object sidebarContent) {
            base.UpdateSidebarContent(sidebarContent);
            PaletteChooser _paletteChooser = sidebarContent as PaletteChooser;
            if(_paletteChooser != null && ActualChart != null)
                _paletteChooser.UpdateChart(ActualChart);
        }

        public override ImageSource GetSidebarIcon() {
            return new BitmapImage(new Uri("/ChartsDemo;component/Images/Palette.png", UriKind.Relative));
        }
        public override ImageSource GetSidebarIconSelected() {
            return new BitmapImage(new Uri("/ChartsDemo;component/Images/PaletteSelected.png", UriKind.Relative));
        }
        public override string GetSidebarTag() {
            return "Palette";
        }
    }
}
