using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Company.Models
{
    public class ConsumptionModel
    {
        public Guid Id { get; set; }
        public string Voltage { get; set; }
        public string Current { get; set; }
        public string TimeStamp { get; set; }
    }
}
