using System.Windows;
using DevExpress.Xpf.Printing;

namespace PrintingDemo {
    public class PageNumberDataContext {
        PageNumberKind kind;
        string format;
        HorizontalAlignment horizontalContentAlignment;

        public PageNumberKind Kind { get { return kind; } }
        public string Format { get { return format; } }
        public HorizontalAlignment HorizontalContentAlignment { get { return horizontalContentAlignment; } }

        public PageNumberDataContext(PageNumberKind kind, string format, HorizontalAlignment horizontalContentAlignment) {
            this.kind = kind;
            this.format = format;
            this.horizontalContentAlignment = horizontalContentAlignment;
        }
    }
}
