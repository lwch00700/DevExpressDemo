using DevExpress.Utils;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace RibbonDemo {
    public class GlyphHelper {
        public static ImageSource GetGlyph(string ItemPath) {
            return new BitmapImage(AssemblyHelper.GetResourceUri(typeof(MVVMRibbon).Assembly, ItemPath));
        }
    }
}
