using System;
using System.Collections.Generic;
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
        public string route { get; set; }
        public string category { get; set; }
        public int amount { get; set; }
        public Status status { get; set; }
        public DateTime date { get; set; }

    }
}
