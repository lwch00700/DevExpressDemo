using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Utils;

namespace ControlsDemo {
    public partial class BookCalendar : ControlsDemoModule {
        public BookCalendar() {
            InitializeComponent();
            InitCalendar();
            InitCities();
        }

        public static Coordinate CurrentCoordinates;
        Dictionary<string, Coordinate> Cities { get; set; }
        string SelectedCity { get { return (string)city.SelectedItem; } }
        int CurrentDayIndex { get; set; }

        void InitCalendar() {
            book.DataSource = GetCalendar();
            CurrentDayIndex = DateTime.Now.DayOfYear - 1;
            book.PageIndex = CurrentDayIndex;
        }
        void InitCities() {
            Cities = GetCities();
            FillCities();
        }
        IList GetCalendar() {
            var result = new List<DateTime>();
            var date = DateTime.Now.AddDays(1 - DateTime.Now.DayOfYear);
            while(date.Year <= DateTime.Now.Year) {
                result.Add(date);
                date = date.AddDays(1);
            }
            return result;
        }
        Dictionary<string, Coordinate> GetCities() {
            var result = new Dictionary<string, Coordinate>();
            result.Add("Beijing, China", new Coordinate(39.91, 116.39, 8.0));
            result.Add("Berlin, Germany", new Coordinate(52.50, 13.40, 1.0));
            result.Add("Cairo, Egypt", new Coordinate(30.06, 31.23, 2.0));
            result.Add("Johannesburg, South Africa", new Coordinate(-26.20, 28.05, 2.0));
            result.Add("Las Vegas, United States", new Coordinate(36.18, -115.14, -8.0));
            result.Add("London, United Kingdom", new Coordinate(51.51, 0.13, 0.0));
            result.Add("Los Angeles, United States", new Coordinate(34.05, -118.25, -8.0));
            result.Add("Moscow, Russia", new Coordinate(55.75, 37.62, 4.0));
            result.Add("New Delhi, India", new Coordinate(28.61, 77.21, 5.30));
            result.Add("New York, United States", new Coordinate(40.73, -74.0, -5.0));
            result.Add("Paris, France", new Coordinate(48.86, 2.35, 1.0));
            result.Add("Santiago, Chile", new Coordinate(-33.45, -70.67, -4.0));
            result.Add("Sydney, Australia", new Coordinate(-33.86, 151.21, 10.0));
            result.Add("Tokyo, Japan", new Coordinate(35.70, 137.72, 9.0));
            return result;
        }
        void FillCities() {
            foreach(string name in Cities.Keys)
                city.Items.Add(name);
            city.SelectedIndex = 0;
        }
        void city_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            CurrentCoordinates = Cities[SelectedCity];
            book.DataSource = GetCalendar();
        }
    }

    public class Coordinate {
        public Coordinate(double lat, double lon, double utcOffset) {
            Latitude = lat;
            Longitude = lon;
            UtcOffset = utcOffset;
        }

        public double Longitude { get; private set; }
        public double Latitude { get; private set; }
        public double UtcOffset { get; private set; }
    }

    public class TextConverter : IValueConverter {
        public static Dictionary<string, string> Holidays;

        static TextConverter() {
            Holidays = new Dictionary<string, string>();
            Holidays.Add("January 01", "New Year's Day, Kwanzaa");
            Holidays.Add("January 19", "Martin Luther King, Jr. Day");
            Holidays.Add("January 20", "Inauguration Day");
            Holidays.Add("February 01", "Super Bowl Sunday");
            Holidays.Add("February 02", "Groundhog Day");
            Holidays.Add("February 14", "Valentine's Day");
            Holidays.Add("February 16", "Presidents Day (officially George Washington's Birthday)");
            Holidays.Add("February 25", "Ash Wednesday");
            Holidays.Add("March 17", "St. Patrick's Day");
            Holidays.Add("March 20", "Vernal Equinox");
            Holidays.Add("April 01", "April Fools' Day");
            Holidays.Add("April 05", "Palm Sunday");
            Holidays.Add("April 09", "First day of Passover");
            Holidays.Add("April 10", "Good Friday");
            Holidays.Add("April 16", "Last Day of Passover");
            Holidays.Add("April 20", "Patriot's Day/Marathon Monday");
            Holidays.Add("April 22", "Earth Day");
            Holidays.Add("April 24", "Arbor Day");
            Holidays.Add("May 05", "Cinco De Mayo");
            Holidays.Add("May 10", "Mother's Day");
            Holidays.Add("May 25", "Memorial Day");
            Holidays.Add("May 31", "Pentecost Sunday");
            Holidays.Add("June 14", "Flag Day");
            Holidays.Add("June 21", "Father's Day, Summer Solstice");
            Holidays.Add("July 04", "Independence Day");
            Holidays.Add("July 24", "Pioneer Day");
            Holidays.Add("August 22", "First day of Ramadan");
            Holidays.Add("September 07", "Labor Day");
            Holidays.Add("September 13", "Grandparents Day");
            Holidays.Add("September 19", "Rosh Hashanah");
            Holidays.Add("September 20", "Last day of Ramadan");
            Holidays.Add("September 21", "Eid-al-Fitr/Day after the end of Ramadan");
            Holidays.Add("September 22", "Autumnal equinox");
            Holidays.Add("September 28", "Yom Kippur");
            Holidays.Add("October 03", "First day of Sukkot");
            Holidays.Add("October 09", "Leif Erikson Day, Last Day of Sukkot");
            Holidays.Add("October 10", "Simchat Torah");
            Holidays.Add("October 12", "Columbus Day");
            Holidays.Add("October 30", "Mischief Night");
            Holidays.Add("October 31", "Halloween");
            Holidays.Add("November 01", "All Saints Day");
            Holidays.Add("November 11", "Veterans Day");
            Holidays.Add("November " + GetThanksgiving().ToString(), "Thanksgiving");
            Holidays.Add("November " + (GetThanksgiving() + 1).ToString(), "Black Friday");
            Holidays.Add("December 07", "Pearl Harbor Remembrance Day");
            Holidays.Add("December 12", "First day of Hanukkah");
            Holidays.Add("December 19", "Last day of Hanukkah");
            Holidays.Add("December 21", "Winter Solstice");
            Holidays.Add("December 24", "Christmas Eve");
            Holidays.Add("December 25", "Christmas Day");
            Holidays.Add("December 26", "First day of Kwanzaa");
            Holidays.Add("December 27", "Kwanzaa");
            Holidays.Add("December 28", "Kwanzaa");
            Holidays.Add("December 29", "Kwanzaa");
            Holidays.Add("December 30", "Kwanzaa");
            Holidays.Add("December 31", "New Year's Eve, Kwanzaa");

        }

        public static int GetThanksgiving() {
            DateTime dt = new DateTime(DateTime.Now.Year, 11, 1);
            int count = 0;
            while(count < 4) {
                if(dt.DayOfWeek == DayOfWeek.Thursday)
                    count++;
                dt = dt.AddDays(1);
            }
            return dt.Day - 1;
        }
        public static bool IsHoliday(DateTime d) {
            return Holidays.ContainsKey(GetKey(d));
        }
        static string GetKey(DateTime d) {
            return d.ToString("MMMM dd", CultureInfo.InvariantCulture.DateTimeFormat);
        }

        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return string.Empty;
            DateTime d = (DateTime)value;
            if(IsHoliday(d))
                return Holidays[GetKey(d)];
            return " ";
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class MonthConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return string.Empty;
            return ((DateTime)value).ToString("MMMM", CultureInfo.InvariantCulture.DateTimeFormat);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class DayConverter : IValueConverter {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return null;
            DateTime date = (DateTime)value;
            if(date.Month == 4 && date.Day == 12) {
                Assembly assembly = Assembly.GetExecutingAssembly();
                BitmapImage bi = new BitmapImage();
                using(Stream stream = AssemblyHelper.GetResourceStream(assembly, "gagarin.jpg", false)) {
                    bi.BeginInit();
                    bi.StreamSource = stream;
                    bi.EndInit();
                }
                return new Image() { Source = bi, Stretch = Stretch.Uniform, Height = 170, Margin = new Thickness(20) };
            }
            return date.Day;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class DOWConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return string.Empty;
            return ((DateTime)value).ToString("dddd", CultureInfo.InvariantCulture.DateTimeFormat);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class ForegroundConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return null;
            DayOfWeek dow = ((DateTime)value).DayOfWeek;
            if(dow == DayOfWeek.Sunday || dow == DayOfWeek.Saturday || TextConverter.IsHoliday((DateTime)value))
                return new SolidColorBrush(Color.FromArgb(0xFF, 0xC0, 0x1D, 0x1D));
            return new SolidColorBrush(Color.FromArgb(0xFF, 0x65, 0x67, 0x85));
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class SunRiseConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return string.Empty;
            double utcOffset = BookCalendar.CurrentCoordinates.UtcOffset;
            double lat = BookCalendar.CurrentCoordinates.Latitude;
            double lon = BookCalendar.CurrentCoordinates.Longitude;
            DateTime? time = SunCalculatior.SunRise((DateTime)value, utcOffset, lat, lon);
            if(time.HasValue)
                return string.Format("Sun rise: {0}", time.Value.ToString("t", CultureInfo.InvariantCulture));
            return "There is no sun rise today";
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class SunSetConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return string.Empty;
            double utcOffset = BookCalendar.CurrentCoordinates.UtcOffset;
            double lat = BookCalendar.CurrentCoordinates.Latitude;
            double lot = BookCalendar.CurrentCoordinates.Longitude;
            DateTime? time = SunCalculatior.SunSet((DateTime)value, utcOffset, lat, lot);
            if(time.HasValue)
                return string.Format("Sun set: {0}", time.Value.ToString("t", CultureInfo.InvariantCulture));
            return "There is no sun set today";
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
