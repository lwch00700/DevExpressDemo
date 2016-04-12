using System;
using System.Xml;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Collections.Generic;
using DevExpress.XtraScheduler.Native;
using DevExpress.Xpf.Scheduler;
using System.Collections;
using SchedulerDemo.Internal;
using DevExpress.Mvvm;
using DevExpress.XtraScheduler.Internal;

namespace SchedulerDemo {
    public class DefaultDemoViewModel : BindableBase {
        IList appointmentListCore;
        IList resourceListCore;
        Dictionary<string, string> appointmentMappingsDictionaryCore = new Dictionary<string, string>();
        Dictionary<string, string> resourceMappingsDictionaryCore = new Dictionary<string, string>();
        DateTime startTimeCore = DateTime.MinValue;
        bool showBorderCore = false;

        public DefaultDemoViewModel() {
            this.appointmentListCore = CreateAppointmentList();
            this.resourceListCore = CreateResourceList();

            this.appointmentMappingsDictionaryCore = CreateAppointmentMappings();
            this.resourceMappingsDictionaryCore = CreaterRsourceMappings();

            StartTime = new DateTime(2010, 07, 12);
        }

        public IList AppointmentsList { get { return appointmentListCore; } }
        public Dictionary<string, string> AppointmentMappingsDictionary { get { return appointmentMappingsDictionaryCore; } }
        public IList ResourcesList { get { return resourceListCore; } }
        public Dictionary<string, string> ResourceMappingsDictionary { get { return resourceMappingsDictionaryCore; } }
        public DateTime StartTime {
            get { return startTimeCore; }
            set { SetProperty(ref startTimeCore, value, "StartTime"); }
        }
        public bool ShowBorder {
            get { return showBorderCore; }
            set { SetProperty(ref showBorderCore, value, "ShowBorder"); }
        }
        protected virtual IList CreateAppointmentList() {
            EventItemXmlPersistenceHelper helper = new EventItemXmlPersistenceHelper();
            XmlDocument doc = SchedulerDataHelper.LoadXmlDocumentFromResource(SchedulerDataHelper.DefaultAppointmentXmlSource);
            return helper.FromXmlNode(doc) as IList;
        }
        protected virtual IList CreateResourceList() {
            CarItemXmlPersistenceHelper helper = new CarItemXmlPersistenceHelper();
            XmlDocument doc = SchedulerDataHelper.LoadXmlDocumentFromResource(SchedulerDataHelper.DefaultResourceXmlSource);
            return helper.FromXmlNode(doc) as IList;
        }
        protected virtual Dictionary<string, string> CreaterRsourceMappings() {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add(ResourceSR.Caption, "Caption");
            result.Add(ResourceSR.Color, "Color");
            result.Add(ResourceSR.Id, "Id");
            result.Add(ResourceSR.Image, "Image");
            return result;
        }
        protected virtual Dictionary<string, string> CreateAppointmentMappings() {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add(AppointmentSR.AllDay, "AllDay");
            result.Add(AppointmentSR.Description, "Description");
            result.Add(AppointmentSR.End, "EndTime");
            result.Add(AppointmentSR.Label, "Label");
            result.Add(AppointmentSR.Location, "Location");
            result.Add(AppointmentSR.RecurrenceInfo, "RecurrenceInfo");
            result.Add(AppointmentSR.ReminderInfo, "ReminderInfo");
            result.Add(AppointmentSR.ResourceId, "ResourceId");
            result.Add(AppointmentSR.Start, "StartTime");
            result.Add(AppointmentSR.Status, "Status");
            result.Add(AppointmentSR.Subject, "Subject");
            result.Add(AppointmentSR.Type, "Type");
            return result;
        }
    }

    public class AppointmentDictionaryToMappingConverter : IValueConverter {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            AppointmentMapping appointmentMappings = new AppointmentMapping();
            Dictionary<string, string> mappings = value as Dictionary<string, string>;
            if(mappings != null) {
                if(mappings.ContainsKey("AllDay"))
                    appointmentMappings.AllDay = mappings["AllDay"];
                if(mappings.ContainsKey("Description"))
                    appointmentMappings.Description = mappings["Description"];
                if(mappings.ContainsKey("End"))
                    appointmentMappings.End = mappings["End"];
                if(mappings.ContainsKey("Label"))
                    appointmentMappings.Label = mappings["Label"];
                if(mappings.ContainsKey("Location"))
                    appointmentMappings.Location = mappings["Location"];
                if(mappings.ContainsKey("RecurrenceInfo"))
                    appointmentMappings.RecurrenceInfo = mappings["RecurrenceInfo"];
                if(mappings.ContainsKey("ReminderInfo"))
                    appointmentMappings.ReminderInfo = mappings["ReminderInfo"];
                if(mappings.ContainsKey("ResourceId"))
                    appointmentMappings.ResourceId = mappings["ResourceId"];
                if(mappings.ContainsKey("Start"))
                    appointmentMappings.Start = mappings["Start"];
                if(mappings.ContainsKey("Status"))
                    appointmentMappings.Status = mappings["Status"];
                if(mappings.ContainsKey("Subject"))
                    appointmentMappings.Subject = mappings["Subject"];
                if(mappings.ContainsKey("Type"))
                    appointmentMappings.Type = mappings["Type"];
            }
            return appointmentMappings;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class ResourceDictionaryToMappingConverter : IValueConverter {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            ResourceMapping resourceMappings = new ResourceMapping();
            Dictionary<string, string> mappings = value as Dictionary<string, string>;
            if(mappings != null) {
                if(mappings.ContainsKey("Caption"))
                    resourceMappings.Caption = mappings["Caption"];
                if(mappings.ContainsKey("Color"))
                    resourceMappings.Color = mappings["Color"];
                if(mappings.ContainsKey("Id"))
                    resourceMappings.Id = mappings["Id"];
                if(mappings.ContainsKey("Image"))
                    resourceMappings.Image = mappings["Image"];
            }
            return resourceMappings;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
