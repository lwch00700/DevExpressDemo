using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DevExpress.Xpf.Utils;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.DemoBase.DataClasses;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using DevExpress.Xpf.Grid.TreeList;

namespace GridDemo {
    public class CustomNodeImageSelector : TreeListNodeImageSelector {
        public CustomNodeImageSelector() {
            ImageCache = new Dictionary<string, ImageSource>();
        }
        Dictionary<string, ImageSource> ImageCache { get; set; }
        public override ImageSource Select(TreeListRowData rowData) {
            string groupName = rowData.View.DataControl.GetCellValue(rowData.RowHandle.Value, "GroupName") as string;
            if(ImageCache.ContainsKey(groupName))
                return ImageCache[groupName];
            ImageSource image = new BitmapImage(new Uri(GroupNameToImageConverter.GetImagePathByGroupName(groupName), UriKind.Relative));
            ImageCache.Add(groupName, image);
            return image;
        }
    }
}
