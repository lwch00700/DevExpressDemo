using System.Windows;
using System.Linq;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Printing;
using System;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using DevExpress.Xpf.PivotGrid.Internal;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.PivotGrid.Printing;
using System.Windows.Data;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public enum MoonPhase {
        NewMoon,
        WaxingCrescentMoon,
        FirstQuarterMoon,
        WaxingGibbousMoon,
        FullMoon,
        WaningGibbousMoon,
        LastQuarterMoon,
        WaningCrescentMoon
    }

    public partial class PrintTemplates:PivotGridDemoModule {
        DataTemplate defaultCellTemplate, defaultValueTemplate, defaultHeaderTemplate;
        DataTemplateSelector defaultCellTemplateSelector, defaultValueTemplateSelector;

        public PrintTemplates() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().ProductReports.ToList();
            SaveDefaultTemplates();
            ResetItems();
        }

        void SaveDefaultTemplates() {
            defaultCellTemplate = pivotGrid.PrintFieldCellTemplate;
            defaultValueTemplate = pivotGrid.PrintFieldValueTemplate;
            defaultHeaderTemplate = pivotGrid.PrintFieldHeaderTemplate;
            defaultCellTemplateSelector = pivotGrid.PrintFieldCellTemplateSelector;
            defaultValueTemplateSelector = pivotGrid.PrintFieldValueTemplateSelector;
        }

        void Button_Click(object sender, RoutedEventArgs e) {
            ShowPrintPreview(pivotGrid);
        }

        void pivotGrid_CustomGroupInterval(object sender, PivotCustomGroupIntervalEventArgs e) {
            DateTime date = (DateTime)e.Value;
            e.GroupValue = CalculateMoonPhase(date.Year, date.Month, date.Day);
        }

        static double GetFracPart(double value) {
            return value - Convert.ToDouble(Decimal.Truncate(Convert.ToDecimal(value)));
        }
        static MoonPhase MoonPhaseFromInt(int phase) {
            switch(phase) {
                case 0:
                    return MoonPhase.NewMoon;
                case 1:
                    return MoonPhase.WaxingCrescentMoon;
                case 2:
                    return MoonPhase.FirstQuarterMoon;
                case 3:
                    return MoonPhase.WaxingGibbousMoon;
                case 4:
                    return MoonPhase.FullMoon;
                case 5:
                    return MoonPhase.WaningGibbousMoon;
                case 6:
                    return MoonPhase.LastQuarterMoon;
                case 7:
                    return MoonPhase.WaningCrescentMoon;
                default:
                    throw new ArgumentException("Phase must be between 0 and 7", "phase");
            }
        }

        MoonPhase CalculateMoonPhase(int year, int month, int day) {
            double moonCycle = 29.53;
            double daysInMonth = 30.6;
            double daysInYear = 365.25;
            int phasesCount = 8;

            if(month < 3) {
                year--;
                month += 12;
            }
            month++;
            double totalDaysElapsed = daysInYear * year + daysInMonth * month + day - 694039.09;
            double phase = totalDaysElapsed / moonCycle;
            int result = (int)Math.Round(GetFracPart(phase) * phasesCount);
            return MoonPhaseFromInt(result == phasesCount ? 0 : result);
        }

        void ResetItems() {
            fieldCategory.Area = FieldArea.RowArea;
            fieldMoonPhase.Area = FieldArea.ColumnArea;
            fieldMoonPhase.Visible = false;
            fieldMoonPhase.FilterValues.Clear();
            fieldYear.Area = FieldArea.ColumnArea;
            fieldYear.Visible = true;
            fieldYear.AreaIndex = 0;
            fieldYear.FilterValues.Clear();
            fieldQuarter.Area = FieldArea.ColumnArea;
            fieldQuarter.Visible = true;
            fieldQuarter.AreaIndex = 1;
            fieldQuarter.FilterValues.Clear();
            fieldSales.Area = FieldArea.DataArea;
            fieldSales.Visible = true;
            fieldSales.FilterValues.Clear();
            if(defaultCellTemplate == null) return;
            pivotGrid.PrintFieldCellTemplate = defaultCellTemplate;
            pivotGrid.PrintFieldValueTemplate = defaultValueTemplate;
            pivotGrid.PrintFieldHeaderTemplate = defaultHeaderTemplate;
            pivotGrid.PrintFieldCellTemplateSelector = null;
            pivotGrid.PrintFieldValueTemplateSelector = null;
        }

        void templatesList_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if(!IsInitialized) return;
            ResetItems();
            if(templatesList.SelectedIndex == 1) {
                fieldYear.Visible = false;
                fieldQuarter.Visible = false;
                fieldMoonPhase.Visible = true;
            }
            if(templatesList.SelectedIndex == 2) {
                pivotGrid.PrintFieldCellTemplateSelector = defaultCellTemplateSelector;
                pivotGrid.PrintFieldValueTemplateSelector = defaultValueTemplateSelector;
                pivotGrid.PrintFieldCellTemplate = null;
                pivotGrid.PrintFieldValueTemplate = null;
                pivotGrid.PrintFieldHeaderTemplate = (DataTemplate)this.FindResource("headerTemplate");
            }
        }
    }

    public class CellTemplateSelector : DataTemplateSelector {

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            PrintCellsAreaItem cellItem = item as PrintCellsAreaItem;
            if(cellItem == null)
                return base.SelectTemplate(item, container);
            else
                return cellItem.IsTotalAppearance ? CellTotalTemplate : CellTemplate;
        }

        public DataTemplate CellTemplate { get; set; }
        public DataTemplate CellTotalTemplate { get; set; }
    }

    public class FieldValueTemplateSelector : DataTemplateSelector {

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            PrintFieldValueItem cellItem = item as PrintFieldValueItem;
            if(cellItem == null)
                return base.SelectTemplate(item, container);
            else
                return cellItem.IsTotalAppearance ? ValueTotalTemplate : ValueTemplate;
        }

        public DataTemplate ValueTemplate { get; set; }
        public DataTemplate ValueTotalTemplate { get; set; }
    }

    public class MoonPhaseImageConverter : IValueConverter {

        static Dictionary<string, BitmapImage> images = new Dictionary<string, BitmapImage>();
        static BitmapImage LoadImage(string imageName) {
            return new BitmapImage(new Uri(string.Format(@"/PivotGridDemo;component/Images/MoonPhase/{0}.png", imageName), UriKind.RelativeOrAbsolute));
        }

        static void image_ImageFailed(object sender, ExceptionRoutedEventArgs e) {
            throw new NotImplementedException();
        }

        static BitmapImage GetImage(string imageName) {
            BitmapImage image = null;
            if(!images.TryGetValue(imageName, out image)) {
                image = LoadImage(imageName);
                images.Add(imageName, image);
            }
            return image;
        }

        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            FieldValueItem item = value as FieldValueItem;
            if(item == null || !(item.Value is MoonPhase))
                return null;
            else
                return GetImage(item.Value.ToString());
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
