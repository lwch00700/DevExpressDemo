using DevExpress.Diagram.Core;
using DevExpress.Mvvm.Native;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpf.Diagram;
using System.Windows.Media;
using System.Windows;
using DevExpress.Xpf.DemoBase;

namespace DiagramDemo {
    [CodeFile("Resources/OfficePlanResources.xaml")]
    public partial class OfficePlanModule : DiagramDemoModule {
        public OfficePlanModule() {
            InitializeComponent();
            diagramControl.LoadDemoData("OfficePlan.xml");
        }
        public const string OfficeStencilId = "OfficeShapes";
        protected override void RegisterCustomToolboxItems() {
            var dashboardStencil = new DiagramStencil(OfficeStencilId, () => "Office Shapes");
            foreach(var tool in GetResourcesTools())
                dashboardStencil.RegisterTool(tool);
            DiagramToolboxRegistrator.RegisterStencil(dashboardStencil);
        }
        protected override void UnregisterCustomToolboxItems() {
            DiagramToolboxRegistrator.UnregisterStencil(DiagramToolboxRegistrator.GetStencil(OfficeStencilId));
        }
    }
    public class OfficePlanItemTool : DiagramContentItemTool {
        protected override DiagramItem CreateItem() {
            var item = base.CreateItem();
            item.ThemeStyleId = null;
            return item;
        }
    }
}
