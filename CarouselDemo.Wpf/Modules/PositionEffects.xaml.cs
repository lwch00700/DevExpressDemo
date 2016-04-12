using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using DevExpress.Xpf.DemoBase;
using System.Windows.Controls;
using System.Windows.Data;
using System;
using DevExpress.Xpf.Carousel;
using System.Windows.Markup;
using System.Collections;
using System.Windows.Threading;
using DevExpress.Xpf.Editors;

namespace CarouselDemo {
    public partial class PositionEffects : CarouselDemoModule {
        public PositionEffects() {
            InitializeComponent();
            AddItems(@"/CarouselDemo;component/Data/Images/Logos", ItemType.BinaryImage, carousel);
        }
        bool fixedF = false;
        protected override Size ArrangeOverride(Size arrangeBounds) {
            Size result = base.ArrangeOverride(arrangeBounds);
            if(!fixedF) {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new PrimeDelegate(UpdateBindings));
                fixedF = !fixedF;
            }
            return result;
        }
        protected delegate void PrimeDelegate();
        protected void UpdateBindings() {
            UpdateBinding(scaleListBox);
            UpdateBinding(angleListBox);
            UpdateBinding(opacityListBox);
        }
        protected void UpdateBinding(ListBoxEdit cb) {
            cb.SelectedIndex = 1;
        }
    }
}
