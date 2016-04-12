using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.Xpf.Reports.UserDesigner;
using DevExpress.Xpf.Reports.UserDesigner.Extensions;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.IO;
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
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Editors;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DevExpress.Mvvm;

namespace GridDemo {
    public partial class ReportsSideBarControl : UserControl {
        public static readonly DependencyProperty ServiceProperty;

        static ReportsSideBarControl() {
            Type ownerclass = typeof(GridReportManagerService);
            ServiceProperty = DependencyProperty.Register("Service", typeof(GridReportManagerService), ownerclass, new PropertyMetadata(null));
        }

        public GridReportManagerService Service {
            get { return (GridReportManagerService)GetValue(ServiceProperty); }
            set { SetValue(ServiceProperty, value); }
        }

        public ReportsSideBarControl(string moduleName, IGridViewFactory<ColumnWrapper, RowBaseWrapper> reportWrapper) {
            InitializeComponent();
            DataContext = this;
            CreateService(moduleName, reportWrapper);
        }

        void CreateService(string moduleName, IGridViewFactory<ColumnWrapper, RowBaseWrapper> reportWrapper) {
            Service = new GridReportManagerService() { Name = moduleName };
            Interaction.GetBehaviors(reportWrapper as DependencyObject).Add(Service);
        }
    }

    public class ListBoxEditReportBehavior : Behavior<ListBoxEdit> {
        public static readonly DependencyProperty ReportsProperty;
        static ListBoxEditReportBehavior() {
            Type ownerclass = typeof(ListBoxEditReportBehavior);
            ReportsProperty = DependencyProperty.Register("Reports", typeof(ObservableCollection<ReportInfoViewModel>), ownerclass, new PropertyMetadata(null, (d, e) => ((ListBoxEditReportBehavior)d).OnReportsChanged(e.OldValue, e.NewValue)));
        }

        void OnReportsChanged(object oldValue, object newValue) {
            ObservableCollection<ReportInfoViewModel> oldReports = oldValue as ObservableCollection<ReportInfoViewModel>;
            if(oldValue != null) oldReports.CollectionChanged -= Reports_CollectionChanged;
            ObservableCollection<ReportInfoViewModel> newReports = newValue as ObservableCollection<ReportInfoViewModel>;
            if(oldValue != null) newReports.CollectionChanged += Reports_CollectionChanged;
        }

        public ObservableCollection<ReportInfoViewModel> Reports {
            get { return (ObservableCollection<ReportInfoViewModel> )GetValue(ReportsProperty); }
            set { SetValue(ReportsProperty, value); }
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            if(Reports != null)
                Reports.CollectionChanged -= Reports_CollectionChanged;
        }

        void Reports_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if(e.OldItems.Count == 0 && AssociatedObject != null)
                AssociatedObject.SelectedIndex = 0;
        }
    }
}
