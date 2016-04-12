using System;
using System.ComponentModel;

namespace SchedulerDemo {
    public partial class ObjectDataProvider : SchedulerDemoModule {
        public ObjectDataProvider() {
            InitializeComponent();
        }
    }
    public class CustomEventProvider {
        public static BindingList<CustomEvent> GetCustomEvents() {
            BindingList<CustomEvent> result = new BindingList<CustomEvent>();
            int count = DemoUtils.ResourceList.Count;
            for(int i = 0; i < count; i++) {
                string subjPrefix = DemoUtils.ResourceList[i] + "'s ";
                result.Add(CreateEvent(subjPrefix + "meeting", i, 2, 5));
                result.Add(CreateEvent(subjPrefix + "travel", i, 3, 6));
                result.Add(CreateEvent(subjPrefix + "phone call", i, 0, 10));
            }
            return result;
        }
        static CustomEvent CreateEvent(string subject, object resourceId, int status, int label) {
            CustomEvent apt = new CustomEvent();
            apt.Subject = subject;
            apt.OwnerId = resourceId;
            Random rnd = DemoUtils.RandomInstance;
            int rangeInMinutes = 60 * 24;
            apt.StartTime = DateTime.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes));
            apt.EndTime = apt.StartTime + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes / 4));
            apt.Status = status;
            apt.Label = label;
            return apt;
        }

    }
    public class CustomResourceProvider {
        public static BindingList<CustomResource> GetCustomResources() {
            BindingList<CustomResource> result = new BindingList<CustomResource>();
            int count = DemoUtils.ResourceList.Count;
            for(int i = 0; i < count; i++) {
                CustomResource customResource = new CustomResource();
                customResource.Caption = DemoUtils.ResourceList[i];
                customResource.Id = i;
                result.Add(customResource);
            }
            return result;
        }
    }
}
