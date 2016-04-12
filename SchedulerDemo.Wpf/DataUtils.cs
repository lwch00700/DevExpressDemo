using System;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.XtraScheduler;
using System.IO;
using DevExpress.Xpf.Scheduler;
using System.Windows.Media.Imaging;
using System.Collections;
using DevExpress.Utils.Serializing;
using System.Xml;
using DevExpress.XtraScheduler.Native;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using System.Reflection;
using System.Windows;
using DevExpress.XtraScheduler.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;

namespace SchedulerDemo {
    public class CustomEvent {
        object id;
        DateTime start;
        DateTime end;
        string subject;
        int status;
        string description;
        int label;
        string location;
        bool allday;
        int eventType;
        string recurrenceInfo;
        string reminderInfo;
        object ownerId;
        double price;
        string contactInfo;

        public DateTime StartTime { get { return start; } set { start = value; } }
        public DateTime EndTime { get { return end; } set { end = value; } }
        public string Subject { get { return subject; } set { subject = value; } }
        public int Status { get { return status; } set { status = value; } }
        public string Description { get { return description; } set { description = value; } }
        public int Label { get { return label; } set { label = value; } }
        public string Location { get { return location; } set { location = value; } }
        public bool AllDay { get { return allday; } set { allday = value; } }
        public int EventType { get { return eventType; } set { eventType = value; } }
        public string RecurrenceInfo { get { return recurrenceInfo; } set { recurrenceInfo = value; } }
        public string ReminderInfo { get { return reminderInfo; } set { reminderInfo = value; } }
        public object OwnerId { get { return ownerId; } set { ownerId = value; } }
        public object Id { get { return id; } set { id = value; } }
        public double Price { get { return price; } set { price = value; } }
        public string ContactInfo { get { return contactInfo; } set { contactInfo = value; } }
    }
    public class CustomResource {
        object id;
        string caption;

        public object Id { get { return id; } set { id = value; } }
        public string Caption { get { return caption; } set { caption = value; } }
    }

    public class SchedulerItemList<T> : CollectionBase, IBindingList where T : new() {
        public T this[int idx] { get { return (T)base.List[idx]; } }

        public void Add(T item) {
            base.List.Add(item);
        }
        public int IndexOf(T item) {
            return List.IndexOf(item);
        }
        public object AddNew() {
            T app = new T();
            List.Add(app);
            return app;
        }
        public bool AllowEdit { get { return true; } }
        public bool AllowNew { get { return true; } }
        public bool AllowRemove { get { return true; } }

        private ListChangedEventHandler listChangedHandler;
        public event ListChangedEventHandler ListChanged {
            add { listChangedHandler += value; }
            remove { listChangedHandler -= value; }
        }
        internal void OnListChanged(ListChangedEventArgs args) {
            if (listChangedHandler != null) {
                listChangedHandler(this, args);
            }
        }
        protected override void OnClearComplete() {
            base.OnClearComplete();
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        protected override void OnRemoveComplete(int index, object value) {
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
        }
        protected override void OnInsertComplete(int index, object value) {
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
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

    public class DemoUtils {
        const string ResourcePathData = "SchedulerDemo.Data";
        static string[] Users = new string[] { "Peter Dolan", "Ryan Fischer", "Andrew Miller", "Tom Hamlett",
                                               "Jerry Campbell", "Carl Lucas", "Mark Hamilton", "Steve Lee"  };
        public static Random RandomInstance = new Random();
        public static DateTime Date = new DateTime(2010, 7, 13);
        public static readonly Dictionary<int, string> ResourceList = new Dictionary<int, string>();
        static string[] taskDescriptions = new string[] {
                                                   "Implementing Developer Express MasterView control into Accounting System.",
                                                   "Web Edition: Data Entry Page. The issue with date validation.",
                                                   "Payables Due Calculator. It is ready for testing.",
                                                   "Web Edition: Search Page. It is ready for testing.",
                                                   "Main Menu: Duplicate Items. Somebody has to review all menu items in the system.",
                                                   "Receivables Calculator. Where can I found the complete specs",
                                                   "Ledger: Inconsistency. Please fix it.",
                                                   "Receivables Printing. It is ready for testing.",
                                                   "Screen Redraw. Somebody has to look at it.",
                                                   "Email System. What library we are going to use?",
                                                   "Adding New Vendors Fails. This module doesn't work completely!",
                                                   "History. Will we track the sales history in our system?",
                                                   "Main Menu: Add a File menu. File menu is missed!!!",
                                                   "Currency Mask. The current currency mask in completely inconvinience.",
                                                   "Drag & Drop. In the schedule module drag & drop is not available.",
                                                   "Data Import. What competitors databases will we support?",
                                                   "Reports. The list of incomplete reports.",
                                                   "Data Archiving. This features is still missed in our application",
                                                   "Email Attachments. How to add the multiple attachment? I did not find a way to do it.",
                                                   "Check Register. We are using different paths for different modules.",
                                                   "Data Export. Our customers asked for export into Excel"};

        static DemoUtils() {
            int count = Users.Length;
            for (int i = 0; i < count; i++)
                ResourceList.Add(i, Users[i]);
        }
        public static Stream GetResourceStream(string resourcePath, string resourceName) {
            string fullResourceName = string.Format("{0}.{1}", resourcePath, resourceName);
            Stream result = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(fullResourceName);
            if (result != null)
                return result;
            return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        }
        public static BitmapImage ConvertBitmapToBitmapImage(Image source) {
            MemoryStream stream = new MemoryStream();
            source.Save(stream, ImageFormat.Png);
            stream.Position = 0;
            BitmapImage result = new BitmapImage();
            result.BeginInit();
            result.StreamSource = stream;
            result.EndInit();
            return result;
        }
        public static MemoryStream ConvertImageToMemoryStream(Image img) {
            var ms = new MemoryStream();
            img.Save(ms, ImageFormat.Png);
            return ms;
        }
        public static void FillResources(SchedulerStorage storage, int maxCount) {
            ResourceStorage resources = storage.ResourceStorage;
            storage.BeginUpdate();
            try {
                resources.Clear();
                int count = Math.Min(maxCount, ResourceList.Count);
                for (int i = 0; i < count; i++) {
                    string caption = string.Empty;
                    if (ResourceList.TryGetValue(i, out caption)) {
                        if (string.IsNullOrEmpty(caption))
                            continue;

                        Resource resource = storage.CreateResource(i);
                        resource.Caption = caption;
                        resources.Add(resource);
                    }
                }

            }
            finally {
                storage.EndUpdate();
            }
        }

        static string[] outlookCalendarPaths = null;

        public static DataTable GetSportEventsData() {
            DataSet sportEventDS = new DataSet();
            using (Stream stream = GetResourceStream(ResourcePathData, "sportevents.xml")) {
                sportEventDS.ReadXml(stream);
                stream.Close();
            }
            return sportEventDS.Tables[0];
        }
        public static DataTable GenerateScheduleTasks() {
            DataTable tbl = new DataTable();
            tbl = new DataTable("ScheduleTasks");
            tbl.Columns.Add("ID", typeof(int));
            tbl.Columns.Add("Subject", typeof(string));
            tbl.Columns.Add("Severity", typeof(int));
            tbl.Columns.Add("Priority", typeof(int));
            tbl.Columns.Add("Duration", typeof(int));
            tbl.Columns.Add("Description", typeof(string));
            for (int i = 0; i < 21; i++) {
                string description = taskDescriptions[i];
                int index = description.IndexOf('.');
                string subject;
                if (index <= 0)
                    subject = "task" + Convert.ToInt32(i + 1);
                else
                    subject = description.Substring(0, index);
                tbl.Rows.Add(new object[] { i + 1, subject, RandomInstance.Next(3), RandomInstance.Next(3), Math.Max(1, RandomInstance.Next(8)), description });
            }
            return tbl;
        }

        public static string[] OutlookCalendarPaths {
            get {
                if (outlookCalendarPaths != null)
                    return outlookCalendarPaths;

                try {
                    outlookCalendarPaths = DevExpress.XtraScheduler.Outlook.OutlookExchangeHelper.GetOutlookCalendarPaths();
                }
                catch {
                    ReportOutlookWarning("get the list of available calendars from Microsoft Outlook");
                    outlookCalendarPaths = new string[0];
                }
                return outlookCalendarPaths;
            }
        }
        public static void ReportOutlookWarning(string message) {
            DXMessageBox.Show(String.Format("Unable to {0}.\nCheck whether MS Outlook is installed.", message), "Import from MS Outlook", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public static void ReportOutlookError(string message) {
            DXMessageBox.Show(String.Format("Impossible to {0}. Probably, Microsoft Outlook isn't installed on this machine.", message), Assembly.GetEntryAssembly().FullName, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void ReportOutlookError(string message, string exceptionMessage) {
            DXMessageBox.Show(String.Format("Failed to {0}. An exception has occured:\r\n{1}", message, exceptionMessage), Assembly.GetEntryAssembly().FullName, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
namespace SchedulerDemo.Internal {
    public abstract class SchedulerDemoXmlPersistenceHelper<T> : CollectionXmlPersistenceHelper where T : new() {
        public SchedulerDemoXmlPersistenceHelper()
            : base(null) {
        }
        public SchedulerDemoXmlPersistenceHelper(SchedulerItemList<T> eventList)
            : base(eventList) {
        }

        protected override string XmlCollectionName { get { return AppointmentSR.XmlCollectionName; } }
        SchedulerItemList<T> Items { get { return (SchedulerItemList<T>)Collection; } }

        protected override IXmlContextItem CreateXmlContextItem(object obj) {
            return null;
        }
    }
    public class EventItemXmlPersistenceHelper : SchedulerDemoXmlPersistenceHelper<EventItem> {
        protected override ObjectCollectionXmlLoader CreateObjectCollectionXmlLoader(XmlNode root) {
            return new EventItemListXmlLoader(root);
        }
    }
    public class CarItemXmlPersistenceHelper : SchedulerDemoXmlPersistenceHelper<CarItem> {
        protected override ObjectCollectionXmlLoader CreateObjectCollectionXmlLoader(XmlNode root) {
            return new CarItemListXmlLoader(root);
        }
    }

    public abstract class SchedulerDemoCollectionXmlLoader<T> : ObjectCollectionXmlLoader where T : new() {
        SchedulerItemList<T> items = new SchedulerItemList<T>();

        protected SchedulerDemoCollectionXmlLoader(XmlNode root)
            : base(root) {
        }
        protected override ICollection Collection { get { return items; } }
        protected override string XmlCollectionName { get { return AppointmentSR.XmlCollectionName; } }
        public SchedulerItemList<T> Items { get { return items; } }

        protected override void AddObjectToCollection(object obj) {
            Items.Add((T)obj);
        }
        protected override void ClearCollectionObjects() {
            Items.Clear();
        }
        protected virtual ObjectXmlLoader CreateItemLoader(XmlNode root) {
            return new EventItemXmlLoader(root);
        }
        protected override object LoadObject(XmlNode root) {
            ObjectXmlLoader loader = CreateItemLoader(root);
            return loader.ObjectFromXml();
        }
    }
    public class EventItemListXmlLoader : SchedulerDemoCollectionXmlLoader<EventItem> {
        public EventItemListXmlLoader(XmlNode root)
            : base(root) {
        }
        protected override ObjectXmlLoader CreateItemLoader(XmlNode root) {
            return new EventItemXmlLoader(root);
        }
    }
    public class CarItemListXmlLoader : SchedulerDemoCollectionXmlLoader<CarItem> {
        public CarItemListXmlLoader(XmlNode root)
            : base(root) {
        }
        protected override ObjectXmlLoader CreateItemLoader(XmlNode root) {
            return new CarItemXmlLoader(root);
        }
    }

    public class EventItemXmlLoader : ObjectXmlLoader {
        XmlNode root;
        public EventItemXmlLoader(XmlNode root)
            : base(root) {
            this.root = root;
        }
        public XmlNode Root { get { return root; } }

        public override object ObjectFromXml() {
            EventItem ev = new EventItem();
            ev.Type = ReadAttributeAsInt("Type", 0);
            ev.StartTime = ReadAttributeAsDateTime("Start", DateTime.MinValue);
            ev.EndTime = ReadAttributeAsDateTime("End", DateTime.MinValue);
            ev.Label = ReadAttributeAsInt("Label", 0);
            ev.Description = ReadAttributeAsString("Description", String.Empty);
            ev.Location = ReadAttributeAsString("Location", String.Empty);
            ev.ResourceId = ReadAttributeAsString("ResourceId", String.Empty);
            ev.Status = ReadAttributeAsInt("Status", 0);
            ev.Subject = ReadAttributeAsString("Subject", String.Empty);
            ev.Price = ReadAttributeAsInt("Price", 0);
            ev.RecurrenceInfo = ReadChildNodesAsString("RecurrenceInfo", String.Empty);
            ev.ReminderInfo = ReadChildNodesAsString("Reminder", String.Empty);
            return ev;
        }
        string ReadChildNodesAsString(string name, string defaultValue) {
            string obj = defaultValue;
            foreach (XmlNode node in Root.ChildNodes) {
                if (node.Name == name)
                    obj = node.OuterXml;
            }
            return obj;
        }
    }
    public class CarItemXmlLoader : ObjectXmlLoader {
        public CarItemXmlLoader(XmlNode root)
            : base(root) {
        }
        public override object ObjectFromXml() {
            CarItem car = new CarItem();
            car.Caption = ReadAttributeAsString("Caption", String.Empty);
            car.Id = ReadAttributeAsString("Id", String.Empty);
            car.Picture = (byte[])ReadAttributeValue("Picture", typeof(byte[]));
            return car;
        }
    }

    #region EventItem
    public class EventItem {
        public object Type { get; set; }
        public object StartTime { get; set; }
        public object EndTime { get; set; }
        public string Description { get; set; }
        public bool AllDay { get; set; }
        public object Label { get; set; }
        public string Location { get; set; }
        public object ResourceId { get; set; }
        public object Status { get; set; }
        public string Subject { get; set; }
        public object Price { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }

        public EventItem() {
            Type = (int)AppointmentType.Normal;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue + DateTimeHelper.HalfHourSpan;
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
    #region CarItem
    public class CarItem {
        public string Caption { get; set; }
        public object Id { get; set; }
        public byte[] Picture { get; set; }

        public CarItem() {
            Caption = String.Empty;
            Id = null;
            Picture = null;
        }
    }
    #endregion
    #region SportItem
    public class SportItem {
        public object StartTime { get; set; }
        public object EndTime { get; set; }
        public string Caption { get; set; }
        public bool AllDay { get; set; }
        public object ResourceId { get; set; }
        public object SportId { get; set; }

        public SportItem() {
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue + DateTimeHelper.HalfHourSpan;
            Caption = String.Empty;
            AllDay = false;
            ResourceId = EmptyResourceId.Id;
            SportId = 0;
        }
    }
    #endregion
    #region SchedulerBindingList<T>
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
    #region XmlHelper
    public static class SchedulerXmlHelper {
        delegate T MapperHandler<T, U>(U u);
        const string ResourcePathData = "SchedulerDemo.Data";
        public static void WriteEventsToXml(AppointmentCollection collectoin) {
            WriteToXmlCore<EventItem, Appointment>(collectoin, SchedulerDataHelper.EventsXmlSource, new MapperHandler<EventItem, Appointment>(CreateEventItemFromAppointment));
        }
        public static void WriteSportEventsToXml(AppointmentCollection collectoin) {
            WriteToXmlCore<SportItem, Appointment>(collectoin, "sportevents.xml", new MapperHandler<SportItem, Appointment>(CreateSportItemFromAppointment));
        }
        public static void WriteCarsToXml(ResourceCollection collection) {
            WriteToXmlCore<CarItem, Resource>(collection, SchedulerDataHelper.CarsXmlSource, new MapperHandler<CarItem, Resource>(MapResourceToCarItem));
        }
        public static Stream GetDataStream(string fileName) {
            return DemoUtils.GetResourceStream(ResourcePathData, fileName);
        }
        public static SchedulerBindingList<T> LoadFromXml<T>(string fileName) where T : new() {
            SchedulerBindingList<T> eventList = new SchedulerBindingList<T>();
            using (Stream stream = GetDataStream(fileName)) {
                XmlSerializer s = new XmlSerializer(typeof(SchedulerBindingList<T>));
                eventList = (SchedulerBindingList<T>)s.Deserialize(stream);
                stream.Close();
            }
            return eventList;
        }
        static EventItem CreateEventItemFromAppointment(Appointment appointment) {
            EventItem item = new EventItem();
            if (appointment.Reminder != null)
                item.ReminderInfo = CreateReminderString(appointment);
            item.Description = appointment.Description;
            item.AllDay = appointment.AllDay;
            item.EndTime = appointment.End;
            item.Label = appointment.LabelKey;
            item.Location = appointment.Location;
            if (appointment.RecurrenceInfo != null)
                item.RecurrenceInfo = appointment.RecurrenceInfo.ToXml();
            item.ResourceId = appointment.ResourceId;
            item.StartTime = appointment.Start;
            item.Status = appointment.StatusKey;
            item.Subject = appointment.Subject;
            item.Type = appointment.Type;
            return item;
        }
        static SportItem CreateSportItemFromAppointment(Appointment appointment) {
            SportItem item = new SportItem();
            item.Caption = appointment.Subject;
            item.AllDay = appointment.AllDay;
            item.EndTime = appointment.End;
            item.ResourceId = appointment.ResourceId;
            item.SportId = appointment.LabelKey;
            item.StartTime = appointment.Start;
            return item;
        }
        private static string CreateReminderString(Appointment appointment) {
            ReminderXmlPersistenceHelper helper = new ReminderXmlPersistenceHelper(appointment.Reminder);
            return helper.ToXml();
        }
        static CarItem MapResourceToCarItem(Resource resource) {
            CarItem item = new CarItem();
            item.Caption = resource.Caption;
            item.Id = resource.Id;
            return item;
        }
        static void WriteToXmlCore<T, U>(NotificationCollection<U> sourceCollection, string fileName, MapperHandler<T, U> map) {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Write)) {
                NotificationCollection<T> result = new NotificationCollection<T>();
                foreach (U item in sourceCollection)
                    result.Add(map(item));
                XmlSerializer s = new XmlSerializer(typeof(NotificationCollection<T>));
                s.Serialize(stream, result);
                stream.Close();
            }
        }
    }
    #endregion
}
