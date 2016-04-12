using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Scheduler.Drawing;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Native;
using System.Windows.Markup;
using System.IO;
using DevExpress.Xpf.Core.Native;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;



namespace SchedulerDemo {
    public partial class MVVMAppointmentForm : SchedulerDemoModule {
        public MVVMAppointmentForm() {
            InitializeComponent();
        }
    }

    public class AppointmentCustomizationViewModel : ViewModelBase {
        public AppointmentCustomizationViewModel() {
            FillEmployees();
            FillTasks();
        }

        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<HospitalAppointment> Appointments { get; set; }

        void FillEmployees() {
            Doctors = new ObservableCollection<Doctor>();
            Doctors.Add(new Doctor() { Id = 1, Name = "Stomatologist" });
            Doctors.Add(new Doctor() { Id = 2, Name = "Ophthalmologist" });
            Doctors.Add(new Doctor() { Id = 3, Name = "Surgeon" });
        }

        void FillTasks() {
            Random rand = new Random(DateTime.Now.Millisecond);

            Appointments = new ObservableCollection<HospitalAppointment>();
            Appointments.Add(new HospitalAppointment() { StartDate = DateTime.Now.Date.AddHours(10), EndDate = DateTime.Now.Date.AddHours(11), DoctorId = 1, Notes = "", Location = "101", PatientName = "Dave Murrel", InsuranceNumber = GenerateNineNumbers(rand), FirstVisit = true });
            Appointments.Add(new HospitalAppointment() { StartDate = DateTime.Now.Date.AddDays(2).AddHours(15), EndDate = DateTime.Now.Date.AddDays(2).AddHours(16), DoctorId = 1, Notes = "", Location = "101", PatientName = "Mike Roller", InsuranceNumber = GenerateNineNumbers(rand), FirstVisit = true });

            Appointments.Add(new HospitalAppointment() { StartDate = DateTime.Now.Date.AddDays(1).AddHours(11), EndDate = DateTime.Now.Date.AddDays(1).AddHours(12), DoctorId = 2, Notes = "", Location = "103", PatientName = "Bert Parkins", InsuranceNumber = GenerateNineNumbers(rand), FirstVisit = true });
            Appointments.Add(new HospitalAppointment() { StartDate = DateTime.Now.Date.AddDays(2).AddHours(10), EndDate = DateTime.Now.Date.AddDays(2).AddHours(12), DoctorId = 2, Notes = "", Location = "103", PatientName = "Carl Lucas", InsuranceNumber = GenerateNineNumbers(rand), FirstVisit = false });

            Appointments.Add(new HospitalAppointment() { StartDate = DateTime.Now.Date.AddHours(12), EndDate = DateTime.Now.Date.AddHours(13), DoctorId = 3, Notes = "Analysis results are required", Location = "104", PatientName = "Brad Barnes", InsuranceNumber = GenerateNineNumbers(rand), FirstVisit = false });
            Appointments.Add(new HospitalAppointment() { StartDate = DateTime.Now.Date.AddDays(1).AddHours(14), EndDate = DateTime.Now.Date.AddDays(1).AddHours(15), DoctorId = 3, Notes = "", Location = "104", PatientName = "Richard Fisher", InsuranceNumber = GenerateNineNumbers(rand), FirstVisit = true });
        }

        string GenerateNineNumbers(Random rand) {
            string result = "";
            for(int i = 0; i < 9; i++) {
                result += rand.Next(9).ToString();
            }
            return result;
        }
    }

    public class HospitalAppointment {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PatientName { get; set; }
        public string Notes { get; set; }
        public string Location { get; set; }
        public Int64 DoctorId { get; set; }
        public string InsuranceNumber { get; set; }
        public bool FirstVisit { get; set; }
        public string Recurrence { get; set; }
        public int Type { get; set; }
    }
}
