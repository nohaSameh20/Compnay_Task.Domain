using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Company.Models
{
    public class Consumption
    {
        [Key]
        public Guid Id { get; set; }
        public double Voltage { get; set; }
        public double Current { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
