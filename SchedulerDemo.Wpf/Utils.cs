using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using DevExpress.DemoData.Models;
using DevExpress.Utils;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.DemoData.Helpers;
using DevExpress.Mvvm;
using DevExpress.Xpf.Scheduler.Reporting;
using DevExpress.Xpf.Scheduler.Reporting.UI;

namespace SchedulerDemo {
    public class DemoViewTypeListItem : BindableBase {
        string captionCore;
        SchedulerViewType viewTypeCore;

        public string Caption {
            get { return captionCore; }
            set { SetProperty(ref captionCore, value, "Caption"); }
        }
        public SchedulerViewType ViewType {
            get { return viewTypeCore; }
            set { SetProperty(ref viewTypeCore, value, "ViewType"); }
        }
    }
    public class DemoGroupingListItem : BindableBase {
        string captionCore;
        SchedulerGroupType groupTypeCore;

        public string Caption {
            get { return captionCore; }
            set { SetProperty(ref captionCore, value, "Caption"); }
        }
        public SchedulerGroupType GroupType {
            get { return groupTypeCore; }
            set { SetProperty(ref groupTypeCore, value, "GroupType"); }
        }
    }
    #region SchedulerReportTemplateInfo
    public class SchedulerReportTemplateInfo : ISchedulerReportTemplateInfo {
        string displayNameCore;
        string templatePath;
        public SchedulerReportTemplateInfo(string displayName, string templatePath) {
            this.displayNameCore = displayName;
            this.templatePath = templatePath;
        }

        public string DisplayName { get { return displayNameCore; } }
        public string ReportTemplatePath { get { return templatePath; } }
        string ISchedulerReportTemplateInfo.DisplayName { get { return displayNameCore; } }
        string ISchedulerReportTemplateInfo.ReportTemplatePath { get { return templatePath; } }
    }
    #endregion
    public class SchedulerDataHelper {
        static CarsContext staticContext = new CarsContext();
        public const string DefaultAppointmentXmlSource = "Appointments.xml";
        public const string DefaultResourceXmlSource = "Resources.xml";
        public const string EventsXmlSource = "Events.xml";
        public const string CarsXmlSource = "Cars.xml";

        public static Stream GetEventsXmlDataStream() {
            return GetResourceStream(EventsXmlSource);
        }
        public static Stream GetCarsXmlDataStream() {
            return GetResourceStream(CarsXmlSource);
        }
        public static void DataBindToDatabase(SchedulerControl scheduler) {
            CarsContext context = new CarsContext();
            InitCustomAppointmentStatuses(scheduler.Storage);
            scheduler.Storage.ResourceStorage.Mappings.Caption = "Model";
            scheduler.Storage.ResourceStorage.Mappings.Id = "ID";
            scheduler.Storage.ResourceStorage.Mappings.Image = "Picture";
            scheduler.Storage.ResourceStorage.DataSource = context.Cars.ToList();
            scheduler.Storage.AppointmentStorage.Mappings.AllDay = "AllDay";
            scheduler.Storage.AppointmentStorage.Mappings.Description = "Description";
            scheduler.Storage.AppointmentStorage.Mappings.End = "EndTime";
            scheduler.Storage.AppointmentStorage.Mappings.Label = "Label";
            scheduler.Storage.AppointmentStorage.Mappings.Location = "Location";
            scheduler.Storage.AppointmentStorage.Mappings.RecurrenceInfo = "RecurrenceInfo";
            scheduler.Storage.AppointmentStorage.Mappings.ReminderInfo = "ReminderInfo";
            scheduler.Storage.AppointmentStorage.Mappings.ResourceId = "CarId";
            scheduler.Storage.AppointmentStorage.Mappings.Start = "StartTime";
            scheduler.Storage.AppointmentStorage.Mappings.Status = "Status";
            scheduler.Storage.AppointmentStorage.Mappings.Subject = "Subject";
            scheduler.Storage.AppointmentStorage.Mappings.Type = "EventType";
            scheduler.Storage.AppointmentStorage.DataSource = context.CarSchedule.ToList();
            scheduler.InitNewAppointment += new AppointmentEventHandler(OnSchedulerInitNewAppointment);
        }
        static void OnSchedulerInitNewAppointment(object sender, AppointmentEventArgs e) {
            e.Appointment.StatusKey = 0;
        }
        public static void DataBind(SchedulerControl scheduler) {
            DataBind(scheduler.Storage);
            scheduler.InitNewAppointment += new AppointmentEventHandler(OnSchedulerInitNewAppointment);
        }
        public static void DataBind(SchedulerStorage storage) {
            InitCustomAppointmentStatuses(storage);
            FillStorageData(storage);
        }
        static void InitCustomAppointmentStatuses(SchedulerStorage storage) {
            storage.BeginUpdate();
            try {
                IAppointmentStatusStorage statuses = storage.AppointmentStorage.Statuses;
                statuses.Clear();
                statuses.Add(CreateNewAppointmentStatus(statuses, AppointmentStatusType.Free, Colors.White, "Free", "Free"));

                statuses.Add(CreateNewAppointmentStatus(statuses, AppointmentStatusType.Custom, Colors.SkyBlue, "Wash", "Wash"));
                statuses.Add(CreateNewAppointmentStatus(statuses, AppointmentStatusType.Custom, Colors.SteelBlue, "Maintenance", "Maintenance"));
                statuses.Add(CreateNewAppointmentStatus(statuses, AppointmentStatusType.Custom, Colors.YellowGreen, "Rent", "Rent"));
                statuses.Add(CreateNewAppointmentStatus(statuses, AppointmentStatusType.Custom, Colors.Coral, "CheckUp", "CheckUp"));
            } finally {
                storage.EndUpdate();
            }
        }
        private static IAppointmentStatus CreateNewAppointmentStatus(IAppointmentStatusStorage storage, AppointmentStatusType type, Color color, string name, string caption) {
            IAppointmentStatus status = storage.CreateNewStatus(null, name, caption);
            status.Type = type;
            status.SetBrush(new SolidColorBrush(color));
            return status;
        }

        private static void CreateStorageMappings(SchedulerStorage storage) {
            storage.ResourceStorage.Mappings.Caption = "Caption";
            storage.ResourceStorage.Mappings.Id = "Id";
            storage.ResourceStorage.Mappings.Image = "Picture";
            storage.AppointmentStorage.Mappings.AllDay = "AllDay";
            storage.AppointmentStorage.Mappings.Description = "Description";
            storage.AppointmentStorage.Mappings.End = "EndTime";
            storage.AppointmentStorage.Mappings.Label = "Label";
            storage.AppointmentStorage.Mappings.Location = "Location";
            storage.AppointmentStorage.Mappings.RecurrenceInfo = "RecurrenceInfo";
            storage.AppointmentStorage.Mappings.ReminderInfo = "ReminderInfo";
            storage.AppointmentStorage.Mappings.ResourceId = "ResourceId";
            storage.AppointmentStorage.Mappings.Start = "StartTime";
            storage.AppointmentStorage.Mappings.Status = "Status";
            storage.AppointmentStorage.Mappings.Subject = "Subject";
            storage.AppointmentStorage.Mappings.Type = "Type";
        }
        public static void FillStorageData(SchedulerStorage storage) {
            CreateStorageMappings(storage);
            storage.AppointmentStorage.DataSource = SchedulerDemo.Internal.SchedulerXmlHelper.LoadFromXml<SchedulerDemo.Internal.EventItem>(SchedulerDataHelper.EventsXmlSource);
            storage.ResourceStorage.DataSource = SchedulerDemo.Internal.SchedulerXmlHelper.LoadFromXml<SchedulerDemo.Internal.CarItem>(SchedulerDataHelper.CarsXmlSource);
        }
        public static XmlDocument LoadXmlDocumentFromResource(string resourceName) {
            XmlDocument doc = new XmlDocument();
            doc.Load(GetResourceStream(resourceName));
            return doc;
        }
        static void FillStorageCollection(AppointmentCollection c, string resourceName) {
            using(Stream stream = GetResourceStream(resourceName)) {
                c.ReadXml(stream);
                stream.Close();
            }
        }
        static void FillStorageCollection(ResourceCollection c, string resourceName) {
            using(Stream stream = GetResourceStream(resourceName)) {
                c.ReadXml(stream);
                stream.Close();
            }
        }
        static Stream GetResourceStream(string resourceName) {
            Stream result = GetResourceStreamCore(GetFullResourceName(resourceName));
            if(result == null)
                result = GetResourceStreamCore(resourceName);
            return result;
        }
        static string GetFullResourceName(string resourceName) {
            return String.Format("{0}.{1}", "SchedulerDemo.Data", resourceName);
        }
        public static BitmapImage GetImageFromResource(string imageName) {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            BitmapImage result = DevExpress.Xpf.Core.Native.ImageHelper.CreateImageFromEmbeddedResource(executingAssembly, imageName);
            if(result == null)
                result = DevExpress.Xpf.Core.Native.ImageHelper.CreateImageFromEmbeddedResource(executingAssembly, GetFullImageResourceName(imageName));
            return result;
        }
        static string GetFullImageResourceName(string imageResourceName) {
            return string.Format("SchedulerDemo.Data.Images.{0}", imageResourceName);
        }
        static Stream GetResourceStreamCore(string resourceName) {
            return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        }
        static System.Drawing.Color GetStatusColor(int rowIndex) {
            int statusColor = staticContext.UsageTypes.ElementAt(rowIndex).Color;
            return System.Drawing.Color.FromArgb(0xFF, (statusColor & 0xFF0000) >> 16, (statusColor & 0xFF00) >> 8, statusColor & 0xFF);
        }
        public static void DataUnbind(SchedulerControl control) {
            control.Storage.ResourceStorage.DataSource = null;
            control.Storage.AppointmentStorage.DataSource = null;
        }
        public static List<ISchedulerReportTemplateInfo> GetReportTemplateCollection() {
            List<ISchedulerReportTemplateInfo> reportTemplateCollection = new List<ISchedulerReportTemplateInfo>();
            string reportTemplatesDirectory = DataFilesHelper.FindFile("SchedulerReportTemplates", DataFilesHelper.DataPath);
            DirectoryInfo directoryInfo = new DirectoryInfo(reportTemplatesDirectory);
            foreach(FileInfo fileInfo in directoryInfo.GetFiles()) {
                reportTemplateCollection.Add(new SchedulerReportTemplateInfo(fileInfo.Name, fileInfo.FullName));
            }
            return reportTemplateCollection;
        }
    }

    public class SchedulerDemoUtils {
        public static void UpdateSchedulerWorkDays(SchedulerControl scheduler, WeekDays weekDays) {
            WorkDaysCollection workDays = scheduler.WorkDays;
            workDays.BeginUpdate();
            try {
                workDays.Clear();
                workDays.Add(new WeekDaysWorkDay(weekDays));
            } finally {
                workDays.EndUpdate();
            }
        }
        public static WeekDays AddWeekDay(WeekDays workDays, WeekDays day) {
            return workDays | day;
        }
        public static WeekDays RemoveWeekDay(WeekDays workDays, WeekDays day) {
            return workDays & ~day;
        }
    }
    public class UsedAppointmentTypeToBoolConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            UsedAppointmentType type = (UsedAppointmentType)value;
            return type.Equals(UsedAppointmentType.All) ? true : false;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return (bool)value ? UsedAppointmentType.All : UsedAppointmentType.None;
        }
    }
    public class AppointmentConflictsModeToBoolConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            AppointmentConflictsMode mode = (AppointmentConflictsMode)value;
            return mode.Equals(AppointmentConflictsMode.Allowed) ? true : false;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return (bool)value ? AppointmentConflictsMode.Allowed : AppointmentConflictsMode.Forbidden;
        }
    }
    public class BitmapToBitmapSourceConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            System.Drawing.Bitmap source = value as System.Drawing.Bitmap;
            if(source == null || targetType != typeof(ImageSource))
                return null;
            return DemoUtils.ConvertBitmapToBitmapImage(source);


        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class DateTimeToShortDateStringConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null)
                return null;
            DateTime dateTimeValue = (DateTime)value;

            string param = parameter != null ? parameter.ToString() : string.Empty;
            if(param == string.Empty)
                param = "MM/dd";

            return dateTimeValue.ToString(param);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }
    public class LengthToCenterConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(!(value is double) || targetType != typeof(double))
                return null;
            return (double)value / 2;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if(!(value is double) || targetType != typeof(double))
                return null;
            return (double)value * 2;
        }
        #endregion
    }
    public class TimeIntervalToStringConverter : IValueConverter {
        public const string DefaultFormat = "MMMM, dd yyyy";

        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            TimeInterval interval = value as TimeInterval;
            if(interval == null || targetType != typeof(string))
                return null;
            string format = parameter as string;
            string actualFormat = format != null ? format : DefaultFormat;
            if(interval.Duration > TimeSpan.FromDays(1)) {
                string start = interval.Start.ToString(actualFormat);
                string end = interval.End.ToString(actualFormat);
                return String.Format("{0} - {1}", start, end);
            }
            return interval.Start.ToString(actualFormat);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if(!(value is string) || targetType != typeof(TimeInterval))
                return null;
            string[] dateTimes = ((string)value).Split('-');
            if(dateTimes == null || dateTimes.Length == 0)
                return null;
            if(dateTimes.Length > 1) {
                DateTime start;
                DateTime end;
                if(DateTime.TryParse(dateTimes[0], out start) && DateTime.TryParse(dateTimes[1], out end))
                    return new TimeInterval(start, end);
            }
            DateTime dt;
            if(DateTime.TryParse(dateTimes[0], out dt))
                return new TimeInterval(dt, dt);
            return null;
        }
        #endregion
    }
    public class WeekDaysToBooleanConverter : IValueConverter {
        public static WeekDays GetDay(string day) {
            WeekDays result;
            if(!Enum.TryParse<WeekDays>(day, out result))
                return (WeekDays)0;
            return result;
        }

        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            WorkDaysCollection workDays = value as WorkDaysCollection;
            string day = parameter as string;
            if(workDays == null || day == null)
                return null;
            WeekDays weekDay = GetDay(day);
            if((int)weekDay == 0)
                return null;
            return (workDays.GetWeekDays() & weekDay) != 0;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class TimelineViewActiveConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            SchedulerViewType viewType = (SchedulerViewType)value;
            if(viewType.Equals(SchedulerViewType.Timeline))
                return Visibility.Visible;

            return Visibility.Collapsed;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }
    public abstract class DemoNamedImageConverter : IValueConverter {
        const string resourcePath = "SchedulerDemo.Data.Images";

        protected static BitmapImage CreateImage(string name) {
            string imagePath = string.Format("{0}.png", name);
            Stream stream = DemoUtils.GetResourceStream(resourcePath, imagePath);
            Debug.Assert(stream != null);
            return ImageHelper.CreateImageFromStream(stream);
        }

        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(!(value is int) || targetType != typeof(ImageSource))
                return null;
            BitmapImage image = null;
            if(TryGetImageById((int)value, out image))
                return image;
            return null;
        }
        protected abstract bool TryGetImageById(int id, out BitmapImage image);

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class AppointmentStatusIdToImageConverter : DemoNamedImageConverter {
        static Dictionary<int, BitmapImage> imagesTable = CreateImagesTable();
        static Dictionary<int, BitmapImage> CreateImagesTable() {
            Dictionary<int, BitmapImage> result = new Dictionary<int, BitmapImage>();
            result.Add(0, CreateImage("Free"));
            result.Add(1, CreateImage("Wash"));
            result.Add(2, CreateImage("Maintance"));
            result.Add(3, CreateImage("Rent"));
            result.Add(4, CreateImage("CheckUp"));
            return result;
        }
        public AppointmentStatusIdToImageConverter() {
        }
        protected override bool TryGetImageById(int id, out BitmapImage image) {
            return imagesTable.TryGetValue(id, out image);
        }
    }
    public class AppointmentLabelIdToImageConverter : DemoNamedImageConverter {
        static Dictionary<int, BitmapImage> imagesTable = CreateImagesTable();
        static Dictionary<int, BitmapImage> CreateImagesTable() {
            Dictionary<int, BitmapImage> result = new Dictionary<int, BitmapImage>();
            result.Add(0, CreateImage("Red"));
            result.Add(1, CreateImage("Blue"));
            result.Add(2, CreateImage("Yellow"));
            result.Add(3, CreateImage("Lilac"));
            result.Add(4, CreateImage("Beige"));
            return result;
        }
        public AppointmentLabelIdToImageConverter() {
        }
        protected override bool TryGetImageById(int id, out BitmapImage image) {
            return imagesTable.TryGetValue(id, out image);
        }
    }
    public class FilteredAppointmentTextColorConverter : IValueConverter {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null || targetType != typeof(Brush))
                return null;
            return (bool)value ? new SolidColorBrush(SystemColors.WindowTextColor) : new SolidColorBrush(Colors.Gray);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }

        #endregion

    }
    public class SchedulerResourceImageConverter : DependencyObject, IValueConverter {
        #region SchedulerControl
        public SchedulerControl SchedulerControl {
            get { return (SchedulerControl)GetValue(SchedulerControlProperty); }
            set { SetValue(SchedulerControlProperty, value); }
        }
        public static readonly DependencyProperty SchedulerControlProperty = CreateSchedulerControlProperty();
        static DependencyProperty CreateSchedulerControlProperty() {
            return DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterProperty<SchedulerResourceImageConverter, SchedulerControl>("SchedulerControl", null);
        }
        #endregion

        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null || targetType != typeof(ImageSource) || SchedulerControl == null)
                return null;
            if(SchedulerControl.Storage == null)
                return null;
            object resourceId = value;
            Resource resource = SchedulerControl.Storage.ResourceStorage.GetResourceById(resourceId);
            if(resource != null && resource.ImageBytes != null) {
                return resource.GetImage();
            }
            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }

        #endregion
    }
    public class ObjectList : List<Object> {
    }
    public class ResourceNavigatorVisibilityTypes : List<ResourceNavigatorVisibility> {
    }
}
