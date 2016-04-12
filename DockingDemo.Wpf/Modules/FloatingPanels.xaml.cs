using System;

namespace DockingDemo {
    public partial class FloatingPanels : DockingDemoModule {
        public FloatingPanels() {
            InitializeComponent();
            DesktopContainer.Loaded += OnDesktopContainerLoaded;
        }
        protected override void RaiseModuleAppear() {
            base.RaiseModuleAppear();
            InvalidateVisual();
        }
        void OnDesktopContainerLoaded(object sender, System.Windows.RoutedEventArgs e) {
            DesktopContainer.Loaded -= OnDesktopContainerLoaded;
            ShowWindowModeContainer();
        }
        bool isWindow;
        void OnFloatingModeButtonlick(object sender, System.Windows.RoutedEventArgs e) {
            if(isWindow) ShowDesktopModeContainer();
            else ShowWindowModeContainer();
        }
        void ShowWindowModeContainer() {
            ModeSwitchButton.Content = "Show in Desktop Mode";
            WindowBarManager.Visibility = System.Windows.Visibility.Visible;
            DesktopBarManager.Visibility = System.Windows.Visibility.Collapsed;
            isWindow = true;
        }
        void ShowDesktopModeContainer() {
            ModeSwitchButton.Content = "Show in Window Mode";
            DesktopBarManager.Visibility = System.Windows.Visibility.Visible;
            WindowBarManager.Visibility = System.Windows.Visibility.Collapsed;
            isWindow = false;
        }
        protected override DevExpress.Xpf.Docking.DockLayoutManager Container {
            get { return isWindow ? WindowContainer : DesktopContainer; }
        }
    }
}
