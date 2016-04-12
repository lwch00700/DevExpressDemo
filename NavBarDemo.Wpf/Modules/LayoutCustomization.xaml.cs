using System;
using System.Collections;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.NavBar;
using DevExpress.DemoData.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NavBarDemo {
    public partial class LayoutCustomization : NavBarDemoModule {
        CarsContext context = new CarsContext();
        public LayoutCustomization() {
            InitializeComponent();
            navBar.ItemsSource = context.Cars.ToList();
            itemsPanelListBox.SelectedIndex = 0;
        }

        void UpdateControlAvailability() {
            if(!IsInitialized)
                return;
            verticalPanelAlignment.IsEnabled = !(navBar.View is ExplorerBarView);

            if(orientationComboBox.SelectedItem != null) {
                Orientation orientation = (Orientation)orientationComboBox.SelectedItem;
                horizontalPanelAlignment.IsEnabled = !(orientation == Orientation.Horizontal && navBar.View is ExplorerBarView);
                verticalPanelAlignment.IsEnabled = !(orientation == Orientation.Vertical && navBar.View is ExplorerBarView);
            }

            if(itemsPanelListBox.SelectedItem != null) {
                bool isCarouselPanel = (string)itemsPanelListBox.SelectedItem == "Carousel Panel";
                panelOrientation.IsEnabled = !isCarouselPanel;
                alignmentGroupBox.IsEnabled = !isCarouselPanel;
                displayModeGroupBox.IsEnabled = !isCarouselPanel;
            }

            textAlignment.IsEnabled = displayModeComboBox.SelectedIndex != 1;
            imageAlignment.IsEnabled = displayModeComboBox.SelectedIndex == 1 ||
                (displayModeComboBox.SelectedIndex == 0 && (imageDocking.SelectedIndex == 2 || imageDocking.SelectedIndex == 3));
        }

        protected override void SelectView(object sender, RoutedEventArgs e) {
            if(navBarControl == null)
                return;
            base.SelectView(sender, e);
            if (navBar.View is ExplorerBarView) {
                itemsPanelListBox.ItemsSource = (IEnumerable)FindResource("ItemsPanel");
                if (itemsPanelListBox.SelectedIndex == -1)
                    itemsPanelListBox.SelectedIndex = 0;
            } else
                itemsPanelListBox.ItemsSource = (IEnumerable)FindResource("ItemsPanelExplorerBar");
            ChangePanel();
            UpdateControlAvailability();
        }

        private void OrientationChanged(object sender, RoutedEventArgs e) {
            ChangeOrientation();
        }
        void ChangeOrientation() {
            Orientation orientation = (Orientation)orientationComboBox.SelectedItem;
            navBar.HorizontalAlignment = orientation == Orientation.Horizontal ? HorizontalAlignment.Stretch : HorizontalAlignment.Left;
            navBar.VerticalAlignment = orientation == Orientation.Vertical ? VerticalAlignment.Stretch : VerticalAlignment.Top;
            UpdateControlAvailability();
        }
        private void itemsPanelListBox_SelectionChanged(object sender, RoutedEventArgs e) {
            ChangePanel();
        }

        void ChangePanel() {
            ItemsPanelTemplate panelTemplate;
            switch((string)itemsPanelListBox.SelectedItem) {
                case "Stack Panel":
                    panelTemplate = (ItemsPanelTemplate)FindResource("stackPanel");
                    displayModeComboBox.SelectedIndex = 0;
                    break;
                case "Wrap Panel":
                    panelTemplate = (ItemsPanelTemplate)FindResource("wrapPanel");
                    displayModeComboBox.SelectedIndex = 0;
                    break;
                case "Carousel Panel":
                    panelTemplate = (ItemsPanelTemplate)FindResource("carouselPanel");
                    displayModeComboBox.SelectedIndex = 1;
                    break;
                default:
                    throw new ArgumentException("Could not find specified panel.");
            }
            UpdateItemVisualStyle();
            navBar.View.ItemsPanelTemplate = panelTemplate;
            UpdateControlAvailability();
        }

        private void displayMode_SelectionChanged(object sender, RoutedEventArgs e) {
            UpdateControlAvailability();
        }
        private void itemImageDocking_SelectionChanged(object sender, RoutedEventArgs e) {
            UpdateControlAvailability();
        }
        protected virtual void UpdateItemVisualStyle() {
            if((string)itemsPanelListBox.SelectedItem == "Carousel Panel") {
                navBar.View.ItemVisualStyle = (Style)FindResource("carouselItemVisualStyle");
                return;
            }
            HorizontalAlignment alignment = (HorizontalAlignment)imageAlignment.SelectedItem;
            if(alignment == System.Windows.HorizontalAlignment.Stretch) {
                navBar.View.ItemVisualStyle = (Style)FindResource("itemVisualStyle");
            }
            else {
                navBar.View.ItemVisualStyle = (Style)FindResource("nonStretchImageItemVisualStyle");
            }
        }
        private void imageAlignment_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            UpdateItemVisualStyle();
        }
    }
    public class DoubleToIntConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (int)((double)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class StringToDisplayModeConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            switch((string)value) {
                case "Image and Text":
                    return DisplayMode.ImageAndText;
                case "Image":
                    return DisplayMode.Image;
                default:
                    return DisplayMode.Text;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class SelectedItemsToLayoutSettingsConverter : MarkupExtension, IMultiValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            foreach(object obj in values)
                if(obj == DependencyProperty.UnsetValue)
                    return new LayoutSettings();
            return new LayoutSettings() {
                TextHorizontalAlignment = (HorizontalAlignment)values[0],
                ImageHorizontalAlignment = (HorizontalAlignment)values[1],
                ImageDocking = (Dock)values[2]
            };
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class VisibleItemCountToActiveElementIndexConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (targetType != typeof(int))
                throw new InvalidOperationException();
            return ((((int)value) - 1) / 2);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
