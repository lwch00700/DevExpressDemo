using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace DockingDemo.ViewModels {
    public class BarItemTemplateSelector : DataTemplateSelector {
        public DataTemplate BarCheckItemTemplate { get; set; }
        public DataTemplate BarItemTemplate { get; set; }
        public DataTemplate BarSubItemTemplate { get; set; }
        public DataTemplate BarItemSeparatorTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            CommandViewModel commandViewModel = item as CommandViewModel;
            if(commandViewModel != null) {
                DataTemplate template = null;
                if(commandViewModel.Owner != null)
                    template = BarCheckItemTemplate;
                if(commandViewModel.IsSubItem)
                    template = BarSubItemTemplate;
                if(commandViewModel.IsSeparator)
                    template = BarItemSeparatorTemplate;
                return template == null ? BarItemTemplate : template;
            }
            return base.SelectTemplate(item, container);
        }
    }
    public class BarTemplateSelector : DataTemplateSelector {
        public DataTemplate MainMenuTemplate { get; set; }
        public DataTemplate ToolbarTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            BarModel barModel = item as BarModel;
            if(barModel != null) {
                return barModel.IsMainMenu ? MainMenuTemplate : ToolbarTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
