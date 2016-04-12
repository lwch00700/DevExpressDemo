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
using DevExpress.Xpf.Scheduler.UI;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Scheduler;

namespace SchedulerDemo.Forms {
    public partial class CustomAppointmentInplaceEditor : AppointmentInplaceEditorBase {
        public CustomAppointmentInplaceEditor(SchedulerControl control, Appointment apt)
            : base(control, apt) {
            InitializeComponent();
        }

        private void OnOkButton(object sender, RoutedEventArgs e) {
            OnCommitChanges();
        }
        private void OnCancelButton(object sender, RoutedEventArgs e) {
            OnRollbackChanges();
        }
        public override void Activate() {
            Dispatcher.BeginInvoke(new Action(() => {
                this.txtSubject.Focus();
                if (IsNewAppointment) {
                    this.txtSubject.SelectionStart = this.txtSubject.Text.Length;
                    this.txtSubject.SelectionLength = 0;
                }
                else
                    this.txtSubject.SelectAll();
            }));
        }
    }
}
