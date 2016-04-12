using DevExpress.Xpf.DemoBase.DemoTesting;
using System.Threading;
using System.Windows.Threading;
using DevExpress.Xpf.Core.Native;
using System;
using DevExpress.Xpf.NavBar;
using System.Collections;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Editors;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Core;
using System.Linq;

namespace NavBarDemo.Tests {
    public class NavBarCheckAllDemosFixture : CheckAllDemosFixture {
        Type[] skipMemoryLeaksCheckModules = new Type[] { };
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return !skipMemoryLeaksCheckModules.Contains(moduleType);
        }
        protected override void CreateSwitchAllThemesActions() {
            base.CreateSwitchAllThemesActions();
        }
    }
    #region NavBarDemoModulesAccessor
    public class NavBarDemoModulesAccessor : DemoModulesAccessor<NavBarDemoModule> {
        public NavBarDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
        public NavBarControl NavBarControl { get { return DemoModule.navBarControl; } }
        public NavBarViewBase View { get { return NavBarControl.View; } }
        public ExplorerBarView ExplorerBarView { get { return (ExplorerBarView)View; } }
        public NavigationPaneView NavigationPaneView { get { return (NavigationPaneView)View; } }
        public SideBarView SideBarView { get { return (SideBarView)View; } }
        public GroupsAndItems GroupsAndItemsModule { get { return (GroupsAndItems)DemoModule; } }
    }
    #endregion
    public abstract class BaseNavBarTestingFixture : BaseDemoTestingFixture {
        readonly NavBarDemoModulesAccessor modulesAccessor;
        public BaseNavBarTestingFixture() {
            modulesAccessor = new NavBarDemoModulesAccessor(this);
        }
        public NavBarControl NavBarControl { get { return modulesAccessor.NavBarControl; } }
        public NavBarViewBase View { get { return modulesAccessor.View; } }
        public ExplorerBarView ExplorerBarView { get { return modulesAccessor.ExplorerBarView; } }
        public NavigationPaneView NavigationPaneView { get { return modulesAccessor.NavigationPaneView; } }
        public SideBarView SideBarView { get { return modulesAccessor.SideBarView; } }
        public GroupsAndItems GroupsAndItemsModule { get { return (GroupsAndItems)DemoBaseTesting.CurrentDemoModule; } }
        public Events Events { get { return (Events)DemoBaseTesting.CurrentDemoModule; } }
    }
    public class CheckDemoOptionsFixture : BaseNavBarTestingFixture {
        protected override void CreateActions() {
            base.CreateActions();
            AddSimpleAction(CreateCheckDemosActions);
        }
        void CreateCheckDemosActions() {
            CreateCheckTableViewDemoActions();
            CreateCheckLayoutCustomizationDemoActions();
            CreateEventsDemoActions();
            CreateSetCurrentDemoActions(null, false);
        }
        #region GroupsAndItems
        void CreateCheckTableViewDemoActions() {
            AddLoadModuleActions(typeof(GroupsAndItems));
            AddSimpleAction(delegate() {
                Assert.AreEqual(0, GroupsAndItemsModule.viewsList.SelectedIndex);
                AssertGroupsAndItemsModuleView(typeof(ExplorerBarView));

                GroupsAndItemsModule.viewsList.SelectedIndex = 1;
                UpdateLayoutAndDoEvents();
                AssertGroupsAndItemsModuleView(typeof(NavigationPaneView));

                GroupsAndItemsModule.viewsList.SelectedIndex = 2;
                UpdateLayoutAndDoEvents();
                AssertGroupsAndItemsModuleView(typeof(SideBarView));
            });
        }
        void AssertGroupsAndItemsModuleView(Type viewType) {
            ArrayList images = ((ArrayList)GroupsAndItemsModule.FindResource("Images"));
            Assert.AreEqual(viewType, View.GetType());
            Assert.IsTrue(GroupsAndItemsModule.addGroupButton.IsEnabled);
            Assert.IsTrue(GroupsAndItemsModule.deleteLastGroupButton.IsEnabled);
            Assert.IsTrue(GroupsAndItemsModule.addNewItemButton.IsEnabled);
            Assert.IsFalse(GroupsAndItemsModule.deleteSelectedItemButton.IsEnabled);

            Assert.AreEqual(0, GroupsAndItemsModule.newGroupImageStyleList.SelectedIndex);
            Assert.AreEqual(0, GroupsAndItemsModule.newItemImageList.SelectedIndex);

            Assert.AreEqual(2, NavBarControl.Groups.Count);
            Assert.AreEqual("Group 1", NavBarControl.Groups[0].Header);
            Assert.AreEqual(1, NavBarControl.Groups[0].Items.Count);
            Assert.AreEqual("Item 1", ((NavBarItem)NavBarControl.Groups[0].Items[0]).Content);
            Assert.AreEqual("Group 2", NavBarControl.Groups[1].Header);
            Assert.AreEqual(1, NavBarControl.Groups[1].Items.Count);
            Assert.AreEqual("Item 1", ((NavBarItem)NavBarControl.Groups[1].Items[0]).Content);

            UIAutomationActions.ClickButton(GroupsAndItemsModule.addGroupButton);
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(3, NavBarControl.Groups.Count);
            Assert.AreEqual("Group 3", NavBarControl.Groups[2].Header);
            Assert.AreEqual(0, NavBarControl.Groups[2].Items.Count);
            Assert.AreEqual(((Image)images[0]).Source, NavBarControl.Groups[2].ImageSource);
            Assert.AreEqual(16d, NavBarControl.Groups[2].ItemImageSettings.Height);
            Assert.AreEqual(16d, NavBarControl.Groups[2].ItemImageSettings.Width);

            GroupsAndItemsModule.newItemImageList.SelectedIndex = 1;
            GroupsAndItemsModule.newGroupImageStyleList.SelectedIndex = 1;
            UpdateLayoutAndDoEvents();
            UIAutomationActions.ClickButton(GroupsAndItemsModule.addGroupButton);
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(4, NavBarControl.Groups.Count);
            Assert.AreEqual("Group 4", NavBarControl.Groups[3].Header);
            Assert.AreEqual(0, NavBarControl.Groups[3].Items.Count);
            Assert.AreEqual(((Image)images[1]).Source, NavBarControl.Groups[3].ImageSource);
            Assert.AreEqual(32d, NavBarControl.Groups[3].ItemImageSettings.Height);
            Assert.AreEqual(32d, NavBarControl.Groups[3].ItemImageSettings.Width);
            Assert.AreEqual(System.Windows.HorizontalAlignment.Center, NavBarControl.Groups[3].ItemLayoutSettings.ImageHorizontalAlignment);
            Assert.AreEqual(System.Windows.HorizontalAlignment.Center, NavBarControl.Groups[3].ItemLayoutSettings.TextHorizontalAlignment);
            Assert.AreEqual(Dock.Top, NavBarControl.Groups[3].ItemLayoutSettings.ImageDocking);

            Assert.IsTrue(GroupsAndItemsModule.addGroupButton.IsEnabled);
            Assert.IsTrue(GroupsAndItemsModule.deleteLastGroupButton.IsEnabled);
            Assert.IsTrue(GroupsAndItemsModule.addNewItemButton.IsEnabled);
            Assert.IsFalse(GroupsAndItemsModule.deleteSelectedItemButton.IsEnabled);

            NavBarControl.ActiveGroup = NavBarControl.Groups[2];
            UpdateLayoutAndDoEvents();
            UIAutomationActions.ClickButton(GroupsAndItemsModule.addNewItemButton);
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(1, NavBarControl.Groups[2].Items.Count);
            Assert.AreEqual("Item 1", ((NavBarItem)NavBarControl.Groups[2].Items[0]).Content);
            Assert.AreEqual(((Image)images[1]).Source, ((NavBarItem)NavBarControl.Groups[2].Items[0]).ImageSource);

            GroupsAndItemsModule.newItemImageList.SelectedIndex = 2;
            UpdateLayoutAndDoEvents();
            UIAutomationActions.ClickButton(GroupsAndItemsModule.addNewItemButton);
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(2, NavBarControl.Groups[2].Items.Count);
            Assert.AreEqual("Item 2", ((NavBarItem)NavBarControl.Groups[2].Items[1]).Content);
            Assert.AreEqual(((Image)images[2]).Source, ((NavBarItem)NavBarControl.Groups[2].Items[1]).ImageSource);

            Assert.IsTrue(GroupsAndItemsModule.addGroupButton.IsEnabled);
            Assert.IsTrue(GroupsAndItemsModule.deleteLastGroupButton.IsEnabled);
            Assert.IsTrue(GroupsAndItemsModule.addNewItemButton.IsEnabled);
            Assert.IsFalse(GroupsAndItemsModule.deleteSelectedItemButton.IsEnabled);

            NavBarControl.ActiveGroup.SelectedItem = (NavBarItem)NavBarControl.Groups[2].Items[1];
            UpdateLayoutAndDoEvents();
            Assert.IsTrue(GroupsAndItemsModule.addGroupButton.IsEnabled);
            Assert.IsTrue(GroupsAndItemsModule.deleteLastGroupButton.IsEnabled);
            Assert.IsTrue(GroupsAndItemsModule.addNewItemButton.IsEnabled);
            Assert.IsTrue(GroupsAndItemsModule.deleteSelectedItemButton.IsEnabled);

            UIAutomationActions.ClickButton(GroupsAndItemsModule.deleteSelectedItemButton);
            UpdateLayoutAndDoEvents();
            Assert.IsFalse(GroupsAndItemsModule.deleteSelectedItemButton.IsEnabled);
            Assert.AreEqual(1, NavBarControl.Groups[2].Items.Count);
            Assert.AreEqual("Item 1", ((NavBarItem)NavBarControl.Groups[2].Items[0]).Content);

            NavBarControl.ActiveGroup.SelectedItem = (NavBarItem)NavBarControl.Groups[2].Items[0];
            UpdateLayoutAndDoEvents();
            Assert.IsTrue(GroupsAndItemsModule.deleteSelectedItemButton.IsEnabled);
            UIAutomationActions.ClickButton(GroupsAndItemsModule.deleteSelectedItemButton);
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(0, NavBarControl.Groups[2].Items.Count);

            Assert.IsTrue(GroupsAndItemsModule.deleteLastGroupButton.IsEnabled);
            Assert.IsFalse(GroupsAndItemsModule.deleteSelectedItemButton.IsEnabled);
            UIAutomationActions.ClickButton(GroupsAndItemsModule.deleteLastGroupButton);
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(3, NavBarControl.Groups.Count);
            Assert.AreEqual("Group 3", NavBarControl.Groups[2].Header);

            Assert.IsTrue(GroupsAndItemsModule.deleteLastGroupButton.IsEnabled);
            Assert.IsFalse(GroupsAndItemsModule.deleteSelectedItemButton.IsEnabled);
            UIAutomationActions.ClickButton(GroupsAndItemsModule.deleteLastGroupButton);
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(2, NavBarControl.Groups.Count);
            Assert.AreEqual("Group 2", NavBarControl.Groups[1].Header);

            Assert.IsTrue(GroupsAndItemsModule.deleteLastGroupButton.IsEnabled);
            Assert.IsFalse(GroupsAndItemsModule.deleteSelectedItemButton.IsEnabled);
            UIAutomationActions.ClickButton(GroupsAndItemsModule.deleteLastGroupButton);
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(1, NavBarControl.Groups.Count);
            Assert.AreEqual("Group 1", NavBarControl.Groups[0].Header);

            Assert.IsFalse(GroupsAndItemsModule.deleteLastGroupButton.IsEnabled);
            Assert.IsFalse(GroupsAndItemsModule.deleteSelectedItemButton.IsEnabled);
            NavBarControl.Groups.RemoveAt(0);

            GroupsAndItemsModule.newGroupImageStyleList.SelectedIndex = 0;
            GroupsAndItemsModule.newItemImageList.SelectedIndex = 0;
            UpdateLayoutAndDoEvents();
            UIAutomationActions.ClickButton(GroupsAndItemsModule.addGroupButton);
            UpdateLayoutAndDoEvents();
            NavBarControl.ActiveGroup = NavBarControl.Groups[0];
            UpdateLayoutAndDoEvents();
            UIAutomationActions.ClickButton(GroupsAndItemsModule.addNewItemButton);
            UpdateLayoutAndDoEvents();
            UIAutomationActions.ClickButton(GroupsAndItemsModule.addGroupButton);
            UpdateLayoutAndDoEvents();
            NavBarControl.ActiveGroup = NavBarControl.Groups[1];
            UpdateLayoutAndDoEvents();
            UIAutomationActions.ClickButton(GroupsAndItemsModule.addNewItemButton);
            UpdateLayoutAndDoEvents();
        }
        #endregion
        void CreateCheckLayoutCustomizationDemoActions() {
            AddLoadModuleActions(typeof(LayoutCustomization));
        }
        void CreateEventsDemoActions() {
            AddLoadModuleActions(typeof(Events));
            AddSimpleAction(ClearEventCheckBoxes);

            AddCheckCommonEventsActions();
            AddCheckExplorerBarGroupExpandedEventsActions();

            AddSimpleAction(delegate() {
                Events.viewsList.SelectedIndex = 1;
                UpdateLayoutAndDoEvents();
            });
            AddCheckCommonEventsActions();
            AddCheckNavigationPaneExpandedEventsActions();

            AddSimpleAction(delegate() {
                Events.viewsList.SelectedIndex = 2;
                UpdateLayoutAndDoEvents();
            });
            AddCheckCommonEventsActions();

            AddSimpleAction(delegate() {
                Events.viewsList.SelectedIndex = 0;
                UpdateLayoutAndDoEvents();
            });
            AddCheckCommonEventsActions();
            AddCheckExplorerBarGroupExpandedEventsActions();
        }

        void AddCheckCommonEventsActions() {
            Storyboard collapsingStoryboard = CreateDXExpanderAnimation(0);
            Storyboard expandingStoryboard = CreateDXExpanderAnimation(1);
            AddSimpleAction(
                delegate() {
                    ToggleCheckEditAndRaiseEvent(Events.mouseDownCheckbox, "MouseDown",
                        new MouseButtonEventArgs(Mouse.PrimaryDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseDownEvent });

                });
            AddSimpleAction(
                delegate() {
                    ToggleCheckEditAndRaiseEvent(Events.mouseMoveCheckbox, "MouseMove",
                        new MouseEventArgs(Mouse.PrimaryDevice, Environment.TickCount) { RoutedEvent = UIElement.MouseMoveEvent });
                });
            AddSimpleAction(
                delegate() {
                    ToggleCheckEditAndRaiseEvent(Events.mouseUpCheckbox, "MouseUp",
                        new MouseButtonEventArgs(Mouse.PrimaryDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseUpEvent });
                });

            AddSimpleAction(
                delegate() {
                    ToggleCheckEditAndRaiseEvent(Events.clickCheckbox, "Click",
                        new MouseEventArgs(Mouse.PrimaryDevice, Environment.TickCount) { RoutedEvent = ButtonBase.ClickEvent });
                });
            AddSimpleAction(
                delegate() {
                    ToggleCheckEditAndPerformAction(Events.selectionChangingCheckbox, "ItemSelecting", SelectFirstItem, SelectSecondItem);
                });
            AddSimpleAction(
                delegate() {
                    ToggleCheckEditAndPerformAction(Events.selectionChangedCheckbox, "ItemSelected", SelectFirstItem, SelectSecondItem);
                });
            AddWaitUntilAnimationInProgress();
            AddSimpleAction(
                delegate() {
                    NavBarAnimationOptions.SetCollapseStoryboard(this.NavBarControl.View, collapsingStoryboard);
                    NavBarAnimationOptions.SetExpandStoryboard(this.NavBarControl.View, expandingStoryboard);
                    ToggleCheckEditAndPerformAction(Events.activeGroupChangingCheckbox, "ActiveGroupChanging", SelectSecondGroup, SelectFirstGroup);
                });
            AddWaitUntilAnimationInProgress();
            AddSimpleAction(
                delegate() {
                    NavBarAnimationOptions.SetCollapseStoryboard(this.NavBarControl.View, collapsingStoryboard);
                    NavBarAnimationOptions.SetExpandStoryboard(this.NavBarControl.View, expandingStoryboard);
                    ToggleCheckEditAndPerformAction(Events.activeGroupChangedCheckbox, "ActiveGroupChanged", SelectSecondGroup, SelectFirstGroup);
                });
            AddWaitUntilAnimationInProgress();
        }

        void SelectSecondGroup() {
            Events.navBar.ActiveGroup = Events.navBar.Groups[1];
        }
        void SelectFirstGroup() {
            Events.navBar.ActiveGroup = Events.navBar.Groups[0];
        }

        void SelectFirstItem() {
            Events.navBar.ActiveGroup.SelectedItemIndex = 0;
        }
        void SelectSecondItem() {
            Events.navBar.ActiveGroup.SelectedItemIndex = 2;
        }

        void AddWaitUntilAnimationInProgress() {
            AddConditionAction(WaitUntilAnimationInProgressCondition, null);
        }
        bool WaitUntilAnimationInProgressCondition() {
            foreach(NavBarGroup group in Events.navBar.Groups)
                if(group.IsExpanding || group.IsCollapsing)
                    return false;
            return true;
        }
        void AddCheckExplorerBarGroupExpandedEventsActions() {
            AddSimpleAction(
                delegate() {
                    ToggleCheckEditAndPerformAction(Events.groupExpandedChangingCheckbox, "GroupExpandedChanging",
                        delegate() {
                            Events.navBar.ActiveGroup.IsExpanded = !Events.navBar.ActiveGroup.IsExpanded;
                        }
                    );
                });
            AddSimpleAction(
                delegate() {
                    ToggleCheckEditAndPerformAction(Events.groupExpandedChangedCheckbox, "GroupExpandedChanged",
                        delegate() {
                            Events.navBar.ActiveGroup.IsExpanded = !Events.navBar.ActiveGroup.IsExpanded;
                        }
                    );
                });
        }
        Storyboard CreateDXExpanderAnimation(double toValue) {
            Storyboard stb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation(toValue, new Duration());
            Storyboard.SetTargetName(da, "PART_DXExpander");
            Storyboard.SetTargetProperty(da, new PropertyPath(DXExpander.AnimationProgressProperty));
            return stb;
        }
        void AddCheckNavigationPaneExpandedEventsActions() {
            AddSimpleAction(
                delegate() {
                    ToggleCheckEditAndPerformAction(Events.navPaneExpandedChangingCheckbox, "NavPaneExpandedChanging",
                        delegate() {
                            ((NavigationPaneView)Events.navBar.View).IsExpanded = !((NavigationPaneView)Events.navBar.View).IsExpanded;
                        }
                    );
                });
            AddSimpleAction(
                delegate() {
                    ToggleCheckEditAndPerformAction(Events.navPaneExpandedChangedCheckbox, "NavPaneExpandedChanged",
                        delegate() {
                            ((NavigationPaneView)Events.navBar.View).IsExpanded = !((NavigationPaneView)Events.navBar.View).IsExpanded;
                        }
                    );
                });
        }

        void ClearEventCheckBoxes() {
            Events.mouseDownCheckbox.IsChecked = false;
            Events.mouseMoveCheckbox.IsChecked = false;
            Events.mouseUpCheckbox.IsChecked = false;
            Events.clickCheckbox.IsChecked = false;
            Events.selectionChangingCheckbox.IsChecked = false;
            Events.selectionChangedCheckbox.IsChecked = false;
            Events.activeGroupChangingCheckbox.IsChecked = false;
            Events.activeGroupChangedCheckbox.IsChecked = false;
            Events.groupExpandedChangingCheckbox.IsChecked = false;
            Events.groupExpandedChangedCheckbox.IsChecked = false;
            Events.navPaneExpandedChangingCheckbox.IsChecked = false;
            Events.navPaneExpandedChangedCheckbox.IsChecked = false;
        }
        void ToggleCheckEditAndPerformAction(CheckEdit checkEdit, string message, Action action) {
            ToggleCheckEditAndPerformAction(checkEdit, message, action, delegate() { });
        }
        void ToggleCheckEditAndRaiseEvent(CheckEdit checkEdit, string message, RoutedEventArgs eventArgs) {
            FrameworkElement item = HelperActions.FindElementByType<NavBarItemControl>(Events.navBar);
            ToggleCheckEditAndPerformAction(checkEdit, message, delegate() { item.RaiseEvent(eventArgs); });
        }
        void ToggleCheckEditAndPerformAction(CheckEdit checkEdit, string message, Action action, Action rollBack) {
            action();
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(string.Empty, Events.textBox.Text);
            rollBack();
            UpdateLayoutAndDoEvents();

            checkEdit.IsChecked = true;
            action();
            UpdateLayoutAndDoEvents();
            Assert.IsTrue(Events.textBox.Text.Contains(message));
            rollBack();

            UIAutomationActions.ClickButton(Events.clearButton);
            checkEdit.IsChecked = false;
            UpdateLayoutAndDoEvents();
        }
    }
}
