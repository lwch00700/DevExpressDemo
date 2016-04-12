using System;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Core;

namespace SchedulerDemo {
    public partial class DragDropData : SchedulerDemoModule {
        Point startPoint;
        bool startDrag = false;

        public DragDropData() {
            InitializeComponent();
            InitializeSchedulerProperties(scheduler);
            DemoUtils.FillResources(scheduler.Storage, 5);
            grdTasks.ItemsSource = DemoUtils.GenerateScheduleTasks();

            scheduler.AppointmentDrop += new AppointmentDragEventHandler(scheduler_AppointmentDrop);
            SubscribeGridEvents();
            scheduler.ShowBorder = true;
        }

        void SubscribeGridEvents() {
            grdTasks.View.PreviewMouseDown += new MouseButtonEventHandler(View_PreviewMouseDown);
            grdTasks.View.PreviewMouseMove += new MouseEventHandler(View_PreviewMouseMove);
        }

        void View_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if(e.LeftButton != MouseButtonState.Pressed)
                return;
            this.startPoint = e.GetPosition(null);
            this.startDrag = IsGridRowAvailable(e);
        }
        void View_PreviewMouseMove(object sender, MouseEventArgs e) {
            Point position = e.GetPosition(null);
            if(this.startDrag && e.LeftButton == MouseButtonState.Pressed && IsGridRowAvailable(e) && (Math.Abs(position.X - this.startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(position.Y - this.startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)) {
                DataViewBase view = grdTasks.View;
                int rowHandler = view.GetRowHandleByMouseEventArgs(e);
                object schedulerData = GetSchedulerData(rowHandler);
                this.startDrag = false;
                DragDropEffects de = DragDrop.DoDragDrop(grdTasks, schedulerData, DragDropEffects.Move);
            }
            this.startPoint = e.GetPosition(null);
        }
        object GetSchedulerData(int rowHandler) {
            AppointmentBaseCollection appointments = new AppointmentBaseCollection();
            Appointment apt = scheduler.Storage.CreateAppointment(AppointmentType.Normal);
            apt.Subject = (string)ObtaiDataFromRow(rowHandler, "Subject");
            apt.LabelKey = (int)ObtaiDataFromRow(rowHandler, "Severity");
            apt.StatusKey = (int)ObtaiDataFromRow(rowHandler, "Priority");
            apt.Duration = TimeSpan.FromHours((int)ObtaiDataFromRow(rowHandler, "Duration"));
            apt.Description = (string)ObtaiDataFromRow(rowHandler, "Description");
            appointments.Add(apt);
            return new SchedulerDragData(appointments, 0);
        }
        object ObtaiDataFromRow(int rowHandler, string column) {
            return grdTasks.GetCellValue(rowHandler, grdTasks.Columns[column]);
        }
        bool IsGridRowAvailable(MouseEventArgs e) {
            int rowHandler = grdTasks.View.GetRowHandleByMouseEventArgs(e);
            return grdTasks.GetRow(rowHandler) != null;
        }

        void scheduler_AppointmentDrop(object sender, AppointmentDragEventArgs e) {
            string createEventMsg = "Creating an event at {0} on {1}.";
            string moveEventMsg = "Moving the event from {0} on {1} to {2} on {3}.";

            DateTime srcStart = e.SourceAppointment.Start;
            DateTime newStart = e.EditedAppointment.Start;

            string msg = (srcStart == DateTime.MinValue) ? String.Format(createEventMsg, newStart.ToShortTimeString(), newStart.ToShortDateString()) :
                String.Format(moveEventMsg, srcStart.ToShortTimeString(), srcStart.ToShortDateString(), newStart.ToShortTimeString(), newStart.ToShortDateString());

            if(DXMessageBox.Show(msg + "\r\nProceed?", "Demo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) {
                e.Allow = false;
            }

            scheduler.Focus();
        }
    }
}
