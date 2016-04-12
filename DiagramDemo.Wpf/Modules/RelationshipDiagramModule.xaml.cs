using DevExpress.Diagram.Core;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Diagram;
using DiagramDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace DiagramDemo {
    [CodeFile("ViewModels/RelationshipDiagramViewModel.cs")]
    [CodeFile("Resources/RelationshipDiagramModuleResources.xaml")]
    public partial class RelationshipDiagramModule : DiagramDemoModule {
        const double DefaultStrokeThickness = 2;
        const double SelectedStrokeThickness = 4;
        readonly RelationshipDiagramViewModel ViewModel;

        public RelationshipDiagramModule() {
            ViewModel = new RelationshipDiagramViewModel(EmployeesData.Employees.Take(9).ToArray());
            DataContext = ViewModel;
            InitializeComponent();
            diagramControl.Loaded += DiagramControl_Loaded;
        }

        void DiagramControl_Loaded(object sender, RoutedEventArgs e) {
            diagramControl.Loaded -= DiagramControl_Loaded;
            CreateRelationshipDiagram();
        }
        void CreateRelationshipDiagram() {
            Dictionary<Employee, DiagramItem> employeeToDiagramItemCache = new Dictionary<Employee, DiagramItem>();
            var diagramItems = ViewModel.Employees.Select(employee => {
                var item = new DiagramContentItem() { CustomStyleId = "employeeStyle", Content = employee, FontSize = 12, CanDelete = false, CanCopy = false };
                employeeToDiagramItemCache[employee] = item;
                return item;
            }).ToArray();
            var diagramConnectors = ViewModel.Relationships.
                Select(employeeRelation => CreateDiagramConnector(employeeToDiagramItemCache[employeeRelation.Item1], employeeToDiagramItemCache[employeeRelation.Item2], employeeRelation.Item3)).ToArray();
            Array.ForEach<DiagramItem>(diagramConnectors, item => diagramControl.Items.Add(item));
            Array.ForEach<DiagramItem>(diagramItems, item => diagramControl.Items.Add(item));
            diagramControl.UpdateLayout();
            DiagramDemoHelper.LayoutCircleDiagramItems(diagramItems, diagramControl.PageSize, 300);
            diagramControl.SelectItem(diagramItems.First());
        }
        static DiagramConnector CreateDiagramConnector(DiagramItem beginItem, DiagramItem endItem, EmployeeRelationship employeeRelation) {
            DiagramConnector connector = new DiagramConnector();
            connector.BeginItem = beginItem;
            connector.EndItem = endItem;
            connector.BeginArrow = connector.EndArrow = null;
            connector.Type = ConnectorType.Straight;
            connector.StrokeThickness = DefaultStrokeThickness;
            connector.CanDelete = connector.CanCopy = false;
            switch(employeeRelation) {
                case EmployeeRelationship.KnowEachOther:
                    connector.StrokeDashArray = new DoubleCollection(new double[] { 4, 8 });
                    connector.ThemeStyleId = DiagramConnectorStyleId.Subtle2;
                    break;
                case EmployeeRelationship.Friends:
                    connector.ThemeStyleId = DiagramConnectorStyleId.Subtle3;
                    break;
            }
            return connector;
        }
        void diagramControl_SelectionChanged(object sender, EventArgs e) {
            foreach(var diagramConnector in diagramControl.Items.OfType<DiagramConnector>()) {
                if(diagramControl.SelectedItems.Contains(diagramConnector.BeginItem) || diagramControl.SelectedItems.Contains(diagramConnector.EndItem))
                    diagramConnector.StrokeThickness = SelectedStrokeThickness;
                else
                    diagramConnector.StrokeThickness = DefaultStrokeThickness;
            }
            ViewModel.SelectedEmployee = (diagramControl.PrimarySelection as DiagramContentItem).With(item => item.Content) as Employee;
        }
    }
}
