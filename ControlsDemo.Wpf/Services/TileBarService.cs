using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Navigation;

namespace ControlsDemo {
    public interface ITileBarService {
        void CloseFlyout();
    }
    public class TileBarService : DevExpress.Mvvm.UI.ServiceBase, ITileBarService {
        public void CloseFlyout() {
            TileBar tileBar = AssociatedObject as TileBar;
            if(tileBar != null)
                tileBar.CloseFlyout();
        }
    }
}
