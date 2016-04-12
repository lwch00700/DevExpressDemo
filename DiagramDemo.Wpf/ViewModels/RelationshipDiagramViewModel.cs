using DevExpress.Diagram.Core;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DiagramDemo.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DiagramDemo {
    public class RelationshipDiagramViewModel : ViewModelBase {
        readonly ObservableCollection<Employee> friendsCollection = new ObservableCollection<Employee>();
        readonly ObservableCollection<Employee> knownPersonsCollection = new ObservableCollection<Employee>();
        public readonly Employee[] Employees;
        public readonly Tuple<Employee, Employee, EmployeeRelationship>[] Relationships;
        Employee selectedEmployee;

        public ObservableCollection<Employee> FriendsCollection { get { return friendsCollection; } }
        public ObservableCollection<Employee> KnownPersonsCollection { get { return knownPersonsCollection; } }
        public Employee SelectedEmployee {
            get { return selectedEmployee; }
            set { SetProperty(ref selectedEmployee, value, "SelectedEmployee", OnSelectedEmployeeChanged); }
        }

        public RelationshipDiagramViewModel(Employee[] employees) {
            Employees = employees;
            Relationships = GenerateRelationships(Employees);
        }

        static Tuple<Employee, Employee, EmployeeRelationship>[] GenerateRelationships(IEnumerable<Employee> employees) {
            Random rand = new Random();
            var comparer = new DelegateEqualityComparer<Tuple<Employee, Employee>>(
                (a, b) => (a.Item1.Equals(b.Item1) && a.Item2.Equals(b.Item2)) || (a.Item1.Equals(b.Item2) && a.Item2.Equals(b.Item1)),
                (item) => item.Item1.GetHashCode() ^ item.Item2.GetHashCode());
            var pairs = employees.SelectMany(employee1 => employees.Select(employee2 => new Tuple<Employee, Employee>(employee1, employee2)))
                .Where(pair => !pair.Item1.Equals(pair.Item2)).Distinct(comparer).ToArray();
            var relations = new List<Tuple<Employee, Employee, EmployeeRelationship>>();
            pairs.ForEach((pair, index) => {
                if((index % 4) <= 1)
                    relations.Add(new Tuple<Employee, Employee, EmployeeRelationship>(pair.Item1, pair.Item2, (EmployeeRelationship)(index % 4)));
            });
            return relations.ToArray();
        }
        void OnSelectedEmployeeChanged() {
            FriendsCollection.Clear();
            KnownPersonsCollection.Clear();
            if(SelectedEmployee == null)
                return;
            GetFriends(SelectedEmployee).ForEach(friend => FriendsCollection.Add(friend));
            GetKnownPersons(SelectedEmployee).ForEach(friend => KnownPersonsCollection.Add(friend));
        }
        IEnumerable<Employee> GetFriends(Employee employee) {
            return Relationships
                .Where(employeeRelation => (employeeRelation.Item1 == employee || employeeRelation.Item2 == employee) && employeeRelation.Item3 == EmployeeRelationship.Friends)
                .Select(employeeRelation => employeeRelation.Item1 != employee ? employeeRelation.Item1 : employeeRelation.Item2);
        }
        IEnumerable<Employee> GetKnownPersons(Employee employee) {
            return Relationships
                .Where(employeeRelation => (employeeRelation.Item1 == employee || employeeRelation.Item2 == employee) && employeeRelation.Item3 == EmployeeRelationship.KnowEachOther)
                .Select(employeeRelation => employeeRelation.Item1 != employee ? employeeRelation.Item1 : employeeRelation.Item2);
        }
    }
    public enum EmployeeRelationship {
        KnowEachOther,
        Friends
    }
}
