using System;
using System.Linq;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Printing;
using DevExpress.DemoData.Models;
using System.Collections.Generic;

namespace PrintingDemo {
    public partial class BadgesModule : ModuleBase {
        public BadgesModule() {
            InitializeComponent();
        }
    }

    public class BadgesViewModel : ModuleViewModelBase {
        List<Employee> employees = NWindContext.Create().Employees.ToList();
        protected override TemplatedLink CreateLink() {
            SimpleLink link = new SimpleLink();
            link.ReportHeaderTemplate = ReportHeaderTemplate;
            link.DetailTemplate = DetailTemplate;
            link.ReportFooterTemplate = ReportFooterTemplate;
            link.PageFooterTemplate = PageFooterTemplate;
            link.DetailCount = employees.Count;
            link.DocumentName = "Badges";
            link.ReportFooterData = String.Format("Generated on {0}", DateTime.Now);
            link.ReportFooterData = String.Concat(link.ReportFooterData, String.Format(" by {0}\\{1}", Environment.UserDomainName, Environment.UserName));
            link.CreateDetail += link_CreateDetail;
            return link;
        }
        void link_CreateDetail(object sender, CreateAreaEventArgs e) {
            e.Data = employees[e.DetailIndex];
        }
    }
}
