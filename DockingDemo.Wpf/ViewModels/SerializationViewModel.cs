using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using DevExpress.Xpf.Docking;
using DevExpress.Mvvm;

namespace DockingDemo.ViewModels {
    public class SerializableViewModel : BindableBase {
        public string DisplayName {
            get { return GetProperty(() => DisplayName); }
            set { SetProperty(() => DisplayName, value); }
        }
        public object Content {
            get { return GetProperty(() => Content); }
            set { SetProperty(() => Content, value); }
        }
        public string Name {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }
    }
    public class MVVMSerializationViewModel : ViewModelBase {
        private IList<object> _Items;

        public MVVMSerializationViewModel() {
            _Items = GenerateItems(5);
        }
        public IList<object> Items {
            get { return _Items; }
        }
        IList<object> GenerateItems(int count) {
            ObservableCollection<object> items = new ObservableCollection<object>();
            for(int i = 0; i < count; i++) {
                items.Add(new SerializableViewModel() { DisplayName = "Model " + i, Content = i, Name = "Model" + i });
            }
            return items;
        }
        public void Store(Stream stream) {
            ViewModelSerializer serializer = new ViewModelSerializer();
            foreach(SerializableViewModel item in Items) {
                serializer.Views.Add(item);
            }
            ViewModelSerializer.Serialize(stream, serializer);
        }
        public void Store(string stream) {
            ViewModelSerializer serializer = new ViewModelSerializer();
            foreach(SerializableViewModel item in Items) {
                serializer.Views.Add(item);
            }
            ViewModelSerializer.Serialize(stream, serializer);
        }
        public void Restore(Stream stream) {
            ViewModelSerializer serializer = ViewModelSerializer.Deserialize(stream);
            Items.Clear();
            foreach(var item in serializer.Views) {
                Items.Add(item);
            }
        }
        [XmlRoot("ViewModels")]
        public class ViewModelSerializer {
            public string Name { get; set; }
            public List<SerializableViewModel> Views { get; set; }
            public ViewModelSerializer() {
                Views = new List<SerializableViewModel>();
            }
            public static void Serialize(Stream stream, ViewModelSerializer obj) {
                XmlSerializer s = new XmlSerializer(typeof(ViewModelSerializer), new Type[] { typeof(SerializableViewModel) });
                s.Serialize(stream, obj);
            }
            public static void Serialize(string path, ViewModelSerializer obj) {
                XmlSerializer s = new XmlSerializer(typeof(ViewModelSerializer), new Type[] { typeof(SerializableViewModel) });
                using(Stream st = new FileStream(path, FileMode.Create)) {
                    s.Serialize(st, obj);
                }
            }
            public static ViewModelSerializer Deserialize(Stream stream) {
                ViewModelSerializer res;
                XmlSerializer s = new XmlSerializer(typeof(ViewModelSerializer), new Type[] { typeof(SerializableViewModel) });
                res = (ViewModelSerializer)s.Deserialize(stream);
                return res;
            }
        }
    }
}
