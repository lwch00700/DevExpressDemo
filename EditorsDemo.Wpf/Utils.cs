using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Data;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Scheduler;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Media;
using DevExpress.Xpf.Editors;
using System.Collections.Generic;
using GridDemo;
using DevExpress.Mvvm.UI.Interactivity;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Diagnostics;
using System.Reflection;

namespace EditorsDemo.Utils {
    public class FormatWrapper {
        public FormatWrapper() { }
        public FormatWrapper(string name, string format) {
            FormatName = name;
            FormatString = format;
        }
        public string FormatName { get; set; }
        public string FormatString { get; set; }
    }
    public class BaseKindHelper<T> {
        public Array GetEnumMemberList() {
            return Enum.GetValues(typeof(T));
        }
    }
    public class ClickModeKindHelper : BaseKindHelper<ClickMode> { }
    public class TextWrappingKindHelper : BaseKindHelper<TextWrapping> { }
    public class ScrollBarVisibilityKindHelper : BaseKindHelper<System.Windows.Controls.ScrollBarVisibility> { }
    public class CharacterCasingKindHelper : BaseKindHelper<CharacterCasing> { }
    public class NullableToStringConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        string nullString = "Null";
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null)
                return nullString;
            return value.ToString();
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class DecimalToConverter : MarkupExtension, IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Type target = parameter as Type;
            if (target == null)
                return value;
            return System.Convert.ChangeType(value, target, culture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return System.Convert.ToDecimal(value);
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }

    public class IConvertibleConverter : IValueConverter {
        public string ToType { get; set; }
        public string FromType { get; set; }

        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Type target = Type.GetType(ToType, false);
            if (target == null)
                return value;
            return System.Convert.ChangeType(value, target, culture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Type target = Type.GetType(FromType, false);
            if (target == null)
                return value;
            return System.Convert.ChangeType(value, target, culture);
        }

        #endregion
    }

    public static class SchedulerDataHelper {
        const string ResourcePathData = "EditorsDemo.Data";

        public static void LoadTo(SchedulerControl scheduler) {
            SchedulerStorage storage = scheduler.Storage;

            InitCustomAppointmentStatuses(storage);

            storage.AppointmentStorage.Mappings.AllDay = "AllDay";
            storage.AppointmentStorage.Mappings.Description = "Description";
            storage.AppointmentStorage.Mappings.End = "EndTime";
            storage.AppointmentStorage.Mappings.Label = "Label";
            storage.AppointmentStorage.Mappings.Location = "Location";
            storage.AppointmentStorage.Mappings.RecurrenceInfo = "RecurrenceInfo";
            storage.AppointmentStorage.Mappings.ReminderInfo = "ReminderInfo";
            storage.AppointmentStorage.Mappings.Start = "StartTime";
            storage.AppointmentStorage.Mappings.Status = "Status";
            storage.AppointmentStorage.Mappings.Subject = "Subject";
            storage.AppointmentStorage.Mappings.Type = "Type";

            scheduler.Storage.AppointmentStorage.DataSource = LoadFromXml<EventItem>("Events.xml");
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
            }
            finally {
                storage.EndUpdate();
            }
        }

        private static IAppointmentStatus CreateNewAppointmentStatus(IAppointmentStatusStorage storage, AppointmentStatusType type, Color color, string name, string caption) {
            IAppointmentStatus status = storage.CreateNewStatus(null, name, caption);
            status.Type = type;
            status.SetBrush(new SolidColorBrush(color));
            return status;
        }

        static IBindingList LoadFromXml<T>(string fileName) where T : new() {
            SchedulerBindingList<T> eventList = new SchedulerBindingList<T>();
            using (Stream stream = GetDataStream(fileName)) {
                XmlSerializer s = new XmlSerializer(typeof(SchedulerBindingList<T>));
                eventList = (SchedulerBindingList<T>)s.Deserialize(stream);
                stream.Close();
            }
            return eventList;
        }
        static Stream GetDataStream(string fileName) {
            return GetResourceStream(ResourcePathData, fileName);
        }
        static Stream GetResourceStream(string resourcePath, string resourceName) {
            string fullResourceName = string.Format("{0}.{1}", resourcePath, resourceName);
            Stream result = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(fullResourceName);
            if (result != null)
                return result;
            return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        }
    }
    #region SchedulerBindingList
    public class SchedulerBindingList<T> : CollectionBase, IBindingList
    where T : new() {
        public T this[int idx] { get { return (T)base.List[idx]; } }

        public void Add(T appointment) {
            base.List.Add(appointment);
        }
        public virtual object AddNew() {
            T newItem = new T();
            List.Add(newItem);
            return newItem;
        }
        public bool AllowEdit { get { return true; } }
        public bool AllowNew { get { return true; } }
        public bool AllowRemove { get { return true; } }

        private ListChangedEventHandler listChangedHandler;
        public event ListChangedEventHandler ListChanged {
            add { listChangedHandler += value; }
            remove { listChangedHandler -= value; }
        }

        public void AddIndex(PropertyDescriptor pd) { throw new NotSupportedException(); }
        public void ApplySort(PropertyDescriptor pd, ListSortDirection dir) { throw new NotSupportedException(); }
        public int Find(PropertyDescriptor property, object key) { throw new NotSupportedException(); }
        public bool IsSorted { get { return false; } }
        public void RemoveIndex(PropertyDescriptor pd) { throw new NotSupportedException(); }
        public void RemoveSort() { throw new NotSupportedException(); }
        public ListSortDirection SortDirection { get { throw new NotSupportedException(); } }
        public PropertyDescriptor SortProperty { get { throw new NotSupportedException(); } }
        public bool SupportsChangeNotification { get { return true; } }
        public bool SupportsSearching { get { return false; } }
        public bool SupportsSorting { get { return false; } }
    }
    #endregion
    #region EventItem
    public class EventItem {
        public object Type { get; set; }
        public object StartTime { get; set; }
        public object EndTime { get; set; }
        public string Description { get; set; }
        public bool AllDay { get; set; }
        public int Label { get; set; }
        public string Location { get; set; }
        public object ResourceId { get; set; }
        public int Status { get; set; }
        public string Subject { get; set; }
        public object Price { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }

        public EventItem() {
            Type = (int)AppointmentType.Normal;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue + DevExpress.XtraScheduler.Native.DateTimeHelper.HalfHourSpan;
            Description = String.Empty;
            AllDay = false;
            Label = 0;
            Location = String.Empty;
            ResourceId = EmptyResourceId.Id;
            Status = (int)AppointmentStatusType.Free;
            Subject = String.Empty;
            Price = 0.0;
            RecurrenceInfo = String.Empty;
            ReminderInfo = String.Empty;
        }
    }
    #endregion

    public class CategoryDataToImageSourceConverter : BytesToImageSourceConverter {
        static Dictionary<string, object> cachedImages = new Dictionary<string, object>();

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            CategoryData categoryData = value as CategoryData;
            if (value == null) return null;
            if (cachedImages.ContainsKey(categoryData.Name)) {
                return cachedImages[categoryData.Name];
            }
            else {
                object image = base.Convert(categoryData.Picture, targetType, parameter, culture);
                cachedImages.Add(categoryData.Name, image);
                return image;
            }
        }
    }
    public class HyperLinkAttachedBehavior : Behavior<Hyperlink> {
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.RequestNavigate += OnRequestNavigate;
        }
        protected override void OnDetaching() {
            AssociatedObject.RequestNavigate -= OnRequestNavigate;
            base.OnDetaching();
        }
        private void OnRequestNavigate(object sender, RequestNavigateEventArgs e) {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
    public class IssueStatusImageConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null)
                return null;
            string name = ((string)value).Replace(" ", "");
            string path = "EditorsDemo.Images.IssueIcons." + name + ".png";
            return DevExpress.Xpf.Core.Native.ImageHelper.CreateImageFromEmbeddedResource(Assembly.GetExecutingAssembly(), path);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class IdToUriConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value != null) {
                return "http://devexpress.com/Support/Center/p/" + value.ToString() + ".aspx";
            }
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
