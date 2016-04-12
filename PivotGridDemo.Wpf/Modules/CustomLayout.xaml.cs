using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Xpf.DemoBase;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.PivotGrid;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Reflection;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class CustomLayout : PivotGridDemoModule {

        public CustomLayout() {
            InitializeComponent();
        }

        private void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
        }
    }

    public class PivotImageExtension : MarkupExtension {
        static Dictionary<string, BitmapImage> images = new Dictionary<string, BitmapImage>();
        static BitmapImage LoadImage(string imageName) {
            string resourcePath = string.Format("DevExpress.Xpf.PivotGrid.Images.{0}.png", imageName);
            Assembly asm = typeof(PivotGridControl).Assembly;
            return DevExpress.Xpf.Core.Native.ImageHelper.CreateImageFromEmbeddedResource(asm, resourcePath);
        }
        public static BitmapImage GetImage(string imageName) {
            BitmapImage image = null;
            if(!images.TryGetValue(imageName, out image)) {
                image = LoadImage(imageName);
                images.Add(imageName, image);
            }
            return image;
        }
        public string ImageName { get; set; }

        public PivotImageExtension() { }

        public PivotImageExtension(string imageName) {
            ImageName = imageName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return GetImage(ImageName);
        }
    }
}
