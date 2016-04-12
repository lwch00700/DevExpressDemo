using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Threading;
using System.Windows.Threading;
using DevExpress.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    [CodeFile("ModuleResources/RealtimeDataSourceViewModel.(cs)")]
    [CodeFile("ModuleResources/RealtimeDataSourceClasses.(cs)")]
    public partial class RealtimeDataSource : GridDemoModule {
        readonly RealtimeDataSourceViewModel viewModel;
        RealTimeSource realTimeSource;
        DispatcherOperation updateGraphOperation;
        public RealtimeDataSource() {
            viewModel = new RealtimeDataSourceViewModel();
            DataContext = viewModel;
            InitializeComponent();
            viewModel.TimerShowTick += model_TimerShowTick;
            PatchInterval();
            realTimeSource = CreateRealTimeSource(viewModel.List);
            grid.ItemsSource = realTimeSource;
            viewModel.WithRealTimeSource = true;
            Unloaded += RealtimeDataSource_Unloaded;
        }

        void RealtimeDataSource_Unloaded(object sender, RoutedEventArgs e) {
            viewModel.Dispose();
        }
        protected override bool IsGridBorderVisible { get { return true; } }

        delegate void UpdateGraphDelegate(SeriesPointCollection points, double value);
        UpdateGraphDelegate updateGraph = (UpdateGraphDelegate)Delegate.CreateDelegate(typeof(UpdateGraphDelegate), typeof(RealtimeDataSource), "UpdateGraph");
        void model_TimerShowTick(object sender, EventArgs e) {
            if(updateGraphOperation != null && updateGraphOperation.Status != DispatcherOperationStatus.Completed) return;
            double changePerSecond = viewModel.ChangePerSecond;
            updateGraphOperation = Dispatcher.BeginInvoke(DispatcherPriority.Send, updateGraph, UPSDiagram.Points, changePerSecond);
        }
        static void UpdateGraph(SeriesPointCollection points, double value) {
            while(points.Count > 20) {
                points.RemoveAt(0);
            }
            points.Add(new SeriesPoint(DateTime.Now.TimeOfDay.TotalSeconds, value));
        }
        static RealTimeSource CreateRealTimeSource(IList dataSource) {
            RealTimeSource rts = new RealTimeSource { DataSource = dataSource };
            return rts;
        }
        void DisposeRealTimeSource() {
            if(realTimeSource != null) {
                realTimeSource.DataSource = null;
                realTimeSource.Dispose();
            }
            realTimeSource = null;
        }
        void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            PatchInterval();
        }
        void PatchInterval() {
            double pow = (this.slider.Maximum - this.slider.Value + 3) / 2.0;
            viewModel.InterEventDelay = (int)Math.Pow(2.0, pow);
        }

        void CheckBox_Unchecked(object sender, RoutedEventArgs e) {
            viewModel.Update();
            DisposeRealTimeSource();
            grid.ItemsSource = null;
            grid.ItemsSource = viewModel.List;
            PatchInterval();
        }
        void CheckBox_Checked(object sender, RoutedEventArgs e) {
            if(grid == null)
                return;
            grid.ItemsSource = null;
            realTimeSource = CreateRealTimeSource(viewModel.List);
            grid.ItemsSource = realTimeSource;
            viewModel.Update();
            PatchInterval();
        }
        protected override DevExpress.Xpf.Grid.DataViewBase GetExportView() {
            return null;
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
