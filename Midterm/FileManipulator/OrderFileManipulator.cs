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
            return new Order
            {
                route = itemList[0],
                category = itemList[1],
                amount = Convert.ToInt32(itemList[2]),
                status = Convert.ToInt32(itemList[3]),
                date = Convert.ToDateTime(itemList[4])
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
