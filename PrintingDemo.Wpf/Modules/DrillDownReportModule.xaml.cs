using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DevExpress.Xpf.Printing;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm;

namespace PrintingDemo {
    public partial class DrillDownReportModule : ModuleBase {
        public DrillDownReportModule() {
            InitializeComponent();
        }
    }

    public class DrillDownReportModuleViewModel : ModuleViewModelBase {
        int currentPageNumber;
        ICommand previewClickCommand;
        int tempPageNumber;
        NWindContext context = NWindContext.Create();
        public class CategoryWrapper {
            public bool IsExpanded { get; set; }
            public Category Category { get; private set; }

            public CategoryWrapper(bool isExpanded, Category category) {
                this.IsExpanded = isExpanded;
                this.Category = category;
            }
        }

        public int CurrentPageNumber {
            get { return currentPageNumber; }
            set { SetProperty(ref currentPageNumber, value, () => CurrentPageNumber); }
        }

        Dictionary<long, CategoryWrapper> CategoryWrappers { get; set; }

        public ICommand PreviewClickCommand {
            get {
                return previewClickCommand ?? (previewClickCommand = new DelegateCommand<DocumentPreviewMouseEventArgs>(OnPreviewMouseClick));
            }
        }

        public DrillDownReportModuleViewModel() {
            FillCategoryWrappers();
        }

        protected override TemplatedLink CreateLink() {
            SimpleLink link = new SimpleLink();
            link.DetailTemplate = DetailTemplate;
            link.DocumentName = "Drill-Down";
            link.DetailCount = CategoryWrappers.Count;
            link.CreateDetail += link_CreateDetail;
            return link;
        }

        void FillCategoryWrappers() {
            CategoryWrappers = new Dictionary<long, CategoryWrapper>();
            foreach(var category in context.Categories.ToList()) {
                CategoryWrappers.Add(category.CategoryID, new CategoryWrapper(false, category));
            }
        }

        void link_CreateDetail(object sender, CreateAreaEventArgs e) {
            e.Data = CategoryWrappers[CategoryWrappers.ElementAt(e.DetailIndex).Value.Category.CategoryID];
        }

        void link_CreateDocumentFinished(object sender, EventArgs e) {
            Link.CreateDocumentFinished -= link_CreateDocumentFinished;
            CurrentPageNumber = tempPageNumber;
        }

        void OnPreviewMouseClick(DocumentPreviewMouseEventArgs args) {
            int categoryID;
            if(int.TryParse(string.Format("{0}", args.ElementTag), out categoryID)) {
                CategoryWrappers[categoryID].IsExpanded = !CategoryWrappers[categoryID].IsExpanded;
                tempPageNumber = CurrentPageNumber;
                Link.CreateDocumentFinished += link_CreateDocumentFinished;
                Link.CreateDocument(true);
            }
        }
    }
}
