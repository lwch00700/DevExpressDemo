using System;
using DevExpress.Xpf.DemoBase;
using System.IO;
using System.Reflection;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Diagram;
using DevExpress.Diagram.Core;
using DevExpress.Internal;
using System.Collections.Generic;
using DevExpress.Mvvm.Native;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace DiagramDemo {
    public class DiagramDemoModule : DemoModule {
        protected virtual bool NeedChangeEditorsTheme { get { return false; } }

        public static Stream GetDataStream(string fileName) {
            string path = DiagramDemoHelper.GetDataFilePath(fileName);
            return File.OpenRead(path);
        }
        protected override void RaiseBeforeModuleAppear() {
            base.RaiseBeforeModuleAppear();
            RegisterCustomToolboxItems();
        }
        protected override void RaiseBeforeModuleDisappear() {
            base.RaiseBeforeModuleDisappear();
            UnregisterCustomToolboxItems();
        }
        protected IEnumerable<ItemTool> GetResourcesTools() {
            return Resources.Yield().Concat(Resources.MergedDictionaries).SelectMany(x => x.Values.OfType<ItemTool>()).OrderBy(x => !x.IsQuick);
        }
        protected virtual void RegisterCustomToolboxItems() {
        }
        protected virtual void UnregisterCustomToolboxItems() {
        }
    }
    public static class DiagramControlDemoExtensions {
        public static void LoadDemoData(this DiagramControl diagramControl, string dataSourceName) {
            diagramControl.LoadDocument(DiagramDemoHelper.GetDataFilePath(dataSourceName));
        }
    }
    public static class DiagramDemoHelper {
        public static string GetDataFilePath(string relativePath) {
            return DataDirectoryHelper.GetFile(relativePath, DataDirectoryHelper.DataFolderName);
        }
        public static Point GetCircleDiagramItemPosition(double radius, double phase, Point diagramCenter, Size itemSize) {
            return GetCartesianPointByPolarPoint(radius, phase).OffsetPoint(diagramCenter).GetCenteredRect(itemSize).Location;
        }
        public static Point GetCartesianPointByPolarPoint(double magnitude, double phase) {
            double x = magnitude * Math.Cos(phase);
            double y = magnitude * Math.Sin(phase);
            return new Point(x, y);
        }
        public static void LayoutCircleDiagramItems(DiagramItem[] items, Size pageSize, double radius) {
            double phase = -Math.PI / 2;
            double phaseDelta = 2 * Math.PI / items.Length;
            Point center = new Point(pageSize.Width / 2, pageSize.Height / 2);
            foreach(var item in items) {
                item.Position = DiagramDemoHelper.GetCircleDiagramItemPosition(radius, phase, center, item.DesiredSize);
                phase += phaseDelta;
            }
        }
    }
    public class DiagramContentItemTool : MarkupExtension {
        public string ToolId { get; set; }
        public string ToolName { get; set; }
        public string CustomStyleId { get; set; }
        public Size DefaultSize { get; set; }
        public bool IsQuick { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return new FactoryItemTool(ToolId, () => ToolName, diagram => CreateItem(), DefaultSize, IsQuick);
        }
        protected virtual DiagramItem CreateItem() {
            return new DiagramContentItem { CustomStyleId = CustomStyleId };
        }
    }
}
