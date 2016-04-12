using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EditorsDemo.SCService;

namespace EditorsDemo.ViewModels {
    public class WCFInstantFeedbackViewModel {
        public WCFInstantFeedbackViewModel() {
            WcfSCService = new SCEntities(new Uri("http://demos.devexpress.com/Services/WcfLinqSC/WcfSCService.svc/"));
        }
        public virtual SCEntities WcfSCService { get; set; }
    }

}
