using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.Controls;

namespace ControlsDemo {
    public enum ImageSourceKind { Data, Uri };

    public partial class BookEmployees : ControlsDemoModule {
        public BookEmployees() {
            InitializeComponent();
            book.DataSource = NWindContext.Create().Employees.ToList();
            book.FirstPage = PageType.Even;
        }
    }
    public class ImageContentConverter : IValueConverter {
        public ImageSourceKind SourceKind { get; set; }

        public ImageContentConverter() { }
        public ImageContentConverter(ImageSourceKind sourceKind) {
            SourceKind = sourceKind;
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(SourceKind == ImageSourceKind.Data && value is byte[]) {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream((byte[])value);
                bi.EndInit();
                return bi;
            }
            else {
                if(SourceKind == ImageSourceKind.Uri && value is string)
                    return new BitmapImage(new Uri(value as string));
                else
                    return null;
            }
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
