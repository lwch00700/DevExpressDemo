using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Utils;
using DevExpress.Xpf.Carousel;
using DevExpress.Xpf.DemoBase;

namespace CarouselDemo {
    public class ResourcesTable {
        public const string ImagesPath = @"Data/Images/";
        public const string ImagesPrefix = "/CarouselDemo;component/";

        static ResourcesTable() {
            UriTable = GetImageNames(ImagesPath);
        }

        static string[] GetImageNames(string path) {
            Assembly asm = Assembly.GetExecutingAssembly();
            IDictionaryEnumerator enumerator = AssemblyHelper.GetResourcesEnumerator(asm);
            List<string> imageNames = new List<string>();
            while(enumerator.MoveNext()) {
                string resourceName = (string)enumerator.Key;
                if(resourceName.StartsWith(path, StringComparison.OrdinalIgnoreCase)) {
                    if(resourceName.Contains("logo-back.png"))
                        continue;
                    imageNames.Add(ImagesPrefix + resourceName);
                }
            }
            imageNames.Sort();
            return imageNames.ToArray();
        }
        public static string[] UriTable;

        public static Uri GetUri(string name) {
            string result = Array.Find<string>(ResourcesTable.UriTable, s => s.EndsWith(name, StringComparison.OrdinalIgnoreCase));
            return new Uri(result, UriKind.Relative);
        }
    }
    public class CarouselDemoModule : DemoModule {
        static CarouselDemoModule() {
            Type ownerType = typeof(CarouselDemoModule);
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            CommandManager.InvalidateRequerySuggested();
        }
        protected virtual List<FrameworkElement> CreateItems(string path, ItemType it) {
            ContentLoadHelper contentLoadHelper = new ContentLoadHelper();
            contentLoadHelper.Path = path;
            var itemList = new List<FrameworkElement>(contentLoadHelper.LoadItems(it).ToArray());
            for(int i = 0; i < itemList.Count; i++) {
                itemList[i].Name = "Item" + i.ToString();
                ((Image)itemList[i]).Stretch = System.Windows.Media.Stretch.Fill;
            }
            return itemList;
        }
        protected virtual void AddItems(string path, ItemType it, CarouselPanel carousel) {
            var itemList = CreateItems(path, it);
            foreach(var item in itemList) {
                item.Name = "item" + carousel.Children.Count;
                carousel.Children.Add(item);
            }
        }
    }
}
