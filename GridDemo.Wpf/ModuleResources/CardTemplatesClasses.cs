using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using DevExpress.Xpf.Grid;

namespace GridDemo {
    public class EMailCardRowTemplateSelector : DataTemplateSelector {
        DataTemplate template;
        public EMailCardRowTemplateSelector(DataTemplate template) {
            this.template = template;
        }
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            GridCellData data = (GridCellData)item;
            return data.Column.FieldName == "EMail" ? template : null;
        }
    }
}
