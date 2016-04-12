using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DockingDemo {
    public class DashboardViewModel : DevExpress.Mvvm.BindableBase {
        public List<Team> Teams { get; set; }
        public DashboardViewModel() {
            Teams = DashboardViewModelData.GetTeams();
            CurrentTeam = Teams[0];
        }
        public Team CurrentTeam {
            get { return GetProperty(() => CurrentTeam); }
            set { SetProperty(() => CurrentTeam, value, OnCurrentTeamChanged); }
        }
        void OnCurrentTeamChanged() {
            if(CurrentTeam.CurrentProject == null)
                CurrentTeam.CurrentProject = CurrentTeam.Projects[0];
        }
    }
    public class Team : DevExpress.Mvvm.BindableBase {
        public Team(string name) {
            Name = name;
        }
        public string Name { get; set; }
        public Person Lead { get; set; }
        public List<Project> Projects { get; set; }
        public List<Person> Staff { get; set; }

        public Project CurrentProject {
            get { return GetProperty(() => CurrentProject); }
            set { SetProperty(() => CurrentProject, value); }
        }
    }
    public class Person {
        public Person(string firstName, string lastName) {
            FirstName = firstName;
            LastName = lastName;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public ImageSource Photo { get; set; }
        public string ICQ { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
    public class Project {
        public Project(string title) {
            Title = title;
        }
        public string Title { get; set; }
        public int IssuesTotal { get; set; }
        public int BugsTotal { get; set; }
        public ImageSource History { get; set; }
    }
    #region SampleData
    public static class DashboardViewModelData {
        public static List<Team> GetTeams() {
            return CreateSampleData();
        }
        static List<Team> CreateSampleData() {
            Person lead1 = new Person("John", "Doe")
            {
                Email = "JohnDoe@team.com",
                Phone = "111-2222",
                ICQ = "77-77-77",
                JobTitle = "Team lead",
                Photo = new BitmapImage(new Uri("/DockingDemo;component/Images/Dashboard/1.png", UriKind.Relative))
            };
            Person lead2 = new Person("Jane", "Doe")
            {
                Email = "JaneDoe@team.com",
                Phone = "222-3333",
                ICQ = "88-88-88",
                JobTitle = "Team lead",
                Photo = new BitmapImage(new Uri("/DockingDemo;component/Images/Dashboard/2.png", UriKind.Relative))
            };

            Person person1 = new Person("James", "Sheppard") { JobTitle = "Developer", ICQ = "11-11-11" };
            Person person2 = new Person("Kate", "Locke") { JobTitle = "Designer", ICQ = "22-22-22" };
            Person person3 = new Person("Clarie", "Ford") { JobTitle = "Developer", ICQ = "33-33-33" };
            Person person4 = new Person("Jack", "Littleton") { JobTitle = "Developer", ICQ = "44-44-44" };
            Person person5 = new Person("Hugo", "Pace") { JobTitle = "Designer", ICQ = "55-55-55" };
            Person person6 = new Person("Helen", "Hunt") { JobTitle = "Developer", ICQ = "66-66-66" };

            Project project1 = new Project("Billing System")
            {
                BugsTotal = 15,
                IssuesTotal = 27,
                History = new BitmapImage(new Uri("/DockingDemo;component/Images/Dashboard/history1.png", UriKind.Relative))
            };
            Project project2 = new Project("Contract Management System")
            {
                BugsTotal = 15,
                IssuesTotal = 24,
                History = new BitmapImage(new Uri("/DockingDemo;component/Images/Dashboard/history2.png", UriKind.Relative))
            };
            Project project3 = new Project("Internal Software")
            {
                BugsTotal = 40,
                IssuesTotal = 50,
                History = new BitmapImage(new Uri("/DockingDemo;component/Images/Dashboard/history3.png", UriKind.Relative))
            };
            Project project4 = new Project("Company WebSite")
            {
                BugsTotal = 20,
                IssuesTotal = 22,
                History = new BitmapImage(new Uri("/DockingDemo;component/Images/Dashboard/history4.png", UriKind.Relative))
            };

            Team team1 = new Team("Bad Boys") { Lead = lead1 };
            team1.Projects = new List<Project>();
            team1.Projects.AddRange(new Project[] { project1, project2 });
            team1.Staff = new List<Person>();
            team1.Staff.AddRange(new Person[] { lead1, person1, person3, person4, person5 });

            Team team2 = new Team("Dream Girls") { Lead = lead2 };
            team2.Projects = new List<Project>();
            team2.Projects.AddRange(new Project[] { project3, project4 });
            team2.Staff = new List<Person>();
            team2.Staff.AddRange(new Person[] { lead2, person2, person6 });

            List<Team> list = new List<Team>();
            list.AddRange(new Team[] { team1, team2 });

            team1.CurrentProject = team1.Projects[0];
            team2.CurrentProject = team2.Projects[0];
            return list;
        }
    }
    #endregion SampleData
}
