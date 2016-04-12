using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ChartsDemo {
    public class AgePopulation {
        string name;
        string age;
        string sex;
        double population;

        public string Name { get { return name; } }
        public string Age { get { return age; } }
        public string Sex { get { return sex; } }
        public string SexAgeKey { get { return sex.ToString() + ": " + age; } }
        public string CountryAgeKey { get { return name + ": " + age; } }
        public string CountrySexKey { get { return name + ": " + sex.ToString(); ; } }
        public double Population { get { return population; } }

        public AgePopulation(string name, string age, string gender, double population) {
            this.name = name;
            this.age = age;
            this.sex = gender;
            this.population = population;
        }
    }

    public static class AgePopulationDataLoader {
        public static List<AgePopulation> CreateDataSource() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/PopulationAgeStructure.xml");
            List<AgePopulation> result = new List<AgePopulation>();
            if (document != null) {
                foreach (XElement genderElement in document.Element("Genders").Elements()) {
                    string gender = genderElement.Element("Name").Value;
                    foreach (XElement ageElement in genderElement.Element("Ages").Elements()) {
                        string age = ageElement.Element("Value").Value;
                        foreach (XElement countryElement in ageElement.Element("Countries").Elements()) {
                            string country = countryElement.Element("Name").Value;
                            double population = Convert.ToDouble(countryElement.Element("Population").Value, CultureInfo.InvariantCulture);
                            result.Add(new AgePopulation(country, age, gender, population));
                        }
                    }
                }
            }
            return result;
        }
    }

}
