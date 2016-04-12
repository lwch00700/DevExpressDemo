using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Editors;

namespace DockingDemo {
    public partial class PanelAnimation : DockingDemoModule {
        #region static
        public static readonly DependencyProperty TileLayoutProperty;
        static TimeSpan Duration = new TimeSpan(0, 0, 0, 0, 500);
        static GridLength ZeroLength = new GridLength(0, GridUnitType.Star);
        static PanelAnimation() {
            TileLayoutProperty = DependencyProperty.Register("TileLayout", typeof(TileLayout), typeof(PanelAnimation),
                new PropertyMetadata(TileLayout.Default, OnTileLayoutChanged));
        }
        static void OnTileLayoutChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e) {
            ((PanelAnimation)dObj).OnTileLayoutChanged((TileLayout)e.NewValue);
        }
        #endregion static
        bool isExpanded;
        IDictionary<BaseLayoutItem, GridLength> widths;
        IDictionary<BaseLayoutItem, GridLength> heights;
        Timer DataTimer;
        int animationLock = 0;
        List<PanelContentControl> panelContentList = new List<PanelContentControl>();
        string[] indicies = new string[] { "DJA", "IIX", "NASD", "NIKKEY", "NYA", "RUA", "SPX", "SOXX", "XAX" };
        public PanelAnimation() {
            InitializeComponent();
            widths = new Dictionary<BaseLayoutItem, GridLength>();
            heights = new Dictionary<BaseLayoutItem, GridLength>();
            dockManager.Loaded += ManagerLoaded;
            dockManager.Unloaded += ManagerUnloaded;
        }
        protected override void RaiseAfterModuleDisappear() {
            base.RaiseAfterModuleDisappear();
            if(DataTimer != null) {
                DataTimer.Dispose();
                DataTimer = null;
            }
        }
        public TileLayout TileLayout {
            get { return (TileLayout)GetValue(TileLayoutProperty); }
            set { SetValue(TileLayoutProperty, value); }
        }
        void ManagerUnloaded(object sender, RoutedEventArgs e) {
            if(DataTimer != null)
                DataTimer.Stop();
        }
        void ManagerLoaded(object sender, RoutedEventArgs e) {
            Binding tileLayoutBinding = new Binding("TileLayout");
            tileLayoutBinding.Source = this;
            tileLayoutBinding.Converter = new TileLayoutContainerConverter();
            tileLayoutListBox.SetBinding(ListBoxEdit.SelectedItemProperty, tileLayoutBinding);
            tileLayoutListBox.SelectedIndex = 1;
            SetupTimer();
        }
        void SetupTimer() {
            if(DataTimer == null)
                DataTimer = new Timer();
            DataTimer.Interval = 1000;
            DataTimer.Elapsed += ChangeIndexes;
            DataTimer.Start();
        }
        void OnTileLayoutChanged(TileLayout value) {
            rootGroup.Clear();
            int rowCount = TileLayoutExtension.GetRowCount(value);
            int columnCount = TileLayoutExtension.GetColumnsCount(value);
            BaseLayoutItem[] rows = new BaseLayoutItem[rowCount];
            panelContentList.Clear();
            for(int i = 0; i < rowCount; i++) {
                rows[i] = CreateRow(i, columnCount);
            }
            rootGroup.AddRange(rows);
            dockManager.Update();
        }
        LayoutGroup CreateRow(int row, int itemsCount) {
            LayoutGroup group = new LayoutGroup() { AllowSplitters = false };
            BaseLayoutItem[] items = new BaseLayoutItem[itemsCount];
            for(int i = 0; i < itemsCount; i++) {
                items[i] = CreateItem(row * itemsCount + i);
            }
            group.AddRange(items);
            return group;
        }
        LayoutPanel CreateItem(int index) {
            LayoutPanel panel = new LayoutPanel();
            Random random = new Random(DateTime.Now.Millisecond);
            PanelContentControl panelContentControl = new PanelContentControl()
            {
                IndexName = indicies[index],
                CurrentValue = random.Next(400, 1000),
                CurrentChange = Math.Round(random.NextDouble() - 0.5, 3)
            };
            for(int i = 0; i < 7; i++) {
                panelContentControl.Data.Add(new Point(i, random.Next(0, 300)));
            }
            panelContentControl.MouseLeftButtonDown += OnPanelContentControlMouseLeftButtonDown;
            panelContentControl.BackButtonClicked += new EventHandler(panelContentControlBackButtonClicked);
            panelContentList.Add(panelContentControl);
            panel.Content = panelContentControl;
            return panel;
        }
        void DoExpand(BaseLayoutItem clickItem) {
            if(clickItem != null) {
                tileLayoutListBox.IsEnabled = isExpanded;
                var itemsToAnimate = GetItemsToAnimate(clickItem);
                foreach(BaseLayoutItem item in itemsToAnimate) {
                    if(item != clickItem && item != clickItem.Parent)
                        Animate(item, isExpanded);
                }
                isExpanded = !isExpanded;
            }
        }
        IEnumerable<BaseLayoutItem> GetItemsToAnimate(BaseLayoutItem target) {
            LayoutGroup animationGroup = target.Parent;
            return from item in dockManager.GetItems()
                   where (item != target && item != animationGroup) &&
                        (object.Equals(item.Parent, animationGroup) || object.Equals(item.Parent, animationGroup.Parent))
                   select item;
        }
        void Animate(BaseLayoutItem layoutItem, bool expanded) {
            GridLength w = layoutItem.ItemWidth;
            GridLength h = layoutItem.ItemHeight;
            if(!expanded) {
                widths[layoutItem] = w;
                heights[layoutItem] = h;
            }
            else {
                w = widths[layoutItem];
                h = heights[layoutItem];
            }
            animationLock += 2;
            layoutItem.BeginWidthAnimation(expanded ? ZeroLength : w, expanded ? w : ZeroLength, Duration, AnimationCompleted);
            layoutItem.BeginHeightAnimation(expanded ? ZeroLength : h, expanded ? h : ZeroLength, Duration, AnimationCompleted);
        }
        void AnimationCompleted(object sender, EventArgs e) {
            animationLock--;
        }
        void ChangeIndexes(object sender, ElapsedEventArgs e) {
            Dispatcher.BeginInvoke(new Action(UpdateIndexes),
                System.Windows.Threading.DispatcherPriority.Loaded);
        }
        void UpdateIndexes() {
            Random random = new Random(DateTime.Now.Millisecond);
            foreach(PanelContentControl p in panelContentList) {
                p.CurrentValue += Math.Round(random.NextDouble() - 0.5, 3);
                for(int i = 0; i < 6; i++) {
                    p.Data[i] = new Point(i, p.Data[i + 1].Y);
                }
                p.Data[6] = new Point(6, random.Next(0, 300));
            }
        }
        void panelContentControlBackButtonClicked(object sender, EventArgs e) {
            if(animationLock > 0) return;
            PanelContentControl pcc = (PanelContentControl)sender;
            DoExpand(DockLayoutManager.GetLayoutItem(sender as DependencyObject));
            pcc.IsExpanded = !pcc.IsExpanded;
        }
        void OnPanelContentControlMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if(animationLock > 0) return;
            PanelContentControl pcc = (PanelContentControl)sender;
            if(!pcc.IsExpanded) {
                DoExpand(DockLayoutManager.GetLayoutItem(sender as DependencyObject));
                pcc.IsExpanded = !pcc.IsExpanded;
            }
        }
    }
    public class PanelContentControl : ContentControl {
        #region static
        public static readonly DependencyProperty IndexNameProperty;
        public static readonly DependencyProperty CurrentChangeProperty;
        public static readonly DependencyProperty CurrentValueProperty;
        public static readonly DependencyProperty IsChangePositiveProperty;
        public static readonly DependencyProperty IsExpandedProperty;
        public static readonly DependencyProperty DataProperty;
        static PanelContentControl() {
            IndexNameProperty = DependencyProperty.Register("IndexName", typeof(string), typeof(PanelContentControl));
            CurrentChangeProperty = DependencyProperty.Register("CurrentChange", typeof(double), typeof(PanelContentControl),
                new PropertyMetadata(0.0, OnCurrentChangeChanged));
            CurrentValueProperty = DependencyProperty.Register("CurrentValue", typeof(double), typeof(PanelContentControl),
                new PropertyMetadata(0.0, OnCurrentValueChanged));
            IsChangePositiveProperty = DependencyProperty.Register("IsChangePositive", typeof(bool?), typeof(PanelContentControl),
                new PropertyMetadata(null, OnIsChangePositiveChanged));
            IsExpandedProperty = DependencyProperty.Register("IsExpanded", typeof(bool), typeof(PanelContentControl),
                new PropertyMetadata(false, OnIsExpandedChanged));
            DataProperty = DependencyProperty.Register("Data", typeof(ObservableCollection<Point>), typeof(PanelContentControl));
        }
        private static void OnCurrentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            PanelContentControl panelContentControl = (PanelContentControl)d;
            panelContentControl.CurrentChange = Math.Round((double)e.NewValue - (double)e.OldValue, 3);
        }
        private static void OnCurrentChangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            PanelContentControl panelContentControl = (PanelContentControl)d;
            panelContentControl.IsChangePositive = (double)(e.NewValue) == 0 ? null : (bool?)((double)(e.NewValue) > 0);
        }
        private static void OnIsChangePositiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((PanelContentControl)d).UpdateVisualState();
        }
        private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((PanelContentControl)d).UpdateVisualState();
        }
        #endregion
        public PanelContentControl() {
            DefaultStyleKey = typeof(PanelContentControl);
            Background = Brushes.Transparent;
            Data = new ObservableCollection<Point>();
            Unloaded += new RoutedEventHandler(PanelContentControl_Unloaded);
            SizeChanged += new SizeChangedEventHandler(PanelContentControl_SizeChanged);
            Cursor = Cursors.Hand;
        }
        void PanelContentControl_Unloaded(object sender, RoutedEventArgs e) {
            if(PartBackButton != null) {
                PartBackButton.Click -= PartBackButton_Click;
            }
        }
        void PanelContentControl_SizeChanged(object sender, SizeChangedEventArgs e) {
            UpdateVisualState();
        }
        public event EventHandler BackButtonClicked;
        protected void RaiseBackButtonClicked() {
            if(BackButtonClicked != null)
                BackButtonClicked(this, EventArgs.Empty);
        }
        Button PartBackButton { get; set; }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            UpdateVisualState();
            PartBackButton = GetTemplateChild("PART_BackButton") as Button;
            if(PartBackButton != null) {
                PartBackButton.Click += new RoutedEventHandler(PartBackButton_Click);
            }
        }
        void PartBackButton_Click(object sender, RoutedEventArgs e) {
            RaiseBackButtonClicked();
        }
        protected override void OnMouseEnter(MouseEventArgs e) {
            base.OnMouseEnter(e);
            UpdateVisualState();
        }
        protected override void OnMouseLeave(MouseEventArgs e) {
            base.OnMouseEnter(e);
            UpdateVisualState();
        }
        private void UpdateVisualState() {
            if(IsMouseOver)
                VisualStateManager.GoToState(this, "MouseOver", true);
            else
                VisualStateManager.GoToState(this, "Normal", true);
            if(IsChangePositive == null)
                VisualStateManager.GoToState(this, "Zero", false);
            if(IsChangePositive == true)
                VisualStateManager.GoToState(this, "Positive", false);
            if(IsChangePositive == false)
                VisualStateManager.GoToState(this, "Negative", false);
            if(IsExpanded) {
                Cursor = Cursors.Arrow;
                VisualStateManager.GoToState(this, "Checked", false);
            }
            else {
                Cursor = Cursors.Hand;
                VisualStateManager.GoToState(this, "Unchecked", false);
            }
            if(RenderSize.Width < 165)
                VisualStateManager.GoToState(this, "CompactView", false);
            else
                VisualStateManager.GoToState(this, "AdvancedView", false);
        }
        public string IndexName {
            get { return (string)GetValue(IndexNameProperty); }
            set { SetValue(IndexNameProperty, value); }
        }
        public double CurrentChange {
            get { return (double)GetValue(CurrentChangeProperty); }
            set { SetValue(CurrentChangeProperty, value); }
        }
        public double CurrentValue {
            get { return (double)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }
        public bool? IsChangePositive {
            get { return (bool?)GetValue(IsChangePositiveProperty); }
            set { SetValue(IsChangePositiveProperty, value); }
        }
        public bool IsExpanded {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }
        public ObservableCollection<Point> Data {
            get { return (ObservableCollection<Point>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
    }
    public class SimpleChartControl : ItemsControl {
        public SimpleChartControl() {
            DefaultStyleKey = typeof(SimpleChartControl);
        }
        protected override DependencyObject GetContainerForItemOverride() {
            return new SimpleChartItem();
        }
        protected override bool IsItemItsOwnContainerOverride(object item) {
            return item is SimpleChartItem;
        }
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            base.PrepareContainerForItemOverride(element, item);
            SimpleChartItem simpleChartItem = element as SimpleChartItem;
            if(simpleChartItem != null) {
                Point p = (Point)item;
                double left = p.X == 0 ? 45 : 90;
                simpleChartItem.Margin = new Thickness(left, p.Y, 0, 0);
            }
        }
    }
    public class SimpleChartItem : Control {
        public SimpleChartItem() {
            DefaultStyleKey = typeof(SimpleChartItem);
        }
    }
}
