using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DevExpress.Xpf.LayoutControlDemo {
    public class PageDashboardViewModel {
        public List<Agent> Agents { get { return DevExpress.Xpf.LayoutControlDemo.Agents.DataSource; } }
        public List<Listing> Listings { get { return DevExpress.Xpf.LayoutControlDemo.Listings.DataSource; } }
    }
    public class Agent {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public ImageSource PhotoSource {
            get {
                return string.IsNullOrEmpty(Photo) ? null : new BitmapImage(new Uri(LayoutControlDemoModule.UriPrefix + Photo, UriKind.Relative));
            }
        }
    }
    public static class Agents {
        public static readonly List<Agent> DataSource =
            new List<Agent> {
                new Agent { FirstName = "Anthony", LastName = "Peterson", Phone = "(555) 444-0782", Photo = "/Images/Agents/1.jpg" },
                new Agent { FirstName = "Cindy", LastName = "Haneline", Phone = "(555) 638-9820", Photo = "/Images/Agents/2.jpg" },
                new Agent { FirstName = "Albert", LastName = "Walker", Phone = "(555) 232-2303", Photo = "/Images/Agents/3.jpg" },
                new Agent { FirstName = "Rachel", LastName = "Scott", Phone = "(555) 249-1614", Photo = "/Images/Agents/4.jpg" },
                new Agent { FirstName = "Vernon", LastName = "Rounds", Phone = "(555) 682-5403", Photo = "/Images/Agents/5.jpg" },
                new Agent { FirstName = "Andrew", LastName = "Carter", Phone = "(555) 578-3946", Photo = "/Images/Agents/6.jpg" }
            };
    }
}
