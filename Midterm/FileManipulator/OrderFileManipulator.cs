using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Midterm.Models;

namespace Midterm.FileManipulator
{
    public class OrderFileManipulator : IFileManipulator<Models.Order>
    {
        public string Path { get; set; }
        private List<Order> OrderCollection;

        public void WriteToFile(Order t)
        {
            string OrderString = t.route + ";" + t.category + ";" + t.amount + ";" + t.status + ";" + t.date + ";";
            using (StreamWriter sw = File.AppendText(Path))
            {
                sw.WriteLine(OrderString);
            }
        }

        public Order ConvertItem(string item)
        {
            var itemList = item.Split(';');
            var route = itemList[0];
            var category = itemList[1];
            var amount = Convert.ToInt32(itemList[2]);
            var status = Status.Proceed;
            switch (itemList[3])
            {
                case "NotOnTime":
                    status = Status.NotOnTime;
                    break;
                case "Delivered":
                    status = Status.Delivered;
                    break;
                case "Canceled":
                    status = Status.Canceled;
                    break;
                default:
                    status = Status.Proceed;
                    break;

            }
            var date = Convert.ToDateTime(itemList[4]);
            return new Order
            {
                route = route,
                category = category,
                amount = amount,
                status = status,
                date = date
            };
        }

        public List<Order> GetCollection()
        {
            if (OrderCollection == null)
            {
                var data = File.ReadAllLines(Path);
                OrderCollection = data
                    .Select(x => ConvertItem(x))
                    .ToList();
            }
            return OrderCollection;
        }
    }
}
