using DevExpress.Xpf.DemoBase.Helpers;
using GridDemo.Controls;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Reflection;
using System.Xml;
using PersonInfo = GridDemo.Controls.Person;

namespace GridDemo {
    public class PersonGenerator {
        Random rnd = new Random();

        List<string> lastNames = Dump("LastNames.xml");
        List<string> firstNames = Dump("FirstNames.xml");
        List<string> cities = Dump("Cities.xml");
        List<string> streets = Dump("Streets.xml");

        static string[] employeeJobTitles = {
                "Accounting Manager",
                "Assistant Sales Agent",
                "Assistant Sales Representative",
                "Coordinator Foreign Markets",
                "Export Administrator",
                "International Marketing Manager",
                "Marketing Assistant",
                "Marketing Manager",
                "Marketing Representative",
                "Order Administrator",
                "Product Manager",
                "Purchasing Agent",
                "Purchasing Manager",
                "Regional Account Representative",
                "Sales Agent",
                "Sales Associate",
                "Sales Manager",
                "Sales Representative"
            };
        static string[] ownerJobTitles = {
                "Owner",
                "Owner/Marketing Assistant",
            };
        static string[] companySuffixes = {
            "General Partnership",
            "LP",
            "LLP",
            "LLP",
            "LLLP",
            "LLC",
            "PLLC",
            "Corp.",
            "Inc.",
            "PC",
            "Company",
            "Limited",
            "Urban Development",
            "Finance",
            "Mobile",
            "Foundation",
            "Association",
            "Bank",
            "Industries",
            "Motors",
            "Electric"
        };

        static List<string> Dump(string fileName) {
            var result = new List<string>();
            Assembly assembly = typeof(PersonGenerator).Assembly;
            var file = assembly.GetManifestResourceStream(DemoHelper.GetPath(assembly.GetName().Name + ".Data.", assembly) + fileName);
            if (file != null) {
                var reader = new XmlTextReader(file);
                while (reader.Read()) {
                    if (reader.NodeType == XmlNodeType.Text) {
                        string nodeValue = reader.Value;
                        if (!string.IsNullOrEmpty(nodeValue))
                            result.Add(reader.Value);
                    }
                }
            }
            return result;
        }


        public PersonInfo CreatePerson() {
            var person = new PersonInfo();
            string firstName = GenerateFirstName();
            string lastName = GenerateLastName();
            person.FullName = CreateFullName(firstName, lastName);
            string companyBaseName = GetRandomElement(lastNames);
            string companySuffix = GetRandomElement(companySuffixes);
            person.Company = CreateCompanyName(companyBaseName, companySuffix);
            person.JobTitle = GenerateJobTitle();
            person.City = GenerateCity();
            person.Address = GenerateAddress();
            person.Phone = GeneratePhone();
            person.Email = CreateEmail(firstName, lastName, companyBaseName);
            return person;
        }

        string GenerateFirstName() {
            return GetRandomElement(firstNames);
        }
        string GenerateLastName() {
            return GetRandomElement(lastNames);
        }
        string CreateFullName(string firstName, string lastName) {
            return firstName + " " + lastName;
        }
        string CreateCompanyName(string companyBase, string companySuffix) {
            return companyBase + " " + companySuffix;
        }
        string GenerateJobTitle() {
            if(rnd.NextDouble() > 0.9)
                return GetRandomElement(ownerJobTitles);
            return GetRandomElement(employeeJobTitles);
        }
        string GenerateCity() {
            return GetRandomElement(cities);
        }
        string GenerateAddress() {
            return string.Format("{0}, {1}-{2}", GetRandomElement(streets), rnd.Next(1, 99), rnd.Next(10, 999));
        }
        string GeneratePhone() {
            return string.Format("({0}) {1}-{2}", rnd.Next(100, 999), rnd.Next(100, 999), rnd.Next(1000, 9999));
        }
        string CreateEmail(string firstName, string lastName, string companyBaseName) {
            return firstName.ToLowerInvariant() + "." + lastName.ToLowerInvariant() + "@" + companyBaseName.ToLowerInvariant() + ".com";
        }
        T GetRandomElement<T>(IList<T> list) {
            return list[rnd.Next(0, list.Count - 1)];
        }
    }

    public class ServerModePeopleDataGenerator : ServerModeRecordsGeneratorBase {
        public ServerModePeopleDataGenerator(SQLConnectionWindow parentWindow, bool clearRecords) : base(parentWindow, clearRecords) { }

        PersonDataContext context;

        PersonGenerator personGenerator = new PersonGenerator();

        protected override DataContext DataBaseContext { get { return context; } }

        protected override void SetupDataContext(string connection) {
            context = CreateContext(connection);
        }
        PersonDataContext CreateContext(string connection) {
            return new PersonDataContext(connection);
        }

        protected override void AddNewItem() {
            context.Person.InsertOnSubmit(personGenerator.CreatePerson());
        }

        protected override string TableName { get { return typeof(Controls.Person).Name; } }

        protected override void CreateTable() {
            DataBaseContext.ExecuteCommand(string.Format(@"
            CREATE TABLE {0}(
            [PersonID] [int] NOT NULL IDENTITY,
            [FullName] [nvarchar](max) NULL,
            [Company] [nvarchar](max) NULL,
            [JobTitle] [nvarchar](max) NULL,
            [City] [nvarchar](max) NULL,
            [Address] [nvarchar](max) NULL,
            [Phone] [nvarchar](max) NULL,
            [Email] [nvarchar](max) NULL,
            CONSTRAINT {0}_pk PRIMARY KEY (PersonID)
            );
            ", TableName));
        }

        protected override void Clear() {
            DataBaseContext.ExecuteCommand("DELETE FROM " + TableName + " WHERE PersonID>=0");
            DataBaseContext.ExecuteCommand(string.Format("DBCC CHECKIDENT('{0}', RESEED, 0)", TableName));
        }
    }
    public class PeopleGeneratorProvider : ServerModeRecordsGeneratorProviderBase {
        public override ServerModeRecordsGeneratorBase Create(SQLConnectionWindow parentWindow, bool clearRecords) {
            return new ServerModePeopleDataGenerator(parentWindow, clearRecords);
        }

        public override int CalcRecordCount(string serverConnectionString) {
            return CalcRecordCountCore(() => new PersonDataContext(serverConnectionString), x => x.Person);
        }
    }
}
