namespace RibbonDemo {
    public class RecentItemViewModel {
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Glyph { get; set; }
        public RecentItemViewModel() { }
        public RecentItemViewModel(string caption, string description, string glyph) {
            Caption = caption;
            Description = description;
            Glyph = glyph;
        }
    }
}
