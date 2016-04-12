using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DiagramDemo.Data {
    [XmlRoot("Employees")]
    public class EmployeesWithPhotoData : List<Employee> {
    }
    public class Employee {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string StateProvinceName { get; set; }
        public string PostalCode { get; set; }
        public string CountryRegionName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Title { get; set; }
        public byte[] ImageData { get; set; }
        public override string ToString() {
            return FirstName + " " + LastName;
        }
    }
    public static class EmployeesData {
        public static readonly EmployeesWithPhotoData Employees;

        static EmployeesData() {
            var stream = DiagramDemoModule.GetDataStream("EmployeesWithPhoto.xml");
            var serializer = new XmlSerializer(typeof(EmployeesWithPhotoData));
            Employees = (EmployeesWithPhotoData)serializer.Deserialize(stream);
        }
    }
}
