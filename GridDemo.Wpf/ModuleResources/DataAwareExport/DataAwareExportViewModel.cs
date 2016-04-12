using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace GridDemo {
    [POCOViewModel]
    public class DataAwareExportViewModel {
        ProductSaleDataGenerator dataGenerator;

        public virtual int? ItemCount { get; set; }
        protected void OnItemCountChanged() {
            if(!ViewModelBase.IsInDesignMode && ItemCount.HasValue)
                Items = dataGenerator.GenerateSales(ItemCount.Value);
        }

        public virtual List<ProductSaleData> Items { get; set; }

        public DataAwareExportViewModel() {
            if(!ViewModelBase.IsInDesignMode)
                dataGenerator = new ProductSaleDataGenerator();
        }
    }
}
