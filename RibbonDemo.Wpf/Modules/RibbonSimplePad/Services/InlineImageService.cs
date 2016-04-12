using DevExpress.Mvvm.UI;
using System.Windows;
using System.Windows.Documents;
namespace RibbonDemo {
    public interface IInlineImageService {
        void InsertImage(InlineImageViewModel imageViewModel);
        InlineImageViewModel GetViewModelFromContainer();
    }
    public class RibbonSimplePadImageService : ServiceBase, IInlineImageService {
        protected DemoRichControl RichControl { get { return AssociatedObject as DemoRichControl; } }
        public void InsertImage(InlineImageViewModel imageViewModel) {
            RichControl.InsertContainer(new InlineUIContainer() { Child = new InlineImage(imageViewModel) });
        }
        public InlineImageViewModel GetViewModelFromContainer() {
            if(RichControl.Container != null)
                return ((InlineImage)((InlineUIContainer)RichControl.Container).Child).InlineImageViewModel;
            return null;
        }
    }
}
