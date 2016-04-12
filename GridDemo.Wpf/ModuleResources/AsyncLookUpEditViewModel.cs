using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.DemoBase.Helpers;
using System.ComponentModel;
using DevExpress.Mvvm;

namespace GridDemo {
    public class AsyncLookUpEditViewModel : BindableBase {
        CountItemCollection countItems;
        CountItem selectedCountItem;
        IQueryable queryableSource;
        OrderDataGenerator orderDataGenerator;
        OrderDataListSource orderDataListSource;
        bool isDesignTime = true;

        public OrderDataListSource ListSource {
            get { return orderDataListSource; }
            set { SetProperty(ref orderDataListSource, value, () => orderDataListSource); }
        }
        public IQueryable QueryableSource {
            get { return queryableSource; }
            set { SetProperty(ref queryableSource, value, () => QueryableSource); }
        }
        public CountItemCollection CountItems {
            get { return countItems; }
            set {
                if (SetProperty(ref countItems, value, () => CountItems)) {
                    if((CountItems != null) && (CountItems.Count > 0)) {
                        SelectedCountItem = CountItems[CountItems.Count / 2];
                    } else {
                        SelectedCountItem = null;
                    }
                }
            }
        }
        public CountItem SelectedCountItem {
            get { return selectedCountItem; }
            set { SetProperty(ref selectedCountItem, value, () => SelectedCountItem, RecreateItemsSource); }
        }

        public AsyncLookUpEditViewModel() {
            orderDataGenerator = new OrderDataGenerator(0);
        }

        void RecreateItemsSource() {
            if(SelectedCountItem == null || isDesignTime) {
                orderDataGenerator.Count = 0;
            }
            else {
                orderDataGenerator.Count = SelectedCountItem.Count;
            }
            ListSource = new OrderDataListSource(orderDataGenerator);
            QueryableSource = ((IListSource)ListSource).GetList().AsQueryable();
        }
        public void SetIsDesignTime(bool value) {
            isDesignTime = value;
            RecreateItemsSource();
        }
    }
}
