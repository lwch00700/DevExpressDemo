using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using DevExpress.Utils;
using DevExpress.Utils.Zip;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase.Helpers.TextColorizer;
using DevExpress.Xpf.RichEdit;
using DevExpress.Xpf.SpellChecker;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSpellChecker;

namespace SpellCheckerDemo {
    public class DemoUtils {
        public static readonly string PathToDemoData = "SpellCheckerDemo.Data";
        public static readonly string PathToDictionaries = PathToDemoData + ".Dictionaries";

        public static string GetPathToResource(string path, string name) {
            if (DemoHelper.GetDemoLanguage(typeof(DemoUtils).Assembly) == CodeLanguage.CS)
                return String.Format("{0}.{1}", path, name);
            else
                return name;
        }
        public static Stream GetDataStream(string path, string name) {
            string fullPath = DemoUtils.GetPathToResource(path, name);
            if (!String.IsNullOrEmpty(fullPath))
                return Assembly.GetExecutingAssembly().GetManifestResourceStream(fullPath);
            return null;
        }
        public static void ShowDialog(string title, string text, FrameworkElement owner) {
            TextBlock textBox = new TextBlock() { Text = text };
            textBox.TextWrapping = TextWrapping.Wrap;
            textBox.VerticalAlignment = VerticalAlignment.Center;
            textBox.HorizontalAlignment = HorizontalAlignment.Center;
            DialogControl dialogControl = new DialogControl() { DialogContent = textBox, UseContentIndents = true };
            dialogControl.CancelButton.Visibility = System.Windows.Visibility.Collapsed;
            FloatingContainer.ShowDialog(dialogControl, owner, Size.Empty, new FloatingContainerParameters() {
                AllowSizing = false,
                CloseOnEscape = true,
                Title = title
            });
        }
        public static BitmapImage GetBitmapImage(string fileName) {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = DemoUtils.GetDataStream(DemoUtils.PathToDemoData, fileName);
            bmp.EndInit();
            return bmp;
        }
    }

    public static class SpellCheckerHelper {
        public static void RegisterDefaultDictionaries(SpellChecker spellChecker) {
            spellChecker.Dictionaries.Add(GetDefaultDictionary());
            spellChecker.Dictionaries.Add(GetCustomDictionary());
        }
        public static void RegisterHunspellDictionaries(SpellChecker spellChecker) {
            spellChecker.Dictionaries.Add(CreateHunspellDictionaries(new CultureInfo("en-US")));
            spellChecker.Dictionaries.Add(CreateHunspellDictionaries(new CultureInfo("de-DE")));
            spellChecker.Dictionaries.Add(CreateHunspellDictionaries(new CultureInfo("ru-RU")));
        }
        static HunspellDictionary CreateHunspellDictionaries(CultureInfo culture) {
            string[] parts = culture.Name.Split('-');
            HunspellDictionary result = new HunspellDictionary();
            string uriPath = String.Format("pack://application:,,,/SpellCheckerDemo;component//Data/Dictionaries/{0}/", parts[0]);
            Stream dictionaryStream = Application.GetResourceStream(new Uri(String.Format("{0}{1}_{2}.dic", uriPath, parts[0], parts[1]))).Stream;
            Stream grammarStream = Application.GetResourceStream(new Uri(String.Format("{0}{1}_{2}.aff", uriPath, parts[0], parts[1]))).Stream;
            try {
                result.LoadFromStream(dictionaryStream, grammarStream);
            }
            catch { }
            finally {
                dictionaryStream.Close();
                grammarStream.Close();
            }
            result.Culture = culture;
            return result;
        }
        static Stream GetFileStream(InternalZipFileCollection files, string name) {
            Stream stream = files.Find(delegate(InternalZipFile file) {
                return file.FileName.IndexOf(name) >= 0;
            }).FileDataStream;
            try {
                return CreateMemoryStream(stream);
            }
            finally {
                stream.Close();
            }
        }
        static Stream CreateMemoryStream(Stream stream) {
            MemoryStream result = new MemoryStream();
            for (; ; ) {
                int readedByte = stream.ReadByte();
                if (readedByte < 0)
                    break;
                result.WriteByte((byte)readedByte);
            }
            result.Flush();
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }
        static ISpellCheckerDictionary GetDefaultDictionary() {
            SpellCheckerISpellDictionary dic = new SpellCheckerISpellDictionary();
            using (Stream stream = DemoUtils.GetDataStream(DemoUtils.PathToDictionaries, "default.zip")) {
                InternalZipFileCollection files = InternalZipArchive.Open(stream);
                Stream dictionaryStream = GetFileStream(files, "american.xlg");
                Stream grammarStream = GetFileStream(files, "english.aff");
                Stream alphabetStream = DemoUtils.GetDataStream(DemoUtils.PathToDictionaries, "EnglishAlphabet.txt");
                try {
                    dic.LoadFromStream(dictionaryStream, grammarStream, alphabetStream);
                }
                catch {
                }
                finally {
                    dictionaryStream.Close();
                    grammarStream.Close();
                    alphabetStream.Close();
                }
            }
            dic.Culture = new CultureInfo("en-US");
            return dic;
        }
        static ISpellCheckerDictionary GetCustomDictionary() {
            SpellCheckerCustomDictionary result = new SpellCheckerCustomDictionary();
            Stream dictionaryStream = DemoUtils.GetDataStream(DemoUtils.PathToDictionaries, "CustomEnglish.dic");
            Stream alphabetStream = DemoUtils.GetDataStream(DemoUtils.PathToDictionaries, "EnglishAlphabet.txt");
            try {
                result.Load(dictionaryStream, alphabetStream);
            }
            catch {
            }
            finally {
                dictionaryStream.Close();
                alphabetStream.Close();
            }
            result.Culture = new CultureInfo("en-US");
            return result;
        }
    }
    public class DocumentLoadHelper {
        public static void Load(String fileName, DocumentFormat format, RichEditControl richEditControl) {
            string path = DemoUtils.GetPathToResource(DemoUtils.PathToDemoData, fileName);
            if (!String.IsNullOrEmpty(path)) {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path)) {
                    richEditControl.LoadDocument(stream, format);
                }
            }
        }
    }

    public class Employees : System.ComponentModel.INotifyPropertyChanged {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public double Salary { get; set; }
        public bool OnVacation { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public int ReportsTo { get; set; }

        #region INotifyPropertyChanged Members
        System.ComponentModel.PropertyChangedEventHandler onPropertyChanged;
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged { add { onPropertyChanged += value; } remove { onPropertyChanged -= value; } }
        void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler handler = onPropertyChanged;
            if (handler != null)
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public static class EmployeesData {
        static IList dataSource;

        public static IList DataSource {
            get {
                if (dataSource == null) {
                    dataSource = GetDataSource();
                    DoMistakes(dataSource);
                }
                return dataSource;
            }
        }
        static IList GetDataSource() {
            XmlSerializer s = new XmlSerializer(typeof(List<Employees>), new XmlRootAttribute("NewDataSet"));
            Stream stream = typeof(EmployeesData).Assembly.GetManifestResourceStream(DemoUtils.GetPathToResource(DemoUtils.PathToDemoData, "nwind.xml"));
            return (IList)s.Deserialize(stream);
        }
        static void DoMistakes(IList dataSet) {
            foreach (Employees employee in dataSet) {
                StringBuilder text = new StringBuilder(employee.Notes);
                List<char> charSet = CreateCharSet(text);
                Random random = new Random(Environment.TickCount);
                for (int i = text.Length - 1; i >= 0; i -= 30) {
                    if (!Char.IsLetter(text[i]))
                        continue;
                    char ch = GetRandomChar(charSet);
                    if (Char.IsUpper(text[i]))
                        ch = Char.ToUpper(ch);
                    if (text[i] == ch)
                        text.Remove(i, 1);
                    else
                        text[i] = ch;
                }
                employee.Notes = text.ToString();
            }
        }
        static List<char> CreateCharSet(StringBuilder text) {
            List<char> result = new List<char>();
            int length = text.Length;
            for (int i = 0; i < length; i++) {
                char ch = text[i];
                if (!Char.IsLetter(ch))
                    continue;
                ch = Char.ToLower(ch);
                int index = result.BinarySearch(ch);
                if (index < 0)
                    result.Insert(~index, ch);
            }
            return result;
        }
        static char GetRandomChar(List<char> charSet) {
            Random random = new Random(Environment.TickCount);
            int index = random.Next(0, charSet.Count - 1);
            return charSet[index];
        }
    }
    public class BitmapToBitmapSourceConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return GetImageSource((byte[])value);
        }
        public static ImageSource GetImageSource(byte[] bytes) {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            try {
                bi.StreamSource = new MemoryStream(bytes);
            }
            finally {
                bi.EndInit();
            }
            return bi;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class EmployeeToAddressStringConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Employees employee = value as Employees;
            if (employee == null || typeof(string) != targetType)
                return null;
            return String.Format("{0}, {1}, {2}", employee.Country, employee.City, employee.Address);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
