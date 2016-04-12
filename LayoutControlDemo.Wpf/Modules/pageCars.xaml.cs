using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.LayoutControl;

namespace DevExpress.Xpf.LayoutControlDemo {
    public abstract class BrandOrCar {
        public BitmapImage Image { get { return new BitmapImage(ImageFullName); } }
        public Uri ImageFullName { get { return new Uri(LayoutControlDemoModule.UriPrefix + "/Images/Cars/" + ImageName, UriKind.Relative); } }
        public string ImageName { get; set; }
    }

    public class Brand : BrandOrCar {
        public List<Car> Cars { get; set; }
        public Color Color { get; set; }
    }

    public class Car : BrandOrCar {
        public string Engine { get; set; }
        public string Name { get; set; }
        public string Power { get; set; }
        public string Torque { get; set; }
    }

    public partial class pageCars : LayoutControlDemoModule {
        static Car carA3 = new Car { Name = "A3", ImageName = "a3.jpg", Engine = "2.0L I4 turbo", Power = "200 hp @ 5,100-6,000 rpm", Torque = "207 lb-ft @ 1,800-5,000 rpm" };
        static Car carA4 = new Car { Name = "A4", ImageName = "a4.jpg", Engine = "3.2L V6", Power = "255 hp @ 6,500 rpm", Torque = "243 lb-ft @ 3,250 rpm" };
        static Car carA5 = new Car { Name = "A5", ImageName = "a5.jpg", Engine = "3.2L V6", Power = "265 hp", Torque = "243 lb-ft" };
        static Car carA6 = new Car { Name = "A6", ImageName = "a6.jpg", Engine = "4.2L V8", Power = "350 hp @ 6,800 rpm", Torque = "325 lb-ft @ 3,500 rpm" };
        static Car carA8 = new Car { Name = "A8", ImageName = "a8.jpg", Engine = "4.2L V8", Power = "350 hp @ 6,800 rpm", Torque = "325 lb-ft @ 3,500 rpm" };
        static Car carTT = new Car { Name = "TT", ImageName = "tt.jpg", Engine = "3.2L V6", Power = "250 hp @ 6,300 rpm", Torque = "236 lb-ft @ 2,500-3,000 rpm" };
        static Car carS4 = new Car { Name = "S4", ImageName = "s4.jpg", Engine = "4.2L V8", Power = "340 hp @ 7,000 rpm", Torque = "302 lb-ft @ 3,500 rpm" };
        static Car carS5 = new Car { Name = "S5", ImageName = "s5.jpg", Engine = "4.2L V8", Power = "354 hp @ 6,800 rpm", Torque = "325 lb-ft @ 3,500 rpm" };
        static Car carS6 = new Car { Name = "S6", ImageName = "s6.jpg", Engine = "5.2L V10", Power = "435 hp @ 6,800 rpm", Torque = "398 lb-ft @ 3,000-4,000 rpm" };
        static Car carS8 = new Car { Name = "S8", ImageName = "s8.jpg", Engine = "5.2L V10", Power = "450 hp @ 7,000 rpm", Torque = "398 lb-ft @ 3,500 rpm" };
        static Car carRS4 = new Car { Name = "RS4", ImageName = "rs4.jpg", Engine = "4.2L V8", Power = "420 hp @ 7,800 rpm", Torque = "317 lb-ft @ 5,500 rpm" };
        static Car carR8 = new Car { Name = "R8", ImageName = "r8.jpg", Engine = "4.2L V8", Power = "420 hp @ 7,800 rpm", Torque = "317 lb-ft @ 4,500-6,000 rpm" };

        static Car car3S = new Car { Name = "3 Series", ImageName = "3.jpg", Engine = "3.0L I6", Power = "230 hp @ 6,500 rpm", Torque = "200 lb-ft @ 2,750 rpm" };
        static Car car5S = new Car { Name = "5 Series", ImageName = "5.jpg", Engine = "4.8L V8", Power = "360 hp @ 6,300 rpm", Torque = "360 lb-ft @ 3,400 rpm" };
        static Car car6S = new Car { Name = "6 Series", ImageName = "6.jpg", Engine = "4.8L V8", Power = "360 hp @ 6,300 rpm", Torque = "360 lb-ft @ 3,400 rpm" };
        static Car car7S = new Car { Name = "7 Series", ImageName = "7.jpg", Engine = "6.0L V12", Power = "438 hp @ 6,000 rpm", Torque = "444 lb-ft @ 3,950 rpm" };
        static Car carZ4 = new Car { Name = "Z4", ImageName = "z4.jpg", Engine = "3.0L I6", Power = "215 hp @ 6,250 rpm", Torque = "185 lb-ft @ 2,750 rpm" };
        static Car carM3 = new Car { Name = "M3", ImageName = "m3.jpg", Engine = "4.0L V8", Power = "414 hp", Torque = "295 lb-ft" };
        static Car carM5 = new Car { Name = "M5", ImageName = "m5.jpg", Engine = "5.0L V10", Power = "500 hp @ 7,750 rpm", Torque = "383 lb-ft @ 6,100 rpm" };
        static Car carM6 = new Car { Name = "M6", ImageName = "m6.jpg", Engine = "5.0L V10", Power = "500 hp @ 7,750 rpm", Torque = "383 lb-ft @ 6,100 rpm" };
        static Car carZ4M = new Car { Name = "Z4 M", ImageName = "z4m.jpg", Engine = "3.2L I6", Power = "330 hp @ 7,900 rpm", Torque = "262 lb-ft @ 4,900 rpm" };

        static Car carCC = new Car { Name = "C-Class", ImageName = "c.jpg", Engine = "3.0L V6", Power = "228 hp @ 6,000 rpm", Torque = "221 lb-ft @ 2,700 – 5,000 rpm" };
        static Car carEC = new Car { Name = "E-Class", ImageName = "e.jpg", Engine = "3.5L V6", Power = "268 hp @ 6,000 rpm", Torque = "258 lb-ft @ 2,400 - 5,000 rpm" };
        static Car carSC = new Car { Name = "S-Class", ImageName = "s.jpg", Engine = "5.5L V12 biturbo", Power = "510 hp @ 5,000 rpm", Torque = "612 lb-ft @ 1,800 - 3,500 rpm" };
        static Car carCLK = new Car { Name = "CLK-Class", ImageName = "clk.jpg", Engine = "5.5L V8", Power = "382 hp @ 6,000 rpm", Torque = "391 lb-ft @ 2,800 - 4,800 rpm" };
        static Car carCLS = new Car { Name = "CLS-Class", ImageName = "cls.jpg", Engine = "5.5L V8", Power = "382 hp @ 6,000 rpm", Torque = "391 lb-ft @ 2,800 - 4,800 rpm" };
        static Car carCLC = new Car { Name = "CL-Class", ImageName = "cl.jpg", Engine = "5.5L V12 biturbo", Power = "510 hp @ 5,000 rpm", Torque = "612 lb-ft @ 1,800 - 3,500 rpm" };
        static Car carSLKC = new Car { Name = "SLK-Class", ImageName = "slk.jpg", Engine = "3.5L V6", Power = "268 hp @ 6,000 rpm", Torque = "258 lb-ft @ 2,400 - 5,000 rpm" };
        static Car carSLC = new Car { Name = "SL-Class", ImageName = "sl.jpg", Engine = "5.5L V12", Power = "510 hp @ 5,000 rpm", Torque = "612 lb-ft @ 1,800 - 3,500 rpm" };
        static Car carSLR = new Car { Name = "SLR", ImageName = "slr.jpg", Engine = "5.5L V8 supercharged", Power = "617 hp @ 6,500 rpm", Torque = "575 lb-ft @ 3,250 – 5,000 rpm" };

        public static readonly List<Brand> Brands = new List<Brand> {
            new Brand {
                ImageName = "audi.png",
                Color = Colors.Red,
                Cars = new List<Car> { carA3, carA4, carA5, carA6, carA8, carTT, carS4, carS5, carS6, carS8, carRS4, carR8,}
            },
            new Brand {
                ImageName = "bmw.png",
                Color = Colors.Blue,
                Cars = new List<Car> { car3S, car5S, car6S, car7S, carZ4, carM3, carM5, carM6, carZ4M }
            },
            new Brand {
                ImageName = "mercedes.png",
                Color = Colors.Gray,
                Cars = new List<Car> { carCC, carEC, carSC, carCLK, carCLS, carCLC, carSLKC, carSLC, carSLR  }
            }
        };

        public pageCars() {
            InitializeComponent();
            LayoutUpdated += OnLayoutUpdated;
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        public IEnumerable Cars {
            get {
                var result = new List<BrandOrCar>();
                foreach (Brand brand in Brands) {
                    result.Add(brand);
                    result.AddRange(brand.Cars);
                }
                return result;
            }
        }

        void CarElementMouseEnter(object sender, MouseEventArgs e) {
            CarElement = (controlCarInfo)sender;
            CarElementPosition = CarElement.GetPosition(layoutCars);

            var carDetailsElement = new controlCarInfo(true, true, true);
            carDetailsElement.DataContext = CarElement.DataContext;
            carDetailsElement.FlowDirection = CarElement.FlowDirection;
            carDetailsElement.FontFamily = CarElement.FontFamily;
            carDetailsElement.Owner = CarElement;

            CarDetailsPopup = new TransparentPopup();
            CarDetailsPopup.Child = carDetailsElement;
            CarDetailsPopup.PlacementTarget = layoutCars;
            CarDetailsPopup.IsOpen = true;
            carDetailsElement.UpdateLayout();
            if (CarDetailsPopup == null)
                return;
            Point carDetailsPopupPosition =
                PointHelper.Subtract(PointHelper.Add(CarElementPosition, CarElement.ContentOffset), carDetailsElement.ContentOffset);
            CarDetailsPopup.HorizontalOffset = carDetailsPopupPosition.X;
            CarDetailsPopup.VerticalOffset = carDetailsPopupPosition.Y;
            CarDetailsPopup.MakeVisible();

            Storyboard.SetTarget(CarDetailsShowingAnimation, carDetailsElement);
            CarDetailsShowingAnimation.Begin();
        }
        void CarElementMouseLeave(object sender, MouseEventArgs e) {
            if (CarDetailsPopup == null)
                return;
            CarDetailsShowingAnimation.Stop();

            CarDetailsPopup.IsOpen = false;
            CarDetailsPopup.Child = null;
            CarDetailsPopup = null;

            CarElement = null;
        }
        void OnLayoutUpdated(object sender, EventArgs e) {
            if (CarDetailsPopup == null)
                return;
            Point newPosition = CarElement.GetPosition(layoutCars);
            Point offset = PointHelper.Subtract(newPosition, CarElementPosition);
            if (offset.X == 0 && offset.Y == 0)
                return;
            CarElementPosition = newPosition;
            CarDetailsPopup.HorizontalOffset += offset.X;
            CarDetailsPopup.VerticalOffset += offset.Y;
        }
        void OnApplicationDeactivated(object sender, EventArgs e) {
            CarElementMouseLeave(null, null);
        }
        void OnLoaded(object sender, RoutedEventArgs e) {
            Application.Current.Deactivated += OnApplicationDeactivated;
        }
        void OnUnloaded(object sender, RoutedEventArgs e) {
            Application.Current.Deactivated -= OnApplicationDeactivated;
        }

        TransparentPopup CarDetailsPopup { get; set; }
        controlCarInfo CarElement { get; set; }
        Point CarElementPosition { get; set; }
        Storyboard CarDetailsShowingAnimation { get { return (Storyboard)Resources["animationCarDetailsShowing"]; } }
    }

    public class IsFirstCarConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            foreach (Brand brand in pageCars.Brands)
                if (brand.Cars[0] == value)
                    return true;
            return false;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class CarDataTemplateSelector : DataTemplateSelector {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            var control = (FlowLayoutControl)container;
            return (DataTemplate)control.Resources[item.GetType().Name + "DataTemplate"];
        }
    }
}
