using System;
using System.Collections.ObjectModel;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Printing;
using ColumnLayout = DevExpress.XtraPrinting.ColumnLayout;
using DevExpress.DemoData.Models;
using System.Linq;
using System.Collections.Generic;

namespace PrintingDemo {
    public enum PageNumberLocation { TopMargin, BottomMargin }

    public partial class MultiColumnModule : ModuleBase {
        public MultiColumnModule() {
            InitializeComponent();
        }
    }

    public class MultiColumnViewModel : ModuleViewModelBase {
        static readonly ReadOnlyCollection<HorizontalAlignment> pageNumberAlignmentValues = new ReadOnlyCollection<HorizontalAlignment>(
            new HorizontalAlignment[] { HorizontalAlignment.Left, HorizontalAlignment.Center, HorizontalAlignment.Right }
        );

        PageNumberKind pageNumberKind = PageNumberKind.NumberOfTotal;
        string pageNumberFormat;
        PageNumberLocation pageNumberLocation;
        HorizontalAlignment pageNumberAlignment;
        bool isAcrossThenDownChecked = true;
        List<Customer> customers;
        NWindContext context = NWindContext.Create();

        public DataTemplate PageNumberTemplate { get; set; }
        public PageNumberKind PageNumberKind {
            get { return pageNumberKind; }
            set {
                if(pageNumberKind.Equals(value))
                    return;
                pageNumberKind = value;
                PageNumberFormat = GetDefaultFormatString();
                CreateDocument();
                RaisePropertyChanged("PageNumberKind");
            }
        }
        public string PageNumberFormat {
            get { return pageNumberFormat; }
            set {
                if(pageNumberFormat == value)
                    return;
                pageNumberFormat = value;
                CreateDocument();
                RaisePropertyChanged("PageNumberFormat");
            }
        }
        public PageNumberLocation PageNumberLocation {
            get { return pageNumberLocation; }
            set {
                if(pageNumberLocation == value)
                    return;
                pageNumberLocation = value;
                CreateDocument();
                RaisePropertyChanged("PageNumberLocation");
            }
        }
        public HorizontalAlignment PageNumberAlignment {
            get { return pageNumberAlignment; }
            set {
                if(object.Equals(pageNumberAlignment, value))
                    return;
                pageNumberAlignment = value;
                CreateDocument();
                RaisePropertyChanged("PageNumberAlignment");
            }
        }
        public bool IsAcrossThenDownChecked {
            get { return isAcrossThenDownChecked; }
            set {
                if(isAcrossThenDownChecked == value)
                    return;
                isAcrossThenDownChecked = value;
                CreateDocument();
                RaisePropertyChanged("IsAcrossThenDownChecked");
            }
        }
        public static Array PageNumberLocationValues {
            get { return Enum.GetValues(typeof(PageNumberLocation)); }
        }
        public static Array PageNumberKindValues {
            get { return Enum.GetValues(typeof(PageNumberKind)); }
        }
        public static ReadOnlyCollection<HorizontalAlignment> PageNumberAlignmentValues { get { return pageNumberAlignmentValues; } }

        public MultiColumnViewModel() {
            pageNumberFormat = GetDefaultFormatString();
            customers = context.Customers.ToList();
        }

        protected override TemplatedLink CreateLink() {
            SimpleLink link = new SimpleLink();
            link.ColumnWidth = 198;
            link.ReportHeaderTemplate = ReportHeaderTemplate;
            link.DetailTemplate = DetailTemplate;
            link.DetailCount = context.Customers.Count();
            link.DocumentName = "Multi-Column";
            link.CreateDetail += link_CreateDetail;
            return link;
        }
        protected override void ProcessLink(TemplatedLink link) {
            ((SimpleLink)link).ColumnLayout = GetColumnLayout();
            ClearPageHeaderFooter();
            DataTemplate template = PageNumberTemplate;
            switch(PageNumberLocation) {
                case PageNumberLocation.TopMargin:
                    link.TopMarginTemplate = template;
                    link.TopMarginData = GetPageNumberDataContext();
                    break;
                case PageNumberLocation.BottomMargin:
                    link.BottomMarginTemplate = template;
                    link.BottomMarginData = GetPageNumberDataContext();
                    break;
            }
        }
        void link_CreateDetail(object sender, CreateAreaEventArgs e) {
            e.Data = customers[e.DetailIndex];
        }
        ColumnLayout GetColumnLayout() {
            return IsAcrossThenDownChecked ? ColumnLayout.AcrossThenDown : ColumnLayout.DownThenAcross;
        }
        object GetPageNumberDataContext() {
            return new PageNumberDataContext(PageNumberKind, PageNumberFormat, PageNumberAlignment);
        }
        void ClearPageHeaderFooter() {
            Link.TopMarginTemplate = null;
            Link.BottomMarginTemplate = null;
            Link.TopMarginData = null;
            Link.BottomMarginData = null;
        }
        string GetDefaultFormatString() {
            switch(PageNumberKind) {
                case PageNumberKind.Number: return "Page {0}";
                case PageNumberKind.NumberOfTotal: return "Page {0} of {1}";
                case PageNumberKind.RomanHiNumber: return "- {0} -";
                case PageNumberKind.RomanLowNumber: return "{0}";
            }
            return string.Empty;
        }
    }
}
