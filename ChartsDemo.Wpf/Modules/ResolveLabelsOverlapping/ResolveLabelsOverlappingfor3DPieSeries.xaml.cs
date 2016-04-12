using System.Windows.Media.Media3D;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ResolveLabelsOverlapping/ResolveLabelsOverlappingfor3DPieSeries(.SL).xaml"),
     CodeFile("Modules/ResolveLabelsOverlapping/ResolveLabelsOverlappingfor3DPieSeries.xaml.(cs)")]
    public partial class ResolveLabelsOverlappingfor3DPieSeries : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public ResolveLabelsOverlappingfor3DPieSeries() {
            InitializeComponent();
            simpleDiagram3D.ContentTransform = new MatrixTransform3D(new Matrix3D(-0.719, -0.414, 0.558, 0,
                                                                                   0.693, -0.389, 0.605, 0,
                                                                                  -0.032,  0.822, 0.567, 0,
                                                                                   0.000,  0.000, 0.000, 1));
        }
    }
}
