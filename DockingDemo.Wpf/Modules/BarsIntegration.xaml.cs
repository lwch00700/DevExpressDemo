using System;

namespace DockingDemo {
    public partial class BarsIntegration : DockingDemoModule {
        public BarsIntegration() {
            InitializeComponent();
        }
        protected override void RaiseModuleAppear() {
            base.RaiseModuleAppear();
            InvalidateVisual();
        }
    }
}
