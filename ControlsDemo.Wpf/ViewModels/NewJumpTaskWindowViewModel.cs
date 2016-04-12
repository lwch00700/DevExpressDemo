using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Utils;
using DevExpress.Xpf.Core.Native;

namespace ControlsDemo {
    [POCOViewModel(ImplementIDataErrorInfo = true)]
    public class NewJumpTaskWindowViewModel {
        public NewJumpTaskWindowViewModel() {
            Icons = new NamedIcon[] {
                new NamedIcon() {
                    Caption = "Moon",
                    Icon = ImageSourceHelper.GetImageSource(AssemblyHelper.GetResourceUri(typeof(NewJumpTaskWindowViewModel).Assembly, "Images/Moon.png")) },
                new NamedIcon() {
                    Caption = "Sun",
                    Icon = ImageSourceHelper.GetImageSource(AssemblyHelper.GetResourceUri(typeof(NewJumpTaskWindowViewModel).Assembly, "Images/Sun.png")) }
            };
            CustomCategory = "";
            Title = "Title";
            Description = "";
            MessageText = "Message";
            Icon = Icons.ElementAt(0);
        }
        public IEnumerable<NamedIcon> Icons { get; set; }
        public virtual string CustomCategory { get; set; }
        [Required]
        public virtual string Title { get; set; }
        public virtual NamedIcon Icon { get; set; }
        public virtual string Description { get; set; }
        public virtual string MessageText { get; set; }
    }
}
