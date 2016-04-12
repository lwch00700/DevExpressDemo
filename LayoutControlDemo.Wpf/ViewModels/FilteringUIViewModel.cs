using DevExpress.DemoData.Models.Vehicles;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.Filtering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DevExpress.Xpf.LayoutControlDemo {
    public class FilteringUIViewModel {
        public IEnumerable<Model> Vehicles { get; private set; }
        public IEnumerable<Trademark> Trademarks { get; private set; }
        public IEnumerable<TransmissionType> TransmissionTypes { get; private set; }
        public IEnumerable<BodyStyle> BodyStyles { get; private set; }
        public int[] DoorTypes { get; private set; }
        public decimal MinPrice { get; private set; }
        public decimal MaxPrice { get; private set; }
        public int MinMPGCity { get; private set; }
        public int MaxMPGCity { get; private set; }
        public int MinMPGHighway { get; private set; }
        public int MaxMPGHighway { get; private set; }
        public FilteringUIViewModel() {
            if(this.IsInDesignMode()) return;
            var context = new VehiclesContext();
            Vehicles = context.Models.ToList();
            Trademarks = context.Trademarks.ToList();
            TransmissionTypes = context.TransmissionTypes.ToList();
            BodyStyles = context.BodyStyles.ToList();
            DoorTypes = new int[] { 2, 3, 4 };
            CalculateProperties();
        }
        void CalculateProperties() {
            MinPrice = Vehicles.Min(x => x.Price);
            MaxPrice = Vehicles.Max(x => x.Price);
            MinMPGCity = Vehicles.Min(x => x.MPGCity ?? int.MaxValue);
            MaxMPGCity = Vehicles.Max(x => x.MPGCity ?? int.MinValue);
            MinMPGHighway = Vehicles.Min(x => x.MPGHighway ?? int.MaxValue);
            MaxMPGHighway = Vehicles.Max(x => x.MPGHighway ?? int.MinValue);
        }
    }
    public class VehiclesFilteringViewModel {
        [Display(Name = "Price ($)")]
        [FilterRange("MinPrice", "MaxPrice")]
        public decimal Price { get; set; }
        [Display(Name = "Trademark")]
        [FilterLookup("Trademarks", 8, DisplayMember = "Name", ValueMember = "ID")]
        public int TrademarkID { get; set; }
        [Display(Name = "Transmission Type")]
        [FilterLookup("TransmissionTypes", DisplayMember = "Name", ValueMember = "ID", UseFlags = false, UseSelectAll = true)]
        public int TransmissionTypeID { get; set; }
        [Display(Name = "Body Style")]
        [FilterLookup("BodyStyles", DisplayMember = "Name", ValueMember = "ID", UseSelectAll = false)]
        public int BodyStyleID { get; set; }
        [Display(Name = "Doors")]
        [FilterLookup("DoorTypes", UseFlags = false, UseSelectAll = true )]
        public int Doors { get; set; }
        [Display(Name = "MPG City")]
        [FilterRange("MinMPGCity", "MaxMPGCity", EditorType = RangeUIEditorType.Range)]
        public int? MPGCity { get; set; }
        [Display(Name = "MPG Highway")]
        [FilterRange("MinMPGHighway", "MaxMPGHighway", EditorType = RangeUIEditorType.Range)]
        public int? MPGHighway { get; set; }
    }
}
