using DevExpress.Xpf.DemoBase.DemoTesting;
using System.Threading;
using System.Windows.Threading;
using DevExpress.Xpf.Core.Native;
using System;
using DevExpress.Xpf.Carousel;
using System.Collections;
using DevExpress.Xpf.Carousel.Helpers;
using DevExpress.Xpf.Core;
using System.Windows.Controls;
using DevExpress.Xpf.Editors;

namespace CarouselDemo.Tests {
    #region CarouselDemoModulesAccessor
    public class CarouselDemoModulesAccessor : DemoModulesAccessor<CarouselDemoModule> {
        public CarouselDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
        public MovingPath MovingPathModule { get { return (MovingPath)DemoModule; } }
    }
    #endregion
    public class CarouselCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool CheckMemoryLeaks(Type moduleTyle) {
            return true;
        }
    }
    public abstract class BaseCarouselTestingFixture : BaseDemoTestingFixture {
        protected readonly CarouselDemoModulesAccessor modulesAccessor;
        public BaseCarouselTestingFixture() {
            modulesAccessor = new CarouselDemoModulesAccessor(this);
        }
    }
    public class CheckDemoOptionsFixture : BaseCarouselTestingFixture {
        protected override void CreateActions() {
            base.CreateActions();
            AddSimpleAction(CheckModule_MovingPath);
        }
        #region MovingPath module checking
        void CheckModule_MovingPath() {
            AddLoadModuleActions(typeof(MovingPath));
            AddSimpleAction(CheckDefaultProperties);
            AddSimpleAction(CheckPathList);
            AddSimpleAction(B152058_CheckPathPadding);
            AddSimpleAction(CheckDrawPath);
            AddSimpleAction(CheckStretchPath);
        }
        void CheckDefaultProperties() {
            Assert.AreEqual(false, modulesAccessor.MovingPathModule.carousel.PathVisible);
            Assert.AreEqual(PathSizingMode.Stretch, modulesAccessor.MovingPathModule.carousel.PathSizingMode);
        }
        void CheckPathList() {
            int index = 0;
            foreach(var path in (IEnumerable)modulesAccessor.MovingPathModule.pathList.ItemsSource) {
                PathList_SetIndexAndAssert(index, index == 0);
                index++;
            }
        }
        void PathList_SetIndexAndAssert(int index, bool equals) {
            object path = modulesAccessor.MovingPathModule.carousel.GetValue(CarouselPanel.ItemMovingPathProperty);
            modulesAccessor.MovingPathModule.pathList.SelectedIndex = index;
            object newPath = modulesAccessor.MovingPathModule.carousel.GetValue(CarouselPanel.ItemMovingPathProperty);
            Assert.AreEqual(equals, object.ReferenceEquals(path, newPath));
        }
        void B152058_CheckPathPadding(){
            ChangePathPaddingAndAssert(modulesAccessor.MovingPathModule.paddingTopSlider);
            ChangePathPaddingAndAssert(modulesAccessor.MovingPathModule.paddingLeftSlider);
            ChangePathPaddingAndAssert(modulesAccessor.MovingPathModule.paddingRightSlider);
            ChangePathPaddingAndAssert(modulesAccessor.MovingPathModule.paddingBottomSlider);
        }
        void ChangePathPaddingAndAssert(TrackBarEdit slider) {
            int renderCount = CarouselPanelHelper.GetRenderCount(modulesAccessor.MovingPathModule.carousel);
            object pathPadding = modulesAccessor.MovingPathModule.carousel.GetValue(CarouselPanel.PathPaddingProperty);
            slider.Value += 5;
            UpdateLayoutAndDoEvents();
            object newPathPadding = modulesAccessor.MovingPathModule.carousel.GetValue(CarouselPanel.PathPaddingProperty);
            int newRenderCount = CarouselPanelHelper.GetRenderCount(modulesAccessor.MovingPathModule.carousel);
            Assert.IsFalse(object.ReferenceEquals(pathPadding, newPathPadding));
            Assert.AreEqual(1, newRenderCount - renderCount);
        }
        void CheckDrawPath() {
            modulesAccessor.MovingPathModule.checkBoxPathVisible.IsChecked = true;
            Assert.IsTrue(modulesAccessor.MovingPathModule.carousel.PathVisible);
        }
        void CheckStretchPath() {
            modulesAccessor.MovingPathModule.checkBoxPathSizingMode.IsChecked = false;
            Assert.AreEqual(PathSizingMode.Proportional, modulesAccessor.MovingPathModule.carousel.PathSizingMode);
        }
        #endregion
    }
}
