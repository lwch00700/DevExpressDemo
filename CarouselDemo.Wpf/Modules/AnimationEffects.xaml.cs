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

namespace CarouselDemo {
    public partial class AnimationEffects : CarouselDemoModule {
        public AnimationEffects() {
            InitializeComponent();
            AddItems(@"/CarouselDemo;component/Data/Images/Logos", ItemType.BinaryImage, carousel);
        }
    }
}
