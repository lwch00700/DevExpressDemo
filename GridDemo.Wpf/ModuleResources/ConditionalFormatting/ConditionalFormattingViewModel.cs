using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GridDemo {
    public class ConditionalFormattingViewModel {
        public ConditionalFormattingViewModel() {
            Items = SaleOverviewDataGenerator.GenerateSales();
        }
        public SaleOverviewData[] Items { get; private set; }
    }
}
