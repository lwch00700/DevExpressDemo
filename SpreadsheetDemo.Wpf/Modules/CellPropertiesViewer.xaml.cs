using DevExpress.Mvvm;
using DevExpress.Spreadsheet;
using System;
using System.ComponentModel;
using CellValue = DevExpress.Spreadsheet.CellValue;
namespace SpreadsheetDemo {
    public partial class CellPropertiesViewer : SpreadsheetDemoModule {
        public CellPropertiesViewer() {
            InitializeComponent();
            DataContext = new RangeAdapter();
            this.spreadsheetControl1.Options.Culture = DefaultCulture;
            bool loaded = this.spreadsheetControl1.Document.LoadDocument(DemoUtils.GetRelativePath("CellPropertiesViewer_template.xlsx"));
            System.Diagnostics.Debug.Assert(loaded);
        }

        void CustomExpand(object sender, DevExpress.Xpf.PropertyGrid.CustomExpandEventArgs args) {
            args.IsExpanded = true;
        }
    }
    public class RangeAdapter : BindableBase {
        Range range;

        public RangeAdapter() {
        }
        public Range Range {
            get { return range; }
            set {
                range = value;
                RaiseChanges();
            }
        }

        private void RaiseChanges() {
            RaisePropertyChanged(() => Range);
            RaisePropertyChanged(() => Cell);
            RaisePropertyChanged(() => Value);
            RaisePropertyChanged(() => ArrayFormula);
            RaisePropertyChanged(() => Formula);
            RaisePropertyChanged(() => Alignment);
            RaisePropertyChanged(() => Borders);
            RaisePropertyChanged(() => Fill);
            RaisePropertyChanged(() => Font);
            RaisePropertyChanged(() => Style);
            RaisePropertyChanged(() => NumberFormat);
            RaisePropertyChanged(() => Width);
            RaisePropertyChanged(() => Height);
            RaisePropertyChanged(() => WidthInCharacters);
            RaisePropertyChanged(() => Protection);
            RaisePropertyChanged(() => Locked);
        }
        public Cell Cell {
            get {
                if (Range == null) return null;
                return Range.Worksheet.Cells[Range.TopRowIndex, Range.LeftColumnIndex];
            }
        }
        [Category("CellContent")]
        public CellValue Value {
            get {
                if (Range == null) return null;
                return Range.Value;
            }
        }
        [Category("CellContent")]
        public string ArrayFormula {
            get {
                if (Cell == null) return null;
                return Cell.ArrayFormula;
            }
            set {
                if (Cell == null) return;
                Cell.ArrayFormula = value;
            }
        }
        [Category("CellContent")]
        public string Formula {
            get {
                if (Cell == null) return null;
                return Cell.Formula;
            }
            set {
                if (Cell == null) return;
                Cell.Formula = value;
            }
        }

        [Category("Formatting")]
        public Alignment Alignment {
            get {
                if (Range == null) return null;
                return Range.Alignment;
            }
        }
        [Category("Formatting")]
        public Borders Borders {
            get {
                if (Range == null) return null;
                return Range.Borders;
            }
        }
        [Category("Formatting")]
        public Fill Fill {
            get {
                if (Range == null) return null;
                return Range.Fill;
            }
        }
        [Category("Formatting")]
        public DevExpress.Spreadsheet.SpreadsheetFont Font {
            get {
                if (Range == null) return null;
                return Range.Font;
            }
        }
        [Category("Formatting")]
        public Style Style {
            get {
                if (Range == null) return null;
                return Range.Style;
            }
        }
        [Category("Formatting")]
        public string NumberFormat {
            get {
                if (Cell == null) return null;
                return Cell.NumberFormat;
            }
            set {
                if (Cell == null) return;
                Cell.NumberFormat = value;
            }
        }

        [Category("Layout")]
        public double Width {
            get {
                return Range == null ? 0d : Range.ColumnWidth;
            }
            set {
                if (Range == null) return;
                Range.ColumnWidth = value;
            }
        }
        [Category("Layout")]
        public double Height {
            get {
                return Range == null ? 0d : Range.RowHeight;
            }
            set {
                if (Range == null) return;
                Range.RowHeight = value;
            }
        }
        [Category("Layout")]
        public double WidthInCharacters {
            get {
                return Range == null ? 0d : Range.ColumnWidthInCharacters;
            }
            set {
                if (Range == null) return;
                Range.ColumnWidthInCharacters = value;
            }
        }

        [Category("Protection")]
        [Browsable(false)]
        public Protection Protection {
            get {
                if (Range == null) return null;
                return Range.Protection;
            }
        }
        [Category("Protection")]
        public Boolean Locked {
            get {
                return Protection == null ? false : Protection.Locked;
            }
            set {
                if (Protection == null) return;
                Protection.Locked = value;
            }
        }
    }
}
