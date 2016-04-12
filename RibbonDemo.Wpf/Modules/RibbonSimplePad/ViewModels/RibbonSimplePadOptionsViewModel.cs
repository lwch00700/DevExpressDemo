using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Ribbon;
using System.Windows.Media;
using DevExpress.Mvvm.DataAnnotations;

namespace RibbonDemo {
    [POCOViewModel]
    public class RibbonSimplePadOptionsViewModel {
        public virtual RibbonTitleBarVisibility TitleBarVisibility { get; set; }
        public virtual Color PageCategoryColor { get; set; }
        public virtual RibbonQuickAccessToolbarShowMode ToolbarShowMode { get; set; }
        public virtual RibbonPageCategoryCaptionAlignment PageCategoryAlignment { get; set; }
        public virtual RibbonStyle RibbonStyle { get; set; }

        protected RibbonSimplePadOptionsViewModel() { }
        public static RibbonSimplePadOptionsViewModel Create() {
            RibbonSimplePadOptionsViewModel options = ViewModelSource.Create(() => new RibbonSimplePadOptionsViewModel());
            options.TitleBarVisibility = RibbonTitleBarVisibility.Auto;
            options.PageCategoryColor = Colors.Orange;
            options.ToolbarShowMode = RibbonQuickAccessToolbarShowMode.ShowAbove;
            options.PageCategoryAlignment = RibbonPageCategoryCaptionAlignment.Right;
            options.RibbonStyle = RibbonStyle.Office2010;
            return options;
        }
    }
}
