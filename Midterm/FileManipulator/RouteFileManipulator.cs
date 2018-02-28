using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Midterm.Models;

namespace Midterm.FileManipulator
{
    public class RouteFileManipulator : IFileManipulator<Models.Route>
    {
        public string Path { get; set; }
        private List<Route> RouteCollection;

        public Route ConvertItem(string item)
        {
            var itemList = item.Split(";");
            return new Route
            {
                routeName = itemList[0],
                price = Convert.ToInt32(itemList[1]),
                duration = Convert.ToInt32(itemList[2])
            };
        }

        public List<Route> GetCollection()
        {
            var data = File.ReadAllLines(Path);
            if (RouteCollection == null)
            {
                RouteCollection = data
                    .Select(x => ConvertItem(x))
                    .ToList();
            }
            return RouteCollection;

        }

        public void WriteToFile(Route t)
        {
            string RouteString = t.routeName + ";" + t.price + ";" + t.duration + ";";
            using (StreamWriter sw = File.AppendText(Path))
            {
                sw.WriteLine(RouteString);
            }
        }
    }
}
