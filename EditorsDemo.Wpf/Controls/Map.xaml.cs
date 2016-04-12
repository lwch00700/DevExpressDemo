using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors;
using System.Globalization;

namespace EditorsDemo {
    public partial class Map : Button {
        const double totalDistance = 684d;
        const double totalTime = 6d;
        public static readonly DependencyProperty MapDataProperty;
        static Map() {
            Type ownerType = typeof(Map);
            MapDataProperty = DependencyProperty.Register("MapData", typeof(MapData), ownerType, new PropertyMetadata(null));
        }
        public static string[] towns = new string[] {
            "Afrene",
            "Hibesona",
            "Erarium",
            "Myralana",
            "Myrynana",
            "Minacius",
            "Lucacova",
            "Danyrova",
            "Tritrium",
        };
        public Map() {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Map_Loaded);
        }

        void Map_Loaded(object sender, RoutedEventArgs e) {
            UpdateData((TrackBarEdit)GetTemplateChild("tb"));
            TrackBarEdit edit = (TrackBarEdit)GetTemplateChild("tb");
        }
        public MapData MapData {
            get { return (MapData)GetValue(MapDataProperty); }
            set { SetValue(MapDataProperty, value); }
        }
        void TrackBarEdit_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            UpdateData((TrackBarEdit)sender);
        }
        int GetStartIndex(TrackBarEdit edit) {
            return (int)((TrackBarEditRange)(edit.EditValue)).SelectionStart;
        }
        int GetEndIndex(TrackBarEdit edit) {
            return (int)((TrackBarEditRange)(edit.EditValue)).SelectionEnd;
        }
        void UpdateData(TrackBarEdit edit) {
            MapData data = new MapData();
            data.StartTown = towns[GetStartIndex(edit)];
            data.EndTown = towns[GetEndIndex(edit)];
            data.DistanceFromAToStart = (((TrackBarEditRange)edit.EditValue).SelectionStart - edit.Minimum) / (edit.Maximum - edit.Minimum) * totalDistance;
            data.DistanceFromAToEnd = (edit.Maximum - ((TrackBarEditRange)edit.EditValue).SelectionStart) / (edit.Maximum - edit.Minimum) * totalDistance;
            data.DistanceFromBToStart = (((TrackBarEditRange)edit.EditValue).SelectionEnd - edit.Minimum) / (edit.Maximum - edit.Minimum) * totalDistance;
            data.DistanceFromBToEnd = (edit.Maximum - ((TrackBarEditRange)edit.EditValue).SelectionEnd) / (edit.Maximum - edit.Minimum) * totalDistance;
            data.TimeFromAToStart = (((TrackBarEditRange)edit.EditValue).SelectionStart - edit.Minimum) / (edit.Maximum - edit.Minimum) * totalTime;
            data.TimeFromAToEnd = (edit.Maximum - ((TrackBarEditRange)edit.EditValue).SelectionStart) / (edit.Maximum - edit.Minimum) * totalTime;
            data.TimeFromBToStart = (((TrackBarEditRange)edit.EditValue).SelectionEnd - edit.Minimum) / (edit.Maximum - edit.Minimum) * totalTime;
            data.TimeFromBToEnd = (edit.Maximum - ((TrackBarEditRange)edit.EditValue).SelectionEnd) / (edit.Maximum - edit.Minimum) * totalTime;
            data.DistanceBetween = (((TrackBarEditRange)edit.EditValue).SelectionEnd - ((TrackBarEditRange)edit.EditValue).SelectionStart) / (edit.Maximum - edit.Minimum) * totalDistance;
            data.TimeBetween = (((TrackBarEditRange)edit.EditValue).SelectionEnd - ((TrackBarEditRange)edit.EditValue).SelectionStart) / (edit.Maximum - edit.Minimum) * totalTime;
            MapData = data;
        }
    }
    public class MapData {
        public string StartTown { get; set; }
        public string EndTown { get; set; }
        public double DistanceBetween { get; set; }
        public double DistanceFromAToEnd { get; set; }
        public double DistanceFromBToEnd { get; set; }
        public double DistanceFromAToStart { get; set; }
        public double DistanceFromBToStart { get; set; }
        public double TimeBetween { get; set; }
        public double TimeFromAToEnd { get; set; }
        public double TimeFromBToEnd { get; set; }
        public double TimeFromAToStart { get; set; }
        public double TimeFromBToStart { get; set; }
    }
    public class MapDataToTextConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            MapData data = (MapData)value;
            if(data == null)
                return null;
            string text = null;
            if((string)parameter == "Start")
                text = data.StartTown.ToUpper() + "\n"
                    + Map.towns[0] + " - " + data.StartTown + ": " + data.TimeFromAToStart + " h, " + data.DistanceFromAToStart + " km" + "\n"
                    + data.StartTown + " - " + Map.towns[Map.towns.Length - 1] + ": " + data.TimeFromAToEnd + " h, " + data.DistanceFromAToEnd + " km";
            if((string)parameter == "End")
                text = data.EndTown.ToUpper() + "\n"
                    + Map.towns[0] + " - " + data.EndTown + ": " + data.TimeFromBToStart + " h, " + data.DistanceFromBToStart + " km" + "\n"
                    + data.EndTown + " - " + Map.towns[Map.towns.Length - 1] + ": " + data.TimeFromBToEnd + " h, " + data.DistanceFromBToEnd + " km";
            if((string)parameter == "Total")
                text = data.StartTown.ToUpper() + " - " + data.EndTown.ToUpper() + "\n"
                    + data.TimeBetween.ToString() + " h, " + data.DistanceBetween.ToString() + " km";
            return text;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
