using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Mvvm;
using DevExpress.DemoData.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GridDemo {
    public class VerticalScrollingOptionsViewModel : BindableBase {
        CarsContext context = new CarsContext();
        GridControlDefinition selectedDefinition;
        public GridControlDefinition SelectedDefinition {
            get { return selectedDefinition; }
            set { SetProperty(ref selectedDefinition, value, () => SelectedDefinition); }
        }
        GridControlDefinitionCollection controlDefinitions;
        public GridControlDefinitionCollection ControlDefinitions {
            get { return controlDefinitions; }
            set {
                controlDefinitions = value;
                if(ControlDefinitions != null && ControlDefinitions.Count > 0)
                    SelectedDefinition = ControlDefinitions[0];
            }
        }
        public IEnumerable<Car> Cars {
            get { return context.Cars.ToList(); }
        }
    }
}
