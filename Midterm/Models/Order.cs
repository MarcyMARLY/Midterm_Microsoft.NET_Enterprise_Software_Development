using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Midterm.Models
{
    public enum Status
    {
        NotOnTime,
        Delivered,
        Canceled,
        Proceed

    }
    public class Order
    {
        public int id { get; set; }
        public string route { get; set; }
        public string category { get; set; }
        public int amount { get; set; }
        public Status status { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
    

    }
}
