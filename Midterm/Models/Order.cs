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
        Canceled

    }
    public class Order
    {
        public Route route { get; set; }
        public Category category { get; set; }
        public int amount { get; set; }

    }
}
