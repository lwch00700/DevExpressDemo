using System.Windows;
using DevExpress.Xpf.Diagram;
using DevExpress.Xpf.Ribbon;
using DevExpress.Diagram.Core;
using System.Windows.Data;

namespace DiagramDemo {
    public partial class MainDemoModule : DiagramDemoModule {
        public MainDemoModule() {
            InitializeComponent();
        }

        void ShowDiagramDesignerControl(object sender, RoutedEventArgs e) {
            var diagramControl = new DiagramDesignerControl();
            diagramControl.DefaultStencils = new StencilCollection { BasicShapes.StencilId, ArrowShapes.StencilId, DecorativeShapes.StencilId };

            var window = CreateWindow();
            window.Content = diagramControl;
            var titleBinding = new Binding(DiagramDesignerControl.TitleProperty.Name) { Source = diagramControl };
            window.SetBinding(DXRibbonWindow.TitleProperty, titleBinding);
            window.ShowDialog();
        }
        DXRibbonWindow CreateWindow() {
            var window = new DXRibbonWindow();
            const double margin = 15;
            window.Width = Application.Current.MainWindow.ActualWidth;
            window.Height = Application.Current.MainWindow.ActualHeight - 2 * margin;
            window.IsAeroMode = false;
            window.WindowState = Application.Current.MainWindow.WindowState;
            Point pos = Application.Current.MainWindow.PointToScreen(new Point());
            window.Left = pos.X;
            window.Top = pos.Y + margin;
            return window;
        }
    }
}
