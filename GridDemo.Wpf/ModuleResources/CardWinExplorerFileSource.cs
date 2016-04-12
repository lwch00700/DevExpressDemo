using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace GridDemo.ModuleResources {
    public class CardWinExplorerFileSource : INotifyPropertyChanged {

        string fileName;
        string fullPath;
        TypeElement type;
        byte[] icon;
        int size;

        static byte[] folder;
        static Dictionary<string, byte[]> cache = new Dictionary<string, byte[]>();

        public event PropertyChangedEventHandler PropertyChanged;

        public string FileName {
            get { return fileName; }
            set {
                fileName = value;
                if(PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("FileName"));
                    PropertyChanged(this, new PropertyChangedEventArgs("FileNameFirst"));
                }
            }
        }
        public char FileNameFirst {
            get { return char.ToUpper(FileName[0]); }
        }
        public byte[] Icon {
            get { return icon; }
            set {
                icon = value;
                if(PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Icon"));
            }
        }
        public int Size {
            get { return size; }
            set {
                if(size != value) {
                    size = value;
                    if(PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Size"));
                }
            }
        }
        public string FullPath {
            get { return fullPath; }
            set {
                fullPath = value;
                if(PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("FullPath"));
            }
        }
        public TypeElement Type {
            get { return type; }
            set {
                type = value;
                if(PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Type"));
            }
        }

        public enum TypeElement {
            Folder,
            File,
            Drive
        }

        public CardWinExplorerFileSource(string fullPath, TypeElement type, CardWinExplorerDataSource.SizeIcon sizeType, int size) {
            Type = type;
            FullPath = fullPath;
            SetIcon(type, fullPath, sizeType, size);
        }

        public void Resize(CardWinExplorerDataSource.SizeIcon sizeType, int sizeInt) {
            SetIcon(Type, FullPath, sizeType, sizeInt);
        }

        void SetIcon(TypeElement type, string fullPath, CardWinExplorerDataSource.SizeIcon sizeType, int sizeInt) {
            Size = sizeInt;
            Size size = new Size(sizeInt, sizeInt);
            switch(type) {
                case TypeElement.Folder:
                    FileName = Path.GetFileName(fullPath);
                    if(folder == null)
                        folder = CardWinExplorerIconManager.GetLargeIconByte(fullPath, false);
                    Icon = folder;
                    break;
                case TypeElement.File:
                    FileName = Path.GetFileName(fullPath);
                    string ext = System.IO.Path.GetExtension(fullPath);
                    if(ext == ".exe" || ext == ".lnk") {
                        Icon = CardWinExplorerIconManager.GetLargeIconByte(fullPath, true, sizeType);
                    }
                    else if(!cache.ContainsKey(ext)) {
                            byte[] bi = CardWinExplorerIconManager.GetLargeIconByte(fullPath, true, sizeType);
                            cache.Add(ext, bi);
                            Icon = bi;
                          } else
                            Icon = cache[ext];
                    break;
                case TypeElement.Drive:
                    FileName = fullPath;
                    Icon = CardWinExplorerIconManager.GetLargeIconByte(fullPath, false);
                    break;
                default:
                    break;
            }
        }

        public static void ClearCache() {
            cache.Clear();
            folder = null;
        }
    }
}
