﻿using System;
using System.Windows.Media;
using System.Xml.Linq;
using System.Collections.Generic;
using DevExpress.Xpf.TreeMap;

namespace TreeMapDemo {
    public partial class Customizing : TreeMapDemoModule {
        public Customizing() {
            InitializeComponent();
            DataContext = LoadDataFromXML();
        }
        List<OlympicMedals> LoadDataFromXML() {
            XDocument document = DataLoader.LoadXDocumentFromResources("/Data/OlympicMedals2014.xml");
            List<OlympicMedals> infos = new List<OlympicMedals>();
            if (document != null) {
                foreach (XElement element in document.Element("ArrayOfOlympicMedals").Elements()) {
                    OlympicMedals medal = new OlympicMedals();
                    medal.Country = element.Element("Country").Value;
                    medal.SportCategory = element.Element("SportCategory").Value;
                    medal.MedalType = element.Element("MedalType").Value;
                    medal.Count = Convert.ToInt32(element.Element("Count").Value);
                    medal.Athletes = GetAthletes(element.Element("Medals"));
                    infos.Add(medal);
                }
            }
            return infos;
        }

        private List<string> GetAthletes(XElement enumerable) {
            List<string> athletes = new List<string>();
            foreach (XElement item in enumerable.Elements("MedalDetail"))
                athletes.Add(item.Element("Athlete").Value);
            return athletes;
        }
    }

    public class MedalsColorizer : TreeMapColorizerBase {
        class MinMaxColors {
            public Color Min { get; set; }
            public Color Max { get; set; }
        }

        MinMaxColors goldColors = new MinMaxColors() { Min = Color.FromRgb(241, 136, 24), Max = Color.FromRgb(252, 174, 43) };
        MinMaxColors silverColors = new MinMaxColors() { Min = Color.FromRgb(134, 134, 134), Max = Color.FromRgb(192, 192, 192) };
        MinMaxColors bronzeColors = new MinMaxColors() { Min = Color.FromRgb(168, 90, 41), Max = Color.FromRgb(226, 162, 119) };

        Color MixColors(MinMaxColors colors, double coeff) {
            return Color.FromRgb((byte)(colors.Max.R * (1 - coeff) + colors.Min.R * coeff),
                (byte)(colors.Max.G * (1 - coeff) + colors.Min.G * coeff),
                (byte)(colors.Max.B * (1 - coeff) + colors.Min.B * coeff));
        }

        public override Color? GetItemColor(TreeMapItem item, TreeMapItemGroupInfo group) {
            if (group.GroupLevel == 1)
                return Colors.White;
            if (group.GroupLevel == 2) {
                double delta = group.MaxValue - group.MinValue;
                double coef = 1 - (group.MaxValue - item.Value) / delta;
                switch (((OlympicMedals)item.Tag).MedalType) {
                    case "Gold": return MixColors(goldColors, coef);
                    case "Silver": return MixColors(silverColors, coef);
                    case "Bronze": return MixColors(bronzeColors, coef);
                    default:
                        break;
                }
            }
            return null;
        }
        protected override TreeMapDependencyObject CreateObject() {
            return new MedalsColorizer();
        }
    }

    public class OlympicMedals {
        public string Country { get; set; }
        public string SportCategory { get; set; }
        public string MedalType { get; set; }
        public int Count { get; set; }
        public List<string> Athletes { get; set; }
    }
}
