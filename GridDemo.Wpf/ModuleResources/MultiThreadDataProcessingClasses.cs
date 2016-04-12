using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DevExpress.Xpf.Utils;
using System.ComponentModel;
using System.Collections;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace GridDemo {
    [POCOViewModel]
    public class CountItem {
        public virtual string DisplayName { get; set; }
        public virtual int Count { get; set; }
    }
    public class CountItemCollection : List<CountItem> { }
    public class OrderDataListSource : IListSource {
        OrderDataGenerator orderDataGenerator;

        public OrderDataListSource(OrderDataGenerator orderDataGenerator) {
            this.orderDataGenerator = orderDataGenerator;
        }
        public bool ContainsListCollection {
            get { return false; }
        }
        public IList GetList() {
            return orderDataGenerator.GetOrders();
        }
    }

}
