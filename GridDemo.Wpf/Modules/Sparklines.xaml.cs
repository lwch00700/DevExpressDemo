
using DevExpress.Xpf.Grid.Printing;
using DevExpress.XtraExport.Helpers;
namespace GridDemo {
    public partial class Sparklines : GridDemoModule {
        public Sparklines() {
            InitializeComponent();
        }

        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
