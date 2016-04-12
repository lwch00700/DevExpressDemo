using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;

namespace TreeMapDemo {
    public static class DataLoader {
        static Stream GetStream(string fileName) {
            Uri uri = GetResourceUri(fileName);
            return Application.GetResourceStream(uri).Stream;
        }
        public static Uri GetResourceUri(string fileName) {
            fileName = "/TreeMapDemo;component" + fileName;
            return new Uri(fileName, UriKind.RelativeOrAbsolute);
        }
        public static XDocument LoadXDocumentFromResources(string fileName) {
            try {
                return XDocument.Load(GetStream(fileName));
            }
            catch {
                return null;
            }
        }
        public static XmlDocument LoadXmlDocumentFromResources(string fileName) {
            XmlDocument document = new XmlDocument();
            try {
                document.Load(GetStream(fileName));
                return document;
            }
            catch {
                return null;
            }
        }
    }

    public class LeafVerticalStackPanel : Panel {
        protected override Size MeasureOverride(Size availableSize) {
            foreach (UIElement child in Children)
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return availableSize;
        }
        protected override Size ArrangeOverride(Size finalSize) {
            Rect freeRect = new Rect(new Point(0, 0), finalSize);
            foreach (UIElement child in Children) {
                FrameworkElement element = child as FrameworkElement;
                if (element != null) {
                    Size size = element.DesiredSize;
                    double horzPosition = element.HorizontalAlignment == HorizontalAlignment.Right ? freeRect.Right - size.Width : freeRect.Left;
                    double vertPosition = element.VerticalAlignment == VerticalAlignment.Bottom ? freeRect.Bottom - size.Height : freeRect.Top;
                    Rect locationRect = new Rect(new Point(horzPosition, vertPosition), size);
                    if (freeRect.Contains(locationRect)) {
                        element.Visibility = Visibility.Visible;
                        element.Arrange(locationRect);
                        if (element.VerticalAlignment == VerticalAlignment.Bottom)
                            freeRect = new Rect(new Point(freeRect.Left, freeRect.Top), new Size(freeRect.Width, freeRect.Height - size.Height));
                        else
                            freeRect = new Rect(new Point(freeRect.Left, freeRect.Top + size.Height), new Point(freeRect.Right, freeRect.Bottom));
                    }
                    else
                        element.Visibility = Visibility.Hidden;
                }
            }
            return finalSize;
        }
    }

    public class StringToImagePathConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string stringValue = value as string;
            if (stringValue != null)
                return DataLoader.GetResourceUri("/Images/" + stringValue + ".png");
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class CountToTextConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (System.Convert.ToInt32(value) == 1)
                return value.ToString() + " medal";
            return value.ToString() + " medals";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class EnergyTypeToBrushConverter : IValueConverter {
        Color GetColor(string typeName) {
            switch (typeName) {
                case "Nuclear": return Color.FromRgb(0x9D, 0x90, 0xA0);
                case "Oil": return Color.FromRgb(0x2A, 0x5F, 0x8E);
                case "Natural Gas": return Color.FromRgb(0x62, 0x9D, 0xD1);
                case "Hydro Electric": return Color.FromRgb(0x29, 0x7F, 0xD5);
                case "Coal": return Color.FromRgb(0x4A, 0x66, 0xAC);
                default: return Colors.Transparent;
            }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string)
                return new SolidColorBrush(GetColor((string)value));
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
