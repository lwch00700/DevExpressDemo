using System;
using System.Windows;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Layout.Core;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Bars;

namespace DockingDemo.Tests {
    #region DockingDemoModulesAccessor
    public class DockingDemoModulesAccessor : DemoModulesAccessor<DockingDemoModule> {
        public DockingDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
        DockLayoutManager manager;
        public DockLayoutManager DockLayoutManager {
            get {
                if(manager == null)
                    manager = LayoutHelper.FindElement(DemoModule, Criteria) as DockLayoutManager;
                return manager;
            }
        }
        bool Criteria(FrameworkElement element) {
            return element is DevExpress.Xpf.Docking.DockLayoutManager;
        }
        public void ResetDockLayoutManager() {
            manager = null;
        }
        public IDEDockLayout IDEComplexLayoutModule { get { return DemoModule as IDEDockLayout; } }
        public PanelGroups RowColumnLayoutModule { get { return DemoModule as PanelGroups; } }
        public FloatingPanels FloatPanelsModule { get { return DemoModule as FloatingPanels; } }
        public Serialization SerializationModule { get { return DemoModule as Serialization; } }
        public DocumentGroups DocumentGroupsModule { get { return DemoModule as DocumentGroups; } }
        public IDEWorkspaces IDEWorkspacesModule { get { return DemoModule as IDEWorkspaces; } }
    }
    #endregion DockingDemoModulesAccessor
    public abstract class BaseDockingDemoTestingFixture : BaseDemoTestingFixture {
        readonly DockingDemoModulesAccessor modulesAccessor;
        public BaseDockingDemoTestingFixture() {
            modulesAccessor = new DockingDemoModulesAccessor(this);
        }
        public DockLayoutManager DockLayoutManager { get { return modulesAccessor.DockLayoutManager; } }
        protected void ResetDockLayoutManager() {
            modulesAccessor.ResetDockLayoutManager();
        }
        public IDEDockLayout IDEComplexLayoutModule { get { return modulesAccessor.IDEComplexLayoutModule; } }
        public PanelGroups RowColumnLayoutModule { get { return modulesAccessor.RowColumnLayoutModule; } }
        public Serialization SerializationModule { get { return modulesAccessor.SerializationModule; } }
        public FloatingPanels FloatPanelsModule { get { return modulesAccessor.FloatPanelsModule; } }
        public DocumentGroups DocumentGroupsModule { get { return modulesAccessor.DocumentGroupsModule; } }
        public IDEWorkspaces IDEWorkspaces { get { return modulesAccessor.IDEWorkspacesModule; } }
    }

    public class DockingCheckAllDemosFixture : CheckAllDemosFixture {
        protected override void CreateSwitchAllThemesActions() {
            base.CreateSwitchAllThemesActions();
        }
    }
    public class CheckDemoOptionsFixture : BaseDockingDemoTestingFixture {
        #region Initialization
        protected override void CreateActions() {
            base.CreateActions();
            AddSimpleAction(CreateCheckDemosActions);
        }
        void CreateCheckDemosActions() {
            CheckIDEWorkspacesModule();
            CheckSerializationModule();
            CheckDocumentGroupsModule();
            CheckRowColumnLayoutModule();
            CreateSetCurrentDemoActions(null, false);
        }
        void CheckRowColumnLayoutModule() {
            AddLoadModuleActions(typeof(PanelGroups));
            AddSimpleAction(RowColumnLayoutTest);
        }
        void CheckSerializationModule() {
            AddLoadModuleActions(typeof(Serialization));
            AddSimpleAction(SerializationTest);
        }
        void CheckDocumentGroupsModule() {
            AddLoadModuleActions(typeof(DocumentGroups));
            AddSimpleAction(DocumentGroupsTest);
        }
        void CheckIDEWorkspacesModule() {
            AddLoadModuleActions(typeof(IDEWorkspaces));
            AddSimpleAction(IDEWorkspacesTest);
        }
        #endregion Initialization
        void IDEWorkspacesTest() {
            DocumentPanel document1 = IDEWorkspaces.DemoDockContainer.GetItem("document1") as DocumentPanel;
            Assert.IsNotNull(document1);
            FloatGroup floatGroup = IDEWorkspaces.DemoDockContainer.DockController.Float(document1);
            UpdateLayoutAndDoEvents();
            IDEWorkspaces.DemoDockContainer.DockController.Hide(floatGroup);
            UpdateLayoutAndDoEvents();
            IDEWorkspaces.DemoDockContainer.DockController.Close(floatGroup);
            UpdateLayoutAndDoEvents();
            WorkspaceManager.GetWorkspaceManager(IDEWorkspaces.barManager).ApplyWorkspace("workspace2");
            UpdateLayoutAndDoEvents();
            Assert.AreEqual("File", ((BarItemLink)IDEWorkspaces.barManager.MainMenu.ItemLinks[0]).ActualContent.ToString());
            return;
        }
        void RowColumnLayoutTest() {
            ResetDockLayoutManager();

            Assert.AreEqual(3, DockLayoutManager.LayoutRoot.Items.Count);
            Assert.IsTrue(DockLayoutManager.LayoutRoot.Orientation == System.Windows.Controls.Orientation.Horizontal);
            Assert.IsTrue(RowColumnLayoutModule.allowSplittersCheck.IsChecked.Value);
            Assert.AreEqual(0, RowColumnLayoutModule.orientationListBox.SelectedIndex);
            Assert.IsTrue(DockLayoutManager.LayoutRoot.IsSplittersEnabled);
            EditorsActions.ToggleCheckEdit(RowColumnLayoutModule.allowSplittersCheck);
            UpdateLayoutAndDoEvents();
            Assert.IsFalse(DockLayoutManager.LayoutRoot.IsSplittersEnabled);

            EditorsActions.ToggleCheckEdit(RowColumnLayoutModule.allowSplittersCheck);
            UpdateLayoutAndDoEvents();
            Assert.IsTrue(DockLayoutManager.LayoutRoot.IsSplittersEnabled);

            RowColumnLayoutModule.orientationListBox.SelectedIndex = 1;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(1, RowColumnLayoutModule.orientationListBox.SelectedIndex);
            Assert.IsTrue(DockLayoutManager.LayoutRoot.Orientation == System.Windows.Controls.Orientation.Vertical);

            RowColumnLayoutModule.orientationListBox.SelectedIndex = 0;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(0, RowColumnLayoutModule.orientationListBox.SelectedIndex);
            Assert.IsTrue(DockLayoutManager.LayoutRoot.Orientation == System.Windows.Controls.Orientation.Horizontal);
        }
        void SerializationTest() {
            ResetDockLayoutManager();

            LayoutGroup root = DockLayoutManager.LayoutRoot;

            Assert.AreEqual(3, root.Items.Count);
            Assert.IsTrue(root.Items[0] is LayoutGroup);
            Assert.IsTrue(root.Items[1] is LayoutPanel);
            Assert.IsTrue(root.Items[2] is LayoutGroup);

            LayoutPanel panel1 = ((LayoutGroup)root.Items[0]).Items[0] as LayoutPanel;
            LayoutPanel panel2 = ((LayoutGroup)root.Items[0]).Items[1] as LayoutPanel;
            LayoutPanel panel3 = ((LayoutGroup)root.Items[2]).Items[0] as LayoutPanel;
            LayoutPanel panel4 = ((LayoutGroup)root.Items[2]).Items[1] as LayoutPanel;

            Assert.IsTrue(SerializationModule.serializeButton.IsEnabled);
            Assert.IsFalse(SerializationModule.deserializeButton.IsEnabled);
            Assert.AreEqual(SerializationModule.layoutSampleName.Items.Count, 4);
            Assert.AreEqual(SerializationModule.layoutSampleName.SelectedIndex, 0);

            SerializationModule.layoutSampleName.SelectedIndex = 1;
            UIAutomationActions.ClickButton(SerializationModule.loadSampleLayoutButton);
            UpdateLayoutAndDoEvents();
            SerializationModule.layoutSampleName.SelectedIndex = 2;
            UIAutomationActions.ClickButton(SerializationModule.loadSampleLayoutButton);
            UpdateLayoutAndDoEvents();
            SerializationModule.layoutSampleName.SelectedIndex = 3;
            UIAutomationActions.ClickButton(SerializationModule.loadSampleLayoutButton);
            UpdateLayoutAndDoEvents();
            SerializationModule.layoutSampleName.SelectedIndex = 0;
            UIAutomationActions.ClickButton(SerializationModule.loadSampleLayoutButton);
            UpdateLayoutAndDoEvents();

            UIAutomationActions.ClickButton(SerializationModule.serializeButton);
            UpdateLayoutAndDoEvents();
            Assert.IsTrue(SerializationModule.deserializeButton.IsEnabled);
            Assert.IsFalse(panel1.IsAutoHidden);
            DockLayoutManager.DockController.Hide(panel1);
            Assert.IsTrue(panel1.IsAutoHidden);
            UIAutomationActions.ClickButton(SerializationModule.deserializeButton);
            UpdateLayoutAndDoEvents();
            Assert.IsFalse(panel1.IsAutoHidden);
        }
        void DocumentGroupsTest() {
            ResetDockLayoutManager();
            Assert.AreEqual(DockLayoutManager.LayoutRoot.Items[0], DocumentGroupsModule.documentContainer);

            DocumentGroup dGroup = DocumentGroupsModule.documentContainer;

            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.Default);
            DocumentGroupsModule.closeButtonListBox.SelectedIndex = 1;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InTabControlHeader);
            DocumentGroupsModule.closeButtonListBox.SelectedIndex = 2;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InAllTabPageHeaders);
            DocumentGroupsModule.closeButtonListBox.SelectedIndex = 3;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InActiveTabPageHeader);
            DocumentGroupsModule.closeButtonListBox.SelectedIndex = 4;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader);
            DocumentGroupsModule.closeButtonListBox.SelectedIndex = 5;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InActiveTabPageAndTabControlHeader);
            DocumentGroupsModule.closeButtonListBox.SelectedIndex = 6;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.NoWhere);
            DocumentGroupsModule.closeButtonListBox.SelectedIndex = 0;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.Default);

            Assert.AreEqual(dGroup.CaptionOrientation, System.Windows.Controls.Orientation.Horizontal);
            DocumentGroupsModule.headerOrientationListBox.SelectedIndex = 1;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.CaptionOrientation, System.Windows.Controls.Orientation.Vertical);
            DocumentGroupsModule.headerOrientationListBox.SelectedIndex = 0;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.CaptionOrientation, System.Windows.Controls.Orientation.Horizontal);

            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Default);
            DocumentGroupsModule.headerLocationListBox.SelectedIndex = 1;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Left);
            DocumentGroupsModule.headerLocationListBox.SelectedIndex = 2;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Top);
            DocumentGroupsModule.headerLocationListBox.SelectedIndex = 3;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Right);
            DocumentGroupsModule.headerLocationListBox.SelectedIndex = 4;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Bottom);
            DocumentGroupsModule.headerLocationListBox.SelectedIndex = 0;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Default);

            Assert.IsFalse(DocumentGroupsModule.headersAutoFill.IsChecked.Value);
            Assert.IsFalse(dGroup.TabHeadersAutoFill);
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.Scroll);
            DocumentGroupsModule.headerLayoutListBox.SelectedIndex = 1;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.Trim);
            DocumentGroupsModule.headerLayoutListBox.SelectedIndex = 2;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.Scroll);
            DocumentGroupsModule.headerLayoutListBox.SelectedIndex = 3;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.MultiLine);
            Assert.IsFalse(DocumentGroupsModule.headersAutoFill.IsChecked.Value);
            Assert.IsFalse(dGroup.TabHeadersAutoFill);

            DocumentGroupsModule.headersAutoFill.IsChecked = true;
            Assert.IsTrue(dGroup.TabHeadersAutoFill);
            UpdateLayoutAndDoEvents();

            DocumentGroupsModule.headerLayoutListBox.SelectedIndex = 0;
            UpdateLayoutAndDoEvents();
            Assert.IsTrue(DocumentGroupsModule.headersAutoFill.IsChecked.Value);
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.Scroll);
            Assert.IsTrue(dGroup.TabHeadersAutoFill);
        }
    }
}
