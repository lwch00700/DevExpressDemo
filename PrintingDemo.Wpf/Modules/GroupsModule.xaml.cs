using System.Windows;
using System.Windows.Data;
using System.Linq;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.DataNodes;
using DevExpress.DemoData.Models;

namespace PrintingDemo {
    public partial class GroupsModule : ModuleBase {
        public GroupsModule() {
            InitializeComponent();
        }
    }

    public class GroupsViewModel : ModuleViewModelBase {
        bool keepTogether;
        bool repeatHeaderEveryPage;
        bool pageBreakAfter;
        bool isKeepTogetherEnabled = true;
        bool isRepeatHeaderEveryPageEnabled = true;
        bool isPageBreakAfterEnabled = true;
        NWindContext context = NWindContext.Create();

        public bool KeepTogether {
            get {
                return keepTogether;
            }
            set {
                if(keepTogether == value)
                    return;
                keepTogether = value;
                RaisePropertyChanged("KeepTogether");
                CreateDocument();
            }
        }
        public bool IsKeepTogetherEnabled {
            get {
                return isKeepTogetherEnabled;
            }
            set {
                if(isKeepTogetherEnabled == value)
                    return;
                isKeepTogetherEnabled = value;
                RaisePropertyChanged("IsKeepTogetherEnabled");
            }
        }
        public bool RepeatHeaderEveryPage {
            get {
                return repeatHeaderEveryPage;
            }
            set {
                if(repeatHeaderEveryPage == value)
                    return;
                repeatHeaderEveryPage = value;
                RaisePropertyChanged("RepeatHeaderEveryPage");
                CreateDocument();
            }
        }
        public bool IsRepeatHeaderEveryPageEnabled {
            get {
                return isRepeatHeaderEveryPageEnabled;
            }
            set {
                if(isRepeatHeaderEveryPageEnabled == value)
                    return;
                isRepeatHeaderEveryPageEnabled = value;
                RaisePropertyChanged("IsRepeatHeaderEveryPageEnabled");
            }
        }
        public bool PageBreakAfter {
            get {
                return pageBreakAfter;
            }
            set {
                if(pageBreakAfter == value)
                    return;
                pageBreakAfter = value;
                RaisePropertyChanged("PageBreakAfter");
                CreateDocument();
            }
        }
        public bool IsPageBreakAfterEnabled {
            get {
                return isPageBreakAfterEnabled;
            }
            set {
                if(isPageBreakAfterEnabled == value)
                    return;
                isPageBreakAfterEnabled = value;
                RaisePropertyChanged("IsPageBreakAfterEnabled");
            }
        }
        public DataTemplate GroupHeaderTemplate { get; set; }

        protected override TemplatedLink CreateLink() {
            CollectionViewLink link = new CollectionViewLink();
            link.GroupInfos.Add(new GroupInfo(GroupHeaderTemplate));
            link.ReportHeaderTemplate = ReportHeaderTemplate;
            link.DetailTemplate = DetailTemplate;
            link.PageFooterTemplate = PageFooterTemplate;
            link.CollectionView = CreateCollectionViewSource().View;
            link.DocumentName = "Products by Categories";
            return link;
        }
        protected override void ProcessLink(TemplatedLink link) {
            IsRepeatHeaderEveryPageEnabled = !(PageBreakAfter || KeepTogether);
            IsPageBreakAfterEnabled = !RepeatHeaderEveryPage;
            IsKeepTogetherEnabled = !RepeatHeaderEveryPage;
            CollectionViewLink collectionViewLink = (CollectionViewLink)link;
            collectionViewLink.GroupInfos[0].Union = KeepTogether ? GroupUnion.WholePage : GroupUnion.None;
            collectionViewLink.GroupInfos[0].RepeatHeaderEveryPage = RepeatHeaderEveryPage;
            collectionViewLink.GroupInfos[0].PageBreakAfter = PageBreakAfter;
        }
        class ProductForPrinting {
            public string CategoryName { get; set; }
            public long? CategoryID { get; set; }
            public string ProductName { get; set; }
            public string QuantityPerUnit { get; set; }
            public decimal? UnitPrice { get; set; }
        }
        CollectionViewSource CreateCollectionViewSource() {
            CollectionViewSource source = new CollectionViewSource {
                Source = context.Products
                         .Join(context.Categories, p => p.CategoryID, c => c.CategoryID,
                            (ps, cs) => new ProductForPrinting {
                                CategoryName = cs.CategoryName,
                                CategoryID = ps.CategoryID,
                                ProductName = ps.ProductName,
                                QuantityPerUnit = ps.QuantityPerUnit,
                                UnitPrice = ps.UnitPrice
                            })
                         .ToList()
            };
            source.View.GroupDescriptions.Add(new PropertyGroupDescription("CategoryName"));
            return source;
        }
    }
}
