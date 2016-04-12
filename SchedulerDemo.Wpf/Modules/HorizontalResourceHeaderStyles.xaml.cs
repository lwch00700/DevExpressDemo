using System;
using System.Windows;
using DevExpress.Xpf.Scheduler;

namespace SchedulerDemo {
    public partial class HorizontalResourceHeaderStyles : SchedulerDemoModule {
        public HorizontalResourceHeaderStyles() {
            InitializeComponent();
            InitializeScheduler();

            ApplyHorizontalResourceHeaderStyle();
        }

        private void ApplyHorizontalResourceHeaderStyle() {
            Style style = (Style)this.FindResource("HorizontalResourceHeaderStyle");
            foreach (SchedulerViewBase view in Views) {
                view.HorizontalResourceHeaderStyle = style;
            }
        }
        private void ClearHorizontalResourceHeaderStyle() {
            foreach (SchedulerViewBase view in Views) {
                view.ClearValue(SchedulerViewBase.HorizontalResourceHeaderStyleProperty);
            }
        }
        private void cheHorizontalResourceHeader_Checked(object sender, RoutedEventArgs e) {
            ApplyHorizontalResourceHeaderStyle();
        }
        private void cheHorizontalResourceHeader_Unchecked(object sender, RoutedEventArgs e) {
            ClearHorizontalResourceHeaderStyle();
        }
    }
}
