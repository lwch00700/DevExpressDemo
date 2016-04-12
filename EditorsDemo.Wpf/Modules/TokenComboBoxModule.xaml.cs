using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using DevExpress.Xpf.DemoBase.DataClasses;

namespace EditorsDemo {
    public partial class TokenComboBoxModule : EditorsDemoModule {
        public TokenComboBoxModule() {
            InitializeComponent();
            DataContext = new TokenComboBoxSource();
        }
    }
    public class TokenComboBoxSource {
        NWindContext context = NWindContext.Create();
        public object Countries { get { return context.CountriesArray; } }

        List<object> selectedCountries;
        public object SelectedCountries {
            get {
                if (selectedCountries == null)
                    selectedCountries = CreateSelectedCountries();
                return selectedCountries;
            }
        }
        List<object> selectedCountries2;
        public object SelectedCountries2 {
            get {
                if (selectedCountries2 == null)
                    selectedCountries2 = CreateSelectedCountries2();
                return selectedCountries2;
            }
        }
        List<object> selectedCountries3;
        public object SelectedCountries3 {
            get {
                if (selectedCountries3 == null)
                    selectedCountries3 = CreateSelectedCountries3();
                return selectedCountries3;
            }
        }

        private List<object> CreateSelectedCountries3() {
            return new List<object>() {
                context.CountriesArray[0],
                context.CountriesArray[1],
                context.CountriesArray[12],
                context.CountriesArray[5],
                context.CountriesArray[7],
                context.CountriesArray[3],
                context.CountriesArray[10],
                context.CountriesArray[15],
                context.CountriesArray[21],
                context.CountriesArray[25],
                context.CountriesArray[29],
                context.CountriesArray[30],
                context.CountriesArray[90],
                context.CountriesArray[40],
                context.CountriesArray[22],
                context.CountriesArray[54],
                context.CountriesArray[20],
                context.CountriesArray[31],
                context.CountriesArray[37],
                context.CountriesArray[43],
                context.CountriesArray[49],
                context.CountriesArray[63],
                context.CountriesArray[4],
                context.CountriesArray[6],
                context.CountriesArray[60],
                context.CountriesArray[61],
                context.CountriesArray[65],
                context.CountriesArray[70],
                context.CountriesArray[74],
                context.CountriesArray[76],
                context.CountriesArray[71],
                context.CountriesArray[73],
            };
        }

        private List<object> CreateSelectedCountries2() {
            return new List<object>() {
                context.CountriesArray[0],
                context.CountriesArray[1],
                context.CountriesArray[12],
                context.CountriesArray[5],
                context.CountriesArray[7],
                context.CountriesArray[3],
                context.CountriesArray[10],
                context.CountriesArray[15],
                context.CountriesArray[2],
                context.CountriesArray[8]
            };
        }
        private List<object> CreateSelectedCountries() {
            return new List<object>() {
                context.CountriesArray[7],
                context.CountriesArray[3],
                context.CountriesArray[10]
            };
        }
    }
}
