using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Utils;
using DevExpress.Xpf.DemoBase;

namespace GridDemo {
    [CodeFile("ModuleResources/MultiThreadDataProcessingViewModel.(cs)")]
    [CodeFile("ModuleResources/MultiThreadDataProcessingClasses.(cs)")]
    [CodeFile("ModuleResources/MultiThreadDataProcessingTemplates(.SL).xaml")]
    [CodeFile("Controls/OrderDataGenerator.(cs)")]
    public partial class MultiThreadDataProcessing : GridDemoModule {
        public MultiThreadDataProcessing() {
            InitializeComponent();
            PLinqInstantFeedbackDemoViewModel viewModel = (PLinqInstantFeedbackDemoViewModel)Resources["PLinqInstantFeedbackDemoViewModel"];
            viewModel.SetIsDesignTime(false);
        }
        protected override DevExpress.Xpf.Grid.DataViewBase GetExportView() {
            return null;
        }
    }
}
