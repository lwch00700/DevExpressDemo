using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartsDemo {
    public class GSP {
        public GSP(string region, string year, double product) {
            this.Region = region;
            this.Year = year;
            this.Product = product;
        }
        public double Product { get; private set; }
        public string Region { get; private set; }
        public string Year { get; private set; }
    }
}
