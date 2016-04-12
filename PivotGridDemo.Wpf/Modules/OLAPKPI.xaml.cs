using System;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.PivotGrid;
using System.Windows.Threading;

namespace PivotGridDemo.PivotGrid {
    public partial class OLAPKPI : PivotGridDemoModule {

        public OLAPKPI() {
            InitializeComponent();

            cbStatusGraphics.ItemsSource = Enum.GetValues(typeof(PivotKpiGraphic));
            cbStatusGraphics.SelectedIndex = 1;
            cbTrendGraphics.ItemsSource = Enum.GetValues(typeof(PivotKpiGraphic));
            cbTrendGraphics.SelectedIndex = 1;

            InitPivotGrid();
        }

        private void InitPivotGrid() {
            pivotGrid.OlapConnectionString = SampleConnectionString;
        }
        static string sampleFileName;

        string SampleConnectionString {
            get { return "Provider=msolap;Data Source=" + SampleFileName + ";Initial Catalog=Adventure Works;Cube Name=Adventure Works"; }
        }
        protected static string SampleFileName {
            get {
                if(string.IsNullOrEmpty(sampleFileName)) {
                    sampleFileName = Path.GetFullPath(DataFilesHelper.FindFile(GetSampleCubeFileName(), DataFilesHelper.DataPath));
                    if(File.Exists(sampleFileName))
                        File.SetAttributes(sampleFileName, FileAttributes.Normal);
                }
                return sampleFileName;
            }
        }

        static string GetSampleCubeFileName() {
            return "AdventureWorks.cub";
        }

        private void cbStatusGraphics_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            fieldStatus.KpiGraphic = (PivotKpiGraphic)(((ComboBoxEdit)sender).SelectedItem);
        }

        private void cbTrendGraphics_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            fieldTrend.KpiGraphic = (PivotKpiGraphic)(((ComboBoxEdit)sender).SelectedItem);
        }

        void OnPivotGridOlapException(object sender, PivotOlapExceptionEventArgs e) {
            e.Handled = true;
            pivotGrid.Dispatcher.BeginInvoke(
                new Action(delegate() {
                ShowOLAPErrorMessage(e);
            })
               );
        }

        void ShowOLAPErrorMessage(PivotOlapExceptionEventArgs e) {
            errorText.Text = (e.Exception.Message + "\r\n " + ((e.Exception.InnerException != null) ? e.Exception.InnerException.Message : string.Empty));
            VisualStateManager.GoToState(this, "ShowErrorMessage", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += OnTimerTick;
            timer.Start();
            e.Handled = true;
        }

        void OnTimerTick(object sender1, EventArgs e1) {
            DispatcherTimer self = sender1 as DispatcherTimer;
            if(self == null)
                return;
            self.Stop();
            self.Tick -= OnTimerTick;
            VisualStateManager.GoToState(this, "HideErrorMessage", true);
        }
    }
}
