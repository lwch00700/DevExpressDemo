using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Carousel;
using DevExpress.Xpf.DemoBase;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;

namespace CarouselDemo {
    public enum ItemType { BinaryImage, DrawingImage }
    public class ContentLoadHelper {
        public string Path { get; set; }
        public ContentLoadHelper() {
        }
        public List<FrameworkElement> LoadItems(ItemType it) {
            LoadDelegate loadDelegate = null;
            switch (it) {
                case ItemType.BinaryImage:
                    loadDelegate = LoadImage;
                    break;
                case ItemType.DrawingImage:
                    loadDelegate = LoadDrawingImage;
                    break;
            }
            var items = new List<FrameworkElement>();
            if (loadDelegate != null) {
                foreach (var name in ResourcesTable.UriTable)
                    if (name.StartsWith(Path, StringComparison.OrdinalIgnoreCase)) {
                        try {
                            Uri resUri = ResourcesTable.GetUri(name);
                            Stream stream = Application.GetResourceStream(resUri).Stream;
                            items.Add(loadDelegate(stream));
                        }
                        catch { }
                    }
            }
            return items;
        }
        public delegate FrameworkElement LoadDelegate(Stream stream);
        public Image LoadDrawingImage(Stream stream) {
            var rd = (ResourceDictionary)XamlReader.Load(stream);
            var di = (DrawingImage)rd["Layer_1"];
            return new Image() { Source = di };
        }
        public Image LoadImage(Stream stream) {
            var image = new Image();
            image.Source = BitmapFrame.Create(stream);
            return image;
        }
    }
    public class ImageContainer {
        public string Name { get; set; }
        public ImageSource Source { get; set; }
    }
    public class PathContainer {
        public string Name { get; set; }
        public PathGeometry Path { get; set; }
    }
    public class FunctionContainer {
        public string Name { get; set; }
        public FunctionBase FunctionBase { get; set; }
    }
    public class BitmapEffectContainer {
        public string Name { get; set; }
        public BitmapEffect BitmapEffect { get; set; }
    }
    public class LanguageContainer {
        public string Name { get; set; }
        public string FlagImageSource { get; set; }
        public string Phrase { get; set; }
    }
    public class PhraseConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var cc = (ContentControl)value;
            if (cc != null) {
                var lc = (LanguageContainer)cc.Content;
                return lc.Phrase;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }

        #endregion
    }
    public class LanguageContainerConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }

        #region IValueConverter Members
        public static Image LoadFromResources(string path, Assembly asm) {
            try {
                Uri resUri = ResourcesTable.GetUri(path);
                Stream stream = Application.GetResourceStream(resUri).Stream;
                var rd = (ResourceDictionary)XamlReader.Load(stream);
                return new Image() { Source = (DrawingImage)rd["Layer_1"], Stretch = Stretch.Fill, UseLayoutRounding = true };
            }
            catch { }
            return new Image();
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var fileName = (string)value;
            return LoadFromResources(@"/CarouselDemo;component/Data/Images/Flags/" + fileName, Assembly.GetExecutingAssembly());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }

        #endregion
    }
    public class FunctionContainerConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            IList list = (IList)parameter;
            foreach (object item in list) {
                FunctionContainer container = item as FunctionContainer;
                if (container != null && container.FunctionBase == value)
                    return container;
            }
            return list[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return ((FunctionContainer)value).FunctionBase;
        }

        #endregion
    }
    public class BitmapEffectContainerConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return ((IList)parameter)[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return ((FunctionContainer)value).FunctionBase;
        }

        #endregion
    }
    public class DoubleToVisibleItemCountConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            double doubleValue = (double)value;
            int returnValue = (int)(doubleValue);
            returnValue *= 2;
            return returnValue + 1;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (targetType != typeof(double))
                throw new InvalidOperationException();
            return (double)((((int)value) - 1) / 2);
        }
        #endregion
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
    public class DoubleToIntConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (targetType != typeof(int))
                throw new InvalidOperationException();
            return (int)((double)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class PathSizingModeConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return object.Equals(value, PathSizingMode.Stretch);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if ((bool)value)
                return PathSizingMode.Stretch;
            return PathSizingMode.Proportional;
        }
        #endregion
    }
    public class SizeTransformConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(System.IServiceProvider serviceProvider) {
            return this;
        }
        object IValueConverter.Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            //if ((string)parameter == "Width")
            return 200d * (double)value;
            //else
            //    return 150d * (double)value;
        }
        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new System.NotImplementedException();
        }
    }
}
