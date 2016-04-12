using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Ribbon;
using System.Windows;
namespace RibbonDemo {
    public interface IBackstageViewService {
        void Close();
    }
    public class BackstageViewService : ServiceBase, IBackstageViewService {
        public void Close() {
            if((AssociatedObject is RibbonControl))
                ((RibbonControl)AssociatedObject).CloseApplicationMenu();
        }
    }
}
