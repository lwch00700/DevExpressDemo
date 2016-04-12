using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core;
using DevExpress.DemoData.Helpers;

using System.Data.EntityClient;

namespace GridDemo {
    public class DemoValuesProvider {
        public IEnumerable<CardLayout> CardLayouts { get { return DevExpress.Utils.EnumExtensions.GetValues(typeof(CardLayout)).Cast<CardLayout>(); } }
        public IEnumerable<Alignment> Alignments { get { return DevExpress.Utils.EnumExtensions.GetValues(typeof(Alignment)).Cast<Alignment>(); } }
        public IEnumerable<GridViewNavigationStyle> NavigationStyles { get { return DevExpress.Utils.EnumExtensions.GetValues(typeof(GridViewNavigationStyle)).Cast<GridViewNavigationStyle>(); } }
    }
}
