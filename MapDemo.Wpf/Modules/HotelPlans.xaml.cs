﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using System.Xml.Linq;
using DevExpress.Map;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;

namespace MapDemo {
    [POCOViewModel]
    public class HotelsViewModel : BindableBase {
        public virtual CoordinateSystemType CoordinateSystemType { get; set; }
        public virtual ObservableCollection<HotelInfo> HotelInfos { get; set; }
        public virtual HotelInfo SelectedHotel { get; set; }
        public virtual int ZoomLevel { get; set; }
        public virtual int MinZoomLevel { get; set; }
        public virtual int MaxZoomLevel { get; set; }
        public virtual CoordPoint CenterPoint { get; set; }

        GeoPoint mapDefaultPosition = new GeoPoint(-21.16, -175.13);
        int geoMinZoomLevel = 12;
        int geoMaxZoomLevel = 15;
        int hotelMinZoomLevel = 2;
        int hotelMaxZoomLevel = 6;
        int mapDefaultZoomLevel = 13;

        public HotelsViewModel() {
            CoordinateSystemType = CoordinateSystemType.Geo;
            MinZoomLevel = geoMinZoomLevel;
            MaxZoomLevel = geoMaxZoomLevel;
            ZoomLevel = mapDefaultZoomLevel;
            CenterPoint = mapDefaultPosition;
            HotelInfos = LoadDataFromXML();
        }

        protected void OnSelectedHotelChanged() {
            if (SelectedHotel == null) {
                MinZoomLevel = geoMinZoomLevel;
                MaxZoomLevel = geoMaxZoomLevel;
                CoordinateSystemType = CoordinateSystemType.Geo;
                CenterPoint = mapDefaultPosition;
                ZoomLevel = mapDefaultZoomLevel;
            }
            else {
                MinZoomLevel = hotelMinZoomLevel;
                MaxZoomLevel = hotelMaxZoomLevel;
                CoordinateSystemType = CoordinateSystemType.Cartesian;
                CenterPoint = SelectedHotel.Center;
                ZoomLevel = SelectedHotel.ZoomLevel;
            }
        }
        ObservableCollection<HotelInfo> LoadDataFromXML() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/Hotels.xml");
            ObservableCollection<HotelInfo> infos = new ObservableCollection<HotelInfo>();
            if (document != null) {
                foreach (XElement element in document.Element("Hotels").Elements()) {
                    HotelInfo hotelInfo = new HotelInfo();
                    hotelInfo.Name = element.Element("Name").Value;
                    hotelInfo.Latitude = Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture);
                    hotelInfo.Longitude = Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture);
                    hotelInfo.ShpFileUri = new Uri(element.Element("ShpFileUri").Value, UriKind.RelativeOrAbsolute);
                    hotelInfo.Center = new CartesianPoint(Convert.ToDouble(element.Element("CenterX").Value, CultureInfo.InvariantCulture), Convert.ToDouble(element.Element("CenterY").Value, CultureInfo.InvariantCulture));
                    hotelInfo.ZoomLevel = Convert.ToInt32(element.Element("ZoomLevel").Value);
                    hotelInfo.ImageUri = new Uri(element.Element("ImageUri").Value, UriKind.RelativeOrAbsolute);
                    hotelInfo.HighlightedImageUri = new Uri(element.Element("HighlightedImageUri").Value, UriKind.RelativeOrAbsolute);
                    infos.Add(hotelInfo);
                }
            }
            return infos;
        }
    }

    public partial class HotelPlans : MapDemoModule {
        public HotelsViewModel ViewModel { get { return LayoutRoot.DataContext as HotelsViewModel; } }

        public HotelPlans() {
            InitializeComponent();
            LayoutRoot.DataContext = ViewModelSource.Create(() => new HotelsViewModel());
        }
        void Label_MouseDown(object sender, MouseButtonEventArgs e) {
            ViewModel.SelectedHotel = null;
        }
    }

    public class HotelInfo {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Uri ShpFileUri { get; set; }
        public string Name { get; set; }
        public CartesianPoint Center { get; set; }
        public int ZoomLevel { get; set; }
        public Uri ImageUri { get; set; }
        public Uri HighlightedImageUri { get; set; }
    }

    public enum CoordinateSystemType { Geo, Cartesian }
}
