using System;
using System.Windows;
using SchedulerDemo;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Scheduler;
using System.Windows.Input;
using DevExpress.Xpf.Scheduler.UI;
using System.ComponentModel;
using DevExpress.Mvvm;

namespace SchedulerDemo {
    public partial class CustomMenu : SchedulerDemoModule {
        CustomMenuDemoAppointmentCommandsCollection commandsCollection;

        public CustomMenu() {
            InitializeComponent();
            InitializeScheduler();
            this.scheduler.PopupMenuShowing += new SchedulerMenuEventHandler(PopupMenuShowing);
            CustomMenuModule.DataContext = this;
            commandsCollection = new CustomMenuDemoAppointmentCommandsCollection(scheduler);
        }

        public CustomMenuDemoAppointmentCommandsCollection CommandsCollection { get { return commandsCollection; } }

        void PopupMenuShowing(object sender, SchedulerMenuEventArgs e) {
            if(e.Menu.Name == SchedulerMenuItemName.AppointmentMenu) {
                e.Customizations.Add(changeAppointment);
            }
        }
    }

    public abstract class AppointmentCommandBase : BindableBase, ICommand {
        SchedulerControl controlCore = null;
        string subjectCore;
        int labelIdCore;

        public AppointmentCommandBase(SchedulerControl control, string subject, int labelId) {
            Control = control;
            subjectCore = subject;
            labelIdCore = labelId;
        }
        public AppointmentCommandBase()
            : this(null, String.Empty, 0) {
        }

        public SchedulerControl Control {
            get { return controlCore; }
            set { SetProperty(ref controlCore, value, "Control"); }
        }
        public string Subject { get { return subjectCore; } set { subjectCore = value; } }
        public int LabelId { get { return labelIdCore; } set { labelIdCore = value; } }

        #region ICommand Members
        bool ICommand.CanExecute(object parameter) {
            return true;
        }
        event EventHandler ICommand.CanExecuteChanged { add { _canExecuteChanged += value; } remove { _canExecuteChanged -= value; } }
        event EventHandler _canExecuteChanged;
        protected void FakeMethod() {
            _canExecuteChanged(this, EventArgs.Empty);
        }
        void ICommand.Execute(object parameter) {
            Execute(parameter);
        }
        #endregion
        public abstract void Execute(object parameter);
    }
    public class CreateAppointmentCommand : AppointmentCommandBase {
        public CreateAppointmentCommand(SchedulerControl control, string subject, int labelId)
            : base(control, subject, labelId) {
        }
        public CreateAppointmentCommand()
            : this(null, String.Empty, 0) {
        }

        public override void Execute(object parameter) {
            Appointment apt = Control.Storage.CreateAppointment(AppointmentType.Normal);
            apt.Subject = Subject;
            apt.Start = Control.ActiveView.SelectedInterval.Start;
            apt.End = Control.ActiveView.SelectedInterval.End;
            apt.ResourceId = Control.ActiveView.SelectedResource.Id;
            apt.StatusKey = 1;
            apt.LabelKey = LabelId;
            Control.Storage.AppointmentStorage.Add(apt);
        }
    }
    public class ChangeAppointmentCommand : AppointmentCommandBase {
        public ChangeAppointmentCommand(SchedulerControl control, string subject, int labelId)
            : base(control, subject, labelId) {
        }
        public ChangeAppointmentCommand()
            : this(null, String.Empty, 0) {
        }

        public override void Execute(object parameter) {
            AppointmentBaseCollection appointments = Control.SelectedAppointments;
            for(int i = 0; i < appointments.Count; i++) {
                Appointment apt = appointments[i];
                apt.Subject = Subject;
                apt.StatusKey = 1;
                apt.LabelKey = LabelId;
            }
        }
    }

    public class CustomMenuDemoAppointmentCommandsCollection : BindableBase {
        public CustomMenuDemoAppointmentCommandsCollection(SchedulerControl control) {
            ChangeCheckEngineOilCommand = new ChangeAppointmentCommand(control, "Check engine oil", 1);
            CreateCheckEngineOilCommand = new CreateAppointmentCommand(control, "Check engine oil", 1);
            ChangeWashTheCarCommand = new ChangeAppointmentCommand(control, "Wash the car", 2);
            CreateWashTheCarCommand = new CreateAppointmentCommand(control, "Wash the car", 2);
            ChangeWaxTheCarCommand = new ChangeAppointmentCommand(control, "Wax the car", 3);
            CreateWaxTheCarCommand = new CreateAppointmentCommand(control, "Wax the car", 3);
            ChangeCheckTransmissionFluidCommand = new ChangeAppointmentCommand(control, "Check transmission fluid", 4);
            CreateCheckTransmissionFluidCommand = new CreateAppointmentCommand(control, "Check transmission fluid", 4);
            ChangeInspectByMechanicCommand = new ChangeAppointmentCommand(control, "Inspect by mechanic", 5);
            CreateInspectByMechanicCommand = new CreateAppointmentCommand(control, "Inspect by mechanic", 5);
        }

        public ChangeAppointmentCommand ChangeCheckEngineOilCommand { get; private set; }
        public CreateAppointmentCommand CreateCheckEngineOilCommand { get; private set; }
        public ChangeAppointmentCommand ChangeWashTheCarCommand { get; private set; }
        public CreateAppointmentCommand CreateWashTheCarCommand { get; private set; }
        public ChangeAppointmentCommand ChangeWaxTheCarCommand { get; private set; }
        public CreateAppointmentCommand CreateWaxTheCarCommand { get; private set; }
        public ChangeAppointmentCommand ChangeCheckTransmissionFluidCommand { get; private set; }
        public CreateAppointmentCommand CreateCheckTransmissionFluidCommand { get; private set; }
        public ChangeAppointmentCommand ChangeInspectByMechanicCommand { get; private set; }
        public CreateAppointmentCommand CreateInspectByMechanicCommand { get; private set; }
    }
}
