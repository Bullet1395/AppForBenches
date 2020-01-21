using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForBenches.Models
{
    public class BenchmarkResult
    {
        public DateTime DateBench { get; set; }
        public double MaxFramerate { get; set; }
        public double AvgFramerate { get; set; }
        public double MinFramerate { get; set; }
        public double LowFramerate { get; set; }
        public double VeryLowFranerate { get; set; }
    }
}
