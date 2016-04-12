using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.PivotGrid;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class Serialization : PivotGridDemoModule {
        MemoryStream currentLayoutStream;

        public Serialization() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().CustomerReports.ToList();
        }

        private void LoadSampleButton_Click(object sender, RoutedEventArgs e) {
            RestoreLayout(((LayoutSampleBase)layoutSamplesComboBox.SelectedItem).GetStream());
            pivotGrid.BestFit(FieldArea.RowArea, true, false);
            pivotGrid.BestFitColumn(pivotGrid.ColumnCount - 1);
        }

        private void SaveLayoutButton_Click(object sender, RoutedEventArgs e) {
            currentLayoutStream = SaveLayout();
            restoreLayoutButton.IsEnabled = true;
        }

        private void RestoreLayoutButton_Click(object sender, RoutedEventArgs e) {
            RestoreLayout(currentLayoutStream);
        }


        private void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            List<LayoutSampleBase> layoutSamples = new List<LayoutSampleBase>();
            layoutSamples.Add(new MemoryStreamLayoutSample("Original", SaveLayout()));
            Assembly assembly = typeof(Serialization).Assembly;
            layoutSamples.Add(new ResourceLayoutSample("Brief view", DemoHelper.GetPath("PivotGridDemo.Data.LayoutSamples.", assembly) + "BriefView.xml"));
            layoutSamples.Add(new ResourceLayoutSample("Full view", DemoHelper.GetPath("PivotGridDemo.Data.LayoutSamples.", assembly) + "FullView.xml"));
            layoutSamplesComboBox.ItemsSource = layoutSamples;
            layoutSamplesComboBox.SelectedIndex = 0;
        }

        MemoryStream SaveLayout() {
            MemoryStream stream = new MemoryStream();
            pivotGrid.SaveLayoutToStream(stream);
            return stream;
        }
        void RestoreLayout(Stream stream) {
            if(stream == null)
                return;
            stream.Position = 0;
            pivotGrid.RestoreLayoutFromStream(stream);
        }

        public abstract class LayoutSampleBase {
            readonly string description;
            public LayoutSampleBase(string description) {
                this.description = description;
            }
            public abstract Stream GetStream();
            public override string ToString() {
                return description;
            }
        }
        public class ResourceLayoutSample : LayoutSampleBase {
            readonly string resourcePath;
            public ResourceLayoutSample(string description, string resourcePath)
                : base(description) {
                this.resourcePath = resourcePath;
            }
            public override Stream GetStream() {
                return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            }
        }
        public class MemoryStreamLayoutSample : LayoutSampleBase {
            readonly MemoryStream stream;
            public MemoryStreamLayoutSample(string description, MemoryStream stream)
                : base(description) {
                this.stream = stream;
            }
            public override Stream GetStream() {
                return stream;
            }
        }


    }
}
