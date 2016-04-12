using System.Data.Common;
using System.IO;
using DevExpress.Internal;
using DevExpress.Mvvm;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
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

namespace SchedulerDemo {
    public partial class EntityBoundMode : SchedulerDemoModule {
        public EntityBoundMode() {
            InitializeComponent();
        }
    }

    #region Data model
    public class DoctorSchedule {
        [System.ComponentModel.DataAnnotations.Key]
        public Int64 Id { get; set; }
        public bool AllDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string PatientName { get; set; }
        public string Note { get; set; }
        public int PaymentStutusId { get; set; }
        public int IssueId { get; set; }
        public int EventType { get; set; }
        public string Location { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }
        public Int64 DoctorId { get; set; }
    }
    public class Doctor {
        [System.ComponentModel.DataAnnotations.Key]
        public Int64 Id { get; set; }
        public string Name { get; set; }
    }

    public class DoctorScheduleContext : DbContext {
        public DoctorScheduleContext() : base(CreateConnection(), true) { }
        public DoctorScheduleContext(string connectionString) : base(connectionString) { }
        public DoctorScheduleContext(DbConnection connection) : base(connection, true) { }

        static DoctorScheduleContext() {
            Database.SetInitializer<DoctorScheduleContext>(null);
        }

        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }

        static string filePath;
        static DbConnection CreateConnection() {
            if(filePath == null)
                filePath = DataDirectoryHelper.GetFile("doctors.db", DataDirectoryHelper.DataFolderName);
            File.SetAttributes(filePath, File.GetAttributes(filePath) & ~FileAttributes.ReadOnly);
            var connection = DbProviderFactories.GetFactory("System.Data.SQLite.EF6").CreateConnection();
            connection.ConnectionString = new SQLiteConnectionStringBuilder { DataSource = filePath }.ConnectionString;
            return connection;
        }
    }
    #endregion

    public class EntityBoundModeViewModel : ViewModelBase {
        public static DateTime BaseDate = DateTime.Today.AddDays(-1);
        public static string[] IssueList = { "Consultation", "Treatment", "X-Ray" };
        public static Color[] IssueColorList = { Colors.SteelBlue, Colors.Pink, Colors.SeaShell };
        public static string[] PatientsName = { "Devid Mitchell", "Mark Oliver", "Addison Davis", "Benjamin Hughes", "Lucas Smith" };
        public static string[] DoctorsName = { "Isabella Carter", "Miguel Simmons", "Madeline Russell", "Ariana Alexander" };
        public static string[] PaymentStatuses = { "Paid", "Unpaid" };
        public static Color[] PaymentColorStatuses = { Colors.Green, Colors.Red };

        DoctorScheduleContext dataContext;
        Random rnd = new Random();

        public EntityBoundModeViewModel() {
            this.dataContext = new DoctorScheduleContext();
            this.dataContext.Doctors.Load();
            this.dataContext.DoctorSchedules.Load();
            bool shouldCreateDoctors = this.dataContext.Doctors.Count() <= 0;
            if (shouldCreateDoctors)
                CreateDoctors();
            bool shouldCreateDoctorSchedules = this.dataContext.DoctorSchedules.Count() <= 0;
            if (shouldCreateDoctorSchedules)
                CreateDoctorsSchedule();
            if (shouldCreateDoctorSchedules || shouldCreateDoctors)
                this.dataContext.SaveChanges();

            InitializeLabelsAndStutuses();
        }

        public IEnumerable Doctors { get { return this.dataContext.Doctors.Local; } }
        public IEnumerable DoctorSchedules { get { return this.dataContext.DoctorSchedules.Local; } }
        public AppointmentLabelCollection Labels { get; set; }
        public AppointmentStatusCollection Statuses { get; set; }

        void CreateDoctors() {
            int doctorCount = DoctorsName.Length;
            var doctors = this.dataContext.Doctors;
            for (int i = 0; i < doctorCount; i++) {
                Doctor doctor = dataContext.Doctors.Create();
                doctor.Name = DoctorsName[i];
                doctors.Add(doctor);
            }
        }
        void CreateDoctorsSchedule() {
            Random rnd = new Random();
            int scheduleCount = dataContext.DoctorSchedules.Count();
            if (scheduleCount > 0)
                return;
            int doctorCount = DoctorsName.Length;
            for (int doctorId = 1; doctorId <= doctorCount; doctorId++)
                CreateDoctorSchedule(doctorId);
        }
        void CreateDoctorSchedule(int doctorId) {
            for (DateTime start = BaseDate; start < BaseDate.AddDays(7); start += TimeSpan.FromDays(1)) {
                CreateDoctorSchedule(doctorId, start.AddHours(this.rnd.Next(9, 12)));
                CreateDoctorSchedule(doctorId, start.AddHours(this.rnd.Next(14, 16)));
                CreateDoctorSchedule(doctorId, start.AddHours(this.rnd.Next(18, 20)));
            }
        }
        void CreateDoctorSchedule(int doctorId, DateTime start) {
            DoctorSchedule doctorSchedule = dataContext.DoctorSchedules.Create();
            doctorSchedule.Start = start;
            doctorSchedule.End = start.AddHours(this.rnd.Next(1, 3));
            doctorSchedule.IssueId = this.rnd.Next(0, 3);
            doctorSchedule.PaymentStutusId = this.rnd.Next(0, 2);
            int patintsNameCount = PatientsName.Length;
            doctorSchedule.PatientName = PatientsName[this.rnd.Next(0, patintsNameCount - 1)];
            doctorSchedule.DoctorId = doctorId;
            this.dataContext.DoctorSchedules.Add(doctorSchedule);
        }
        void InitializeLabelsAndStutuses() {
            Labels = new AppointmentLabelCollection();
            int count = IssueList.Length;
            for (int i = 0; i < count; i++) {
                AppointmentLabel label = Labels.CreateNewLabel(i, IssueList[i]);
                label.SetColor(IssueColorList[i]);
                Labels.Add(label);
            }

            Statuses = new AppointmentStatusCollection();
            count = PaymentStatuses.Length;
            for (int i = 0; i < count; i++) {
                IAppointmentStatus status = Statuses.CreateNewStatus(PaymentStatuses[i], string.Empty);
                status.Type = AppointmentStatusType.Custom;
                status.SetBrush(new SolidColorBrush(PaymentColorStatuses[i]));
                Statuses.Add(status);
            }
        }
    }
}
