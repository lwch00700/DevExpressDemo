﻿using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Ribbon;

namespace RibbonDemo {
    [NoAutogeneratedCodeFiles]
    public class RibbonOfficeSlim : RibbonSimplePad {
        public RibbonOfficeSlim() { }

        protected override RibbonTitleBarVisibility GetTitleBarVisibility() {
            return RibbonTitleBarVisibility.Collapsed;
        }
        protected override RibbonStyle GetRibbonStyle() {
            return RibbonStyle.OfficeSlim;
        }
    }
}
