using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForBenches.Models
{
    public class Devices
    {
        public int IdDevice { get; set; }
        public string NameDevice { get; set; }
        public List<Games> Games { get; set; }
    }
}
