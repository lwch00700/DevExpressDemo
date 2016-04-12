using System;
using System.Windows;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;
using System.Collections.Generic;
using SchedulerDemo.Internal;

namespace SchedulerDemo {
    public partial class AppointmentFiltering : SchedulerDemoModule {
        public static DateTime Start = new DateTime(2010, 09, 15, 10, 0, 0);
        Dictionary<object, SchedulerColorSchema> resourceColorSchemas = new Dictionary<object, SchedulerColorSchema>();

        public AppointmentFiltering() {
            InitializeComponent();
            FillSportsComboBox();
            AddSportChanels();
            PrepareResourceColorSchemas();
            SetStart();
            FillData();
        }

        void SetStart() {
            this.scheduler.Start = Start;
            TimeInterval selectedInterval = this.scheduler.TimelineView.SelectedInterval;
            Resource selectedResource = this.scheduler.TimelineView.SelectedResource;
            this.scheduler.TimelineView.SetSelection(new TimeInterval(Start, selectedInterval.Duration), selectedResource);
        }
        void AddSportChanels() {
            scheduler.Storage.ResourceStorage.BeginUpdate();
            AddResource(0, "SPORT TV 1");
            AddResource(1, "SPORT TV 2");
            AddResource(2, "SPORT TV 3");
            AddResource(3, "SPORT TV 4");
            AddResource(4, "TV 5");
            AddResource(5, "TV 6");
            AddResource(6, "TV 7");
            AddResource(7, "TV 8");
            scheduler.Storage.ResourceStorage.EndUpdate();
        }
        void AddResource(int index, string caption) {
            Resource r = scheduler.Storage.CreateResource(index.ToString());
            r.Caption = caption;
            string imageName = string.Format("tv{0}.png", index + 1);
            BitmapImage bitmapImage = SchedulerDataHelper.GetImageFromResource(imageName);

            r.SetImage(bitmapImage);
            scheduler.Storage.ResourceStorage.Add(r);
        }

        private void FillData() {
            scheduler.Storage.AppointmentStorage.DataSource = SchedulerXmlHelper.LoadFromXml<SportItem>("Sports.xml");
        }

        private void FillSportsComboBox() {
            cbSports.BeginInit();
            for (int i = 0; i < scheduler.Storage.AppointmentStorage.Labels.Count; i++) {
                IAppointmentLabel lab = scheduler.Storage.AppointmentStorage.Labels.GetByIndex(i);
                int index = cbSports.Items.Add(lab.DisplayName);
                cbSports.SelectedItems.Add(cbSports.Items[index]);
            }
            cbSports.EndInit();
        }

        private void SchedulerStorage_FilterAppointment(object sender, PersistentObjectCancelEventArgs e) {
            Appointment apt = (Appointment)e.Object;
            e.Cancel = !IsAppointmentVisible((int)apt.LabelKey);
        }
        bool IsAppointmentVisible(int labelId) {
            int count = cbSports.SelectedItems.Count;
            for (int i = 0; i < count; i++) {
                int selectedIndex = cbSports.Items.IndexOf(cbSports.SelectedItems[i]);
                if (selectedIndex == labelId)
                    return true;
            }
            return false;
        }
        private void cbSports_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            scheduler.ActiveView.LayoutChanged();
        }
        private void PrepareResourceColorSchemas() {
            int count = scheduler.Storage.ResourceStorage.Count;
            SchedulerColorSchemaCollection currentColorchemas = scheduler.GetResourceColorSchemasCopy();
            int schemaCount = currentColorchemas.Count;
            for (int i = 0; i < count; i++) {
                Resource resource = scheduler.Storage.ResourceStorage[i];
                resourceColorSchemas.Add(resource.Id, currentColorchemas[i % schemaCount]);
            }
        }
        private void OnQueryResourceColorSchema(object sender, QueryResourceColorSchemaEventArgs e) {
            object key = e.Resource.Id;
            if (this.resourceColorSchemas.ContainsKey(key))
                e.ResourceColorSchema = this.resourceColorSchemas[key];
        }
    }
}
