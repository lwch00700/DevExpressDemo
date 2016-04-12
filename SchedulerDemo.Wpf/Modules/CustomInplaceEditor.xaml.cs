using System;
using SchedulerDemo.Forms;
using System.Windows;

namespace SchedulerDemo {
    public partial class CustomInplaceEditor : SchedulerDemoModule {
        public CustomInplaceEditor() {
            InitializeComponent();
            InitializeScheduler();
            SubscribeToInplaceEditorShowingEvent();
        }

        private void cheCustomInplaceEditor_Checked(object sender, System.Windows.RoutedEventArgs e) {
            SubscribeToInplaceEditorShowingEvent();
        }
        private void cheCustomInplaceEditor_Unchecked(object sender, System.Windows.RoutedEventArgs e) {
            UnsubscribeToInplaceEditorShowingEvent();
        }
        private void SubscribeToInplaceEditorShowingEvent() {
            UnsubscribeToInplaceEditorShowingEvent();
            this.scheduler.InplaceEditorShowing += OnInplaceEditorShowing;
        }
        private void UnsubscribeToInplaceEditorShowingEvent() {
            this.scheduler.InplaceEditorShowing -= OnInplaceEditorShowing;
        }
        void OnInplaceEditorShowing(object sender, DevExpress.Xpf.Scheduler.InplaceEditorEventArgs e) {
            CustomAppointmentInplaceEditor editor = new CustomAppointmentInplaceEditor(this.scheduler, e.Appointment);
            e.InplaceEditor = editor;
            editor.DataContext = editor;
            Rect bounds = this.scheduler.RenderTransform.TransformBounds(e.Bounds);
            Size controlSize = this.scheduler.DesiredSize;
            Point location = e.Bounds.TopRight;
            if ((controlSize.Width - location.X) > editor.Width)
                e.Bounds = new Rect(location, new Size(editor.Width, editor.Height));
            else if (e.Bounds.X > editor.Width) {
                location = new Point(bounds.X - editor.Width, e.Bounds.Y);
                e.Bounds = new Rect(location, new Size(editor.Width, editor.Height));
            }
            else
                e.Bounds = new Rect(bounds.TopLeft, new Size(editor.Width, editor.Height));
        }
    }
}
