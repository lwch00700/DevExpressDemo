using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public partial class PaletteChooser : UserControl {
        ChartControl chart;

        public PaletteChooser(ChartControl chart) {
            InitializeComponent();
            this.chart = chart;
            int count = 0;
            if (chart != null)
                chart.Palette = PaletteSelectorHelper.ActualPalette;
            foreach (PaletteKind paletteKind in Palette.GetPredefinedKinds()) {
                RowDefinition rowDefenition = new RowDefinition();
                rowDefenition.Height = GridLength.Auto;
                grPalettes.RowDefinitions.Add(rowDefenition);
                PaletteItem paletteItem = new PaletteItem();
                paletteItem.Palette = Activator.CreateInstance(paletteKind.Type) as Palette;
                if (chart != null && chart.Palette.PaletteName == paletteItem.Palette.PaletteName)
                    paletteItem.IsChecked = true;
                paletteItem.Checked += new RoutedEventHandler(paletteItem_Checked);
                paletteItem.GotMouseCapture += new MouseEventHandler(PaletteItem_ReleaseMouseCapture);
                paletteItem.ClickMode = ClickMode.Press;
                Grid.SetRow(paletteItem, count);
                grPalettes.Children.Add(paletteItem);
                count++;
            }
        }
        void PaletteItem_ReleaseMouseCapture(object sender, MouseEventArgs e) {
            PaletteItem paletteItem = sender as PaletteItem;
            paletteItem.ReleaseMouseCapture();
        }
        void paletteItem_Checked(object sender, RoutedEventArgs e) {
            PaletteItem paletteItem = sender as PaletteItem;
            if (chart != null && paletteItem != null) {
                chart.Palette = paletteItem.Palette;
                PaletteSelectorHelper.ActualPalette = paletteItem.Palette;
            }
        }
        public void UpdateChart(ChartControl chart) {
            this.chart = chart;
            chart.Palette = PaletteSelectorHelper.ActualPalette;
        }
    }
}
