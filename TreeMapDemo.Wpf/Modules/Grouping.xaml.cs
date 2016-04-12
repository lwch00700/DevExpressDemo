using System.Collections.Generic;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.TreeMap;
using System.Xml;

namespace TreeMapDemo {
    public partial class Grouping : TreeMapDemoModule {
        public Grouping() {
            InitializeComponent();
            DataContext = ViewModelSource.Create(() => new GrouppingViewModel());
        }
    }

    [POCOViewModel]
    public class GrouppingViewModel : ViewModelBase {
        public virtual XmlDocument BillionaresInfos { get; set; }
        public virtual List<string> ValueMembers { get; set; }
        public virtual List<GroupDefinitionSettings> GroupDefinitionSettings { get; set; }
        public virtual GroupDefinitionSettings GroupDefinition { get; set; }

        public GrouppingViewModel() {
            BillionaresInfos = DataLoader.LoadXmlDocumentFromResources("/Data/Billionares.xml");
            GroupDefinitionSettings = GetGroupDefinitionSettings();
            GroupDefinition = GroupDefinitionSettings[3];
        }

        List<GroupDefinitionSettings> GetGroupDefinitionSettings() {
            GroupDefinitionCollection GroupByResidence = new GroupDefinitionCollection();
            GroupByResidence.Add(new TreeMapGroupDefinition() { GroupDataMember = "Residence" });
            GroupDefinitionCollection GroupByAgeCategory = new GroupDefinitionCollection();
            GroupByAgeCategory.Add(new TreeMapGroupDefinition() { GroupDataMember = "AgeCategory" });
            GroupDefinitionCollection GroupByResidenceAndAgeCategory = new GroupDefinitionCollection();
            GroupByResidenceAndAgeCategory.Add(new TreeMapGroupDefinition() { GroupDataMember = "Residence" });
            GroupByResidenceAndAgeCategory.Add(new TreeMapGroupDefinition() { GroupDataMember = "AgeCategory" });
            return new List<GroupDefinitionSettings>() {
                new GroupDefinitionSettings() { CollectionGroupDefinitionName = "Without Groupping", ColorizeGroups = false, CollectionGroupDefinition = new GroupDefinitionCollection() },
                new GroupDefinitionSettings() { CollectionGroupDefinitionName = "Group By Residence", ColorizeGroups = true, CollectionGroupDefinition = GroupByResidence },
                new GroupDefinitionSettings() { CollectionGroupDefinitionName = "Group By Age Category", ColorizeGroups = true, CollectionGroupDefinition = GroupByAgeCategory },
                new GroupDefinitionSettings() { CollectionGroupDefinitionName = "Group By Residence And Age Category", ColorizeGroups = true, CollectionGroupDefinition = GroupByResidenceAndAgeCategory }
            };
        }
    }

    public class GroupDefinitionSettings {
        public GroupDefinitionCollection CollectionGroupDefinition { get; set; }
        public string CollectionGroupDefinitionName { get; set; }
        public bool ColorizeGroups { get; set; }

        public override string ToString() {
            return CollectionGroupDefinitionName;
        }
    }
}
