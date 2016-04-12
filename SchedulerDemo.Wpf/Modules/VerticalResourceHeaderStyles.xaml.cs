using System;
using System.Windows;
using DevExpress.Xpf.Scheduler;

namespace SchedulerDemo {
    public partial class VerticalResourceHeaderStyles : SchedulerDemoModule {
        public VerticalResourceHeaderStyles() {
            InitializeComponent();
            InitializeScheduler();
            ApplyVerticalResourceHeaderStyle();
        }
        private void ApplyVerticalResourceHeaderStyle() {
            Style verticalResourceHeaderStyle = (Style)this.FindResource("VerticalResourceHeaderStyle");
            foreach (SchedulerViewBase view in Views)
                view.VerticalResourceHeaderStyle = verticalResourceHeaderStyle;
        }
        private void ClearVerticalResourceHeaderStyle() {
            foreach (SchedulerViewBase view in Views)
                view.ClearValue(SchedulerViewBase.VerticalResourceHeaderStyleProperty);
        }
        private void cheVerticalResourceHeader_Checked(object sender, RoutedEventArgs e) {
            ApplyVerticalResourceHeaderStyle();
        }
        private void cheVerticalResourceHeader_Unchecked(object sender, RoutedEventArgs e) {
            ClearVerticalResourceHeaderStyle();
        }
    }
}
